using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCoin
{
    class Animation : Form1
    {
        public enum Effect { FADEIN, FADEOUT, WATERDOWN};

        private Timer actionTimer = new Timer();
        private double interval = 10;
        private double duration = 1000;
        private Form formTarget;
        private EventHandler eventHandler;

        public void setInverval(int intervalInput)
        {
            interval = intervalInput;
            actionTimer.Interval = (int)interval;
        }

        public void setDuration(int durationInput)
        {
            duration = durationInput;
        }

        public void run(Form formInput, Effect effect ) // Duration in millisecond
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
    }
}
