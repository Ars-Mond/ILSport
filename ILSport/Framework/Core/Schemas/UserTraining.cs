namespace ILSport.Framework.Core.Schemas;

public class UserTraining
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TrainingId { get; set; }

    public User User { get; set; } = null!;
    public Training Training { get; set; } = null!;
}