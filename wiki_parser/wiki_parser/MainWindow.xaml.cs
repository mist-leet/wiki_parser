using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace wiki_parser
{
    // https://en.wikipedia.org/wiki/Saint_Petersburg
    // <title>Saint Petersburg - Wikipedia</title>

    public partial class MainWindow : Window
    {
        private string FindName(string s)
        {
            int start = s.IndexOf("<title>") + 7;
            int end = s.IndexOf("</title>");
            if (end > 0)
                return s.Substring(start, end - start);
            else
                return null;
        }

        private string FindPic(string s)
        {
            int start = s.IndexOf("https://upload.wikimedia.org/");
            if (start > 0)
            {
                int i;
                for (i = start; s[i] != '\"'; i++) ;
                return s.Substring(start, i - start);
            }
            else
                return null;
        }
        public MainWindow()
        {
            InitializeComponent();
            
            WebRequest req = WebRequest.Create("https://en.wikipedia.org/wiki/Saint_Petersburg");
            WebResponse res = req.GetResponse();

            WebClient wclient = new WebClient();

            string title = "";
            string img = "";
            
            
            using (Stream stream = res.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null
                        && (title == "" || img == ""))
                    {
                        if (FindName(line) != null && title == "")
                            title = FindName(line);
                        if (FindPic(line) != null && img == "")
                            img = FindPic(line);
                    }
                }
                wclient.DownloadFile(img, "img.png");
            }

            main.Text = title;
            BitmapImage bm = new BitmapImage(new Uri("img.png", UriKind.Relative));
            image.Source = bm;

        }


    }
}
