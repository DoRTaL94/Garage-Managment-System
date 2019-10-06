using System;
using System.Collections.Generic;
using GarageLogic;
using ConsoleUtils;

namespace ConsoleUI
{
     public class MainMenu
     {
          private const int m_MaxInputLength = 25;
          private const int m_PhoneNumberLength = 10;
          private const int m_RegistrationNumberLength = 7;
          private const int m_MinInputLength = 1;
          private Garage m_Garage = new Garage();
          const int newVehicle = 1;
          const int displayVehicleByReg = 2;
          const int changeVehicleStatus = 3;
          const int inflateWheelsAirPressureToMax = 4;
          const int refuelGasVehicle = 5;
          const int rechargeElectricVehicle = 6;
          const int showFullVehicleInfo = 7;
          const string exit = "E";

          public bool HandleUserInput()
          {
               string sUserChoice = string.Empty;
               int iUserChoice = 0;
               bool isStopProgram = false;
               
               try
               {
                    sUserChoice = InputManager.GetUserChoice(string.Empty, newVehicle, displayVehicleByReg, changeVehicleStatus, inflateWheelsAirPressureToMax, refuelGasVehicle, rechargeElectricVehicle, showFullVehicleInfo, exit);

                    if (sUserChoice != exit)
                    {
                         iUserChoice = int.Parse(sUserChoice);
                         Console.Clear();

                         switch (iUserChoice)
                         {
                              case newVehicle:
                                   printNewVehicleDialog();
                                   break;

                              case displayVehicleByReg:
                                   registrationNumberDisplayMenu();
                                   break;

                              case changeVehicleStatus:
                                   changeVehicleStatusMenu();
                                   break;

                              case inflateWheelsAirPressureToMax:
                                   inflateWheelsAirPressureToMaxMenu();
                                   break;

                              case refuelGasVehicle:
                                   fuelGasVehicleMenu();
                                   break;

                              case rechargeElectricVehicle:
                                   chargeElectricVehicleMenu();
                                   break;

                              case showFullVehicleInfo:
                                   showFullVehicleInfoMenu();
                                   break;
                         }
                    }
                    else if(sUserChoice == exit)
                    {
                         isStopProgram = true;
                    }
               }
               catch(FormatException)
               {
                    Console.WriteLine("Error: Input format does not match the format type required.{0}", Environment.NewLine);
               }
               catch (Exception i_Ex)
               {
                    if(i_Ex.Message == "Invalid data input")
                    {
                         Console.WriteLine("Error: {0}.{1}", i_Ex.Message, Environment.NewLine);
                    }
                    else
                    {
                         Console.WriteLine("Error: Unknown.{0}", Environment.NewLine);
                    }
               }

               if(isStopProgram == false)
               {
                    isStopProgram = askIfReturnToMainMenu();
               }

               return isStopProgram;
          }

