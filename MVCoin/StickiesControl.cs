using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
        public string m_command;
        public bool m_replyreceived;
        public string m_reply;

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


    }
}
