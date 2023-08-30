using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AtiraInimigo : Atirador
{
    private float velAtirarMax, velAtirarMin;
    [SerializeField] protected int tipoBala;

    void Start() 
    {
        tipos = GameObject.Find("GameManager").GetComponent<TipoTiro>().inimigo;
        atirador = transform.parent.gameObject;
        balaCarregada = false;

        GetValores();

        StartCoroutine(Recarrega(Random.Range(velAtirarMin, velAtirarMax)));
    }
    
    void Update()
    {
        if(IsServer)
            AtiraServerRpc(tipoBala);
    }


    protected void GetValores() {
        ValoresSpawn valSpawn = atirador.GetComponent<ValoresSpawn>();


        //ALTERA DPS//
        if(tipos[tipoBala].danoBala < 4 && tipos[tipoBala].danoBala > 0.5f){
            tipos[tipoBala].danoBala *= valSpawn.multiplicadorDanoBala;
            tipos[tipoBala].velBala *= valSpawn.multiplicadorVelBala;
        }

        velAtirarMin = valSpawn.velAtirarMin;
        velAtirarMax = valSpawn.velAtirarMax;
    }
}
