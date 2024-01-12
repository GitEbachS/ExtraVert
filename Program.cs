// See https://aka.ms/new-console-template for more information
using System.Data.Common;


List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Species = "corn",
        LightNeeds = 1,
        AskingPrice = 12.00M,
        City = "Chicago",
        Zip = 60195,
        Sold = false,
        AvailableUntil = new DateTime(2024, 1, 30)
    },
     new Plant()
    {
        Species = "Limnanthes floccosa",
        LightNeeds = 4,
        AskingPrice = 9.00M,
        City = "Atlanta",
        Zip = 33221,
        Sold = true,
        AvailableUntil = new DateTime(2024, 3, 23)
    },
      new Plant()
    {
        Species = "flax",
        LightNeeds = 2,
        AskingPrice = 13.00M,
        City = "Chicago",
        Zip = 60193,
        Sold = false,
        AvailableUntil = new DateTime(2024, 4, 22)
    },
       new Plant()
    {
        Species = "allium munzii",
        LightNeeds = 3,
        AskingPrice = 11.00M,
        City = "Chattanooga",
        Zip = 37122,
        Sold = true,
        AvailableUntil = new DateTime(2024, 5, 21)
    },
        new Plant()
    {
        Species = "brodiaea filifolia",
        LightNeeds = 5,
        AskingPrice = 10.00M,
        City = "Pheonix",
        Zip = 45678,
        Sold = false,
        AvailableUntil = new DateTime(2024, 4, 12)
    }
};

MainMenu();

void MainMenu()
{
    string greeting = @"Welcome to ExtraVert! This is where you will find all kinds of plants along with the details about them!";
    Console.WriteLine(greeting);
    string choice = null;
    while (choice == null)
    {
        Console.WriteLine(@"Choose an option:
                            0. Exit
                            1. View All Products
                            2. Post a plant to be adopted
                            3. Adopt a plant
                            4. Delist a plant
                            5. Display the random plant of the day!
                            6. Search for plants with a lower light need
                            7. Stats");
        choice = Console.ReadLine();
        if (choice == "0")
        {
            Console.WriteLine("Goodbye!");
        }
        else if (choice == "1")
        {
            ViewPlants();
        }
        else if(choice == "2")
        {
            AddPlant();
        }
        else if(choice == "3")
        {
            Adopt();
        }
        else if(choice == "4")
        {
            DeletePlant();
        }
        else if (choice == "5")
        {
            RandomPlant();
        }
        else if (choice == "6")
        {
            LightNeedsMax();
        }
        else if (choice == "7")
        {
            Stats();
        }
    }
}

void ViewPlants()
{
    ListPlants();
    Plant chosen = null;
    while (chosen == null)
    {
        Console.WriteLine("Please enter a plant number: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosen = plants[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do Better!");
        }
    }
    Console.WriteLine($@"You chose:
{chosen.Species}, which costs {chosen.AskingPrice}, and it is found in {chosen.City}. {(chosen.Sold ? $"This plant is not available." : "This plant is available.")}");
    MainMenu();
}

void ListPlants()
{
    Console.WriteLine("Plants:");
for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. A {plants[i].Species} plant {(plants[i].Sold ? $"was sold" : "was not sold")} for {plants[i].AskingPrice} dollars.");
    }
};

void AddPlant()
{
    Plant addedPlant = new Plant()
    {
        Species = "",
        LightNeeds = 0,
        AskingPrice = 0.0M,
        City = "",
        Zip = 0,
        Sold = false,
        AvailableUntil = new DateTime()
    };

    string response = "";
    decimal price = 0M;
    bool validNumber = false;
    DateTime dateTime = new DateTime();

    Console.WriteLine("What is your new plant's species?");
    while (response == "")
    {
        response = Console.ReadLine().Trim().ToLower();
        addedPlant.Species += response;
    };
    Console.WriteLine($"What is the asking price of your {addedPlant.Species}?");
    response = Console.ReadLine();

    validNumber = decimal.TryParse(response, out price);
    if (validNumber == true)
    {
        addedPlant.AskingPrice += price;
    }
    else
    {
        Console.WriteLine("Sorry. Please enter a valid price.");
    };

    int number = 0;
    Console.WriteLine($"What is the Light Needs for your plant (please pick a number between 1 & 5)?");
     response = Console.ReadLine();
    try
    {
        validNumber = int.TryParse(response, out number);
        if (validNumber == true && number >= 1 && number <= 5)
        {
            addedPlant.LightNeeds += number;
        }
    }
    catch
    {
        Console.WriteLine("Please enter a number between 1 & 5 only!");
    }

    Console.WriteLine($"Please enter the city name that your plant is found in: ");
   
     response = Console.ReadLine().Trim().ToLower();
     addedPlant.City += response;

    Console.WriteLine($"What is the zip code for your plant?");
    response = Console.ReadLine().Trim();
    try 
    {
        validNumber = int.TryParse(response, out number);
        if (validNumber == true && number.ToString().Length == 5)
        {
            addedPlant.Zip += number;
        }
    }
    catch
    {
        Console.WriteLine("Please enter a valid zip code!");
    }
    Console.WriteLine("Please enter the date available until in this format: yyyy, m, d");
    response = Console.ReadLine().Trim();
    try
    {
        validNumber = DateTime.TryParse(response, out dateTime);
        if(validNumber == true)
        {
            addedPlant.AvailableUntil = dateTime;
        }
    }
    catch
    {
        Console.WriteLine("Please enter a valid date!");
        AddPlant();
    }

    Console.WriteLine(@$"Thank you for adding your new plant! Your plants's species is {addedPlant.Species}, your asking price is {addedPlant.AskingPrice}, your plant's location is {addedPlant.City}, {addedPlant.Zip}, and it is available.");
   
    plants.Add(addedPlant);
    MainMenu();
};

