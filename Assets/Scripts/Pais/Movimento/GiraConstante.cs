using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraConstante : MonoBehaviour
{
    public float velGirar;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3 (0, 0, velGirar * Time.deltaTime));
    }
}
