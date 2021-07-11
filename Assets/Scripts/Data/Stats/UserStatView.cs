using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserStatView : MonoBehaviour, IShowable<UserStat>
{
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected TextMeshProUGUI shadowText;

    protected UserStat currentData;


    public virtual void Init(UserStat userStat)
    {
        currentData = userStat;
        currentData.OnDataChanged += (value) => Show(value);

        Refresh();
    }
    public virtual void Show(UserStat userStat)
    {
        text.text = userStat.Amount.ToString();
        shadowText.text = text.text;
    }
    public virtual void Refresh()
    {
        Show(currentData);
    }
}
