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

        public UiInit(TextBlock[] var, Image image, WikiData[] data, string[] urls)
        {
            RightAnsInit();

            SetVars(var, data);
            SetImg(image, data, new Random().Next(0, 4));
            MakeParsedUrlList(urls);
        }

        private void SetVars(TextBlock[] var, WikiData[] data)
        {
            for (int i = 0; i < n; i++)
            {
                var[i].Text = data[i].title;
            }
        }
        private void SetImg(Image img, WikiData[] data, int k)
        {
            WebClient wclient = new WebClient();
            wclient.DownloadFile(data[k].img_url, "img.png");

            BitmapImage bm = new BitmapImage(new Uri("img.png", UriKind.Relative));
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
