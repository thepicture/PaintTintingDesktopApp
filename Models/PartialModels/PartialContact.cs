namespace PaintTintingDesktopApp.Models.Entities
{
    public partial class Contact
    {
        public string FormatedPhoneNumber
        {
            get
            {
                {
                    return $"+{PhoneNumber[0]} " +
                        $"({PhoneNumber[1]}{PhoneNumber[2]}{PhoneNumber[3]}) " +
                        $"{PhoneNumber[4]}{PhoneNumber[5]}{PhoneNumber[6]}-" +
                        $"{PhoneNumber[7]}{PhoneNumber[8]}-" +
                        $"{PhoneNumber[9]}{PhoneNumber[10]}";
                }
            }
        }
    }
}
