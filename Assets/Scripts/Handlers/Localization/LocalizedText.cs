using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : LocalizedUiElement
{
    protected Text text;


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
        text = GetComponent<Text>();
    }
}