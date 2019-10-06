using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using ConsoleUtils;

namespace GarageLogic
{
     public class Garage
     {
          private VehicleCreator m_VehicleCreator = new VehicleCreator();
          private Dictionary<Vehicle, KeyValuePair<string, string>> m_CostumerBook = new Dictionary<Vehicle, KeyValuePair<string, string>>();
          private List<Vehicle> m_Vehicles = new List<Vehicle>();

          public void AddVehicleInfo(string i_InfoDescription, object i_Info)
          {
               m_VehicleCreator.AddVehicleInfo(i_InfoDescription, i_Info);
          }

          private int getVehicleIndexByRegNum(string i_RegNum)
          {
               int index = -1;

               for(int i = 0; i < m_Vehicles.Count; i++)
               {
                    if(m_Vehicles[i].RegistrationNumber == i_RegNum)
                    {
                         index = i;
                         break;
                    }
               }

               return index;
          }

          public Vehicle GetVehicleByRegNum(string i_RegNum)
          {
               int vehicleIndex = getVehicleIndexByRegNum(i_RegNum);

               if(vehicleIndex >= 0)
               {
                    return m_Vehicles[vehicleIndex];
               }

               throw new Exception("System Error: Vehicle does not exists in collection.");
          }

          public Dictionary<string, string> GetVehicleInfo(ref Vehicle io_Vehicle)
          {
               Dictionary<string, string> vehicleInfo = new Dictionary<string, string>();
               PropertyInfo[] VehicleProperties = io_Vehicle.GetType().GetProperties();

               vehicleInfo.Add("Owner Name", m_CostumerBook[io_Vehicle].Key);
               vehicleInfo.Add("Owner Phone Number", m_CostumerBook[io_Vehicle].Value);
               vehicleInfo.Add("Vehicle Type", io_Vehicle.GetType().Name);

               Dictionary<string, string> tmpObjInfo = GetObjectInfo(io_Vehicle);
               mergeDictionaries(ref vehicleInfo, ref tmpObjInfo);

               return vehicleInfo;
          }

          public Dictionary<string, string> GetObjectInfo(object i_Obj)
          {
               Dictionary<string, string> objInfo = new Dictionary<string, string>();

               try
               {
                    PropertyInfo[] objectProperties = i_Obj.GetType().GetProperties();
                    string propertyName = string.Empty;

                    foreach (PropertyInfo vehicleProperty in objectProperties)
                    {
                         if (vehicleProperty.GetValue(i_Obj).GetType().IsConstructedGenericType == true)
                         {
                              IList tmpArray = (IList)vehicleProperty.GetValue(i_Obj);

                              propertyName = StringManager.AddSpaceBetweenWordsWithCapLetters(vehicleProperty.Name);
                              objInfo.Add(propertyName, tmpArray.Count.ToString());
                              Dictionary<string, string> tmpObjInfo = GetObjectInfo(tmpArray[0]);
                              mergeDictionaries(ref objInfo, ref tmpObjInfo);
                         }
                         else if (vehicleProperty.GetValue(i_Obj).GetType().IsClass == true &&
                              vehicleProperty.GetValue(i_Obj).GetType() != typeof(string))
                         {
                              propertyName = StringManager.AddSpaceBetweenWordsWithCapLetters(vehicleProperty.Name);
                              objInfo.Add(propertyName, vehicleProperty.GetValue(i_Obj).ToString());
                              Dictionary<string, string> tmpObjInfo = GetObjectInfo(vehicleProperty.GetValue(i_Obj));
                              mergeDictionaries(ref objInfo, ref tmpObjInfo);
                         }
                         else
                         {
                              propertyName = StringManager.AddSpaceBetweenWordsWithCapLetters(vehicleProperty.Name);
                              objInfo.Add(propertyName, vehicleProperty.GetValue(i_Obj).ToString());
                         }
                    }
               }
               catch(AccessViolationException i_AccessViolationException)
               {
                    throw new Exception("System Error: IList is not allocated in the given index.", i_AccessViolationException);
               }

               return objInfo;
          }

