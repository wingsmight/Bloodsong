using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class StoryMenuEntity : MonoBehaviour
    {
        [SerializeField] private List<StoryMenuElement> storyMenuElements;


        public void NotifyStateChanged(ElementState state)
        {
            storyMenuElements.ForEach(x => x.SetState(state));
        }
    }
}
