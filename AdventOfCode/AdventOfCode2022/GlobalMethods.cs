using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
   static class GlobalMethods
   {



      /// <summary>
      /// A method that gets a list of ints from the input. Use this if the input is an list of integers
      /// </summary>
      /// <param name="fileName"></param>
      /// <returns></returns>
      public static int[] GetInputIntArray( string fileName )
      {
      //Validation
         if( fileName == null )
            return null;

      //Read text file..
         string[] lines = System.IO.File.ReadAllLines( fileName );

         int[] parsedLines = new int[lines.Length];
         for( int i = 0; i < lines.Length; i++ )
            parsedLines[i] = int.Parse( lines[i] );

      //Return the int array;
         return parsedLines;
      }


      /// <summary>
      /// Gets the string array if the input should be regarded as such
      /// </summary>
      /// <param name="fileName">The file name</param>
      /// <returns></returns>
      public static string[] GetInputStringArray( string fileName )
      {
      //Validation
         if( fileName == null )
            return null;

      //Read text file..
         string[] lines = System.IO.File.ReadAllLines( fileName );

      //Return the int array;
         return lines;

      }


      public static List<string[]> SplitStringArrayByEmptyLine( string[] input )
      {
      //Collect the individual passport data in the list.
         List<string[]> rawData = new List<string[]>( );
         int lastEmptyIdx = -1;
         for( int i = 0; i < input.Length + 1; i++ )
         {
            if( i == input.Length || input[i].Equals( "" ) )
            {
               int nOfLines = i - lastEmptyIdx-1;
               string[] thisRawData = new string[nOfLines];
               for( int j = 0; j < nOfLines; j++ )
               {
                  thisRawData[j] = input[j + lastEmptyIdx+1];
               }
               rawData.Add( thisRawData );
               lastEmptyIdx = i;
            }
         }
         return rawData;
      }


      /// <summary>
      /// Counts the occurence of char c in string str. If positions is not null, it will count the occurences of character c in string str at positions in the array.
      /// </summary>
      /// <param name="str">The string that is checked.</param>
      /// <param name="c">The character to look for</param>
      /// <param name="positions">An optional parameter that is passed if only those positions should be counted</param>
      /// <returns></returns>
      public static int CountOccurenceOfCharInString( string str, char c, int[] positions = null )
      {
         if( str == null )
            return 0;


         if( positions != null )
         {
            int count = 0;
            for( int i = 0; i < positions.Length; i++ )
            {
               if( str[positions[i]] == c )
                  count++;
            }
            return count;
         }
         else
         {
            return str.Count( y => y == c );
         }
      }



      /// <summary>
      /// Gets a nice header for each day.
      /// </summary>
      /// <param name="decemberDate"></param>
      /// <returns></returns>
      public static string GetConsoleHeader( int decemberDate )
      {
         StringBuilder sb = new StringBuilder( );
         sb.AppendLine( "*****************************************************************" );
         sb.AppendLine( "*                                                               *" );
         sb.AppendLine( "*                    ADVENT OF CODE 2020                        *" );
         sb.AppendLine( "*                        December " + ( decemberDate.ToString().Length == 1 ? decemberDate.ToString() + " " : decemberDate.ToString() ) + "                            *" );
         sb.AppendLine( "*                                                               *" );
         sb.AppendLine( "*                                                               *" );
         sb.AppendLine( "*****************************************************************" );
         sb.AppendLine( " " );
         return sb.ToString( );
      }


   }
}
