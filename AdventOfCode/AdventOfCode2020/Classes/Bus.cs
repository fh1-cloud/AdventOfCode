using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class Bus
   {


      protected long m_ID;
      protected long m_CycleLength;
      protected int m_OffSet;

      public Bus( int id, int offs)
      {
         m_ID = id;
         m_CycleLength = id;
         m_OffSet = offs;
      }


      public long ID => m_ID;
      public long CycleTime => m_CycleLength;
      public int Offset => m_OffSet;

      /// <summary>
      /// returns the next departure of this bus oafter the timestamp sent
      /// </summary>
      /// <param name="timeStamp"></param>
      /// <returns></returns>
      public long GetNextDepartureAfterTime( long timeStamp, out long timeToWait )
      {
         long missedBusBy = timeStamp % m_CycleLength;
         timeToWait = m_CycleLength - missedBusBy;
         return timeStamp + timeToWait;
      }


      public bool DoesSatisfyDeparture( long timeStamp, int offSet )
      {
         return ( timeStamp + offSet ) % m_CycleLength == 0;
      }

      /// <summary>
      /// Calculates the first occurence of a timestamp that satisfied the bus offset and cycle time.
      /// </summary>
      /// <param name="timestampSoFar"></param>
      /// <param name="repeater"></param>
      /// <param name="cycleTime"></param>
      /// <param name="offSet"></param>
      /// <returns></returns>
      public static long CalculateDepartureTimeP2( long timestampSoFar, long repeater, long cycleTime, int offSet )
      {
      //Declare
         bool foundOne = false;

      //Add the multiplier until the remainer+the minute offset is zero. When the remainder is zero, we know we have a number that has to correct offset, THis cannot also have happened before, because the numbers below are needed to satisfy the constraints for the previous busses
         while( !foundOne )
         {
         //Increment the time stamp by the common divisor..
            timestampSoFar += repeater;
            long remainder = ( timestampSoFar + offSet ) % cycleTime;
            if( remainder == 0 )
               foundOne = true;
         }
         return timestampSoFar;
      }


   }
}
