using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Numerics
{
   public class Vector2D
   {



      protected double m_X = 0.0;
      protected double m_Y = 0.0;


      public Vector2D( double x, double y )
      {
         m_X = x;
         m_Y = y;
      }

      public Vector2D( Vector2D old )
      {
         m_X = old.m_X;
         m_Y = old.m_Y;
      }

      public double X => m_X;
      public double Y => m_Y;


      public static Vector2D operator + ( Vector2D lhs, Vector2D rhs )
      {
         return new Vector2D( lhs.X + rhs.X, lhs.Y+ rhs.Y );
      }

      public static Vector2D operator - ( Vector2D lhs, Vector2D rhs )
      {
         return new Vector2D( lhs.X - rhs.X, lhs.Y - rhs.Y );
      }

      public static double operator * ( Vector2D lhs, Vector2D rhs )
      {
         double dorProd = lhs.X * rhs.X + lhs.Y * rhs.Y;
         return dorProd;
      }

      public static Vector2D operator * ( double lhs, Vector2D rhs )
      {
         return new Vector2D( lhs * rhs.X, lhs * rhs.Y );
      }
      public static Vector2D operator * ( Vector2D lhs, double rhs )
      {
         return rhs * lhs; 
      }


      public static Vector2D operator * ( Matrix2x2 lhs, Vector2D rhs )
      {
         double t = lhs[0, 0] * rhs.X + lhs[0, 1] * rhs.Y;
         double b = lhs[1, 0] * rhs.X + lhs[1, 1] * rhs.Y;
         return new Vector2D( t, b );
      }


      public double GetLength( )
      {
         double t = Math.Pow( m_X, 2.0 ) + Math.Pow( m_Y, 2.0 );
         return Math.Sqrt( t );
      }







   }
}
