using System.Collections.Generic;

namespace GarageLogic
{
     public class VehicleCreator
     {
          private const float m_TruckMaxFuelAmount = 110f;
          private const float m_MotorcycleMaxFuelAmount = 8f;
          private const float m_MotorcycleMaxBatteryHours = 1.4f;
          private const float m_CarMaxFuelAmount = 55f;
          private const float m_CarMaxBatteryHours = 1.8f;

          private Dictionary<string, object> m_VehicleInfo = new Dictionary<string, object>();

          public Dictionary<string, object> VehicleInfo
          {
               get
               {
                    return m_VehicleInfo;
               }
          }

          public void ClearVehicleInfo()
          {
               m_VehicleInfo.Clear();
          }

          public void AddVehicleInfo(string i_InfoDescription, object i_Info)
          {
               m_VehicleInfo.Add(i_InfoDescription, i_Info);
          }

          public Car CreateCar(string i_Model, string i_RegistrationNumber, bool i_IsEnergySource)
          {
               EnergySource energySource;

               if(i_IsEnergySource == true)
               {
                    energySource = new Battery();
               }
               else
               {
                    energySource = new FuelTank();
               }

               Car car = new Car(i_Model, i_RegistrationNumber, energySource);

               if(i_IsEnergySource == true)
               {
                    (car.EnergySource as Battery).MaxEnergyTime = m_CarMaxBatteryHours;
               }
               else
               {
                    (car.EnergySource as FuelTank).FuelType = eFuelType.Octan96;
                    (car.EnergySource as FuelTank).MaxFuelAmount = m_CarMaxFuelAmount;
               }

               return car;
          }

          public Motorcycle CreateMotorcycle(string i_Model, string i_RegistrationNumber, bool i_IsEnergySource)
          {
               EnergySource energySource;

               if (i_IsEnergySource == true)
               {
                    energySource = new Battery();
               }
               else
               {
                    energySource = new FuelTank();
               }

               Motorcycle motorcycle = new Motorcycle(i_Model, i_RegistrationNumber, energySource);

               if (i_IsEnergySource == true)
               {
                    (motorcycle.EnergySource as Battery).MaxEnergyTime = m_MotorcycleMaxBatteryHours;
               }
               else
               {
                    (motorcycle.EnergySource as FuelTank).FuelType = eFuelType.Octan95;
                    (motorcycle.EnergySource as FuelTank).MaxFuelAmount = m_MotorcycleMaxFuelAmount;
               }

               return motorcycle;
          }

          public Truck CreateTruck(string i_Model, string i_RegistrationNumber)
          {
               Truck truck = new Truck(i_Model, i_RegistrationNumber);

               (truck.EnergySource as FuelTank).MaxFuelAmount = m_TruckMaxFuelAmount;
               (truck.EnergySource as FuelTank).FuelType = eFuelType.Soler;

               return truck;
          }
     }
}
