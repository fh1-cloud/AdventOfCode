using AdventOfCodeLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Numerics
{

   /// <summary>
   /// A class that represents a vector
   /// </summary>
   public class UVector2D
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected double m_X = 0.0;
      protected double m_Y = 0.0;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor
      /// </summary>
      public UVector2D( )
      {

      }

      /// <summary>
      /// Creates a vector from two points
      /// </summary>
      /// <param name="p1"></param>
      /// <param name="p2"></param>
      public UVector2D( UVector2D startPoint, UVector2D endPoint )
      {
         m_X = endPoint.m_X - startPoint.m_X;
         m_Y = endPoint.m_Y - startPoint.m_Y;
      }

      /// <summary>
      /// Set constructor
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      public UVector2D( double x, double y )
      {
         m_X = x;
         m_Y = y;
      }

      /// <summary>
      /// Copy constructor
      /// </summary>
      /// <param name="oldSet"></param>
      public UVector2D( UVector2D oldSet )
      {
         m_X = oldSet.X;
         m_Y = oldSet.Y;
      }


   #endregion

   /*PROPERTIES*/
   #region


      public double X
      {
         get{ return m_X; }
         set{ m_X = value; }
      }

      public double Y
      {
         get{ return m_Y; }
         set{ m_Y = value; }
      }

   #endregion

   /*OPERATORS*/
   #region


      /// <summary>
      /// Addition operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static UVector2D operator + ( UVector2D lhs, UVector2D rhs )
      {
         return new UVector2D( lhs.X + rhs.X, lhs.Y + rhs.Y );
      }


      /// <summary>
      /// Minus operator for a single vector.
      /// </summary>
      /// <param name="lhs"></param>
      /// <returns></returns>
      public static UVector2D operator - ( UVector2D lhs )
      {
         return new UVector2D( -lhs.X, -lhs.Y );
      }

      /// <summary>
      /// Subtraction operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static UVector2D operator - ( UVector2D lhs, UVector2D rhs )
      {
         return new UVector2D( lhs.X - rhs.X, lhs.Y - rhs.Y );
      }

      /// <summary>
      /// Dot product operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static double operator * (UVector2D lhs, UVector2D rhs )
      {
         return lhs.X * rhs.X + lhs.Y * rhs.Y;
      }

      /// <summary>
      /// Constant premultiplier operator
      /// </summary>
      /// <param name="a"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static UVector2D operator * ( double a, UVector2D rhs )
      {
         return new UVector2D( a * rhs.X, a * rhs.Y );
      }

      /// <summary>
      /// Constant post multiplier operator
      /// </summary>
      /// <param name="rhs"></param>
      /// <param name="a"></param>
      /// <returns></returns>
      public static UVector2D operator * ( UVector2D rhs, double a )
      {
         return a * rhs;
      }
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Returns the cross product between this vector and the vector passed.
      /// </summary>
      /// <param name="crossedWith"></param>
      /// <returns></returns>
      public double CrossProduct( UVector2D crossedWith )
      {
      //Validation
         if( crossedWith == null )
            return double.NaN;

      //Calculate cross product and return.
         return m_X * crossedWith.m_Y - m_Y * crossedWith.m_X;
      }

      /// <summary>
      /// Gets the manhattan length of this vector
      /// </summary>
      /// <returns></returns>
      public double GetManhattanLength( )
      {
         return Math.Abs( m_X ) + Math.Abs( m_Y );
      }

      /// <summary>
      /// Gets the length of this vector
      /// </summary>
      /// <returns></returns>
      public double GetLength()
      {
         return Math.Pow( Math.Pow( m_X, 2.0 ) + Math.Pow( m_Y, 2.0 ), 0.5 );
      }


      /// <summary>
      /// Normalizes this vector.
      /// </summary>
      public UVector2D Normalize( )
      {
         double l = this.GetLength( );
         return new UVector2D( m_X/l, m_Y/l );
      }

   #endregion

   /*STATIC METHODS*/
   #region


      /// <summary>
      /// Gets a vector from two points in space.
      /// </summary>
      /// <param name="p1">Point 1.</param>
      /// <param name="p2">Point 2</param>
      /// <returns></returns>
      public static UVector2D GetVectorFromPoints( UVector2D p1, UVector2D p2 )
      {
      //Validation
         if( p1 == null || p2 == null )
            return null;

      //Calculate vector and return.
         return new UVector2D( p2.X - p1.X, p2.Y - p1.Y );
      }

      /// <summary>
      /// Gets a unit vector in the x direction.
      /// </summary>
      /// <returns></returns>
      public static UVector2D UnitX()
      {
         return new UVector2D( 1.0, 0.0 );
      }

      /// <summary>
      /// Gets a unit vector in the Y direction.
      /// </summary>
      /// <returns></returns>
      public static UVector2D UnitY()
      {
         return new UVector2D( 0.0, 1.0 );
      }


      /// <summary>
      /// Checks if two UVector2D lines intersect. If so return true and sets the intersection point as an out.
      /// </summary>
      /// <param name="point1Start"></param>
      /// <param name="point1End"></param>
      /// <param name="point2Start"></param>
      /// <param name="point2End"></param>
      /// <param name="intersectionPoint"></param>
      /// <returns></returns>
      public static bool DoesLinesIntersect( UVector2D point1Start, UVector2D point1End, UVector2D point2Start, UVector2D point2End, bool inclusiveEnds, out UVector2D intersectionPoint )
      {
         intersectionPoint = null;
         
      //Calculate direction vectors
         UVector2D ad = new UVector2D( point1Start, point1End );
         UVector2D bd = new UVector2D( point2Start, point2End );

      //Check determinant
         double det = bd.X * ad.Y - bd.Y * ad.X;
         if( det.IsZero( ) )
            return false;

      //Calculate u and v variables
      //https://stackoverflow.com/questions/2931573/determining-if-two-rays-intersect
         double u = ( ( point2Start.Y - point1Start.Y ) * bd.X - ( point2Start.X - point1Start.X ) * bd.Y ) / ( bd.X * ad.Y - bd.Y * ad.X );
         double v = ( ( point2Start.Y - point1Start.Y ) * ad.X - ( point2Start.X - point1Start.X ) * ad.Y ) / ( bd.X * ad.Y - bd.Y * ad.X );

      //Checks for intersection and calculate.
         if( inclusiveEnds )
         {
            if( u >= 0.0 && v >= 0.0 && u <= 1.0 && v <= 1.0 )
            {
               double intX = point1Start.X + ad.X * u;
               double intY = point1Start.Y + ad.Y * u;
               intersectionPoint = new UVector2D( intX, intY );
               return true;
            }
            else
               return false;
         }
         else
         {
            if( u > 0.0 && v > 0.0 && u < 1.0 && v < 1.0 )
            {
               double intX = point1Start.X + ad.X * u;
               double intY = point1Start.Y + ad.Y * u;
               intersectionPoint = new UVector2D( intX, intY );
               return true;
            }
            else
               return false;
         }

      }

   #endregion


   }
}
 