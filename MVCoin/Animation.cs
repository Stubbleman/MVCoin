using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCoin
{
    class Animation
    {
        public enum Effect { FADEIN, FADEOUT, WATERDOWN, FLYTO};

        private Timer actionTimer = new Timer();
        private float interval = 5; // Default 10
        private float duration = 100; // Default 1000
        private float speed = 1; // Default 1
        private Form formTarget;
        private EventHandler eventHandler;
        private bool isDone = false;
        private Point dst = // Default screen center
            new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2); 

        public void setInverval(int intervalInput)
        {
            interval = intervalInput;
            actionTimer.Interval = (int)interval;
        }

        public void setDuration(int durationInput)
        {
            duration = durationInput;
        }

        public void setFlyToDst(Point dstInput)
        {
            dst = dstInput;
        }

        public void run(Form formInput, Effect effect) // Duration in millisecond
        {
            formTarget = formInput;            

            switch(effect)
            {
                case Effect.FADEIN :
                    timerStart(fadeIn);
                    break;

                case Effect.FADEOUT :
                    timerStart(fadeOut);
                    break;

                case Effect.WATERDOWN :
                    timerStart(waterDown);
                    break;

                case Effect.FLYTO:                    
                    timerStart(flyTo);
                    break;
            }                               
            
        }

        private void timerStart(EventHandler eventInput)
        {
            actionTimer.Tick -= eventHandler;
            eventHandler = new EventHandler(eventInput);
            actionTimer.Tick += eventHandler;
            actionTimer.Start();
        }

        private void timerStop()
        {
            actionTimer.Stop();
        }

        private float rss(float x, float y) // root-square-sum
        {
            return (float)Math.Sqrt(Math.Pow(x - y, 2) + Math.Pow(x - y, 2));
        }
        
        private void fadeIn(object sender, EventArgs e)
        {
            if (formTarget.Opacity >= 1)
                timerStop();                
            else
                formTarget.Opacity += interval / duration;
        }

        private void fadeOut(object sender, EventArgs e)
        {
            if (formTarget.Opacity <= 0)
                timerStop();
            else
                formTarget.Opacity -= interval / duration;
        }

        private void waterDown(object sender, EventArgs e)
        {
            if (formTarget.Opacity <= 0.2)
                timerStop();
            else
                formTarget.Opacity -= interval / duration;
        }

        private void flyTo(object sender, EventArgs e) // dst: destination
        {            
            PointF loc = formTarget.Location; // current location
            float distance = rss(dst.X - loc.X, dst.Y - loc.Y);
            float dirX = (dst.X - loc.X) / distance;
            float dirY = (dst.Y - loc.Y) / distance;
            if (!isDone)
            {
                speed = distance / duration;
                isDone = true;
            }

            PointF step = new PointF(dirX * speed, dirY * speed);

            if (distance <= 0)
            {
                actionTimer.Stop();
            }
            else if(distance < rss(step.X,step.Y))
            {
                formTarget.Location = dst;
            }
            else
                formTarget.Location = new Point((int)Math.Ceiling(loc.X + step.X), (int)Math.Ceiling(loc.Y + step.Y));
        }


    }
}
