using WEB_153503_Kakhnouski.Domain.Entities;

namespace WEB_153503_Kakhnouski.Domain.Models.Cart;

public class CartItem
{
    public Car Car { get; set; } = null!;
    public int Quantity { get; set; }
}