          private void mergeDictionaries(ref Dictionary<string, string> io_Dst, ref Dictionary<string, string> io_Src)
          {
               foreach(KeyValuePair<string, string> pair in io_Src)
               {
                    io_Dst.Add(pair.Key, pair.Value);
               }
          }

          public bool CheckCarColor(string i_CarColor, out eVehicleColor o_eCarColor)
          {
               bool isValidCarColor = false;

               switch (i_CarColor)
               {
                    case "Red":
                         o_eCarColor = eVehicleColor.Red;
                         isValidCarColor = true;
                         break;
                    case "Blue":
                         o_eCarColor = eVehicleColor.Black;
                         isValidCarColor = true;
                         break;
                    case "Grey":
                         o_eCarColor = eVehicleColor.Blue;
                         isValidCarColor = true;
                         break;
                    case "Black":
                         o_eCarColor = eVehicleColor.Grey;
                         isValidCarColor = true;
                         break;
                    default:
                         o_eCarColor = eVehicleColor.None;
                         break;
               }

               return isValidCarColor;
          }

          public bool CheckRegistrationType(string i_RegType, out eRegistrationType o_eRegType)
          {
               bool isValidRegType = false;

               switch (i_RegType)
               {
                    case "A":
                         o_eRegType = eRegistrationType.A;
                         isValidRegType = true;
                         break;
                    case "A1":
                         o_eRegType = eRegistrationType.A1;
                         isValidRegType = true;
                         break;
                    case "A2":
                         o_eRegType = eRegistrationType.A2;
                         isValidRegType = true;
                         break;
                    case "B":
                         o_eRegType = eRegistrationType.B;
                         isValidRegType = true;
                         break;
                    default:
                         o_eRegType = eRegistrationType.None;
                         isValidRegType = false;
                         break;
               }

               return isValidRegType;
          }

          public List<string> GetRegistrationNumbersList(bool i_IsFilterByInRepair, bool i_IsFilterByFixed, bool i_IsFilterByPaid)
          {
               List<string> registrationNumbersList = new List<string>();

               foreach (Vehicle vehicle in m_Vehicles)
               {
                    if(i_IsFilterByInRepair == true)
                    {
                         if (vehicle.VehicleStatus == eVehicleStatus.InRepair)
                         {
                              registrationNumbersList.Add(vehicle.RegistrationNumber);
                         }
                    }
                    else if(i_IsFilterByPaid == true)
                    {
                         if (vehicle.VehicleStatus == eVehicleStatus.Paid)
                         {
                              registrationNumbersList.Add(vehicle.RegistrationNumber);
                         }
                    }
                    else if(i_IsFilterByFixed == true)
                    {
                         if (vehicle.VehicleStatus == eVehicleStatus.Fixed)
                         {
                              registrationNumbersList.Add(vehicle.RegistrationNumber);
                         }
                    }
                    else
                    {
                         registrationNumbersList.Add(vehicle.RegistrationNumber);
                    }
               }

               return registrationNumbersList;
          }

          public bool CheckEngineCapacity(string i_EngineCapacity, ref int io_EngineCapacity)
          {
               int engineCapacity = 0;
               bool isEngineCapacityNumber = int.TryParse(i_EngineCapacity, out engineCapacity);
               bool isValidEngineCapacity = false;

               if (isEngineCapacityNumber == true)
               {
                    isValidEngineCapacity = engineCapacity >= 50 && engineCapacity <= 2000;

                    if (isValidEngineCapacity == true)
                    {
                         io_EngineCapacity = engineCapacity;
                    }
                    else
                    {
                         io_EngineCapacity = 0;
                    }
               }

               return isValidEngineCapacity;
          }

          public bool IsVehicleExists(string i_RegistrationNumber)
          {
               bool isExists = false;

               foreach (Vehicle vehicle in m_Vehicles)
               {
                    if (vehicle.RegistrationNumber == i_RegistrationNumber)
                    {
                         isExists = true;
                         break;
                    }
               }

               return isExists;
          }

