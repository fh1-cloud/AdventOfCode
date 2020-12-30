using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{

   /// <summary>
   /// Represents a boarding pass used in the Day05 AoC
   /// </summary>
   public class BoardingPass
   {


      protected BoardingPass( )
      {

      }

      public int SeatID { get; protected set; }
      public int SeatRow { get; protected set; }
      public int SeatColumn { get; protected set; }


      /// <summary>
      /// Method to create a boarding pass. Constructor is protected, so this has to be called.
      /// </summary>
      /// <param name="inp"></param>
      /// <returns></returns>
      public static BoardingPass CreateBoardingPassFromString( string inp )
      {
      //Validation   
         if( inp == null )
            return null;

      //Split the string into rows and columns.
         string rowString = inp.Substring( 0, 7 );
         string colString = inp.Substring( 7 );

      //Create the returning boarding pass
         BoardingPass retPass = new BoardingPass( );
         retPass.SetSeatRow( rowString );
         retPass.SetSeatColumn( colString );
         retPass.SetSeatID( );

      //Return the boarding pass
         return retPass;
      }


      protected int SetSeatRow( string inp )
      {
      //Validation
         if( inp == null )
            return -1;

      //Declare the min and max value
         int lowBound = 0;
         int highBound = 127;

      //Loop through
         foreach( char c in inp.ToCharArray( ) )
         {
            int thisInterval = highBound - lowBound + 1;
            if( c == 'F' )
               highBound = highBound - thisInterval / 2; 
            else if( c == 'B' )
               lowBound = lowBound + thisInterval / 2;
         }
         if( lowBound != highBound )
            throw new Exception( );

         this.SeatRow = lowBound;
         return lowBound;
      }

      protected int SetSeatColumn( string inp )
      {
      //Validation
         if( inp == null )
            return -1;

      //Declare the min and max value
         int lowBound = 0;
         int highBound = 7;

      //Loop through..
         foreach( char c in inp.ToCharArray( ) )
         {
            int thisInterval = highBound - lowBound + 1;
            if( c == 'L' )
               highBound = highBound - thisInterval / 2; 
            else if( c == 'R' )
               lowBound = lowBound + thisInterval / 2;
         }
         if( lowBound != highBound )
            throw new Exception( );

         this.SeatColumn = lowBound;
         return lowBound;
      }

      protected void SetSeatID( )
      {
         this.SeatID = this.SeatRow * 8 + this.SeatColumn;
      }




   }
}
