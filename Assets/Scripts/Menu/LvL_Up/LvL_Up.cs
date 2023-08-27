using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LvL_Up : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private AcertaInimigo balaComum, balaTeleguiada;
    
    private GameObject player;
    private PlayerDash playerDash;
    private PlayerGerenciaHP playerHp;
    private PlayerGerenciaXP playerXp;
    private TipoBala[] tipos;
    private int qntUpgrades = 0, limiteVelAtaque = 0, limiteImprecisao = 0, limiteDashCooldown = 0;

    void Start()
    {
        tipos = GameObject.Find("Funcoes").GetComponent<TipoTiro>().player;
        GetPlayerComponents();
    }

    void Update()
    {
        if(playerXp == null){
            GetPlayerComponents();
        }
        points.text = "Pontos de level up disponíveis: " +  (playerXp.level - qntUpgrades);
    }

    public bool MaxUpgrades()
    {
        if(qntUpgrades < playerXp.level)
        {
            return true;
        }
        else return false;

    }

    public void GetPlayerComponents()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }
        if(player != null)
        {
            playerHp = player.GetComponent<PlayerGerenciaHP>();
            playerXp = player.GetComponent<PlayerGerenciaXP>();
            playerDash = player.GetComponent<PlayerDash>();
        }
    }

    public void AumentaHP()
    {
        if(playerHp != null && MaxUpgrades()){
            playerHp.hp +=2;
            playerHp.maxHp += 2;
            qntUpgrades++;
        }
    }

    public void DashCooldown()
    {
        if(playerDash != null && MaxUpgrades() && limiteDashCooldown <= 10){
            playerDash.tempoCarrega -= 0.05f;
            qntUpgrades++;
            limiteDashCooldown++;
        }
    }

    public void AumentaDano()
    {
        if(MaxUpgrades()){
            balaComum.dano++;
            balaTeleguiada.dano++;
            qntUpgrades++;
        }
    }

    public void AumentaVelAtaque()
    {
        if(MaxUpgrades() && limiteVelAtaque < 10){
            tipos[0].cooldownTiro_Min -= 0.01f;
            tipos[0].cooldownTiro_Max -= 0.01f;
            tipos[1].cooldownTiro_Min -= 0.05f;
            tipos[1].cooldownTiro_Max -= 0.05f;
            tipos[2].cooldownTiro_Min -= 0.05f;
            tipos[2].cooldownTiro_Max -= 0.05f;
            tipos[3].cooldownTiro_Min -= 0.02f;
            tipos[3].cooldownTiro_Max -= 0.02f;
            qntUpgrades++;
            limiteVelAtaque++;
        }
    }

    public void AumentaPrecisao()
    {
         if(MaxUpgrades() && limiteImprecisao < 10){
            tipos[0].imprecisaoBala -= 1.2f;
            tipos[1].imprecisaoBala -= 0.5f;
            tipos[2].arcoTiro -= 2f;
            qntUpgrades++;
            limiteImprecisao++;
        }
    }
}