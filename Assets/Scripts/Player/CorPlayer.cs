using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CorPlayer : NetworkBehaviour
{
    private SpriteRenderer sprRenderer;
    [SerializeField] private Color color;
    
    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        AtribuiCor();
    }

    private void AtribuiCor()
    {
        color = IsOwner ? ColorPicker.baseColor[ColorCode.Jogador] : ColorPicker.baseColor[ColorCode.Aliado];
        sprRenderer.material.SetColor("_Color", color); 
    }

    void Update()
    {
        AtribuiCor();
    }
}
