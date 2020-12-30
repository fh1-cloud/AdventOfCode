using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class HexTile
   {

   /*ENUMS*/
   #region

      /// <summary>
      /// Represents a direction for the hexagon tiles.
      /// </summary>
      public enum HEXTILEDIRECTION
      {
         EAST,
         WEST,
         NORTHEAST,
         NORTHWEST,
         SOUTHEAST,
         SOUTHWEST
      }


      /// <summary>
      /// The color of this tile.
      /// </summary>
      public enum HEXCOLOR
      {
         WHITE,
         BLACK
      }



      #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected KeyValuePair<double, double> m_Coordinates;
      protected HEXCOLOR m_State;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Creates a hexagon tile from a series of strings..
      /// </summary>
      /// <param name="inp"></param>
      public HexTile( string inp )
      {
         int curIdx = 0;
         double rowIdx = 0;
         double colIdx = 0;
         while( curIdx < inp.Length )
         {
            char thisChar = inp[curIdx];
            if( thisChar == 'w' )
            {
               curIdx++;
               colIdx--;
            }
            else if( thisChar == 'e' )
            {
               curIdx++;
               colIdx++;
            }
            else if( thisChar == 'n' )
            {
               char nextChar = inp[curIdx + 1];
               rowIdx--;
               if( nextChar == 'w' )
               {
                  colIdx = colIdx - 0.5;
               }
               else if( nextChar == 'e' )
               {
                  colIdx = colIdx + 0.5;
               }
               else
                  throw new Exception( );

               curIdx++;
               curIdx++;
            }
            else if( thisChar == 's' )
            {
               char nextChar = inp[curIdx + 1];
               rowIdx++;
               if( nextChar == 'w' )
               {
                  colIdx = colIdx - 0.5;
               }
               else if( nextChar == 'e' )
               {
                  colIdx = colIdx + 0.5;
               }
               else
                  throw new Exception( );

               curIdx++;
               curIdx++;
            }
            else
               throw new Exception( );
         }

      //Set the location coordinates for this tile..
         m_Coordinates = new KeyValuePair<double, double>( rowIdx, colIdx );

      //Since it is the first time it is created. The tile is set to black;
         m_State = HEXCOLOR.BLACK;

      }


      /// <summary>
      /// Copy constructor
      /// </summary>
      /// <param name="oldTile"></param>
      public HexTile( HexTile oldTile )
      {
         m_Coordinates = new KeyValuePair<double, double>( oldTile.Coordinates.Key, oldTile.Coordinates.Value );
         m_State = oldTile.State;
      }

      /// <summary>
      /// Creates a hex tile with initialization of variables
      /// </summary>
      /// <param name="inpString"></param>
      /// <param name="state"></param>
      /// <param name="coordinates"></param>
      public HexTile( HEXCOLOR state, KeyValuePair<double, double> coordinates )
      {
         m_State = state;
         m_Coordinates = coordinates;
      }


   #endregion

   /*PROPERTIES*/
   #region


      /// <summary>
      /// Gets the current state for this tile.
      /// </summary>
      public HEXCOLOR State
      {
         get
         {
            return m_State;
         }
         set
         {
            m_State = value;
         }
      }


      /// <summary>
      /// Gets the placement for this tile
      /// </summary>
      public KeyValuePair<double,double> Coordinates
      {
         get
         {
            return m_Coordinates;
         }
      }



   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Swaps the state of this tile. Used if it is identified before.
      /// </summary>
      public void SwapState()
      {
         if( m_State == HEXCOLOR.BLACK )
            m_State = HEXCOLOR.WHITE;
         else if( m_State == HEXCOLOR.WHITE )
            m_State = HEXCOLOR.BLACK;
         else
            throw new Exception( );
      }


   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Gets the coordinates for a specific string input. Used to check if the same tile is identified or not Used to check if the same tile is identified or not
      /// </summary>
      /// <param name="inp"></param>
      /// <returns></returns>
      public static KeyValuePair<double,double> GetCoordinatesForTileInput( string inp )
      {

         int curIdx = 0;
         double rowIdx = 0;
         double colIdx = 0;
         while( curIdx < inp.Length )
         {
            char thisChar = inp[curIdx];
            if( thisChar == 'w' )
            {
               curIdx++;
               colIdx--;
            }
            else if( thisChar == 'e' )
            {
               curIdx++;
               colIdx++;
            }
            else if( thisChar == 'n' )
            {
               char nextChar = inp[curIdx + 1];
               rowIdx--;
               if( nextChar == 'w' )
               {
                  colIdx = colIdx - 0.5;
               }
               else if( nextChar == 'e' )
               {
                  colIdx = colIdx + 0.5;
               }
               else
                  throw new Exception( );

               curIdx++;
               curIdx++;
            }
            else if( thisChar == 's' )
            {
               char nextChar = inp[curIdx + 1];
               rowIdx++;
               if( nextChar == 'w' )
               {
                  colIdx = colIdx - 0.5;
               }
               else if( nextChar == 'e' )
               {
                  colIdx = colIdx + 0.5;
               }
               else
                  throw new Exception( );

               curIdx++;
               curIdx++;
            }
            else
               throw new Exception( );

         }

      //Return the coordinates.
         return new KeyValuePair<double, double>( rowIdx, colIdx );
      }



   #endregion



   }
}
