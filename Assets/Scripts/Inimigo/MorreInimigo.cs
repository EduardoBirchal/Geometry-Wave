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
        // TODO: Decidir o sistema de XP
        // Por enquanto, será xp igual para todos
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<PlayerGerenciaXP>().xp += valorXp;
        }
        
        Destroy(gameObject);
    }
}