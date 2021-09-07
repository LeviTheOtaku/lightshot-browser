using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightShot_Browser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Controls.Count > 2)
            {
                tableLayoutPanel1.Controls.RemoveAt(0);
            }
            try
            {
                button1.Text = ">";

                var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[6];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("user-agent", ":)");
                    string htmlCode = client.DownloadString("http://prnt.sc/" + finalString);

                    htmlCode.IndexOf("under-image\">");

                    htmlCode.IndexOf("\">");
                    string extractedUrl = htmlCode.Substring(htmlCode.IndexOf("twitter:image:src") + 28, 100);
                    var pictureBox = new PictureBox();
                    pictureBox.LoadAsync(extractedUrl.Split('"')[0]);
                    pictureBox.LoadCompleted += new AsyncCompletedEventHandler(this.pictureBox_done);
                    pictureBox.AccessibleDescription = "http://prnt.sc/" + finalString;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox.Click += new EventHandler(this.pictureBox_click);
                    pictureBox.Dock = DockStyle.Fill;
                    tableLayoutPanel1.Controls.Add(pictureBox, 0, 0);

                }
            }
            catch
            {
                button1.Text = "caught invalid url. try again!";
            }
        }

        void pictureBox_click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            System.Diagnostics.Process.Start(pictureBox.AccessibleDescription);
        }
        void pictureBox_done(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            this.Text = pictureBox.ImageLocation.ToString();
        }
    }
}
