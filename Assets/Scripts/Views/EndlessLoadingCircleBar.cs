using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessLoadingCircleBar : MonoBehaviour
{
    [SerializeField] private Image image;
    [Space(12)]
    [SerializeField] private float fillSpeed = 1.0f;
    [SerializeField] private float minFillAmount = 0.15f;
    [SerializeField] private float maxFillAmount = 0.75f;
    [SerializeField] private float rotateSpeed = 2.0f;


    private bool isShowing;


    private void Awake()
    {
        image.type = Image.Type.Filled;
    }


    public void Show()
    {
        gameObject.SetActive(true);

        isShowing = true;

        StartCoroutine(ChangeFillRoutine());
        StartCoroutine(RotateRoutine());
    }
    public void Hide()
    {
        isShowing = false;

        gameObject.SetActive(false);
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


    public bool IsShowing => isShowing;
}
