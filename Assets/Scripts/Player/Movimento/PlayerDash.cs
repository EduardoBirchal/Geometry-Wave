using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : FuncoesGerais
{
    public float vel, tempoCarrega, tempoParado;
    MovePlayer mvPlayer;
    Rigidbody2D rb;
    bool dashCarregado = true;

    // Start is called before the first frame update
    void Start()
    {
        mvPlayer = GetComponent<MovePlayer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1) && dashCarregado) {
            Dash();
        }
    }

    void Dash() {
        dashCarregado = false;
        StartCoroutine(Recarrega(tempoCarrega)); // Descarrega o dash e ativa o timer de recarregar

        if(mvPlayer.vetorMove.x != 0f || mvPlayer.vetorMove.y != 0f) { // Se o player está se movendo, dá um dash na direção de movimento dele.
            Vector3 direcaoMovimento = new Vector3(TransformaEm1(Input.GetAxis("Horizontal")), TransformaEm1(Input.GetAxis("Vertical")), 0);

            if (mvPlayer.vetorMove.x != 0f && mvPlayer.vetorMove.y != 0f) 
                Impulso(direcaoMovimento, (vel * Mathf.Sqrt(2)) / 2); 
            else                                    // Se o player tem dois vetores de movimento, ele vai se mover numa reta que é a hipotenusa de um triângulo 
                Impulso(direcaoMovimento, vel);     // onde os dois catetos são os dois vetores de movimento. Ao dividir a velocidade dessa forma, a 
        }                                           // hipotenusa resultante vai ter comprimento [vel].   
        // Se o player não está se movendo, dá um dash na direção onde ele está mirando.                                      
        else { 
            Vector3 direcaoMira = transform.rotation * Vector3.up;

            Impulso(direcaoMira, vel);
        }
    }

    // Dá o impulso do dash
    void Impulso(Vector3 direcao, float velocidade) {
        mvPlayer.move = false; // Desativa o movimento do player, pra não interferir no dash.

        rb.AddForce(direcao * velocidade);
        StartCoroutine(VoltaVelocidade(tempoParado)); // Ativa o timer pra reativar o movimento do player
    }

    // Espera e dá de volta a velocidade do player
    IEnumerator VoltaVelocidade(float duracao) {
        yield return new WaitForSeconds(duracao);

        mvPlayer.move = true;
    }

    // Espera um tempo e recarrega o dash
    IEnumerator Recarrega(float tempo) {
        yield return new WaitForSeconds(tempo);

        dashCarregado = true;
    }
}