namespace EbecShop.WebAPI.Customer.DTO
{
    public class Team_DTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }

        public static Team_DTO MapFromModel(Model.Team model)
        {
            if (model == null)
                return null;

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
