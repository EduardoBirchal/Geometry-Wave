using UnityEngine;

public class AcertaPlayer : AcertaAlvo
{
    private void Start() {
        tagAlvo = "Player";
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject outro = other.gameObject;
        string tag = outro.tag;

        if (tag == tagAlvo) {
            HandleAlvo(outro);
        }
    }

    private void HandleAlvo(GameObject alvo) {
        alvo.GetComponent<PlayerGerenciaHP>().TomaDano(dano);
        DestroiBala();
    }
}
