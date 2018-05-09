using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace GTARP.fr_Launcher
{
    public partial class Launcher : Form
    {
        User user = new User();
        static String path;
        String[] pathGame = { path + @"\game"};
        String dir = AppDomain.CurrentDomain.BaseDirectory;
        String FileName = "gtarp.fr-settings.json";

        FolderBrowserDialog fdlg = new FolderBrowserDialog();
        public Launcher()
        {
            InitializeComponent();
            if(File.Exists(dir + FileName))
            {
                loadSettings();
                pathbox.Text = path;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(pathbox.Text == "")
            {
                MessageBox.Show("Vous n'avez pas signifiez votre cache /appdata/...");

            } else {

                System.Diagnostics.Process.Start("fivem://connect/158.69.251.166:30120");
                path = fdlg.SelectedPath;
                DeleteFolderContent(path, pathGame);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                pathbox.Text = fdlg.SelectedPath;
                path = fdlg.SelectedPath;
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            Close();
            saveSettings();
        }

        void DeleteFolderContent(string folder, string[] exceptions)
        {
            string[] filePaths = Directory.GetFiles(path);
            foreach (string filePath in filePaths)
            {
                var name = new FileInfo(filePath).Name;
                name = name.ToLower();
                if (name != "game")
                {
                    File.Delete(filePath);
                    
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/jE3s5Cj");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://gtarp.fr");
        }

        public class User
        {
            public string name { get; set; }
            public string path { get; set; }
        }

        private void saveSettings()
        {
            user.name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            user.path = path;

            File.WriteAllText(dir + FileName, JsonConvert.SerializeObject(user));
        }

        private void loadSettings()
        {
            user = JsonConvert.DeserializeObject<User>(File.ReadAllText(dir + FileName));
            path = user.path;
            Console.WriteLine(path);
        }
    }
}
