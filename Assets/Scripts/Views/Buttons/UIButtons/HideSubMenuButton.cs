using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSubMenuButton : UIButton
{
    [SerializeField] [RequireInterface(typeof(IHidable))] private Object hidableObject;
    [SerializeField] [RequireInterface(typeof(IShowable))] private Object showableObject;


    protected override void OnClick()
    {
        HidableObject.Hide();
        ShowableObject.Show();
    }


    public IHidable HidableObject => hidableObject as IHidable;
    public IShowable ShowableObject => showableObject as IShowable;
}
