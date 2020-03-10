using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DosyaIndirici
{
    class DownloadHelper
    {
        public event TickHandler StepChanged;
        public delegate void TickHandler(int max, int min);

        private void StepChangedTrigger(int max, int current)
        {
            if (StepChanged == null) return;
            StepChanged(max, current);
        }

        public string[] SaveFilesToLocal(string path, List<string> list)
        {
            string[] statuses = new string[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                StepChangedTrigger(list.Count, i);
                statuses[i] = SaveFileToLocal(path, list[i]);
            }

            return statuses;
        }

        private string SaveFileToLocal(string path, string fileUrl)
        {
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(fileUrl, path + "\\" + fileUrl.Split('/').Last());
                    return "success";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
