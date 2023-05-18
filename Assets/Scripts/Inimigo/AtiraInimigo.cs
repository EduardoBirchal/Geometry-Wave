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

        StartCoroutine(Recarrega(2.0f));
        // StartCoroutine(Recarrega(/*Random.Range(velAtirarMin, velAtirarMax)*/2.0f));
    }
    
    void Update()
    {
        AtiraBalaServerRpc(tipoBala);
    }

    // [ServerRpc]
    // protected void InimigoAtiraServerRpc(int bala) {
    //     if(balaCarregada) {
    //         balaCarregada = false;

    //         CriaBala(bala);
    //         StartCoroutine(Recarrega(/*Random.Range(velAtirarMin, velAtirarMax)*/3));
    //     }
    // }

    // protected IEnumerator Recarrega(float tempoMin, float tempoMax) {
    //     yield return new WaitForSeconds(Random.Range(tempoMin, tempoMax));

    //     balaCarregada = true;
    // }

    void GetValores() {
        ValoresSpawn valSpawn = atirador.GetComponent<ValoresSpawn>();

        TipoTiro.tipos[3].danoBala = valSpawn.danoBala;
        TipoTiro.tipos[3].danoBala = valSpawn.velBala;
        velAtirarMin = valSpawn.velAtirarMin;
        velAtirarMax = valSpawn.velAtirarMax;
    }
}
