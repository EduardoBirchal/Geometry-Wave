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
        GameObject game_Manager = GameObject.Find("GameManager");
        tipos = game_Manager.GetComponent<TipoTiro>().player;
        menu = game_Manager.GetComponent<MenuManager>();
        mudaBala = atirador.GetComponent<MudaBala>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        if(TimeManager.localPause == true) return; 
        balaAtual = mudaBala.modoTiro.Value;
        QuerAtirar();
        tiroAutomatico = menu.AutoFire();
    }

    void QuerAtirar()
    {
        if(Input.GetMouseButton(0) || tiroAutomatico) {
            AtiraServerRpc(balaAtual);
        }
    }
}
