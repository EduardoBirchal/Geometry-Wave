using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AtiraPlayer : Atirador
{
    public int balaAtual = 0;
    private MudaBala mudaBala;
    private PlayerNetwork PlayerNet;

    void Start() {
        mudaBala = atirador.GetComponent<MudaBala>();
        PlayerNet = atirador.transform.root.GetComponent<PlayerNetwork>();

    }

    // Update is called once per frame
    void Update()
    {
        balaAtual = mudaBala.modoTiro;
        QuerAtirar();
    }

    void QuerAtirar() {
        if(PlayerNet.CheckForClient())
        {
            if(Input.GetMouseButton(0)) {
                AtiraServerRpc(balaAtual);
            }
        }
    }
}
