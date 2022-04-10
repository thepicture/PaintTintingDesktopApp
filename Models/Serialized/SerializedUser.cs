namespace PaintTintingDesktopApp.Models.Serialized
{
    public class SerializedUser
    {
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
    }
}
