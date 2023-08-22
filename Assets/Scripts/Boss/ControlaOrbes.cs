using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbe {
    public GameObject obj;
    public AtiraOrbe atr;
    public MoveInimigo mv;
}

public class ControlaOrbes : MonoBehaviour
{
    [SerializeField] private const int numOrbes = 2;
    private Orbe[] orbes;

    // Start is called before the first frame update
    void Start()
    {
        GetOrbes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetOrbes () {
        for (int i = 0; i < numOrbes; ++i) {
            Transform novoOrbe = transform.GetChild(i);
            orbes[i] = new Orbe();

            orbes[i].obj = novoOrbe.gameObject;
            orbes[i].atr = novoOrbe.GetComponent<AtiraOrbe>();
            orbes[i].mv = novoOrbe.GetComponent<MoveInimigo>();
        }
    }

    void SetRajada (bool status) {
        foreach (Orbe orbe in orbes) {
            orbe.atr.rajadaAtivada = status;
            orbe.mv.mudaAngulo = status;
        }
    }
}
