using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CommunicationPanel : MonoBehaviour, IHidable
{
    [SerializeField] protected FadeAnimation fadeAnimation;
    [SerializeField] protected float appearingDelay = 1.0f;


    public event UnityAction OnConversationHidden;

    private UnityAction actionAfterHide;


    public virtual void Hide()
    {
        fadeAnimation.Disappear();

        OnConversationHidden?.Invoke();

        actionAfterHide?.Invoke();
        actionAfterHide = null;
    }
    public void AddActionAfterHide(UnityAction unityAction)
    {
        actionAfterHide += unityAction;
    }


    public bool IsShowing => fadeAnimation.IsShowing;
}
