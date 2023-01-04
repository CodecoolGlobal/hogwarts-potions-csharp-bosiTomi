using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models
{
    public class RegisterForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public HouseType HouseType { get; set; }
        public PetType PetType { get; set; }
        public RegisterForm() { }
        public RegisterForm(string username, string password, HouseType houseType, PetType petType)
        {
            Username = username;
            Password = password;
            HouseType = houseType;
            PetType = petType;
        }
    }
}