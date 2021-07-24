using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Position position;
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private Image image;
    [SerializeField] private float appearDuration;


    private string characterName;


    public void Show(Character character, Direction direction)
    {
        Show(character);

        StartCoroutine(AppearRoutine(direction));
    }
    public void Show(Character character)
    {
        characterName = character.name;
        image.sprite = character.GetSprite(position);
        image.AdjustWidth();

        fadeAnimation.Appear();
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }
    public void HideImmediately()
    {
        fadeAnimation.SetVisible(false);
    }

    private IEnumerator AppearRoutine(Direction direction)
    {
        Vector2 startPosition;
        Vector2 finishPosition = image.rectTransform.anchoredPosition;

        switch (direction)
        {
            case Direction.FromLeft:
                {
                    startPosition = new Vector2(-image.rectTransform.rect.width, image.rectTransform.anchoredPosition.y);

                    break;
                }
            case Direction.FromRight:
                {
                    startPosition = new Vector2(image.rectTransform.rect.width, image.rectTransform.anchoredPosition.y);

                    break;
                }
            case Direction.FromButtom:
                {
                    startPosition = new Vector2(image.rectTransform.anchoredPosition.x, -image.rectTransform.rect.height);

                    break;
                }
            case Direction.FromTop:
                {
                    startPosition = new Vector2(image.rectTransform.anchoredPosition.x, image.rectTransform.rect.height);

                    break;
                }
            default:
                {
                    yield break;
                }
        }

        Vector2 position;
        float timeElapsed = 0;

        image.rectTransform.anchoredPosition = startPosition;

        while (timeElapsed < appearDuration)
        {
            float t = timeElapsed / appearDuration;

            position = Vector2.Lerp(startPosition, finishPosition, MathfExt.Smoothstep3(t));
            image.rectTransform.anchoredPosition = position;

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        image.rectTransform.anchoredPosition = finishPosition;
    }
    public Sprite Sprite => image.sprite;
    public string CharacterName => characterName;
    public bool IsShowing => fadeAnimation.IsShowing;


    public enum Position
    {
        Left,
        Middle,
        Right,
    }
    public enum Direction
    {
        FromLeft,
        FromRight,
        FromButtom,
        FromTop,
    }
}
