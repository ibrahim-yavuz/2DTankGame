using UnityEngine;

public class UDPClient : MonoBehaviour
{
    public SocketClient socketClient;
    public User user = new User("ibrahim");

    void Start()
    {
        socketClient = new SocketClient();
        socketClient.Connect("192.168.1.108", 65413);
    }
}