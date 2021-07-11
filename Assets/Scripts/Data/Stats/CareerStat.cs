using UnityEngine;

public class CareerStat : UserStat
{
    [SerializeField] private int amountInCurrentBound;
    [SerializeField] private int boundIndex;
    [SerializeField] private int[] bounds;


    public CareerStat(int amount, int[] bounds, string name, AdditiveType additiveType) : base(amount, 0, bounds[bounds.Length - 1], name, additiveType)
    {
        for (int i = 0; i < bounds.Length; i++)
        {
            this.maxAmount += bounds[i];
        }
        this.bounds = bounds;
    }

    protected override void SetAmount(int value)
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


        int currentMinBound = 0;
        amountInCurrentBound = amount;
        boundIndex = 0;
        while (boundIndex < bounds.Length && currentMinBound + bounds[boundIndex] < amount)
        {
            currentMinBound += bounds[boundIndex];
            amountInCurrentBound -= bounds[boundIndex];
            boundIndex++;
        }
    }

    public int AmountInCurrentBound => amountInCurrentBound;
    public int BoundIndex => boundIndex;
    public int[] Bounds => bounds;
}