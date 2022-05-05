using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public struct UdpState
{
    public UdpClient u;
    public IPEndPoint e;
}

public class SocketClient
{
    public UdpClient client;
    private int _sendPort;
    public string receivedData;

    public SocketClient()
    {
        try
        {
            client = new UdpClient(65434);
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
            SendData("connected");
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
                UdpState state = new UdpState();

                state.e = remoteEndPoint;
                state.u = client;

                client.BeginReceive(new AsyncCallback(ReceiveCallback), state);
            }
            return "NoData";

        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void SendData(string data)
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

    public void ReceiveCallback(IAsyncResult ar)
    {
        UdpClient u = ((UdpState)(ar.AsyncState)).u;
        IPEndPoint e = ((UdpState)(ar.AsyncState)).e;

        byte[] receivedBytes = u.EndReceive(ar, ref e);
        string receiveString = Encoding.ASCII.GetString(receivedBytes);
        receivedData = receiveString;
    }
}