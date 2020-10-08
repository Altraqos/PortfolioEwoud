using System;
using System.Net.Sockets;

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
                return;
            }
            byte[] newBytes = new byte[readBytes];
            System.Buffer.BlockCopy(readBuffer, 0, newBytes, 0, readBytes);
            myStream.BeginRead(readBuffer, 0, socket.ReceiveBufferSize, ReceiveDataCallBack, null);
        }

        catch (Exception)
        {
            throw;
        }
    }
}
