
class Program{
    static Func<int, bool> isLeapYear = year => (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);

    static void Main(string[] args){
         Console.WriteLine($"Is 1200 a leap year ? : {isLeapYear(1200)}");
    

        RandomizedList<int> randomList = new RandomizedList<int>();

        // Adding elements

        Random r = new Random();
        for (int i =0; i<30 ;i++){
            randomList.Add(r.Next(1000));
        }

        randomList.PrintAll(); // See element order

        int g = r.Next(30);
        Console.WriteLine($"Random element from index range 0-{g}: {randomList.Get(g)}");

        // Checking if empty
        bool f = randomList.IsEmpty();
        Console.WriteLine($"Is list empty? {f}");
    }
}