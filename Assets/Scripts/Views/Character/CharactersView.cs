using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersView : MonoBehaviour, IHidable, IResetable, IDataLoading, IDataSaving
{
    [SerializeField] private PositionCharacterViewDictionary characterViews;
    [SerializeField] private CharacterView siraCenterView;


    public void Show(Character character, CharacterView.Position position, CharacterView.Direction direction)
    {
        if (character == null || string.IsNullOrEmpty(character.name) || string.IsNullOrEmpty(character.Name))
        {
            Hide(position);

            return;
        }

        if (character.Name == "Sira" && position == CharacterView.Position.Middle)
        {
            siraCenterView.Show(character, direction);

            return;
        }

        characterViews[position].Show(character, direction);
    }
    public void Show(Character character, CharacterView.Position position)
    {
        if (character.Name == "Sira" && position == CharacterView.Position.Middle)
        {
            siraCenterView.Show(character);

            return;
        }
        characterViews[position].Show(character);
    }
    public void Hide(CharacterView.Position position)
    {
        if (position == CharacterView.Position.Middle)
        {
            siraCenterView.Hide();
        }
        characterViews[position].Hide();
    }
    public void Hide()
    {
        foreach (var characterView in characterViews.Values)
        {
            characterView.Hide();
        }
        siraCenterView.Hide();
    }
    public void HideImmediately(CharacterView.Position position)
    {
        characterViews[position].HideImmediately();
        siraCenterView.HideImmediately();
    }
    public void HideImmediately()
    {
        foreach (var characterView in characterViews.Values)
        {
            characterView.HideImmediately();
        }
        siraCenterView.HideImmediately();
    }
    public void Reset()
    {
        HideImmediately();
    }

    public void LoadData()
    {
        foreach (var postionCharacter in Storage.GetData<GameDayData>().characters)
        {
            Character character = ScriptableObjectFinder.Get(postionCharacter.Value, typeof(Character)) as Character;
            Show(character, postionCharacter.Key);
        }
    }
    public void SaveData()
    {
        Storage.GetData<GameDayData>().characters = new PositionCharacterNameDictionary();
        foreach (var characterView in characterViews)
        {
            if (characterView.Value.IsShowing)
            {
                Storage.GetData<GameDayData>().characters.Add(characterView.Key, characterView.Value.CharacterName);
            }
        }

        if (siraCenterView.IsShowing)
        {
            Storage.GetData<GameDayData>().characters.Add(CharacterView.Position.Middle, "Sira");
        }
    }
}
