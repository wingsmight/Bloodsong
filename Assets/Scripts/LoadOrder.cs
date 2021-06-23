using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOrder : MonoBehaviour
{
    [SerializeField] private MessageTelling messageTelling;
    [SerializeField] private Message message;
    [SerializeField] private FadeAnimation gameLogoAnimation;

    private Coroutine showGameLogoCoroutine;


    private void Start()
    {
        Load();
        messageTelling.OnStop += () => StartDay();
    }

    public void Load()
    {
        messageTelling.Tell(message);
    }
    public void StartDay()
    {
        showGameLogoCoroutine = StartCoroutine(ShowGameLogoRoutine(3.0f));
    }

    private IEnumerator ShowGameLogoRoutine(float duration)
    {
        gameLogoAnimation.Appear();

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene("Story");
    }
}
