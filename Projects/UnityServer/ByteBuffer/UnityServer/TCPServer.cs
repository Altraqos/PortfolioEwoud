using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace UnityServer
{
    public enum ServerPackets
    {
        S_INFORMATION = 1,
        S_EXECUTEMETHODONCLIENT,
        S_PLAYERDATA,
        S_PLAYERPOSTION,
    }

    public enum ClientPackets
    {
        C_CONNECT = 1,
        C_POSITION, 
    }

    class TCPServer
    {

        private TcpListener serverSocket;

        public static Clients[] Client = new Clients[1500];

        public void InitNetwork()
        {
            for (int i = 0; i < Client.Length; i++)
            {
                Client[i] = new Clients();
            }

            serverSocket = new TcpListener(IPAddress.Any, Program.GameServerPort);
            serverSocket.Start();

            Console.WriteLine("Server Online And Ready For Connections");
            Console.WriteLine(" ");

            while (true)
            {
                serverSocket.BeginAcceptTcpClient(ClientConnectCallBack, null);
            }
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

                    Program.currentPlayersOnline++;
                    Console.WriteLine(Program.currentPlayersOnline);
                    Console.WriteLine("Connection received from user with ip: " + Client[i].ip + ".");

                    SendJoinGame(i);

                    return;
                }
            }
        }

        public static void ShowActivePlayers()
        {
            Console.WriteLine("Now " + Program.currentPlayersOnline + " users online.");

            for (int i = 0; i < Program.currentPlayersOnline; i++)
            {
                Console.WriteLine(Client[i].userName + " | " + Client[i].connectionID);
            }
        }

        public void SendDataTo(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteLong((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
            buffer.WriteBytes(data);
            Client[connectionID].myStream.BeginWrite(buffer.TooArray(), 0, buffer.TooArray().Length, null, null);
        }

        public void SendDataToAll(byte[] data)
        {
            for (int i = 0; i < Client.Length; i++)
            {
                if (Client[i].socket != null)
                    SendDataTo(i, data);
            }
        }

        //Send Methods To Client
        /*public void SendInformation(int connectionID)
        {
            ByteBuffer buffer = new ByteBuffer();

            //Add the packet identifier
            buffer.WriteLong((long)ServerPackets.S_INFORMATION);

            //Adds data to package

            buffer.WriteString("Welcome To The Server.");
            buffer.WriteString("You are now able to play."); //String from server to client
            buffer.WriteInteger(10); //Ints from server to client

            SendDataTo(connectionID, buffer.TooArray());
        }*/

            /*

        public void SendExecuteMethodOnClient(int connectionID)
        {
            ByteBuffer buffer = new ByteBuffer();

            //PacketIdentifier
            buffer.WriteLong((long)ServerPackets.S_EXECUTEMETHODONCLIENT);

            SendDataTo(connectionID, buffer.TooArray());
        }

    */

        //All data from player for logging in, level, name, inventory, stats, etc.
        public byte[] PlayerData(int connectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteLong((long)ServerPackets.S_PLAYERDATA);
            buffer.WriteInteger(connectionID);
            return buffer.TooArray();
        }

        public void SendJoinGame(int connectionID)
        {
            ByteBuffer buffer = new ByteBuffer();

            //Send all players on current map to new player

            for (int i = 0; i < Client.Length; i++)
            {
                if (Client[i].socket != null)
                {
                    if (i != connectionID)
                    {
                        SendDataTo(connectionID, PlayerData(i));
                    }
                }
            }

            //Send new playerdata to everyone, INCLUDING himself

            SendDataToAll(PlayerData(connectionID));
        }

        public void SendPlayerPostions(int connectionID)
        {
            ByteBuffer buffer = new ByteBuffer();

            //Send all players on current map to new player

            for (int i = 0; i < Client.Length; i++)
            {
                if (Client[i].socket != null)
                {
                    if (i != connectionID)
                    {
                        SendDataTo(connectionID, PlayerData(i));
                    }
                }
            }

            //Send new playerdata to everyone, INCLUDING himself

            SendDataToAll(PlayerData(connectionID));
        }
    }
}
