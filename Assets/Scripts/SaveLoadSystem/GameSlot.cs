using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System;

public class GameSlot : MonoBehaviour
{
    [SerializeField] private int index;


    public event Action OnDeleted;

    public void Delete()
    {
        IsUsed = false;

        StartCoroutine(DeleteDirectoryRoutine(Path.Combine(SaveSystem.saveFolder, "GameSlot" + index)));
    }

    private void SetPrevLastGameSlot()
    {
        bool isAnyGameSlotAvailable = false;
        for (int i = 0; i < 3; i++)
        {
            if (Storage.GeneralSettings.isSlotUseds[i])
            {
                Storage.GeneralSettings.lastGameSlot = i;
                isAnyGameSlotAvailable = true;
                break;
            }
        }
        if (!isAnyGameSlotAvailable)
        {
            Storage.GeneralSettings.lastGameSlot = -1;
        }
    }
    private IEnumerator DeleteDirectoryRoutine(string path)
    {
        Time.timeScale = 0;

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        IsUsed = false;
        Storage.DeleteData(index);

        SetPrevLastGameSlot();

        yield return new WaitWhile(() => Directory.Exists(path));

        Time.timeScale = 1;

        OnDeleted?.Invoke();
    }


    public int Index => index;
    public bool IsUsed
    {
        get => Storage.GeneralSettings.isSlotUseds[index];
        set => Storage.GeneralSettings.isSlotUseds[index] = value;
    }
}
