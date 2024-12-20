﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
   class Program
   {
      [STAThread]          //Add this to get access to the clipboard for copying results..
      static void Main( string[] args )
      {
         Console.Write( GetConsoleHeader( 07 ) );
         Days.Dec07( );
         Console.ReadKey( );
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
         sb.AppendLine( "*                    ADVENT OF CODE 2018                        *" );
         sb.AppendLine( "*                        December " + ( decemberDate.ToString().Length == 1 ? decemberDate.ToString() + " " : decemberDate.ToString() ) + "                            *" );
         sb.AppendLine( "*                                                               *" );
         sb.AppendLine( "*                                                               *" );
         sb.AppendLine( "*****************************************************************" );
         sb.AppendLine( " " );
         return sb.ToString( );
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



   }
}
