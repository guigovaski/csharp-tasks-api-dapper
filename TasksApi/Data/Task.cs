using System.ComponentModel.DataAnnotations.Schema;

namespace TasksApi.Data;


[Table("tasks")]
public record TaskE(int Id, string Name, bool Status);
