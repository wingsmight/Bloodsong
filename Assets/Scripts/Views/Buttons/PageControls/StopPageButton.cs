using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPageButton : PageControlButton
{
    protected override void OnClick()
    {
        TextShowing.Stop();
    }
}
