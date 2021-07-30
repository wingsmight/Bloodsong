using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Character;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private FadeView fadeView;
    [SerializeField] private Position position;
    [SerializeField] private Image image;
    [SerializeField] private float appearDuration;


    public event Action OnShown;
    public event Action OnHidden;

    private Action hiddenAction;
    private CharacterProperty characterProperty;


    private void Awake()
    {
        hiddenAction = () => OnHidden?.Invoke();

        fadeView.OnHidden += hiddenAction;
    }
    private void OnDestroy()
    {
        fadeView.OnHidden -= hiddenAction;
    }


    public void Show(Character character, Emotion emotion, Direction direction)
    {
        if (!IsShowing)
        {
            StartCoroutine(AppearRoutine(direction));
            ShowImmediately(character, emotion);
        }
        else
        {
            ShowImmediately(character, emotion);
            OnShown?.Invoke();
        }
    }
    public void ShowWithFade(Character character, Emotion emotion)
    {
        characterProperty = new CharacterProperty(character.name, position, emotion);
        image.sprite = character.GetSprite(emotion);
        image.AdjustWidth();

        fadeView.Show();

        OnShown?.Invoke();
    }
    public void Hide()
    {
        characterProperty = null;

        fadeView.Hide();
    }
    public void HideImmediately()
    {
        characterProperty = null;

        fadeView.HideImmediately();
    }

    private void ShowImmediately(Character character, Emotion emotion)
    {
        characterProperty = new CharacterProperty(character.name, position, emotion);
        image.sprite = character.GetSprite(emotion);
        image.AdjustWidth();

        fadeView.ShowImmediately();
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

        OnShown?.Invoke();
    }


    public Sprite Sprite => image.sprite;
    public CharacterProperty CharacterProperty => characterProperty;
    public bool IsShowing => fadeView.IsShowing;


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
