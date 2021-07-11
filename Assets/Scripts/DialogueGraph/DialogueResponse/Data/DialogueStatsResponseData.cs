using System;
using System.Collections.Generic;

[Serializable]
public class DialogueStatsResponseData : DialogueResponseData
{
    public List<StatAdditive> statAdditives;


    public DialogueStatsResponseData() : base()
    {
        this.statAdditives = new List<StatAdditive>();
    }
    public DialogueStatsResponseData(string text, List<StatAdditive> statAdditives) : base(text)
    {
        this.statAdditives = statAdditives;
    }
}
