using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class PlayerGerenciaHP : NetworkBehaviour
{
    public NetworkVariable<float> maxHp = new(
        value: 0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );
    public NetworkVariable<float> hp = new(
        value: 0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );
    public NetworkVariable<float> tempoInvulneravel = new(
        value: 0,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
    );

    public Image barra;
    SpriteRenderer sprRenderer;
    public bool tomaDano = true;
    public static bool isDead;
    Collider2D colisor;
    [SerializeField] private GameObject death_Screen;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        maxHp.Value = 10;
        hp.Value = maxHp.Value;
        tempoInvulneravel.Value = 2.0f;
    }
    void Start()
    {
        barra = GameObject.Find("VidaBarra").GetComponent<Image>();
        sprRenderer = GetComponent<SpriteRenderer>();
        colisor = GetComponent<Collider2D>();
    }

    void Update()
    {
        if(!IsOwner) return;
        EsticaBarraHP();
    }

    public async void TomaDano(float dano)
    {
        if(!IsOwner) return;
        
        hp.Value -= dano;
        isDead = hp.Value <= 0;
        if(isDead)
        {
            RemovePlayerServerRpc();
            hp.Value = 0;
            EsticaBarraHP();
            await GameObject.Find("GameManager").GetComponent<DeathManager>().KillPlayer();
        }
        else StartCoroutine(Invulneravel());
    }

    [ServerRpc]
    public void RemovePlayerServerRpc()
    { 
        NetworkObject.Despawn(true);
        GameObject.Find("Network").GetComponent<NetStatus>().PlayersAlive.Value--;
    }

    void EsticaBarraHP() {
        barra.fillAmount = Mathf.Clamp(hp.Value/maxHp.Value, 0, 1f); 
    }

    IEnumerator Invulneravel() {
        // Torna objeto transparente (temp.a é a opacidade)
        Color temp = sprRenderer.color;
        temp.a = 0.25f;
        sprRenderer.color = temp; // Não dá pra mudar sprRenderer.color.a diretamente, então tem que fazer uma nova Color só pra mudar o .a dela e passar ela inteira de volta

        colisor.isTrigger = true; // Deixa de ter colisão com rigidbodies

        yield return new WaitForSeconds(tempoInvulneravel.Value);

        // Torna objeto opaco
        temp.a = 1f;
        sprRenderer.color = temp;

        colisor.isTrigger = false; // Faz o objeto colidir normalmente
    }
}