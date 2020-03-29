﻿using System;
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
    public partial class Satellite : Form
    {
        private Animation effectAni = new Animation();
        private FormControl formController = new FormControl();
        private TaskLauncher taskLauncher;
        private Point mouse_offset;
        private Point offset; // Offset vector from center form
        private bool mouseEntered = false;
        private bool isolatedState = false; // when the satellite is dragged individually, this = true
        private taskName task;
        private int serialNum = 0;

        public Satellite()
        {
            InitializeComponent();
        }

        private void Satellite_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(250, 0, 0);
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0;
            taskLauncher = new TaskLauncher(this);
        }

        public void setBackgroundeImg(Image bgImg)
        {
            this.BackgroundImage = bgImg;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        // Set center offset from center form
        public void setOffset(Point offsetInput)
        {
            offset = offsetInput;
        }

        public void setSerailNum(int numberInput)
        {
            serialNum = numberInput;
        }

        public void setTask(taskName taskInput)
        {
            task = taskInput;
        }

        public void setIsolatedState(bool state)
        {
            isolatedState = state;
        }

        public Point getOffset()
        {
            return offset;
        }

        public bool isIsolated()
        {
            return isolatedState;
        }

        public void appear()
        {
            effectAni.setDuration(50);
            effectAni.setInverval(5);
            effectAni.run(this, Animation.Effect.FADEIN);
        }

        public void disappear()
        {
            effectAni.setDuration(50);
            effectAni.setInverval(2);
            effectAni.run(this, Animation.Effect.FADEOUT);
        }

        private void Satellite_MouseEnter(object sender, EventArgs e)
        {
            if (!mouseEntered)
            {
                effectAni.run(this, Animation.Effect.FADEIN);
                mouseEntered = true;
            }
        }

        private void Satellite_MouseLeave(object sender, EventArgs e)
        {
            if (mouseEntered)
            {
                effectAni.run(this, Animation.Effect.WATERDOWN);
                mouseEntered = false;
            }
        }

        private void Satellite_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void Satellite_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos;
                isolatedState = true;
                /*** This move too LAG!!!
                if(task == taskName.STICKIES)
                {
                    StickiesControl stiControl = new StickiesControl(this);
                    if(!stiControl.isExpanded())
                        stiControl.moveSticky();
                }
                */
            }
        }

        private void Satellite_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            taskLauncher.launch(task);
        }
    }
}
