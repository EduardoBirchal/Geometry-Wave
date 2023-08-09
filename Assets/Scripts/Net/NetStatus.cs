using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using System.Net;
using System.Linq;
using System.Collections;

public class NetStatus : NetworkBehaviour
{
    public static MaxNumPlayers;
    public static bool isSingleplayer = true;
    public static bool gameStarted = false;

    private void Start()
    {
        MaxNumPlayers = isSingleplayer ? 1 : 4;
    }
}