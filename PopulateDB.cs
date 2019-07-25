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
            employeeNumber = 1,
            email = "stephen@gmail.com",
            firsttName = "Stephen",
            lastName = "White",
            pass = "goober",
            userName = "Stevo",
        };
       
        public PopulateDB()
        {
            db.Employees.InsertOnSubmit(steveEmployee);
            db.SubmitChanges();

        }
    }
}
