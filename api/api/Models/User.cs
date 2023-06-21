 namespace api.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  
        public double Coin { get; set; }

        public List<Address> Address { get; set; } 
        public List<ShopCart> ShopCarts { get; set; }
        public List<Favorite> Favorites { get; set; }

        public int RoleId { get; set; } //FK
        //[JsonIgnore] 
        public Role Role { get; set; }
    }
}
