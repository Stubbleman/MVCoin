using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCoin
{
    public enum taskName {NONE, STICKIES, YTVIEWER };

    class TaskLauncher
    {
        
        private Form satellite;
        bool collapseStatus = false;
        YTViewer ytViewer;
        StickiesControl stiController;

        public TaskLauncher(Form sate)
        {
            satellite = sate;
            ytViewer = new YTViewer(satellite);
            stiController = new StickiesControl(satellite);
        }

        public void launch(taskName task, MouseEventArgs e)
        {
            switch(task)
            {
                case taskName.NONE:
                    break;
                case taskName.STICKIES:
                    if(e.Button == MouseButtons.Left)
                    {
                        if (collapseStatus == false)
                        {                            
                            stiController.showDesktopSticky();
                            collapseStatus = true;
                        }                        
                        else
                        {
                            stiController.hideAllSticky();
                            collapseStatus = false;
                        }
                    }

                    break;
                case taskName.YTVIEWER:
                    if (e.Button == MouseButtons.Left)
                    {
                        if (collapseStatus == false)
                        {
                            ytViewer.unCollapse();
                            collapseStatus = true;
                        }

                        else if (collapseStatus == true)
                        {
                            ytViewer.collapse();
                            collapseStatus = false;
                        }

                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        ytViewer.showUrlBox();
                    }
                    else if (e.Delta > 0)
                    {
                        ytViewer.scaleUp();
                    }
                    else if (e.Delta < 0)
                    {
                        ytViewer.scaleDown();
                    }

                    break;
            }
        }
    }
}
