namespace E_LearningApp.Models.EntityLayer
{
    public class Administrator : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
