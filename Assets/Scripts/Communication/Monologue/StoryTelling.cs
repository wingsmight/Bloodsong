using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTelling : MonoBehaviour, IDataSaving
{
    private const int BACKGROUND_APPEAR_INDEX = 4;
    private const int JAMES_APPEAR_INDEX = 5;
    private const int ARTEN_APPEAR_INDEX = 7;
    private const int ZARI_APPEAR_INDEX = 9;


    [SerializeField] private MonologuePanel monologuePanel;
    [Space(12)]
    [SerializeField] private float delayBeforeMonologue;
    [Space(12)]
    [SerializeField] private BackgroundView backgroundView;
    [SerializeField] private List<CharacterView> characterViews;


    private int phraseIndex = 0;
    private Story story;



    private void Awake()
    {
        monologuePanel.OnConversationStoped += NextFrame;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Stop();
        }
    }
#endif

    public void StartStory(Story story, int phraseIndex = 0)
    {
        this.story = story;
        this.phraseIndex = phraseIndex;

        ClearScene();
        if (phraseIndex > 0)
        {
            PollPrevEvents();
        }

        ReadPhrase();
    }
    public void NextFrame()
    {
        if (phraseIndex < story.phrases.Count - 1)
        {
            phraseIndex++;

            ReadPhrase();
        }
        else
        {
            Stop();
        }
    }
    public void Stop()
    {

    }
    public void SaveData()
    {
        Storage.GetData<GameDayData>().phraseIndex = phraseIndex;
    }

    private IEnumerator ShowMonologueWihtDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        monologuePanel.StartConversation(CurrentPhrase.text);
        monologuePanel.SetSpeaker(CurrentPhrase.speaker);
    }
    private void ReadPhrase()
    {
        StopAllCoroutines();
        StartCoroutine(ShowMonologueWihtDelay(delayBeforeMonologue));
        PollEvents();
    }
    private void PollEvents()
    {
        switch (phraseIndex)
        {
            case BACKGROUND_APPEAR_INDEX:
                {
                    backgroundView.Show();

                    break;
                }
            case JAMES_APPEAR_INDEX:
                {
                    characterViews[2].Show(CharacterView.Direction.FromRight);

                    break;
                }
            case ARTEN_APPEAR_INDEX:
                {
                    characterViews[0].Show(CharacterView.Direction.FromLeft);

                    break;
                }
            case ZARI_APPEAR_INDEX:
                {
                    characterViews[1].Show(CharacterView.Direction.FromButtom);

                    break;
                }
        }
    }
    private void PollPrevEvents()
    {
        int lastPhraseIndex = phraseIndex;

        for (int i = 0; i < lastPhraseIndex; i++)
        {
            phraseIndex = i;
            PollEvents();
        }

        phraseIndex = lastPhraseIndex;
    }
    private void ClearScene()
    {
        backgroundView.HideImmediately();
        characterViews.ForEach(x => x.HideImmediately());
    }


    private Phrase CurrentPhrase => story.phrases[phraseIndex];
}
