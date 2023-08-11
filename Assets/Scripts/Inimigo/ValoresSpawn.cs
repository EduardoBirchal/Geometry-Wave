using UnityEngine;
using Unity.Netcode;

public class ValoresSpawn : NetworkBehaviour 
{
    public int valorXp;
    public float maxHp, multiplicadorDanoBala, multiplicadorVelBala, velAtirarMax, velAtirarMin, velMovimento;

    void Start() 
    {
        if(!IsHost) return;
        DeterminaValoresServerRpc();
    }

    [ServerRpc]
    void DeterminaValoresServerRpc()
    {
        float dificuldade = GameObject.Find("SpawnerInimigo").GetComponent<Flock>().dificuldade;

        maxHp *= dificuldade;
        multiplicadorDanoBala = dificuldade;
        multiplicadorVelBala = 0.5f + dificuldade/2;
        velMovimento *= 0.5f + dificuldade/2;

        velAtirarMax *= (1.1f / (Mathf.Pow(dificuldade, 2) + 1)) + 0.45f; // Essa equação é mais ou menos arbitrária, mas ela gera um número
        velAtirarMin *= (1.1f / (Mathf.Pow(dificuldade, 2) + 1)) + 0.45f; // que diminui cada vez mais com o aumento da dificuldade, mas essa
    }                                                                       // diminuição vai ficando cada vez menor.
}
