using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StoryMenu
{
    public abstract class StoryMenuElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
    {
        [SerializeField] private StoryMenuEntity parentEntity;


        protected virtual void Start()
        {
            SetState(ElementState.Idle);
        }


        public abstract void SetState(ElementState state);

        public void OnPointerEnter(PointerEventData eventData)
        {
            ElementState newState = ElementState.Hover;
            SetState(newState);

            parentEntity.NotifyStateChanged(newState);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            ElementState newState = ElementState.Idle;
            SetState(newState);

            parentEntity.NotifyStateChanged(newState);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            ElementState newState = ElementState.Click;
            SetState(newState);

            parentEntity.NotifyStateChanged(newState);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            ElementState newState = ElementState.ClickDown;
            SetState(newState);

            parentEntity.NotifyStateChanged(newState);
        }
    }
}