namespace MinimalAPI_EF_Postgres.Model;

public record Todo
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public bool IsDone { get; set; }
}