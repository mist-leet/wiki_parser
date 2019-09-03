using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wiki_parser
{
    class WikiData
    {
        public string[] title;

        public string[] img_url;

        private int[] r;

        private string[] url_list;

        private const int n = 4;

        public WikiData(string[] urls)
        {
            title    = new string[] { };
            img_url  = new string[] { };
            r        =    new int[] { };
            url_list = new string[] { };

            r = MakeRandomList(urls.Length);

            url_list = urls;

            for (int i = 0; i < 4; i++)
            {
                Array.Resize<string>(ref title, title.Length + 1);
                Array.Resize<string>(ref img_url, img_url.Length + 1);

                title[i] = new ParserTitle().GetData(url_list[r[i]])[0];
                img_url[i] = new ParserImg().GetData(url_list[r[i]])[0];
            }
            for (int i = 0; i < 4; i++) 
            {
                if (title[i] == null || title[i] == "")
                    ReDoElement(ref title, new ParserTitle(), i);
                if (img_url[i] == null || img_url[i] == "")
                    ReDoElement(ref img_url, new ParserImg(), i);
            }
        }

        private void ReDoElement(ref string[] arr, IParser parser, int i)
        {
            Array.Resize<string>(ref arr, arr.Length + 1);
            arr[i] = parser.GetData(url_list[GetRandomNumber(r, i, url_list.Length)])[0];
        }

        private bool CheckRandom(int[] r)
        {
            for (int i = 0; i < r.Length; i++)
                for (int j = 0; j < r.Length; j++)
                    if (i == j)
                        continue;
                    else
                        if (r[i] == r[j])
                            return false;
            return true;
        }

        private int[] MakeRandomList(int maxValue)
        {
            Random rnd = new Random();
            int[] r = new int[n];
            for (int i = 0; i < n; i++)
                r[i] = rnd.Next(maxValue); 

            while (!CheckRandom(r))
                for (int i = 0; i < n; i++)
                    r[i] = rnd.Next(maxValue);
            return r;
        }

        private int GetRandomNumber(int[] r, int k, int maxValue)
        {
            while (true)
            {
                int i;
                int rn = new Random().Next(maxValue);
                for (i = 0; i < r.Length - 1; i++)
                {
                    if (r[i] == rn)
                        break;
                }
                if (r[i] != rn)
                    return rn;
            }
        }
    }
}
