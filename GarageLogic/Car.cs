using System;

namespace GarageLogic
{
     public class Car : Vehicle
     {
          private const int m_MaxAirPressure = 31;
          private const int m_NumOfCarWheels = 4;
          private int m_DoorsCount = 0;
          private eVehicleColor m_Color = eVehicleColor.None;

          public Car()
          {
          }

          public Car(string i_Model, string i_RegistrationNumber, EnergySource i_EnergySource)
          : base(i_Model, i_RegistrationNumber, m_NumOfCarWheels, i_EnergySource)
          {
               SetWheelsMaxPressure(m_MaxAirPressure);
          }

          public int DoorsCount
          {
               get
               {
                    return m_DoorsCount;
               }

               set
               {
                    m_DoorsCount = value;
               }
          }

          public eVehicleColor Color
          {
               get
               {
                    return m_Color;
               }

               set
               {
                    m_Color = value;
               }
          }

          public override string ToString()
          {
               return "Car";
          }
     }
}
