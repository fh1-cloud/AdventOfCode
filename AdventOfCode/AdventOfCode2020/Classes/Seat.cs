using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class Seat
   {

      public int[,] m_OtherSeatsICareAbout;

      public Seat( int rIdx, int cIdx, int maxRows, int maxColumns, int adventOfCodePart, int[,] exists = null )
      {
         this.Row = rIdx;
         this.Column = cIdx;

         if( adventOfCodePart == 1 )
         {

            List<int> rows = new List<int>( );
            List<int> cols = new List<int>( );

            rows.Add( rIdx );
            cols.Add( cIdx );

            if( rIdx != 0 )
               rows.Add( rIdx - 1 );
            if( rIdx != maxRows - 1 )
               rows.Add( rIdx + 1 );
            if( cIdx != 0 )
               cols.Add( cIdx - 1 );
            if( cIdx != maxColumns - 1 )
               cols.Add( cIdx + 1 );

            m_OtherSeatsICareAbout = new int[rows.Count * cols.Count - 1, 2];
            int arrayCounter = 0;
            foreach( int row in rows )
            {
               foreach( int col in cols )
               {
                  if( !( row == rIdx && col == cIdx ) )
                  {
                     m_OtherSeatsICareAbout[arrayCounter, 0] = row;
                     m_OtherSeatsICareAbout[arrayCounter++, 1] = col;
                  }
               }
            }
         }
         else //AdventOfCodePart == 2
         {
            m_OtherSeatsICareAbout = FindSeatsICanSee( exists, rIdx, cIdx );
         }

 
      }

      public int Row { get; }
      public int Column { get; }

      public int[,] SeatsICareAbout
      {
         get
         {
            return m_OtherSeatsICareAbout;
         }
      }




      /// <summary>
      /// Collects an array of with the indexes of seats you can see in a particular direction
      /// </summary>
      /// <param name="seatsThatExists"></param>
      /// <param name="rowIdx"></param>
      /// <param name="colIdx"></param>
      /// <returns></returns>
      public static int[,] FindSeatsICanSee( int[,] seatsThatExists, int rowIdx, int colIdx )
      {

         List<Tuple<int, int>> seatsICanSee = new List<Tuple<int, int>>( );

      //Loop over all the directions..
         for( int i = 0; i < 8; i++ )
         {

         //Sets the modifiers to know where to look for seats
            int rowModifier = 0;
            int colModifier = 0;
            //Straight right
            if( i == 0 )
            {
               rowModifier = 0;
               colModifier = 1;
            }
            else if( i == 1 ) //Right upwards
            {
               rowModifier = -1;
               colModifier = 1;
            }
            else if( i == 2 )
            {
               rowModifier = -1;
               colModifier = 0;
            }
            else if( i == 3 )
            {
               rowModifier = -1;
               colModifier = -1;
            }
            else if( i == 4 )
            {
               rowModifier = 0;
               colModifier = -1;
            }
            else if( i == 5 )
            {
               rowModifier = 1;
               colModifier = -1;
            }
            else if( i == 6 )
            {
               rowModifier = 1;
               colModifier = 0;
            }
            else if( i == 7 )
            {
               rowModifier = 1;
               colModifier = 1;
            }

            int nextRowIdx = rowIdx;
            int nextColIdx = colIdx;

            List<int> dirSeats = new List<int>( );
            while( true )
            {
               nextRowIdx += rowModifier;
               nextColIdx += colModifier;

               if( nextRowIdx < 0 || nextRowIdx > seatsThatExists.GetLength( 0 ) - 1 )
                  break;
               else if( nextColIdx < 0 || nextColIdx > seatsThatExists.GetLength( 1 ) - 1 )
                  break;
               else
                  dirSeats.Add( seatsThatExists[nextRowIdx, nextColIdx] );
            }

         //Count the offset until i can see something..
            int? offSet = CountNumberOfPositionsUntilICanSeeSomething( dirSeats.ToArray( ) );
            if( offSet != null )
            {
               int offsetVal = ( int ) offSet;
               seatsICanSee.Add( new Tuple<int, int>( rowIdx + rowModifier * offsetVal, colIdx + colModifier * offsetVal ) );
            }
         }


      //Declare the array of seats that i can see..
         int[,] retSeatsIdx = new int[seatsICanSee.Count, 2];
         int rrr = 0;
         foreach( Tuple<int, int> t in seatsICanSee )
         {
            retSeatsIdx[rrr, 0] = t.Item1;
            retSeatsIdx[rrr, 1] = t.Item2;
            rrr++;
         }

      //Return the indexes of the seats i can see..
         return retSeatsIdx;

      }

      /// <summary>
      /// Gets an array of what a seat sees in a particular direction, then gets the offset to the first seat that exists in that direction
      /// </summary>
      /// <param name="dirSeats"></param>
      /// <returns></returns>
      public static int? CountNumberOfPositionsUntilICanSeeSomething( int[] dirSeats )
      {
         int offSet = 0;

         bool foundSomething = false;
         for( int i = 0; i < dirSeats.Length; i++ )
         {
            offSet++;
            if( IsNextASeat( dirSeats, i ) )
            {
               foundSomething = true;
               break;
            }
         }
         if( foundSomething )
            return offSet;
         else
            return null;
      }


      /// <summary>
      /// Checks if the next seat is a seat. If it is, return 1, if not, return 0;
      /// </summary>
      /// <param name="whatISee"></param>
      /// <param name="idx"></param>
      /// <returns></returns>
      public static bool IsNextASeat( int[] whatISee, int idx )
      {
         if( whatISee.Length - 1 - idx < 0 )
            return false;
         else if( whatISee[idx] == 0 )
            return false;
         else
            return true;
      }




   }
}
