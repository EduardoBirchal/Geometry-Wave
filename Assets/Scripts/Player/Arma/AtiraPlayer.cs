using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AtiraPlayer : Atirador
{
    public int balaAtual = 0;
    public bool tiroAutomatico;
    private MudaBala mudaBala;
    private MenuManager menu;

    void Start() 
    {
        tipos = GameObject.Find("Funcoes").GetComponent<TipoTiro>().player;
        mudaBala = atirador.GetComponent<MudaBala>();
        menu = GameObject.Find("GameManager").GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        balaAtual = mudaBala.modoTiro.Value;
        QuerAtirar();
        tiroAutomatico = menu.AutoFire();
    }

    void QuerAtirar() {
        if(IsOwner && TimeManager.paused == false)
        {
            if(Input.GetMouseButton(0) || tiroAutomatico) {
                // Vector3 direc = transform.parent.gameObject.GetComponent<MovePlayer>().vetorMove;
                // if(IsHost) direc *= -1;
                AtiraServerRpc(balaAtual);
            }
        }
    }
}
