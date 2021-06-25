using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : UIButton
{
    [SerializeField] [RequireInterface(typeof(IHidable))] private Object hidableObject;


    protected override void OnClick()
    {
        HidableObject.Hide();
    }


    public IHidable HidableObject => hidableObject as IHidable;
}
