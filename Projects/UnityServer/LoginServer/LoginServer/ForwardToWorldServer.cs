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
            IPAddress ipAddress = IPAddress.Parse(InitializeServerData.Server_Ip);
            int serverPort = 3085;
            Int32.TryParse(InitializeServerData.LoginServerPort, out serverPort);
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
                    DebugConsole.SlowlyWrite("Waiting for a connection from the clients.");
                    DebugConsole.CleanLine();
                    listener.BeginAccept(
                    new AsyncCallback(AcceptCallback), listener);
                    DebugConsole.ShowActivePlayers();
                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                DebugConsole.SlowlyWriteError(e.ToString());
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
                        string UserDataString = ServerDataClass.UserDataFolder + @"\" + UserName + " - UserData.txt";

                        if (File.Exists(UserDataString))
                        {
                            string[] UserDataText = File.ReadAllLines(UserDataString);

                            if (UserDataText[1] == ConsoleStringArray[3])
                            {
                                Send(handler, "canConnect");
                            }
                            else
                            {
                                Send(handler, "wrongPassword");
                            }
                        }
                        else
                        {
                            Send(handler, "notValidAccount");
                        }
                    }
                    catch
                    {
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