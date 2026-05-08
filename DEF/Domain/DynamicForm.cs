using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.DataAnnotations;

namespace DEF.Domain;

public enum FieldType
{
    Text,
    Integer,
    Currency,
    Date,
    DropDown
}
public enum EntityType
{
    Client,
    Job
}

public class DefineForm
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public EntityType entity { get; set; }
    public string FieldsJson { get; set; } = "[]";
    public List<DefineFields> Fields
    {
        get => System.Text.Json.JsonSerializer.Deserialize<List<DefineFields>>(FieldsJson) ?? new List<DefineFields>();
        set => FieldsJson = System.Text.Json.JsonSerializer.Serialize(value);
    }
}
public class DefineFields
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public FieldType Type { get; set; }
    public bool IsRequired { get; set; }
    public List<string>? Options { get; set; }
}
