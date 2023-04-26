using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirForaDaTela : MonoBehaviour
{
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
