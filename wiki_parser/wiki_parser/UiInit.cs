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
    class UiInit
    {
        private const int n = 4;
        // Number of questions 
        private const int var_n = 20;

        private int[] right_ans = new int[var_n];

        private string[] urllist;

        private TextBlock[] _var;
        private Image _img;

        public UiInit(TextBlock[] var, Image image, string[] urls)
        {
            for (int i = 0; i < n; i++)
                var[i].MouseLeftButtonUp += new MouseButtonEventHandler(OnVarClick);

            _var = var;
            _img = image;
        
            urllist = urls;
            MakeParsedUrlList(urls);
        }

        private void OnVarClick(object sender, RoutedEventArgs args)
        {
            
        }

        private void SetSldie()
        {
            for (int i = 0; i < n; i++)
                _var[i].Text = 
        }

        private void SetVars(TextBlock[] var, string[] s)
        {
            for (int i = 0; i < n; i++)
            {
                var[i].Text = s[i];
            }
        }
        private void SetImg(Image img, string s, int k)
        {
            string f_ext;

            f_ext = "." + s.Substring(s.Length - 3, 3);
            
            WebClient wclient = new WebClient();
            wclient.DownloadFile(s, "img" + f_ext);

            BitmapImage bm = new BitmapImage(new Uri("img" + f_ext, UriKind.Relative));
            bm.Freeze();
            img.Source = bm;
            img.Stretch = Stretch.Uniform;
        }
        private static void MakeParsedUrlList(string[] urls)
        {
            using (StreamWriter writer = new StreamWriter("urllist.txt"))
            {
                foreach (string line in urls)
                    writer.WriteLine(line);
            }
        }
        private void RightAnsInit()
        {
            for (int i = 0; i < var_n; i++)
                right_ans[i] = new Random().Next(0, 4);
        }
    }
}
