using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeLib;

namespace AdventOfCode2023
{
   public class Program
   {

      [STAThread]
      static void Main( string[ ] args )
      {
      //Set the day..
         int day = 3;

      //Try to get the input if it doesnt exist already..
         GlobalMethods.GetInput( day, 2023 );

      //Run the day
         string methodName = day.ToString( ).Length <= 1 ? ( "Dec0" + day.ToString( ) ) : ( "Dec" + day.ToString( ) ); //Create the name of the method..
         Console.Write( GlobalMethods.GetConsoleHeader( day ) ); //Create console header..
         Days d = new Days( );
         GlobalMethods.GetMethodByName( d, methodName ).Invoke( d, null );
         Console.ReadKey( );
      }


   }
}
