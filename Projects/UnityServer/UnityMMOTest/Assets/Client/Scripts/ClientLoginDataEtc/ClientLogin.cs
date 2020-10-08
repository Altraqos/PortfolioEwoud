using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System.Threading;
using System;
using System.Net.NetworkInformation;

public class StateObject
{
    public Socket workSocket = null;  
    public const int BufferSize = 256; 
    public byte[] buffer = new byte[256];
    public StringBuilder sb = new StringBuilder();
}

public class ClientLogin : MonoBehaviour
{ 
    private static ManualResetEvent connectDone =
        new ManualResetEvent(false);
    private static ManualResetEvent sendDone =
        new ManualResetEvent(false);
    private static ManualResetEvent receiveDone =
        new ManualResetEvent(false);

    private static String response = String.Empty;

    private string IP_Adress = "127.0.0.1";
    private int Port = 3085;

    public InputField Username;
    public InputField Password;
    public Text errorText;
    public GameObject ErrorImage;
    public string UsernameString;
    public string PasswordString;

    public int ConnectionID;
    public ForwardConnectionID forwardID;

    private void OnEnable()
    {
        ErrorImage.SetActive(false);
    }

    void Update()
    {
        UsernameString = Username.text;
        PasswordString = Password.text;
    }

    public void LoginButton()
    {
        string loginData = UsernameString + " " + PasswordString;

        if (UsernameString != null && PasswordString != null)
        {
            try
            {
                string IPTEST = "127.0.0.1";
                IPAddress ipAddress = IPAddress.Parse(IPTEST);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);
                Socket client = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
                client.BeginConnect(remoteEP,
                new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
                Send(client, loginData + "<EOF>");
                sendDone.WaitOne();

                Receive(client);
                receiveDone.WaitOne();

                Debug.Log(response);

                try
                {
                    string[] responseHolder = new string[8];
                    responseHolder = response.Split(',');

                    for (int i = 0; i < responseHolder.Length; i++)
                    {
                        //Debug.Log("Response(" + i + ") - " + responseHolder[i]);
                    }

                    if (responseHolder[0] == "canConnect")
                    {
                        ErrorImage.SetActive(false);
                        Int32.TryParse(responseHolder[1], out ConnectionID);
                        forwardID.GetConnectionID(ConnectionID);
                        SceneManager.LoadScene(1);
                    }

                    if (response == "wrongPassword")
                    {
                        ErrorImage.SetActive(true);
                        errorText.text = "Wrong password.";
                        loginData = null;
                    }

                    if (response == "notValidAccount")
                    {
                        ErrorImage.SetActive(true);
                        errorText.text = "Incorrect username or password.";
                        loginData = null;
                    }
                }
                catch
                {
                    if (response == "canConnect")
                    {
                        ErrorImage.SetActive(false);
                        //Int32.TryParse(response, out ConnectionID);
                        forwardID.GetConnectionID(ConnectionID);
                        SceneManager.LoadScene(1);
                    }

                    if (response == "wrongPassword")
                    {
                        ErrorImage.SetActive(true);
                        errorText.text = "Wrong password.";
                        loginData = null;
                    }

                    if (response == "notValidAccount")
                    {
                        ErrorImage.SetActive(true);
                        errorText.text = "Incorrect username or password.";
                        loginData = null;
                    }
                }
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                ErrorImage.SetActive(true);
                errorText.text = "Couldn't connect to the server.";
                //Debug.Log(e.ToString());
            }
        }
    }

    private static void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = (Socket)ar.AsyncState;
            client.EndConnect(ar);
            connectDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Receive(Socket client)
    {
        try
        {
            StateObject state = new StateObject();
            state.workSocket = client;
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReceiveCallback), state);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
  
            int bytesRead = client.EndReceive(ar);

            if (bytesRead > 0)
            {  
                state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            else
            {
                if (state.sb.Length > 1)
                {
                    response = state.sb.ToString();
                }  
                receiveDone.Set();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Send(Socket client, String data)
    { 
        byte[] byteData = Encoding.ASCII.GetBytes(data);
        client.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), client);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = (Socket)ar.AsyncState; 
            int bytesSent = client.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to server.", bytesSent);
            
            sendDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
