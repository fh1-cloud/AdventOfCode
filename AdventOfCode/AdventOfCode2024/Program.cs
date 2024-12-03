namespace AdventOfCode2024
{
   internal class Program
   {
      static void Main( string[ ] args )
      {
         //Set the day..
         int day = 4;

         //Try to get the input if it doesnt exist already..
         GlobalMethods.GetInput( day, 2024 );

         //Run the day
         string methodName = day.ToString( ).Length <= 1 ? ( "Dec0" + day.ToString( ) ) : ( "Dec" + day.ToString( ) ); //Create the name of the method..
         Console.Write( GlobalMethods.GetConsoleHeader( day ) ); //Create console header..
         Days d = new Days( );
         GlobalMethods.GetMethodByName( d, methodName ).Invoke( d, null );
         Console.ReadKey( );
      }
   }
}
