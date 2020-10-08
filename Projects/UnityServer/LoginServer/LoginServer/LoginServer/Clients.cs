using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer
{
    class Clients
    {
        public int connectionID;
        public string ip;
        public TcpClient socket;
        public NetworkStream myStream;
        private byte[] readBuffer;
        public ByteBuffer Buffer;

        public void Start()
        {
            socket.SendBufferSize = 4096;
            socket.ReceiveBufferSize = 4096;
            myStream = socket.GetStream();
            readBuffer = new byte[4096];
            myStream.BeginRead(readBuffer, 0, socket.ReceiveBufferSize, ReceiveDataCallBack, null);
        }

        private void ReceiveDataCallBack(IAsyncResult result)
        {
            try
            {
                int readBytes = myStream.EndRead(result);
                if (readBytes <= 0)
                {
                    CloseConnection();
                    return;
                }
                byte[] newBytes = new byte[readBytes];
                System.Buffer.BlockCopy(readBuffer, 0, newBytes, 0, readBytes);

                //ServerHandlePackets.HandleData(connectionID, newBytes);

                myStream.BeginRead(readBuffer, 0, socket.ReceiveBufferSize, ReceiveDataCallBack, null);
            }

            catch (Exception)
            {
                CloseConnection();
                throw;
            }
        }

        private void CloseConnection()
        {
            string Time;
            Time = DateTime.Now.ToString("HH:mm");
            Console.WriteLine("[" + Time + "] - " + "Connection lost from user with ip: " + ip + ".");
            socket.Close();
            socket = null;
        }
    }
}
