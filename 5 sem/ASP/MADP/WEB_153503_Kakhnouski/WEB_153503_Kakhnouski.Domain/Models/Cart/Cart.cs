using WEB_153503_Kakhnouski.Domain.Entities;

namespace WEB_153503_Kakhnouski.Domain.Models.Cart;


public class Cart
{
    /// <summary>
    /// Список объектов в корзине
    /// key - идентификатор объекта
    /// </summary>
    public Dictionary<int, CartItem> CartItems { get; set; } = new();

    /// <summary>
    /// Добавить объект в корзину
    /// </summary>
    /// <param name="car">Добавляемый объект</param>
    public virtual void AddToCart(Car car)
    {
        if (CartItems.ContainsKey(car.Id))
            CartItems[car.Id].Quantity++;
        else
            CartItems[car.Id] = new CartItem()
            {
                Car = car,
                Quantity = 1
            };
    }

    /// <summary>
    /// Удалить объект из корзины
    /// </summary>
    /// <param name="id"> id удаляемого объекта</param>
    public virtual void Remove(int id)
    {
        CartItems.Remove(id);
    }

    /// <summary>
    /// Очистить корзину
    /// </summary>
    public virtual void ClearAll()
    {
        CartItems.Clear();
    }

    /// <summary>
    /// Количество объектов в корзине
    /// </summary>
    public int Count =>
        CartItems.Sum(item => item.Value.Quantity);

    /// <summary>
    /// Общее сумма корзины
    /// </summary>
    public double TotalPrice =>
        CartItems.Sum(item => item.Value.Car.Price * item.Value.Quantity);
}
