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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string dir = UserTxt.Text;
            if(!Directory.Exists("data\\users\\"+ dir))
            {
                MessageBox.Show("User"+ dir + " was not found");
            }
            else
            {
                var reader = new StreamReader("data\\users\\" + dir + "\\data.ls");

                string encryptUser = reader.ReadLine();
                string encryptPass = reader.ReadLine();
                string readGuid = reader.ReadLine();


                reader.Close();

                string decryptedUser = AEScrypt.Decrypt(encryptUser);
                string decryptedPass = AEScrypt.Decrypt(encryptPass);

                if (!isGuid(readGuid))
                {
                    MessageBox.Show("Invalid MachineGUID");
                }
                else
                {

                    if (decryptedUser == UserTxt.Text && decryptedPass == PassTxt.Text)
                    {
                        MessageBox.Show("Successfully Logged In");

                    }
                    else
                    {
                        MessageBox.Show("User or Password is wrong!");
                    }
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.Show();
        }

        public bool isGuid(string readGuid)
        {
            if(readGuid == hwid.GetMachineGuid())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
