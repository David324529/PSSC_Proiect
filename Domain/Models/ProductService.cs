using Domain.Models;


public class ProductService
{
    public CalculatedProduct ProcessProduct(UnvalidatedProduct unvalidatedProduct)
    {
        var validatedProduct = ProductValidator.ValidateProduct(unvalidatedProduct);
        
        var calculatedProduct = ProductValidator.CalculateProduct(validatedProduct);
        
        return calculatedProduct;
    }
}