using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOrder : MonoBehaviour
{
    [SerializeField] private StoryTelling storyTelling;
    [SerializeField] private Story story;
    [SerializeField] private FadeAnimation gameLogoAnimation;

    private Coroutine showGameLogoCoroutine;


    private void Start()
    {
        Load();    
    }

    public void Load()
    {
        storyTelling.StartStory(story);
    }
    public void StartDay()
    {
        if (StoryTelling.phraseIndexReal >= story.phrases.Count - 1 && showGameLogoCoroutine == null)
        {
            showGameLogoCoroutine = StartCoroutine(ShowGameLogoRoutine(3.0f));
        }
    }

    private IEnumerator ShowGameLogoRoutine(float duration)
    {
        gameLogoAnimation.Appear();

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene("Story");
    }
}
