using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitTextAppearing : MonoBehaviour
{
    [SerializeField] private List<SplitTextAppearingSet> sets;


    public void Type(List<string> fullTexts)
    {
        sets.ForEach(x => x.gameObject.SetActive(false));
        sets[fullTexts.Count - 1].gameObject.SetActive(true);
        sets[fullTexts.Count - 1].Type(fullTexts);
    }
}
