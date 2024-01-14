using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Classes
{

   //public class BrentCycle
   //{

   //   public static ( int cycleStartIdx, int cycleLength ) FindCycle( List<int> list, int x0 )
   //   {
   //      int power = 1;
   //      int cycleLength = 1;
   //      int cycleStartPosition = 0;

   //      int tortoise = x0;
   //      int hare = list[x0];
   //      while( tortoise != hare )
   //      {
   //         if( power == cycleLength )
   //         {
   //            tortoise = hare;
   //            power = power*2;
   //            cycleLength = 0;
   //         }
   //         hare = list[hare];
   //         cycleLength += 1;
   //      }

   //      tortoise = x0;
   //      hare = x0;
   //      for( int i = 0; i<cycleLength; i++ )
   //         hare = list[hare];


   //      while( tortoise != hare )
   //      {
   //         tortoise = list[tortoise];
   //         hare = list[hare];
   //         cycleStartPosition++;
   //      }
   //      return ( cycleStartPosition, cycleLength );
   //   }
   //}


}