          public bool InflateWheelsAirPressure(string i_RegistrationNumber)
          {
               bool isInflateSucceeded = false;

               foreach (Vehicle vehicle in m_Vehicles)
               {
                    if (vehicle.RegistrationNumber.Equals(i_RegistrationNumber))
                    {
                         foreach (Wheel wheel in vehicle.Wheels)
                         {
                              wheel.InflateTire(wheel.MaxAirPressure - wheel.AirPressure);
                              isInflateSucceeded = true;
                         }

                         break;
                    }
               }

               return isInflateSucceeded;
          }

          public bool CheckIfValidFuelType(string i_RegistrationNumber, eFuelType i_FuelType)
          {
               bool isValidFuelType = false;

               foreach (Vehicle vehicle in m_Vehicles)
               {
                    if (vehicle.RegistrationNumber.Equals(i_RegistrationNumber))
                    {
                         FuelTank fuelTank = vehicle.EnergySource as FuelTank;

                         if (fuelTank.FuelType == i_FuelType)
                         {
                              isValidFuelType = true;
                         }

                         break;
                    }
               }

               return isValidFuelType;
          }

          public bool CheckIfFueableCar(string i_RegistrationNumber)
          {
               bool isFueable = false;

               foreach (Vehicle vehicle in m_Vehicles)
               {
                    if (vehicle.RegistrationNumber.Equals(i_RegistrationNumber))
                    {
                         if (vehicle.EnergySource is FuelTank)
                         {
                              isFueable = true;
                         }

                         break;
                    }
               }

               return isFueable;
          }

          public bool CheckIfElectricVehicle(string i_RegistrationNumber)
          {
               bool isElectric = false;

               foreach (Vehicle vehicle in m_Vehicles)
               {
                    if (vehicle.RegistrationNumber.Equals(i_RegistrationNumber))
                    {
                         if (vehicle.EnergySource is Battery)
                         {
                              isElectric = true;
                         }

                         break;
                    }
               }

               return isElectric;
          }

          public void RefillVehicle(string i_RegistrationNumber, float i_FuelAmount)
          {
               foreach (Vehicle vehicle in m_Vehicles)
               {
                    if (vehicle.RegistrationNumber.Equals(i_RegistrationNumber))
                    {
                         vehicle.EnergySource.Refill(i_FuelAmount);
                         break;
                    }
               }
          }

          public bool ChangeVehicleStatus(string i_RegistrationNumber, int i_NewStatus)
          {
               bool isChangeSucceeded = false;

               foreach (Vehicle vehicle in m_Vehicles)
               {
                    if (vehicle.RegistrationNumber.Equals(i_RegistrationNumber))
                    {
                         switch (i_NewStatus)
                         {
                              case 1:
                                   vehicle.VehicleStatus = eVehicleStatus.InRepair;
                                   break;
                              case 2:
                                   vehicle.VehicleStatus = eVehicleStatus.Fixed;
                                   break;
                              case 3:
                                   vehicle.VehicleStatus = eVehicleStatus.Paid;
                                   break;
                              default:
                                   break;
                         }

                         isChangeSucceeded = true;
                         break;
                    }
               }

               return isChangeSucceeded;
          }

          public void ClearVehicleInfo()
          {
               m_VehicleCreator.ClearVehicleInfo();
          }

