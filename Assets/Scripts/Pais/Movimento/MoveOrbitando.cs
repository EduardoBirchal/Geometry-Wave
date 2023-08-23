using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrbitando : MoveAutomatico
{
    public bool viraPraFora = false;

    public GameObject planeta;
    private float anguloAtual;
    [SerializeField] private float anguloInicial;
    [SerializeField] private float velocidadeAngular = 5f;
    [SerializeField] private Vector3 raioOrbita;

    private void Start() 
    {
        planeta = transform.parent.gameObject;
        anguloAtual = anguloInicial;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = planeta.transform.position + RotacionaEmVoltaDoEixo(velocidadeAngular) * raioOrbita;
        // Multiplicar um vetor (no caso raioOrbita) por um quaternion (no caso RotacionaEmVoltaDoEixo) é igual
        // a rotacionar ele RotacionaEmVoltaDoEixo "graus".

        if (viraPraFora)
            ViraPraObjeto(planeta.transform.position, true);
    }

    // Retorna uma rotação de anguloAtual graus em volta do eixo forward do planeta
    Quaternion RotacionaEmVoltaDoEixo(float velocidadeAngular) {
        return Quaternion.AngleAxis (
            anguloAtual += velocidadeAngular * Time.deltaTime,  // Aumenta anguloAtual em velocidadeAngular
            planeta.transform.forward                           // Rotaciona em volta de planeta.Transform.forward
        );
    }
}