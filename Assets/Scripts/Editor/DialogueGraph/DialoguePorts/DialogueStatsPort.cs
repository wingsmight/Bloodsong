using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueStatsPort : DialoguePort
{
    private DialogueStatsResponseData responseData;
    private int statsCount = 0;


    public DialogueStatsPort(DialogueStatsResponseData responseData)
        : base(responseData)
    {
        this.responseData = responseData;

        Button addStatButton = new Button(() =>
        {
            var addedStat = new StatAdditive(AdditiveType.Money, 0);
            responseData.statAdditives.Add(addedStat);
            AddStatAdditive(addedStat);
        });
        addStatButton.text = "+";

        contentContainer.Add(addStatButton);

        Button removeLastStatButton = new Button(() =>
        {
            RemoveLastStatAdditive();
        });
        removeLastStatButton.text = "-";

        contentContainer.Add(removeLastStatButton);

        foreach (var stat in responseData.statAdditives)
        {
            AddStatAdditive(stat);
        }
        statsCount += responseData.statAdditives.Count;

        contentContainer.Add(new Label("  "));
    }

    public void AddStatAdditive(StatAdditive addedStat)
    {
        int contentIndex = statsCount;
        statsCount++;

        AdditiveType additive = AdditiveType.Money;
        EnumField additiveEnumField = new EnumField(additive);
        additiveEnumField.value = addedStat.type;
        additiveEnumField.style.width = 76;
        additiveEnumField.RegisterValueChangedCallback(evt =>
        {
            responseData.statAdditives[contentIndex].type = (AdditiveType)evt.newValue;
        });

        contentContainer.Add(additiveEnumField);

        IntegerField additiveIntField = new IntegerField();
        additiveIntField.value = addedStat.value;
        additiveIntField.RegisterValueChangedCallback(evt =>
        {
            responseData.statAdditives[contentIndex].value = (int)evt.newValue;
        });

        contentContainer.Add(additiveIntField);
    }
    public void RemoveLastStatAdditive()
    {
        if (responseData.statAdditives.Count > 0 && statsCount > 0)
        {
            statsCount--;

            responseData.statAdditives.RemoveAt(responseData.statAdditives.Count - 1);

            int lastConentIndex = contentContainer.childCount - 1;

            var lastContentType = contentContainer[lastConentIndex].GetType();
            var nextToLastContentType = contentContainer[lastConentIndex - 1].GetType();

            if (lastContentType == typeof(IntegerField) && nextToLastContentType == typeof(EnumField))
            {
                contentContainer.RemoveAt(contentContainer.childCount - 1);
                contentContainer.RemoveAt(contentContainer.childCount - 1);
            }
        }
    }
}