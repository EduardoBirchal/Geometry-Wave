using UnityEngine;
using Unity.Netcode;

public class DestruirForaDaTela : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "LimiteTela") {
            DestruirBalaServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    protected void DestruirBalaServerRpc()
    {
        Destroy(gameObject);
    }
}
