using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCoin
{
    class FormControl
    {
        public Form myForm = new Form();
        static int width, height;

        public FormControl()
        {

        }

        public void setSize(int widthInput, int heightInput)
        {
            width = widthInput;
            height = heightInput;
            myForm.Size = new Size(width, height);
        }

        public void setCenter(int x, int y)
        {
            myForm.Location = new Point(x - myForm.Width / 2, y - myForm.Height / 2);
        }

        public Point getCenter()
        {
            return new Point(myForm.Location.X + myForm.Width / 2, myForm.Location.Y + myForm.Height / 2);
        }

        public void ChangeFormSize(int percentage)
        {
            myForm.Size = new Size(width * percentage / 100, height * percentage / 100);
        }


    }
}
