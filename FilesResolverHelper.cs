using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DosyaIndirici
{
    class FilesResolverHelper
    {
        public event TickHandler StepChanged;
        public delegate void TickHandler(int max, int min);

        public List<List<string>> fileLists = new List<List<string>>();
        List<string> sourceUrls = new List<string>();
        string baseUrl = "";
        bool recursive;



        private void StepChangedTrigger(int i, int max, int current)
        {
            if (StepChanged == null) return;

            var sum = 0;
            for (int j = 0; j < fileLists.Count; j++)
                if (j != i)
                    sum += fileLists[j].Count;

            StepChanged(sum + max, sum + current);
        }



        public void  FillLists(string url, bool recursive)
        {
            this.recursive = recursive;

            FillFirstUrls(url);
            FillFileLists();
        }

        private void FillFileLists()
        {
            for (int i = 0; i < sourceUrls.Count; i++)
            {
                string sourceUrl = sourceUrls[i];
                string html = GetHtmlFromUrl(sourceUrl);

                string[] tempArray = GetFileListFromHtml(html);
                for(int j = 0; j < tempArray.Length; j++)
                {
                    StepChangedTrigger(i, tempArray.Length, j);

                    string file = tempArray[j];
                    if(file.Substring(file.Length-1, 1) != "/")
                    {
                        ControlFileUrlAndAddFileList(i, file);
                        continue;
                    }

                    if (!recursive) continue;

                    if (file.Substring(0, 1) == "/")
                        file = baseUrl + file.Substring(1, file.Length - 1);
                    else if (file.Substring(0, 0) != "http")
                        file = sourceUrl + file;

                    FillFirstUrls(file);
                }
            }
        }

        private void ControlFileUrlAndAddFileList(int i, string url)
        {
            url = ClearLinkUrl(url);

            if (url.Length < 4 || url.Substring(0, 4) != "http")
            {
                if (url.Length == 0) return;

                if (url.Substring(0, 1) == "/")
                    url = baseUrl + url.Substring(1, url.Length - 1);
                else
                    url = sourceUrls[i] + url;
            }

            foreach (var fileList in fileLists)
                foreach (var fileUrl in fileList)
                    if (fileUrl.IndexOf(url) == 0) 
                        return;

            fileLists[i].Add(url);
        }

        private string ClearLinkUrl(string url)
        {
            url = url.Trim('"');
            url = url.Trim('>');
            url = url.Trim(' ');

            return url;
        }

        private string[] GetFileListFromHtml(string html)
        {
            string[] tempFileList = html.Split(new[] { "href=", "src=" }, StringSplitOptions.None);
            string[] fileList = new string[tempFileList.Length -1];

            int fileCount = 0;
            for (int i = 1; i < tempFileList.Length; i++)
            {
                string item = tempFileList[i];
                if (item.Length == 0) continue;

                var temp = item.Split(new[] { item.Substring(0, 1) }, StringSplitOptions.None);

                if (temp[1].Length == 0) continue;
                if (temp[1].Substring(0, 1) == "?") continue;

                fileList[fileCount++] = temp[1];
            }

            return fileList.Take(fileCount).ToArray();
        }

        private string GetHtmlFromUrl(string url)
        {
            url = url.Replace("&amp;", "&");

            WebClient client = new WebClient();
            return client.DownloadString(url);
        }

        private void FillFirstUrls(string url)
        {
            if (url.Substring(url.Length - 1, 1) != "/")
                url += "/";

            foreach (var sourceUrl in sourceUrls)
                if (sourceUrl.IndexOf(url) == 0)
                    return;

            sourceUrls.Add(url);

            string[] tempArray = url.Split('/');
            string tempUrl = tempArray[0] + "//" + tempArray[2] + "/";
            if (baseUrl == "") baseUrl = tempUrl;

            fileLists.Add(new List<string>());
        }
    }
}
