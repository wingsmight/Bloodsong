using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundtrackTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip soundtrack;


    private void Start()
    {
        AudioPlayer.Instance.PlaySmoothy(soundtrack);
    }
}
