using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   /// <summary>
   /// A class that represents the small seatscreen used in advent of code day 9
   /// </summary>
   public class SeatScreen
   {



      protected long[] m_Values;
      
      public SeatScreen( string[] input )
      {

      //Parse the input strings as long integers..
         m_Values = new long[input.Length];
         for( int i = 0; i < input.Length; i++ )
         {
            long val;
            bool parsed = long.TryParse( input[i], out val );
            if( !parsed )
               throw new Exception( );

            m_Values[i] = val;
         }

      }


      public long FindInvalidNumber( int preAmbleLength )
      {
      //Set the startindex
         int startIdx = 0;
         bool foundIt = true;
         int foundIdx = -1;
      //Loop over all the numbers
         for( int i = preAmbleLength; i < m_Values.Length; i++ )
         {
         //Declare this value.
            long thisVal = m_Values[i];
            bool foundOne = false;
            startIdx = i - preAmbleLength;


         //Loop over all the values below..
            for( int j = startIdx; j < i; j++ )
            {
               for( int k = j + 1; k < i; k++ )
               {
                  long checkSum = m_Values[j] + m_Values[k];
                  if( checkSum == thisVal )
                     foundOne = true;
               }
            }

         //if something was not found, the value is the invalid number
            if( !foundOne )
            {
               foundIt = true;
               foundIdx = i;
               break;
            }
         }
         if( foundIt )
            return m_Values[foundIdx];

         return -1;

      }


      public long FindContiguousSetThatSumsToTargetThenReturnSumOfMinAndMax( long target )
      {

      //Declare start and endidx of the answer..
         int startIdx = -1;
         int endIdx = -1;
         bool foundOneStretch = false;
 
      //Loop over all the values..
         for( int i = 1; i < m_Values.Length; i++ )
         {
            long thisSum = 0;

            for( int j = i; j >= 0; j-- )
            {
               thisSum += m_Values[j];
               if( thisSum == target )
               {
                  endIdx = i;
                  startIdx = j;
                  foundOneStretch = true;
                  break;
               }
            }
            if( foundOneStretch )
               break;
         }

      //Create the new array..
         int ansLength = endIdx - startIdx;
         long[] ans = new long[ansLength];
         for( int i = 0; i < ansLength; i++ )
            ans[i] = m_Values[startIdx + i];

      //Find the maximum and minimum values in this list.
         long minVal = ans.Min( );
         long maxVal = ans.Max( );
         long finalAns = minVal + maxVal;

      //Return the sum..
         return finalAns;

      }
      




   }
}
