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
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            string url = "https://en.wikipedia.org/wiki/Saint_Petersburg";
            WebClient wclient = new WebClient();

            Parser parser_img = new Parser(new ParserImg());
            Parser parser_title = new Parser(new ParserTitle());

            WikiData data = new WikiData(
                parser_img.Parse(url),
                parser_title.Parse(url)
                );
            
            wclient.DownloadFile(data.img_url, "img.png");
            main.Text = data.title;
             
            BitmapImage bm = new BitmapImage(new Uri("img.png", UriKind.Relative));
            bm.Freeze();
            image.Source = bm;
            image.Stretch = Stretch.Uniform;
        }


    }
}
