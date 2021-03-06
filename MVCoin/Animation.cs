﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using tmr = System.Timers;

namespace MVCoin
{
    class Animation
    {
        public enum Effect { FADEIN, FADEOUT, WATERDOWN, FLYTO};

        private tmr::Timer actionTimer = new tmr::Timer();
        private float interval = 5; // Default 10
        private float duration = 100; // Default 1000
        private float speed = 1; // Default 1
        private double opacity = 0.25;
        private Form formTarget;
        //private List<Satellite> satelliteTargetList;
        //private EventHandler eventHandler;
        private tmr::ElapsedEventHandler elapseEventHandler;
        //private List<EventHandler> eventHandlerList;
        private bool isDone = false;
        private Point dst = // Default screen center
            new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2); 

        public void setInverval(int intervalInput)
        {
            interval = intervalInput;
            actionTimer.Interval = interval;
        }

        public void setDuration(int durationInput)
        {
            duration = durationInput;
        }

        public void setFlyToDst(Point dstInput)
        {
            dst = dstInput;
        }

        public void setOpacity(double opacityInput)
        {
            opacity = opacityInput;
        }

        public Point getFlyToDst()
        {
            return dst;
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
        /*
        public void run(List<Satellite> satellireInputList, Effect effect) // Duration in millisecond
        {
            eventHandlerList = new List<EventHandler>();
            satelliteTargetList = satellireInputList;

            switch (effect)
            {
                case Effect.FADEIN:
                    timerStartMulti(fadeIn);
                    break;

                case Effect.FADEOUT:
                    timerStartMulti(fadeOut);
                    break;

                case Effect.WATERDOWN:
                    timerStartMulti(waterDown);
                    break;

                case Effect.FLYTO:
                    timerStartMulti(flyTo);
                    break;
            }

        }
        */
        private void timerStart(tmr.ElapsedEventHandler eventInput)
        {
            actionTimer.Elapsed -= elapseEventHandler; // Clear previous event            
            elapseEventHandler = new tmr::ElapsedEventHandler(eventInput); // Create new event
            actionTimer.Elapsed += elapseEventHandler; // Add new event
            actionTimer.Start();
        }
        /*
        private void timerStartMulti(EventHandler eventInput)
        {
            // Clear previous events
            if(eventHandlerList.Count > 0)
            {
                for (int i = 0; i < satelliteTargetList.Count; i++)
                {
                    actionTimer.Tick -= eventHandlerList[i];
                }
            }

            // Add new events
            for (int i = 0; i < satelliteTargetList.Count; i++)
            {
                formTarget = satelliteTargetList[i];
                eventHandlerList.Add(new EventHandler(eventInput));
                actionTimer.Tick += eventHandlerList[i];
            }

            actionTimer.Start();
        }
        */
        private void timerStop()
        {
            actionTimer.Stop();
        }
        
        private void fadeIn(object sender, tmr::ElapsedEventArgs e)
        {
            formTarget.InvokeIfRequired(() => 
            {
                if (formTarget.Opacity >= 1)
                    timerStop();
                else
                    formTarget.Opacity += interval / duration;
            });
        }

        private void fadeOut(object sender, tmr::ElapsedEventArgs e)
        {
            formTarget.InvokeIfRequired(() =>
            {
                if (formTarget.Opacity <= 0)
                    timerStop();
                else
                    formTarget.Opacity -= interval / duration;
            });

        }

        private void waterDown(object sender, tmr::ElapsedEventArgs e)
        {
            formTarget.InvokeIfRequired(() =>
            {
                if (formTarget.Opacity <= opacity)
                    timerStop();
                else
                    formTarget.Opacity -= interval / duration;
            });
        }

        private void flyTo(object sender, tmr::ElapsedEventArgs e) // dst: destination
        {
            formTarget.InvokeIfRequired(() =>
            {
                FormControl formController = new FormControl();
                PointF loc = formController.getCenter(formTarget); // current center location
                float distance = MathF.RSS(dst.X - loc.X, dst.Y - loc.Y);
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
                else if (distance < MathF.RSS(step.X, step.Y))
                {
                    formController.setCenter(dst, formTarget);
                }
                else
                {
                    formController.setCenter(new Point((int)Math.Ceiling(loc.X + step.X), (int)Math.Ceiling(loc.Y + step.Y)), formTarget);
                }
            });
        }
    }
}

public static class Extension
{
    //非同步委派更新UI
    public static void InvokeIfRequired(
        this Control control, MethodInvoker action)
    {
        if (control.InvokeRequired)//在非當前執行緒內 使用委派
        {
            control.Invoke(action);
        }
        else
        {
            action();
        }
    }
}