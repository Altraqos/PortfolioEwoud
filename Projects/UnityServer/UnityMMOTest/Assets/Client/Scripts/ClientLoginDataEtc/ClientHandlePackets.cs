using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerPackets
{
    S_INFORMATION = 1,
    S_EXECUTEMETHODONCLIENT,
    S_PLAYERDATA,
}

public enum ClientPackets
{
    C_LOGIN = 1,
    C_CONNECT,
}

public class ClientHandlePackets : MonoBehaviour {

    public static Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();
    public GameObject playerPref;

    public static ByteBuffer playerBuffer;
    private delegate void Packet_(byte[] data);
    private static Dictionary<long, Packet_> packets;
    private static long pLength;

    private void Awake()
    {
        InitializePackets();
    }

    private void InitializePackets()
    {
        packets = new Dictionary<long, Packet_>();
        packets.Add((long)ServerPackets.S_INFORMATION, PACKET_INFORMATION);
        packets.Add((long)ServerPackets.S_EXECUTEMETHODONCLIENT, PACKET_EXECUTEMETHOD);
        packets.Add((long)ServerPackets.S_PLAYERDATA, PACKET_PLAYERDATA);
    }

    public static void HandleData(byte[] data)
    {
        byte[] Buffer;
        Buffer = (byte[])data.Clone();

        if (playerBuffer == null)
        {
            playerBuffer = new ByteBuffer();
        }

        playerBuffer.WriteBytes(Buffer);

        if (playerBuffer.Count() == 0)
        {
            playerBuffer.Clear();
            return;
        }

        if (playerBuffer.Length() >= 8)
        {
            pLength = playerBuffer.ReadLong(false);
            if (pLength <= 0)
            {
                playerBuffer.Clear();
                return;
            }
        }

        while (pLength > 0 & pLength <= playerBuffer.Length() - 8)
        {
            if (pLength <= playerBuffer.Length() - 8)
            {
                playerBuffer.ReadLong(); // reads our packet identifier
                data = playerBuffer.ReadBytes((int)pLength); // get full package length
                HandleDataPackets(data);
            }

            pLength = 0;

            if (playerBuffer.Length() >= 8)
            {
                pLength = playerBuffer.ReadLong(false);

                if (pLength < 0)
                {
                    playerBuffer.Clear();
                    return;
                }
            }
        }
    }
    private static void HandleDataPackets(byte[] data)
    {
        long packetIdentifier; ByteBuffer buffer;
        Packet_ packet;

        buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        packetIdentifier = buffer.ReadLong();
        buffer.Dispose();

        if (packets.TryGetValue(packetIdentifier, out packet))
        {
            packet.Invoke(data);
        }
    }

    private static void PACKET_INFORMATION(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        long packetIdentifier = buffer.ReadLong();
        string msg1 = buffer.ReadString();
        string msg2 = buffer.ReadString();
        int level = buffer.ReadInterger();

        Debug.Log(msg1 + msg2 + level);

        ClientTCP.instance.SEND_CONNECT();
    }

    private void PACKET_PLAYERDATA(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        long packetIdentifier = buffer.ReadLong();
        int connectionID = buffer.ReadInterger();

        GameObject tempPlayer = playerPref;
        tempPlayer.name = "Player: " + connectionID;
        playerList.Add(connectionID, tempPlayer);

        Instantiate(playerList[connectionID]);
    }

    private static void PACKET_EXECUTEMETHOD(byte[] data)
    {
        Debug.Log("I'm getting executed from the server");
    }
}