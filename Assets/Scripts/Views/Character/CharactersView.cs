using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersView : MonoBehaviour
{
    [SerializeField] private PositionCharacterViewDictionary characterViews;


    public void Show(Character character, CharacterView.Position position, CharacterView.Direction direction)
    {
        characterViews[position].Show(character, direction);
    }
    public void Hide(CharacterView.Position position)
    {
        characterViews[position].Hide();
    }
    public void HideImmediately(CharacterView.Position position)
    {
        characterViews[position].HideImmediately();
    }
}
