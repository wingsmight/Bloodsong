using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class GalleryButton : StoryMenuButton
    {
        [SerializeField] private GalleryView galleryView;
        [SerializeField] private List<GalleryScenePhoto> testPhotos;


        protected override void OnClick()
        {
            galleryView.Show(testPhotos);
        }
    }
}