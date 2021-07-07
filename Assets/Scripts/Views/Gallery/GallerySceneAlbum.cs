using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallerySceneAlbum : Album<GalleryScenePhotoView, GalleryScenePhoto>
{
    public override void Show(List<GalleryScenePhoto> photos, int pageIndex = 0)
    {
        base.Show(photos, pageIndex);
    }
}
