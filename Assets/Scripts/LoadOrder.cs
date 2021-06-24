using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOrder : MonoBehaviour
{
    [SerializeField] private MessageTelling messageTelling;
    [SerializeField] private Message message;
    [SerializeField] private FadeAnimation gameLogoAnimation;
    [SerializeField] private CompanySplashScreen companySplashScreen;


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

        messageTelling.Tell(message);
        messageTelling.OnStop += () => isMessageTold = true;

        yield return new WaitWhile(() => !isMessageTold);

        gameLogoAnimation.Appear();

        yield return new WaitForSeconds(gameLogoShowDuration);

        gameLogoAnimation.Disappear();

        yield return new WaitWhile(() => gameLogoAnimation.IsShowing);

        SceneManager.LoadScene("Story");
    }
}
