using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCommunicationButton : UIButton
{
    [SerializeField] private CommunicationPanel communicationPanel;


    protected override void OnClick()
    {
        if (communicationPanel.IsShowing)
        {
            communicationPanel.Hide();
        }
    }
}
