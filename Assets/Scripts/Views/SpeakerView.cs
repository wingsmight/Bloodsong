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
        if (IsEmptySpeaker(name))
        {
            Hide();
        }
        else
        {
            base.Show();

            nameText.text = name;
        }
    }


    public static bool IsEmptySpeaker(string speakerName)
    {
        return string.IsNullOrEmpty(speakerName) || speakerName == EMPTY_SPEAKER_NAME_EN || speakerName == EMPTY_SPEAKER_NAME_RU;
    }
}
