using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class NetworkStart : MonoBehaviour
{
    private void Start() 
    {
        NetworkManager.Singleton.StartHost();
        gameObject.name = "Player";
    }
}