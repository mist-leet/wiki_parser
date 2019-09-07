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
    public partial class MainWindow : Window
    {
        private enum Categories : int
        {
            Cities = 0,
            CultBuildings,
            Contry,
            Parks,
            Gods,
            EthnicGroups,
            Plants,
            Mushrooms

        }

        private string[] categories = new string[]
        {
            "https://ru.wikipedia.org/wiki/Категория:Населённые_пункты_по_алфавиту",
            "https://ru.wikipedia.org/wiki/Категория:Культовые_сооружения_по_алфавиту",
            "https://ru.wikipedia.org/wiki/Категория:Государства_по_алфавиту",
            "https://ru.wikipedia.org/wiki/Категория:Сады_и_парки_по_алфавиту",
            "https://ru.wikipedia.org/wiki/Категория:Боги_по_алфавиту",
            "https://ru.wikipedia.org/wiki/Категория:Этнические_группы_по_алфавиту",
            "https://ru.wikipedia.org/wiki/Категория:Растения_по_алфавиту",
            "https://ru.wikipedia.org/wiki/Категория:Грибы_по_алфавиту"
        };
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                TextBlock[] var = {
                var_0, var_1, var_2, var_3
            };

                UiInit ui = new UiInit(
                    var, image,
                    new ParserUrlList()
                        .GetData(categories[(int)Categories.EthnicGroups]));
                
            }
            catch (FormatException e)
            {
                MessageBox.Show(e.Message);
                Close();
            }
        }


    }
}
