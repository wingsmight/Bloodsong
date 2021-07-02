using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ImageExt
{
    public static void AdjustSize(this Image image)
    {
        var spriteRect = image.sprite.rect;

        if (spriteRect.width > spriteRect.height)
        {

        }
    }
}
