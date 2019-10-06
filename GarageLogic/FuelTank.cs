using System;
using ConsoleUtils;

namespace GarageLogic
{
     public class FuelTank : EnergySource
     {
          private const float m_MinFuelAmount = 0;
          private eFuelType m_FuelType = eFuelType.None;
          private float m_CurrentFuelAmount = 0;
          private float m_MaxFuelAmount = 0;

          public override void Refill(float i_HowMuchToAdd)
          {
               float newFuelAmount = m_CurrentFuelAmount + i_HowMuchToAdd;

               if (newFuelAmount <= m_MaxFuelAmount && newFuelAmount != m_CurrentFuelAmount)
               {
                    this.CurrentFuelAmount = newFuelAmount;
               }
               else
               {
                    throw new ValueOutOfRangeException("Fuel amount", m_MinFuelAmount, m_MaxFuelAmount);
               }
          }

          public eFuelType FuelType
          {
               get
               {
                    return m_FuelType;
               }

               set
               {
                    m_FuelType = value;
               }
          }

          public float MaxFuelAmount
          {
               get
               {
                    return m_MaxFuelAmount;
               }

               set
               {
                    m_MaxFuelAmount = value;
               }
          }

          public float CurrentFuelAmount
          {
               get
               {
                    return m_CurrentFuelAmount;
               }

               set
               {
                    m_CurrentFuelAmount = value;
               }
          }

          public override string ToString()
          {
               return "Fuel Tank";
          }
     }
}
