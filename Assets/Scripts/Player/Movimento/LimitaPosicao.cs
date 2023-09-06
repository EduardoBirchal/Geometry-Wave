using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitaPosicao : MonoBehaviour
{
    [SerializeField] private float limiteVertical;
    [SerializeField] private float limiteHorizontal;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (
            Mathf.Clamp(transform.position.x, limiteHorizontal * -1, limiteHorizontal),
            Mathf.Clamp(transform.position.y, limiteVertical * -1, limiteVertical),
            transform.position.z
        );
    }

}
