using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class Flight
   {

      protected static int m_NumberOfRowsOnFlight = 126;
      protected static int m_NumberOfColumnsOnFlight = 8;

      protected BoardingPass[,] m_Seats = new BoardingPass[m_NumberOfRowsOnFlight, m_NumberOfColumnsOnFlight];


      public Flight( )
      {
      //Initialize the empty seats. Not populated with a boarding pass
         for( int i = 0; i < m_NumberOfRowsOnFlight; i++ )
            for( int j = 0; j < m_NumberOfColumnsOnFlight; j++ )
               m_Seats[i, j] = null;
      }


      /// <summary>
      /// Inserts a passenger to the flight.
      /// </summary>
      /// <param name="pass"></param>
      public void InsertPassenger( BoardingPass pass )
      {
      //Validation
         if( pass == null )
            throw new Exception( );

      //Seats that does not exist
         if( pass.SeatRow == 0 || pass.SeatRow == 127 )
            throw new Exception( );

         BoardingPass thisSeat = m_Seats[pass.SeatRow - 1, pass.SeatColumn];

      //Check if the seat is vacant
         if( thisSeat != null )
            throw new Exception( );

      //The seat is empty. Populate it with the boarding pass
         m_Seats[pass.SeatRow - 1, pass.SeatColumn] = pass;
      }


      /// <summary>
      /// Find the vacant seats on the plane..
      /// </summary>
      /// <param name="availableSeats"></param>
      public List<Tuple<int,int>> FindVacantSeatsInFlight( )
      {
         List<Tuple<int,int>> availableSeats = new List<Tuple<int, int>>( );
         for( int i = 1; i < m_NumberOfRowsOnFlight; i++ )
            for( int j = 1; j < m_NumberOfColumnsOnFlight; j++ )
               if( m_Seats[i, j] == null )
                  availableSeats.Add( new Tuple<int, int>( i + 1, j ) );

         return availableSeats;
      }

   }
}
