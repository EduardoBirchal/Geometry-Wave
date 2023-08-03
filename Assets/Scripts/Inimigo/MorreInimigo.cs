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
        playerXp = player.GetComponent<PlayerGerenciaXP>();

        valorXp = GetComponent<ValoresSpawn>().valorXp;
    }

    [ServerRpc]
    public void MatarInimigoServerRpc(ServerRpcParams serverRpcParams = default)
    {
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<PlayerGerenciaXP>().xp += valorXp;
        }
        

        inimigoFlock = gameObject.GetComponent<FlockAgent>();

          
        inimigoFlock.AgentFlock.removeAgents(inimigoFlock);    

        Destroy(gameObject);
    }
}