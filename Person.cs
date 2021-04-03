using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            TimeSpan ts = DateTime.Today - DateOfBirth;
            return ts.Days / 365;
        }
    }
}
