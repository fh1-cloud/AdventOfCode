using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2022.Classes;
using System.Windows.Forms;

namespace AdventOfCode2022
{
   public class Days
   {
      /// <summary>
      /// 
      /// </summary>
      public static void Dec25( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec24( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec23( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec22( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec21( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec20( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec19( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec18( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec17( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec16( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec15( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec14( )
      {



      }
      /// <summary>
      /// Dec13
      /// </summary>
      public static void Dec13( )
      {



      }
      /// <summary>
      /// Dec12
      /// </summary>
      public static void Dec12( )
      {



      }
      /// <summary>
      /// Dec11
      /// </summary>
      public static void Dec11( )
      {



      }
      /// <summary>
      /// Dec10
      /// </summary>
      public static void Dec10( )
      {



      }
      /// <summary>
      /// Dec09
      /// </summary>
      public static void Dec09( )
      {



      }
      /// <summary>
      /// Dec08
      /// </summary>
      public static void Dec08( )
      {



      }
      /// <summary>
      /// De07c
      /// </summary>
      public static void Dec07( )
      {



      }
      /// <summary>
      /// De06c
      /// </summary>
      public static void Dec06( )
      {



      }
      /// <summary>
      /// Dec05
      /// </summary>
      public static void Dec05( )
      {



      }

      /// <summary>
      /// Dec04
      /// </summary>
      public static void Dec04( )
      {



      }

      /// <summary>
      /// Dec03
      /// </summary>
      public static void Dec03( )
      {
      //Parse input file..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec03.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );

      //Loop over items and create rucksacks..
         long ans = 0;
         List<Rucksack> sacks = new List<Rucksack>( );
         for( int i = 0; i<inp.Length; i++ )
            sacks.Add( new Rucksack( inp[i] ) );

      //Find badge items..
         for( int i = 0; i<sacks.Count; i = i+3 )
         {
            long badgeScore = sacks[i].FindBadgeValue( sacks[i+1], sacks[i+2] );
            ans += badgeScore;
         }

      //Compact solution
         //var score = File.ReadLines("input.txt")
         //    .Select(e => e.ToCharArray())
         //    .Chunk(3)
         //    .Select(e => e[0].Intersect(e[1]).Intersect(e[2]).First())
         //    .Select(e => char.IsUpper(e) ? e - 38 : e - 96)
         //    .Sum();

         //Console.WriteLine(score);
      //Loop over the
         Console.WriteLine( "Ans: " + ans.ToString( ) );
         Clipboard.SetText( ans.ToString( ) );
      }


      /// <summary>
      /// Dec2
      /// </summary>
      public static void Dec02( )
      {
      //Parse input file..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec02.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );

         long ans = 0;
         for( int i = 0; i<inp.Length; i++ )
         {
            string[] split = inp[i].Split( ' ' );
            RockPaperScissors thisGame = new RockPaperScissors( split[0][0], split[1][0] );
            ans += thisGame.Score;
         }

         Console.WriteLine( "Total score: " + ans.ToString( ) );
         Clipboard.SetDataObject( ans.ToString( ) );

      }

      /// <summary>
      /// Dec1
      /// </summary>
      public static void Dec01( )
      {
      //Parse input file..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec01.txt" );
         
      //Create elfs..
         ElfWithCalories currentElf = new ElfWithCalories( );
         List<ElfWithCalories> allElves = new List<ElfWithCalories>( );
         allElves.Add( currentElf );
         for( int i = 0; i < inp.GetLength( 0 ); i++ )
         {
         //Create new elf if reached end..
            if( inp[i].Length == 0 )
            {
               currentElf = new ElfWithCalories( );
               allElves.Add( currentElf );
               continue;
            }

         //Add the food item..
            currentElf.Food.Add( int.Parse( inp[i] ) );
         }

         //Find max elf..
         //List<long> calList = allElves.Select( x => x.TotalCalories ).ToList( );
         //Console.WriteLine( "Max calories of one elf: " + maxCal.ToString( ) );

         List<ElfWithCalories> sorted = allElves.OrderBy( x => x.TotalCalories ).ToList( );
         sorted.Reverse( );
         long totSum = sorted[0].TotalCalories + sorted[1].TotalCalories + sorted[2].TotalCalories;

         Console.WriteLine( "Max calories of top three elves: " + totSum.ToString( ) );


      }

   }
}
