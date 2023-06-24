using to_do_list_app.Enums;

namespace to_do_list_app.Models;
public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime BeginTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public Enums.ProcessStatus Status { get; set; } = Enums.ProcessStatus.Do;
    public int OwnerId { get; set; }
}
