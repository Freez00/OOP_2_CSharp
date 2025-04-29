internal class Program
{
    private static void Main(string[] args)
    {
        PointShowcase();
    }

    private static void BitWiseEven()
    {
        int x = int.Parse(Console.ReadLine());
        Console.WriteLine("Is even? - " + ((x & 1) == 0));

    }

    private static void BitwiseXORForArrayPairs()   //  A^B
    {
        int[] arr = {  1, 1, 1, 1, 2};

        for (int i = 0; i < arr.Length; i++)
        {
            bool flag = false;
            for(int j = 0; j < arr.Length; j++)
            {
                if (i == j)
                    continue;

                if ((arr[i] ^ arr[j]) == 0)
                    flag = true;
            }

            if(!flag)
                Console.WriteLine(arr[i]);
        }
    }

    private static void BitCounter()
    {
        int x = 53; //  1 1 0 1 0 1
        int count = 0;

        while (x != 0)
        {
            if ((x & 1) == 1)
                count++;

            x = x >> 1;
        }

        Console.WriteLine(count);
    }

    private static void PointShowcase()
    {
        Point p1 = new Point(2, 3);
        Point p2 = new Point();

        Console.WriteLine("p1 " + p1);
        Console.WriteLine("p2 " + p2);

        Console.WriteLine("d1 " + p1.DistanceTo(p2));
        Console.WriteLine("d2 " + Point.DistanceBetween(p1, p2));

    }

}

class Point
{
    private int _x;
    private int _y;

    public int x { get { return _x; } }
    public int y { get { return _y; } }
    public Point(int x, int y)
    {
        this._x = x;
        this._y = y;
    }

    public Point() : this(0, 0) {}

    public double DistanceTo(Point point)
    {
        double distance = Math.Sqrt(Math.Pow(this.x - point.x, 2) + Math.Pow(this.y - point.y, 2));

        return Math.Round(distance,2);
    }

    public static double DistanceBetween(Point point1, Point point2)
    {
        double distance = Math.Sqrt(Math.Pow(point1.x - point2.x, 2) + Math.Pow(point1.y - point2.y, 2));

        return Math.Round(distance, 2);
    }

    public override string ToString()
    {
        string s = $"[x] = {this.x}  |  [y] = {this.y}";
        return s;
    }
}

class Vehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }

    public int Year { get; set; }

    public FuelType fuelType { get; set; }

    public Vehicle(string brand, string model, int year, FuelType fuelType)
    {
        this.Brand = brand;
        this.Model = model;
        this.Year = year;
        this.fuelType = fuelType;
    }

    public Vehicle() : this("", "", 0, FuelType.None) { }

    public void Drive()
    {
        Console.WriteLine(this + " is driving");
    }

    public void ShowInfo()
    {
        Console.WriteLine(this);
    }

    public override string ToString()
    {
        return $"{Brand} {Model} {Year} [ft - {fuelType}]";
    }


}

public enum FuelType
{
    None,
    Petrol,
    Diesel,
    LPG,
    Electricity,
}

class Car : Vehicle
{

}