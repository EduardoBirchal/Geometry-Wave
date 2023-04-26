using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoGerenciaHP : FuncoesGerais
{
    public float maxHp, hp;
    float velAnterior;
    Rigidbody2D rb;
    MorreInimigo morreInimigo;
    MoveInimigo mvInimigo;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = GetComponent<ValoresSpawn>().maxHp;
        hp = maxHp;
        rb = GetComponent<Rigidbody2D>();
        morreInimigo = GetComponent<MorreInimigo>();
        mvInimigo = GetComponent<MoveInimigo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f) morreInimigo.Morre();
    }

    public void TomaDano(float dano, float impacto, float angulo) {
        hp -= dano;
        //Knockback(angulo, impacto);
    }

    void Knockback(float angulo, float impacto) {
        mvInimigo.move = false;

        rb.AddForce(AnguloPraVetor(angulo + 90) * impacto);
        StartCoroutine(VoltaVelocidade(0.4f));
    }

    IEnumerator VoltaVelocidade(float duracao) {
        yield return new WaitForSeconds(duracao);

        mvInimigo.move = true;
    }
}