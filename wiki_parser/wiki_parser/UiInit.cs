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
        private int _current_img_name = 0;

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

            SetSldie(_right_ans[_current_q]);
        }

        private void OnVarClick(object sender, RoutedEventArgs args)
        {
            if (_current_data.title[_right_ans[_current_q]] ==
             ((TextBlock)sender).Text)
                MessageBox.Show("You were right!");
            else
                MessageBox.Show("You were wrong!" + " No, it was \n " +
                    _current_data.title[_right_ans[_current_q]]);
            _current_q++;

            SetSldie(_right_ans[_current_q]);
        }

        private void SetSldie(int k)
        {
            _current_data = new WikiData(_urllist);
            for (int i = 0; i < n; i++)
                _var[i].Text = _current_data.title[i];
            _img.Source = GetImage(_current_data.img_url[k]);
        }
                  
        private BitmapImage GetImage(string s)
        {
            string f_ext;

            f_ext = "." + s.Substring(s.Length - 3, 3);
            f_ext = f_ext.ToLower();

            using (WebClient wclient = new WebClient())
            {
                wclient.DownloadFile(s, "img" + _current_img_name.ToString() + f_ext);
                wclient.Dispose();
            }
            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.UriSource = new Uri("img" + _current_img_name.ToString() + f_ext, UriKind.Relative);
            bm.CacheOption = BitmapCacheOption.OnLoad;
            bm.EndInit();
            bm.Freeze();
            _current_img_name++;
            return bm;
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
