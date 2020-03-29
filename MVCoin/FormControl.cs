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

        public FormControl(Size size)
        {
            width = size.Width;
            height = size.Height;
        }

        public void setSize(int widthInput, int heightInput)
        {
            myForm.Size = new Size(width, height);
        }

        public void setSize(int widthInput, int heightInput, Form myForm)
        {
            myForm.Size = new Size(width, height);
        }

        public void setCenter(Point location)
        {
            myForm.Location = new Point(location.X - myForm.Width / 2, location.Y - myForm.Height / 2);
        }

        public void setCenter(Point location, Form myForm)
        {
            myForm.Location = new Point(location.X - myForm.Width / 2, location.Y - myForm.Height / 2);
        }

        public Point getCenter()
        {
            return new Point(myForm.Location.X + myForm.Width / 2, myForm.Location.Y + myForm.Height / 2);
        }

        public Point getCenter(Form myForm)
        {
            return new Point(myForm.Location.X + myForm.Width / 2, myForm.Location.Y + myForm.Height / 2);
        }

        public void scaleFormSize(double percentage)
        {
            Point center = getCenter();
            myForm.Size = new Size((int)Math.Round(width * percentage), (int)Math.Round(height * percentage));
            setCenter(center);
        }

        public void scaleFormSize(double percentage,Form formInput)
        {
            Point center = getCenter();
            formInput.Size = new Size((int)Math.Round(formInput.Size.Width * percentage), (int)Math.Round(formInput.Size.Height * percentage));
            setCenter(center, formInput);
        }


    }
}
