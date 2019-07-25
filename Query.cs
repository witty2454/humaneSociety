using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        private delegate void EmployeeCrud(Employee _employee);
        public static Client GetClient(string userName, string password)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.UserName == userName).Where(c => c.Password == password).First();
            return clientData;
        }
        public static IQueryable<Client> GetUserAdoptionStatus(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.AddressId == client.AddressId).First();
            var junctionData = db.Clients.Where(c => client.AddressId == clientData.AddressId); 
            return junctionData;
        }
        public static Animal GetAnimalByID(int iD)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var animalData = db.Animals.Where(c => c.AnimalId == iD).First();
            return animalData;
        }
        public static void Adopt(object animal, Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            var animalData = db.Animals.Where(a => a == animal).First();
            var clientData = db.Clients.Where(c => c.AddressId == client.AddressId).First();

            var clientJunctionData = db.Clients.Where(c => c.ClientId == clientData.ClientId).Where(cj => cj.Animal== animalData.AnimalId).First();

            clientJunctionData.approvalStatus = "pending";
            animalData.AdoptionStatus = "pending";
            db.SubmitChanges();
        }
        public static IQueryable<Client> RetrieveClients()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = from c in db.Clients select c;
            return clientData;
        }
        public static IQueryable<USState> GetStates()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var stateData = from us in db.USStates select us;
            return stateData;
        }

        public static void AddNewClient(string inputFirstName, string inputLastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Address newUserAddress = new Address()
            {
                AddressLine1 = streetAddress,
                Zipcode = zipCode,
                USState = state ,
            };
            db.Addresses.InsertOnSubmit(newUserAddress);
            db.SubmitChanges();
            Client newClient = new Client()
            {
                FirstName = inputFirstName,
                LastName = inputLastName,
                UserName = username,
                Password = password,
                Email = email,
                Address = Address.Address,
        };
            db.Clients.InsertOnSubmit(newClient);
            db.SubmitChanges(); 
        }

        public static void UpdateClient(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ClientId == client.ClientId).First();
            clientData = client;
            db.SubmitChanges();
        }
        public static void UpdateUsername(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ClientId == client.ClientId).First();
            clientData.UserName = client.UserName;
            db.SubmitChanges();
        }

        public static void UpdateEmail(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ClientId == client.ClientId).First();
            clientData.Email = client.Email;
            db.SubmitChanges();
        }

        public static void UpdateAddress(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ClientId == client.ClientId).First();
            clientData.Address = client.Address;
            db.SubmitChanges();
        }

        public static void UpdateFirstName(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ClientId == client.ClientId).First();
            clientData.FirstName = client.FirstName;
            db.SubmitChanges();
        }

        public static void UpdateLastName(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ClientId == client.ClientId).First();
            clientData.LastName = client.LastName;
            db.SubmitChanges();
        }



        public static void RunEmployeeQueries(Employee employee, string v)
        {
            EmployeeCrud create = CreateEmployee;
            EmployeeCrud read = ReadEmployee;
            EmployeeCrud update = UpdateEmployee;
            EmployeeCrud delete = DeleteEmployee;
            switch (v)
            {
                case "create":
                    create(employee);
                    break;
                case "read":
                    read(employee);
                    break;
                case "update":
                    update(employee);
                    break;
                case "delete":
                    delete(employee);
                    break;
                default:
                    throw new Exception();

            }

        }
        public static void CreateEmployee(Employee employee)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Employee newEmployee = employee;
            db.Employees.InsertOnSubmit(newEmployee);
            db.SubmitChanges();
        }
        public static void ReadEmployee(Employee employee)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var readEmployee = db.Employees.Where(c => employee.EmployeeId == c.EmployeeId).First();
            List<string> employeeInfoList = new List<string>();
            employeeInfoList.Add(readEmployee.EmployeeId.ToString());
            employeeInfoList.Add(readEmployee.FirstName);
            employeeInfoList.Add(readEmployee.LastName);
            employeeInfoList.Add(readEmployee.UserName);
            employeeInfoList.Add(readEmployee.EmployeeId.ToString());
            employeeInfoList.Add(readEmployee.Email);
            UserInterface.DisplayUserOptions(employeeInfoList);

        }
        public static void UpdateEmployee(Employee employee)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var updatedEmployee = db.Employees.Where(c => employee.EmployeeNumber == c.EmployeeNumber).First();
            updatedEmployee.FirstName = employee.FirstName;
            updatedEmployee.LastName = employee.LastName;
            updatedEmployee.EmployeeNumber  = employee.EmployeeNumber;
            updatedEmployee.Email = employee.Email;
            db.SubmitChanges();

        }
        public static void DeleteEmployee(Employee employee)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var deletedEmployee = db.Employees.Where(c => c.EmployeeNumber == employee.EmployeeNumber).First();
            db.Employees.DeleteOnSubmit(deletedEmployee);
            db.SubmitChanges();
        }

        public static IQueryable<Adoption> GetPendingAdoptions()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var pendingAdoptions = db.Adoptions.Where(pa => pa.ApprovalStatus == "pending");
            return pendingAdoptions;
        }

        public static void UpdateAdoption(bool v, ClientAnimalJunction clientAnimalJunction)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var animalAdopted = db.Animals.Where(c => c.AnimalId == clientAnimalJunction.animal).First();
            if (v == true)
            {
                animalAdopted.AdoptionStatus = "adopted";
                clientAnimalJunction.approvalStatus = "adopted";
            }
            else if (v == false)
            {
                clientAnimalJunction.approvalStatus = "not adopted";
                animalAdopted.AdoptionStatus = "not adopted";
            }
            db.SubmitChanges();

        }

        internal static IQueryable<Animal> GetShots(AnimalShot animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var shotData = db.AnimalShots.Where(c => c.AnimalId == animal.AnimalId);
            return S;
        }

        internal static void UpdateShot(string v, Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.AnimalId == animal.AnimalId).First();
            Shot shotUpdating = db.Shots.Where(c => c.Name == v).First();
            AnimalShot animalShotJunction = db.AnimalShots.Where(c => c.AnimalId == animalUpdating.AnimalId).Where(c => c.ShotId == shotUpdating.ShotId).First();
            animalShotJunction.DateReceived = DateTime.Now;
        }

        public static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            for (int i = 0; i < updates.Count; i++)
            {
                var item = updates.ElementAt(i);
                switch (item.Key)
                {
                    case 1:
                        UpdateCategory(animal, item.Value);
                        break;
                    case 2:
                        UpdateBreed(animal, item.Value);
                        break;
                    case 3:
                        UpdateAnimalName(animal, item.Value);
                        break;
                    case 4:
                        UpdateAnimalAge(animal, item.Value);
                        break;
                    case 5:
                        UpdateDemeanor(animal, item.Value);
                        break;
                    case 6:
                        UpdateKidFriendly(animal, item.Value);
                        break;
                    case 7:
                        UpdatePetFriendly(animal, item.Value);
                        break;
                    case 8:
                        UpdatePetWeight(animal, item.Value);
                        break;
                }
            }
        }

        private static void UpdateCategory(Animal animal, string NewValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.AnimalId == animal.AnimalId).First();
            var Category= db.Animals.Where(c => c.AnimalId == animalUpdating.AnimalId).First();
            var category = db.Categories.Where(c => c.CategoryId == Category.CategoryId).First();
            category.CategoryId = NewValue;
            Category.Category = Category.CategoryId;
            db.SubmitChanges();
        }

        private static void UpdateBreed(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.CategoryId == animal.CategoryId).First();
            Category newBreed = db.Categories.Where(c => c.CategoryId == newValue).First();
            animalUpdating.CategoryId = newBreed.CategoryId;
            db.SubmitChanges();
        }

        private static void UpdateAnimalName(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.CategoryId == animal.CategoryId).First();
            animalUpdating.Name = newValue;
            db.SubmitChanges();
        }

        private static void UpdateAnimalAge(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.CategoryId == animal.CategoryId).First();
            animalUpdating.Age = Convert.ToInt32(newValue);
            db.SubmitChanges();
        }

        private static void UpdateDemeanor(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.CategoryId == animal.CategoryId).First();
            animalUpdating.Demeanor = newValue;
            db.SubmitChanges();
        }

        private static void UpdateKidFriendly(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.CategoryId == animal.CategoryId).First();
            bool kidFriendlyUpdate = Boolean.TryParse(newValue, out kidFriendlyUpdate);
            animalUpdating.KidFriendly = kidFriendlyUpdate;
            db.SubmitChanges();
        }

        private static void UpdatePetFriendly(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.AnimalId == animal.AnimalId).First();
            bool petFriendlyUpdate = Boolean.TryParse(newValue, out petFriendlyUpdate);
            animalUpdating.PetFriendly = petFriendlyUpdate;
            db.SubmitChanges();
        }

        private static void UpdatePetWeight(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.AnimalId == animal.AnimalId).First();
            animalUpdating.Weight = Convert.ToInt32(newValue);
            UserInterface.DisplayUserOptions("Weight entered was not a number.");
            db.SubmitChanges();
        }

        internal static void RemoveAnimal(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var deletedAnimal = db.Animals.Where(c => c.AnimalId == animal.AnimalId).First();
            db.Animals.DeleteOnSubmit(deletedAnimal);
            db.SubmitChanges();
        }

        internal static int? GetBreed(string breedName)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            int? breedID;
            if (db.Categories.Any(c => c.CategoryId == Category))
            {
                var breedType = db.Categories.Where(d => d.CategoryId == breedName).First();
                breedID = breedType.ID;
            }
            else
            {
                AddNewBreed(Category;
                breedID = GetBreed(Category);
            }
            return breedID;
        }

        private static void AddNewBreed(string breedName)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Category newBreed = new Category();
            newBreed.CategoryId = Category;
            newBreed.newCategory = UserInterface.GetStringData("Animal", "the animal's");
            db.Categories.InsertOnSubmit(newBreed);
            db.SubmitChanges();
        }

        internal static int? GetDiet(string foodType, int foodAmount)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            int? dietId;
            if (db.DietPlans.Any(c => c.FoodType == foodType) && db.DietPlans.Any(d => d.FoodAmountInCups == foodAmount))
            {
                var dietPlan = db.DietPlans.Where(c => c.FoodType == foodType).Where(c => c.FoodAmountInCups == foodAmount).First();
                dietId = dietPlan.DietPlanId;
            }
            else
            {
                AddNewDiet(foodType, foodAmount);
                dietId = GetDiet(foodType, foodAmount);
            }
            return dietId;

        }

        private static void AddNewDiet(string foodType, int foodAmount)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            DietPlan newDietPlan = new DietPlan();
            newDietPlan.FoodType = foodType;
            newDietPlan.FoodAmountInCups = foodAmount;
            db.DietPlans.InsertOnSubmit(newDietPlan);
            db.SubmitChanges();
        }

        internal static int? GetLocation(string roomName)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Room selectedRoom = db.Rooms.Where(c => c.RoomNumber == roomName).First();
            int? roomID = selectedRoom.RoomId;
            return roomID;
        }

        internal static void AddAnimal(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var employee = db.Employees.Where(c => c.UserName == userName && c.Password == password).FirstOrDefault();
            return employee;
        }

        public static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Employee employee = db.Employees.Where(c => c.EmployeeNumber == employeeNumber).Where(c => c.Email == email).First();
            return employee;
        }

        public static void AddUsernameAndPassword(Employee employee)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Employee currentEmployee = db.Employees.Where(c => c.EmployeeId == employee.EmployeeId).First();
            currentEmployee.UserName = employee.UserName;
            currentEmployee.Password = employee.Password;

        }

        public static bool CheckEmployeeUserNameExist(string username)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            bool userNameExists;
            if (db.Employees.Any(c => c.UserName == username))
            {
                userNameExists = true;
            }
            else
            {
                userNameExists = false;
            }

            return userNameExists;
        }
    }
}
