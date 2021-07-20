using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Location
{
    public const string TEXTURES_PATH = "Backgrounds";


    public string name;
    [NonSerialized] private Sprite sprite;


    public Location(string name)
    {
        this.name = name;
    }


    private void InitSprite()
    {
        if (string.IsNullOrEmpty(name))
            return;

        string locationPath = TEXTURES_PATH + "/" + name;
        var texture = Resources.Load<Texture2D>(locationPath);
        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
        sprite.name = name;
    }


    public Sprite Sprite
    {
        get
        {
            if (sprite == null)
            {
                InitSprite();
            }
            return sprite;
        }
    }
}
