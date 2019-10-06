using System.Collections.Generic;

namespace GarageLogic
{
     public abstract class Vehicle
     {
          private const int ONE_HUNDRED = 100;

          private EnergySource m_EnergySource;
          private string m_Model = string.Empty;
          private string m_RegistrationNumber = string.Empty;
          private float m_EnergyPercentage = 0;
          private List<Wheel> m_Wheels = new List<Wheel>();
          private eVehicleStatus m_VehicleStatus = eVehicleStatus.None;

          public Vehicle()
          {
          }

          public Vehicle(string i_Model, string i_RegistrationNumber, int i_NumOfWheels, EnergySource i_EnergySource)
          {
               m_Model = i_Model;
               m_EnergySource = i_EnergySource;
               m_RegistrationNumber = i_RegistrationNumber;
               m_EnergyPercentage = 0;
               m_VehicleStatus = eVehicleStatus.InRepair;
               m_Wheels = new List<Wheel>();
               setWheelsAmount(i_NumOfWheels);
          }

          public abstract override string ToString();

          public string RegistrationNumber
          {
               get
               {
                    return m_RegistrationNumber;
               }

               set
               {
                    m_RegistrationNumber = value;
               }
          }

          public string Model
          {
               get
               {
                    return m_Model;
               }
          }

          public EnergySource EnergySource
          {
               get
               {
                    return m_EnergySource;
               }

               set
               {
                    m_EnergySource = value;
               }
          }

          public float EnergyPercentage
          {
               get
               {
                    if(m_EnergySource is Battery)
                    {
                         m_EnergyPercentage = (m_EnergySource as Battery).EnergyTimeLeft / (m_EnergySource as Battery).MaxEnergyTime;
                    }
                    else
                    {
                         m_EnergyPercentage = (m_EnergySource as FuelTank).CurrentFuelAmount / (m_EnergySource as FuelTank).MaxFuelAmount;
                    }

                    m_EnergyPercentage *= ONE_HUNDRED;

                    return m_EnergyPercentage;
               }
          }

          private void setWheelsAmount(int i_NumOfWheels)
          {
               for (int i = 0; i < i_NumOfWheels; i++)
               {
                    m_Wheels.Add(new Wheel());
               }
          }

          public void SetWheelsManufacture(string i_ManufactureName)
          {
               foreach(Wheel wheel in m_Wheels)
               {
                    wheel.ManufactureName = i_ManufactureName;
               }
          }

          public void SetWheelsMaxPressure(int i_MaxPressure)
          {
               foreach(Wheel wheel in m_Wheels)
               {
                    wheel.MaxAirPressure = i_MaxPressure;
               }
          }

          public void SetWheelsCurrentAirPressure(int i_WheelsCurrentAirPressure)
          {
               foreach (Wheel wheel in m_Wheels)
               {
                    wheel.AirPressure = i_WheelsCurrentAirPressure;
               }
          }

          public eVehicleStatus VehicleStatus
          {
               get
               {
                    return m_VehicleStatus;
               }

               set
               {
                    m_VehicleStatus = value;
               }
          }

          public void AddEnergyPercentage(float i_EnergyPercentageToAdd)
          {
               float newEnergyPercentage = m_EnergyPercentage + i_EnergyPercentageToAdd;

               if (newEnergyPercentage <= ONE_HUNDRED)
               {
                    m_EnergyPercentage += i_EnergyPercentageToAdd;
               }
          }

          public List<Wheel> Wheels
          {
               get
               {
                    return m_Wheels;
               }
          }
     }
}
