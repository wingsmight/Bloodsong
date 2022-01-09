using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats : IStoredData
{
    public UserStat money = new UserStat(0, int.MinValue, int.MaxValue, "Money", AdditiveType.Money);
    public UserStat reputation = new UserStat(0, int.MinValue, int.MaxValue, "Reputation", AdditiveType.Reputation);
    public CareerStat career = new CareerStat(0, new int[] { 3, 5, 10, 15, 20 }, "Career", AdditiveType.Career);
    public UserStat relationship = new UserStat(0, int.MinValue, int.MaxValue, "Relationship", AdditiveType.Relationship);


    public UserStat GetStatByAdditiveType(AdditiveType additiveType)
    {
        switch (additiveType)
        {
            case AdditiveType.Money: return money;
            case AdditiveType.Reputation: return reputation;
            case AdditiveType.Career: return career;
            case AdditiveType.Relationship: return relationship;
            default: throw new Exception("Can't return UsetStat of nonexistent AdditiveType");
        }
    }
}
