using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSplitMessageButton : UIButton
{
    [SerializeField] private SplitMessagePanel splitTextAppearing;


    protected override void OnClick()
    {
        splitTextAppearing.Hide();
    }
}
