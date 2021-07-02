using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenCheckmark : MonoBehaviour
{
    [SerializeField] private Toggle toggle;


    private void Awake()
    {
        toggle.isOn = Screen.fullScreen;
        toggle.onValueChanged.AddListener(SetFullscreen);
    }
    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(SetFullscreen);
    }


    private void SetFullscreen(bool isEnable)
    {
        Screen.fullScreen = isEnable;
    }
}
