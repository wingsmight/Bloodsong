using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanySplashScreen : MonoBehaviour
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GlintEffect logoGlint;
    [SerializeField] private float startGlintDelay;
    [SerializeField] private float finishDelay;


    private bool isShowing;
    private bool isFadeFinished;


    public void Show()
    {
        isShowing = true;

        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        fadeAnimation.Appear();
        fadeAnimation.OnActiveChanged += (state) => isFadeFinished = state;

        yield return new WaitWhile(() => !isFadeFinished);
        yield return new WaitForSeconds(startGlintDelay);

        logoGlint.Show();

        yield return new WaitWhile(() => logoGlint.IsShowing);
        yield return new WaitForSeconds(finishDelay);

        fadeAnimation.Disappear();

        yield return new WaitWhile(() => fadeAnimation.IsShowing);

        isShowing = false;
    }


    public bool IsShowing => isShowing;
}
