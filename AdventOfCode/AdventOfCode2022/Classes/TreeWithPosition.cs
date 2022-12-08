using System;
using System.Collections.Generic;
using System.Data;
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
      protected long m_ScenicScore = -1;
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
      public bool IsVisible { get { return m_IsVisible; } }
      public long ScenicScore { get { return m_ScenicScore; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region


      /// <summary>
      /// Sets the scenic score for this tree..
      /// </summary>
      /// <param name="forest"></param>
      public void SetScenicScore( TreeWithPosition[,] forest )
      {
      //Extract rows and colums..
         TreeWithPosition[] currRow = new TreeWithPosition[ forest.GetLength( 0 ) ];
         TreeWithPosition[] currCol = new TreeWithPosition[ forest.GetLength( 1 ) ];
         for( int i = 0; i < forest.GetLength( 0 ); i++ )
            currCol[i] = forest[i, m_ColIdx];
         for( int i = 0; i < forest.GetLength( 1 ); i++ )
            currRow[i] = forest[m_RowIdx, i];

      //Calculate the scores..
         long upwardsScore = 0;
         for( int i = m_RowIdx-1; i>=0; i-- )
         {
            if( currCol[i].m_Height < m_Height )
               upwardsScore++;
            else
            {
               upwardsScore++;
               break;
            }
         }
         long downWardsScore = 0;
         for( int i = m_RowIdx+1; i<currCol.Length; i++ )
         {
            if( currCol[i].m_Height < m_Height )
               downWardsScore++;
            else
            {
               downWardsScore++;
               break;
            }
         }

         long leftScore = 0;
         for( int i = m_ColIdx-1; i>=0; i-- )
         {
            if( currRow[i].m_Height < m_Height )
               leftScore++;
            else
            {
               leftScore++;
               break;
            }
         }
         long rightScore = 0;
         for( int i = m_ColIdx+1; i<currRow.Length; i++ )
         {
            if( currRow[i].m_Height < m_Height )
               rightScore++;
            else
            {
               rightScore++;
               break;
            }
         }

      //Calculate and set the scenic score..
         m_ScenicScore = downWardsScore * upwardsScore * leftScore * rightScore;

      }

      /// <summary>
      /// Sets the visible status for this tree.
      /// </summary>
      /// <param name="allTrees"></param>
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
         bool canSeeUpwards = true;
         for( int i = m_RowIdx-1; i>=0; i-- )
            if( currCol[i].m_Height >= m_Height )
               canSeeUpwards = false;
         bool canSeeDownwards = true;
         for( int i = m_RowIdx+1; i<currCol.Length; i++ )
            if( currCol[i].m_Height >= m_Height )
               canSeeDownwards = false;
         bool canSeeLeft = true;
         for( int i = m_ColIdx-1; i>=0; i-- )
            if( currRow[i].m_Height >= m_Height )
               canSeeLeft = false;
         bool canSeeRIght = true;
         for( int i = m_ColIdx+1; i<currRow.Length; i++ )
            if( currRow[i].m_Height >= m_Height )
               canSeeRIght = false;

      //Set the visible status..
         m_IsVisible = ( canSeeUpwards || canSeeDownwards || canSeeLeft || canSeeRIght );
      }
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   }
}
