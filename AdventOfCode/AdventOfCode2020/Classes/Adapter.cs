using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   /// <summary>
   /// Dec10 power adapter class
   /// </summary>
   public class Adapter
   {
      public static Dictionary<int, long> SolutionCache = new Dictionary<int, long>( ); //Store the number of ways to get to the end from the key position. This saves time for recursive calls


      public static long FindPathwaysToEnd( long[] arr, int thisIdx )
      {
         if( thisIdx == arr.Length - 1 )
            return 1;
         if( SolutionCache.ContainsKey( thisIdx ) )
            return SolutionCache[thisIdx];

         long ans = 0;
         int endIdx = thisIdx + 4;
         if( endIdx > arr.Length )
            endIdx = arr.Length;

         for( int i = thisIdx + 1; i < endIdx; i++ )
         {
            if( arr[i] - arr[thisIdx] <= 3 )
            {
               ans += FindPathwaysToEnd( arr, i );
            }

         }

      //Set the dictionary value to cache the solution
         SolutionCache[thisIdx] = ans;

         return ans;
      }







      public Adapter( string inp )
      {
         //Parse the input to an int
         long j;
         bool suc = long.TryParse( inp, out j );
         if( !suc )
            throw new Exception( );

         this.Joltage = j;
      }

      public long Joltage { get; set; }









   }







}
