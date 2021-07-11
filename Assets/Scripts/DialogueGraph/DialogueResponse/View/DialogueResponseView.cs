using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueResponseView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private FadeAnimation fadeAnimation;


    public virtual void Init(DialogueResponseData data, UnityAction action)
    {
        text.text = data.text;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
    public void Show()
    {
        fadeAnimation.Appear();
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }
    public void HideImmediately()
    {
        fadeAnimation.SetVisible(false);
    }


    public Button Button => button;
    public TextMeshProUGUI Text => text;
}
