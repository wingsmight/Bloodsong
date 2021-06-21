using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CommunicationPanel : MonoBehaviour
{
    [SerializeField] protected FadeAnimation fadeAnimation;
    [SerializeField] protected float appearingDelay = 1.0f;

    public delegate void ChangedEventHandler();
    public event ChangedEventHandler OnConversationStoped;

    private UnityAction actionAfterStop;


    public virtual void StopConversation()
    {
        OnConversationStoped?.Invoke();

        actionAfterStop?.Invoke();
        actionAfterStop = null;

        fadeAnimation.Disappear();
    }
    public void AddActionAfterStop(UnityAction unityAction)
    {
        actionAfterStop += unityAction;
    }


    public bool IsShowing => fadeAnimation.IsShowing;
}
