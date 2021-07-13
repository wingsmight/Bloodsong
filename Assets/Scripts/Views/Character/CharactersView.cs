using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersView : MonoBehaviour, IDataLoading, IDataSaving
{
    [SerializeField] private PositionCharacterViewDictionary characterViews;


    private PositionCharacterNameDictionary characters = new PositionCharacterNameDictionary();// TODO think up something to replace it


    public void Show(Character character, CharacterView.Position position, CharacterView.Direction direction)
    {
        characterViews[position].Show(character, direction);

        SetCharacter(position, character);
    }
    public void Show(Character character, CharacterView.Position position)
    {
        characterViews[position].Show(character);

        SetCharacter(position, character);
    }
    public void Hide(CharacterView.Position position)
    {
        characterViews[position].Hide();
    }
    public void HideImmediately(CharacterView.Position position)
    {
        characterViews[position].HideImmediately();
    }

    public void LoadData()
    {
        characters = Storage.GetData<GameDayData>().characters;

        foreach (var postionCharacter in characters.ToList())
        {
            Character character = ScriptableObjectFinder.Get(postionCharacter.Value, typeof(Character)) as Character;
            Show(character, postionCharacter.Key);
        }
    }
    public void SaveData()
    {
        Storage.GetData<GameDayData>().characters = characters;
    }

    // it is not a part of VIEW!
    private void SetCharacter(CharacterView.Position position, Character character)
    {
        if (!characters.Contains(position))
        {
            characters.Add(position, character.name);
        }
        else
        {
            characters[position] = character.name;
        }
    }
}
