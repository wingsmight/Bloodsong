using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryView : MonoBehaviour, IHidable
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GallerySceneAlbum album;


    public void Show(List<GalleryScenePhoto> photos)
    {
        album.Show(photos);

        fadeAnimation.Appear();
    }
    public void Hide()
    {
        fadeAnimation.Disappear();
    }
}
