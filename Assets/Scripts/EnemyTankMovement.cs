using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTankMovement : MonoBehaviour
{
    private List<GameObject> _tanks;
    private SocketClient _socketClient;

    void Start()
    {
        _socketClient = LobbyController.socketClient;
        _tanks = UDPClient.enemyTanks;
    }
    
    void Update()
    {
        _socketClient.GetData();
        GetDataAndMoveObject();
    }
    
    void GetDataAndMoveObject()
    {
        try
        {
            var data = _socketClient.receivedData;
            if (string.IsNullOrEmpty(data)) return;
            var playerInfo = PlayerInfo.fromJson(data);
            var rotationZ = playerInfo.rot_z;
            var positionX = playerInfo.pos_x;
            var positionY = playerInfo.pos_y;
            var username = playerInfo.name;

            int tankIndex = findTankByUserName(username);

            if (username != LobbyController.users.ElementAt(tankIndex).username) return;
            _tanks.ElementAt(tankIndex).transform.position = new Vector3(positionX, positionY, 0);
            _tanks.ElementAt(tankIndex).transform.rotation =  Quaternion.Euler(0, 0, rotationZ);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    int findTankByUserName(string username)
    {
        for (int i = 0; i < LobbyController.users.Count; i++)
        {
            if (username == LobbyController.users.ElementAt(i).username)
            {
                return i;
            }
        }

        return 0;
    }
}
