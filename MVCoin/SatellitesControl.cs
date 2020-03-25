using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MVCoin
{
    class SatellitesControl
    {
        public enum Cmd {CREATE, EXPAND, COLLAPSE, MOVE, WATERDOWN, FADEIN };
        
        Form mainForm = new Form();
        FormControl formController = new FormControl();
        List<Satellite> satelliteList = new List<Satellite>();
        List<Animation> satelliteAniList = new List<Animation>();
        List<Point> satelliteOffsetList = new List<Point>();
        int satelliteNumber = 6;
        int i = 0;

        public SatellitesControl(Form mainFormInput)
        {
            mainForm = mainFormInput;
        }

        public void actionCmd(Cmd cmd)
        {
            for (i = 0; i < satelliteNumber; i++)
            {
                switch(cmd)
                {
                    case Cmd.CREATE:
                        create();
                        break;

                    case Cmd.EXPAND:
                        expand();
                        break;

                    case Cmd.COLLAPSE:
                        collapse();
                        break;

                    case Cmd.MOVE:
                        move();
                        break;

                    case Cmd.WATERDOWN:
                        waterDown();
                        break;

                    case Cmd.FADEIN:
                        fadeIn();
                        break;
                }
            }
        }

        private void create()
        {
            satelliteList.Add(new Satellite());
            satelliteList[i].setBackgroundeImg(Image.FromFile("../../Icon/satellite2.png"));
            satelliteList[i].Show();
            formController.setSize(mainForm.Size.Width, mainForm.Size.Height, satelliteList[i]);
            formController.ChangeFormSize(50, satelliteList[i]);

            satelliteAniList.Add(new Animation());
            satelliteAniList[i].setDuration(10);
            satelliteAniList[i].setInverval(1);
        }

        private void expand()
        {
            formController.setCenter(formController.getCenter(mainForm), satelliteList[i]);
            satelliteAniList[i].setFlyToDst(orbitCalculate(satelliteNumber, i));
            satelliteAniList[i].run(satelliteList[i], Animation.Effect.FLYTO);
            satelliteList[i].appear();

            Point tempPoint = satelliteAniList[i].getFlyToDst();
            tempPoint.Offset(-formController.getCenter(mainForm).X, -formController.getCenter(mainForm).Y);
            satelliteList[i].setOffset(tempPoint);
        }

        private Point orbitCalculate(int totalNumber, int sequenceNumber)
        {
            Point satellitePoint = new Point();
            float radius = 50 * 2;
            float angleStep = 360 / totalNumber; // Unit: angle

            Point fisrtPoint = new Point(formController.getCenter(mainForm).X + (int)radius
                    , formController.getCenter(mainForm).Y);

            if (sequenceNumber == 0) // First pint at right side
            {
                satellitePoint = fisrtPoint;
            }
            else if (sequenceNumber % 2 == 1) // Odd number, above
            {
                int num = (sequenceNumber + 1) / 2;
                float angleA = angleStep * num;
                satellitePoint = MathF.Rotate(fisrtPoint, formController.getCenter(mainForm), angleA);
            }
            else if (sequenceNumber % 2 == 0)  // Even number, under
            {
                int num = sequenceNumber / 2;
                float angleA = -angleStep * num;
                satellitePoint = MathF.Rotate(fisrtPoint, formController.getCenter(mainForm), angleA);
            }

            return satellitePoint;
        }

        private void collapse()
        {
            satelliteAniList[i].setFlyToDst(formController.getCenter(mainForm));
            satelliteAniList[i].run(satelliteList[i], Animation.Effect.FLYTO);
            satelliteList[i].disappear();
        }

        private void move()
        {
            Point tempPoint = formController.getCenter(mainForm);
            tempPoint.Offset(satelliteList[i].getOffset());
            formController.setCenter(tempPoint, satelliteList[i]);
        }

        private void waterDown()
        {
            satelliteAniList[i].run(satelliteList[i], Animation.Effect.WATERDOWN);
        }

        private void fadeIn()
        {
            satelliteAniList[i].run(satelliteList[i], Animation.Effect.FADEIN);
        }

        private void fadeOut()
        {
            satelliteAniList[i].run(satelliteList[i], Animation.Effect.FADEOUT);
        }

    }
}
