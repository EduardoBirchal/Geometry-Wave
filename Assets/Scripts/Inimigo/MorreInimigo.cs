using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MorreInimigo : NetworkBehaviour
{
    private GameObject player;
    PlayerGerenciaXP playerXp;
    public int valorXp;

    void Start()
    {
        player = GameObject.Find("Player");
        playerXp = player.GetComponent<PlayerGerenciaXP>();

        valorXp = GetComponent<ValoresSpawn>().valorXp;
    }

    [ServerRpc]
    public void MatarInimigoServerRpc(ServerRpcParams serverRpcParams = default)
    {
        // # FICA PRA QUANDO DECIDIRMOS COMO FUNCIONA O XP
        // ulong clientId = serverRpcParams.Receive.SenderClientId;
        // GameObject player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject.gameObject;
        // player.GetComponent<PlayerGerenciaXP>().xp = valorXp;
        Destroy(gameObject);
    }
}