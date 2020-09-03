using System;

namespace GameServer
{
    public enum ClientPackets
    {
        CHelloServer = 1,
        CPlayerPos,
        CPlayerName,
        CEnemyState,
    }

    static class DataReceiver
    {
        public static void HandlePlayerName(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInterger();
            string msg = buffer.ReadString();
            string[] playerName = msg.Split('#');
            ClientManager.client[connectionID].playerName = msg;
            ClientManager.client[connectionID].charVal = Int32.Parse(playerName[1]);
            ClientManager.ShowOnlinePlayers();
            DataSend.SendPlayerName(connectionID, msg);
            ClientManager.InstantiatePlayer(connectionID, msg);
        }

        public static void HandleHelloServer(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInterger();
            string msg = buffer.ReadString();
            MFClient(msg);
        }

        public static void HandlePlayerPos(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInterger();
            string msg = buffer.ReadString();
            ClientManager.PositionPlayer(connectionID, msg);
        }
        
        public static void HandleEnemyState(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInterger();
            string msg = buffer.ReadString();
            ClientManager.EnemyState(connectionID, msg);
            MFClient(msg);
        }

        static void MFClient(string messageToWrite)
        {
            WriteToConsole.writeVarData("Messsage from client: *" + messageToWrite + "*", ConsoleColor.Yellow);
        }
    }
}
