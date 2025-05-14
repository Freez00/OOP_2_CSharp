using System.Linq.Expressions;

internal class Program
{
    private static void Main(string[] args)
    {
        //PairExample();
        //ItemListExample();

        MovieStoreExample();
    }

    private static void PairExample()
    {
        Pair<string, double> pair = new Pair<string, double>("string", 12.3);
        Console.WriteLine(pair.getEl1());
        Console.WriteLine(pair.getEl2());
    }

    private static void ItemListExample()
    {
        ItemList<int> list = new ItemList<int>(5);
        list.Add(7);
        list.Add(2);
        list.Add(9);
        list.Print();
        Console.WriteLine();
        list.Remove(7);
        list.Remove(9);
        list.Print();
    }

    private static void MovieStoreExample()
    {
        Movie m1 = new Movie("m1", MovieCategory.New_Release, MovieFormat.DVD);
        Movie m2 = new Movie("m2", MovieCategory.Classic, MovieFormat.Blu_Ray);
        Movie m3 = new Movie("m3", MovieCategory.Classic, MovieFormat.DVD);
        Movie m4 = new Movie("m4", MovieCategory.New_Release, MovieFormat.Blu_Ray);

        Store store = new Store(m1, m2, m3, m4);

        while (true)
        {
            Console.Clear();
            for (int i = 0; i < store.movies.Count; i++)
            {
                Console.WriteLine($"[{i}] {store.movies[i]}");
            }

            int index = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("1 for rent 2 for return");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
                store.movies[index].RentMovie();
            else if (choice == 2)
                store.movies[index].ReturnMovie();

            Console.ReadKey();
        }
    }
}

class Pair<T, K>
{
    private T element1;
    private K element2;

    public Pair(T el1, K el2)
    {
        element1 = el1;
        element2 = el2;
    }

    public T getEl1()
    {
        return element1;
    }

    public K getEl2()
    {
        return element2;
    }
}

class ItemList<T>
{
    private Element root;
    private class Element
    {
        public T value;
        public Element next;

        public Element(T val)
        {
            value = val;
        }
    }

    public ItemList()
    {
        root = null;
    }
    
    public ItemList(T val)
    {
        root = new Element(val);
    }

    public void Add(T val)
    {
        if (root == null)
        {
            root = new Element(val);
            return;
        }
        Element current = root;
        while(current.next != null)
        {
            current = current.next;
        }

        current.next = new Element(val);
    }

    public void Remove(T val)
    {
        if (IsEmpty())
            return;

        if (root.value.Equals(val))
        {
            if (root.next == null)
                root = null;
            else
                root = root.next;
            return;
        }

        Element current = root;
        while(current.next != null && !current.next.value.Equals(val))
        {
            current = current.next;
        }
        var toDel = current.next;

        current.next = toDel.next;
        
    }

    public bool IsEmpty()
    {
        return root == null;
    }

    public void Print()
    {
        var current = root;
        while(current != null)
        {
            Console.WriteLine(current.value);
            current = current.next;
        }
    }
}

class Movie
{
    public string Title { get; private set; }

    public MovieCategory Category { get; private set; }

    public MovieFormat Format { get; private set; }

    public bool Available { get; private set; }

    private readonly decimal LATE_FEE_PER_DAY = 1m;

    private DateTime? _scheduledReturn;

    private static Dictionary<MovieCategory, decimal> categoryPricing =
        new Dictionary<MovieCategory, decimal>()
        {
            {MovieCategory.Classic, 2.25m},
            {MovieCategory.New_Release, 4.95m }
        };
    private static Dictionary<MovieFormat, decimal> formatPricing =
        new Dictionary<MovieFormat, decimal>()
        {
            {MovieFormat.DVD, 0m},
            {MovieFormat.Blu_Ray, 4.95m}
        };


    public Movie(string title, MovieCategory category, MovieFormat format)
    {
        Title = title;
        Category = category;
        Format = format;
        Available = true;
    }

    public void RentMovie()
    {
        if (!Available)
        {
            Console.WriteLine("Movie is not available.");
            return;
        }
        Console.WriteLine("Renting " + this);
        _scheduledReturn = ReadDate();
        Available = false;
        Console.WriteLine("Successfully rented");
    }

    public decimal ReturnMovie()
    {
        Console.WriteLine("Returning " + this);
        var date = ReadDate();
        Console.WriteLine("Scheduled return date: " + _scheduledReturn.ToString());

        var cost = CalculatePrice(date);
        Console.WriteLine("Costs: " + cost);
        _scheduledReturn = null;
        Available = true;
        Console.WriteLine("Successfully returned");


        return cost;
    }

    private decimal CalculatePrice(DateTime returnDate)
    {
        decimal total = 0;
        total += categoryPricing[Category];
        total += formatPricing[Format];
        if(returnDate > _scheduledReturn)
        {
            int daysLate = Math.Abs(((TimeSpan)(returnDate - _scheduledReturn)).Days);
            total += daysLate * LATE_FEE_PER_DAY;
        }

        return total;
    }

    private DateTime ReadDate()
    {
        Console.WriteLine("Enter date in format dd/mm/yyyy");
        string[] input;
        do
        {
            do
            {
                input = Console.ReadLine().Split('/').ToArray();
            }while(input.Length != 3);

        } while (CreateDate(input) == null);
        DateTime date = (DateTime)CreateDate(input);

        return date;
    }

    private DateTime? CreateDate(string[] input)
    {
        int day, month, year;
        if (int.TryParse(input[0], out day) &&
            int.TryParse(input[1], out month) &&
            int.TryParse(input[2], out year))
        {
            return new DateTime(year, month, day);
        }

        return null;
    }

    public override string ToString()
    {
        return $"{Title}  --  Format: {Format}  |  Category: {Category}";
    }
}

class Store
{
    public List<Movie> movies { get; private set; } = new List<Movie>();

    public Store()
    {

    }

    public Store(params Movie[] movies)
    {
        this.movies.AddRange(movies);
    }

    public void AddMovie(Movie movie) => movies.Add(movie);
}

enum MovieCategory
{
    Classic,
    New_Release
}

enum MovieFormat
{
    DVD,
    Blu_Ray
}