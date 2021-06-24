using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlintEffect : MonoBehaviour
{
    private const float LEFT_FINISH_SHINE_LOCATION = 0.0f;
    private const float RIGHT_FINISH_SHINE_LOCATION = 1.0f;


    [SerializeField] private Material material;
    [SerializeField] private float speed;
    [SerializeField] private Direction direction;
    [SerializeField] private bool useAcceleration;
    [SerializeField] private float acceleration;


    private Coroutine glintCoroutine;
    private bool isShowing;


    private void OnDestroy()
    {
        material.SetFloat("_ShineLocation", LEFT_FINISH_SHINE_LOCATION);
    }


    public void Show()
    {
        isShowing = true;

        if (glintCoroutine != null)
        {
            StopCoroutine(glintCoroutine);
        }
        glintCoroutine = StartCoroutine(GlintRoutine());
    }
    public void Show(Direction direction)
    {
        this.direction = direction;

        Show();
    }
    private IEnumerator GlintRoutine()
    {
        float shineLocation = direction == Direction.Right ? LEFT_FINISH_SHINE_LOCATION : RIGHT_FINISH_SHINE_LOCATION;
        float startSpeed = speed;

        while (shineLocation >= LEFT_FINISH_SHINE_LOCATION && shineLocation <= RIGHT_FINISH_SHINE_LOCATION)
        {
            material.SetFloat("_ShineLocation", shineLocation);

            shineLocation += Time.deltaTime * speed * (int)direction;

            if (useAcceleration)
            {
                speed += acceleration;
            }

            yield return new WaitForEndOfFrame();
        }

        speed = startSpeed;

        isShowing = false;
    }


    public bool IsShowing => isShowing;


    public enum Direction
    {
        Left = -1,
        Right = 1
    }
}
