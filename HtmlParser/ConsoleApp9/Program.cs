using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp9
{
    class Program
    {
        static List<Haber> haberler;
        static bool wait = true;

        static string[] Bol(string metin, string ayrac)
        {
            string[] temp = { ayrac };
            return metin.Split(temp, StringSplitOptions.None);
        }

        static string IcindenAl(string metin, string baslangic, string bitis)
        {
            string[] gecici = Bol(metin, baslangic);
            gecici = Bol(gecici[1], bitis);
            return gecici[0];
        }

        static string GetHtml(string url)
        {
            Thread.Sleep(300);

            WebClient httpGetClient = new WebClient();
            string html = httpGetClient.DownloadString(url);
            byte[] temp = Encoding.Default.GetBytes(html);
            html = Encoding.UTF8.GetString(temp);

            return System.Net.WebUtility.HtmlDecode(html);
        }

        static async Task<string> PostHtml(string url, IEnumerable<KeyValuePair<string, string>> data)
        {
            Thread.Sleep(300);

            HttpClient client = new HttpClient();
            var content = new FormUrlEncodedContent(data);

            var response = await client.PostAsync(url, content);

            return await response.Content.ReadAsStringAsync();
        }

        static string ClearHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        static List<Haber> ParseNews(string html, List<Haber> haberler)
        {
            string[] haberlerHtml = Bol(html, "<div class=\"card-stretch\">");
            Console.WriteLine((haberlerHtml.Length -1) + " haber geldi...");

            for (int i = 1; i < haberlerHtml.Length; i++)
            {
                Haber haber = new Haber();

                string haberHtml = haberlerHtml[i];

                haber.haberUrl = IcindenAl(haberHtml, "<a class=\"card news-card-horizontal\" href=\"", "\">");
                if (haber.haberUrl.IndexOf("www.kutahya.gov.tr") < 0)
                    haber.haberUrl = "/www.kutahya.gov.tr"+ haber.haberUrl;

                haber.haberUrl = "http:/" + haber.haberUrl;
                haber.haberUrl = haber.haberUrl.Replace("///", "//");


                haber.resimUrl = "http://www.kutahya.gov.tr" + IcindenAl(haberHtml, "<img class=\"card-img-top", "\" alt=\"");
                haber.resimUrl = haber.resimUrl.Replace(" lazy\" src=\"", "");
                haber.resimUrl = haber.resimUrl.Replace("\" src=\"", "");

                string haberSayfasiHtml = GetHtml(haber.haberUrl);

                string gecici = IcindenAl(haberSayfasiHtml, "<h2 class=\"page-title\">", "</h2>");
                haber.baslik = IcindenAl(gecici, "<span>", "</span>");

                gecici = "<div " + IcindenAl(haberSayfasiHtml, "<div class=\"icerik\"", "<script");
                haber.icerik = ClearHtml(gecici).Trim();

                haberler.Add(haber);

                Console.WriteLine((haberlerHtml.Length -1) + "/" +i + " OK");
            }

            return haberler;
        }

        static async Task<string> GetMoreNewsHtml(string contentTypeId, int page)
        {
            var data = new Dictionary<string, string>
            {
                { "ContentCount", "10" },
                { "ContentTypeId", contentTypeId },
                { "OrderByAsc", "true" },
                { "page", page.ToString() },
            };

            return await PostHtml("http://www.kutahya.gov.tr/ISAYWebPart/ContentList/DahaFazlaYukle", data);
        }

        static async void FillNews()
        {
            haberler = new List<Haber>();

            string html = GetHtml("http://www.kutahya.gov.tr/haberler");
            haberler = ParseNews(html, haberler);

            string contentTypeId = IcindenAl(html, "var ContentTypeId = '", "';");

            html = await GetMoreNewsHtml(contentTypeId, 2);
            haberler = ParseNews(html, haberler);

            html = await GetMoreNewsHtml(contentTypeId, 3);
            haberler = ParseNews(html, haberler);

            wait = false;
        }

        static void Main(string[] args)
        {
            FillNews();

            while (wait) Thread.Sleep(100);

            //haberler;
        }
    }
}
