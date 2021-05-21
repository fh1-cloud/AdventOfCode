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



      public static void Dec06( )
      {
      //Parse input
         string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec06.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Temp01.txt" );

      //Declare a dictionary with the name of the object as the key and the object itself as the value
         Dictionary<string,OrbitObject> objectsInOrbit = new Dictionary<string,OrbitObject>( );
         foreach( string s in inp )
         {
            string[] spl = s.Split( ')' );
            string name1 = spl[0];
            string name2 = spl[1];

         //Create the parent if it doesnt exist.
            if( !objectsInOrbit.ContainsKey( name1 ) )
            {
               OrbitObject o = new OrbitObject( name1 );
               objectsInOrbit.Add( name1, o );
            }

         //Create the child if it doesnt exist. If it exists, add the parent.
            if( !objectsInOrbit.ContainsKey( name2 ) )
            {
               OrbitObject o = new OrbitObject( name2 );
               o.Parent = objectsInOrbit[name1];
               objectsInOrbit.Add( name2, o );
            }
            else
               objectsInOrbit[name2].Parent = objectsInOrbit[name1];

         }

      //Loop over all the objects created and could the orbits..
         long nOfOrbits = 0;
         foreach( KeyValuePair<string, OrbitObject> kvp in objectsInOrbit )
            nOfOrbits += kvp.Value.CountOrbitsAbove( );

      //Write output..
         Console.WriteLine( nOfOrbits );

      }




      public static void Dec05( )
      {
         string inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec05.txt" )[0];
         //string inp = "3,0,4,0,99";
         //string inp = "1002,4,3,4,33";
         //string inp = "1101,100,-1,4,0"
         //string inp = "3,9,8,9,10,9,4,9,99,-1,8";
         //string inp = "3,3,1107,-1,8,3,4,3,99";
         //string inp = "3,3,1108,-1,8,3,4,3,99";
         //string inp = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9";
         //string inp = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1";
         //string inp = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31, 1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104, 999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";

         IntcodeComputer ic = new IntcodeComputer( inp );
         ic.RunIntcode( );
      }




      public static void Dec04( )
      {
         string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec04.txt" );
         int minValue = int.Parse( inp[0].Split( '-' )[0] );
         int maxValue = int.Parse( inp[0].Split( '-' )[1] );


         List<int> validPassWords = new List<int>( );
         for( int i = minValue+1; i < maxValue; i++ )
            if( PasswordValidater.Validate( i ) )
               validPassWords.Add( i );

         Console.WriteLine( validPassWords.Count );
      }

   
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
