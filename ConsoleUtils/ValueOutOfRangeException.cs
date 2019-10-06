using System;

namespace ConsoleUtils
{
     public class ValueOutOfRangeException : Exception
     {
          private float m_MaxValue;
          private float m_MinValue;

          public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
               : base(i_MinValue == i_MaxValue ? string.Format("Given value is out of range. Required length is {0}.", i_MaxValue) : string.Format("Given value is out of range. Legal range is {1}-{2}.", i_MinValue, i_MaxValue))
          {
               m_MaxValue = i_MaxValue;
               m_MinValue = i_MinValue;
          }

          public ValueOutOfRangeException(string i_WhatIsOutOfRange, float i_MinValue, float i_MaxValue)
               : base(i_MinValue == i_MaxValue ? string.Format("{0} is out of range. Required length is {1}.", i_WhatIsOutOfRange, i_MaxValue) : string.Format("{0} is out of range. Legal range is {1}-{2}.", i_WhatIsOutOfRange, i_MinValue, i_MaxValue))
          {
               m_MaxValue = i_MaxValue;
               m_MinValue = i_MinValue;
          }

          public ValueOutOfRangeException(string i_WhatIsOutOfRange, float i_MinValue, float i_MaxValue, Exception i_InnerException)
               : base(i_MinValue == i_MaxValue ? string.Format("{0} is out of range. Required length is {1}.", i_WhatIsOutOfRange, i_MaxValue) : string.Format("{0} is out of range. Legal range is {1}-{2}.", i_WhatIsOutOfRange, i_MinValue, i_MaxValue), i_InnerException)
          {
               m_MaxValue = i_MaxValue;
               m_MinValue = i_MinValue;
          }

          public float MaxValue
          {
               get
               {
                    return m_MaxValue;
               }

               set
               {
                    m_MaxValue = value;
               }
          }

          public float MinValue
          {
               get
               {
                    return m_MinValue;
               }

               set
               {
                    m_MinValue = value;
               }
          }
     }
}
