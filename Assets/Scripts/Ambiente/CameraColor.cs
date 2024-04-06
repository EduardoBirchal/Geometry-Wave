using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColor : MonoBehaviour
{

    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        Debug.Log(ColorPicker.baseColor[ColorCode.Fundo]);
        camera.backgroundColor = ColorPicker.baseColor[ColorCode.Fundo];
    }

    
}
