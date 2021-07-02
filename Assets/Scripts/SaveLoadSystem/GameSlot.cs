using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System;

public class GameSlot : MonoBehaviour
{
    [SerializeField] private int index;


    public void Delete()
    {
        IsUsed = false;

        StartCoroutine(DeleteDirectoryRoutine());
    }

    private void SetPrevLastGameSlot()
    {
        bool isAnyoneGameSlotAvailable = false;
        for (int i = 0; i < 3; i++)
        {
            if (Storage.GeneralSettings.isSlotUseds[i])
            {
                Storage.GeneralSettings.lastGameSlot = i;
                isAnyoneGameSlotAvailable = true;
                break;
            }
        }
        if (!isAnyoneGameSlotAvailable)
        {
            Storage.GeneralSettings.lastGameSlot = -1;
        }
    }
    private IEnumerator DeleteDirectoryRoutine()
    {
        Time.timeScale = 0;

        string directoryPath = Path.Combine(Application.persistentDataPath, "GameSlot" + index);
        if (Directory.Exists(directoryPath))
        {
            Directory.Delete(directoryPath, true);
        }

        SetPrevLastGameSlot();

        yield return new WaitWhile(() => Directory.Exists(directoryPath));

        Time.timeScale = 1;
    }


    public int Index => index;
    public bool IsUsed
    {
        get => Storage.GeneralSettings.isSlotUseds[index];
        set => Storage.GeneralSettings.isSlotUseds[index] = value;
    }
}
