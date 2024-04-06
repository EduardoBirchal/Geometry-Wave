using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public enum ColorCode
{
    Fundo = 0,
    Jogador = 1,
    Aliado = 2,
    Inimigo = 3
};

public class ColorPicker : NetworkBehaviour
{
    private Slider Red;
    private Slider Green;
    private Slider Blue;
    private bool alterColor;
    
    public static Dictionary<ColorCode, Color> baseColor = new Dictionary<ColorCode, Color>()
    {
        { ColorCode.Fundo, new Color(0.2f, 0.2f, 0.2f) },   
        { ColorCode.Jogador, new Color(0.2f, 0.6f, 0.2f) },   
        { ColorCode.Aliado, new Color(1.0f, 1.0f, 1.0f) },   
        { ColorCode.Inimigo, new Color(0.75f, 0.25f, 0.25f) }    
    };
    public static Dictionary<ColorCode, Color> bufferColor = new Dictionary<ColorCode, Color>()
    {
        { ColorCode.Fundo, baseColor[ColorCode.Fundo] },
        { ColorCode.Jogador, baseColor[ColorCode.Jogador] }, 
        { ColorCode.Aliado, baseColor[ColorCode.Aliado] },  
        { ColorCode.Inimigo, baseColor[ColorCode.Inimigo] },
    };
    [SerializeField] private Image preview;

    public void Constructor()
    {
        Red = GameObject.Find("ColorSlider_R").GetComponent<Slider>();
        Green = GameObject.Find("ColorSlider_G").GetComponent<Slider>();
        Blue = GameObject.Find("ColorSlider_B").GetComponent<Slider>();
    }

    private ColorCode currentCategory = ColorCode.Fundo;

    public void OnCategoryButtonClick(TMP_Dropdown category)
    {
        alterColor = false;
        currentCategory = (ColorCode) category.value;
        Red.value = bufferColor[currentCategory].r;
        Green.value = bufferColor[currentCategory].g;
        Blue.value = bufferColor[currentCategory].b;
        PreviewColorValues();
        alterColor = true;
    }

    public void OnColorSliderClick()
    {
        if(alterColor == false) return;
        bufferColor[currentCategory] = new Color(Red.value, Green.value, Blue.value);
        Debug.Log($"Categoria[{currentCategory}]: {bufferColor[currentCategory].r} {bufferColor[currentCategory].g} {bufferColor[currentCategory].b}");
    }

    public void PreviewColorValues()
    {
        preview.color = bufferColor[currentCategory];
    }

    public void OnApplyButtonClick()
    {
        baseColor[ColorCode.Fundo] = bufferColor[ColorCode.Fundo];
        baseColor[ColorCode.Jogador] = bufferColor[ColorCode.Jogador];
        baseColor[ColorCode.Aliado] = bufferColor[ColorCode.Aliado];
        baseColor[ColorCode.Inimigo] = bufferColor[ColorCode.Inimigo];

        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < player.Length; i++)
            player[i].GetComponent<CorPlayer>().AlterarCor();
 
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
        for(int i = 0; i < inimigos.Length; i++)
        {
            Debug.Log("Inimigo " + i + " alterado");
            inimigos[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", baseColor[ColorCode.Inimigo]);
        }
    }
}