using UnityEngine;
using TMPro;
using Unity.Netcode;

public class Error : MonoBehaviour
{
    public enum PopupState
    {
        Error,
        Waiting,
        Sucess,
        Idle
    }
    [SerializeField] private GameObject objMessage;
    [SerializeField] private GameObject objButton;
    [SerializeField] private GameObject objFlag;
    public string text = null;
    public PopupState state = PopupState.Idle;

    public void Start()
    {
        objFlag.SetActive(false);
        objButton.SetActive(false);
    }
    public void Update()
    {
        switch(state)
        {
            case PopupState.Idle:
                this.gameObject.SetActive(false);
                break;
            case PopupState.Sucess:
                text = "Pronto";
                this.gameObject.SetActive(true);
                break;
            case PopupState.Error:
                if (NetworkStatus.HasTimedOut())
                    text = "Tempo esgotado";
                else
                {
                    text = GameObject.Find("NetworkManager")
                            .GetComponent<NetworkManager>()
                            .DisconnectReason;
                }
                this.gameObject.SetActive(true);
                objFlag.SetActive(true);
                objButton.SetActive(true);
                break;
            case PopupState.Waiting:
                text = "Conectando...";
                this.gameObject.SetActive(true);
                break;
        }

        TMP_Text message = objMessage.GetComponent<TMP_Text>();
        message.text = text;
    }
}
