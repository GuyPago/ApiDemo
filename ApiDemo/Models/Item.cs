namespace ApiDemo.Models
{
    public record Item
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private decimal Price { get; set; }
        private DateTimeOffset CreatedDate { get; set; }
    }
}
