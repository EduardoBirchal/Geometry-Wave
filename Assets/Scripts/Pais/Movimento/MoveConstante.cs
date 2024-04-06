using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;

public class MoveConstante : MoveAutomatico
{
    void Update()
    {
        MoveFrente();
    }

    [ClientRpc]
    public void CorrectPositionClientRpc(NetworkObjectReference parent, int myIndex)
    {
        if(parent.TryGet(out NetworkObject dereferParent))
        {
            Color newColor = dereferParent.gameObject.GetComponent<SpriteRenderer>().color;
            transform.position = dereferParent.gameObject.transform.GetChild(myIndex).transform.position;
            Debug.Log($"{newColor.r} {newColor.g} {newColor.b}");
            GetComponent<SpriteRenderer>().material.SetColor("_Color",
                dereferParent.gameObject.GetComponent<SpriteRenderer>().material.color
            );
        }
    }
}
