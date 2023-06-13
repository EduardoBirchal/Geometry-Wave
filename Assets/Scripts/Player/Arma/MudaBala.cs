using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MudaBala : NetworkBehaviour
{
    public int numTiros;
    SpriteRenderer sprRenderer;
    GameObject arma;
    public Sprite[] spritesPlayer;
    private PlayerNetwork PlayerNet;
    public NetworkVariable<int> modoTiro = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    void Start() 
    {
        PlayerNet = GetComponent<PlayerNetwork>();
        sprRenderer = GetComponent<SpriteRenderer>();
        arma = transform.GetChild(0).gameObject;
        modoTiro.Value = 0;
        MudaSprite(modoTiro.Value);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerNet.CheckForClient() == false) return;
        GetModo();
    }

    void MudaSprite(int novoValor) {
        sprRenderer.sprite = spritesPlayer[novoValor];
    }

    [ClientRpc]
    void ReceberNovoSpriteClientRpc(int novoValor)
    {
        Debug.Log("Sprite Recebido");
        sprRenderer.sprite = spritesPlayer[novoValor];
    }

    [ServerRpc] 
    void EnviarNovoSpriteServerRpc(int novoValor)
    {
        Debug.Log("Sprite Enviado");
        modoTiro.Value = novoValor;
        ReceberNovoSpriteClientRpc(novoValor);
    }

    void GetModo() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0 && IsOwner)  {
            scroll = scroll/Mathf.Abs(scroll);

            int novoValor = (modoTiro.Value + (int) scroll) % numTiros;

            if(novoValor < 0) novoValor = numTiros - 1;
            EnviarNovoSpriteServerRpc(novoValor);
        }
    }
}