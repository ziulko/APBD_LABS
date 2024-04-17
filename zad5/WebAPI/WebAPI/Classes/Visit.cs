namespace WebAPI.Classes
{ 
    public class Visit
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AnimalId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}