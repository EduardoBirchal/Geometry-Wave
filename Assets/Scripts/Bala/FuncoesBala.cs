using Unity.Netcode;
public class FuncoesBala : FuncoesGerais
{
    [ServerRpc]  
    protected void DestroiBalaServerRpc()
    {
        Destroy(gameObject);
    }
}
