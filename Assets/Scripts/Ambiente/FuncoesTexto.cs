using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuncoesTexto : MonoBehaviour
{
    TextMeshProUGUI texto;
    float alphaMax = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
        texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 0);
    }

    public void FadeIn(float tempo) {
        StartCoroutine(FadeInCorotina(tempo));
    }

    IEnumerator FadeInCorotina(float tempo) {
        while (texto.color.a < alphaMax) {
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, texto.color.a + (Time.deltaTime / tempo));
            yield return null;
        }
    }

    public void FadeOut(float tempo) {
        StartCoroutine(FadeOutCorotina(tempo));
    }

    IEnumerator FadeOutCorotina(float tempo) {
        while (texto.color.a > 0) {
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, texto.color.a - (Time.deltaTime / tempo));
            yield return null;
        }
    }

    public void MostraFade(float demoraFade, float tempoAtivo, string str) {
        StartCoroutine(MostraFadeCorotina(demoraFade, tempoAtivo, str));
    }

    IEnumerator MostraFadeCorotina(float demoraFade, float tempoAtivo, string str) {
        texto.text = str;
        yield return StartCoroutine(FadeInCorotina(demoraFade));
        yield return new WaitForSeconds(tempoAtivo);
        yield return StartCoroutine(FadeOutCorotina(demoraFade));
    }
}
