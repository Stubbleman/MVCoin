using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCoin
{
    public enum taskName {STICKIES };

    class TaskLauncher
    {
        private StickiesControl stiController = new StickiesControl();

        public void launch(taskName task)
        {
            switch(task)
            {
                case taskName.STICKIES:
                    string reply = stiController.SendToStickies("do new sticky HaHaHa");
                    break;
            }
        }
    }
}
