using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPageControlButton : PageControlButton
{
    [SerializeField] private MonologuePanel monologuePanel;
    [SerializeField] private DialoguePanel dialoguePanel;


    protected override void OnClick()
    {
        TextShowing.Stop();

        if (monologuePanel != null && monologuePanel.IsShowing)
        {
            monologuePanel.StopConversation();
        }
        if (dialoguePanel != null && dialoguePanel.IsShowing)
        {
            dialoguePanel.StopConversation();
        }
    }
}
