using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviourSingleton<AudioPlayer>
{
    public const string MUSIC_PATH = "Music";

    private const float DEFAULT_SMOOTH_DURATION_SECONDS = 1.5f;


    [SerializeField] private AudioSource audioSource;


    protected override void Awake()
    {
        base.Awake();

        if (audioSource == null)
        {
            audioSource = FindObjectOfType<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }


    public void PlaySmoothy(AudioClip clip, float smoothDurationSeconds = DEFAULT_SMOOTH_DURATION_SECONDS)
    {
        if (clip == audioSource.clip)
            return;

        if (audioSource.clip != null && audioSource.isPlaying)
        {
            StopSmoothy(smoothDurationSeconds, () =>
            {
                StartPlaying();
            });
        }
        else
        {
            StartPlaying();
        }

        void StartPlaying()
        {
            audioSource.clip = clip;
            audioSource.Play();

            StartCoroutine(ChangeVolumeSmoothlyRoutine(smoothDurationSeconds, 0.0f, 1.0f));
        }
    }
    public void StopSmoothy(float smoothDurationSeconds = DEFAULT_SMOOTH_DURATION_SECONDS, Action OnStoped = null)
    {
        StartCoroutine(ChangeVolumeSmoothlyRoutine(smoothDurationSeconds, audioSource.volume, 0.0f));
        StartCoroutine(WaitForVolume(0.0f, OnStoped));
    }
    public void Stop()
    {
        StopAllCoroutines();
        audioSource.Stop();
    }
    public static AudioClip GetAudioFromResource(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return null;

        string audioClipPath = MUSIC_PATH + "/" + fileName;
        return Resources.Load<AudioClip>(audioClipPath);
    }

    private IEnumerator ChangeVolumeSmoothlyRoutine(float smoothDurationSeconds, float startVolume, float finishVolume)
    {
        audioSource.volume = startVolume;

        float timeElapsed = 0.0f;
        while (timeElapsed < smoothDurationSeconds)
        {
            audioSource.volume = Mathf.Lerp(startVolume, finishVolume, timeElapsed / smoothDurationSeconds);
            timeElapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        audioSource.volume = finishVolume;
    }
    private IEnumerator WaitForVolume(float targetVolume, Action OnFinish)
    {
        yield return new WaitUntil(() => Mathf.Abs(audioSource.volume - targetVolume) < Mathf.Epsilon);

        OnFinish?.Invoke();
    }


    public AudioSource AudioSource => audioSource;
}
