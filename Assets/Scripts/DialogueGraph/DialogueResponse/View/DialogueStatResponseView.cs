using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueStatResponseView : DialogueResponseView
{
    public override void Init(DialogueResponseData data, UnityAction action)
    {
        base.Init(data, action);

        if(data is DialogueStatResponseData)
        {
            DialogueStatResponseData statData = data as DialogueStatResponseData;

            Button.onClick.AddListener(() =>
            {
                Storage.GetData<PlayerStats>().GetStatByAdditiveType(statData.statAdditive.type).Amount += statData.statAdditive.value;
            });
        }
    }
}
