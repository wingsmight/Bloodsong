using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StoryMenu
{
    public abstract class StoryMenuButton : UIButton
    {
        [SerializeField] private StoryMenuEntity storyMenuEntity;
    }
}