using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum ColorCode
    {
        Fundo = 0,
        Texto = 1,
        Menu = 2,
        Interativo = 3,
        Jogador = 4,
        Aliado = 5,
        Inimigo = 6
};

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private GameObject[] currentColor_Backg;
    //[SerializeField] private GameObject[] currentColor_Button;
    //[SerializeField] private GameObject[] currentColor_Menu;
    //[SerializeField] private GameObject[] currentColor_Text;
    [SerializeField] private GameObject[] currentColor_Player;
    [SerializeField] private GameObject[] currentColor_Ally;
    [SerializeField] private GameObject[] currentColor_Enemy;

    [SerializeField] private GameObject[] Previews;

    private TextMeshProUGUI[] texts;
    private Image[] menus;
    private GameObject[] interativos;

    private Slider Red;
    private Slider Green;
    private Slider Blue;

    void Start()
    {
        texts = FindObjectsOfType<TextMeshProUGUI>(true);
        menus = FindObjectsOfType<Image>(true);
        interativos = GameObject.FindGameObjectsWithTag("interativos"); 
    }

    static Dictionary<ColorCode, Color> baseColor = new Dictionary<ColorCode, Color>()
    {
        { ColorCode.Fundo, new Color(1, 1, 1) },   
        { ColorCode.Texto, new Color(1, 1, 1) },   
        { ColorCode.Menu, new Color(1, 1, 1) },   
        { ColorCode.Interativo, new Color(1, 1, 1) },  
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
    }

    public void OnColorSliderClick()
    {
        baseColor[currentCategory] = new Color(Red.value, Green.value, Blue.value);
        Debug.Log($"{baseColor[currentCategory].r} {baseColor[currentCategory].g} {baseColor[currentCategory].b}");
    }


    public void OnApplyButtonClick()
    {
        //texts = GameObject.FindWithTag("text");

        foreach (TextMeshProUGUI txt in texts)
            txt.color = baseColor[ColorCode.Texto];
        foreach(Image menu in menus)
            menu.color = baseColor[ColorCode.Menu];
        foreach(GameObject interativo in interativos)
            interativo.GetComponent<Image>().color = baseColor[ColorCode.Interativo];

        // for(int i = 0; i < currentColor_Text.Length; i++)
        //     currentColor_Text[i].GetComponent<TextMeshProUGUI>().color = baseColor[ColorCode.Texto];
        // for(int i = 0; i < currentColor_Menu.Length; i++)
        //     currentColor_Menu[i].GetComponent<Image>().color = baseColor[ColorCode.Menu];
        // for(int i = 0; i < currentColor_Button.Length; i++)
        //     currentColor_Button[i].GetComponent<Image>().color = baseColor[ColorCode.Interativo];

        // for(int i = 0; i < currentColor_Backg.Length; i++)
        //     currentColor_Backg[i].GetComponent<Image>().color = baseColor[ColorCode.Fundo];

        for(int i = 0; i < currentColor_Player.Length; i++)
            currentColor_Player[i].GetComponent<Image>().color = baseColor[ColorCode.Jogador];

        for(int i = 0; i < currentColor_Ally.Length; i++)
            currentColor_Ally[i].GetComponent<Image>().color = baseColor[ColorCode.Aliado];
 
        for(int i = 0; i < currentColor_Enemy.Length; i++)
            currentColor_Enemy[i].GetComponent<Image>().color = baseColor[ColorCode.Inimigo];   
    }
}
