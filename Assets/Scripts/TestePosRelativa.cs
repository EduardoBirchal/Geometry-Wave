using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestePosRelativa : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        print(transform.InverseTransformPoint(player.transform.position));
    }
}
