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

        GetValores();

        StartCoroutine(Recarrega(velAtirarMin, velAtirarMax));

        tipoBala = 3;
    }
    
    void Update()
    {
        AtiraBalaServerRpc(tipoBala);
    }

    [ServerRpc]
    protected new void AtiraBalaServerRpc(int bala) {
        if(balaCarregada) {
            balaCarregada = false;

            CriaBala(bala);
            StartCoroutine(Recarrega(velAtirarMin, velAtirarMax));
        }
    }

    protected IEnumerator Recarrega(float tempoMin, float tempoMax) {
        yield return new WaitForSeconds(Random.Range(tempoMin, tempoMax));

        balaCarregada = true;
    }

    void GetValores() {
        ValoresSpawn valSpawn = atirador.GetComponent<ValoresSpawn>();

        TipoTiro.tipos[3].danoBala = valSpawn.danoBala;
        TipoTiro.tipos[3].danoBala = valSpawn.velBala;
        velAtirarMin = valSpawn.velAtirarMin;
        velAtirarMax = valSpawn.velAtirarMax;
    }
}
