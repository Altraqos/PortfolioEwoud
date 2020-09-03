using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public enum ServerPackets
    {
        SWelcomeMessage = 1,
        SInstantiatePlayer,
        SPlayerPos,
        SPlayerDestroy,
        SPlayerName,
        SEnemyState,
    }

    static class DataReceiver
    {
        public static Dictionary<int, MoveOnlinePlayer> PlayerPosHolder = new Dictionary<int, MoveOnlinePlayer>();

        public static void HandleWelcomeMessage(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInterger();
            string msg = buffer.ReadString();
            int playerID = buffer.ReadInterger();
            buffer.Dispose();
            NetworkManager.instance.playerID = playerID;
            DataSender.SendHelloServer();
            MFServer(msg);
        }
        public static void HandlePlayerDestroy(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInterger();
            string msg = buffer.ReadString();
            int playerID = buffer.ReadInterger();
            buffer.Dispose();
            NetworkManager.instance.DestroyPlayer(playerID);
            MFServer(msg);
        }

        public static void HandleInstantiate(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInterger();
            int index = buffer.ReadInterger();
            string playerName = buffer.ReadString();
            buffer.Dispose();
            if (NetworkManager.instance.playerID != index)
            {
                string[] stringHolder = playerName.Split('#');
                NetworkManager.instance.InstantiatePlayer(index, Int32.Parse(stringHolder[1])) ;
                NetworkManager.instance.playerList[index].GetComponent<MoveOnlinePlayer>().playerID = index;
                NetworkManager.instance.playerList[index].GetComponent<MoveOnlinePlayer>().playerName = stringHolder[0];
            }
            MFServer(playerName);
        }

        public static void HandlePlayerPos(byte[] data)
        {
            //Create a new buffer and read out the incomming message from the server as a string
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInterger();
            string msg = buffer.ReadString();
            buffer.Dispose();
            //create a loop for every client currently online
            foreach (var item in NetworkManager.instance.playerList)
            {
                string[] stringholder = msg.Split('#');
                //Check if the playerID send with the message isn't the same as the one who sent it
                if (NetworkManager.instance.playerList[item.Key].GetComponent<MoveOnlinePlayer>().playerID.ToString() == stringholder[0])
                {
                    //Split the incomming message to get the XYZ position and rotation, then create 2 new vectors. And sent them to the proper online player object
                    MoveOnlinePlayer POPlayer = NetworkManager.instance.playerList[item.Key].GetComponent<MoveOnlinePlayer>();
                    string[] posStringholder = stringholder[1].Split('*');
                    string[] rotStringholder = stringholder[2].Split('*');
                    string[] animStringholder = stringholder[3].Split('*');
                    Vector3 playerPos = new Vector3(float.Parse(posStringholder[0]), float.Parse(posStringholder[1]), float.Parse(posStringholder[2]));
                    Vector3 playerRot = new Vector3(float.Parse(rotStringholder[0]), float.Parse(rotStringholder[1]), float.Parse(rotStringholder[2]));
                    POPlayer.currentPos = playerPos;
                    POPlayer.currentRot = playerRot;
                    POPlayer.inputX = float.Parse(animStringholder[0]);
                    POPlayer.inputZ = float.Parse(animStringholder[1]);
                    POPlayer.isAiming = bool.Parse(animStringholder[2]);
                    POPlayer.isShooting = bool.Parse(animStringholder[3]);
                    POPlayer.isJumping = bool.Parse(animStringholder[4]);
                    POPlayer.isGrounded = bool.Parse(animStringholder[5]);
                }
            }
            MFServer(msg);
        }

        public static void HandleEnemyState(byte[] data)
        {
            //Create a new buffer and read out the incomming message from the server as a string
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInterger();
            string msg = buffer.ReadString();
            buffer.Dispose();

            //create a loop for every client currently online
            foreach (var item in NetworkManager.instance.playerList)
            {
                string[] stringholder = msg.Split('#');
                /*
                if (NetworkManager.instance.playerList[item.Key].GetComponent<MoveOnlinePlayer>().playerID.ToString() == stringholder[0])
                {
                    //Split the incomming message to get the XYZ position and rotation, then create 2 new vectors. And sent them to the proper online player object
                    NetworkManager.instance.playerList[item.Key].GetComponent<MoveOnlinePlayer>().playerName = stringholder[1];
                    Debug.Log(stringholder[1]);
                }
                */
            }

            MFServer("HandleEnemyState" + msg);
        }

        public static void HandlePlayerName(byte[] data)
        {
            //Create a new buffer and read out the incomming message from the server as a string
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packetID = buffer.ReadInterger();
            string msg = buffer.ReadString();
            buffer.Dispose();

            //create a loop for every client currently online
            foreach (var item in NetworkManager.instance.playerList)
            {
                string[] stringholder = msg.Split('#');
                Debug.Log(stringholder[0]);
                Debug.Log(stringholder[1]);
                if (NetworkManager.instance.playerList[item.Key].GetComponent<MoveOnlinePlayer>().playerID.ToString() == stringholder[0])
                {
                    //Split the incomming message to get the XYZ position and rotation, then create 2 new vectors. And sent them to the proper online player object
                    NetworkManager.instance.playerList[item.Key].GetComponent<MoveOnlinePlayer>().playerName = stringholder[1];
                    Debug.Log(stringholder[1]);
                }
            }
        }

        public static void MFServer(string message)
        {
            Debug.Log("Message from server: " + message);
        }
    }
}
