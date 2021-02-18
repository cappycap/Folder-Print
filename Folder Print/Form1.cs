using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Folder_Print
{
    public partial class Form1 : Form
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";
        public UserCredential credential { get; set; }
        public string sourceFolder { get; set; }
        public string printer { get; set; }
        public string destinationFolder { get; set; }
        public AutoResetEvent autoEvent = new AutoResetEvent(false);
        public System.Threading.Timer timer { get; set; }

        public void Access(UserCredential credential, string sourceFolder, string printer, string destinationFolder, Form1 form1)
        {
            string folderLink = sourceFolder;

            string[] folderTemp = folderLink.Split("/");
            string[] folderElements = folderTemp[5].Split("?");
            string folderId = folderElements[0];

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Q = $"'{folderId}' in parents";
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

            form1.listView1.Items.Add("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    form1.listView1.Items.Add("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                form1.listView1.Items.Add("No files found.");
            }

        }

        static void changeTimer(int amount)
        {
            bool update = Form1.timer.Change(0, amount);
        }

        public Form1()
        {
            InitializeComponent();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                this.listView1.Items.Add("Credential file saved to: " + credPath);
            }

            timer = new System.Threading.Timer(state => Access(credential, sourceFolder, printer, destinationFolder, this), autoEvent, 0, 30000);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            // Set retrieval link.
        }

        public void label1_Click(object sender, EventArgs e)
        {

        }

        public void label2_Click(object sender, EventArgs e)
        {

        }

        public void label3_Click(object sender, EventArgs e)
        {

        }

        public void label4_Click(object sender, EventArgs e)
        {

        }

        public void label5_Click(object sender, EventArgs e)
        {

        }

        public void button3_Click(object sender, EventArgs e)
        {
            // Set time interval.
        }

        public void label6_Click(object sender, EventArgs e)
        {

        }

        public void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Display Help Summary Form.
        }

        public void button2_Click(object sender, EventArgs e)
        {
            // Set upload link, verify
        }

        public void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Text = "0";
        }
    }
}
