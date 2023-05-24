using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MudaBala : FuncoesGerais
{
    public int numTiros;
    SpriteRenderer sprRenderer;
    GameObject arma;
    public Sprite[] spritesPlayer;
    private PlayerNetwork PlayerNet;
    public NetworkVariable<int> modoTiro = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    void Start() 
    {
        PlayerNet = GetComponent<PlayerNetwork>();
        sprRenderer = GetComponent<SpriteRenderer>();
        arma = transform.GetChild(0).gameObject;
        MudaSprite();
    }

    // Update is called once per frame
    void Update()
    {
        GetModo();
    }

    void MudaSprite() {
        sprRenderer.sprite = spritesPlayer[modoTiro.Value];
    }

    [ClientRpc] 
    void AtualizarSpriteClientRpc()
    {
        Debug.LogWarning("Variavel: " + modoTiro.Value);
        MudaSprite();
    }

    void GetModo() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0 && IsOwner)  {
            scroll = scroll/Mathf.Abs(scroll);

            int novoValor = (modoTiro.Value + (int) scroll) % numTiros;

            if(novoValor < 0) novoValor= 2;
            modoTiro.Value = novoValor;
            
            Debug.LogWarning("Variavel: " + modoTiro.Value);

            MudaSprite();
            AtualizarSpriteClientRpc();
        }
    }
}
