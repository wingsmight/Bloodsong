using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAdditives : MonoBehaviour
{
    public static string AdditivesToText(List<StatAdditive> addditives)
    {
        string outText = "";
        foreach (var additive in addditives)
        {
            if (additive.value != 0)
            {
                outText += additive.ToString() + "\n";
            }
        }

        return outText;
    }
}
