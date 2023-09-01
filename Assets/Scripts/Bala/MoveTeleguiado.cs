using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTeleguiado : MoveConstante
{
    public string tagAlvo;
    public float velVirarBugado;

    GameObject alvoAtual = null;

    void Update()
    {
        ViraProAtual();
        MoveFrente();
    }

    void ViraProAtual() {
        if (alvoAtual == null || !(alvoAtual.scene.IsValid())) {
            alvoAtual = ProcuraObjMaisProximo(tagAlvo);

            if (alvoAtual == null) {
                transform.eulerAngles = new Vector3( 
                    transform.eulerAngles.x,                 
                    transform.eulerAngles.y,
                    transform.eulerAngles.z + velVirarBugado * Time.deltaTime
                );
            }
        }
        else {
            ViraPraObjeto(alvoAtual.transform.position, false);
        }
    }
}
