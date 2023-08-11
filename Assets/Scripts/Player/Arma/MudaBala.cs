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
        GetModo();
        UpdateSprite();
    }
    void UpdateSprite()
    {
        sprRenderer.sprite = spritesPlayer[modoTiro.Value];
    }

    void GetModo() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0 && IsOwner)  {
            scroll = scroll/Mathf.Abs(scroll);

            int novoValor = (modoTiro.Value + (int) scroll) % numTiros;

            if(novoValor < 0) novoValor = numTiros - 1;
            modoTiro.Value = novoValor;
        }
    }
}