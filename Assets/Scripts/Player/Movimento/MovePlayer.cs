using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MoveAutomatico
{
    [SerializeField] private InputActionReference movement;

    public float vel, velAtual;
    public bool move = true, miraAutomatico;
    public Vector3 vetorMove;
    private Vector2 movimento;
    private MenuManager menu;

    void Start() 
    {
        velAtual = vel;
        menu = GameObject.Find("GameManager").GetComponent<MenuManager>();
    }

    //Input Actions Functions

    private void OnEnable() 
    {
        movement.action.Enable();
        movement.action.performed += MovementPerform;
        movement.action.canceled += MovementPerform;
    }
    
    private void OnDisable()
    {
        movement.action.Disable();
        movement.action.performed -= MovementPerform;
        movement.action.canceled -= MovementPerform;
    }

    private void MovementPerform(InputAction.CallbackContext value)
    {
        movimento = movement.action.ReadValue<Vector2>();
    }

    private void movementCanceled(InputAction.CallbackContext value)
    {
        movimento = Vector2.zero;
    }

    void Update()
    {
        if(IsOwner && TimeManager.paused == false)
        {
            miraAutomatico = menu.AutoAim();
            
            if (move) {
                velAtual = vel;
            }
            else {
                velAtual = 0;
            }
            
            vetorMove = new Vector3(MoveHorizontal(), MoveVertical(), 0);
            
            ViraPlayer();
        }
    }

    float MoveVertical() {
        float dir = movimento.y;
        float eixoMove = dir * Time.deltaTime * velAtual;

        transform.Translate(new Vector2(0, eixoMove), Space.World);

        return eixoMove;
    }

    float MoveHorizontal() {
        float dir = movimento.x;
        float eixoMove = dir * Time.deltaTime * velAtual;
        
        transform.Translate(new Vector2(eixoMove, 0), Space.World);

        return eixoMove;
    }

    void ViraPlayer() {
        if (miraAutomatico) {
            GameObject inimigoMaisProximo = ProcuraObjMaisProximo("Inimigo");

            if (inimigoMaisProximo) ViraPraObjeto(inimigoMaisProximo.transform.position); 
        } 
        else {
            SegueMouse();
        }
    }

    void SegueMouse() {
        if(!IsOwner) return;
        ViraPraObjeto(Camera.main.ScreenToWorldPoint(Input.mousePosition)); 

        /*
        Vector3 posMouse = Input.mousePosition;
        Vector3 posPlayer = Camera.main.WorldToScreenPoint(transform.position);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, GetAngulo(posPlayer, posMouse) * Mathf.Rad2Deg * -1));  
        */
    }
}