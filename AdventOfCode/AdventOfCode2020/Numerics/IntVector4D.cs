using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{


   public class IntVector4D
   {

   /*MEMBERS*/
   #region

      protected long m_X = 0;
      protected long m_Y = 0;
      protected long m_Z = 0;
      protected long m_W = 0;
      #endregion


   /*CONSTRUCTORS*/
      #region

      /// <summary>
      /// Default constructor.
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="z"></param>
      public IntVector4D( long x, long y, long z, long w )
      {
         m_X = x;
         m_Y = y;
         m_Z = z;
         m_W = w;
      }


      /// <summary>
      /// Copy constructor
      /// </summary>
      /// <param name="oldVec"></param>
      public IntVector4D( IntVector4D oldVec )
      {
         m_X = oldVec.X;
         m_Y = oldVec.Y;
         m_Z = oldVec.Z;
         m_W = oldVec.W;
      }



   #endregion


   /*PROPERTIES*/
   #region

      public long X => m_X;
      public long Y => m_Y;
      public long Z => m_Z;
      public long W => m_W;

      public string ID
      {
         get
         {
            return "X" + m_X.ToString( ) + "Y" + m_Y.ToString( ) + "Z" + m_Z.ToString( ) + "W" + m_W.ToString( );
         }
      }

   #endregion

   /*OPERATORS*/
   #region

   #endregion

   /*METHODS*/
   #region


      /// <summary>
      /// Checks if the input vector has the exact same components as this vector
      /// </summary>
      /// <param name="ov">The vector that this vector is compared to</param>
      /// <returns></returns>
      public bool IsEqual( IntVector4D ov )
      {
         return ( ov.X == this.X && ov.Y == this.Y && ov.Z == this.Z && ov.W == this.W );
      }

   #endregion

   /*STATIC METHODS*/
   #region



   #endregion
   }

}
