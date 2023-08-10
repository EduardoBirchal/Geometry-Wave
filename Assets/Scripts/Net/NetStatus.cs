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
    public static bool gameStarted = false;
    public static ConnectionResponse status;
    public static int PlayersAlive = 1;

    private void Start()
    {
        status = ConnectionResponse.Waiting;
        MaxNumPlayers = isSingleplayer ? 1 : 4;
    }
}