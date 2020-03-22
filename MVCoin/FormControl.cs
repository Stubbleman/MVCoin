using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCoin
{
    class FormControl : Form
    {
        public Form myForm = new Form();
        static int width, height;

        public FormControl(int widthInput, int heightInput)
        {
            width = widthInput;
            height = heightInput;
        }

        public void setWidth(int widthInput)
        {
            width = widthInput;
        }

        public void setHeighth(int heightInput)
        {
            height = heightInput;
        }

        public void ChangeFormSize(int percentage)
        {
            myForm.Size = new Size(width * percentage / 100, height * percentage / 100);
        }
    }
}
