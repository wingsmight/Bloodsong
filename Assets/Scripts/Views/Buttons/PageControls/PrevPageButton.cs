using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrevPageButton : PageControlButton
{
    protected override void OnClick()
    {
        TextShowing.ShowPreviousPage();
    }
}
