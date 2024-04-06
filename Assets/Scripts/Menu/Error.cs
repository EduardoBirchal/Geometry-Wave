using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class Error : MonoBehaviour
{
    public enum PopupState
    {
        Error,
        Timeout,
        Waiting,
        Sucess,
        Idle
    }
    [SerializeField] private GameObject objMessage;
    [SerializeField] private GameObject objButton;
    [SerializeField] private GameObject objFlag;
    [SerializeField] private Sprite sucessSprite;
    [SerializeField] private Sprite failSprite;
    public string text = null;
    public PopupState state = PopupState.Idle;

    public void Start()
    {
        objFlag.SetActive(false);
        objButton.SetActive(false);
    }
    public void UpdateState()
    {
        switch(state)
        {
            case PopupState.Idle:
                this.gameObject.SetActive(false);
                break;
            case PopupState.Sucess:
                text = "Pronto";
                this.gameObject.SetActive(true);
                objFlag.GetComponent<Image>().sprite = sucessSprite; 
                objFlag.SetActive(true);
                StartCoroutine(Fade());
                break;
            case PopupState.Error:
            case PopupState.Timeout:
                GameObject.Find("GameManager").GetComponent<TimeManager>().Pause();
                this.gameObject.SetActive(true);
                objFlag.SetActive(true);
                objFlag.GetComponent<Image>().sprite = failSprite; 
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

    private IEnumerator Fade()
    {
        yield return new WaitForSecondsRealtime(2);
        this.state = PopupState.Idle;
        this.gameObject.SetActive(false);
    }
}
