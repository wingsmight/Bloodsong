using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CharacterView;
using static Character;
using System;

public class CharactersView : MonoBehaviour, IHidable, IResetable, IDataLoading, IDataSaving
{
    [SerializeField] private PositionCharacterViewDictionary characterViews;


    public void Hide()
    {
        foreach (var characterView in characterViews.Values)
        {
            characterView.Hide();
        }
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
            this[characterProperty.position].ShowWithFade(character, characterProperty.emotion);
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


    public CharacterView this[Position position]
    {
        get
        {
            return characterViews[position];
        }
    }
}
