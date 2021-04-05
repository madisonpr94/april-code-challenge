using System;

namespace DoBApp
{
    class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        public Person(string firstName, string lastName, DateTime dob)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
        }

        public int GetAge()
        {
            // Days since birth
            TimeSpan ts = DateTime.Today - DateOfBirth;

            // Ensure leap years are accounted for
            return (int)Math.Floor((ts.Days + 1) / 365.25f);
        }
    }
}
