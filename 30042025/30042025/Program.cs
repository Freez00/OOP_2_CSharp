using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {

    }
}

class Kitchen
{
    public void Cook(Order order)
    {
        Console.WriteLine($"Cooking {order.dish} for {order.table}");
    }
}

class Waiter
{
    List<Table> assignedTables;
    List<Order> pendingOrders;
    List<Order> readyOrders;
    Kitchen kitchen;

    string name;

    public Waiter(string name) { this.name = name; }

    public void TakeOrder(Table table)
    {
        List<Order> order = table.GetOrder();
        pendingOrders.AddRange(order);
    }

    public void PlaceOrdersInKitchen()
    {
        foreach (var o in pendingOrders)
        {
            pendingOrders.Remove(o);
            kitchen.Cook(o);
            readyOrders.Add(o);
        }
    }

}
class Order
{
    public Dish dish { get; private set; }
    public Table table { get; private set; }

    public Order(Dish dish, Table table)
    {
        this.dish = dish;
        this.table = table;
    }
}

class Table
{
    int id;
    double bill;
    Menu menu;

    public List<Order> GetOrder()
    {
        List<Order> orders = new List<Order>();
        Random rnd = new Random();
        for(int i = 0; i < rnd.Next(0,11); i++)
        {
            var o = new Order(menu.dishes[rnd.Next(0, menu.dishes.Count)], this);
            orders.Add(o);
        }

        return orders;
    }
}

class Menu
{
    public List<Dish> dishes = new List<Dish>();
}

abstract class Dish
{
    public string Name { get; private set; }
    public double Price { get; private set; }

    public Dish(string name, double price)
    {
        Name = name;
        Price = price;
    }
}

class Salad : Dish
{
    public Salad(string name, double price):base(name, price){}
}

class MainDish : Dish
{
    public MainDish(string name, double price): base(name, price) { }
}

class Dessert : Dish
{
    public Dessert(string name, double price): base(name, price) { }
}