using System;
using System.Linq;
using System.Collections.Generic;


namespace AdventOfCode2020
{

   class Program
   {
      [STAThread]          //Add this to get access to the clipboard for copying results..
      static void Main(string[] args)
      {

         Console.Write( GlobalMethods.GetConsoleHeader( 25 ) );
         Days.Dec25( );
         Console.ReadKey( );
      }

   }


}

