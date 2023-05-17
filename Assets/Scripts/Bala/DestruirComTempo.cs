using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirComTempo : MonoBehaviour
{
    public float tempoVida;
    private float vidaAtual = 0;

    // Update is called once per frame
    void Update()
    {
        vidaAtual += Time.deltaTime;

        if (vidaAtual >= tempoVida) Destroy(gameObject);
    }
}
