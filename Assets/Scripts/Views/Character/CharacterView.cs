using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Character;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Position position;
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private Image image;
    [SerializeField] private float appearDuration;


    private CharacterProperty characterProperty;


    public void Show(Character character, Emotion emotion, Direction direction)
    {
        if (characterProperty == null || character.name != characterProperty.name)
        {
            StartCoroutine(AppearRoutine(direction));
        }

        ShowImmediately(character, emotion);
    }
    public void ShowImmediately(Character character, Emotion emotion)
    {
        characterProperty = new CharacterProperty(character.name, position, emotion);
        image.sprite = character.GetSprite(emotion);
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
    public CharacterProperty CharacterProperty => characterProperty;
    public bool IsShowing => fadeAnimation.IsShowing;


    public enum Direction
    {
        FromLeft,
        FromRight,
        FromButtom,
        FromTop,
    }
    public enum Position
    {
        Left,
        Middle,
        Right,
    }
}
