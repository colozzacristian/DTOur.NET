namespace DTOur.Attributes;

[System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct |
                       System.AttributeTargets.Property,
                       AllowMultiple = false)]
public class MapToAttribute : System.Attribute
{
    public string Name { get; }

    public MapToAttribute(string name)
    {
        Name = name;
    }
}
