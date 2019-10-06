namespace GarageLogic
{
     public class Truck : Vehicle
     {
          private const int m_MaxAirPressure = 26;
          private const int m_NumOfWheels = 12;

          private bool m_IsHazardousMaterial = false;
          private float m_CargoVolume = 0;

          public Truck()
          {
          }

          public Truck(string i_Model, string i_RegistrationNumber)
          : base(i_Model, i_RegistrationNumber, m_NumOfWheels, new FuelTank())
          {
               SetWheelsMaxPressure(m_MaxAirPressure);
          }

          public bool IsHazardousMaterial
          {
               get
               {
                    return m_IsHazardousMaterial;
               }

               set
               {
                    m_IsHazardousMaterial = value;
               }
          }

          public float CargoVolume
          {
               get
               {
                    return m_CargoVolume;
               }

               set
               {
                    m_CargoVolume = value;
               }
          }

          public override string ToString()
          {
               return "Truck";
          }
     }
}