          public Vehicle EnterNewVehicle()
          {
               MethodInfo[] VehicleCreatorMethods = m_VehicleCreator.GetType().GetMethods();
               List<object> args = new List<object>();
               string vehicleType = string.Empty;
               string ownerName = string.Empty;
               string ownerPhoneNumber = string.Empty;
               Vehicle vehicle = new Car();

               try
               {
                    vehicleType = m_VehicleCreator.VehicleInfo["Vehicle Type"].ToString();
                    ownerName = m_VehicleCreator.VehicleInfo["i_OwnerName"].ToString();
                    ownerPhoneNumber = m_VehicleCreator.VehicleInfo["i_OwnerPhoneNumber"].ToString();

                    foreach (MethodInfo method in VehicleCreatorMethods)
                    {
                         if (method.ReturnType.Name == vehicleType)
                         {
                              ParameterInfo[] allParams = method.GetParameters();

                              foreach (ParameterInfo param in allParams)
                              {
                                   foreach (KeyValuePair<string, object> pair in m_VehicleCreator.VehicleInfo)
                                   {
                                        if (pair.Key == param.Name)
                                        {
                                             args.Add(pair.Value);
                                             break;
                                        }
                                   }
                              }

                              vehicle = (Vehicle)method.Invoke(m_VehicleCreator, args.ToArray());
                              m_CostumerBook.Add(vehicle, new KeyValuePair<string, string>(ownerName, ownerPhoneNumber));
                              m_Vehicles.Add(vehicle);
                              break;
                         }
                    }
               }
               catch (AccessViolationException i_AccessViolationException)
               {
                    m_VehicleCreator.ClearVehicleInfo();
                    throw new Exception(string.Format("System Error: Vehicle info key does not exist."), i_AccessViolationException);
               }
               catch(TargetParameterCountException i_TargetParameterCountException)
               {
                    m_VehicleCreator.ClearVehicleInfo();
                    throw new Exception(string.Format("System Error: Not all parameters passed to 'Create {0} method'.", m_VehicleCreator.VehicleInfo["Vehicle Type"]), i_TargetParameterCountException);
               }
               
               return vehicle;
          }

          public void RemoveVehicle(ref object io_Vehicle)
          {
               Vehicle vehicle = (Vehicle)io_Vehicle;

               m_CostumerBook.Remove(vehicle);
               m_Vehicles.Remove(vehicle);
          }

          public void UpdateVehicleData(ref Vehicle io_Vehicle)
          {
               string propName = string.Empty;

               try
               {
                    PropertyInfo[] allProperty = io_Vehicle.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                    foreach (PropertyInfo prop in allProperty)
                    {
                         propName = prop.Name.Insert(0, "m_");
                         prop.SetValue(io_Vehicle, m_VehicleCreator.VehicleInfo[propName]);
                         m_VehicleCreator.VehicleInfo.Remove(propName);
                    }

                    foreach (KeyValuePair<string, object> pair in m_VehicleCreator.VehicleInfo)
                    {
                         if (pair.Key == "Current Battery Time")
                         {
                              (io_Vehicle.EnergySource as Battery).EnergyTimeLeft = (float)pair.Value;
                         }
                         else if (pair.Key == "Current Fuel Amount")
                         {
                              (io_Vehicle.EnergySource as FuelTank).CurrentFuelAmount = (float)pair.Value;
                         }
                         else if (pair.Key == "i_WheelsManufacture")
                         {
                              io_Vehicle.SetWheelsManufacture(pair.Value.ToString());
                         }
                         else if (pair.Key == "i_WheelsCurrentAirPressure")
                         {
                              io_Vehicle.SetWheelsCurrentAirPressure((int)pair.Value);
                         }
                    }
               }
               catch(ArgumentNullException i_ArgumentNullException)
               {
                    throw new Exception(string.Format("System Error: {0} not exists in vehicle info dictionary.", propName), i_ArgumentNullException);
               }
               catch(ArgumentException i_ArgumentException)
               {
                    throw new Exception(string.Format("System Error: Value cannot converted to the type of {0}'s property.", io_Vehicle.GetType().Name), i_ArgumentException);
               }
               finally
               {
                    m_VehicleCreator.ClearVehicleInfo();
               }               
          }

          public bool CheckCarDoorsAmount(string i_UserInput, out int i_CarDoorsAmount)
          {
               bool isVaildDoors = false;

               if (i_UserInput == "2" || i_UserInput == "3" || i_UserInput == "4" || i_UserInput == "5")
               {
                    i_CarDoorsAmount = int.Parse(i_UserInput);
                    isVaildDoors = true;
               }
               else
               {
                    i_CarDoorsAmount = 0;
               }

               return isVaildDoors;
          }
     }
}
