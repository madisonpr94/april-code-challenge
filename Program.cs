using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace DoBApp
{
    class Program
    {
        private static string[] Prompts = { "First name: ", "Last name: ", "Date of birth (YYYY-MM-DD): " };

        // Create a new Person instance generated from user input
        static Person GetNewPerson()
        {
            Console.WriteLine(); // Put a newline, it looks nicer

            string[] personInfo = new string[3];

            // Loop through our 3 prompts for user input
            for(int i = 0; i < 3; i++)
            {
                Console.Write(Prompts[i]);
                personInfo[i] = Console.ReadLine();
                if (personInfo[i] == "")
                {
                    // End if any blank input is given
                    return null;
                }
            }

            // Convert date of birth to DateTime, or prompt user if incorrectly formatted
            DateTime dob;
            if (DateTime.TryParse(personInfo[2], out dob))
            {
                return new Person(personInfo[0], personInfo[1], dob);
            }
            else
            {
                Console.WriteLine("Unable to parse date of birth. Please try again.");
                return GetNewPerson();
            }
        }

        // Display ages of each person, and the average age of all people
        private static void PrintStatistics(List<Person> people)
        {
            if (people.Count == 0)
            {
                Console.WriteLine("\nNo family members were provided! Cannot provide statistics.");
            }
            else
            {
                Console.WriteLine("\nHere are everyone's ages:");

                // Calculate the average age while printing everyone's ages
                double averageAge = 0;

                foreach (Person p in people)
                {
                    int age = p.GetAge();
                    averageAge += (double)age / people.Count;

                    Console.WriteLine($"{p.FirstName} is {age} years old today.");
                }

                Console.WriteLine($"\nThe average age of everyone is {averageAge:f}");
            }
        }

        // Serializes the people list and saves the resulting JSON to a user-specified file
        private static void SaveToFile(List<Person> people)
        {
            Console.WriteLine("\nProvide a filename to save the resulting data, or leave blank to exit immediately.");
            string filename = Console.ReadLine();

            if (filename != "")
            {
                JsonSerializerOptions opts = new JsonSerializerOptions
                {
                    WriteIndented = true // Pretty print the resulting JSON
                };

                // Generate JSON from the people list
                string json = JsonSerializer.Serialize(people, opts);

                try
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    using(StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.Write(json);
                        Console.WriteLine($"File {filename} saved.");
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Invalid filename provided. Please try again.");
                    SaveToFile(people);
                }
                catch
                {
                    Console.Error.WriteLine("Unexpected error while writing file. Data not saved.");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter family member information, or leave any field blank to finish");

            List<Person> familyMembers = new List<Person>();

            // Add family members to the list until empty input is given
            Person nextPerson;
            do
            {
                nextPerson = GetNewPerson();
                if (nextPerson != null)
                {
                    familyMembers.Add(nextPerson);
                }
            }
            while (nextPerson != null);

            PrintStatistics(familyMembers);

            SaveToFile(familyMembers);
        }
    }
}
