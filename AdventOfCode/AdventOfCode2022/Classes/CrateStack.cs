using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class CrateStack
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected Stack<char> m_Stack = new Stack<char>();
      protected int m_Idx = -1;
   #endregion

   /*CONSTRUCTORS*/
   #region



      public CrateStack( int idx )
      {
         m_Idx = idx;
      }

   #endregion

   /*PROPERTIES*/
   #region
      public int ID { get { return m_Idx; } }
      public Stack<char> Stack { get { return m_Stack; } }
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
