using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class CamelCard
   {

   /*MEMBERS*/
   #region
      protected static Dictionary<char,int> m_RelValue = new Dictionary<char, int>( ); //Maps the relative value to the comparer.
   #endregion

   /*CONSTRUCTORS*/
   #region

      //Static constructor
      static CamelCard( )
      {
         m_RelValue.Add( 'A', 13 );
         m_RelValue.Add( 'K', 12 );
         m_RelValue.Add( 'Q', 11 );
         m_RelValue.Add( 'T', 10 );
         m_RelValue.Add( '9', 9 );
         m_RelValue.Add( '8', 8 );
         m_RelValue.Add( '7', 7 );
         m_RelValue.Add( '6', 6 );
         m_RelValue.Add( '5', 5 );
         m_RelValue.Add( '4', 4 );
         m_RelValue.Add( '3', 3 );
         m_RelValue.Add( '2', 2 );
         m_RelValue.Add( 'J', 1 );
      }

      //Default constructor
      public CamelCard( char character  )
      {
         this.C = character;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public char C { get; set; }
   #endregion

   /*OPERATORS*/
   #region
      public static bool operator == ( CamelCard c1, CamelCard c2 ) { return m_RelValue[ c1.C ] == m_RelValue[ c2.C ]; }
      public static bool operator != ( CamelCard c1, CamelCard c2 ) { return !(c1.C == c2.C); }
      public static bool operator > ( CamelCard c1, CamelCard c2 ) { return ( m_RelValue[ c1.C ] > m_RelValue[ c2.C ]); }
      public static bool operator < ( CamelCard c1, CamelCard c2 ) { return ( m_RelValue[ c1.C ] < m_RelValue[ c2.C ]); }
      public static bool operator >= ( CamelCard c1, CamelCard c2 ) { return ( m_RelValue[ c1.C ] >= m_RelValue[ c2.C ]); }
      public static bool operator <= ( CamelCard c1, CamelCard c2 ) { return ( m_RelValue[ c1.C ] <= m_RelValue[ c2.C ]); }
   #endregion

   /*METHODS*/
   #region
      public override bool Equals( object obj ) { return base.Equals( obj ); }
      public override int GetHashCode( ) { return base.GetHashCode( ); }
      public override string ToString( ) { return this.C.ToString( ); }
   #endregion
   }
}
