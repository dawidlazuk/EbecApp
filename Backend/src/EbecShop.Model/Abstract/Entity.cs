namespace EbecShop.Model.Abstract
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public bool IsNew
        {
            get
            {
                return Id == default(int);
            }
        }
    }
}
