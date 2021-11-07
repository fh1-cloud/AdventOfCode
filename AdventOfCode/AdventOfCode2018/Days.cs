using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode2018
{
   public class Days
   {



      /// <summary>
      /// Dec01
      /// </summary>
      public static void Dec01()
      {

         string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Dec01.txt" );

         //Part1
         long sum = 0;
         for( int i = 0; i<inp.Length; i++ )
         {

            string opString = inp[i][0].ToString( );
            long val = long.Parse( inp[i].Substring( 1 ) );
            if( opString == "+" )
               sum += val;
            else if( opString == "-" )
               sum -= val;
            else
               throw new Exception( );
         }

         Console.WriteLine( sum );
         Clipboard.SetText( sum.ToString( ) );

      //Part2
         HashSet<long> frequencies = new HashSet<long>( );
         bool foundDuplicate = false;
         int currIdx = 0;
         sum  = 0;
         while( !foundDuplicate )
         {

            string opString = inp[currIdx][0].ToString( );
            long val = long.Parse( inp[currIdx].Substring( 1 ) );
            if( opString == "+" )
               sum += val;
            else if( opString == "-" )
               sum -= val;
            else
               throw new Exception( );

            if( !frequencies.Contains( sum ) )
            {
               frequencies.Add( sum );
               currIdx++;
               if( currIdx > inp.Length-1 )
                  currIdx = 0;
            }
            else
            {
               foundDuplicate = true;
            }
         }

         Console.WriteLine( sum );
         Clipboard.SetText( sum.ToString( ) );


      }


   }
}
