using UnityEngine;
using Unity.Netcode;

public class DestruirForaDaTela : NetworkBehaviour
{
    private void OnBecameInvisible() {
       DestruirBalaServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    protected void DestruirBalaServerRpc()
    {
        Destroy(gameObject);
    }
}
