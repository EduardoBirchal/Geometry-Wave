using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class PlayerGerenciaHP : NetworkBehaviour
{
    public float maxHp, hp, tempoIvulneravel;
    public Image barra;
    SpriteRenderer sprRenderer;
    public bool tomaDano = true;
    Collider2D colisor;
    [SerializeField] private GameObject death_Screen;


    // Start is called before the first frame update
    void Start()
    {
        barra = GameObject.Find("VidaBarra").GetComponent<Image>();
        hp = maxHp;
        sprRenderer = GetComponent<SpriteRenderer>();
        colisor = GetComponent<Collider2D>();
    }

    void Update() {
        EsticaBarraHP();
    }

    public async void TomaDano(float dano) {
        hp -= dano;
        if(hp <= 0 && IsOwner)
        {
            RemovePlayerServerRpc();
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
        barra.fillAmount = Mathf.Clamp(hp/maxHp, 0, 1f); 
    }

    IEnumerator Invulneravel() {
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