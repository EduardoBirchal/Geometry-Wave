using UnityEngine;
using Unity.Netcode;

public class DestruirForaDaTela : NetworkBehaviour
{
    private void OnBecameInvisible() {
       DestruirBalaServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestruirBalaServerRpc()
    {
        Destroy(gameObject);
    }
}
