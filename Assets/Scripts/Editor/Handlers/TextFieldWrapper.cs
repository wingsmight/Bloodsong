public static class TextFieldWrapper
{
    public static string Wrap(string text, int width)
    {
        if (string.IsNullOrEmpty(text))
            return "";

        int maxPermissibleIndex = width - 1;
        for (int i = 0; i < text.Length; i++)
        {
            if ((i > maxPermissibleIndex && text[i] == ' ') || text[i] == '\n' || text[i] == '\r')
            {
                text = text.ReplaceAt(i, '\n');
                maxPermissibleIndex = i + width;
            }
        }

        return text;
    }
    public static string Unwrap(string text)
    {
        int newLineIndex = -1;
        do
        {
            newLineIndex = text.IndexOf('\n', newLineIndex + 1);
            if (newLineIndex > 0 && newLineIndex + 1 < text.Length && char.IsLower(text[newLineIndex + 1]))
            {
                text = text.ReplaceAt(newLineIndex, ' ');
            }
        } while (newLineIndex > 0 && newLineIndex < text.Length);

        return text;
    }
}
