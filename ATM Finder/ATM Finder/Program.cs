using ATM_Finder;
using static System.Math;

Console.WriteLine("================ Register ATM's locations ================");
int numberOfATMs, xCoordinate, yCoordinate = 0;
string input;
var atmList = new List<ATM>();

do
{
    Console.WriteLine("How many ATM's do you like to register?");
    input = Console.ReadLine();
} while (int.TryParse(input, out numberOfATMs) == false);

for (int i = 0; i < numberOfATMs; i++)
{
    do
    {
        Console.WriteLine($"Please enter X coordinate for ATM number [{i+1}]");
        input = Console.ReadLine();
    } while (int.TryParse(input, out xCoordinate) == false);

    do
    {
        Console.WriteLine($"Please enter y coordinate for ATM number [{i + 1}]");
        input = Console.ReadLine();
    } while (int.TryParse(input, out yCoordinate) == false);

    atmList.Add(new ATM(i+1)
    { 
        X = xCoordinate, 
        Y = yCoordinate 
    });
}

Console.WriteLine("================ Where are you? ================");

do
{
    Console.WriteLine($"Please enter your X coordinate");
    input = Console.ReadLine();
} while (int.TryParse(input, out xCoordinate) == false);

do
{
    Console.WriteLine($"Please enter your Y coordinate");
    input = Console.ReadLine();
} while (int.TryParse(input, out yCoordinate) == false);

var atmFound = FindNearestATM(atmList, new Person { X = xCoordinate , Y = yCoordinate});

Console.WriteLine($"The nearest ATM from you is the ATM: {atmFound} !");

static int FindNearestATM(List<ATM> atmList, Person person)
{
    Dictionary<int, double> distances = [];

    foreach (ATM atm in atmList)
    {
        distances.Add(atm.Id, GetDistance(person, atm));
    }

    var orderedAtms = distances.OrderBy(d => d.Value).Select(d => d.Key);

    return orderedAtms.First();
}

static double GetDistance(Person person, ATM atm)
{
    var distance = Sqrt(Pow((atm.X - person.X), 2) + Pow((atm.Y - person.Y), 2));
    return distance;
}