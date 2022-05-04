using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    
    void Start()
    {
        _clientGameObject = GameObject.FindGameObjectWithTag("client");
        _udpClient = _clientGameObject.GetComponent<UDPClient>();
        _socketClient = _udpClient.socketClient;
        _userName = _udpClient.user.UserName;
        
        SendData();
    }

    void Update()
    {
        GetDataAndMoveObject();
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * speed * Time.deltaTime;
            SendData();
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.up * -1 * speed * Time.deltaTime;
            SendData();
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
            _socketClient.SendData(_userName + ":" + position.x + ", " + position.y);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    void GetDataAndMoveObject()
    {
        String data = _socketClient.GetData();
        if (!string.IsNullOrEmpty(data))
        {
            float positionX = float.Parse(data.Split(':')[1].Split(',')[0]);
            float positionY = float.Parse(data.Split(':')[1].Split(',')[1].Replace(" ", ""));
            String name = data.Split(':')[0];
                
            if (name == "carry")
            {
                tank.transform.position = new Vector3(positionX, positionY, 0);
            }
        }
    }
}
