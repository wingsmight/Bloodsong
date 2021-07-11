using System;

[Serializable]
public class DialogueStatResponseData : DialogueResponseData
{
    public StatAdditive statAdditive;


    public DialogueStatResponseData() : base()
    {
        this.statAdditive = new StatAdditive(AdditiveType.Money, 0);
    }
    public DialogueStatResponseData(string text, StatAdditive statAdditive) : base(text)
    {
        this.statAdditive = statAdditive;
    }
}
