[System.Serializable]
public class ExposedVariable
{
    private string name = "New String";
    private string value = "New Value";


    public ExposedVariable()
    {
        name = "VariableName";
        value = "Value";
}
    public ExposedVariable(string name, string value)
    {
        this.name = name;
        this.value = value;
    }


    public string Name { get => name; set => name = value; }
    public string Value { get => value; set => this.value = value; }
}