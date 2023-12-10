using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCodeLib.Classes
{
   public class IntegerPair
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected int m_RowIdx = -1;
      protected int m_ColIdx = -1;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor..
      /// </summary>
      /// <param name="i"></param>
      /// <param name="j"></param>
      public IntegerPair( int i, int j )
      {
         m_RowIdx = i;
         m_ColIdx = j;
      }

      /// <summary>
      /// Copy constructor
      /// </summary>
      /// <param name="oldPair"></param>
      public IntegerPair( IntegerPair oldPair )
      {
         m_RowIdx = oldPair.m_RowIdx;
         m_ColIdx = oldPair.m_ColIdx;
      }

   #endregion

   /*PROPERTIES*/
   #region
      public int RowIdx { get { return m_RowIdx; } set { m_RowIdx = value; } }
      public int ColIdx { get { return m_ColIdx; } set { m_ColIdx = value; } }

   #endregion

   /*OPERATORS*/
   #region

      public static bool operator == ( IntegerPair lhs, IntegerPair rhs )
      {
         return ( lhs.m_RowIdx == rhs.m_RowIdx && lhs.m_ColIdx == rhs.m_ColIdx );
      }
      public static bool operator != ( IntegerPair lhs, IntegerPair rhs )
      {
         return !(lhs == rhs);
      }

   #endregion

   /*STATIC METHODS*/
   #region
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

      #endregion


   }

}
