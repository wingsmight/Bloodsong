using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeakerView : FadeView, IShowable<string>, IHidable
{
    private const string EMPTY_SPEAKER_NAME_EN = "Author";
    private const string EMPTY_SPEAKER_NAME_RU = "Автор";


    [SerializeField] private TextMeshProUGUI nameText;


    public void Show(string name)
    {
        if (string.IsNullOrEmpty(name) || name == EMPTY_SPEAKER_NAME_EN || name == EMPTY_SPEAKER_NAME_RU)
        {
            Hide();
        }
        else
        {
            base.Show();

            nameText.text = name;
        }
    }
}
