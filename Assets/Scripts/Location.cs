using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Location
{
    private const string TEXTURES_PATH = "Backgrounds";


    public string name;
    [NonSerialized] private Sprite sprite;


    public Location(string name)
    {
        this.name = name;

        string locationPath = TEXTURES_PATH + "/" + name;
        var texture = Resources.Load<Texture2D>(locationPath);
        this.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
    }


    public Sprite Sprite => sprite;
}
