using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour, IDataLoading, IDataSaving
{
    public const string MUSIC_PATH = "Music";

    private const float DEFAULT_SMOOTH_DURATION_SECONDS = 1.5f;


    [SerializeField] private AudioSource audioSource;


    public void PlaySmoothy(AudioClip clip, float smoothDurationSeconds = DEFAULT_SMOOTH_DURATION_SECONDS)
    {
        audioSource.clip = clip;
        audioSource.Play();

        StartCoroutine(ChangeVolumeSmoothlyRoutine(smoothDurationSeconds, 0.0f, 1.0f));
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
    public void LoadData()
    {
        var savedAudio = GetAudioFromResource(Storage.GetData<GameDayData>().musicName);
        if (savedAudio != null)
        {
            PlaySmoothy(savedAudio);
        }
    }
    public void SaveData()
    {
        if (audioSource.isPlaying && audioSource.clip != null)
        {
            Storage.GetData<GameDayData>().musicName = audioSource.clip.name;
        }
        else
        {
            Storage.GetData<GameDayData>().musicName = "";
        }
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
}
