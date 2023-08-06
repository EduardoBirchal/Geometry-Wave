using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private GameObject text_Obj;
    [SerializeField] private GameObject death_Screen;
    private TimeManager time_Script;

    private void Start() 
    {
        time_Script = GameObject.Find("Funcoes").GetComponent<TimeManager>();
        text_Obj.GetComponent<TextMeshProUGUI>().text = "";
    }

    public async Task KillPlayer()
    {
        text_Obj.GetComponent<FuncoesTexto>().MostraFade(2, 2, "Você morreu");
        await Task.Delay(5 * 1000);
        
        // TODO: Quando implementar o fim de jogo no MP, adicionar a condição aqui
        if(NetworkStart.isSingleplayer == true || NetworkStatus.numPlayers == 1)
            time_Script.Pause();
        
        death_Screen.SetActive(true);
        return;
    }
}
