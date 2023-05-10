using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    public bool isPlayer;

    private void Start()
    {
        Debug.Log("###" + IsOwner);
        isPlayer = IsOwner;
    }

    public void MpUpdate()
    {
        Debug.Log(IsOwner);
    }
}