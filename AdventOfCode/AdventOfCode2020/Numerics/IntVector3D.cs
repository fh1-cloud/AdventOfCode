using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Numerics
{
   public class IntVector3D
   {

   /*MEMBERS*/
   #region

      protected long m_X = 0;
      protected long m_Y = 0;
      protected long m_Z = 0;
   #endregion


   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor.
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="z"></param>
      public IntVector3D( long x, long y, long z )
      {
         m_X = x;
         m_Y = y;
         m_Z = z;
      }


      /// <summary>
      /// Copy constructor
      /// </summary>
      /// <param name="oldVec"></param>
      public IntVector3D( IntVector3D oldVec )
      {
         m_X = oldVec.X;
         m_Y = oldVec.Y;
         m_Z = oldVec.Z;
      }



   #endregion


   /*PROPERTIES*/
   #region

      public long X => m_X;
      public long Y => m_Y;
      public long Z => m_Z;

      public string ID
      {
         get
         {
            return "X" + m_X.ToString( ) + "Y" + m_Y.ToString( ) + "Z" + m_Z.ToString( );
         }
      }

   #endregion

   /*OPERATORS*/
   #region

      /// <summary>
      /// Plus operator..
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static IntVector3D operator + ( IntVector3D lhs, IntVector3D rhs )
      {
         return new IntVector3D( lhs.X + rhs.X, lhs.Y+ rhs.Y, lhs.Z + rhs.Z );
      }


      /// <summary>
      /// Minus operator..
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static IntVector3D operator - ( IntVector3D lhs, IntVector3D rhs )
      {
         return new IntVector3D( lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z );
      }


   #endregion

   /*METHODS*/
   #region


      /// <summary>
      /// Checks if the input vector has the exact same components as this vector
      /// </summary>
      /// <param name="ov">The vector that this vector is compared to</param>
      /// <returns></returns>
      public bool IsEqual( IntVector3D ov )
      {
         return ( ov.X == this.X && ov.Y == this.Y && ov.Z == this.Z );
      }


   #endregion

   /*STATIC METHODS*/
   #region



   #endregion
   }
}
