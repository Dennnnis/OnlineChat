using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace OnlineChat
{
    static class Network
    {
        public enum Protocols
        {
            ChatMessage = 0,
            UsernameSend = 1,
            ErrorMessage = 2,
            UserLeft = 3
        }

        private static Socket bind;

        public static void Link(Socket socket) => bind = socket;

        public static void Send(Socket socket, bool var) => socket.Send(BitConverter.GetBytes(var));
        public static void Send(Socket socket, int var) => socket.Send(BitConverter.GetBytes(var));
        public static void Send(Socket socket, long var) => socket.Send(BitConverter.GetBytes(var));
        public static void Send(Socket socket, float var) => socket.Send(BitConverter.GetBytes(var));
        public static void Send(Socket socket, double var) => socket.Send(BitConverter.GetBytes(var));
        public static void Send(bool var) => bind.Send(BitConverter.GetBytes(var));
        public static void Send(int var) => bind.Send(BitConverter.GetBytes(var));
        public static void Send(long var) => bind.Send(BitConverter.GetBytes(var));
        public static void Send(float var) => bind.Send(BitConverter.GetBytes(var));
        public static void Send(double var) => bind.Send(BitConverter.GetBytes(var));

        public static bool ReceiveBool(Socket socket)
        {
            byte[] input = new byte[sizeof(bool)];
            socket.Receive(input);
            return BitConverter.ToBoolean(input, 0);
        }
        public static int ReceiveInt(Socket socket)
        {
            byte[] input = new byte[sizeof(int)];
            socket.Receive(input);
            return BitConverter.ToInt32(input, 0);
        }
        public static long ReceiveLong(Socket socket)
        {
            byte[] input = new byte[sizeof(long)];
            socket.Receive(input);
            return BitConverter.ToInt64(input, 0);
        }
        public static float ReceiveFloat(Socket socket)
        {
            byte[] input = new byte[sizeof(float)];
            socket.Receive(input);
            return BitConverter.ToSingle(input, 0);
        }
        public static double ReceiveDouble(Socket socket)
        {
            byte[] input = new byte[sizeof(double)];
            socket.Receive(input);
            return BitConverter.ToDouble(input, 0);
        }
        public static bool ReceiveBool()
        {
            byte[] input = new byte[sizeof(bool)];
            bind.Receive(input);
            return BitConverter.ToBoolean(input, 0);
        }
        public static int ReceiveInt()
        {
            byte[] input = new byte[sizeof(int)];
            bind.Receive(input);
            return BitConverter.ToInt32(input, 0);
        }
        public static long ReceiveLong()
        {
            byte[] input = new byte[sizeof(long)];
            bind.Receive(input);
            return BitConverter.ToInt64(input, 0);
        }
        public static float ReceiveFloat()
        {
            byte[] input = new byte[sizeof(float)];
            bind.Receive(input);
            return BitConverter.ToSingle(input, 0);
        }
        public static double ReceiveDouble()
        {
            byte[] input = new byte[sizeof(double)];
            bind.Receive(input);
            return BitConverter.ToDouble(input, 0);
        }

        public static void Send(Socket socket, string msg)
        {
            //Convert to ascii
            List<byte> byteList = new List<byte>(Encoding.ASCII.GetBytes(msg));

            //Add length
            byte[] length = BitConverter.GetBytes((short)byteList.Count);
            byteList.InsertRange(0, length);

            //Send
            socket.Send(byteList.ToArray());
        }
        public static void Send(string msg)
        {
            //Convert to ascii
            List<byte> byteList = new List<byte>(Encoding.ASCII.GetBytes(msg));

            //Add length
            byte[] length = BitConverter.GetBytes((short)byteList.Count);
            byteList.InsertRange(0, length);

            //Send
            bind.Send(byteList.ToArray());
        }

        public static string ReceiveString(Socket socket)
        {
            //Receive length
            byte[] receivedSize = new byte[2];
            socket.Receive(receivedSize);

            //Receive 
            byte[] receivedString = new byte[BitConverter.ToInt16(receivedSize, 0)];
            socket.Receive(receivedString);

            return Encoding.ASCII.GetString(receivedString);
        }
        public static string ReceiveString()
        {
            //Receive length
            byte[] receivedSize = new byte[2];
            bind.Receive(receivedSize);

            //Receive 
            byte[] receivedString = new byte[BitConverter.ToInt16(receivedSize, 0)];
            bind.Receive(receivedString);

            return Encoding.ASCII.GetString(receivedString);
        }
    }
}