void Adopt()
{
    ListAvailablePlants();
    string answer = "";
    Plant chosen = null;
    while (chosen == null)
    {
        Console.WriteLine("Please enter a plant number to adopt: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosen = plants[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do Better!");
        }
    }
    chosen.Sold = true;
    Console.WriteLine($"Congratulations! {chosen.Species} is now yours to adopt!");
    MainMenu();
}

void DeletePlant()
{
    ListPlants();
    string answer = "";
    Plant chosen = null;
    while (chosen == null)
    {
        Console.WriteLine("Please enter a plant number to delete: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosen = plants[response - 1];
            Console.WriteLine(@$"Are you sure you want to delete {chosen.Species}?
                                            1. Yes
                                            2. No");
            answer = Console.ReadLine();

            if (answer == "1")
            {
                plants.RemoveAt(response - 1);
                Console.WriteLine($"You have successfully deleted {chosen.Species}");
                MainMenu();
            }
            else if (answer == "2")
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Sorry, you have entered an invalid entry.");
                MainMenu();
            }

        }
        catch (FormatException)
        {
            Console.WriteLine("Please type only integers!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do Better!");
        }
    }

}

void ListAvailablePlants()
{
    int count = 0;
    Console.WriteLine("Plants:");
    for (int i = 0; i < plants.Count; i++)
    {
        if (plants[i].Sold == false && DateTime.Now < plants[i].AvailableUntil)
        {
            count += 1;
            Console.WriteLine($"{count}. A {plants[i].Species} plant  in {plants[i].City} {(plants[i].Sold ? $"was sold" : "is available")} for {plants[i].AskingPrice} dollars.");
        
        }
       
    }
};

void RandomPlant()
{
    Random random = new Random();
    List<Plant> availablePlants = plants.Where(s => s.Sold == false && DateTime.Now < s.AvailableUntil).ToList();
    int mIndex = random.Next(0, (availablePlants.Count - 1));
    Plant dayPlant = availablePlants[mIndex];
    Console.WriteLine($"Random plant of the day: {dayPlant.Species}, the location is {dayPlant.City}, the Light Needs is {dayPlant.LightNeeds}, and the asking price is {dayPlant.AskingPrice}");
}

void LightNeedsMax()
{
    int response = 0;
    List<Plant> availablePlants = plants.Where(s => s.Sold == false && DateTime.Now < s.AvailableUntil).ToList();
    Console.WriteLine("Please enter a maximum light needs number that is between 1 & 5");
    while (response == 0)
    {
        try
        {
            response = int.Parse(Console.ReadLine().Trim());
            if (response >= 1 && response <= 5)
            {
                List<Plant> lowerLightNeeds = availablePlants.Where(s => s.LightNeeds <= response).ToList();
                foreach (Plant plant in lowerLightNeeds)
                {
                    Console.WriteLine($"{plant.Species}");
                }
                MainMenu();
            }
            else
            {
                Console.WriteLine("Please enter only a number between 1 & 5");
                LightNeedsMax();
            }
        }
        catch
        {
            Console.WriteLine("Please enter a number between 1 & 5");
            LightNeedsMax();
        }
    }
    
}

void Stats()
 {
    string lowestPlantPrice = "";
    decimal lowestPrice = plants.Min(p => p.AskingPrice);
    Plant priceMatch = plants.FirstOrDefault(s => s.AskingPrice == lowestPrice);
    lowestPlantPrice = priceMatch.Species;

    int plantNumberAvailable = 0;
    List<Plant> availablePlants = plants.Where(s => s.Sold == false && DateTime.Now < s.AvailableUntil).ToList();
    plantNumberAvailable = availablePlants.Count;

    string highLightNeeds = "";
    int maxLight = plants.Max(p => p.LightNeeds);
    Plant maxLightFilter = plants.FirstOrDefault(p => p.LightNeeds == maxLight);
    highLightNeeds = maxLightFilter.Species;

    double aveLightNeed = plants.Average(p => p.LightNeeds);

    double percentPlantsAdopted = 0.0;
    double allPlants = plants.Count();
    percentPlantsAdopted = (plantNumberAvailable / allPlants) * 100;

        Console.WriteLine($@"Here are the current plant stats: 
                    *Lowest price of plant: {lowestPlantPrice}
                    *Number of plants availabe: {plantNumberAvailable}
                    *Number of plants with highest light needs: {highLightNeeds}
                    *Average light needs: {aveLightNeed}
                    *Percentage of plants adopted: {percentPlantsAdopted}");



}
 string PlantInfo (Plant plant)
{
    string  plantResult = $"You chose: {plant.Species}, which costs {plant.AskingPrice}, and it is found in {plant.City}. {(plant.Sold ? $"This plant is not available." : "This plant is available.")}";
    return plantResult;
}
