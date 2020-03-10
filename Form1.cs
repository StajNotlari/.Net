using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListBox;

namespace DosyaIndirici
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            label1.Text = "";
        }



        #region Button Click

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) return;
            backgroundWorker1.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;

            string selectedText = listBox1.SelectedItems[0].ToString();
            Clipboard.SetText(selectedText);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;

            foreach (string item in listBox1.SelectedItems)
                Process.Start(item);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;

            SelectedObjectCollection selectedItems = listBox1.SelectedItems;
            for (int i = selectedItems.Count - 1; i >= 0; i--)
                listBox1.Items.Remove(selectedItems[i]);

            listBox1.ClearSelected();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.ClearSelected();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (backgroundWorker2.IsBusy) return;

            string path = GetSelectedFolderPath();
            if (string.IsNullOrWhiteSpace(path)) return;

            backgroundWorker2.RunWorkerAsync(argument: path);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox2.ClearSelected();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox2.Items.Count; i++)
                listBox2.SetSelected(i, true);
        }

        #endregion



        #region Backgorund Workers

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FilesResolverHelper helper = new FilesResolverHelper();
                helper.StepChanged += new FilesResolverHelper.TickHandler(StepChanged);
                helper.FillLists(textBox1.Text, checkBox1.Checked);

                progressBar1.Value = 0;
                label1.Text = "";

                FillFileList(helper.fileLists);

                groupBox1.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;

            try
            {
                string path = (string)e.Argument;

                List<string> fileList = GetFileListForDownload();

                DownloadHelper helper = new DownloadHelper();
                helper.StepChanged += new DownloadHelper.TickHandler(StepChanged);
                string[] statuses = helper.SaveFilesToLocal(path, fileList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

            progressBar1.Value = 0;
            label1.Text = "";
        }

        private List<string> GetFileListForDownload()
        {
            List<string> fileList;

            if (listBox1.SelectedIndex == -1)
                fileList = listBox1.Items.Cast<string>().ToList();
            else
                fileList = listBox1.SelectedItems.Cast<string>().ToList();

            fileList = FilterFileListByTypes(fileList);

            return fileList;
        }

        private List<string> FilterFileListByTypes(List<string> fileList)
        {
            if (listBox2.SelectedIndex == -1) return fileList;

            List<string> filteredList = new List<string>();
            List<string> types = listBox2.SelectedItems.Cast<string>().ToList();

            for (int i = 0; i < fileList.Count; i++)
            {
                string[] temp = fileList[i].Split('.');
                if (types.Contains(temp.Last()))
                    filteredList.Add(fileList[i]);
            }

            return filteredList;
        }

        #endregion



        #region Helpers

        private void FillFileList(List<List<string>> fileLists)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            List<string> types = new List<string>();

            foreach (var fileList in fileLists)
                foreach (var fileUrl in fileList)
                {
                    listBox1.Items.Add(fileUrl);

                    string[] temp = fileUrl.Split('.');
                    if (!types.Contains(temp.Last().Trim()))
                        types.Add(temp.Last().Trim());
                }

            foreach (string type in types)
                listBox2.Items.Add(type);
        }

        private void StepChanged(int max, int current)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = max;
            progressBar1.Value = current;

            label1.Text = current + "/" + max;
        }
        private string GetSelectedFolderPath()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                return fbd.SelectedPath;
            }
        }

        #endregion
    }
}
