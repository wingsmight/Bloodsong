using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryMenu
{
    public class SaveButton : StoryMenuButton
    {
        protected override void OnClick()
        {
            SaveLoadLauncher.Instance.SaveDatas();
            Storage.SaveDatas();
        }
    }
}