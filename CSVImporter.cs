using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace HumaneSociety
{
    public static class CSVImporter
    {
        public static void CSVImportToDestinationTable(string filePath, string destinationTable)
        {
            switch (destinationTable)
            {
                case "animal":
                case "Animal":
                    HandleCsvAnimals(filePath);
                    break;
                default:
                    Console.WriteLine("Not a known table"); //ui method later
                    break;
            }

        }

        private static void HandleCsvAnimals(string filePath)
        {
            var db = new HumaneSocietyDataContext();

            var query = File.ReadLines(filePath)
                .SelectMany(line => line.Split(';'))
                .Where(csvLine => !String.IsNullOrWhiteSpace(csvLine))
                .Select(csvLine => new { data = csvLine.Split(',') })
                .Select(s => new 
                {
                    ID = Int32.Parse(s.data[0]),
                    name = s.data[1],
                    breed = IntOrNullParse(s.data[2]),
                    weight = IntOrNullParse(s.data[3]),
                    age = IntOrNullParse(s.data[4]),
                    diet = IntOrNullParse(s.data[5]),
                    location = IntOrNullParse(s.data[6]),
                    demeanor = s.data[7],
                    kidFriendly = BoolOrNullParse(s.data[8]),
                    petFriendly = BoolOrNullParse(s.data[9]),
                    gender = BoolOrNullParse(s.data[10]),
                    adoptionStatus = s.data[11],
                    Employee_ID = IntOrNullParse(s.data[12])
                });  /* in this last select statement */
            foreach (var animal in query)
            {
             
                    var a = new Animal();
                    a.name = animal.name;
                    a.breed = animal.breed;
                    a.weight = animal.weight;
                    a.age = animal.age;
                    a.diet = animal.diet;
                    a.location = animal.location;
                    a.demeanor = animal.demeanor;
                    a.kidFriendly = animal.kidFriendly;
                    a.petFriendly = animal.petFriendly;
                    a.gender = animal.gender;
                    a.adoptionStatus = animal.adoptionStatus;
                    a.Employee_ID = animal.Employee_ID;
                    db.Animals.InsertOnSubmit(a);

                Console.WriteLine("saving...");
                db.SubmitChanges();
                //}
                
                
            }
             
        }
        internal static int? IntOrNullParse(string data)
        {
            int returnValue;
            int? nullValue = null;
            bool canBeInt;
            canBeInt = Int32.TryParse(data, out returnValue);
            if (canBeInt == true)
            {
                return returnValue;
            }
            else
            {
                return nullValue;
            }
        }
        internal static bool? BoolOrNullParse(string data)
        {
            bool returnValue;
            bool? nullValue = null;
            bool canBeBool;
            canBeBool = Boolean.TryParse(data, out returnValue);
            if (canBeBool == true)
            {
                return returnValue;
            }
            else
            {
                return nullValue;
            }
        }
    }
}
