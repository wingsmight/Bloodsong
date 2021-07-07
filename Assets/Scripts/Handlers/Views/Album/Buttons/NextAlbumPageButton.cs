using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAlbumPageButton : UIButton, IShowHidable
{
    [SerializeField] [RequireInterface(typeof(IPagable))] private Object album;


    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    protected override void OnClick()
    {
        Album.NextPage();
    }


    private IPagable Album => album as IPagable;
}
