using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LoginServer
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    public class ForwardToWorldServer
    {
        public static int TCPPort;

        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public static void StartListening()
        {
            IPAddress ipAddress = IPAddress.Parse(ServerData.ServerIP);
            int serverPort = 3085;
            Int32.TryParse(ServerData.LoginServerPort, out serverPort);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, serverPort);
            Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    allDone.Reset();
                    Console.WriteLine("Waiting for a connection from the clients.");
                    listener.BeginAccept(
                    new AsyncCallback(AcceptCallback), listener);
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.ASCII.GetString(
                state.buffer, 0, bytesRead));
                content = state.sb.ToString();

                if (content.IndexOf("<EOF>") > -1)
                {
                    string SendUserData = "account login " + content;
                    string SendUserDataHolder = SendUserData.Replace("<EOF>", null);

                    try
                    {
                        string[] ConsoleStringArray = SendUserDataHolder.Split(' ');
                        string UserName;
                        UserName = ConsoleStringArray[2];
                        string UserDataString = StartServerExectutables.UserDataFolder + @"\" + UserName + " - UserData.txt";

                        if (File.Exists(UserDataString))
                        {
                            string[] UserDataText = File.ReadAllLines(UserDataString);
                            Int32.TryParse(ServerData.LoginTCPServerPort, out TCPPort);

                            if (UserDataText[1] == ConsoleStringArray[3])
                            {
                                TcpClient TCPClient = new TcpClient(ServerData.ServerIP, TCPPort);
                                NetworkStream nwStreamTCP = TCPClient.GetStream();
                                byte[] bytesToSend;
                                bytesToSend = ASCIIEncoding.ASCII.GetBytes(SendUserDataHolder);
                                nwStreamTCP.Write(bytesToSend, 0, bytesToSend.Length);

                                DebugConsole.CleanLine();
                                Send(handler, "canConnect");
                            }
                            else
                            {
                                DebugConsole.SlowlyWriteError("Wrong password for user: " + UserName + ".");
                                Send(handler, "wrongPassword");
                            }
                        }
                        else
                        {
                            DebugConsole.SlowlyWriteError("This account doesn't exist.");
                            Send(handler, "This account doesn't exist.");
                        }
                    }
                    catch
                    {
                        DebugConsole.SlowlyWriteError("Error couldn't login.");
                        Send(handler, "Error couldn't login.");
                    }
                }
                else
                {
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
