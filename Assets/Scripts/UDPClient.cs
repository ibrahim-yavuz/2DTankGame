using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UDPClient : MonoBehaviour
{
    public GameObject enemyTankPrefab;
    public static List<GameObject> enemyTanks = new List<GameObject>();

    void Start()
    {
        Debug.Log(LobbyController.users.Count);
        for (var i = 0; i < LobbyController.users.Count; i++)
        {
            var enemyTank = Instantiate(enemyTankPrefab, Vector3.zero, Quaternion.identity);
            enemyTanks.Add(enemyTank);
        }
    }
}