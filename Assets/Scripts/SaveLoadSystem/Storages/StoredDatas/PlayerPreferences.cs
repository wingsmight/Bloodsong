using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerPreferences : IStoredData
{
    public DateTimeData lastExitDate = new DateTimeData(DateTime.MinValue);
}
