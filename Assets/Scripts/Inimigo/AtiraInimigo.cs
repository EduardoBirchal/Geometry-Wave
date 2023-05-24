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
        atirador = transform.parent.gameObject;
        balaCarregada = false;
        tipoBala = 3;

        GetValores();

        StartCoroutine(Recarrega(Random.Range(velAtirarMin, velAtirarMax)));
    }
    
    void Update()
    {
        Atira(tipoBala);
    }


    void GetValores() {
        ValoresSpawn valSpawn = atirador.GetComponent<ValoresSpawn>();

        TipoTiro.tipos[3].danoBala = valSpawn.danoBala;
        TipoTiro.tipos[3].danoBala = valSpawn.velBala;
        velAtirarMin = valSpawn.velAtirarMin;
        velAtirarMax = valSpawn.velAtirarMax;
    }
}
