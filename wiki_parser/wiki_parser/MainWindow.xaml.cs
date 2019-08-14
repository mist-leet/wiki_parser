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

            const int n = 4;
            string[] url = {
                "https://en.wikipedia.org/wiki/Saint_Petersburg",
                "https://en.wikipedia.org/wiki/Moscow",
                "https://en.wikipedia.org/wiki/Perm",
                "https://en.wikipedia.org/wiki/Omsk"
            };

            TextBlock[] var = {
                var_0, var_1, var_2, var_3
            };
                        
            Parser parser_img = new Parser(new ParserImg());
            Parser parser_title = new Parser(new ParserTitle());
            WikiData[] data = new WikiData[4];

            for(int i = 0; i < n; i++)
            {
                data[i] = new WikiData(
                    parser_title.Parse(url[i]),
                    parser_img.Parse(url[i])
                    );
            }

            UiInit ui = new UiInit(var, image, data);
        }


    }
}
