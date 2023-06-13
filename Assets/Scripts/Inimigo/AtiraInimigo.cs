using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AtiraInimigo : Atirador
{
    public float velAtirarMax, velAtirarMin;
    public string nomeBala;
    private int tipoBala;

    void Start() 
    {
        tipos = GameObject.Find("Funcoes").GetComponent<TipoTiro>().inimigo;
        atirador = transform.parent.gameObject;
        balaCarregada = false;
        tipoBala = 0;

        GetValores();

        StartCoroutine(Recarrega(Random.Range(velAtirarMin, velAtirarMax)));
    }
    
    void Update()
    {
        if(IsServer)
            AtiraServerRpc(tipoBala);
    }


    void GetValores() {
        ValoresSpawn valSpawn = atirador.GetComponent<ValoresSpawn>();

        tipos[tipoBala].danoBala = valSpawn.danoBala;
        tipos[tipoBala].danoBala = valSpawn.velBala;
        velAtirarMin = valSpawn.velAtirarMin;
        velAtirarMax = valSpawn.velAtirarMax;
    }
}
