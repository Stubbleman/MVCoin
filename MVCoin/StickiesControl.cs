using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MVCoin
{
    class StickiesControl : Form
    {
        [DllImport("user32", EntryPoint = "FindWindowExA")]
        private static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);

        [DllImport("user32", EntryPoint = "SendMessageA")]
        private static extern int ZSendMessage(int Hwnd, int wMsg, int wParam, int lParam);

        private const short WM_COPYDATA = 0x4A;

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        public int m_commandref;
        //public string m_command;
        public bool m_replyreceived;
        public string m_reply;
                
        private FormControl formController = new FormControl();
        private Form satellite;
        string[] idArray;
        bool isExpandState = false;


        public StickiesControl(Form satelliteInput)
        {
            satellite = satelliteInput;
        }

        public int VarPtr(object e)
        {
            GCHandle GC = GCHandle.Alloc(e, GCHandleType.Pinned);
            int gc = GC.AddrOfPinnedObject().ToInt32();
            GC.Free();
            return gc;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    COPYDATASTRUCT CD = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
                    byte[] B = new byte[CD.cbData];
                    IntPtr lpData = CD.lpData;
                    Marshal.Copy(lpData, B, 0, CD.cbData);
                    string strData = Encoding.Default.GetString(B);
                    if (CD.dwData == (IntPtr)m_commandref)
                    {
                        // A reply, so store it
                        m_reply = strData;
                        m_replyreceived = true;
                    }
                    else
                    {
                        // It's an event
                        // AddLines("-", "-", strData);
                    }
                    break;

                default:
                    // let the base class deal with it
                    base.WndProc(ref m);
                    break;
            }
        }

        public string SendToStickies(string str)
        {
            int hWnd = 0;
            hWnd = FindWindowEx(0, hWnd, null, "ZhornSoftwareStickiesMain");

            if (hWnd == 0)
                return "";      // stickies not found
            else
            {
                // Add API code
                str = "api " + str;

                // Generate a unique number for this command
                m_commandref++;

                // Reset flag
                m_replyreceived = false;

                // Send the message to Stickies
                IntPtr ptr = (IntPtr)Marshal.StringToHGlobalAnsi(str);
                COPYDATASTRUCT cs = new COPYDATASTRUCT();
                cs.dwData = (IntPtr)m_commandref;
                cs.lpData = ptr;
                cs.cbData = str.Length + 1;
                int ret = ZSendMessage(hWnd, WM_COPYDATA, (int)Handle, VarPtr(cs));

                // Wait for a reply for a second
                int count = 0;
                while (count < 20)
                {
                    System.Threading.Thread.Sleep(50);
                    if (m_replyreceived)
                        return m_reply;
                    count++;
                }

                // Free memory
                Marshal.FreeHGlobal(ptr);

                // We timed out so return nothing to indicate an error
                return "";
            }
        }

        public string[] getIdListDesktop()
        {
            string reply = SendToStickies("get list desktop");
            string[] replyArray = reply.Split(' ');
            string[] idArray = replyArray[1].Split(',');
            return idArray;
        }

        public bool isExpanded()
        {
            return isExpandState;
        }

        public void moveSticky()
        {
            idArray = getIdListDesktop();
            string id;
            for (int i = 0; i < idArray.Count() - 1; i++)
            {
                id = idArray[i];
                Point stiSize = getStiSize(id);
                Point stiPos = positionCalculate(idArray.Count(), i, stiSize);
                SendToStickies("do desktop " + id.ToString() + " rolled");
                SendToStickies("set desktop " + id.ToString() + " position " + stiPos.X.ToString() + "," + stiPos.Y.ToString());
            }
        }

        public void showDesktopSticky()
        {
            SendToStickies("do hideall");            
            idArray = getIdListDesktop();
            string id;            
            for (int i = 0; i < idArray.Count() - 1; i++)
            {
                id = idArray[i];
                SendToStickies("do desktop " + id.ToString() + " rolled");
                Point stiSize = getStiSize(id);
                Point stiPos = positionCalculate(idArray.Count(), i, stiSize);                                
                SendToStickies("set desktop " + id.ToString() + " position " + stiPos.X.ToString() + "," + stiPos.Y.ToString());
            }
            SendToStickies("do showall");
            isExpandState = true;
        }

        public void hideAllSticky()
        {
            SendToStickies("do hideall");
        }
        private Point positionCalculate(int totalNumber, int sequenceNumber, Point stiSize)
        {
            Point stiPoint = new Point();
            float shiftH = stiSize.X + satellite.Size.Width / 2 + 10; // Horizontal shift
            float space = stiSize.Y + 10; // space between two stikies in vertical 
            Point formCenter = formController.getCenter(satellite);


            Point fisrtPoint = new Point(formCenter.X - (int)shiftH, formCenter.Y - stiSize.Y / 2);

            if (sequenceNumber == 0) // First pint at right side
            {
                stiPoint = fisrtPoint;
            }
            else if (sequenceNumber % 2 == 1) // Odd number, above
            {
                int num = (sequenceNumber + 1) / 2;
                float shiftV = space * num; // vertical shift
                stiPoint = fisrtPoint;
                stiPoint.Offset(0, (int)shiftV);
            }
            else if (sequenceNumber % 2 == 0)  // Even number, under
            {
                int num = sequenceNumber / 2;
                float shiftV = -space * num;
                stiPoint = fisrtPoint;
                stiPoint.Offset(0, (int)shiftV);
            }

            return stiPoint;
        }

        private Point getStiSize(string id)
        {            
            string sizeReply = SendToStickies("get desktop " + id.ToString() + " size");
            string[] sizeStr = sizeReply.Split(' '); // = {"001", "x,y"}
            Point stiSize = new Point(Convert.ToInt32(sizeStr[1].Split('x')[0]), Convert.ToInt32(sizeStr[1].Split('x')[1]));

            return stiSize;
        }
    }
}
