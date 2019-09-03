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

        private int[] _right_ans = new int[var_n];
        private int _current_q = 0;
        private string[] _urllist;

        private TextBlock[] _var;
        private Image _img;
        private WikiData _current_data;

        
        public UiInit(TextBlock[] var, Image image, string[] urls)
        {
            _var = var;
            _img = image;
            _urllist = urls;
            _current_data = new WikiData(_urllist);
            RightAnsInit();

            for (int i = 0; i < n; i++)
                var[i].MouseLeftButtonUp += new MouseButtonEventHandler(OnVarClick);        
            MakeParsedUrlList(_urllist);

            SetSldie(_right_ans[_current_q]);
        }

        private void OnVarClick(object sender, RoutedEventArgs args)
        {
            if (_current_data.title[_right_ans[_current_q]] ==
             ((TextBlock)sender).Text)
                MessageBox.Show("You were right!");
            else
                MessageBox.Show("You were wrong!");

            _current_data = new WikiData(_urllist);
            SetSldie(_right_ans[_current_q]);
            _current_q++;
        }

        private void SetSldie(int k)
        {
            for (int i = 0; i < n; i++)
                _var[i].Text = _current_data.title[i];
            SetImg(_img, _current_data.img_url[k]);
        }

        private void SetImg(Image img, string s)
        {
            string f_ext;

            f_ext = "." + s.Substring(s.Length - 3, 3);

            using (WebClient wclient = new WebClient())
            {
                wclient.DownloadFile(s, "img" + f_ext);
                wclient.Dispose();
            }
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri("img" + f_ext, UriKind.Relative);
            bm.CacheOption = BitmapCacheOption.OnLoad;
            bm.EndInit();
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
            Random rnd = new Random();
            for (int i = 0; i < var_n; i++)
                _right_ans[i] = rnd.Next(0, 4);
        }
    }
}
