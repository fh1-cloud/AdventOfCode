using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class TreeWithPosition
   {

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*ENUMS*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected int m_RowIdx = -1;
      protected int m_ColIdx = -1;
      protected int m_Height = -1;
      protected bool m_IsVisible = false;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public TreeWithPosition( int height, int rowIdx, int colIdx )
      {
         m_Height = height;
         m_RowIdx = rowIdx;
         m_ColIdx = colIdx;
      }
   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region
      public void SetIsVisible( TreeWithPosition[,] allTrees )
      {
      //Set visibility of outer perimeter..
         if( m_RowIdx == 0 || m_RowIdx == allTrees.Length - 1 || m_ColIdx == 0 || m_ColIdx == allTrees.Length - 1 ) //Set visibility of outer perimeter..
         {
            m_IsVisible = true;
            return;
         }

      //Extract rows and colums..
         TreeWithPosition[] currRow = new TreeWithPosition[ allTrees.GetLength( 0 ) ];
         TreeWithPosition[] currCol = new TreeWithPosition[ allTrees.GetLength( 1 ) ];
         for( int i = 0; i < allTrees.GetLength( 0 ); i++ )
            currCol[i] = allTrees[i, m_ColIdx];
         for( int i = 0; i < allTrees.GetLength( 1 ); i++ )
            currRow[i] = allTrees[m_RowIdx, i];

      //Start from this index and check outwards..
         for( int i = m_RowIdx; i>=0; i-- )
         {


         }

      }
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   }
}
