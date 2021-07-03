using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class GalleryButton : StoryMenuButton
    {
        [SerializeField] private GalleryView galleryView;


        protected override void OnClick()
        {
            galleryView.Show();
        }
    }
}