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
            var clientData = db.Clients.Where(c => c.userName == userName).Where(c => c.pass == password).First();
            return clientData;
        }
        public static IQueryable<ClientAnimalJunction> GetUserAdoptionStatus(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).First();
            var junctionData = db.ClientAnimalJunctions.Where(c => client.ID == clientData.ID); 
            return junctionData;
        }
        public static Animal GetAnimalByID(int iD)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var animalData = db.Animals.Where(c => c.ID == iD).First();
            return animalData;
        }
        public static void Adopt(object animal, Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            var animalData = db.Animals.Where(a => a == animal).First();
            var clientData = db.Clients.Where(c => c.ID == client.ID).First();

            var clientJunctionData = db.ClientAnimalJunctions.Where(c => c.client == clientData.ID).Where(cj => cj.animal == animalData.ID).First();

            clientJunctionData.approvalStatus = "pending";
            animalData.adoptionStatus = "pending";
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
            UserAddress newUserAddress = new UserAddress()
            {
                addessLine1 = streetAddress,
                zipcode = zipCode,
                USStates = state,
            };
            db.UserAddresses.InsertOnSubmit(newUserAddress);
            db.SubmitChanges();
            Client newClient = new Client()
            {
                firstName = inputFirstName,
                lastName = inputLastName,
                userName = username,
                pass = password,
                email = email,
                userAddress = newUserAddress.ID
        };
            db.Clients.InsertOnSubmit(newClient);
            db.SubmitChanges(); 
        }

        public static void UpdateClient(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).First();
            clientData = client;
            db.SubmitChanges();
        }
        public static void UpdateUsername(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).First();
            clientData.userName = client.userName;
            db.SubmitChanges();
        }

        public static void UpdateEmail(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).First();
            clientData.email = client.email;
            db.SubmitChanges();
        }

        public static void UpdateAddress(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).First();
            clientData.userAddress = client.userAddress;
            db.SubmitChanges();
        }

        public static void UpdateFirstName(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).First();
            clientData.firstName = client.firstName;
            db.SubmitChanges();
        }

        public static void UpdateLastName(Client client)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clientData = db.Clients.Where(c => c.ID == client.ID).First();
            clientData.lastName = client.lastName;
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
            var readEmployee = db.Employees.Where(c => employee.employeeNumber == c.employeeNumber).First();
            List<string> employeeInfoList = new List<string>();
            employeeInfoList.Add(readEmployee.employeeNumber.ToString());
            employeeInfoList.Add(readEmployee.firsttName);
            employeeInfoList.Add(readEmployee.lastName);
            employeeInfoList.Add(readEmployee.userName);
            employeeInfoList.Add(readEmployee.employeeNumber.ToString());
            employeeInfoList.Add(readEmployee.email);
            UserInterface.DisplayUserOptions(employeeInfoList);

        }
        public static void UpdateEmployee(Employee employee)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var updatedEmployee = db.Employees.Where(c => employee.employeeNumber == c.employeeNumber).First();
            updatedEmployee.firsttName = employee.firsttName;
            updatedEmployee.lastName = employee.lastName;
            updatedEmployee.employeeNumber = employee.employeeNumber;
            updatedEmployee.email = employee.email;
            db.SubmitChanges();

        }
        public static void DeleteEmployee(Employee employee)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var deletedEmployee = db.Employees.Where(c => c.employeeNumber == employee.employeeNumber).First();
            db.Employees.DeleteOnSubmit(deletedEmployee);
            db.SubmitChanges();
        }

        public static IQueryable<ClientAnimalJunction> GetPendingAdoptions()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var pendingAdoptions = db.ClientAnimalJunctions.Where(pa => pa.approvalStatus == "pending");
            return pendingAdoptions;
        }

        public static void UpdateAdoption(bool v, ClientAnimalJunction clientAnimalJunction)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var animalAdopted = db.Animals.Where(c => c.ID == clientAnimalJunction.animal).First();
            if (v == true)
            {
                animalAdopted.adoptionStatus = "adopted";
                clientAnimalJunction.approvalStatus = "adopted";
            }
            else if (v == false)
            {
                clientAnimalJunction.approvalStatus = "not adopted";
                animalAdopted.adoptionStatus = "not adopted";
            }
            db.SubmitChanges();

        }

        internal static IQueryable<AnimalShotJunction> GetShots(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var shotData = db.AnimalShotJunctions.Where(c => c.Animal_ID == animal.ID);
            return shotData;
        }

        internal static void UpdateShot(string v, Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.ID == animal.ID).First();
            Shot shotUpdating = db.Shots.Where(c => c.name == v).First();
            AnimalShotJunction animalShotJunction = db.AnimalShotJunctions.Where(c => c.Animal_ID == animalUpdating.ID).Where(c => c.Shot_ID == shotUpdating.ID).First();
            animalShotJunction.dateRecieved = DateTime.Now;
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

        private static void UpdateCategory(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.ID == animal.ID).First();
            var breed = db.Breeds.Where(c => c.ID == animalUpdating.breed).First();
            var category = db.Catagories.Where(c => c.ID == breed.catagory).First();
            category.catagory1 = newValue;
            breed.catagory = category.ID;
            db.SubmitChanges();
        }

        private static void UpdateBreed(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.ID == animal.ID).First();
            Breed newBreed = db.Breeds.Where(c => c.breed1 == newValue).First();
            animalUpdating.breed = newBreed.ID;
            db.SubmitChanges();
        }

        private static void UpdateAnimalName(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.ID == animal.ID).First();
            animalUpdating.name = newValue;
            db.SubmitChanges();
        }

        private static void UpdateAnimalAge(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.ID == animal.ID).First();
            animalUpdating.age = Convert.ToInt32(newValue);
            db.SubmitChanges();
        }

        private static void UpdateDemeanor(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.ID == animal.ID).First();
            animalUpdating.demeanor = newValue;
            db.SubmitChanges();
        }

        private static void UpdateKidFriendly(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.ID == animal.ID).First();
            bool kidFriendlyUpdate = Boolean.TryParse(newValue, out kidFriendlyUpdate);
            animalUpdating.kidFriendly = kidFriendlyUpdate;
            db.SubmitChanges();
        }

        private static void UpdatePetFriendly(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.ID == animal.ID).First();
            bool petFriendlyUpdate = Boolean.TryParse(newValue, out petFriendlyUpdate);
            animalUpdating.petFriendly = petFriendlyUpdate;
            db.SubmitChanges();
        }

        private static void UpdatePetWeight(Animal animal, string newValue)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Animal animalUpdating = db.Animals.Where(c => c.ID == animal.ID).First();
            animalUpdating.weight = Convert.ToInt32(newValue);
            UserInterface.DisplayUserOptions("Weight entered was not a number.");
            db.SubmitChanges();
        }

        internal static void RemoveAnimal(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var deletedAnimal = db.Animals.Where(c => c.ID == animal.ID).First();
            db.Animals.DeleteOnSubmit(deletedAnimal);
            db.SubmitChanges();
        }

        internal static int? GetBreed(string breedName)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            int? breedID;
            if (db.Breeds.Any(c => c.breed1 == breedName))
            {
                var breedType = db.Breeds.Where(d => d.breed1 == breedName).First();
                breedID = breedType.ID;
            }
            else
            {
                AddNewBreed(breedName);
                breedID = GetBreed(breedName);
            }
            return breedID;
        }

        private static void AddNewBreed(string breedName)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Breed newBreed = new Breed();
            newBreed.breed1 = breedName;
            newBreed.pattern = UserInterface.GetStringData("pattern", "the animal's");
            db.Breeds.InsertOnSubmit(newBreed);
            db.SubmitChanges();
        }

        internal static int? GetDiet(string foodType, int foodAmount)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            int? dietId;
            if (db.DietPlans.Any(c => c.food == foodType) && db.DietPlans.Any(d => d.amount == foodAmount))
            {
                var dietPlan = db.DietPlans.Where(c => c.food == foodType).Where(c => c.amount == foodAmount).First();
                dietId = dietPlan.ID;
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
            newDietPlan.food = foodType;
            newDietPlan.amount = foodAmount;
            db.DietPlans.InsertOnSubmit(newDietPlan);
            db.SubmitChanges();
        }

        internal static int? GetLocation(string roomName)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Room selectedRoom = db.Rooms.Where(c => c.name == roomName).First();
            int? roomID = selectedRoom.ID;
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
            var employee = db.Employees.Where(c => c.userName == userName).Where(c => c.pass == password).First();
            return employee;
        }

        public static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Employee employee = db.Employees.Where(c => c.employeeNumber == employeeNumber).Where(c => c.email == email).First();
            return employee;
        }

        public static void AddUsernameAndPassword(Employee employee)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Employee currentEmployee = db.Employees.Where(c => c.ID == employee.ID).First();
            currentEmployee.userName = employee.userName;
            currentEmployee.pass = employee.pass;

        }

        public static bool CheckEmployeeUserNameExist(string username)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            bool userNameExists;
            if (db.Employees.Any(c => c.userName == username))
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
