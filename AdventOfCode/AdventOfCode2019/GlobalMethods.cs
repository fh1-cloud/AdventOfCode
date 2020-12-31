using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
   public static class GlobalMethods
   {


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


      /// <summary>
      /// A method that gets a list of ints from the input. Use this if the input is an list of integers
      /// </summary>
      /// <param name="fileName"></param>
      /// <returns></returns>
      public static long[] GetInputIntegerArray( string fileName )
      {
      //Validation
         if( fileName == null )
            return null;

      //Read text file..
         string[] lines = System.IO.File.ReadAllLines( fileName );

         long[] parsedLines = new long[lines.Length];
         for( int i = 0; i < lines.Length; i++ )
            parsedLines[i] = long.Parse( lines[i] );

      //Return the int array;
         return parsedLines;
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
         sb.AppendLine( "*                    ADVENT OF CODE 2019                        *" );
         sb.AppendLine( "*                        December " + ( decemberDate.ToString().Length == 1 ? decemberDate.ToString() + " " : decemberDate.ToString() ) + "                            *" );
         sb.AppendLine( "*                                                               *" );
         sb.AppendLine( "*                                                               *" );
         sb.AppendLine( "*****************************************************************" );
         sb.AppendLine( " " );
         return sb.ToString( );
      }





   }
}
