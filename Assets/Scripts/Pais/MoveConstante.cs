using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConstante : FuncoesGerais
{
    public float velocidade;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, velocidade * Time.deltaTime));
    }
}
