using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AtiraPlayer : Atirador
{
    public int balaAtual = 0;
    public bool tiroAutomatico;
    private MudaBala mudaBala;
    private PlayerNetwork PlayerNet;

    void Start() 
    {
        tipos = GameObject.Find("Funcoes").GetComponent<TipoTiro>().player;
        mudaBala = atirador.GetComponent<MudaBala>();
        PlayerNet = atirador.transform.root.GetComponent<PlayerNetwork>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerNet.CheckForClient() == false) return;
        balaAtual = mudaBala.modoTiro.Value;
        QuerAtirar();
    }

    void QuerAtirar() {
        if(PlayerNet.CheckForClient())
        {
            if(Input.GetMouseButton(0) || tiroAutomatico) {
                AtiraServerRpc(balaAtual);
            }
        }
    }
}
