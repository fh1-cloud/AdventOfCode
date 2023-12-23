﻿using AdventOfCode2023.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode2023
{
   public partial class Days
   {
   /*STATIC METHODS*/
   #region

      public static void Dec25( )
      {

      //Parse the text file to a string..
         string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Test01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Test02.txt" );

         long ans = 0;
         Console.WriteLine( ans );
         Clipboard.SetText( ans.ToString( ) );

      }


      public static void Dec24( )
      {

      //Parse the text file to a string..
         string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Test01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Test02.txt" );

         long ans = 0;
         Console.WriteLine( ans );
         Clipboard.SetText( ans.ToString( ) );

      }


      public static void Dec23( )
      {

      //Parse the text file to a string..
         string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Test01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Test02.txt" );

         long ans = 0;
         Console.WriteLine( ans );
         Clipboard.SetText( ans.ToString( ) );

      }


      public static void Dec22( )
      {

         //Parse the text file to a string..
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Test01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Test02.txt" );

         Dictionary<int, WoodBrick> allBricks = WoodBrick.CreateAllBricksAndSettle( inp );
         Dictionary<int, WoodBrick> safeBricks = WoodBrick.FindBricksThatCanBeDisintegrated( allBricks );

         long ans = safeBricks.Count;
         Console.WriteLine( ans );
         Clipboard.SetText( ans.ToString( ) );

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