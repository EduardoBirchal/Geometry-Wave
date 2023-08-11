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
    public NetworkVariable<int> modoTiro = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    void Start() 
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        arma = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        if(TimeManager.localPause == true) return;
        UpdateSprite();
        GetValorScroll();
        GetValorTeclado();
    }
    void UpdateSprite()
    {
        sprRenderer.sprite = spritesPlayer[modoTiro.Value];
    }

    void GetValorScroll() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0 && IsOwner)
        {
            scroll = scroll/Mathf.Abs(scroll);
            MudaArma((int) scroll);
        }
    }

    void GetValorTeclado() {
        if (Input.GetKeyDown("q") || Input.GetKeyDown("left ctrl"))
            MudaArma(1);
    }

    void MudaArma(int valorSoma) {
        int novoValor = (modoTiro.Value + valorSoma) % numTiros;

        if(novoValor < 0) novoValor = numTiros - 1;
        modoTiro.Value = novoValor;
    }
}