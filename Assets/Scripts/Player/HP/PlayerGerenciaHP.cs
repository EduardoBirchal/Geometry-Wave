using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGerenciaHP : FuncoesGerais
{
    public float maxHp, hp, tempoIvulneravel;
    public Image barra; 
    SpriteRenderer sprRenderer;
    public bool tomaDano = true;
    Collider2D colisor;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        sprRenderer = GetComponent<SpriteRenderer>();
        colisor = GetComponent<Collider2D>();
    }

    void Update() {
        EsticaBarraHP();
    }

    public void TomaDano(float dano) {
        hp -= dano;
        StartCoroutine(Ivulneravel());
    }

    void EsticaBarraHP() {
        barra.fillAmount = Mathf.Clamp(hp/maxHp, 0, 1f); 
    }

    IEnumerator Ivulneravel() {
        // Torna objeto transparente (temp.a é a opacidade)
        Color temp = sprRenderer.color;
        temp.a = 0.25f;
        sprRenderer.color = temp; // Não dá pra mudar sprRenderer.color.a diretamente, então tem que fazer uma nova Color só pra mudar o .a dela e passar ela inteira de volta

        colisor.isTrigger = true; // Deixa de ter colisão com rigidbodies

        yield return new WaitForSeconds(tempoIvulneravel);

        // Torna objeto opaco
        temp.a = 1f;
        sprRenderer.color = temp;

        colisor.isTrigger = false; // Faz o objeto colidir normalmente
    }
}