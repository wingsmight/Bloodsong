using System.Collections.Generic;

public static class EnglishPluralizer
{
    private static Dictionary<string, string> irregulars = new Dictionary<string, string>()
    {

    };


    public static string Pluralize(int count, string noun, string suffix = "s")
    {
        foreach (var item in irregulars)
        {
            if(item.Key == noun)
            {
                return item.Value;
            }
        }
        
        string overriddenSuffix = count == 1 ? "" : suffix;
        return noun + overriddenSuffix;
    }
}
