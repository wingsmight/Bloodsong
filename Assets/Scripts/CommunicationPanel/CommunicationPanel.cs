using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CommunicationPanel : MonoBehaviour, IHidable, IResetable
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
    public virtual void Reset()
    {
        actionAfterHide = null;
        OnConversationHidden = null;

        this.Hide();
    }
    public void AddActionAfterHide(UnityAction unityAction)
    {
        actionAfterHide += unityAction;
    }

    public bool IsShowing => fadeAnimation.IsShowing;
}
