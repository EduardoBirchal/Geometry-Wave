using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MorreInimigo : NetworkBehaviour
{
    private GameObject player;
    private FlockAgent inimigoFlock;
    PlayerGerenciaXP playerXp;
    public int valorXp;

    void Start()
    {
        player = GameObject.Find("Player");
        
        if(player) playerXp = player.GetComponent<PlayerGerenciaXP>();

        valorXp = GetComponent<ValoresSpawn>().valorXp;
    }

    [ClientRpc] void GivePlayerXpClientRpc(int xp)
    {
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<PlayerGerenciaXP>().xp += xp;
        }
    }

    [ServerRpc]
    public void MatarInimigoServerRpc(ServerRpcParams serverRpcParams = default)
    {
        GivePlayerXpClientRpc(valorXp);

        inimigoFlock = gameObject.GetComponent<FlockAgent>();
        
        if (inimigoFlock.AgentFlock)
            inimigoFlock.AgentFlock.removeAgents(inimigoFlock);    

        Destroy(gameObject);
    }
}