using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StartMonologueNodeView : NodeView<MonologueNode>
{
    private const float DELAY_AFTER_STOP = 0.15f;
    private const int FIRST_PHRASE_INDEX = 0;


    [SerializeField] private MonologuePanel monologuePanel;
    [SerializeField] private GameDayControl gameDayControl;


    private int phraseIndex = FIRST_PHRASE_INDEX;


    public override void Act(DialogueGraphData dialogue, MonologueNode nodeData)
    {
        Act(dialogue, nodeData, FIRST_PHRASE_INDEX);
    }
    public void Act(DialogueGraphData dialogue, MonologueNode nodeData, int phraseIndex = FIRST_PHRASE_INDEX)
    {
        this.phraseIndex = phraseIndex;
        gameDayControl.CurrentStoryPhraseIndex = phraseIndex;

        Character speaker = ScriptableObjectFinder.Get(nodeData.speakerName, typeof(Character)) as Character;
        monologuePanel.Show(nodeData.texts[phraseIndex], speaker);
        monologuePanel.AddActionAfterHide(() =>
        {
            if ((phraseIndex + 1) < nodeData.texts.Count)
            {
                DelayExecutor.Instance.Execute(DELAY_AFTER_STOP, () => Act(dialogue, nodeData, phraseIndex + 1));
            }
            else
            {
                ProcessNextWithDelay(dialogue, nodeData, DELAY_AFTER_STOP);
            }
        });
    }
    public override void Stop()
    {
        monologuePanel.Reset();
        monologuePanel.Hide();
    }


    public int PhraseIndex => phraseIndex;
}
