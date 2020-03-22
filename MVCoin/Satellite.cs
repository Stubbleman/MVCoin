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
        private Animation effectAni;
        private Animation motionAni;
        private FormControl formController;

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

            formController = new FormControl();
            formController.myForm = this;
            formController.setSize(100, 115);
            formController.ChangeFormSize(50);
        }

        public void setBackgroundeImg(Image bgImg)
        {
            this.BackgroundImage = bgImg;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Satellite_Shown(object sender, EventArgs e)
        {
            effectAni = new Animation();
            effectAni.setDuration(100);
            effectAni.setInverval(5);
            effectAni.run(this, Animation.Effect.FADEIN);

            motionAni = new Animation();
            motionAni.setDuration(100);
            motionAni.setInverval(1);
            motionAni.run(this, Animation.Effect.FLYTO);
        }
    }
}
