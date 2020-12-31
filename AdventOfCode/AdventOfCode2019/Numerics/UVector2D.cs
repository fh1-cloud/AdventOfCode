using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Numerics
{


   /// <summary>
   /// A class that represents a vector (or point) in 2d space.
   /// </summary>
   public class UVector2D
   {

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region


      protected double m_X;
      protected double m_Y;

   #endregion

   /*CONSTRUCTORS*/
   #region


      /// <summary>
      /// Initializes a new 2D vector
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      public UVector2D( double x, double y )
      {
         m_X = x;
         m_Y = y;
      }


      /// <summary>
      /// Initializes a new vector with zero values
      /// </summary>
      public UVector2D()
      {
         m_X = 0.0;
         m_Y = 0.0;
      }

      /// <summary>
      /// Copy constructor
      /// </summary>
      /// <param name="old"></param>
      public UVector2D( UVector2D old )
      {
         m_X = old.X;
         m_Y = old.Y;
      }


   #endregion

   /*PROPERTIES*/
   #region

      /// <summary>
      /// Gets or sets the x component
      /// </summary>
      public double X
      {
         get
         {
            return m_X;
         }
         set
         {
            m_X = value;   
         }
      }


      /// <summary>
      /// Gets or sets the y component
      /// </summary>
      public double Y
      {
         get
         {
            return m_Y;
         }
         set
         {
            m_Y = value;   
         }
      }



   #endregion

   /*OPERATORS*/
   #region


      public static UVector2D operator - ( UVector2D lhs )
      {
         return new UVector2D( lhs.X * -1.0, lhs.Y * -1.0 );
      }

      public static UVector2D operator + ( UVector2D lhs, UVector2D rhs )
      {
         return new UVector2D( lhs.X + rhs.X, lhs.Y + rhs.Y );
      }

      public static UVector2D operator - ( UVector2D lhs, UVector2D rhs )
      {
         return new UVector2D( lhs.X - rhs.X, lhs.Y - rhs.Y );
      }

      public static double operator * ( UVector2D lhs, UVector2D rhs )
      {
         return lhs.X * rhs.X + lhs.Y * rhs.Y;
      }

      public static UVector2D operator * ( double a, UVector2D rhs )
      {
         return new UVector2D( a * rhs.X, a * rhs.Y );
      }

      public static UVector2D operator * ( UVector2D rhs, double a )
      {
         return a * rhs;
      }


   #endregion

   /*METHODS*/
   #region


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

   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Returns a vector of unit length in the x direction
      /// </summary>
      /// <returns></returns>
      public static UVector2D UnitX()
      {
         return new UVector2D( 1.0, 0.0 );
      }

      /// <summary>
      /// Returns a vector of unit length in the y direction.
      /// </summary>
      /// <returns></returns>
      public static UVector2D UnitY()
      {
         return new UVector2D( 0.0, 1.0 );
      }

      /// <summary>
      /// Gets a vector from a startpoint to an endpoint
      /// </summary>
      /// <param name="start"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public static UVector2D GetVectorFromPoints( UVector2D start, UVector2D end )
      {
         return new UVector2D( end.X - start.X, end.Y - start.Y );
      }


      /// <summary>
      /// Returns the intersection point between two lines with a start and endpoint
      /// </summary>
      /// <param name="v1Start"></param>
      /// <param name="v1End"></param>
      /// <param name="v2Start"></param>
      /// <param name="v2End"></param>
      /// <returns></returns>
      public static UVector2D IntersectionPointForPerpendicularLines( UVector2D v1Start, UVector2D v1End, UVector2D v2Start, UVector2D v2End )
      {
      //Creates the two vectors..
         UVector2D v1 = GetVectorFromPoints( v1Start, v1End );
         UVector2D v2 = GetVectorFromPoints( v2Start, v2End );

      //Check the dot product.
         double dot = v1 * v2;

      //The lines are not perendicular, and therefore have to be paralell because this only is a method for pure x or y vectors
         if( dot != 0.0 )
            return null;




         
         


      }

   #endregion




   }
}
