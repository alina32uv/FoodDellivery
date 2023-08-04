namespace FoodDelivery.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>() {
            new UserModel() {Username = "jason_admin", EmailAddress = "jason.admin@email.com", Password = "MyPass_w0rd", GivenName = "Jason", Surname = "Bryant", Role = "Admin"},
            new UserModel {Username = "elyse_seller", EmailAddress = "elise.seller@email.com", Password = "MyPass_w0rd", GivenName = "Elise", Surname = "Lambert", Role = "Driver"},
            new UserModel {Username = "alina_user", EmailAddress = "alina.user@email.com", Password = "MyPass_w0rd", GivenName = "Alina", Surname = "Dodi", Role = "User"}
        };
    }
}

