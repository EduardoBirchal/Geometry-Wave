using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapTela : FuncoesGerais
{
    public float buffer;
    float larguraFromOrigin, alturaFromOrigin, left, right;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        left = Camera.main.ScreenToWorldPoint( new Vector3(0.0f, 0.0f, 0 - Camera.main.transform.position.z) ).x - buffer;
        right = Camera.main.ScreenToWorldPoint( new Vector3(Screen.width, 0.0f, 0 - Camera.main.transform.position.z) ).x + buffer;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;

        if(pos.x < left) { 
            transform.position = new Vector3 (right, pos.y, pos.z); 
        }


    }
}
