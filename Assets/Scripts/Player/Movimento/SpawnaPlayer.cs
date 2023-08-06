using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class SpawnaPlayer : NetworkBehaviour
{
    [SerializeField] private float raioSpawn = 2.5f;

    private void Start()
    {
        gameObject.name = "Player";
        transform.position = new Vector3(Random.Range(-raioSpawn, raioSpawn), Random.Range(-raioSpawn, raioSpawn), 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}

