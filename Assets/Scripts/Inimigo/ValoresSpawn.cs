using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValoresSpawn : MonoBehaviour
{
    public int valorXp;
    public float maxHp, danoBala, velBala, velAtirarMax, velAtirarMin, velMovimento;

    void Start() 
    {
        DeterminaValores();
    }

    void DeterminaValores() {
        GameObject spawner = GameObject.Find("SpawnerInimigo");
        float dificuldade = spawner.GetComponent<IniciaOnda>().dificuldade;

        maxHp *= dificuldade;
        danoBala *= dificuldade;
        velBala *= 0.5f + dificuldade/2;
        velMovimento *= 0.5f + dificuldade/2;

        velAtirarMax *= (1.1f / ((Mathf.Pow(dificuldade, 2)) + 1)) + 0.45f; // Essa equação é mais ou menos arbitrária, mas ela gera um número
        velAtirarMin *= (1.1f / ((Mathf.Pow(dificuldade, 2)) + 1)) + 0.45f; // que diminui cada vez mais com o aumento da dificuldade, mas essa
    }                                                                       // diminuição vai ficando cada vez menor.
}
