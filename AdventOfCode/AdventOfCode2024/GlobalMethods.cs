using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
   public class GlobalMethods
   {


      /// <summary>
      /// A method that gets a list of integers written on the same line.
      /// </summary>
      /// <param name="fileName"></param>
      /// <returns></returns>
      public static List<int[]> GetInputIntArrayList( string fileName )
      {
      //Validation
         if( fileName == null )
            return null;

      //Read text file..
         string[] lines = File.ReadAllLines( fileName );
         List<int[]> result = new List<int[]>();

         for( int i = 0; i < lines.Length; i++ )
         {

         //Split by space..
            string[] spaceSplit = lines[i].Split( ' ', StringSplitOptions.RemoveEmptyEntries );

         //Declare this int array
            int[] thisArr = new int[spaceSplit.Length];
            for( int j = 0; j<spaceSplit.Length; j++ )
               thisArr[j] = int.Parse( spaceSplit[j] );

            result.Add( thisArr );
         }
         return result;
      }

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


      /// <summary>
      /// A method that splits the input string array by empty lines and returns a list of string arrays that are grouped..
      /// </summary>
      /// <param name="input"></param>
      /// <returns></returns>
      public static List<string[]> SplitStringArrayByEmptyLine( string[] input )
      {
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
      /// Get input for the passed day..
      /// </summary>
      /// <param name="day"></param>
      public static async void GetInput( int day, int year)
      {
      //Build file path for input..
         StringBuilder sb = new StringBuilder( );
         sb.Append( "..\\..\\..\\Inputs\\" );
         sb.Append( day.ToString( ).Length <= 1 ? ( "Dec0" + day.ToString( ) ) : ( "Dec" + day.ToString( ) ) );
         sb.Append( ".txt" );

         //Fetch the input..
         try
         {
            Task task = AdventOfCodeLib.InputFetcher.GetInput( day, year, sb.ToString( ) );
            task.Wait( );
         }
         catch
         {

         }
      }

      /// <summary>
      /// Get method info from passed object. Used to run the day by reflection so we only have to change an integer to swap methods..
      /// </summary>
      /// <param name="obj"></param>
      /// <param name="methodName"></param>
      /// <returns></returns>
      public static MethodInfo GetMethodByName( object obj, string methodName )
      {
         return obj.GetType( ).GetMethods( ).FirstOrDefault( m => m.Name == methodName );
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
         sb.AppendLine( "*                    ADVENT OF CODE 2024                        *" );
         sb.AppendLine( "*                        December " + ( decemberDate.ToString().Length == 1 ? decemberDate.ToString() + " " : decemberDate.ToString() ) + "                            *" );
         sb.AppendLine( "*                                                               *" );
         sb.AppendLine( "*                                                               *" );
         sb.AppendLine( "*****************************************************************" );
         sb.AppendLine( " " );
         return sb.ToString( );
      }


   }
}
