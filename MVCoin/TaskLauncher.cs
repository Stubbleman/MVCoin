using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCoin
{
    public enum taskName {NONE, STICKIES };

    class TaskLauncher
    {
        
        private Form satellite;

        public TaskLauncher(Form sate)
        {
            satellite = sate;
        }

        public void launch(taskName task)
        {
            switch(task)
            {
                case taskName.NONE:
                    break;
                case taskName.STICKIES:
                    StickiesControl stiController = new StickiesControl(satellite);
                        stiController.showDesktopSticky();
                    break;
            }
        }
    }
}
