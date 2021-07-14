using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueStatsResponseView : DialogueResponseView
{
    public override void Init(DialogueResponseData data, UnityAction action)
    {
        base.Init(data, action);

        if (data is DialogueStatsResponseData)
        {
            DialogueStatsResponseData statData = data as DialogueStatsResponseData;

            Button.onClick.AddListener(() =>
            {
                foreach (var statAdditive in statData.statAdditives)
                {
                    Storage.GetData<PlayerStats>().GetStatByAdditiveType(statAdditive.type).Amount += statAdditive.value; // Save only on SaveData()
                }
            });
        }
    }
}
