using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSplitMessageButton : UIButton
{
    [SerializeField] private SplitTextAppearing splitTextAppearing;


    protected override void OnClick()
    {
        splitTextAppearing.Stop();
    }
}
