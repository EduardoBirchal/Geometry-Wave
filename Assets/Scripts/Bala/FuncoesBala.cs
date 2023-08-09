using Unity.Netcode;
public class FuncoesBala : FuncoesGerais
{
    [ServerRpc(RequireOwnership = false)]  
    protected void DestroiBalaServerRpc()
    {
        Destroy(gameObject);
    }

    protected void DestroiBala() {
        if(IsHost) DestroiBalaServerRpc();
    }
}
