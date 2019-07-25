using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class PopulateDB
    {
        HumaneSocietyDataContext db = new HumaneSocietyDataContext();
        Employee steveEmployee = new Employee
        {
            EmployeeNumber = 1,
            Email = "stephen@gmail.com",
            FirstName = "Stephen",
            LastName = "White",
            Password = "goober",
            UserName = "Stevo",
        };
       
        public PopulateDB()
        {
            db.Employees.InsertOnSubmit(steveEmployee);
            db.SubmitChanges();

        }
    }
}
