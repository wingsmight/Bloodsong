using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPageControlButton : PageControlButton
{
    protected override void OnClick()
    {
        TextShowing.ShowNextPage();
    }
}
