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
    public NetworkVariable<int> PlayersAlive;
    private TimeManager time_Script;

    private void OnNetworkStart()
    { 
        PlayersAlive = new(
        value:1,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
        );
    }
    private void Start()
    {
        gameStarted = false;
        MaxNumPlayers = isSingleplayer ? 1 : 4;
        time_Script = GameObject.Find("GameManager").GetComponent<TimeManager>();
        status = ConnectionResponse.Waiting;
    }

    private void Update()
    {
        if(time_Script.globalPause.Value == true)
            Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}