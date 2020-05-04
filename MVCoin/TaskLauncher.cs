using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVCoin
{
    public enum taskName {NONE, STICKIES, YTVIEWER, COVID19};

    class TaskLauncher
    {
        
        private Satellite satellite;
        bool collapseStatus = true;
        YTViewer ytViewer;
        StickiesControl stiController;
        PyCall pyCaller;


        public TaskLauncher(Satellite sat)
        {
            satellite = sat;
            
            switch (satellite.getTask())
            {
                case taskName.NONE:
                    break;
                case taskName.STICKIES:
                    stiController = new StickiesControl(satellite);
                    break;
                case taskName.YTVIEWER:
                    ytViewer = new YTViewer(satellite);
                    break;
                case taskName.COVID19:
                    pyCaller = new PyCall();
                    break;
            }
        }

        public void launch(taskName task, MouseEventArgs e)
        {
            switch(task)
            {
                case taskName.NONE:
                    break;
                case taskName.STICKIES:
                    stikiesTask(e);
                    break;
                case taskName.YTVIEWER:
                    ytViewerTask(e);
                    break;
                case taskName.COVID19:
                    covid19(e);
                    break;
            }
        }

        public void stikiesTask(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
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
        }

        private void ytViewerTask(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (collapseStatus == true) // Viewer is collapsed
                {
                    ytViewer.unCollapse();
                    collapseStatus = false;
                }

                else if (collapseStatus == false) // Viewer is Uncollapsed
                {
                    ytViewer.collapse();
                    collapseStatus = true;
                }

            }
            else if (e.Button == MouseButtons.Right)
            {
                ytViewer.showUrlBox();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                // Function when ytViewer is uncollapsed
                if (collapseStatus == false)
                    ytViewer.setUrl("https://www.youtube.com/watch?v=5qap5aO4i9A");
            }

            else if (e.Delta > 0)
            {
                ytViewer.scaleUp();
            }
            else if (e.Delta < 0)
            {
                ytViewer.scaleDown();
            }
        }

        private void covid19(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string[] strArr = null;//引數列表
                string sArguments = @"virus.py";//這裡是python的檔名字

                string textGet = pyCaller.RunPythonScript(sArguments, "-u", strArr);
                MsgBox msgBox = new MsgBox();
                msgBox.setText(textGet);
                msgBox.Show();
                
            }
        }
    }
}
