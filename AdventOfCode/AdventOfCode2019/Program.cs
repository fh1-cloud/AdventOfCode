using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
   class Program
   {

      [STAThread]          //Add this to get access to the clipboard for copying results..
      static void Main( string[] args )
      {

         Console.Write( GlobalMethods.GetConsoleHeader( 6 ) );
         Days.Dec06( );
         Console.ReadKey( );

      }
   }
}
