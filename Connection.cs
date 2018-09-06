using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace OnlineChat
{
    static class Connection
    {
        public static Socket socket;

        //Returns true if the connection is made
        public static bool Connect(string ip,short port)
        {
            try
            {
                //Reset the socket 
                socket = new Socket(SocketType.Stream, ProtocolType.IP);

                //Try conenction
                socket.Connect(ip, port);
            }
            catch(Exception e)
            {
                //Catch error
                MessageBox.Show( e.Message.ToString(), "Error");

                socket.Dispose();

                return false;
            }
            return true;
        }
    }
}
