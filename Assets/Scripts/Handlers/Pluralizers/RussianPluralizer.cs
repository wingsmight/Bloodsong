public static class RussianPluralizer
{
    public static string Pluralize(int count, string word)
    {
        int countBy100 = count % 100;
        int countBy10 = count % 10;

        switch (word)
        {
            case "day":
                {
                    if (countBy100 >= 5 && countBy100 <= 20)
                    {
                        return "дней";
                    }
                    else if (countBy10 >= 2 && countBy10 <= 4)
                    {
                        return "дня";
                    }
                    else if ((countBy10 >= 5 && countBy10 <= 9) || (countBy10 == 0 && count != 0))
                    {
                        return "дней";
                    }
                    else if (countBy10 % 10 == 1)
                    {
                        return "день";
                    }

                    break;
                }
        }

        return word.ToString();
    }
}
