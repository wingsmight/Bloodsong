using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPageButton : PageControlButton
{
    protected override void OnClick()
    {
        TextShowing.ShowNextPage();
    }
}
