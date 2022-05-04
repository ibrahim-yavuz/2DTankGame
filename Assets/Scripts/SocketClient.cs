using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SocketClient
{
    public UdpClient client;
    private int _sendPort;

    public SocketClient()
    {
        try
        {
            client = new UdpClient(65431);
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Connect(string hostName, int port)
    {
        _sendPort = port;
        try
        {
            client?.Connect(hostName, port);
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string GetData()
    {
        try
        {
            if (client != null)
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, _sendPort);

                var receiveBytes = client.Receive(ref remoteEndPoint);
                var receivedString = Encoding.ASCII.GetString(receiveBytes);

                return receivedString;
            }
            return "NoData";

        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async void SendData(string data)
    {
        try
        {
            if (client == null) return;
            
            var sendBytes = Encoding.ASCII.GetBytes(data);
            client.Send(sendBytes, sendBytes.Length);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

