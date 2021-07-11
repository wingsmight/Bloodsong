[System.Serializable]
public class StatAdditive
{
    public AdditiveType type;
    public int value;


    public StatAdditive(AdditiveType additiveType, int value)
    {
        this.type = additiveType;
        this.value = value;
    }


    public override string ToString()
    {
        return $"{Localization.GetValue(type.ToString())}: {value.ToString("+#;-#;0")}";
    }
}