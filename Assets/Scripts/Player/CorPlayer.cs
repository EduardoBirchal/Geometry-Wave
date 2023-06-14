using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorPlayer : MonoBehaviour
{
    private SpriteRenderer sprRenderer;
    [SerializeField] private Color color;
    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        color = new Color(
            (float) Random.Range(0.3f, 1.0f),   // Red
            (float) Random.Range(0.3f, 1.0f),   // Green
            (float) Random.Range(0.3f, 1.0f)    // Blue
        ); 
        sprRenderer.material.SetColor("_Color", color); 
    }
}
