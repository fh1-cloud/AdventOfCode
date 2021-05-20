using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdventOfCode2019.Classes;
using AdventOfCodeLib.Numerics;

namespace AdventOfCode2019
{
   public class Days
   {



   /*STATIC METHODS*/
   #region



   
      public static void Dec03()
      {
         string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec03.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Temp02.txt" );

      //Create the two wires.
         Wire a = new Wire( inp[0] );
         Wire b = new Wire( inp[1] );

      //Try to calculate the intersection points..
         List<UVector2D> intPoints = a.GetIntersectionPoints( b, out List<double> signalDelays );

      //Find the one with the minimum manhattan distance.
         double minManhattan = 1.0e12;
         UVector2D minPoint = null;
         foreach( var v in intPoints )
         {
            if( v.GetManhattanLength( ) < minManhattan )
            {
               minPoint = v;
               minManhattan = v.GetManhattanLength( );
            }
         }

      //Print the minimum value.
         Console.WriteLine( minManhattan );

      //PART 2
         double minInterference = signalDelays.Min( );

      //Print the minimum interference
         Console.WriteLine( minInterference );

      }



      public static void Dec02()
      {
         string inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec02.txt" )[0];
         //long[] inp = GlobalMethods.GetInputIntegerArray( @"Inputs\\Temp01.txt" );
         //string inp = "1,0,0,0,99";

         //PART1
         IntcodeComputer c = new IntcodeComputer( inp );
         c[1] = 12;
         c[2] = 2;
         c.RunIntcode( );

         long ans1 = c[0];
         Console.WriteLine( "Part 1: " + ans1 );

      //PART2
         long noun = -1;
         long verb = -1;
         long target = 19690720;
         for( int i = 0; i < 100; i++ )
         {
            for( int j = 0; j < 100; j++ )
            {
            //Create a new intcode computer
               IntcodeComputer thisComputer = new IntcodeComputer( inp );
               thisComputer[1] = i;
               thisComputer[2] = j;

            //Run the intcode computer
               thisComputer.RunIntcode( );

            //Chech the output in position zero. If it matches, break the loop
               if( thisComputer[0] == target )
               {
                  noun = i;
                  verb = j;
                  break;
               }
            }
            if( noun >= 0 )
               break;
         }
         long ans2 = 100 * noun + verb;
         Console.WriteLine( "Part 2: " + ans2 );
         Clipboard.SetText( ans2.ToString( ) );
      }



      public static void Dec01()
      {
         long[] inp = GlobalMethods.GetInputIntegerArray( @"Inputs\\Dec01.txt" );
         //long[] inp = GlobalMethods.GetInputIntegerArray( @"Inputs\\Temp01.txt" );

      //PART1
         long massSoFar = 0;
         foreach( long l in inp )
            massSoFar += l / 3 - 2;
         Console.WriteLine( "Part 1: " + massSoFar );

      //PART2
         long fuelSoFar = 0;
         foreach( long l in inp )
            fuelSoFar += FuelCalculator.GetFuel( l );

         Console.WriteLine( "Part 2: " + fuelSoFar );
         Clipboard.SetText( fuelSoFar.ToString( ) );
      }



   #endregion


   }
}
