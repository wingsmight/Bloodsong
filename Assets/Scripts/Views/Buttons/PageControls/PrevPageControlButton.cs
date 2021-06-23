using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrevPageControlButton : PageControlButton
{
    protected override void OnClick()
    {
        textShowing.ShowPreviousPage();
    }
}
