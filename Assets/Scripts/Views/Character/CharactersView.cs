using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CharacterView;
using static Character;

public class CharactersView : MonoBehaviour, IHidable, IResetable, IDataLoading, IDataSaving
{
    [SerializeField] private PositionCharacterViewDictionary characterViews;


    public void Show(Character character, Position position, Emotion emotion, Direction direction)
    {
        if (character == null || string.IsNullOrEmpty(character.name) || string.IsNullOrEmpty(character.Name))
        {
            Hide(position);

            return;
        }

        characterViews[position].Show(character, emotion, direction);
    }
    public void ShowImmediately(Character character, Position position, Emotion emotion)
    {
        characterViews[position].ShowImmediately(character, emotion);
    }
    public void Hide(Position position)
    {
        characterViews[position].Hide();
    }
    public void Hide()
    {
        foreach (var characterView in characterViews.Values)
        {
            characterView.Hide();
        }
    }
    public void HideImmediately(Position position)
    {
        characterViews[position].HideImmediately();
    }
    public void HideImmediately()
    {
        foreach (var characterView in characterViews.Values)
        {
            characterView.HideImmediately();
        }
    }
    public void Reset()
    {
        HideImmediately();
    }

    public void LoadData()
    {
        foreach (var characterProperty in Storage.GetData<GameDayData>().characters)
        {
            Character character = ScriptableObjectFinder.Get<Character>(characterProperty.name);
            ShowImmediately(character, characterProperty.position, characterProperty.emotion);
        }
    }
    public void SaveData()
    {
        Storage.GetData<GameDayData>().characters = new List<CharacterProperty>();
        foreach (var characterView in characterViews)
        {
            if (characterView.Value.IsShowing)
            {
                var characterProperty = characterView.Value.CharacterProperty;
                Storage.GetData<GameDayData>().characters.Add(new CharacterProperty(characterProperty.name, characterView.Key, characterProperty.emotion));
            }
        }
    }
}
