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
        private static readonly Color hoverColor = new Color(1.0f, 0.04313726f, 0.1215686f);
        private static readonly Color clickColor = new Color(0.8f, 0.04313726f, 0.1215686f);
        private static readonly Color clickDownColor = new Color(0.8f, 0.04313726f, 0.1215686f);


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