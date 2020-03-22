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

            formController = new FormControl(100, 115);
            formController.myForm = this;
            //formController.setWidth(100);
            //formController.setHeighth(115);
            formController.ChangeFormSize(100);
            //ChangeFormSize(100); // Change size by given percentage
            this.Location = new Point(Cursor.Position.X - this.Width / 2, Cursor.Position.Y - this.Height / 2);

            animater = new Animation();
            animater.setDuration(100);
            animater.setInverval(5);
            animater.run(this, Animation.Effect.FADEIN);
            
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
            if(e.Button == MouseButtons.Right)
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
        /*
        public void ChangeFormSize(int percentage)
        {
            const int width = 100, height = 115;
            this.Size = new Size(width * percentage / 100, height * percentage / 100);
        }
        */
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
            //animater.run(this, Animation.Effect.FADEOUT);
            //stickyCommand("do new sticky Test");


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

        private void stickyCommand(string str)
        {
            /***Send stickies command***/            
            string reply = stickiesController.SendToStickies(str);
            if (reply.Length == 0)
                MessageBox.Show("ERROR");
            //else
                //MessageBox.Show(reply);
            /***Send stickies command***/
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            List<Satellite> satellites = new List<Satellite>();
            int satelliteNumber = 3;
            for(int i = 0; i<satelliteNumber; i++)
            {
                satellites.Add(new Satellite());
                satellites[i].setBackgroundeImg(Image.FromFile("../../Icon/satellite1.png"));
                satellites[i].Show();
            }

        }
    }
}
