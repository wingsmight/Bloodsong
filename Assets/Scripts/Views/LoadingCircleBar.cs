using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCircleBar : MonoBehaviour
{
    private const bool DEFAULT_IS_ROTATE = true;


    [SerializeField] private Image image;
    [SerializeField] private FadeAnimation fadeAnimation;
    [Space(12)]
    [SerializeField] private float fillSpeed = 1.0f;
    [SerializeField] private float rotateSpeed = 2.0f;
    [SerializeField] private float fillSpeedRange = 0.2f;
    [SerializeField] private Direction direction;


    private Coroutine fillCoroutine;
    private Coroutine rotateCoroutine;


    private void Awake()
    {
        image.type = Image.Type.Filled;
        image.fillAmount = 0;
    }


    public void Show(bool isRotate = DEFAULT_IS_ROTATE)
    {
        fadeAnimation.Appear();

        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);
        if (rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        fillCoroutine = StartCoroutine(FillRoutine());
        if (isRotate)
        {
            rotateCoroutine = StartCoroutine(RotateRoutine());
        }
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }


    private IEnumerator FillRoutine()
    {
        image.fillAmount = 0;

        while (image.fillAmount < 1.0f)
        {
            image.fillAmount += Time.deltaTime * Random.Range(0.0f, fillSpeedRange) * fillSpeed;

            yield return new WaitForEndOfFrame();
        }

        Hide();
    }
    private IEnumerator RotateRoutine()
    {
        int directionScalar = direction == Direction.Clockwise ? -1 : 1;
        float startRotationAngle = Random.Range(0.0f, 360.0f);
        transform.localRotation = Quaternion.Euler(0, 0, startRotationAngle);

        while (IsShowing)
        {
            transform.Rotate(new Vector3(0, 0, directionScalar * rotateSpeed * Time.deltaTime));

            yield return new WaitForEndOfFrame();
        }
    }


    public bool IsShowing => fadeAnimation.IsShowing;


    public enum Direction
    {
        Clockwise,
        Anticlockwise,
    }
}
