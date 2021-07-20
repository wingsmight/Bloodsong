using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOrder : MonoBehaviour
{
    private const string NEXT_SCENE_NAME = "MainMenu";


    [SerializeField] private bool isSkipIntro;
    [Space(12)]
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
        if (isSkipIntro)
        {
            SceneManager.LoadScene(NEXT_SCENE_NAME);
        }
        else
        {
            StartCoroutine(LoadRoutine());
        }
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

        SceneManager.LoadScene(NEXT_SCENE_NAME);
    }
}
