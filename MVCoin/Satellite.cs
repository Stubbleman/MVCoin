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

        public void appear()
        {
            effectAni = new Animation();
            effectAni.setDuration(100);
            effectAni.setInverval(5);
            effectAni.run(this, Animation.Effect.FADEIN);
        }

        public void disappear()
        {
            effectAni = new Animation();
            effectAni.setDuration(100);
            effectAni.setInverval(2);
            effectAni.run(this, Animation.Effect.FADEOUT);
        }
    }
}
