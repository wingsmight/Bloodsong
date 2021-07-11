using System;

[Serializable]
public class DialogueResponseData
{
    public string text;


    public DialogueResponseData()
    {
        text = "Response";
    }
    public DialogueResponseData(string text)
    {
        this.text = text;
    }
}
