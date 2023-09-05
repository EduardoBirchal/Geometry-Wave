using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGerenciaXP : MonoBehaviour
{
    public float xp, maxXp;
    public int level;
    public Image barra;

    // private void Awake()
    // {
    //     GameObject.Find("GameManager").GetComponent<LvL_Up>().GetPlayerComponents(gameObject);
    // }

    // Start is called before the first frame update
    void Start()
    {
        barra = GameObject.Find("XpBarra").GetComponent<Image>();
        xp = 0;
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        checaXp();
        EsticaBarraXP();
    }

    void checaXp() {
        if (xp >= maxXp) {
            xp = 0;
            maxXp *= 2;
            level++;
        }
    }

    void EsticaBarraXP() {
        barra.fillAmount = Mathf.Clamp(xp/maxXp, 0, 1f); 
    }
}
