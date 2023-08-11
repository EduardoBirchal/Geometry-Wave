using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AtiraPlayer : Atirador
{
    public int balaAtual = 0;
    public bool tiroAutomatico;
    private MudaBala mudaBala;

    void Start() 
    {
        tipos = GameObject.Find("Funcoes").GetComponent<TipoTiro>().player;
        mudaBala = atirador.GetComponent<MudaBala>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        if(TimeManager.localPause == true) return; 
        balaAtual = mudaBala.modoTiro.Value;
        QuerAtirar();
    }

    void QuerAtirar()
    {
        if(Input.GetMouseButton(0) || tiroAutomatico) {
            // Vector3 direc = transform.parent.gameObject.GetComponent<MovePlayer>().vetorMove;
            // if(IsHost) direc *= -1;
            AtiraServerRpc(balaAtual);
        }
    }
}
