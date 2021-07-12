using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTelling : MonoBehaviour
{
    [SerializeField] private MonologuePanel monologuePanel;
    [Space(12)]
    [SerializeField] private float delayBeforeMonologue;


    private int phraseIndex = 0;
    private Message message;

    public delegate void StopHandler();
    public event StopHandler OnStop;


    private void Awake()
    {
        monologuePanel.OnConversationHidden += NextFrame;
    }

    public void Tell(Message message)
    {
        this.message = message;

        ReadPhrase();
    }
    public void NextFrame()
    {
        if (phraseIndex < message.Phrases.Count - 1)
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
        OnStop?.Invoke();
    }

    private IEnumerator ShowMonologueWihtDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        monologuePanel.Show(currentPhrase.text, new Character()); // TODO make character
    }
    private void ReadPhrase()
    {
        StopAllCoroutines();
        StartCoroutine(ShowMonologueWihtDelay(delayBeforeMonologue));
    }

    private Message.Phrase currentPhrase => message.Phrases[phraseIndex];
}
