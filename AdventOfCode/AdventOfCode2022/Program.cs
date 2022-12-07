using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
   internal class Program
   {

      [STAThread]
      static void Main( string[ ] args )
      {

         Console.Write( GlobalMethods.GetConsoleHeader( 8 ) );
         Days.Dec08( );
         Console.ReadKey( );
      }
   }
}
