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
    [SerializeField] private FadeAnimation warningScreen;


    private bool isMessageTold;
    private float gameLogoShowDuration = 3.0f;


    private void Start()
    {
        Load();
    }


    public void Load()
    {
#if UNITY_EDITOR
        if (isSkipIntro)
        {
            SceneManager.LoadScene(NEXT_SCENE_NAME);
            return;
        }
#endif
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        companySplashScreen.Show();

        yield return new WaitWhile(() => companySplashScreen.IsShowing);

        warningScreen.Appear();
        messageTelling.Tell(message);
        messageTelling.OnStop += () => isMessageTold = true;

        yield return new WaitWhile(() => !isMessageTold);

        warningScreen.Disappear();
        gameLogoScreenAnimation.Appear();

        yield return new WaitForSeconds(gameLogoShowDuration);

        gameLogoScreenAnimation.Disappear();

        yield return new WaitWhile(() => gameLogoScreenAnimation.IsShowing);

        SceneManager.LoadScene(NEXT_SCENE_NAME);
    }
}
