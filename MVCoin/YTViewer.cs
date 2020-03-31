using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

// Code modified from https://www.youtube.com/watch?v=sBAAgrQBCxc

namespace MVCoin
{
    public partial class YTViewer : Form
    {
        string _ytUrl;
        FormControl formController = new FormControl();
        Form satellate;
        Timer timerFollow = new Timer();
        bool collapseStatus = true;
        Size videoIniSize = new Size(640, 360);
        string id = " ";

        public YTViewer(Form satelliteInput)
        {
            InitializeComponent();
            satellate = satelliteInput;
        }

        private void YTViewer_Load(object sender, EventArgs e)
        {
            URLtextBox.BringToFront();
            timerFollow.Interval = 10;
            timerFollow.Tick += new EventHandler(follow);
            timerFollow.Start();
            collapseStatus = true;
            bgColor();
        }

        // Fold the viewer
        public void collapse()
        {
            this.Visible = false;
            collapseStatus = false;
        }

        // Unfold the viewer
        public void unCollapse()
        {
            this.Visible = true;
            collapseStatus = true;
        }

        // Return collapse status
        public bool isCollapsed()
        {
            return collapseStatus;
        }

        public void showUrlBox()
        {
            URLtextBox.Visible = true;
        }

        public void scaleUp()
        {
            if (id != " ")
            {
                videoIniSize.Width = (int)Math.Round(videoIniSize.Width * 1.1);
                videoIniSize.Height = (int)Math.Round(videoIniSize.Height * 1.1);
                loadVideo(videoIniSize);
            }
          
        }

        public void scaleDown()
        {
            if (id != " ")
            {
                videoIniSize.Width = (int)Math.Round(videoIniSize.Width / 1.1);
                videoIniSize.Height = (int)Math.Round(videoIniSize.Height / 1.1);
                loadVideo(videoIniSize);
            }
        }

        public string VideoId()
        {            
            var ytMatch = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)").Match(_ytUrl);
            return ytMatch.Success ? ytMatch.Groups[1].Value : string.Empty;
        }

        private void URLtextBox_TextChanged(object sender, EventArgs e)
        {
            _ytUrl = URLtextBox.Text;
            id = VideoId();
            if(id != string.Empty)
            {
                loadVideo(videoIniSize);
                URLtextBox.Visible = false;
            }

            if(URLtextBox.Text == "")
            {
                URLtextBox.Text = "Paste your youtube URL";
            }
        }

        private void pictureBox_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void follow(object sender, EventArgs e)
        {
            Point upRightCorner = formController.getCenter(satellate);
            upRightCorner.Offset(-this.Width, 0); // offset to up-left corner
            this.Location = upRightCorner; // Set new locatoin
        }

        private void loadVideo(Size size)
        {
            var embed =
                "<html><head>" +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
                "</head><body style=\"background-color:black;\">" +
                "<iframe width=\"{0}\"height=\"{1}\" src=\"{2}\"" +
                "frameborder = \"0\" allow = \"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\"" +
                "allowfullscreen></iframe>" +
                "</body></html>";
            var url = "https://www.youtube.com/embed/" + id;
            //webBrowser.Navigate("http://youtube.com/v/" + id + "?version=3");
            this.Size = new Size(size.Width + 21, size.Height + 21);
            this.webBrowser.DocumentText = string.Format(embed, size.Width, size.Height, url);
        }

        // Set background color to black
        private void bgColor()
        {
            var embed =
                "<html><head>" +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
                "</head><body style=\"background-color:black;\">" +
                "</body></html>";
            this.webBrowser.DocumentText = string.Format(embed);
        }

    }
}
