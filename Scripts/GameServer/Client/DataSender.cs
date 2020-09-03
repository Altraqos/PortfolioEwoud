using UnityEngine;
namespace Assets.Scripts
{
    public enum ClientPackets
    {
        CHelloServer = 1,
        CPlayerPos,
        CPlayerName,
        CEnemyState,
    }


    static class DataSender
    {
        public static void SendPlayerName(string PlayerName)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInterger((int)ClientPackets.CPlayerName);
            buffer.WriteString(PlayerName);
            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();
        }

        public static void SendHelloServer()
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInterger((int)ClientPackets.CHelloServer);
            buffer.WriteString(NetworkManager.instance.playerName + " has succesfully connected to server!");
            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();     
        }

        public static void SendPlayerPos(string playerPosXYZ)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInterger((int)ClientPackets.CPlayerPos);
            buffer.WriteString(playerPosXYZ);
            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();
        }
        
        public static void SendEnemyState(string enemyState)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInterger((int)ClientPackets.CEnemyState);
            buffer.WriteString(enemyState);
            ClientTCP.SendData(buffer.ToArray());
            buffer.Dispose();
        }
    }
}
