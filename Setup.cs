using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineChat
{
    public partial class Setup : Form
    {
        //Variables
        private short port;

        public Setup()
        {
            InitializeComponent();
        }

        //Pressed Connect
        private void button1_Click(object sender, EventArgs e)
        {
            if (UsernameBox.Text == "")
            {
                MessageBox.Show("Username too short!","WRONG!");
                return;
            }

            if (Connection.Connect(IPbox.Text,port))
            {
                Hide();
                Chat c = new Chat(UsernameBox.Text, IPbox.Text, port);

                c.ShowDialog();
                if (!c.IsDisposed) c.Dispose();

                Show();

                try
                {
                    Network.Send(Connection.socket, (int)Network.Protocols.UserLeft);
                }
                catch (Exception) { }

                Connection.socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                Connection.socket.Close();
            }
        }

        //Changed port
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                port = Convert.ToInt16(PortBox.Text);
            }
            catch(Exception)
            {
                port = 0;
            }
        }
    }
}
