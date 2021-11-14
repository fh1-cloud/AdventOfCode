using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdventOfCodeLib.Extensions;

namespace AdventOfCode2018
{
   public class Days
   {



      public static void Dec03( )
      {

      //Part1
         string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Dec03.txt" );
         //string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Temp1.txt" );
         //string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Temp2.txt" );




         Console.WriteLine( "Part 1: " );
         //Clipboard.SetText( checksum.ToString( ) );


      }


      public static void Dec02( )
      {

      //Part1
         string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Dec02.txt" );
         //string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Temp1.txt" );
         //string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Temp2.txt" );

      //Loop over all the input strings.
         long nOfTwoCount = 0;
         long nOfThreeCount = 0;
         for( int i = 0; i<inp.Length; i++ )
         {
         //Declare current string
            string thisString = inp[i];

         //Create the dictionary of letter counts..
            Dictionary<char,int> letterCounts = new Dictionary<char, int>( );
            int j = 0;
            char curChar = thisString[j];
            while( j<inp[i].Length )
            {
               if( !letterCounts.ContainsKey( curChar ) )
               {
                  int count = thisString.CountOccurencesOfLetter( curChar );
                  letterCounts.Add( curChar, count );
               }
               j++;

               if( j >= thisString.Length )
                  break;
               else
                  curChar=thisString[j];

            }

         //Loop through the dictionary and look for lettercounts that are exactly one time..
            int this2 = letterCounts.Where( x => x.Value == 2 ).ToList( ).Count( );
            int this3 = letterCounts.Where( x => x.Value == 3 ).ToList( ).Count( );

            if( this2 >= 1 )
               this2 = 1;
            if( this3 >= 1 )
               this3 = 1;

            nOfTwoCount += this2;
            nOfThreeCount += this3;
         }
         long checksum = nOfTwoCount * nOfThreeCount;
         Console.WriteLine( "Part 1: " + checksum );
         //Clipboard.SetText( checksum.ToString( ) );


      //PART 2
      //FIND THE TWO LETTERS THAT HAVE THE MOST COMMON LETTERS. THEN REMOVE THE LETTERS THAT DIFFER

      //Loop over all the strings..
         Dictionary<int, List<Tuple<string,string>>> commonLetterCount = new Dictionary<int, List<Tuple<string, string>>>( );
         for( int i = 0; i<inp.Length; i++ )
         {

         //Declare the main string..
            string thisComparer = inp[i];

         //Loop over all strings below this string in the list. ALl the others have been checked already
            for( int j = i + 1; j < inp.Length; j++ )
            {
            //Declare candidate string..
               string thisCandidate = inp[j];

            //Count number of common indices
               int commonIndices = thisComparer.CountCommonPositions( thisCandidate );

            //Create a tuple pair..
               Tuple<string, string> thisPair = new Tuple<string, string>( thisComparer, thisCandidate );

            //Find the list to add the tupe to
               List<Tuple<string,string>> thisList = null;
               if( !commonLetterCount.ContainsKey( commonIndices ) )
               {
                  thisList = new List<Tuple<string, string>>( );
                  commonLetterCount.Add( commonIndices, thisList );
               }
               else
                  thisList = commonLetterCount[commonIndices];

            //Add string tuple to the list..
               thisList.Add( thisPair );
            }
         }

      //Find the minimum string pair. Hopefully only one pair..
         int minCommonCount = commonLetterCount.Select( x => x.Key ).ToList( ).Max( );
         KeyValuePair<int, List<Tuple<string, string>>> pair = commonLetterCount.Where( x => x.Key == minCommonCount ).FirstOrDefault( );

      //Create the new string by combining the two string with common letters..
         StringBuilder sb = new StringBuilder( );
         string first = pair.Value[0].Item1;
         string second = pair.Value[0].Item2;
         for( int i = 0; i < first.Length; i++ )
            if( first[i] == second[i] )
               sb.Append( first[i].ToString( ) );

      //Write answer
         Console.WriteLine( "Part 2 " + sb.ToString( ) );
         Clipboard.SetText( sb.ToString( ) );


      }




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
