using Newtonsoft.Json;

class PlayerInfo
{
    public string name { get; set; }
    public float pos_x { get; set; }
    public float pos_y { get; set; }
    public float rot_z { get; set; }
    
    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static PlayerInfo fromJson(string jsonString)
    {
        return JsonConvert.DeserializeObject<PlayerInfo>(jsonString);
    }
}
