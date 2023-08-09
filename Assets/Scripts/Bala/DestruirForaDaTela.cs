using UnityEngine;
using Unity.Netcode;

public class DestruirForaDaTela : FuncoesBala
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "LimiteTela") {
            DestroiBala();
        }
    }
}