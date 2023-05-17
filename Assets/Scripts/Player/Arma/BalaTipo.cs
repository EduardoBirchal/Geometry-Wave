using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using System.Collections.Generic;

public struct TipoBala
{
    public int numBalas;
    public float velBala, cooldownTiro, imprecisaoBala, arcoTiro, danoBala;
    public FixedString64Bytes nomeTipo;
    public FixedString64Bytes prefab;

    public TipoBala (int num, float dano, float velB, float cooldown, float imprec, float arco, string nome, string prefab) { // Bob o construtor
        numBalas = num;
        danoBala = dano;
        velBala = velB;
        cooldownTiro = cooldown;    // Em segundos
        imprecisaoBala = imprec;    // Em graus
        arcoTiro = arco;            // Em graus
        nomeTipo = nome;
        this.prefab = new FixedString64Bytes(prefab);
    }
}

public class TipoTiro
{
    // METRALHADORA
    public static TipoBala[] tipos = {
    new TipoBala(
        1, // Número de balas
        1f, // Dano
        13f, // Velocidade da bala
        0.1f, // Cooldown (em segundos)
        20f, // Imprecisão (em graus)
        40f, // Arco de tiro (em graus)
        "Metralhadora", // Nome
        "BalaPlayer" // Objeto da bala
    ),
    // KILL AURA
    new TipoBala(
        90,
        100f, 
        20f, 
        0f, 
        0f, 
        360f,
        "KillAura",
        "BalaPlayer" 
    ),
        // SHOTGUN
    new TipoBala(
        5, 
        5f, 
        9f, 
        0.75f, 
        0f, 
        40f,
        "Espingarda",
        "BalaPlayer"
    ),
    new TipoBala(
        1,
        2,
        0.5f,
        0,
        0,
        40,
        "Matador",
        "BalaInimigo"
    )};
}

