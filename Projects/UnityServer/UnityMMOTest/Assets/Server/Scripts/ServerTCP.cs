using System;
using System.Net.Sockets;
using System.Net;

class ServerTCP
{
    private TcpListener serverSocket;

    public Clients[] Client = new Clients[1500];

    public void InitNetwork()
    {
        serverSocket = new TcpListener(IPAddress.Any, 5555);
        serverSocket.Start();
        serverSocket.BeginAcceptTcpClient(ClientConnectCallBack, null);
    }

    private void ClientConnectCallBack(IAsyncResult result)
    {
        TcpClient tempClient = serverSocket.EndAcceptTcpClient(result);
        serverSocket.BeginAcceptTcpClient(ClientConnectCallBack, null);

        for (int i = 0; i < Client.Length; i++)
        {
            if (Client[i].socket == null)
            {
                Client[i].socket = tempClient;
                Client[i].connectionID = i;
                Client[i].ip = tempClient.Client.RemoteEndPoint.ToString();
                Client[i].Start();
                Console.WriteLine("Connection received from " + Client[i].ip + ".");
                return;
            }
        }
    }
}

