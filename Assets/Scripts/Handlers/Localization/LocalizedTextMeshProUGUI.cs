using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedTextMeshProUGUI : LocalizedUiElement
{
    protected TextMeshProUGUI text;


    public override void Refresh()
    {
        if (text == null)
        {
            Init();
        }

        text.text = Localization.GetValue(key);
    }

    protected override void Init()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
}