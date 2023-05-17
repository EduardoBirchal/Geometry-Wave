using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MoveAutomatico
{
    public float vel, velAtual;
    public bool move = true;
    public Vector3 vetorMove;

    void Start() 
    {
        velAtual = vel;
    }

    void Update()
    {
        if (move) {
            velAtual = vel;
        }
        else {
            velAtual = 0;
        }
        
        vetorMove = new Vector3(MoveHorizontal(), MoveVertical(), 0);
        
        SegueMouse();
    }

    float MoveVertical() {
        float dir = Input.GetAxis("Vertical");
        float eixoMove = dir * Time.deltaTime * velAtual;

        transform.Translate(new Vector2(0, eixoMove), Space.World);

        return eixoMove;
    }

    float MoveHorizontal() {
        float dir = Input.GetAxis("Horizontal");
        float eixoMove = dir * Time.deltaTime * velAtual;
        
        transform.Translate(new Vector2(eixoMove, 0), Space.World);

        return eixoMove;
    }

    void SegueMouse() {
        ViraPraObjeto(Camera.main.ScreenToWorldPoint(Input.mousePosition)); 

        /*
        Vector3 posMouse = Input.mousePosition;
        Vector3 posPlayer = Camera.main.WorldToScreenPoint(transform.position);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngulo(posPlayer, posMouse) * Mathf.Rad2Deg * -1));  
        */
    }
}