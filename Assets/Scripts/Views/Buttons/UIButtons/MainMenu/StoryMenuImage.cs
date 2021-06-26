using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StoryMenu
{
    public class StoryMenuImage : StoryMenuElement, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image image;


        protected void Awake()
        {
            image = GetComponent<Image>();
        }


        public override void SetState(ElementState state)
        {
            image.color = StoryMenu.GetColor(state);
        }
    }
}