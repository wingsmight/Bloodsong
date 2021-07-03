using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButton : UIButton
{
    [SerializeField] [RequireInterface(typeof(IShowable))] private Object showableObject;


    protected override void OnClick()
    {
        ShowableObject.Show();
    }


    public IShowable ShowableObject => showableObject as IShowable;
}
