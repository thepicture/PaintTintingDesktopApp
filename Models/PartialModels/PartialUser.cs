namespace PaintTintingDesktopApp.Models.Entities
{
    public partial class User
    {
        public string PlainPassword { get; set; }
        public string FullName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Patronymic))
                {
                    return $"{LastName} {FirstName}";
                }
                else
                {
                    return $"{LastName} {FirstName} {Patronymic}";
                }
            }
        }
    }
}