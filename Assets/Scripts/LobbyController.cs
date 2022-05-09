using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class LobbyController : MonoBehaviour
{
    public GameObject playersListPanel;
    public GameObject settingsPanel;
    public InputField userNameTextField;
    public InputField ipTextField;
    public InputField portTextField;
    public GameObject[] playerTexts;

    public static List<User> users = new List<User>();
    
    public static SocketClient socketClient;
    public static string username;
    private string _host;
    private int _port;

    private int _textCount = 0;
   
    private void Start()
    {
        socketClient = new SocketClient();
    }

    private void Update()
    {
        socketClient.GetData();
        
        Debug.Log("Gelen Veri: " + socketClient.receivedData);

        if (socketClient.receivedData.Equals(Constants.START_COMMAND))
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            try
            {
                var user = User.fromJson(socketClient.receivedData);
                if (!doesContain(user.username))
                {
                    playerTexts.ElementAt(_textCount).SetActive(true);
                    playerTexts.ElementAt(_textCount).GetComponent<Text>().text = user.username;
                    _textCount++;
                    users.Add(user);
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
        }
        
    }

    public void OnPressedCreateRoomButton()
    {
        playersListPanel.SetActive(true);
        settingsPanel.SetActive(false);
        
        _host = ipTextField.text;
        _port = Convert.ToInt32(portTextField.text);
        
        username = userNameTextField.text;
        
        socketClient.Connect(_host, _port);
        socketClient.SendData(username);
    }

    public void StartTheGame()
    {
        socketClient.SendData(Constants.START_COMMAND);
        SceneManager.LoadScene("SampleScene");
    }

    private bool doesContain(string username)
    {
        foreach (var user in users)
        {
            if (user.username.Equals(username))
            {
                return true;
            }
        }

        return false;
    }
}
