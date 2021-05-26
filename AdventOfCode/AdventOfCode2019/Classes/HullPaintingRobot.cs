using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{
   public class HullPaintingRobot
   {
   /*ENUMS*/
   #region

      public enum DIRECTION
      {
         UP = 0,
         LEFT = 1,
         DOWN = 2,
         RIGHT = 3,
      }


      public enum COLOR
      {
         BLACK = 0,
         WHITE = 1
      }
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected DIRECTION m_CurrentDirection = DIRECTION.UP;

      protected long m_PositionX = 0;
      protected long m_PositionY = 0;

      protected HashSet<string> m_PlacesPaintedAtLeastOnce = new HashSet<string>( );

   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Initializes a hull painting robot with a starting position
      /// </summary>
      /// <param name="startPosition"></param>
      public HullPaintingRobot( long x, long y ) 
      {
         m_PositionX = x;
         m_PositionY = y;
      }

   #endregion

   /*PROPERTIES*/
   #region

      public HashSet<string> PlacesPainted => m_PlacesPaintedAtLeastOnce;
      public long X => m_PositionX;
      public long Y => m_PositionY;

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Turns the robot and returns the new direction.
      /// </summary>
      /// <param name="i"></param>
      /// <returns></returns>
      public DIRECTION Turn( long i )
      {
         if( i == 0 ) //LEFT 90 DEGREES
         {
            if( m_CurrentDirection == DIRECTION.RIGHT )
               m_CurrentDirection = DIRECTION.UP;
            else
               m_CurrentDirection++;
         }
         else if( i == 1 ) //TURN RIGHT 90 DEGREES
         {
            if( m_CurrentDirection == DIRECTION.UP )
               m_CurrentDirection = DIRECTION.RIGHT;
            else
               m_CurrentDirection--;
         }
         else
            throw new Exception( );

         return m_CurrentDirection;
      }


      /// <summary>
      /// Instructrs the painting robot to go forward, and returns the new position
      /// </summary>
      /// <returns></returns>
      public COLOR GoForward( COLOR[,] canvas )
      {
      //The robot moves forward.
         if( m_CurrentDirection == DIRECTION.UP )
            m_PositionY++;
         else if( m_CurrentDirection == DIRECTION.LEFT )
            m_PositionX--;
         else if( m_CurrentDirection == DIRECTION.RIGHT )
            m_PositionX++;
         else if( m_CurrentDirection == DIRECTION.DOWN )
            m_PositionY--;
         else
            throw new Exception( );

         return canvas[ m_PositionX, m_PositionY ];
      }

      /// <summary>
      /// Returns the color for the current position.
      /// </summary>
      /// <param name="i">The value from the intcode computer at this position</param>
      /// <param name="atCoordinate">The coordinate that was just painted</param>
      /// <returns></returns>
      public COLOR PaintPosition( long i, COLOR[,] canvas)
      {

         if( i == 0 )
         {
            canvas[m_PositionX,m_PositionY] = COLOR.BLACK;
            return COLOR.BLACK;
         }
         else if( i == 1 )
         {
            string currentPos = GetStringRepresentationForCoordinate( new UVector2D( m_PositionX, m_PositionY ) );
            if( !m_PlacesPaintedAtLeastOnce.Contains( currentPos ) )
               m_PlacesPaintedAtLeastOnce.Add( currentPos );

            canvas[m_PositionX,m_PositionY] = COLOR.WHITE;
            return COLOR.WHITE;
         }
         else
            throw new Exception( );
      }

      public void UpdateMinMaxPosition( long xMin, long xMax, long yMin, long yMax, out long newXMin, out long newXMax, out long newYMin, out long newYMax )
      {
         newXMin = Math.Min( xMin, m_PositionX );
         newXMax = Math.Max( xMax, m_PositionX );
         newYMin = Math.Min( yMin, m_PositionY );
         newYMax = Math.Max( yMax, m_PositionY );
      }

   #endregion

   /*STATIC METHODS*/
   #region

      public static string GetStringRepresentationForCoordinate( UVector2D coord )
      {
         return "("+ coord.X.ToString( "0." ) + "," + coord.Y.ToString( "0." ) + ")";
      }
      



   #endregion

   }
}
