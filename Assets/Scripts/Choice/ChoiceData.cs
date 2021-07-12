using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ChoiceData
{
    public List<Choice> choiceTexts;


    public ChoiceData(List<Choice> choiceTexts)
    {
        this.choiceTexts = choiceTexts;
    }
}

public struct Choice
{
    public string text;


    public Choice(string text)
    {
        this.text = text;
    }
}
