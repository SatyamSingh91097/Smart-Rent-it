namespace Smart_Rent_it.Models
{
    
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal PricePerDay { get; set; }
            public string ImageUrl { get; set; }
            public bool IsAvailable { get; set; }
        }
    }

