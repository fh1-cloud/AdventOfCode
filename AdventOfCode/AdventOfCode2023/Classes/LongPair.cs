using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class LongPair
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected long m_RowIdx = -1;
      protected long m_ColIdx = -1;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor..
      /// </summary>
      /// <param name="i"></param>
      /// <param name="j"></param>
      public LongPair( long i, long j )
      {
         m_RowIdx = i;
         m_ColIdx = j;
      }

      /// <summary>
      /// Copy constructor
      /// </summary>
      /// <param name="oldPair"></param>
      public LongPair( LongPair oldPair )
      {
         m_RowIdx = oldPair.m_RowIdx;
         m_ColIdx = oldPair.m_ColIdx;
      }

   #endregion

   /*PROPERTIES*/
   #region
      public long RowIdx { get { return m_RowIdx; } set { m_RowIdx = value; } }
      public long ColIdx { get { return m_ColIdx; } set { m_ColIdx = value; } }

   #endregion

   /*OPERATORS*/
   #region

      public static bool operator == ( LongPair lhs, LongPair rhs )
      {
         return ( lhs.m_RowIdx == rhs.m_RowIdx && lhs.m_ColIdx == rhs.m_ColIdx );
      }
      public static bool operator != ( LongPair lhs, LongPair rhs )
      {
         return !(lhs == rhs);
      }

   #endregion

   /*STATIC METHODS*/
   #region
      public static long Det( LongPair v1, LongPair v2 )
      {
         return v1.ColIdx*v2.RowIdx - v2.ColIdx*v1.RowIdx;
      }
   #endregion

   /*METHODS*/
   #region



      public override bool Equals( object obj )
      {
         return base.Equals( obj );
      }

      public override int GetHashCode( )
      {
         return base.GetHashCode( );
      }

      public override string ToString( )
      {
         return new StringBuilder( "RowIdx: " + m_RowIdx.ToString( ) + ", ColIdx: " + m_ColIdx.ToString( ) ).ToString( );
      }

   }
      #endregion
}
