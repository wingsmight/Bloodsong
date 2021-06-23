using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class PageControlButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] protected TextShowing textShowing;


    protected bool isActive;


    private void Awake()
    {
        button.onClick.AddListener(() => OnClick());
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public virtual void SetActive(bool isActive)
    {
        this.isActive = isActive;

        gameObject.SetActive(isActive);
    }

    protected abstract void OnClick();
}
