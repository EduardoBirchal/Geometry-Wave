using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapTela : FuncoesGerais
{
    public float buffer;
    float limBaixo, limCima, limEsquerda, limDireita, profundidadeCamera;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        profundidadeCamera = Camera.main.transform.position.z;

        limEsquerda = Camera.main.ScreenToWorldPoint ( new Vector3(0.0f, 0.0f, 0 - profundidadeCamera) ).x - buffer;
        limDireita = Camera.main.ScreenToWorldPoint ( new Vector3(Screen.width, 0.0f, 0 - profundidadeCamera) ).x + buffer;

        limBaixo = Camera.main.ScreenToWorldPoint ( new Vector3(0.0f, 0.0f, 0 - profundidadeCamera) ).y - buffer;
        limCima = Camera.main.ScreenToWorldPoint ( new Vector3(0.0f, Screen.height, 0 - profundidadeCamera) ).y + buffer;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        checaHorizontal(pos);
        checaVertical(pos);
    }

    void checaHorizontal (Vector3 pos) {
        if(pos.x < limEsquerda) { 
            transform.position = new Vector3 (limDireita, pos.y, pos.z); 
        }
        if(pos.x > limDireita) {
            transform.position = new Vector3 (limEsquerda, pos.y, pos.z); 
        }
    }

    void checaVertical (Vector3 pos) {
        if(pos.y < limBaixo) { 
            transform.position = new Vector3 (pos.x, limCima, pos.z); 
        }
        if(pos.y > limCima) {
            transform.position = new Vector3 (pos.x, limBaixo, pos.z); 
        }
    }
}
