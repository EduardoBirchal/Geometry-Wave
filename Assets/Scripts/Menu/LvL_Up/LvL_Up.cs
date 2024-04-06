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
    private MenuLvL_Up menu;
    private int qntUpgrades = 0, velAtaque = 0, imprecisao = 0, dashCooldown = 0, vida = 0, dano = 0 ;
    private int dashCooldownMax = 10, velAtaqueMax = 9, imprecisaoMax = 9, vidaMax = 10000, danoMax = 10000;

    void Start()
    {
        tipos = GameObject.Find("GameManager").GetComponent<TipoTiro>().player;
        menu = GameObject.Find("GameManager").GetComponent<MenuLvL_Up>();
        GetPlayerComponents();
        StringConstructor();
    }

    private void StringConstructor()
    {
        menu.MudaTextoPontosGastos(0, vida, vidaMax);
        menu.MudaTextoPontosGastos(1, dano, danoMax);
        menu.MudaTextoPontosGastos(2, dashCooldown, dashCooldownMax);
        menu.MudaTextoPontosGastos(3, velAtaque,velAtaqueMax);
        menu.MudaTextoPontosGastos(4, imprecisao, imprecisaoMax);
    }

    void Update()
    {
        if(playerXp == null){
            GetPlayerComponents();
        }
        if(playerXp != null){
            menu.MudaTextoPontosDisponiveis((playerXp.level - qntUpgrades));

            if(MaxUpgrades()) menu.Notification("!");
            else menu.Notification("");
        }
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
        if(player == null){
            player = GameObject.Find("Player");
        }
        if(player != null){
            playerHp = player.GetComponent<PlayerGerenciaHP>();
            playerXp = player.GetComponent<PlayerGerenciaXP>();
            playerDash = player.GetComponent<PlayerDash>();
        }
    }

    public void AumentaHP()
    {
        if(playerHp != null && MaxUpgrades()){
            playerHp.hp.Value += 2;
            playerHp.maxHp.Value += 2;
            qntUpgrades++;
            vida++;
            menu.MudaTextoPontosGastos(0, vida, vidaMax);
        }
    }

    public void AumentaDano()
    {
        if(MaxUpgrades()){
            balaComum.dano++;
            balaTeleguiada.dano++;
            qntUpgrades++;
            dano++;
            menu.MudaTextoPontosGastos(1, dano, danoMax);
        }
    }

    public void DashCooldown()
    {
        if(playerDash != null && MaxUpgrades() && dashCooldown <= dashCooldownMax){
            playerDash.tempoCarrega -= 0.05f;
            qntUpgrades++;
            dashCooldown++;
            menu.MudaTextoPontosGastos(2, dashCooldown, dashCooldownMax);
        }
    }

    public void AumentaVelAtaque()
    {
        if(MaxUpgrades() && velAtaque <= velAtaqueMax){
            Debug.Log(tipos);
            tipos[0].cooldownTiro_Min -= 0.01f;
            tipos[0].cooldownTiro_Max -= 0.01f;
            tipos[1].cooldownTiro_Min -= 0.05f;
            tipos[1].cooldownTiro_Max -= 0.05f;
            tipos[2].cooldownTiro_Min -= 0.05f;
            tipos[2].cooldownTiro_Max -= 0.05f;
            tipos[3].cooldownTiro_Min -= 0.02f;
            tipos[3].cooldownTiro_Max -= 0.02f;

            qntUpgrades++;
            velAtaque++;
            menu.MudaTextoPontosGastos(3, velAtaque,velAtaqueMax);
        }
    }

    public void AumentaPrecisao()
    {
         if(MaxUpgrades() && imprecisao <= imprecisaoMax){
            tipos[0].imprecisaoBala -= 1.2f;
            tipos[1].imprecisaoBala -= 0.5f;
            tipos[2].arcoTiro -= 2f;

            qntUpgrades++;
            imprecisao++;
            menu.MudaTextoPontosGastos(4, imprecisao, imprecisaoMax);
        }
    }
}
