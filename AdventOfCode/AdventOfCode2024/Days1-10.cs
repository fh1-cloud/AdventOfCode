using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
   public partial class Days
   {

   //*STATIC METHODS//

      public static void Dec10( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp02.txt" );

      }
      public static void Dec09( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp02.txt" );

      }
      public static void Dec08( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp02.txt" );

      }
      public static void Dec07( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp02.txt" );

      }
      public static void Dec06( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp02.txt" );

      }
      public static void Dec05( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp02.txt" );

      }
      public static void Dec04( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp02.txt" );

      }
      public static void Dec03( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp02.txt" );

      }

      public static void Dec02( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Temp02.txt" );

      }


      public static void Dec01( )
      {
         List<int[ ]> inp = GlobalMethods.GetInputIntArrayList( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );

      //Create a list of the "left" and "right" array
         List<int> left = new List<int>( );
         List<int> right = new List<int>( );
         foreach( int[ ] a in inp )
         {
            left.Add( a[0] );
            right.Add( a[1] );
         }

      //Sort the lists..
         left.Sort( );
         right.Sort( );

      //Distances..
         int dist = 0;
         for( int i = 0; i< inp.Count; i++ )
            dist += Math.Abs( left[i]-right[i] );

      //Part2
         long simScore = 0;
         for( int i = 0; i<left.Count; i++ )
            simScore += left[i] * right.Where( x => x == left[i] ).ToList( ).Count;

         Console.WriteLine( simScore.ToString( ) );
      }


   }
}
