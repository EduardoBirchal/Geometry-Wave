using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;
using System.Collections;

public enum ConnectionResponse
{
    Waiting,
    Connected,
    Offline
}

public class NetStatus : NetworkBehaviour
{
    public static int MaxNumPlayers;
    public static bool isSingleplayer = true;
    public static bool gameStarted;
    public static ConnectionResponse status;
    public static int PlayersAlive;

    private void Start()
    {
        PlayersAlive = 1;
        gameStarted = false;
        status = ConnectionResponse.Waiting;
        MaxNumPlayers = isSingleplayer ? 1 : 4;
    }

    private void Update()
    {
        if(TimeManager.globalPause.Value == true)
            Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}