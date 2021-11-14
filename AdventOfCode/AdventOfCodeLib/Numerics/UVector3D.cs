using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Numerics
{

   public class UVector3D
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
      protected double m_Z = 0.0;
   #endregion

   /*CONSTRUCTORS*/
   #region


      /// <summary>
      /// Default constructor
      /// </summary>
      public UVector3D( )
      {

      }

      /// <summary>
      /// Constructor from two start points
      /// </summary>
      /// <param name="startPoint"></param>
      /// <param name="endPoint"></param>
      public UVector3D( UVector3D startPoint, UVector3D endPoint )
      {
         m_X = endPoint.m_X - startPoint.m_X;
         m_Y = endPoint.m_Y - startPoint.m_Y;
         m_Z = endPoint.m_Z - startPoint.m_Z;
      }

      /// <summary>
      /// Set constructor
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="z"></param>
      public UVector3D( double x, double y, double z )
      {
         m_X = x;
         m_Y = y;
         m_Z = z;
      }

      /// <summary>
      /// copy constructor
      /// </summary>
      /// <param name="oldSet"></param>
      public UVector3D( UVector3D oldSet )
      {
         m_X = oldSet.m_X;
         m_Y = oldSet.m_Y;
         m_Z = oldSet.m_Z;
      }
   #endregion

   /*PROPERTIES*/
   #region

      /// <summary>
      /// Get the x coordinate
      /// </summary>
      public double X
      {
         get{ return m_X; }
         set{ m_X = value; }
      }

      /// <summary>
      /// Get the Y coordinate
      /// </summary>
      public double Y
      {
         get{ return m_Y; }
         set{ m_Y = value; }
      }

      /// <summary>
      /// Get the z coordinate
      /// </summary>
      public double Z
      {
         get{ return m_Z; }
         set{ m_Z = value; }
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
      public static UVector3D operator + ( UVector3D lhs, UVector3D rhs )
      {
         return new UVector3D( lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z );
      }

      /// <summary>
      /// Subtraction operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static UVector3D operator - ( UVector3D lhs, UVector3D rhs )
      {
         return new UVector3D( lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z );
      }


      /// <summary>
      /// Minus single vector operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <returns></returns>
      public static UVector3D operator - ( UVector3D lhs )
      {
         return new UVector3D( -lhs.X, -lhs.Y, -lhs.Z );
      }

      /// <summary>
      /// Dot product operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static double operator * (UVector3D lhs, UVector3D rhs )
      {
         return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
      }

      /// <summary>
      /// Constant premultiplier operator
      /// </summary>
      /// <param name="a"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static UVector3D operator * ( double a, UVector3D rhs )
      {
         return new UVector3D( a * rhs.X, a * rhs.Y, a * rhs.Z );
      }

      /// <summary>
      /// Constant post multiplier operator
      /// </summary>
      /// <param name="rhs"></param>
      /// <param name="a"></param>
      /// <returns></returns>
      public static UVector3D operator * ( UVector3D rhs, double a )
      {
         return a * rhs;
      }
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Gets the length of the vector
      /// </summary>
      /// <returns></returns>
      public double GetLength()
      {
         return Math.Pow( Math.Pow( m_X, 2.0 ) + Math.Pow( m_Y, 2.0 ) + Math.Pow( m_Z, 2.0 ), 0.5 );
      }

      /// <summary>
      /// Normalize this vector
      /// </summary>
      /// <returns></returns>
      public UVector3D Normalize( )
      {
         double l = this.GetLength( );
         return new UVector3D( m_X/l, m_Y/l, m_Z/l );
      }


   #endregion

   /*STATIC METHODS*/
   #region
      
      /// <summary>
      /// Gets a unit vector in the x direction
      /// </summary>
      /// <returns></returns>
      public static UVector3D UnitX( )
      {
         return new UVector3D( 1.0, 0.0, 0.0 );
      }

      /// <summary>
      /// Gets a unit vector in the y direction
      /// </summary>
      /// <returns></returns>
      public static UVector3D UnitY( )
      {
         return new UVector3D( 0.0, 1.0, 0.0 );
      }

      /// <summary>
      /// Gets a unit vector in the z direction.
      /// </summary>
      /// <returns></returns>
      public static UVector3D UnitZ( )
      {
         return new UVector3D( 0.0, 0.0, 1.0 );
      }

   #endregion

   }
}
