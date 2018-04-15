namespace EbecShop.Customer.WebAPI.DTO
{
    public class Team_DTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }

        public static Team_DTO MapFromModel(Model.Team model)
        {
            return new Team_DTO
            {
                Id = model.Id,
                Name = model.Name,
                Balance = model.Balance,
                AvailableBalance = model.AvailableBalance
            };
        }
    }
}
