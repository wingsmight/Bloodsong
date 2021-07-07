using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryScenePhotoView : MonoBehaviour, IShowable<GalleryScenePhoto>, IHidable
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject emptyOverlay;


    public void Show(GalleryScenePhoto photo)
    {
        image.sprite = photo.sprite;

        emptyOverlay.SetActive(false);
    }
    public void Hide()
    {
        emptyOverlay.SetActive(true);
    }
}
