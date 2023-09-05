using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ColorCode
    {
        Fundo = 0,
        //Texto = 1,
        //Menu = 2,
        //Interativo = 3,
        Jogador = 1,
        Aliado = 2,
        Inimigo = 3
};

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private Camera[] currentColor_Backg;
    [SerializeField] private GameObject[] currentColor_Player;
    [SerializeField] private GameObject[] currentColor_Ally;
    [SerializeField] private GameObject[] currentColor_Enemy;

    [SerializeField] private Image preview;

    // private TextMeshProUGUI[] texts;

    private Slider Red;
    private Slider Green;
    private Slider Blue;

    void Start()
    {
        //texts = FindObjectsOfType<TextMeshProUGUI>(true);
        SetTexts(); 
    }

    public static Dictionary<ColorCode, Color> baseColor = new Dictionary<ColorCode, Color>()
    {
        { ColorCode.Fundo, new Color(1, 1, 1) },   
        //{ ColorCode.Texto, new Color(1, 1, 1) },   
        //{ ColorCode.Menu, new Color(45, 50, 65) },   
        //{ ColorCode.Interativo, new Color(1, 1, 1) },  
        { ColorCode.Jogador, new Color(1, 1, 1) },   
        { ColorCode.Aliado, new Color(1, 1, 1) },   
        { ColorCode.Inimigo, new Color(1, 1, 1) }    
    };

    public void Constructor()
    {
        Red = GameObject.Find("ColorSlider_R").GetComponent<Slider>();
        Green = GameObject.Find("ColorSlider_G").GetComponent<Slider>();
        Blue = GameObject.Find("ColorSlider_B").GetComponent<Slider>();
    }

    private ColorCode currentCategory = ColorCode.Fundo;

    public void OnCategoryButtonClick(TMP_Dropdown category)
    {
        currentCategory = (ColorCode) category.value;
        Red.value = baseColor[currentCategory].r;
        Green.value = baseColor[currentCategory].g;
        Blue.value = baseColor[currentCategory].b;
        PreviewColorValues();
    }

    public void OnColorSliderClick()
    {
        baseColor[currentCategory] = new Color(Red.value, Green.value, Blue.value);
        Debug.Log($"{baseColor[currentCategory].r} {baseColor[currentCategory].g} {baseColor[currentCategory].b}");
    }

    private void SetTexts()
    {
        //  foreach (TextMeshProUGUI txt in texts)
        //      txt.color = baseColor[ColorCode.Texto];
        for(int i = 0; i < currentColor_Backg.Length; i++)
            currentColor_Backg[i].backgroundColor = baseColor[ColorCode.Fundo];
    }

    public void PreviewColorValues()
    {
        preview.color =new Color(Red.value, Green.value, Blue.value);
    }

    public void OnApplyButtonClick()
    {
        SetTexts();

        for(int i = 0; i < currentColor_Player.Length; i++)
            currentColor_Player[i].GetComponent<Image>().color = baseColor[ColorCode.Jogador];

        for(int i = 0; i < currentColor_Ally.Length; i++)
            currentColor_Ally[i].GetComponent<Image>().color = baseColor[ColorCode.Aliado];
 
        for(int i = 0; i < currentColor_Enemy.Length; i++)
            currentColor_Enemy[i].GetComponent<Image>().color = baseColor[ColorCode.Inimigo];   
    }
}