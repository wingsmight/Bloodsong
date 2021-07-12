using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOrder : MonoBehaviour
{
    [SerializeField] private MessageTelling messageTelling;
    [SerializeField] private Message message;
    [SerializeField] private FadeAnimation gameLogoScreenAnimation;
    [SerializeField] private CompanySplashScreen companySplashScreen;
    [SerializeField] private FadeAnimation textGameLogoAnimation;


    private bool isMessageTold;
    private float gameLogoShowDuration = 3.0f;


    private void Start()
    {
        Load();
    }


    public void Load()
    {
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        companySplashScreen.Show();

        yield return new WaitWhile(() => companySplashScreen.IsShowing);

        textGameLogoAnimation.Appear();
        messageTelling.Tell(message);
        messageTelling.OnStop += () => isMessageTold = true;

        yield return new WaitWhile(() => !isMessageTold);

        textGameLogoAnimation.Disappear();
        gameLogoScreenAnimation.Appear();

        yield return new WaitForSeconds(gameLogoShowDuration);

        gameLogoScreenAnimation.Disappear();

        yield return new WaitWhile(() => gameLogoScreenAnimation.IsShowing);

        SceneManager.LoadScene("Story");
    }
}
