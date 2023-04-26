using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiraInimigo : Atirador
{
    public int numBalas;
    public float velBala, velAtirarMax, velAtirarMin, imprecisaoBala, arcoTiro, danoBala;
    public string nomeBala;
    public GameObject objBala;

    private TipoBala tipoBala;

    void Start() 
    {
        atirador = transform.parent.gameObject;
        balaCarregada = false;

        GetValores();

        StartCoroutine(Recarrega(velAtirarMin, velAtirarMax));

        tipoBala = new TipoBala(numBalas, danoBala, velBala, velAtirarMax, imprecisaoBala, arcoTiro, nomeBala, objBala);
    }
    
    void Update()
    {
        AtiraBala(tipoBala);
    }

    protected new void AtiraBala(TipoBala bala) {
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

        danoBala = valSpawn.danoBala;
        velBala = valSpawn.velBala;
        velAtirarMin = valSpawn.velAtirarMin;
        velAtirarMax = valSpawn.velAtirarMax;
    }
}
