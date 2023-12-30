using AdventOfCode2023.Classes;
using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
   public partial class Days
   {
   /*STATIC METHODS*/
   #region

      public static void Dec25( )
      {

      //Parse the text file to a string..
         string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Test01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\Test02.txt" );

         long ans = 0;
         Console.WriteLine( ans );

      }


      public static void Dec24( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         List<HailStone> hs = new List<HailStone>( );
         for( int i = 0; i< inp.Length; i++ )
            hs.Add( new HailStone( inp[i] ) );

         Console.WriteLine( HailStone.FindMagicVector( hs ) );
      }


      public static void Dec23( )
      {
         Console.WriteLine( new GardenMap( GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" ) ).FindLongestPath( ) );
      }


      public static void Dec22( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         List<WoodBrick.Brick> allBricks = WoodBrick.ParseBricks( inp );
         WoodBrick.Fall( allBricks );
         int ans2 = WoodBrick.CountChainReactionForAllBricks( allBricks );
         Console.WriteLine( ans2 );
      }


      public static void Dec21( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         Garden g = new Garden( inp );
         g.P2( );
      }

   #endregion

   }
}
