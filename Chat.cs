using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace OnlineChat
{
    public partial class Chat : Form
    {


        private string username;
        private string ipaddress;
        private short port;
        private Thread thread;

        public Chat(string username, string ipaddress, short port)
        {
            InitializeComponent();

            this.username = username;
            this.ipaddress = ipaddress;
            this.port = port;
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(ReceiveThread));
            thread.Start();

            try
            {
                //Send username
                Network.Send(Connection.socket, (int)Network.Protocols.UsernameSend);
                Network.Send(Connection.socket, username);
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Could not communicate with server :(");
            }
        }

        private void ReceiveThread()
        {
            while (true)
            {
                try
                {
                    //Receive data
                    int prot = Network.ReceiveInt(Connection.socket);
                    string str = Network.ReceiveString(Connection.socket);

                    //Add error text
                    if (prot == (int)Network.Protocols.ErrorMessage) str = str.Insert(0, "|ERROR| ");

                    if (str.Length < 1) continue;

                    //Add item to listbox (Thread safe... i think)
                    if (listBox1.InvokeRequired)
                    {
                        SetBoxCallBack d = new SetBoxCallBack(AddText);
                        Invoke(d, new object[] { str });
                    }
                    else
                    {
                        AddText(str);
                    }

                }
                catch(Exception e)
                {
                    break;
                }
            }
        }

        //Pressed send
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Network.Send(Connection.socket, (int)Network.Protocols.ChatMessage);
                Network.Send(Connection.socket, textBox1.Text);
                textBox1.Text = "";
            }
            catch(Exception ex)
            {
                listBox1.Items.Add("Server did not respond!");
            }
        }

        //Pressed enter
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Contains(Environment.NewLine))
            {
                textBox1.Text = textBox1.Text.Replace(Environment.NewLine,"");
                button1_Click(this, EventArgs.Empty);
            }
        }

        //Delegate for invoke
        delegate void SetBoxCallBack(string text);

        //Add text to the listbox
        private void AddText(string text)
        {
            listBox1.Items.Add(text);
            listBox1.TopIndex = listBox1.Items.Count - 1;
        }


    }
}
