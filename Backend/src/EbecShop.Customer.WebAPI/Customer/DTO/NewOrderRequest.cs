using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EbecShop.WebAPI.Customer.DTO
{
    public class NewOrderRequest: IValidatableObject
    {
        [Required]
        public int TeamId {get;set;}
      
        [Required]
        public Dictionary<int, decimal> Products { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Products.Any() == false)
                yield return new ValidationResult("Products list must not be empty.", new[] { nameof(Products) });

            foreach(var product in Products)
            {
                if (product.Value <= 0)
                    yield return new ValidationResult("A product amount must be greater than 0.", new[] { $"Products id: {product.Key}" });
            }
        }
    }
}
