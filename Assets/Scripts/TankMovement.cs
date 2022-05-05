using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 1f;
    private GameObject _clientGameObject;
    private UDPClient _udpClient;
    private SocketClient _socketClient;
    private String _userName;

    public GameObject tank;

    private bool _hasGameStarted = false;
    
    void Start()
    {
        _clientGameObject = GameObject.FindGameObjectWithTag("client");
        _udpClient = _clientGameObject.GetComponent<UDPClient>();
        _socketClient = _udpClient.socketClient;
        _userName = _udpClient.user.UserName;
    }

    void Update()
    {
        if (_socketClient.GetData().Equals("start"))
        {
            _hasGameStarted = true;
        }
        if (true)
        {
            SendData();
            GetDataAndMoveObject();
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * speed * Time.deltaTime;
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.up * -1 * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationSpeed);
        }
    }

    void SendData()
    {
        try
        {
            Vector2 position = transform.position;
            PlayerInfo playerInfo = new PlayerInfo
            {
                name = _userName,
                pos_x = position.x,
                pos_y = position.y,
                rot_z = transform.eulerAngles.z
            };
            string playerInfoJson = JsonConvert.SerializeObject(playerInfo);
            _socketClient.SendData(playerInfoJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    void GetDataAndMoveObject()
    {
        _socketClient.GetData();
        String data = _socketClient.receivedData;
        Debug.Log("Data: " + data);
        if (!string.IsNullOrEmpty(data))
        {
            try
            {
                PlayerInfo playerInfo = JsonConvert.DeserializeObject<PlayerInfo>(data);
                float rotationZ = playerInfo.rot_z;
                float positionX = playerInfo.pos_x;
                float positionY = playerInfo.pos_y;
                String name = playerInfo.name;
                
                if (name == "carry")
                {
                    tank.transform.position = new Vector3(positionX, positionY, 0);
                    tank.transform.rotation =  Quaternion.Euler(0, 0, rotationZ);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
