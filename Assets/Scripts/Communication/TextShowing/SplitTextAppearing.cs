using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TextShowing;

public class SplitTextAppearing : MonoBehaviour
{
    [SerializeField] private List<SplitTextAppearingSet> sets;


    public event eventDelegate OnStopPageTyping;
    public event eventDelegate OnStopTyping;
    public event eventDelegate OnStartTyping;


    private void Awake()
    {
        for (int i = 0; i < sets.Count; i++)
        {
            sets[i].OnStopPageTyping += OnStopPageTyping;
            sets[i].OnStopTyping += OnStopTyping;
            sets[i].OnStartTyping += OnStartTyping;
        }
    }


    public void Type(List<string> fullTexts)
    {
        sets.ForEach(x => x.gameObject.SetActive(false));
        sets[fullTexts.Count - 1].gameObject.SetActive(true);
        sets[fullTexts.Count - 1].Type(fullTexts);
    }
}
