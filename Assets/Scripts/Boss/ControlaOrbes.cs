using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbe {
    public GameObject obj;
    public AtiraOrbe atr;
    public MoveInimigo mv;
    public MoveOrbitando orbt;
}

public class ControlaOrbes : FuncoesGerais
{
    [SerializeField] private const int numOrbes = 3;
    [SerializeField] private const int rajadasPorRotacao = 3;

    [SerializeField] private const float tempoEntreRotacoes = 1.5f;
    [SerializeField] private const float duracaoRaio = 6f;

    private Orbe[] orbes;

    // Start is called before the first frame update
    void Start()
    {
        orbes = new Orbe[numOrbes];
        GetOrbes();
        SetRajada(true);
        SetRaio(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (orbes[0].atr.rajadasAteAgora >= rajadasPorRotacao) {
            StartCoroutine(RotacionaModos());
        }
    }

    void GetOrbes () {
        for (int i = 0; i < numOrbes; ++i) {
            Transform novoOrbe = transform.GetChild(i);
            orbes[i] = new Orbe();

            orbes[i].obj = novoOrbe.gameObject;
            orbes[i].atr = novoOrbe.GetComponent<AtiraOrbe>();
            orbes[i].mv = novoOrbe.GetComponent<MoveInimigo>();
            orbes[i].orbt = novoOrbe.GetComponent<MoveOrbitando>();
        }
    }

    void SetRajada (bool status) {
        foreach (Orbe orbe in orbes) {
            orbe.atr.rajadaAtivada = status;

            orbe.mv.mudaAngulo = status;
            orbe.orbt.viraPraFora = !status;
        }
    }

    void SetRaio (bool status) {
        foreach (Orbe orbe in orbes) {
            orbe.atr.raioAtivado = status;

            orbe.mv.mudaAngulo = !status;
            orbe.orbt.viraPraFora = status;
        }
    }

    private IEnumerator RotacionaModos () {
        SetRajada (false);
        yield return new WaitForSeconds(tempoEntreRotacoes);
        SetRaio (true);
        yield return new WaitForSeconds(duracaoRaio);
        SetRaio(false);
        yield return new WaitForSeconds(tempoEntreRotacoes);
        SetRajada (true);
    }
}
