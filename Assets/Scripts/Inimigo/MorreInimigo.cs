using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorreInimigo : MonoBehaviour
{
    GameObject player;
    PlayerGerenciaXP playerXp;

    public int valorXp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerXp = player.GetComponent<PlayerGerenciaXP>();

        valorXp = GetComponent<ValoresSpawn>().valorXp;
    }

    public void Morre() {
        playerXp.xp += valorXp;
        Destroy(gameObject);
    }
}