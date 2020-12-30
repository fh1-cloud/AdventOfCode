using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{

   /// <summary>
   /// A class that represents the seating system in DEC11 of AOC2020
   /// </summary>
   public class SeatingSystem
   {


   /*ENUMS*/
   #region

      public enum SEATSTATUS
      {
         VACANT,
         OCCUPIED
      }

   #endregion

   /*MEMBERS*/
   #region

      protected static int m_RowCount;
      protected static int m_ColumnCount;
      protected static SEATSTATUS?[,] m_InitialSeatStatus;
      protected static Seat[,] m_Seats;
      protected static int m_AdventOfCodePart = 2;

      protected SEATSTATUS?[,] m_SeatStatus;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Static constructor
      /// </summary>
      static SeatingSystem( )
      {
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec11.txt" );
         m_RowCount = input.Length;
         m_ColumnCount = input[0].Length;
         m_InitialSeatStatus = new SEATSTATUS?[m_RowCount, m_ColumnCount];
         m_Seats = new Seat[m_RowCount, m_ColumnCount];

      //Set the initial status
         for( int i = 0; i < m_RowCount; i++ )
         {
            for( int j = 0; j < m_ColumnCount; j++ )
            {
               if( input[i][j] == 'L' )
                  m_InitialSeatStatus[i, j] = SEATSTATUS.VACANT;
               else if( input[i][j] == '#' )
                  m_InitialSeatStatus[i, j] = SEATSTATUS.OCCUPIED;
               else if( input[i][j] == '.' )
                  m_InitialSeatStatus[i,j] = null;
            }

         }

      //Declare an array of ints that determines wheter a seat exists or not. Regardless of status
         int[,] exists = new int[m_RowCount, m_ColumnCount];
         for( int i = 0; i < m_RowCount; i++ )
            for( int j = 0; j < m_ColumnCount; j++ )
               if( m_InitialSeatStatus[i, j] == null )
                  exists[i, j] = 0;
               else
                  exists[i, j] = 1;

         //Create the array of seats that keeps track of its neighbours
         for( int i = 0; i < m_RowCount; i++ )
            for( int j = 0; j < m_ColumnCount; j++ )
               m_Seats[i, j] = new Seat( i, j, m_RowCount, m_ColumnCount, m_AdventOfCodePart, exists );

      }

      /// <summary>
      /// Initialize a new seating system with the default input
      /// </summary>
      public SeatingSystem( )
      {
         m_SeatStatus = new SEATSTATUS?[m_RowCount, m_ColumnCount];
         //Copy the initial seating status
         for( int i = 0; i < m_RowCount; i++ )
            for( int j = 0; j < m_ColumnCount; j++ )
               m_SeatStatus[i, j] = m_InitialSeatStatus[i, j];

      }

      /// <summary>
      /// Copy constructor
      /// </summary>
      /// <param name="oldSystem"></param>
      public SeatingSystem( SeatingSystem oldSystem )
      {
         m_SeatStatus = new SEATSTATUS?[m_RowCount, m_ColumnCount];
         for( int i = 0; i < m_RowCount; i++ )
            for( int j = 0; j < m_ColumnCount; j++ )
               m_SeatStatus[i, j] = oldSystem.SeatStatus[i, j];
      }

   #endregion

   /*PROPERTIES*/
   #region
      
      /// <summary>
      /// Gets the seating status for this system.
      /// </summary>
      public SEATSTATUS?[,] SeatStatus
      {
         get
         {
            return m_SeatStatus;
         }
      }


      /// <summary>
      /// Gets the number of occupied seats for the current state of this seating system
      /// </summary>
      public int OccupiedSeats
      {
         get
         {
            int occupiedSeats = 0;
            //Copy the initial seating status
            for( int i = 0; i < m_RowCount; i++ )
               for( int j = 0; j < m_ColumnCount; j++ )
                  if( m_SeatStatus[i, j] == SEATSTATUS.OCCUPIED )
                     occupiedSeats++;

            return occupiedSeats;
         }
      }

      /// <summary>
      /// Gets the number of occupied seats for the current state of this seating system
      /// </summary>
      public int VacantSeats
      {
         get
         {
            int occupiedSeats = 0;
            //Copy the initial seating status
            for( int i = 0; i < m_RowCount; i++ )
               for( int j = 0; j < m_ColumnCount; j++ )
                  if( m_SeatStatus[i, j] == SEATSTATUS.VACANT )
                     occupiedSeats++;

            return occupiedSeats;
         }
      }

      #endregion

   /*METHODS*/
      #region

      /// <summary>
      /// UPdate the seating status for this instance.
      /// </summary>
      public void UpdateSeatingStatusUntilStable( )
      {

      //Create a copy of the old seating status..
         SeatingSystem old = new SeatingSystem( this );

         bool nothingChanged = false;

         while( !nothingChanged )
         {
            old = new SeatingSystem( this );
            for( int i = 0; i < m_RowCount; i++ )
               for( int j = 0; j < m_ColumnCount; j++ )
                  m_SeatStatus[i, j] = GetNewStatus( old.SeatStatus, i, j );

         //Check for equality
            if( this.IsEqual( old.SeatStatus ) )
               nothingChanged = true;
         }



      }






      /// <summary>
      /// Check for equality with another seat status.
      /// </summary>
      /// <param name="comparer"></param>
      /// <returns></returns>
      public bool IsEqual( SEATSTATUS?[,] comparer )
      {
         for( int i = 0; i < m_RowCount; i++ )
            for( int j = 0; j < m_ColumnCount; j++ )
               if( m_SeatStatus[i, j] != comparer[i, j] )
                  return false;

         return true;
      }

      #endregion

      /*STATIC METHODS*/
      #region



      public static int CountStatusOfAdjacentSeats( SEATSTATUS?[,] state, int r, int c, SEATSTATUS? status )
      {
         int nOfStatus = 0;
         int[,] seatsToCheck = SeatingSystem.m_Seats[r, c].SeatsICareAbout;
         for( int i = 0; i < seatsToCheck.GetLength( 0 ); i++ )
         {
            if( state[seatsToCheck[i, 0], seatsToCheck[i, 1]] == status )
               nOfStatus++;
         }
         return nOfStatus;
      }


      public static SEATSTATUS? GetNewStatus( SEATSTATUS?[,] currentState, int rowIdx, int colIdx )
      {
         SEATSTATUS? thisSeatStatus = currentState[rowIdx, colIdx];
         if( thisSeatStatus == null )
            return null;

         if( thisSeatStatus == SEATSTATUS.VACANT )
         {
            int adjacentEmptySeats = CountStatusOfAdjacentSeats( currentState, rowIdx, colIdx, SEATSTATUS.OCCUPIED );
            if( adjacentEmptySeats == 0 )
               return SEATSTATUS.OCCUPIED;
            else
               return SEATSTATUS.VACANT;

         }
         else //Seatstatus is occupied
         {
            int adjacentOccupiedSeats = CountStatusOfAdjacentSeats( currentState, rowIdx, colIdx, SEATSTATUS.OCCUPIED );
            if( adjacentOccupiedSeats >= 5 )
               return SEATSTATUS.VACANT;
            else
               return SEATSTATUS.OCCUPIED;
         }
      }


      #endregion
   }
}
