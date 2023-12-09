using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class OasisNumberSeries
   {

   /*MEMBERS*/
   #region
      protected List<long> m_Numbers = new List<long>();
      protected OasisNumberSeries m_Child = null;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Creates an oasis number series from an input of parent numbers. Will continue to create children until the rows are zero.
      /// </summary>
      /// <param name="parentNumbers"></param>
      public OasisNumberSeries( List<long> parentNumbers )
      {
         bool allZeros = true;
         for( int i = 0; i<parentNumbers.Count-1; i++ )
         {
            m_Numbers.Add( parentNumbers[i+1] - parentNumbers[i] );
            if( allZeros && m_Numbers.LastOrDefault( ) != 0 )
               allZeros = false;
         }
         if( !allZeros )
            m_Child = new OasisNumberSeries( m_Numbers );
      }

      /// <summary>
      /// Creates an oasis number series from the first input row.
      /// </summary>
      /// <param name="firstRow"></param>
      public OasisNumberSeries( string firstRow )
      {
         string[] spl = firstRow.Split( new char[] { ' ' } );
         foreach( string s in spl )
            m_Numbers.Add( long.Parse( s ) );

         m_Child = new OasisNumberSeries( m_Numbers );
      }
   #endregion

   /*METHODS*/
   #region

      public long AddValueToSeries( bool atStart = true )
      {
         long retNum = m_Child == null ? 0 : m_Numbers[atStart ? 0 : m_Numbers.Count - 1] + ( atStart ? -1 : 1 ) * m_Child.AddValueToSeries( atStart );
         m_Numbers.Insert( atStart ? 0 : m_Numbers.Count, retNum );
         return retNum;
      }
   #endregion



   }
}
