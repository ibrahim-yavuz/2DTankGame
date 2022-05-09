using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UnityEngine;

public class User
{
    public string username {get; set; }
    public string host {get; set; }
    public int port {get; set; }

    public User(string username, string host, int port)
    {
        this.username = username;
        this.host = host;
        this.port = port;
    }

    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static User fromJson(string jsonString)
    {
        return JsonConvert.DeserializeObject<User>(jsonString);
    }
}
