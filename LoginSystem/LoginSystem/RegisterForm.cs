using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginSystem.Encryption;
using System.IO;

namespace LoginSystem
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Directory.Exists("data\\users\\"))
            {
                MessageBox.Show("The user:" + UserTxt.Text + "already exists");
            }
            else
            {
                if(!isGuid())
                {
                    MessageBox.Show("Your Machine has already been registered");

                }
                else
                {
                    if(!SerialExists(SerialTxt.Text))
                    {
                        MessageBox.Show("Serial key is invalid! ");
                    }
                    else
                    {

                    }

                }
            }


            string dir = UserTxt.Text;
            Directory.CreateDirectory("data\\users\\" + dir);

            var sw = new StreamWriter("data\\users\\" + dir + "\\data.ls");

            string encryptUser = AEScrypt.Encrypt(UserTxt.Text);
            string encryptPass = AEScrypt.Encrypt(PassTxt.Text);
            string hWid = HwidTxt.Text;
            
            sw.WriteLine(encryptUser);
            sw.WriteLine(encryptPass);
            sw.WriteLine(hWid);
            sw.WriteLine(SerialTxt.Text);

            sw.Close();

            string thisguid = hwid.GetMachineGuid();

            File.AppendAllText("data\\guid\\id.ls", thisguid + Environment.NewLine);

            MessageBox.Show("User was successfully created.");
            this.Close();

        }

        private static string[] GetFileNames(string path)
        {
            string[] files = Directory.GetFiles(path);

            for(int i = 0; i < files.Length; i++)
            {
                files[i] = Path.GetFileName(files[i]);
            }
            return files;
        }

        public bool SerialExists(string key)
        {
            string CurrentKey = "a";

            string[] serials = GetFileNames("data\\serial");

            for(int i = 0; CurrentKey != null; i++)
            {
                try
                {
                    CurrentKey = serials[i];
                    if(CurrentKey == key)
                    {
                        File.Delete("data\\serial\\" + CurrentKey);
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    return false;
                }
            }

            return false;
        }

        public bool isGuid()
        {
            string thisguid = hwid.GetMachineGuid();
            StreamReader reader = new StreamReader("data\\guid\\id.ls");

            string readguid = reader.ReadLine();

            while(readguid != null)
            {
                if(thisguid == readguid)
                {
                    reader.Close();
                    return false;

                }
                else if(readguid == null)
                {
                    reader.Close();
                    return true;
                }
            }
            reader.Close();

            return true;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            HwidTxt.Text = hwid.GetMachineGuid();
        }
    }
}
