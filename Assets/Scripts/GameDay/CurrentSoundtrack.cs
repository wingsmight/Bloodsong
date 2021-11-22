using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSoundtrack : MonoBehaviour, IDataLoading, IDataSaving
{
    public void LoadData()
    {
        var savedAudio = AudioPlayer.GetAudioFromResource(Storage.GetData<GameDayData>().musicName);
        if (savedAudio != null)
        {
            AudioPlayer.Instance.PlaySmoothy(savedAudio);
        }
    }
    public void SaveData()
    {
        var audioSource = AudioPlayer.Instance.AudioSource;
        if (audioSource.isPlaying && audioSource.clip != null)
        {
            Storage.GetData<GameDayData>().musicName = audioSource.clip.name;
        }
        else
        {
            Storage.GetData<GameDayData>().musicName = "";
        }
    }
}
