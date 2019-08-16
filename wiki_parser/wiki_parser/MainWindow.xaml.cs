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
            const string path = "url.txt";
            string[] url = {
                "https://ru.wikipedia.org/wiki/Perm",
                "https://ru.wikipedia.org/wiki/Perm",
                "https://ru.wikipedia.org/wiki/Perm",
                "https://ru.wikipedia.org/wiki/Perm"
            };
            try
            {
                //url = ParserUrl.urls(path);
                TextBlock[] var = {
                var_0, var_1, var_2, var_3
            };

                Parser parser_img = new Parser(new ParserImg());
                Parser parser_title = new Parser(new ParserTitle());
                WikiData[] data = new WikiData[var.Length];

                for (int i = 0; i < var.Length; i++)
                {
                    data[i] = new WikiData(
                        parser_title.Parse(url[i])[0],
                        parser_img.Parse(url[i])[0]
                        );
                }


                UiInit ui = new UiInit(var, image, data, 
                    new Parser(new ParserUrlList()).Parse("https://ru.wikipedia.org/wiki/Категория:Город"));
            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message);
                Close();
            }
        }


    }
}
