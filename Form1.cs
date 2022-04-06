﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Utility;
using WeAreDevs_API;

// raifu was here owo

namespace neoblox
{
    public partial class neoblox : FadeForm
    {
        ExploitAPI wrdExploitAPI = new ExploitAPI();
        public neoblox()
        {
            InitializeComponent();
        }

        public void config()
        {
            string topMostOn = "topmost:neutral";

            string discordRPCOn = "discordrpc:neutral";

            string musicOn = "music:neutral";

            // mfw if statements go brr

            if (topMostCheckbox.Checked == true)
            {
                topMostOn = "topmost:true";
            }
            if (topMostCheckbox.Checked == false)
            {
                topMostOn = "topmost:false";
            }
            if (discordRPCCheckbox.Checked == true)
            {
                discordRPCOn = "discordrpc:true";
            }
            if (discordRPCCheckbox.Checked == false)
            {
                discordRPCOn = "discordrpc:false";
            }
            if (musicCheckbox.Checked == true)
            {
                musicOn = "music:true";
            }
            if (musicCheckbox.Checked == false)
            {
                musicOn = "music:false";
            }

            using (StreamWriter writer = new StreamWriter("config.txt"))
            {
                writer.WriteLine(topMostOn);
                writer.WriteLine(discordRPCOn);
                writer.WriteLine(musicOn);
            }
        }

        private void neoblox_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            PopulateListBox(listBox1, "./Scripts", "*.txt");
            PopulateListBox(listBox1, "./Scripts", "*.lua");

            this.aceEditor.Navigate(string.Format("file:///{0}ace/aceEditor.html", AppDomain.CurrentDomain.BaseDirectory));

            aceEditor.Document.InvokeScript("SetText", new object[]
            {
                "print(\"Thanks for downloading Neoblox! Consider starring the github repo! (https://github.com/Plextora/Neoblox)\""
            });

            if (!File.Exists("config.txt"))
            {
                File.Create("config.txt");
            }

            string contents = File.ReadAllText("config.txt");
            if (contents.Contains("topmost:true"))
            {
                topMostCheckbox.Checked = true;
            }
            if (contents.Contains("topmost:false"))
            {
                topMostCheckbox.Checked = false;
            }
            if (contents.Contains("discordrpc:true"))
            {
                discordRPCCheckbox.Checked = true;
            }
            if (contents.Contains("discordrpc:false"))
            {
                discordRPCCheckbox.Checked = false;
            }
            if (contents.Contains("music:true"))
            {
                musicCheckbox.Checked = true;
            }
            if (contents.Contains("music:false"))
            {
                musicCheckbox.Checked = false;
            }

            try
            {
                using (StreamWriter w = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts")))
                {
                    w.WriteLine("# Anti-Banwave measures for Roblox - added by neoblox");
                    w.WriteLine("127.0.0.1 data.roblox.com");
                    w.WriteLine("127.0.0.1 roblox.sp.backtrace.io");
                }
            }
            catch
            {
                MessageBox.Show("We couldn't activate Anti-Ban measures due to an unexpected error!\nTry running neoblox as an administrator!\nAlways use an alt while exploiting!");
            }
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            HtmlDocument document = aceEditor.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = document.InvokeScript(scriptName, args);
            string script = obj.ToString();

            wrdExploitAPI.SendLuaScript(script);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            aceEditor.Document.InvokeScript("SetText", new object[]
            {
                ""
            });
        }

        private void openScriptButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Scripts";
                openFileDialog.Filter = string.Format("Text files (*.txt)|*.txt|Lua files (*.lua)|*.lua", "*.lua");
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        var MainText = reader.ReadToEnd();
                        aceEditor.Document.InvokeScript("SetText", new object[]
                        {
                            MainText
                        });
                    }
                }
            }
        }

        private void saveScriptButton_Click(object sender, EventArgs e)
        {
            HtmlDocument document = aceEditor.Document;
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = document.InvokeScript(scriptName, args);
            string script = obj.ToString();

            try
            {
                var saveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Scripts",
                    Filter = string.Format("Text files (*.txt)|*.txt|Lua files (*.lua)|*.lua", "*.lua"),
                    RestoreDirectory = true,
                    ShowHelp = false,
                    CheckFileExists = false
                };
                if (saveFileDialog1.ShowDialog() == DialogResult.OK) { File.WriteAllText(saveFileDialog1.FileName, script); }
            }
            catch
            {

            }
        }

        private void injectButton_Click(object sender, EventArgs e)
        {
            wrdExploitAPI.LaunchExploit();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Process.Start("deactivate anti ban measures.exe");
            foreach (var process in Process.GetProcessesByName("discordrpc"))
            {
                process.Kill();
            }
            Application.Exit();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        Point lastPoint;

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Left += e.X - lastPoint.X;
                Top += e.Y - lastPoint.Y;
            }
        }

        public static void PopulateListBox(ListBox lsb, string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);
            foreach (FileInfo file in Files)
            {
                lsb.Items.Add(file.Name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            aceEditor.Document.InvokeScript("SetText", new object[]
            {
                File.ReadAllText($"./Scripts/{listBox1.SelectedItem}")
            });
        }

        private void refreshScriptList_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            PopulateListBox(listBox1, "./Scripts", "*.txt");
            PopulateListBox(listBox1, "./Scripts", "*.lua");

        }

        private void walkspeedTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string walkspeed = walkspeedTextbox.Text;
                wrdExploitAPI.SendLuaScript($"game.Players.LocalPlayer.Character.Humanoid.WalkSpeed={walkspeed}");
            }
        }

        private void onButtonFly_Click(object sender, EventArgs e)
        {
            wrdExploitAPI.SendLuaScript("loadstring(game:HttpGet('https://pastebin.com/raw/ETeUDwvV', true))()");
        }

        private void killRblx_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("RobloxPlayerBeta"))
            {
                process.Kill();
            }
        }

        private void topMostCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = topMostCheckbox.Checked;

            config();
        }

        

        private void discordRPCCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (discordRPCCheckbox.Checked == true)
            {
                Process.Start("discordrpc.exe");
            }

            if (discordRPCCheckbox.Checked == false)
            {
                Process.Start("discordrpc.exe");
                foreach (var process in Process.GetProcessesByName("discordrpc"))
                {
                    process.Kill();
                }
            }

            config();
        }

        private void musicCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            var soundPlayer = new System.Media.SoundPlayer();
            if (musicCheckbox.Checked == true)
            {
                soundPlayer.SoundLocation = "https://us-east-1.tixte.net/uploads/plextora.is-from.space/elevatormusic.wav";
                soundPlayer.PlayLooping();
            }
            if (musicCheckbox.Checked == false)
            {
                soundPlayer.Stop();
            }

            config();
        }
    }
}
