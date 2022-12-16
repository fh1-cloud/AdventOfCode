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
         Console.Write( GlobalMethods.GetConsoleHeader( 16 ) );
         Days.Dec16( );
         Console.ReadKey( );
      }
   }
}
