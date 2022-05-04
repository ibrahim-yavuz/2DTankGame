using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private string userName = "";

    public User(string userName)
    {
        this.userName = userName;
    }

    public string UserName
    {
        get => userName;
        set => userName = value;
    }
}
