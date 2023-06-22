using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using System.Collections.Generic;

public struct TipoBala
{
    public int numBalas, perfuracaoBala;
    public float velBala, cooldownTiro_Min, cooldownTiro_Max, imprecisaoBala, arcoTiro, danoBala;
    public string nomeTipo;
    public GameObject prefab;

    public TipoBala (int num, float dano, float velB, float cooldownMin, float cooldownMax, float imprec, float arco, int perfuracao, string nome, GameObject prefab) { // Bob o construtor
        numBalas = num;
        danoBala = dano;
        velBala = velB;
        cooldownTiro_Max = cooldownMax; // Em segundos
        cooldownTiro_Min = cooldownMin; // Em segundos
        imprecisaoBala = imprec;        // Em graus
        arcoTiro = arco;                // Em graus
        perfuracaoBala = perfuracao;    // Em inimigos perfurados
        nomeTipo = nome;
        this.prefab = prefab;
    }
}

public class TipoTiro : MonoBehaviour
{
    [SerializeField] private GameObject prefab_balaPlayer;
    [SerializeField] private GameObject prefab_balaInimigo;
    [SerializeField] private GameObject prefab_balaGuiada;
    public TipoBala[] balasPlayer;
    public TipoBala[] balasInimigo;
    
    public void Start()
    {
        balasPlayer = new TipoBala[]{
        // METRALHADORA
        new TipoBala(
            1, // Número de balas
            1f, // Dano
            13f, // Velocidade da bala
            0.1f, // Cooldown Minimo (em segundos)
            0.1f, // Cooldown Maximo (em segundos)
            20f, // Imprecisão (em graus)
            40f, // Arco de tiro (em graus)
            1, // Perfuração
            "Metralhadora", // Nome
            prefab_balaPlayer // Objeto da bala
        ),
        // KILL AURA
        new TipoBala(
            90,
            100f, 
            20f, 
            0f, 
            0f, 
            0f, 
            360f,
            1,
            "KillAura",
            prefab_balaPlayer 
        ),
        // SHOTGUN
        new TipoBala(
            5, 
            5f, 
            9f, 
            0.75f, 
            0.75f, 
            0f, 
            40f,
            1,
            "Espingarda",
            prefab_balaPlayer
        ),
        // GUIDED
        new TipoBala(
            1, 
            2f, 
            6f, 
            0.3f, 
            0.3f, 
            90f, 
            90f,
            1,
            "Mísseis", 
            prefab_balaGuiada
        )};

        balasInimigo = new TipoBala[]{
            // ENEMY BASE SHOOT
            new TipoBala(
                1,
                2,
                6f,
                0.75f,
                2f,
                0,
                40,
                1,
                "Matador",
                prefab_balaInimigo
            )
        };
    }
}

