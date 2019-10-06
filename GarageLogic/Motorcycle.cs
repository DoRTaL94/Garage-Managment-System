using System;
using System.Collections.Generic;

namespace GarageLogic
{
     public class Motorcycle : Vehicle
     {
          private const int m_MaxAirPressure = 33;
          private const int m_NumOfWheels = 2;

          private eRegistrationType m_RegistrationType = eRegistrationType.None;
          private int m_EngineCapacity = 0;

          public Motorcycle()
          {
          }

          public Motorcycle(string i_Model, string i_RegistrationNumber, EnergySource i_EnergySource)
          : base(i_Model, i_RegistrationNumber, m_NumOfWheels, i_EnergySource)
          {
               SetWheelsMaxPressure(m_MaxAirPressure);
          }

          public eRegistrationType RegistrationType
          {
               get
               {
                    return m_RegistrationType;
               }

               set
               {
                    m_RegistrationType = value;
               }
          }

          public int EngineCapacity
          {
               get
               {
                    return m_EngineCapacity;
               }

               set
               {
                    m_EngineCapacity = value;
               }
          }

          public override string ToString()
          {
               return "Motorcycle";
          }
     }
}
