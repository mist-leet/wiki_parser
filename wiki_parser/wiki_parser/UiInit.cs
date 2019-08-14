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
        public UiInit(TextBlock[] var, Image image, WikiData[] data)
        {
            SetVars(var, data);
            SetImg(image, data, 3);
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
    }
}
