namespace GarageLogic
{
     public class Wheel
     {
          private string m_ManufactureName = string.Empty;
          private float m_AirPressure = 0;
          private float m_MaxAirPressure = 0;

          public string ManufactureName
          {
               get
               {
                    return m_ManufactureName;
               }

               set
               {
                    m_ManufactureName = value;
               }
          }

          public void InflateTire(float i_AirToAdd)
          {
               this.AirPressure += i_AirToAdd;
          }

          public float AirPressure
          {
               get
               {
                    return m_AirPressure;
               }

               set
               {
                    m_AirPressure = value;
               }
          }

          public float MaxAirPressure
          {
               get
               {
                    return m_MaxAirPressure;
               }

               set
               {
                    m_MaxAirPressure = value;
               }
          }
     }
}
