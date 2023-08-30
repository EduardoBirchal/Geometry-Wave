using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class MudaBala : NetworkBehaviour
{
    [SerializeField] private InputActionReference mudaKeyboard;
    
    public int numTiros;
    SpriteRenderer sprRenderer;
    GameObject arma;
    public Sprite[] spritesPlayer;
    public NetworkVariable<int> modoTiro = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    void Start() 
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        arma = transform.GetChild(0).gameObject;
    }
    
    //Input System functions

    private void OnEnable() {
        mudaKeyboard.action.Enable();
        mudaKeyboard.action.performed += GetValorTeclado;
    }

    private void OnDisable() {
        mudaKeyboard.action.performed -= GetValorTeclado;
        mudaKeyboard.action.Disable();
    }

    private void GetValorTeclado(InputAction.CallbackContext obj) {
        MudaArma(1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
        if(!IsOwner) return;
        if(TimeManager.localPause == true) return;
        GetValorScroll();
    }
    void UpdateSprite()
    {
        sprRenderer.sprite = spritesPlayer[modoTiro.Value];
    }

    void GetValorScroll() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0 && IsOwner)
        {
            scroll = scroll/Mathf.Abs(scroll);
            MudaArma((int) scroll);
        }
    }


    void MudaArma(int valorSoma) {
        int novoValor = (modoTiro.Value + valorSoma) % numTiros;

        if(novoValor < 0) novoValor = numTiros - 1;
        modoTiro.Value = novoValor;
    }
}