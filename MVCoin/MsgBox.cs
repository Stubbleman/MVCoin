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
    public partial class MsgBox : Form
    {
        private Point mouse_offset;
        Animation animator = new Animation();

        public MsgBox()
        {
            InitializeComponent();
        }

        private void MsgBox_Load(object sender, EventArgs e)
        {
            animator.setDuration(50);
            animator.setInverval(5);         
            animator.run(this, Animation.Effect.FADEIN);
        }
        
        public void setText(string text)
        {
            this.richTextBox1.Text = text;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point upRight = new Point(this.richTextBox1.Size.Width, 0);
            int marginLeft = upRight.X - pictureBox1.Width - pictureBox1.Margin.Top * 2;
            int marginDown = upRight.Y + pictureBox1.Height + pictureBox1.Margin.Right * 2;
            if (e.X >= marginLeft && e.Y <= marginDown)
            {
                pictureBox1.Visible = true;
            }
            else
            {
                pictureBox1.Visible = false;
            }

            if (e.Button == MouseButtons.Middle)
            {                
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos;
            }

        }

        private void richTextBox1_MouseLeave(object sender, EventArgs e)
        {            
            animator.run(this, Animation.Effect.WATERDOWN);
        }

        private void richTextBox1_MouseEnter(object sender, EventArgs e)
        {
            animator.run(this, Animation.Effect.FADEIN);
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);            
        }
    }
}
