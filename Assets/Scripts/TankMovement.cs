using System;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 1f;

    void FixedUpdate()
    {
        ControlTank();
    }

    private void ControlTank()
    {
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
            SendData();
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationSpeed);
            SendData();
        }
    }

    void SendData()
    {
        try
        {
            Vector2 position = transform.position;
            var playerInfo = new PlayerInfo
            {
                name = LobbyController.username,
                pos_x = position.x,
                pos_y = position.y,
                rot_z = transform.eulerAngles.z
            };
            var playerInfoJson = playerInfo.toJson();
            LobbyController.socketClient.SendData(playerInfoJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
