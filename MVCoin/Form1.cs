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
        private FormControl formController;
        private SatellitesControl satellitesController;
        private bool mouseEntered = false;
        private bool expand = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(250, 0, 0);
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0;

            formController = new FormControl(new Size(100, 115));
            formController.myForm = this;
            formController.setSize(100, 115);
            formController.scaleFormSize(1);
            //formController.setCenter(Cursor.Position);
            formController.setCenter(new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2));

            animater = new Animation();
            animater.setDuration(100);
            animater.setInverval(5);
            animater.run(this, Animation.Effect.FADEIN);

            satellitesController = new SatellitesControl(this);
            satellitesController.actionCmd(SatellitesControl.Cmd.CREATE);
            satellitesController.actionCmd(SatellitesControl.Cmd.PASSTASK);
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
                this.Location = mousePos;

                if (expand)
                    satellitesController.actionCmd(SatellitesControl.Cmd.MOVE);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(MousePosition);
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            if (mouseEntered)
            {
                animater.run(this, Animation.Effect.WATERDOWN);
                mouseEntered = false;

                if (expand)
                    satellitesController.actionCmd(SatellitesControl.Cmd.WATERDOWN);
            }
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            if (!mouseEntered)
            {
                animater.run(this, Animation.Effect.FADEIN);
                mouseEntered = true;

                if (expand)
                    satellitesController.actionCmd(SatellitesControl.Cmd.FADEIN);
            }

        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!expand)
            {
                satellitesController.actionCmd(SatellitesControl.Cmd.EXPAND);
                expand = true;
            }
            else if (expand)
            {
                satellitesController.actionCmd(SatellitesControl.Cmd.COLLAPSE);
                expand = false;
            }
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
            formController.scaleFormSize(3);
            satellitesController.setScale(3);
            satellitesController.actionCmd(SatellitesControl.Cmd.SCALE);
        }

        private void toolStripMenuItem200_Click(object sender, EventArgs e)
        {
            formController.scaleFormSize(2);
            satellitesController.setScale(2);
            satellitesController.actionCmd(SatellitesControl.Cmd.SCALE);
        }

        private void toolStripMenuItem100_Click(object sender, EventArgs e)
        {
            formController.scaleFormSize(1);
            satellitesController.setScale(1);
            satellitesController.actionCmd(SatellitesControl.Cmd.SCALE);
        }

        private void toolStripMenuItem75_Click(object sender, EventArgs e)
        {
            formController.scaleFormSize(0.75);
            satellitesController.setScale(0.75);
            satellitesController.actionCmd(SatellitesControl.Cmd.SCALE);
        }

        private void toolStripMenuItem50_Click(object sender, EventArgs e)
        {
            formController.scaleFormSize(0.5);
            satellitesController.setScale(0.5);
            satellitesController.actionCmd(SatellitesControl.Cmd.SCALE);
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            formController.scaleFormSize(0.25);
            satellitesController.setScale(0.25);
            satellitesController.actionCmd(SatellitesControl.Cmd.SCALE);
        }

        /***Change form size***/

        /***Change opacity***/

        private void toolStripMenuItemOpacity100_Click(object sender, EventArgs e)
        {
            animater.setOpacity(1);
        }

        private void toolStripMenuItemOpacity75_Click(object sender, EventArgs e)
        {
            animater.setOpacity(0.75);
        }

        private void toolStripMenuItemOpacity50_Click(object sender, EventArgs e)
        {
            animater.setOpacity(0.5);
        }

        private void toolStripMenuItemOpacity25_Click(object sender, EventArgs e)
        {
            animater.setOpacity(0.25);
        }

        /***Change opacity***/

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //animater.setFlyToDst(new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2));
            //animater.run(this, Animation.Effect.FLYTO);
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

        // "About" button
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.setName("MVCoin");
            aboutBox.setVersion("Ver. 0.8.0");
            aboutBox.setCopyright("Copyright 2020");
            aboutBox.setCompany("MAKER VILLAGE");
            aboutBox.setDescription("Make Maker Village great again~~~LOL");
            aboutBox.Show();
        }
    }
}
