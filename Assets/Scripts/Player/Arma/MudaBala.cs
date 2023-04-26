using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudaBala : FuncoesGerais
{
    public int modoTiro = 0, numTiros;
    SpriteRenderer sprRenderer;
    GameObject arma;
    public Sprite[] spritesPlayer;  

    void Start() 
    {
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
        sprRenderer.sprite = spritesPlayer[modoTiro];
    }

    void AtualizaArma() {

    }

    void GetModo() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0) {
            scroll = scroll/Mathf.Abs(scroll);

            modoTiro += (int) scroll;
            modoTiro = modoTiro % numTiros;

            if(modoTiro < 0) modoTiro = 2;

            MudaSprite();
        }
    }
}
