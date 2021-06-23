using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPageControlButton : PageControlButton
{
    [SerializeField] private MonologuePanel monologuePanel;


    protected override void OnClick()
    {
        textShowing.Stop();

        monologuePanel.StopConversation();
    }
}
