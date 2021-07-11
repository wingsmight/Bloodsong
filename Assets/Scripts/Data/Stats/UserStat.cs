using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserStat
{
    public delegate void OnDataChangedHandler(UserStat data);
    public event OnDataChangedHandler OnDataChanged;

    [SerializeField] protected int amount;
    [SerializeField] protected int minAmount;
    [SerializeField] protected int maxAmount;
    [SerializeField] protected string name;
    [SerializeField] protected AdditiveType additiveType;


    public UserStat(int amount, int minAmount, int maxAmount, string name, AdditiveType additiveType)
    {
        this.amount = amount;
        this.minAmount = minAmount;
        this.maxAmount = maxAmount;
        this.name = name;
        this.additiveType = additiveType;
    }


    protected virtual void SetAmount(int value)
    {
        amount = value;

        if (amount < MinAmount)
        {
            amount = MinAmount;
        }
        else if (amount > MaxAmount)
        {
            amount = MaxAmount;
        }
    }

    public int Amount
    {
        get
        {
            return amount;
        }
        set
        {
            SetAmount(value);

            OnDataChanged?.Invoke(this);
        }
    }
    public int MinAmount => minAmount;
    public int MaxAmount => maxAmount;
    public string Name => name;
    public AdditiveType AdditiveType => additiveType;
}
