using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ClientTCP : MonoBehaviour
{
    public static string userName;
    public static string password;

    public static ClientTCP instance;

    public TcpClient client;
    public NetworkStream myStream;
    private byte[] aSyncBuffer;
    public bool isConnected;

    public byte[] receivedBytes;
    public bool handleData = false;


    private string IP_Adress = "127.0.0.1";
    private int Port = 3085;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (handleData)
        {
            ClientHandlePackets.HandleData(receivedBytes);
            handleData = false;
        }
    }

    public void Connect()
    {
        Debug.Log("Trying to connect to the server");
        client = new TcpClient();
        client.ReceiveBufferSize = 4096;
        client.SendBufferSize = 4096;
        aSyncBuffer = new byte[8192];

        try
        {
            client.BeginConnect(IP_Adress, Port, new AsyncCallback(ConnectCallBack), client);
        }

        catch
        {
            Debug.Log("Unable to Connect to server");
        }
    }

    private void ConnectCallBack(IAsyncResult result)
    {
        try
        {
            client.EndConnect(result);
            if (client.Connected == false)
            {
                return;
            }

            else
            {
                myStream = client.GetStream();
                myStream.BeginRead(aSyncBuffer, 0, 8192, OnReceiveData, null);
                isConnected = true;
            }
        }

        catch (Exception)
        {
            isConnected = false;
            return;
        }
    }

    private void OnReceiveData(IAsyncResult result)
    {
        try
        {
            int packetLength = myStream.EndRead(result);
            receivedBytes = new byte[packetLength];
            Buffer.BlockCopy(aSyncBuffer, 0, receivedBytes, 0, packetLength);

            if (packetLength == 0)
            {
                Debug.Log("Disconnected");
                Application.Quit();
                return;
            }

            handleData = true;
            myStream.BeginRead(aSyncBuffer, 0, 8192, OnReceiveData, null);
        }

        catch (Exception)
        {
            Debug.Log("Disconnected");
            Application.Quit();
            return;
        }
    }

    public void SendData(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteLong((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
        buffer.WriteBytes(data);
        myStream.Write(buffer.TooArray(), 0, buffer.TooArray().Length);
    }

    public void SEND_CONNECT()
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteLong((long)(ClientPackets.C_CONNECT));

        string Time;
        Time = DateTime.Now.ToString("HH:mm");

        buffer.WriteString("[" + Time + "] - " + "Succesfully Connected Client.");
        SendData(buffer.TooArray());
    }
}

