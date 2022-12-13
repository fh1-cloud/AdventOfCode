using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

   #endregion

   /*PROPERTIES*/
   #region
      public int RowIdx { get { return m_RowIdx; } }
      public int ColIdx { get { return m_ColIdx; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   /*METHODS*/
   #region
   #endregion


   }

}
