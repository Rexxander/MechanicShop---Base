List<int> customerIds = new List<int>();
Dictionary<int, string> customerNames = new Dictionary<int, string>();
Dictionary<int, string> customerPhones = new Dictionary<int, string>();
int nextCustomerId = 1;

List<int> vehicleIds = new List<int>();
Dictionary<int, string> vehiclePlates = new Dictionary<int, string>();
Dictionary<int, string> vehicleBrands = new Dictionary<int, string>();
Dictionary<int, string> vehicleModels = new Dictionary<int, string>();
Dictionary<int, int> vehicleOwnerIds = new Dictionary<int, int>();
int nextVehicleId = 1;

List<int> appointmentIds = new List<int>();
Dictionary<int, int> appointmentCustomerIds = new Dictionary<int, int>();
Dictionary<int, int> appointmentVehicleIds = new Dictionary<int, int>();
Dictionary<int, string> appointmentDates = new Dictionary<int, string>();
Dictionary<int, string> appointmentTimes = new Dictionary<int, string>();
Dictionary<int, string> appointmentServices = new Dictionary<int, string>();
int nextAppointmentId = 1;

bool running = true;

Console.WriteLine("=== Mechanic Shop Appointment Control ===");

while (running)
{
    Console.WriteLine("\n========== MAIN MENU ==========");
    Console.WriteLine("1. Customers");
    Console.WriteLine("2. Vehicles");
    Console.WriteLine("3. Appointments");
    Console.WriteLine("4. Exit");
    Console.Write("Choose an option: ");

    try
    {
        int mainOption = Convert.ToInt32(Console.ReadLine());

        switch (mainOption)
        {
            case 1:
                {
                    Console.WriteLine("\n--- CUSTOMERS ---");
                    Console.WriteLine("1. Add Customer\n2. Search Customer\n3. Edit Customer\n4. Delete Customer\n5. List Customers\n6. Back");
                    Console.Write("Choose an option: ");
                    int customerOption = Convert.ToInt32(Console.ReadLine());

                    switch (customerOption)
                    {
                        case 1:
                            {
                                Console.Write("Customer name: ");
                                string name = Console.ReadLine();
                                Console.Write("Phone number: ");
                                string phone = Console.ReadLine();

                                if (name == "" || phone == "")
                                {
                                    Console.WriteLine("Name and phone cannot be empty.");
                                    break;
                                }

                                int id = nextCustomerId;
                                nextCustomerId++;

                                customerIds.Add(id);
                                customerNames.Add(id, name);
                                customerPhones.Add(id, phone);

                                Console.WriteLine($"Customer added successfully with ID {id}.");
                            }
                            break;
                        case 2:
                            {
                                Console.WriteLine("Search by: 1. Name, 2. Phone");
                                Console.Write("Choose an option: ");
                                int criteria = Convert.ToInt32(Console.ReadLine());

                                if (criteria != 1 && criteria != 2)
                                {
                                    Console.WriteLine("Invalid search option.");
                                    break;
                                }

                                Console.Write("Type the text to search: ");
                                string searchTerm = Console.ReadLine();
                                int foundCount = 0;

                                Console.WriteLine($"\n{"ID",-5}{"Name",-25}{"Phone",-15}");
                                Console.WriteLine("---------------------------------------------");

                                foreach (int id in customerIds)
                                {
                                    string fieldToCheck = criteria == 1 ? customerNames[id] : customerPhones[id];

                                    if (fieldToCheck.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Console.WriteLine($"{id,-5}{customerNames[id],-25}{customerPhones[id],-15}");
                                        foundCount++;
                                    }
                                }

                                if (foundCount == 0)
                                {
                                    Console.WriteLine("No customers found.");
                                }
                            }
                            break;
                        case 3:
                            {
                                Console.Write("Type the ID of the customer to edit: ");
                                int id = Convert.ToInt32(Console.ReadLine());

                                if (!customerNames.ContainsKey(id))
                                {
                                    Console.WriteLine("Customer not found!");
                                    break;
                                }

                                Console.Write($"New name for {customerNames[id]}: ");
                                string newName = Console.ReadLine();
                                Console.Write($"New phone (current: {customerPhones[id]}): ");
                                string newPhone = Console.ReadLine();

                                if (newName == "" || newPhone == "")
                                {
                                    Console.WriteLine("Name and phone cannot be empty. The customer was not updated.");
                                    break;
                                }

                                customerNames[id] = newName;
                                customerPhones[id] = newPhone;

                                Console.WriteLine("Customer updated successfully.");
                            }
                            break;
                        case 4:
                            {
                                Console.Write("Type the ID of the customer to delete: ");
                                int id = Convert.ToInt32(Console.ReadLine());

                                if (!customerNames.ContainsKey(id))
                                {
                                    Console.WriteLine("Customer not found!");
                                    break;
                                }

                                bool hasVehicles = false;
                                foreach (int vehicleId in vehicleIds)
                                {
                                    if (vehicleOwnerIds[vehicleId] == id)
                                    {
                                        hasVehicles = true;
                                    }
                                }

                                if (hasVehicles)
                                {
                                    Console.WriteLine("This customer has registered vehicles. Delete the vehicles first.");
                                    break;
                                }

                                Console.Write($"Are you sure you want to delete {customerNames[id]}? (yes/no): ");
                                string confirmation = Console.ReadLine().ToLower();

                                if (confirmation == "yes")
                                {
                                    customerIds.Remove(id);
                                    customerNames.Remove(id);
                                    customerPhones.Remove(id);
                                    Console.WriteLine("Customer deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Deletion cancelled.");
                                }
                            }
                            break;
                        case 5:
                            {
                                if (customerIds.Count == 0)
                                {
                                    Console.WriteLine("No customers registered.");
                                    break;
                                }

                                Console.WriteLine($"\n{"ID",-5}{"Name",-25}{"Phone",-15}");
                                Console.WriteLine("---------------------------------------------");

                                foreach (int id in customerIds)
                                {
                                    Console.WriteLine($"{id,-5}{customerNames[id],-25}{customerPhones[id],-15}");
                                }
                            }
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                break;
            case 2:
                {
                    Console.WriteLine("\n--- VEHICLES ---");
                    Console.WriteLine("1. Add Vehicle\n2. Search Vehicle\n3. Edit Vehicle\n4. Delete Vehicle\n5. List Vehicles\n6. Back");
                    Console.Write("Choose an option: ");
                    int vehicleOption = Convert.ToInt32(Console.ReadLine());

                    switch (vehicleOption)
                    {
                        case 1:
                            {
                                Console.Write("Type the ID of the owner (customer): ");
                                int ownerId = Convert.ToInt32(Console.ReadLine());

                                if (!customerNames.ContainsKey(ownerId))
                                {
                                    Console.WriteLine("Customer not found! Register the customer first.");
                                    break;
                                }

                                Console.Write("License plate: ");
                                string plate = Console.ReadLine();
                                Console.Write("Brand: ");
                                string brand = Console.ReadLine();
                                Console.Write("Model: ");
                                string model = Console.ReadLine();

                                if (plate == "" || brand == "" || model == "")
                                {
                                    Console.WriteLine("Plate, brand and model cannot be empty.");
                                    break;
                                }

                                bool plateExists = false;
                                foreach (int vehicleId in vehicleIds)
                                {
                                    if (vehiclePlates[vehicleId].ToLower() == plate.ToLower())
                                    {
                                        plateExists = true;
                                    }
                                }

                                if (plateExists)
                                {
                                    Console.WriteLine("There is already a vehicle with that license plate.");
                                    break;
                                }

                                int id = nextVehicleId;
                                nextVehicleId++;

                                vehicleIds.Add(id);
                                vehiclePlates.Add(id, plate);
                                vehicleBrands.Add(id, brand);
                                vehicleModels.Add(id, model);
                                vehicleOwnerIds.Add(id, ownerId);

                                Console.WriteLine($"Vehicle added successfully with ID {id}.");
                            }
                            break;
                        case 2:
                            {
                                Console.WriteLine("Search by: 1. License plate, 2. Brand, 3. Owner name");
                                Console.Write("Choose an option: ");
                                int criteria = Convert.ToInt32(Console.ReadLine());

                                if (criteria < 1 || criteria > 3)
                                {
                                    Console.WriteLine("Invalid search option.");
                                    break;
                                }

                                Console.Write("Type the text to search: ");
                                string searchTerm = Console.ReadLine();
                                int foundCount = 0;

                                Console.WriteLine($"\n{"ID",-5}{"Plate",-12}{"Brand",-15}{"Model",-15}{"Owner",-25}");
                                Console.WriteLine("------------------------------------------------------------------------");

                                foreach (int id in vehicleIds)
                                {
                                    string ownerName = customerNames[vehicleOwnerIds[id]];
                                    string fieldToCheck;

                                    if (criteria == 1)
                                    {
                                        fieldToCheck = vehiclePlates[id];
                                    }
                                    else if (criteria == 2)
                                    {
                                        fieldToCheck = vehicleBrands[id];
                                    }
                                    else
                                    {
                                        fieldToCheck = ownerName;
                                    }

                                    if (fieldToCheck.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Console.WriteLine($"{id,-5}{vehiclePlates[id],-12}{vehicleBrands[id],-15}{vehicleModels[id],-15}{ownerName,-25}");
                                        foundCount++;
                                    }
                                }

                                if (foundCount == 0)
                                {
                                    Console.WriteLine("No vehicles found.");
                                }
                            }
                            break;
                        case 3:
                            {
                                Console.Write("Type the ID of the vehicle to edit: ");
                                int id = Convert.ToInt32(Console.ReadLine());

                                if (!vehiclePlates.ContainsKey(id))
                                {
                                    Console.WriteLine("Vehicle not found!");
                                    break;
                                }

                                Console.Write($"New license plate (current: {vehiclePlates[id]}): ");
                                string newPlate = Console.ReadLine();
                                Console.Write($"New brand (current: {vehicleBrands[id]}): ");
                                string newBrand = Console.ReadLine();
                                Console.Write($"New model (current: {vehicleModels[id]}): ");
                                string newModel = Console.ReadLine();

                                if (newPlate == "" || newBrand == "" || newModel == "")
                                {
                                    Console.WriteLine("Plate, brand and model cannot be empty. The vehicle was not updated.");
                                    break;
                                }

                                bool plateExists = false;
                                foreach (int otherId in vehicleIds)
                                {
                                    if (otherId != id && vehiclePlates[otherId].ToLower() == newPlate.ToLower())
                                    {
                                        plateExists = true;
                                    }
                                }

                                if (plateExists)
                                {
                                    Console.WriteLine("Another vehicle already has that license plate.");
                                    break;
                                }

                                vehiclePlates[id] = newPlate;
                                vehicleBrands[id] = newBrand;
                                vehicleModels[id] = newModel;

                                Console.WriteLine("Vehicle updated successfully.");
                            }
                            break;
                        case 4:
                            {
                                Console.Write("Type the ID of the vehicle to delete: ");
                                int id = Convert.ToInt32(Console.ReadLine());

                                if (!vehiclePlates.ContainsKey(id))
                                {
                                    Console.WriteLine("Vehicle not found!");
                                    break;
                                }

                                bool hasAppointments = false;
                                foreach (int appointmentId in appointmentIds)
                                {
                                    if (appointmentVehicleIds[appointmentId] == id)
                                    {
                                        hasAppointments = true;
                                    }
                                }

                                if (hasAppointments)
                                {
                                    Console.WriteLine("This vehicle has appointments. Cancel the appointments first.");
                                    break;
                                }

                                Console.Write($"Are you sure you want to delete the vehicle {vehiclePlates[id]}? (yes/no): ");
                                string confirmation = Console.ReadLine().ToLower();

                                if (confirmation == "yes")
                                {
                                    vehicleIds.Remove(id);
                                    vehiclePlates.Remove(id);
                                    vehicleBrands.Remove(id);
                                    vehicleModels.Remove(id);
                                    vehicleOwnerIds.Remove(id);
                                    Console.WriteLine("Vehicle deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Deletion cancelled.");
                                }
                            }
                            break;
                        case 5:
                            {
                                if (vehicleIds.Count == 0)
                                {
                                    Console.WriteLine("No vehicles registered.");
                                    break;
                                }

                                Console.WriteLine($"\n{"ID",-5}{"Plate",-12}{"Brand",-15}{"Model",-15}{"Owner",-25}");
                                Console.WriteLine("------------------------------------------------------------------------");

                                foreach (int id in vehicleIds)
                                {
                                    string ownerName = customerNames[vehicleOwnerIds[id]];
                                    Console.WriteLine($"{id,-5}{vehiclePlates[id],-12}{vehicleBrands[id],-15}{vehicleModels[id],-15}{ownerName,-25}");
                                }
                            }
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                break;
            case 3:
                {
                    Console.WriteLine("\n--- APPOINTMENTS ---");
                    Console.WriteLine("1. Create Appointment\n2. Search Appointment\n3. Change Appointment Schedule\n4. Cancel Appointment\n5. List Appointments\n6. Back");
                    Console.Write("Choose an option: ");
                    int appointmentOption = Convert.ToInt32(Console.ReadLine());

                    switch (appointmentOption)
                    {
                        case 1:
                            {
                                Console.Write("Type the ID of the customer: ");
                                int customerId = Convert.ToInt32(Console.ReadLine());

                                if (!customerNames.ContainsKey(customerId))
                                {
                                    Console.WriteLine("Customer not found!");
                                    break;
                                }

                                Console.Write("Type the ID of the vehicle: ");
                                int vehicleId = Convert.ToInt32(Console.ReadLine());

                                if (!vehiclePlates.ContainsKey(vehicleId))
                                {
                                    Console.WriteLine("Vehicle not found!");
                                    break;
                                }

                                if (vehicleOwnerIds[vehicleId] != customerId)
                                {
                                    Console.WriteLine("That vehicle does not belong to this customer.");
                                    break;
                                }

                                Console.Write("Date (dd/mm/yyyy): ");
                                string date = Console.ReadLine();
                                Console.Write("Time (hh:mm): ");
                                string time = Console.ReadLine();
                                Console.Write("Service (e.g. oil change, brake check): ");
                                string service = Console.ReadLine();

                                if (date == "" || time == "" || service == "")
                                {
                                    Console.WriteLine("Date, time and service cannot be empty.");
                                    break;
                                }

                                bool scheduleTaken = false;
                                foreach (int appointmentId in appointmentIds)
                                {
                                    if (appointmentDates[appointmentId] == date && appointmentTimes[appointmentId] == time)
                                    {
                                        scheduleTaken = true;
                                    }
                                }

                                if (scheduleTaken)
                                {
                                    Console.WriteLine("There is already an appointment on that date and time.");
                                    break;
                                }

                                int id = nextAppointmentId;
                                nextAppointmentId++;

                                appointmentIds.Add(id);
                                appointmentCustomerIds.Add(id, customerId);
                                appointmentVehicleIds.Add(id, vehicleId);
                                appointmentDates.Add(id, date);
                                appointmentTimes.Add(id, time);
                                appointmentServices.Add(id, service);

                                Console.WriteLine($"Appointment created successfully with ID {id}.");
                            }
                            break;
                        case 2:
                            {
                                Console.WriteLine("Search by: 1. Customer name, 2. License plate, 3. Date");
                                Console.Write("Choose an option: ");
                                int criteria = Convert.ToInt32(Console.ReadLine());

                                if (criteria < 1 || criteria > 3)
                                {
                                    Console.WriteLine("Invalid search option.");
                                    break;
                                }

                                Console.Write("Type the text to search: ");
                                string searchTerm = Console.ReadLine();
                                int foundCount = 0;

                                Console.WriteLine($"\n{"ID",-5}{"Customer",-20}{"Plate",-12}{"Date",-13}{"Time",-8}{"Service",-25}");
                                Console.WriteLine("-----------------------------------------------------------------------------------");

                                foreach (int id in appointmentIds)
                                {
                                    string customerName = customerNames[appointmentCustomerIds[id]];
                                    string plate = vehiclePlates[appointmentVehicleIds[id]];
                                    string fieldToCheck;

                                    if (criteria == 1)
                                    {
                                        fieldToCheck = customerName;
                                    }
                                    else if (criteria == 2)
                                    {
                                        fieldToCheck = plate;
                                    }
                                    else
                                    {
                                        fieldToCheck = appointmentDates[id];
                                    }

                                    if (fieldToCheck.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                                    {
                                        Console.WriteLine($"{id,-5}{customerName,-20}{plate,-12}{appointmentDates[id],-13}{appointmentTimes[id],-8}{appointmentServices[id],-25}");
                                        foundCount++;
                                    }
                                }

                                if (foundCount == 0)
                                {
                                    Console.WriteLine("No appointments found.");
                                }
                            }
                            break;
                        case 3:
                            {
                                Console.Write("Type the ID of the appointment to change: ");
                                int id = Convert.ToInt32(Console.ReadLine());

                                if (!appointmentDates.ContainsKey(id))
                                {
                                    Console.WriteLine("Appointment not found!");
                                    break;
                                }

                                Console.WriteLine($"Current schedule: {appointmentDates[id]} at {appointmentTimes[id]}");
                                Console.Write("New date (dd/mm/yyyy): ");
                                string newDate = Console.ReadLine();
                                Console.Write("New time (hh:mm): ");
                                string newTime = Console.ReadLine();
                                Console.Write($"Service (current: {appointmentServices[id]}): ");
                                string newService = Console.ReadLine();

                                if (newDate == "" || newTime == "" || newService == "")
                                {
                                    Console.WriteLine("Date, time and service cannot be empty. The appointment was not updated.");
                                    break;
                                }

                                bool scheduleTaken = false;
                                foreach (int otherId in appointmentIds)
                                {
                                    if (otherId != id && appointmentDates[otherId] == newDate && appointmentTimes[otherId] == newTime)
                                    {
                                        scheduleTaken = true;
                                    }
                                }

                                if (scheduleTaken)
                                {
                                    Console.WriteLine("There is already another appointment on that date and time.");
                                    break;
                                }

                                appointmentDates[id] = newDate;
                                appointmentTimes[id] = newTime;
                                appointmentServices[id] = newService;

                                Console.WriteLine("Appointment updated successfully.");
                            }
                            break;
                        case 4:
                            {
                                Console.Write("Type the ID of the appointment to cancel: ");
                                int id = Convert.ToInt32(Console.ReadLine());

                                if (!appointmentDates.ContainsKey(id))
                                {
                                    Console.WriteLine("Appointment not found!");
                                    break;
                                }

                                Console.Write($"Cancel the appointment of {appointmentDates[id]} at {appointmentTimes[id]}? (yes/no): ");
                                string confirmation = Console.ReadLine().ToLower();

                                if (confirmation == "yes")
                                {
                                    appointmentIds.Remove(id);
                                    appointmentCustomerIds.Remove(id);
                                    appointmentVehicleIds.Remove(id);
                                    appointmentDates.Remove(id);
                                    appointmentTimes.Remove(id);
                                    appointmentServices.Remove(id);
                                    Console.WriteLine("Appointment cancelled successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("The appointment was not cancelled.");
                                }
                            }
                            break;
                        case 5:
                            {
                                if (appointmentIds.Count == 0)
                                {
                                    Console.WriteLine("No appointments registered.");
                                    break;
                                }

                                Console.WriteLine($"\n{"ID",-5}{"Customer",-20}{"Plate",-12}{"Date",-13}{"Time",-8}{"Service",-25}");
                                Console.WriteLine("-----------------------------------------------------------------------------------");

                                foreach (int id in appointmentIds)
                                {
                                    string customerName = customerNames[appointmentCustomerIds[id]];
                                    string plate = vehiclePlates[appointmentVehicleIds[id]];
                                    Console.WriteLine($"{id,-5}{customerName,-20}{plate,-12}{appointmentDates[id],-13}{appointmentTimes[id],-8}{appointmentServices[id],-25}");
                                }
                            }
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                break;
            case 4:
                running = false;
                break;
            default:
                Console.WriteLine("Invalid option. Please choose a number from 1 to 4.");
                break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Error: you must type a valid number.");
    }
    catch (OverflowException)
    {
        Console.WriteLine("Error: the number is too large.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }

    if (running)
    {
        string answer = "";

        while (answer != "yes" && answer != "no")
        {
            Console.Write("\nDo you want to perform another operation? (yes/no): ");
            answer = Console.ReadLine().ToLower();

            if (answer != "yes" && answer != "no")
            {
                Console.WriteLine("Please type 'yes' or 'no'.");
            }
        }

        running = answer == "yes" ? true : false;
    }
}

Console.WriteLine("\nClosing the app. Goodbye!");
