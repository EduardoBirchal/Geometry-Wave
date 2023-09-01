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
            transform.position = dereferParent.gameObject.transform.GetChild(myIndex).transform.position;
        }
    }
}
