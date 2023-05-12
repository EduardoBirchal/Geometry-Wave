using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    public bool isPlayer;

    public override void OnNetworkSpawn()
    {
        Debug.Log("###" + IsOwner);
        isPlayer = IsOwner;
    }
    private void Start() 
    {
        gameObject.name = "Player";
    }
}