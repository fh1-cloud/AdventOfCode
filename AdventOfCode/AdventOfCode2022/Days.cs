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
