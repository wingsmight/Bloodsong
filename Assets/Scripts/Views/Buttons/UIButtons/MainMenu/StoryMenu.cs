using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public enum ElementState
    {
        Idle,
        Hover,
        Click,
        ClickDown
    }


    public static class StoryMenu
    {
        private static readonly Color idleColor = new Color(0.05f, 0.05f, 0.05f);
        private static readonly Color hoverColor = new Color(0.6862745f, 0.227451f, 0.1921569f);
        private static readonly Color clickColor = new Color(0.5349056f, 0.1729321f, 0.1227803f);
        private static readonly Color clickDownColor = new Color(0.5849056f, 0.1929321f, 0.1627803f);


        public static Color GetColor(ElementState state)
        {
            switch (state)
            {
                case ElementState.Idle:
                    {
                        return idleColor;
                    }
                case ElementState.Hover:
                    {
                        return hoverColor;
                    }
                case ElementState.Click:
                    {
                        return clickColor;
                    }
                case ElementState.ClickDown:
                    {
                        return clickDownColor;
                    }
                default:
                    {
                        return idleColor;
                    }
            }
        }
    }
}