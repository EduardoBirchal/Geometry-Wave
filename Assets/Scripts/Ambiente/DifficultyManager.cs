using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : FuncoesGerais
{
    private TipoBala[] tiposBala;
    [SerializeField] private float aumentoDificuldade;
    
    public float bonusDanoBala, bonusVelBala;
    public float dificuldade;

    // Start is called before the first frame update
    void Start()
    {
        dificuldade = PlayerPrefs.GetFloat("dificuldade");

        bonusDanoBala = dificuldade/4;
        bonusVelBala = dificuldade/10;

        tiposBala = GameObject.Find("Funcoes").GetComponent<TipoTiro>().inimigo;
        
        SetValoresBala();
    }

    void SetValoresBala() {
        foreach (TipoBala tipo in tiposBala) {
            tipo.danoBala *= dificuldade;
            tipo.velBala *= dificuldade;
        }
    }

    void AumentaValoresBala() {
        foreach (TipoBala tipo in tiposBala) {
            tipo.danoBala += bonusDanoBala;
            tipo.velBala += bonusVelBala;
        }
    }

    public void AumentaDificuldade() {
        dificuldade += aumentoDificuldade;
        AumentaValoresBala();
    }
}