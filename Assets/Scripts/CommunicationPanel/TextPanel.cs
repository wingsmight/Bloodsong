using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPanel : CommunicationPanel
{
    [SerializeField] private TextMeshProUGUI textView;


    public void Show(string text)
    {
        fadeAnimation.Appear();

        textView.text = text;
    }
    public override void Hide()
    {
        base.Hide();

        fadeAnimation.Disappear();
    }
    public void Refresh()
    {

    }
}
