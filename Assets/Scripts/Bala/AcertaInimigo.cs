using UnityEngine;

public class AcertaInimigo : FuncoesBala
{
    public int impacto, perfuracao;
    public float dano;

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject outro = other.gameObject;
        string tag = outro.tag;

        switch (tag)
        {
            case "Inimigo":
                outro.GetComponent<InimigoGerenciaHP>().TomaDano(dano, impacto, transform.eulerAngles.z);
                PerdePerfuracao();
            break;
            
            default:
            break;
        }
    }

    private void PerdePerfuracao() {
        perfuracao--; 

        if (perfuracao < 1 && IsHost) DestroiBalaServerRpc(); 
    }
}
