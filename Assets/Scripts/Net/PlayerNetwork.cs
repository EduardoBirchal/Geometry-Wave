using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    public bool isPlayer;
    public bool isMp;
    public static bool isHost;
    static bool gameStarted;

    public override void OnNetworkSpawn()
    {
        isMp = SceneManager.GetActiveScene().name == "Multiplayer";
        isPlayer = IsOwner;
        isHost = IsHost;
    }
    private void Start() 
    {
        gameObject.name = "Player";
    }

    public bool CheckForClient()
    {
        return (isMp == false || isPlayer == true);
    }
}