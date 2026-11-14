using System.Collections.Generic;
using System.Linq;
using System.Text.Json; //json serializer; objects <-> json
using System.IO;
Console.WriteLine("PROGRAM STARTED");

List<Person> people = new List<Person>();
bool keepAdding = true;

void SavePeople()
{
    //convert list json
    string json = JsonSerializer.Serialize(people);
    File.WriteAllText("people.json", json);
}

void LoadPeople()
{   
    Console.WriteLine("ssss");
    if (!File.Exists("people.json"))
    {
        Console.WriteLine("sdzfasdfasdf");
        return;
    }
    string json = File.ReadAllText("people.json");
    people = JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();
}

void AddPerson()
{
    Console.Write("Enter first name: ");
    string firstName = Console.ReadLine() ?? "";

    Console.Write("Enter last name: ");
    string lastName = Console.ReadLine() ?? "";

    Console.Write("Enter birthday (MM/DD/YYYY): ");
    string birthDayInput = Console.ReadLine() ?? "";

    // if (DateTime.TryParse(birthDayInput, out DateTime birthDay)){
    //     Console.WriteLine($"Name: {firstName} {lastName}");

    //     Console.WriteLine($"Birthday: {birthDay:MMMM d, yyyy}");
    // }
    // else {
    //     Console.WriteLine("Invalid birthday!");
    // }



    if (DateTime.TryParse(birthDayInput, out DateTime birthDay))
    {
        Person person = new Person(firstName, lastName, birthDay);

        // Console.WriteLine($"Name: {person.FirstName} {person.LastName}");
        // Console.WriteLine($"Birthday: {person.Birthday:MMMM d, yyyy}");

        // Check for duplicates
        bool exists = people.Exists( p=> p.FirstName == firstName && p.LastName == lastName );
        if (!exists){
            people.Add(person);
            SavePeople();
        }
        else
        {
            Console.WriteLine("Person already exists in list!");
        }
        Console.WriteLine($"People stored: {people.Count}");
    }
    else
    {
        Console.WriteLine("Invalid birthday!");
    }

    // Console.Write("Add another person? (yes/no): ");
    // string keepAddingResponse = Console.ReadLine() ?? "";
    // if (!keepAddingResponse.Equals("yes", StringComparison.OrdinalIgnoreCase))
    // {
    //     keepAdding = false;
    // }
    
}

void DisplayPeople()
{
    if ( people.Count == 0)
    {
        Console.WriteLine("No people found!");
        return;
    }

    foreach (Person p in people)
    {
        Console.WriteLine($"{p.FirstName} {p.LastName} - {p.Birthday:MMMM d, yyyy}");
    }
}

void SearchPerson()
{
    Console.Write("Enter first name: ");
    string firstName = Console.ReadLine() ?? "";

    Console.Write("Enter last name: ");
    string lastName = Console.ReadLine() ?? "";

    Person? foundPerson = people.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName);

    if (foundPerson != null)
    {
        Console.WriteLine($"{foundPerson.FirstName} {foundPerson.LastName} - {foundPerson.Birthday:MMMM d, yyyy}");
    }
    else
    {
        Console.WriteLine("Person not found");
    }
}

void DeletePerson()
{
    Console.Write("Enter first name: ");
    string firstName = Console.ReadLine() ?? "";

    Console.Write("Enter last name: ");
    string lastName = Console.ReadLine() ?? "";

    Person? foundPerson = people.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName);

    if (foundPerson != null)
    {
        people.Remove(foundPerson);
        Console.WriteLine("Person deleted!");
        SavePeople();
    }
    else
    {
        Console.WriteLine("No person by that name was found!");
    }
}

void UpdatePerson()
{
    //ask and get first and last name
    Console.WriteLine("Enter first name: ");
    string firstName = Console.ReadLine() ?? "";

    Console.WriteLine("Enter last name: ");
    string lastName = Console.ReadLine() ?? "";

    //check list for matching first and last name
    Person? foundPerson = people.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName);

    //verify foundPerson not null
    if (foundPerson == null)
    {
        Console.WriteLine("Person not found");
        return;
    }

    //ask for birthday
    Console.WriteLine("Enter birthday: ");
    string birthDayinput = Console.ReadLine() ?? "";

    if (DateTime.TryParse(birthDayinput, out DateTime birthDay))
    {
        foundPerson.Birthday = birthDay;
        Console.WriteLine("Birthday updated successfully");
        SavePeople();
    }
    else
    {
        Console.WriteLine("Invalid birthday");
        return;
    }

}
Console.WriteLine("rrr");
LoadPeople();
while (keepAdding)
{   
    
    Console.WriteLine("\nBirthday Manager");
    Console.WriteLine("1. Add a person");
    Console.WriteLine("2. Search for a person");
    Console.WriteLine("3. Display all people");
    Console.WriteLine("4. Delete Person");
    Console.WriteLine("5. Update person");
    Console.WriteLine("6. Exit");

    Console.Write("Select an option: ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            // add person
            AddPerson();
            break;

        case "2":
            // search for person
            SearchPerson();
            break;

        case "3":
            // display all people
            DisplayPeople();
            break;
        
        case "4":
            //delete person
            DeletePerson();
            break;

        case "5":
            //update person
            UpdatePerson();
            break;

        case "6":
            // exit application
            keepAdding = false;
            break;

        default:
            Console.WriteLine("Invalid selection!");
            break;
    }


    
}



public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }

    public Person(string firstName, string lastName, DateTime birthday)
    {
        //assign parameters here
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
    }
}