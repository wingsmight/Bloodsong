using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessLoadingCircleBar : FadeView
{
    [SerializeField] private Image image;
    [Space(12)]
    [SerializeField] private float fillSpeed = 2.0f;
    [SerializeField] private float minFillAmount = 0.15f;
    [SerializeField] private float maxFillAmount = 0.75f;
    [SerializeField] private float rotateSpeed = 50.0f;


    private bool isShowing;


    protected override void Awake()
    {
        base.Awake();

        image.type = Image.Type.Filled;
    }


    public override void Show()
    {
        base.Show();

        isShowing = true;

        StartCoroutine(ChangeFillRoutine());
        StartCoroutine(RotateRoutine());
    }
    public override void Hide()
    {
        base.Hide();

        isShowing = false;
    }


    private IEnumerator ChangeFillRoutine()
    {
        while (isShowing)
        {
            image.fillAmount = (Mathf.Sin(Time.time * fillSpeed) * 0.5f + 0.5f) * (maxFillAmount - minFillAmount) + minFillAmount;

            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator RotateRoutine()
    {
        while (isShowing)
        {
            transform.Rotate(new Vector3(0, 0, 1 * rotateSpeed * Time.deltaTime));

            yield return new WaitForEndOfFrame();
        }
    }


    public override bool IsShowing => isShowing;
}
