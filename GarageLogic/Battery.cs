using ConsoleUtils;

namespace GarageLogic
{
     public class Battery : EnergySource
     {
          private const float m_MinEnergyTime = 0;
          private float m_EnergyTimeLeft;
          private float m_MaxEnergyTime;

          public override void Refill(float i_HowMuchToAdd)
          {
               float newEnergyTimeAmount = m_EnergyTimeLeft + i_HowMuchToAdd;

               if (newEnergyTimeAmount <= m_MaxEnergyTime && newEnergyTimeAmount != m_EnergyTimeLeft)
               {
                    this.m_EnergyTimeLeft = newEnergyTimeAmount;
               }
               else
               {
                    throw new ValueOutOfRangeException("Battery time", m_MinEnergyTime, m_MaxEnergyTime);
               }
          }

          public float EnergyTimeLeft
          {
               get
               {
                    return m_EnergyTimeLeft;
               }

               set
               {
                    m_EnergyTimeLeft = value;
               }
          }

          public float MaxEnergyTime
          {
               get
               {
                    return m_MaxEnergyTime;
               }

               set
               {
                    m_MaxEnergyTime = value;
               }
          }

          public override string ToString()
          {
               return "Battery";
          }
     }
}
