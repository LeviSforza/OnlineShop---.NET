namespace Lista10.Models
{
    public class Cart
    {
        public string CartID { get; set; }

        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

    }
}
