using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpurraPlayer : MonoBehaviour
{
    [SerializeField] private float forcaEmpurrar;
    [SerializeField] private float dano;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            Vector3 direcaoEmpurrar = other.transform.position - transform.position;
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            rb.AddForce(direcaoEmpurrar.normalized * forcaEmpurrar, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerGerenciaHP>().TomaDano(dano);
        }
    }
}