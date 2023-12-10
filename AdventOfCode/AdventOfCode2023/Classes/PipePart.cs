using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class PipePart
   {
   /*MEMBERS*/
   #region
      protected char m_Symbol;
      protected bool m_IsOnLoop = false;
      protected bool m_IsInsideLoop = false;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public PipePart( char symbol )
      {
         m_Symbol = symbol;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public char C { get { return m_Symbol; } set { m_Symbol = value; } }
      public bool IsOnLoop { get{ return m_IsOnLoop; } set { m_IsOnLoop = value; } }
      public bool IsInsideLoop { get{ return m_IsInsideLoop; } set { m_IsInsideLoop = value; } }
   #endregion

   /*METHODS*/
   #region

      public bool SetInsideStatus( int rowIdx, int colIdx, PipePart[,] grid )
      {
         if( m_IsOnLoop )
            return false;
         if( !m_IsOnLoop && colIdx == grid.GetLength( 1 ) - 1 ) 
            return false;

      //Shoot a beam towards the right 
         List<PipePart> rightBeam = new List<PipePart>( );
         for( int i = colIdx+1; i < grid.GetLength( 1 ); i++ )
            rightBeam.Add( grid[rowIdx, i] );

      //Loop over list and count number of crossings..
         int nOfCrossings = 0;
         for( int i = 0; i<rightBeam.Count; i++ )
         {
            if( rightBeam[i].m_IsOnLoop ) 
            {
               if( rightBeam[i].C == '|' ) //If vertical wall, we know we crossed.
                  nOfCrossings++;
               else if( rightBeam[i].C == 'F' || rightBeam[i].C == 'L' )
               {
                  char crossChar = 'J';
                  if( rightBeam[i].C == 'L' )
                     crossChar = '7';

                  int currIdx = i;
                  while( true )
                  {
                     currIdx++;
                     if( rightBeam[currIdx].C == '-' )
                        continue;
                     else if( rightBeam[currIdx].C == crossChar )
                        nOfCrossings++;
                     i = currIdx;
                     break;
                  }
               }
            }
         }
      //We found the number of crossings. If odd, it is has to be inside 
         if( nOfCrossings % 2 == 1 )
            m_IsInsideLoop = true;
            
         return m_IsInsideLoop;
      }

      #endregion
   }
}