          public void Show()
          {
               Console.Write(@"Main menu:
1. Enter new vehicle.
2. Display registration numbers of all vehicles in the garage.
3. Change vehicle status.
4. Inflate vehicle wheels air pressure to maximum.
5. Refuel a gas vehicle.
6. Recharge a electric vehicle.
7. Display a vehicle data.
Enter your choice or press 'E' to exit:
>> ");
          }

          private void showFullVehicleInfoMenu()
          {
               try
               {
                    string regNum = getExistingRegistrationNumberFromUser();
                    Vehicle vehicle = m_Garage.GetVehicleByRegNum(regNum);
                    Dictionary<string, string> vehicleInfo = m_Garage.GetVehicleInfo(ref vehicle);

                    foreach (KeyValuePair<string, string> pair in vehicleInfo)
                    {
                         Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                    }

                    Console.WriteLine(string.Empty);
               }
               catch (ArgumentException i_ArgumentException)
               {
                    if (i_ArgumentException.Message == "Vehicle_Not_Exist")
                    {
                         Console.WriteLine("There is no vehicle in the garage with the entered registration number.");
                    }
               }
               catch (Exception i_Ex)
               {
                    if (i_Ex.Message == "Invalid data input")
                    {
                         Console.WriteLine("Error: {0}.{1}", i_Ex.Message, Environment.NewLine);
                    }
                    else
                    {
                         Console.WriteLine("Error: Unknown.{0}", Environment.NewLine);
                    }
               }
          }

          private bool askIfReturnToMainMenu()
          {
               bool isStopProgram = false;
               string userInput = string.Empty;
               bool isValidInput = false;

               while(isValidInput == false)
               {
                    Console.Write(@"Do you want to return to the main menu? Please enter Y/N.
>> ");

                    userInput = Console.ReadLine();

                    if(userInput == "Y")
                    {
                         isStopProgram = false;
                         isValidInput = true;
                         Console.Clear();
                    }
                    else if(userInput == "N")
                    {
                         isStopProgram = true;
                         isValidInput = true;
                         Console.Clear();
                         Console.WriteLine("Goodbye!");
                    }
                    else
                    {
                         Console.Clear();
                         Console.WriteLine("Error: Invalid input.");
                    }
               }

               return isStopProgram;
          }

          private void registrationNumberDisplayMenu()
          {
               try
               {
                    bool isFilter = false;
                    bool isFilterByInRepair = false;
                    bool isFilterByFixed = false;
                    bool isFilterByPaid = false;

                    string userChoice = string.Empty;
                    int registrationNumbersListCount = 0;

                    isFilter = getYesOrNoFromUser("Would you like to filter out vehicles?  Enter Y/N");

                    if (isFilter == true)
                    {
                         isFilterByInRepair = getYesOrNoFromUser("Would you like to filter by In Repair status? (Y/N)");
                         isFilterByFixed = getYesOrNoFromUser("Would you like to filter by Fixed status? (Y/N)");
                         isFilterByPaid = getYesOrNoFromUser("Would you like to filter by Paid status? (Y/N)");
                    }

                    List<string> RegNumberList = m_Garage.GetRegistrationNumbersList(isFilterByInRepair, isFilterByFixed, isFilterByPaid);

                    if (RegNumberList.Count != 0)
                    {
                         Console.WriteLine("Displaying registration numbers: ");

                         foreach (string registrationNum in RegNumberList)
                         {
                              registrationNumbersListCount++;
                              Console.WriteLine("Vehicle No.{0}: {1}", registrationNumbersListCount, registrationNum);
                         }
                    }
                    else
                    {
                         Console.WriteLine("Error: Couldn't find any vehicle. Please try again or enter a new vehicle.");
                    }
               }
               catch (FormatException)
               {
                    Console.WriteLine("Error: Input format does not match the format type required.{0}", Environment.NewLine);
               }
               catch (Exception i_Ex)
               {
                    if (i_Ex.Message == "Invalid data input")
                    {
                         Console.WriteLine("Error: {0}.{1}", i_Ex.Message, Environment.NewLine);
                    }
                    else
                    {
                         Console.WriteLine("Error: Unknown.{0}", Environment.NewLine);
                    }
               }
          }

          private string getExistingRegistrationNumberFromUser()
          {
               string userInput = string.Empty;

               Console.Write(@"Please enter registration number (7 characters, 0-9, 'A'-'Z'):
>> ");
               userInput = Console.ReadLine();

               if (StringManager.IsOnlyDigitsAndCapLetters(userInput) == true && userInput.Length == 7)
               {
                    if (m_Garage.IsVehicleExists(userInput) == false)
                    {
                         throw new ArgumentException("Vehicle_Not_Exist");
                    }
               }
               else
               {
                    throw new Exception("Invalid data input");
               }

               return userInput;
          }

          private void fuelGasVehicleMenu()
          {
               try
               {
                    string registrationNumber = string.Empty;
                    float fuelToAddAmount = 0;

                    registrationNumber = getGasCarToFuelRegNum();
                    fuelToAddAmount = getFuelAmountFromUser();

                    m_Garage.RefillVehicle(registrationNumber, fuelToAddAmount);

                    Console.WriteLine("Refueling was done successfully.");
               }
               catch (ArgumentException i_ArgumentException)
               {
                    if (i_ArgumentException.Message == "Invalid_Fuel_Type")
                    {
                         Console.WriteLine("Error: the fuel type inserted does not match the vehicle.");
                    }
                    else if (i_ArgumentException.Message == "Not_Fuelable_Vehicle")
                    {
                         Console.WriteLine("Error: Vehicle is elctric therefore not fuelable.");
                    }
                    else if (i_ArgumentException.Message == "Vehicle_Not_Exist")
                    {
                         Console.WriteLine("There is no vehicle in the garage with the entered registaration number.");
                    }
               }
               catch (FormatException)
               {
                    Console.WriteLine("Error: Input format does not match the format type required.{0}", Environment.NewLine);
               }
               catch (ValueOutOfRangeException i_ValueOutOfRangeException)
               {
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
               }
          }

          private void chargeElectricVehicleMenu()
          {
               try
               {
                    string registrationNumber = string.Empty;
                    float numOfHoursToCharge = 0;

                    registrationNumber = getElectricVehicleByReg();
                    numOfHoursToCharge = getHoursToChargeFromUser();

                    m_Garage.RefillVehicle(registrationNumber, numOfHoursToCharge);

                    Console.WriteLine("Charging was done successfully.");
               }
               catch (ValueOutOfRangeException i_ValueOutOfRangeException)
               {
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
               }
               catch (ArgumentException i_ArgumentException)
               {
                    if (i_ArgumentException.Message == "Not electric vehicle.")
                    {
                         Console.WriteLine("Error: {0}.", i_ArgumentException.Message);
                    }
                    else if (i_ArgumentException.Message == "Vehicle_Not_Exist")
                    {
                         Console.WriteLine("There is no vehicle in the garage with the entered registaration number.");
                    }
               }
               catch (FormatException)
               {
                    Console.WriteLine("Error: Input format does not match the format type required.{0}", Environment.NewLine);
               }
               catch (Exception i_Ex)
               {
                    if (i_Ex.Message == "Invalid data input")
                    {
                         Console.WriteLine("Error: {0}.{1}", i_Ex.Message, Environment.NewLine);
                    }
                    else
                    {
                         Console.WriteLine("Error: Unknown.{0}", Environment.NewLine);
                    }
               }
          }

          private float getHoursToChargeFromUser()
          {
               string userInput = string.Empty;
               float numOfHoursToCharge = 0;

               Console.Write(@"Please enter amount of battery hours to add to the vehcile
>> ");
               userInput = Console.ReadLine();
               numOfHoursToCharge = float.Parse(userInput);

               return numOfHoursToCharge;
          }

          private float getFuelAmountFromUser()
          {
               string userInput = string.Empty;
               float fuelToAddAmount = 0;

               Console.Write(@"Please enter amount of fuel to add to the car:
>> ");
               userInput = Console.ReadLine();
               fuelToAddAmount = float.Parse(userInput);
               
               return fuelToAddAmount;
          }

          private string getElectricVehicleByReg()
          {
               string registrationNumber = getExistingRegistrationNumberFromUser();

               if (m_Garage.CheckIfElectricVehicle(registrationNumber) == false)
               {
                    throw new ArgumentException("Not electric vehicle.");
               }

               return registrationNumber;
          }

          private string getGasCarToFuelRegNum()
          {
               string registrationNumber = string.Empty;
               eFuelType fuelType = eFuelType.None;

               registrationNumber = getExistingRegistrationNumberFromUser();

               if (m_Garage.CheckIfFueableCar(registrationNumber) == true)
               {
                    fuelType = getFuelTypeInput();

                    if (m_Garage.CheckIfValidFuelType(registrationNumber, fuelType) == false)
                    {
                         throw new ArgumentException("Invalid_Fuel_Type");
                    }
               }
               else
               {
                    throw new ArgumentException("Not_Fuelable_Vehicle");
               }

               return registrationNumber;
          }

          private eFuelType getFuelTypeInput()
          {
               string userChoice = string.Empty;
               eFuelType fuelType = eFuelType.None;
               const int octan95 = 1;
               const int octan96 = 2;
               const int octan98 = 3;
               const int soler = 4;

               try
               {
userChoice = InputManager.GetUserChoice(
@"Choose fuel type:
1. Octan 95
2. Octan 96
3. Octan 98
4. Soler
Please Enter one of the above:
>> ",
1,
2,
3,
4);

                    switch (int.Parse(userChoice))
                    {
                         case octan95:
                              fuelType = eFuelType.Octan95;
                              break;
                         case octan96:
                              fuelType = eFuelType.Octan96;
                              break;
                         case octan98:
                              fuelType = eFuelType.Octan98;
                              break;
                         case soler:
                              fuelType = eFuelType.Octan95;
                              break;
                         default:
                              break;
                    }
               }
               catch(Exception i_Ex)
               {
                    if (i_Ex.Message == "Invalid data input")
                    {
                         throw new Exception("Invalid data input");
                    }
                    else
                    {
                         throw new ValueOutOfRangeException("User choice", 1, 4, i_Ex);
                    }
               }

               return fuelType;
          }

          private bool getYesOrNoFromUser(string i_QuestionStringToPrint)
          {
               string userInput = string.Empty;
               char cUserInput = ' ';

               Console.Write(i_QuestionStringToPrint + @"
>> ");
               userInput = Console.ReadLine();
               cUserInput = char.Parse(userInput);

               if (cUserInput != 'Y' && cUserInput != 'N')
               {
                    throw new Exception("Invalid data input");
               }

               Console.Clear();

               return userInput == "Y";
          }

          private void inflateWheelsAirPressureToMaxMenu()
          {
               try
               {
                    string registrationNumber = getExistingRegistrationNumberFromUser();
                    m_Garage.InflateWheelsAirPressure(registrationNumber);
                    Console.WriteLine("Inflate air pressure was operated successfully.");
               }
               catch (ArgumentException i_ArgumentException)
               {
                    if (i_ArgumentException.Message == "Vehicle_Not_Exist")
                    {
                         Console.WriteLine("There is no vehicle in the garage with the entered registaration number.");
                    }
               }
               catch (Exception i_Ex)
               {
                    if (i_Ex.Message == "Invalid data input")
                    {
                         Console.WriteLine("Error: {0}.{1}", i_Ex.Message, Environment.NewLine);
                    }
                    else
                    {
                         Console.WriteLine("Error: Unknown.{0}", Environment.NewLine);
                    }
               }
          }

          private void changeVehicleStatusMenu()
          {
               try
               {
                    string userChoice;
                    string registrationNumber = getExistingRegistrationNumberFromUser();

                    {
                         userChoice = InputManager.GetUserChoice(
@"Choose new status:
1. In repair.
2. Fixed.
3. Paid.
Please Enter one of the above:
>> ",
1,
2,
3);
                    }

                    Console.Clear();
                    m_Garage.ChangeVehicleStatus(registrationNumber, int.Parse(userChoice));
                    Console.WriteLine("Changing vehicle status was operated successfully.");
               }
               catch (ArgumentException i_ArgumentException)
               {
                    if (i_ArgumentException.Message == "Vehicle_Not_Exist")
                    {
                         Console.WriteLine("There is no vehicle in the garage with the entered registaration number.");
                    }
               }
               catch (FormatException)
               {
                    Console.WriteLine("Error: Input format does not match the format type required.{0}", Environment.NewLine);
               }
               catch (Exception i_Ex)
               {
                    if (i_Ex.Message == "Invalid data input")
                    {
                         Console.WriteLine("Error: {0}.{1}", i_Ex.Message, Environment.NewLine);
                    }
                    else
                    {
                         Console.WriteLine("Error: Unknown.{0}", Environment.NewLine);
                    }
               }
          }

          private void printNewVehicleDialog()
          {
               object newVehicle = new object();

               try
               {
                    string registrationNumber = string.Empty;
                    string userName = string.Empty;
                    string userPhoneNumber = string.Empty;
                    string model = string.Empty;
                    string vehicleType = string.Empty;
                    string wheelsManufacture = string.Empty;
                    int wheelsAirPressure = 0;
                    bool isElectric = false;

                    registrationNumber = getRegistrationNumber();
                    Console.Clear();
                    m_Garage.AddVehicleInfo("i_RegistrationNumber", registrationNumber);

                    userName = getUserName();
                    Console.Clear();
                    m_Garage.AddVehicleInfo("i_OwnerName", userName);

                    userPhoneNumber = getUserPhoneNumber();
                    Console.Clear();
                    m_Garage.AddVehicleInfo("i_OwnerPhoneNumber", userPhoneNumber);

                    vehicleType = getVehicleType();
                    Console.Clear();
                    m_Garage.AddVehicleInfo("Vehicle Type", vehicleType);

                    model = getVehicleModel();
                    Console.Clear();
                    m_Garage.AddVehicleInfo("i_Model", model);

                    isElectric = askIfElectricVehicle();
                    m_Garage.AddVehicleInfo("i_IsEnergySource", isElectric);
                    Console.Clear();

                    Vehicle vehicle = m_Garage.EnterNewVehicle();
                    newVehicle = vehicle;

                    wheelsManufacture = getWheelsManufacture();
                    Console.Clear();
                    m_Garage.AddVehicleInfo("i_WheelsManufacture", wheelsManufacture);

                    wheelsAirPressure = getWheelsAirPressure(ref vehicle);
                    Console.Clear();
                    m_Garage.AddVehicleInfo("i_WheelsCurrentAirPressure", wheelsAirPressure);

                    printSpecificVehicleDialog(ref vehicle);
                    m_Garage.UpdateVehicleData(ref vehicle);
               }
               catch (ValueOutOfRangeException i_ValueOutOfRangeException)
               {
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
                    m_Garage.RemoveVehicle(ref newVehicle);
               }
               catch (ArgumentException i_ArgumentException)
               {
                    switch (i_ArgumentException.Message)
                    {
                         case "Registration_Number_Exists":
                              Console.WriteLine("Error: Vehicle is already in the garage. Changing vehicle status to 'In Repair'.");
                              break;
                         case "User choice":
                              Console.WriteLine("Error: Your option selection does not exists.");
                              break;
                         default:
                              Console.WriteLine(i_ArgumentException.Message);
                              break;
                    }
               }
               catch (FormatException)
               {
                    Console.WriteLine("Error: Input format does not match the format type required.{0}", Environment.NewLine);
               }
               catch (Exception i_Ex)
               {
                    if (i_Ex.Message == "Invalid data input")
                    {
                         Console.WriteLine("Error: {0}.{1}", i_Ex.Message, Environment.NewLine);
                    }
                    else
                    {
                         Console.WriteLine(i_Ex.Message);
                    }
               }
               finally
               {
                    m_Garage.ClearVehicleInfo();
               }
          }

          private int getWheelsAirPressure(ref Vehicle io_Vehicle)
          {
               string userInput = string.Empty;
               int airPressure = 0;
               const float WheelsMinAirPressure = 0;

               Console.Write(
@"Please enter your vehicle current wheels air pressure: (Max air pressure is {0})
>> ",
io_Vehicle.Wheels[0].MaxAirPressure);
               userInput = Console.ReadLine();
               airPressure = int.Parse(userInput);

               if(airPressure > io_Vehicle.Wheels[0].MaxAirPressure)
               {
                    throw new ValueOutOfRangeException("Wheels air pressure", WheelsMinAirPressure, io_Vehicle.Wheels[0].MaxAirPressure);
               }

               return airPressure;
          }

          private string getWheelsManufacture()
          {
               string userInput = string.Empty;
               Console.Write(@"What is your vehicle wheels manufacture? (Name up to 25 characters)
>> ");
               userInput = Console.ReadLine();

               if (StringManager.IsOnlyCharacters(userInput) == true)
               {
                    if (StringManager.CheckStringRange(userInput, m_MaxInputLength) == false)
                    {
                         throw new ValueOutOfRangeException("Input", m_MinInputLength, m_MaxInputLength);
                    }
               }
               else
               {
                    throw new FormatException("Wheels manufacture");
               }

               return userInput;
          }

          private void printSpecificVehicleDialog(ref Vehicle io_Vehicle)
          {
               string typeName = io_Vehicle.GetType().Name;
               bool isElectric = io_Vehicle.EnergySource is Battery;

               switch (typeName)
               {
                    case "Car":
                         printEnergySourceDialog(ref io_Vehicle, isElectric);
                         Console.Clear();
                         printCarDialog();
                         break;
                    case "Motorcycle":
                         printEnergySourceDialog(ref io_Vehicle, isElectric);
                         Console.Clear();
                         printMotorcycleDialog();
                         break;
                    case "Truck":
                         printEnergySourceDialog(ref io_Vehicle, isElectric);
                         Console.Clear();
                         printTruckDialog();
                         break;
               }
          }

          private void printEnergySourceDialog(ref Vehicle io_Vehicle, bool i_IsElectric)
          {
               if(i_IsElectric == true)
               {
                    Battery battery = io_Vehicle.EnergySource as Battery;

                    Console.Write(
@"What is your vehicle current battery time in hours? (Max battry time is {0})
>> ",
battery.MaxEnergyTime);

                    string sBatteryTime = Console.ReadLine();
                    float fBatteryTime = float.Parse(sBatteryTime);

                    if (fBatteryTime > battery.MaxEnergyTime)
                    {
                         throw new ValueOutOfRangeException("Battery time", 0, battery.MaxEnergyTime);
                    }
                    else
                    {
                         m_Garage.AddVehicleInfo("Current Battery Time", fBatteryTime);
                    }
               }
               else
               {
                    FuelTank fuelTank = io_Vehicle.EnergySource as FuelTank;
                    Console.Write(
@"What is your vehicle current fuel amount in liters? (Max fuel tank capacity is: {0})
>> ",
fuelTank.MaxFuelAmount);
                    string sFuelAmount = Console.ReadLine();
                    float fFuelAmount = float.Parse(sFuelAmount);

                    if(fFuelAmount > fuelTank.MaxFuelAmount)
                    {
                         throw new ValueOutOfRangeException("Fuel tank", 0, fuelTank.MaxFuelAmount);
                    }
                    else
                    {
                         m_Garage.AddVehicleInfo("Current Fuel Amount", fFuelAmount);
                    }
               }
          }

          private void printTruckDialog()
          {
               string sUserChoice = string.Empty;
               string userInput = string.Empty;
               int cargoVolume = 0;
               int iUserChoice = 0;
               const int yes = 1;
               const int no = 2;

               Console.Write(@"Is there any hazardous materials in your truck?
1. yes.
2. no.
Please Enter one of the above:
>> ");
               sUserChoice = Console.ReadLine();
               iUserChoice = int.Parse(sUserChoice);

               switch (iUserChoice)
               {
                    case 1:
                         m_Garage.AddVehicleInfo("m_IsHazardousMaterial", true);
                         break;
                    case 2:
                         m_Garage.AddVehicleInfo("m_IsHazardousMaterial", false);
                         break;
                    default:
                         throw new ValueOutOfRangeException("User choice", yes, no);
               }

               Console.Clear();
               Console.Write(@"What is the truck's cargo volume?
>> ");
               userInput = Console.ReadLine();

               if (StringManager.ConvertToInt(userInput, out cargoVolume) == true)
               {
                    m_Garage.AddVehicleInfo("m_CargoVolume", cargoVolume);
               }
               else
               {
                    throw new FormatException("Cargo volume");
               }

               Console.Clear();
          }

          private void printMotorcycleDialog()
          {
               string userInput = string.Empty;
               int engineCapacity = 0;
               eRegistrationType regType = eRegistrationType.None;

               Console.Write(@"What is your motorcycle registration type? (A,A1,A2,B)
>> ");
               userInput = Console.ReadLine();

               if (m_Garage.CheckRegistrationType(userInput, out regType) == true)
               {
                    m_Garage.AddVehicleInfo("m_RegistrationType", regType);
               }
               else
               {
                    throw new ArgumentException("Registration type");
               }

               Console.Clear();

               Console.Write(@"what is your motorcycle engine capacity?
>> ");
               userInput = Console.ReadLine();

               if (m_Garage.CheckEngineCapacity(userInput, ref engineCapacity) == true)
               {
                    m_Garage.AddVehicleInfo("m_EngineCapacity", engineCapacity);
               }
               else
               {
                    Console.WriteLine("Error: Invaild input. Please try again.{0}", Environment.NewLine);
               }

               Console.Clear();
          }

          private void printCarDialog()
          {
               string userInput = string.Empty;
               eVehicleColor carColor = eVehicleColor.None;
               int carDoorsAmount = 0;
               const int minDoorsAmount = 2;
               const int maxDoorsAmount = 5;

               Console.Write(@"How many doors your car has? (2/3/4/5)
>> ");
               userInput = Console.ReadLine();

               if (m_Garage.CheckCarDoorsAmount(userInput, out carDoorsAmount) == false)
               {
                    throw new ValueOutOfRangeException("Doors amount", minDoorsAmount, maxDoorsAmount);
               }

               m_Garage.AddVehicleInfo("m_DoorsCount", carDoorsAmount);
               Console.Clear();

               Console.Write(@"What is Your car color? (Red/Blue/Black/Grey)
>> ");
               userInput = Console.ReadLine();
               
               if (m_Garage.CheckCarColor(userInput, out carColor) == true)
               {
                    m_Garage.AddVehicleInfo("m_Color", carColor);
               }
               else
               {
                    throw new ArgumentException("Car color");
               }

               Console.Clear();
          }

          private string getVehicleModel()
          {
               string userInput = string.Empty;

               Console.Write(@"What is your vehicle model? (Name length up to 25 characters)
>> ");
               userInput = Console.ReadLine();

               if (StringManager.CheckStringRange(userInput, m_MaxInputLength) == false)
               {
                    throw new ValueOutOfRangeException("Input length", m_MinInputLength, m_MaxInputLength);
               }

               return userInput;
          }

          private bool askIfElectricVehicle()
          {
               bool isElectricVehicle = false;
               bool isValidInput = false;
               string sUserChoice = string.Empty;
               int iUserChoice = 0;
               const int electric = 1;
               const int gas = 2;

               while (isValidInput == false)
               {
                    Console.Write(@"What is your vehicle engine type?
1. Electric.
2. Gas.
Please Enter one of the above:
>> ");
                    sUserChoice = Console.ReadLine();
                    iUserChoice = int.Parse(sUserChoice);

                    switch (iUserChoice)
                    {
                         case 1:
                              isElectricVehicle = true;
                              isValidInput = true;
                              break;
                         case 2:
                              isElectricVehicle = false;
                              isValidInput = true;
                              break;
                         default:
                              throw new ValueOutOfRangeException("User choice", electric, gas);
                    }
               }

               return isElectricVehicle;
          }

          private string getVehicleType()
          {
               string sUserChoice = string.Empty;
               int iUserChoice = 0;
               string vehicleType = string.Empty;
               const int car = 1;
               const int motorcycle = 2;
               const int truck = 3;

               Console.Write(@"What is your vehicle type?
1. Car.
2. Motorcycle.
3. Truck.
Please Enter one of the above:
>> ");
               sUserChoice = Console.ReadLine();
               iUserChoice = int.Parse(sUserChoice);

               switch (iUserChoice)
               {
                    case car:
                         vehicleType = "Car";
                         break;
                    case motorcycle:
                         vehicleType = "Motorcycle";
                         break;
                    case truck:
                         vehicleType = "Truck";
                         break;
                    default:
                         throw new ValueOutOfRangeException("User choice", 1, 3);
               }

               return vehicleType;
          }

          private string getUserName()
          {
               string userName = string.Empty;

               Console.Write(@"Please enter your name ('a'-'z' and 'A'-'Z', Name length up to 25 characters):
>> ");
               userName = Console.ReadLine();

               if ((StringManager.IsOnlyCharacters(userName) == true && StringManager.CheckStringRange(userName, m_MaxInputLength) == false) || StringManager.IsOnlyCharacters(userName) == false)
               {
                    throw new FormatException("Input");
               }

               return userName;
          }

          private string getUserPhoneNumber()
          {
               string sPhoneNumber = string.Empty;
               int iPhoneNumber = 0;

               Console.Write(@"Please enter your phone number (10 digits, 0-9):
>> ");
               sPhoneNumber = Console.ReadLine();

               if (StringManager.IsValidLength(sPhoneNumber, m_PhoneNumberLength) == false)
               {
                    throw new ValueOutOfRangeException("Phone number length", m_PhoneNumberLength, m_PhoneNumberLength);
               }

               iPhoneNumber = int.Parse(sPhoneNumber);

               return sPhoneNumber;
          }

          private string getRegistrationNumber()
          {
               string registrationNumber = string.Empty;

               Console.Write(@"Please enter registration number (7 characters, 0-9, 'A'-'Z'):
>> ");
               registrationNumber = Console.ReadLine();

               if (StringManager.IsOnlyDigitsAndCapLetters(registrationNumber) == true && registrationNumber.Length == 7)
               {
                    if (m_Garage.IsVehicleExists(registrationNumber) == true)
                    {
                         m_Garage.GetVehicleByRegNum(registrationNumber).VehicleStatus = eVehicleStatus.InRepair;
                         throw new ArgumentException("Registration_Number_Exists");
                    }
               }
               else
               {
                    throw new ValueOutOfRangeException("Registration number", m_RegistrationNumberLength, m_RegistrationNumberLength);
               }

               return registrationNumber;
          }

          private bool isValidRegistrationNumber(string i_RegistrationNumber)
          {
               return i_RegistrationNumber.Length == m_RegistrationNumberLength;
          }
     }
}
