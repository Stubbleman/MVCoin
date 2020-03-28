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
    public partial class Satellite : Form
    {
        private Animation effectAni = new Animation();
        private FormControl formController = new FormControl();
        private TaskLauncher taskLauncher = new TaskLauncher();
        private Point offset; // Offset vector from center form
        private bool mouseEntered = false;
        private taskName task;
        private int serialNum = 0;

        public Satellite()
        {
            InitializeComponent();
        }

        private void Satellite_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Brown;
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0;
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

        public Point getOffset()
        {
            return offset;
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

        private void Satellite_MouseClick(object sender, MouseEventArgs e)
        {
            taskLauncher.launch(task); 
        }

        /*
        public void waterDown()
        {
            effectAni.setDuration(100);
            effectAni.setInverval(2);
            effectAni.run(this, Animation.Effect.WATERDOWN);
        }
        */
    }
}
