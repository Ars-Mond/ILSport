namespace ILSport.Framework.Core.Schemas;

public sealed class UserTrainingSchema
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TrainingId { get; set; }

    public UserSchema User { get; set; } = null!;
    public TrainingSchema Training { get; set; } = null!;
}