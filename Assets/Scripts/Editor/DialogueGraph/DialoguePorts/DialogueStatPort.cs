using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueStatPort : DialoguePort
{
    public DialogueStatPort(DialogueStatResponseData responseData)
        : base(responseData)
    {
        AdditiveType additive = AdditiveType.Money;
        EnumField additiveEnumField = new EnumField(additive);
        additiveEnumField.value = responseData.statAdditive.type;
        additiveEnumField.style.width = 76;
        additiveEnumField.RegisterValueChangedCallback(evt =>
        {
            responseData.statAdditive = new StatAdditive((AdditiveType)evt.newValue, (int)responseData.statAdditive.value);
        });

        contentContainer.Add(additiveEnumField);

        IntegerField additiveIntField = new IntegerField();
        additiveIntField.value = responseData.statAdditive.value;
        additiveIntField.RegisterValueChangedCallback(evt =>
        {
            responseData.statAdditive = new StatAdditive((AdditiveType)responseData.statAdditive.type, (int)evt.newValue);
        });

        contentContainer.Add(additiveIntField);

        contentContainer.Add(new Label("  "));
    }
}
