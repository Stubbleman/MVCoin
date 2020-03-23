using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCoin
{
    public partial class Form1 : Form
    {
        private Point mouse_offset;
        private Animation animater;
        private StickiesControl stickiesController = new StickiesControl();
        private FormControl formController;
        private bool mouseEnter = false;
        private bool expand = false;
        List<Satellite> satelliteList = new List<Satellite>();
        List<Animation> satelliteAniList = new List<Animation>();
        int satelliteNumber = 9;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Brown;
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0;

            formController = new FormControl();
            formController.myForm = this;
            formController.setSize(100, 115);
            //formController.setWidth(100);
            //formController.setHeighth(115);
            formController.ChangeFormSize(100);
            //ChangeFormSize(100); // Change size by given percentage
            //this.Location = new Point(Cursor.Position.X - this.Width / 2, Cursor.Position.Y - this.Height / 2);
            formController.setCenter(Cursor.Position);

            animater = new Animation();
            animater.setDuration(100);
            animater.setInverval(5);
            animater.run(this, Animation.Effect.FADEIN);

            createSatellites();
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                Location = mousePos;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(MousePosition);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowForm();
        }

        private void ShowForm()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //如果目前是縮小狀態，才恢復成一般大小的視窗
                this.Show();
                this.WindowState = FormWindowState.Normal;

                // Activate the form.
                this.Activate();
                this.Focus();
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Point mousePos = Control.MousePosition;
                contextMenuStrip1.Show(MousePosition);
            }
        }

        /***Change form size***/

        private void toolStripMenuItem300_Click(object sender, EventArgs e)
        {
            formController.ChangeFormSize(300);
        }

        private void toolStripMenuItem200_Click(object sender, EventArgs e)
        {
            formController.ChangeFormSize(200);
        }

        private void toolStripMenuItem100_Click(object sender, EventArgs e)
        {
            formController.ChangeFormSize(100);
        }

        private void toolStripMenuItem75_Click(object sender, EventArgs e)
        {
            formController.ChangeFormSize(75);
        }

        private void toolStripMenuItem50_Click(object sender, EventArgs e)
        {
            formController.ChangeFormSize(50);
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            formController.ChangeFormSize(25);
        }
        
        /***Change form size***/

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //animater.setFlyToDst(new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2));
            animater.run(this, Animation.Effect.FLYTO);
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            if(!mouseEnter)
            {
                animater.run(this, Animation.Effect.WATERDOWN);
                mouseEnter = !mouseEnter;
            }
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            if (mouseEnter)
            {
                animater.run(this, Animation.Effect.FADEIN);
                mouseEnter = !mouseEnter;
                //MessageBox.Show("MouseEnter");
            }
                
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(!expand)
            {
                expandSatellite();
                expand = true;
            }
            else if(expand)
            {
                collapseSatellite();
                expand = false;
            }            
        }

        private void stickyCommand(string str)
        {     
            string reply = stickiesController.SendToStickies(str);
            if (reply.Length == 0)
                MessageBox.Show("ERROR");
            //else
                //MessageBox.Show(reply);
        }

        private void createSatellites()
        {
            for (int i = 0; i < satelliteNumber; i++)
            {
                satelliteList.Add(new Satellite());
                satelliteList[i].setBackgroundeImg(Image.FromFile("../../Icon/satellite1.png"));
                satelliteList[i].Show();
                formController.setSize(this.Size.Width, this.Size.Height, satelliteList[i]);
                formController.ChangeFormSize(50, satelliteList[i]);


                satelliteAniList.Add(new Animation());
                satelliteAniList[i].setDuration(10);
                satelliteAniList[i].setInverval(1);                
            }
        }

        private void expandSatellite()
        {            
            for (int i = 0; i < satelliteNumber; i++)
            {
                formController.setCenter(formController.getCenter(), satelliteList[i]);
                satelliteAniList[i].setFlyToDst(orbitCalculate(satelliteNumber, i));
                satelliteAniList[i].run(satelliteList[i], Animation.Effect.FLYTO);
                satelliteList[i].appear();
            }
        }

        private void collapseSatellite()
        {
            for (int i = 0; i < satelliteNumber; i++)
            {
                satelliteAniList[i].setFlyToDst(formController.getCenter(this));
                satelliteAniList[i].run(satelliteList[i], Animation.Effect.FLYTO);
                satelliteList[i].disappear();               
            }
        }

        private Point orbitCalculate(int totalNumber, int sequenceNumber)
        {
            Point satellitePoint = new Point();
            float radius = 50 * 2;
            float angleStep = 360 / totalNumber; // Unit: angle

            Point fisrtPoint = new Point(formController.getCenter().X + (int)radius
                    , formController.getCenter().Y);

            if (sequenceNumber == 0) // First pint at right side
            {
                satellitePoint = fisrtPoint;
            }
            else if (sequenceNumber % 2 == 1) // Odd number, above
            {
                int num = (sequenceNumber + 1) / 2;
                float angleA = angleStep * num;
                satellitePoint = MathF.Rotate(fisrtPoint, formController.getCenter(), angleA);
            }
            else if (sequenceNumber % 2 == 0)  // Even number, under
            {
                int num = sequenceNumber / 2;
                float angleA = - angleStep * num;
                satellitePoint = MathF.Rotate(fisrtPoint, formController.getCenter(), angleA);
            }

            return satellitePoint;
        }
        

        
    }
}
