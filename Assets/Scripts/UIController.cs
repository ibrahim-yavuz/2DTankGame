using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UIController : MonoBehaviour
{
    public GameObject PlayersListPanel;
    public GameObject SettingsPanel;
    public InputField userNameTextField;
    public Button createRoomButton;

    private void Update()
    {
        if (userNameTextField.text.Length > 3)
        {
            createRoomButton.interactable = true;
        }
        else
        {
            createRoomButton.interactable = false;
        }
    }

    public void OnPressedCreateRoomButton()
    {
        
            PlayersListPanel.SetActive(true);
            SettingsPanel.SetActive(false);

    }
}
