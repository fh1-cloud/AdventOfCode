using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class TiltBoard
   {
   /*ENUMS*/
   #region
      public enum TILDIRECTION
      {
         NORTH,
         SOUTH,
         EAST,
         WEST
      }
   #endregion

   /*MEMBERS*/
   #region
      protected HashSet<Tuple<int, int>> m_CubeRocks = new HashSet<Tuple<int, int>>(); //true == rock
      protected HashSet<Tuple<int, int>> m_RoundRocks = new HashSet<Tuple<int, int>>( );

      protected int m_ColDim = -1;
      protected int m_RowDim = -1;

      protected HashSet<string> m_States = new HashSet<string>();
      protected List<long> m_Cycles = new List<long>( );
      protected string m_FirstCycleState = null;
      protected long m_CycleCounter = 0;
      protected long m_Iterations = 0;
      protected long m_IterationsBeforeCycling = 0;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public TiltBoard( string[] board )
      {
         m_ColDim = board[0].Length;
         m_RowDim = board.Length;
         for( int rowIdx = 0; rowIdx < board.Length; rowIdx++ )
         {
            for( int colIdx = 0; colIdx < board[rowIdx].Length; colIdx++ )
            {
               if( board[rowIdx][colIdx] == 'O' )
                  m_RoundRocks.Add( Tuple.Create( rowIdx, colIdx ) );
               if( board[rowIdx][colIdx] == '#' )
                  m_CubeRocks.Add( Tuple.Create( rowIdx, colIdx ) );
            }
         }
      }
   #endregion

      protected string GetStateString( )
      {
         StringBuilder sb = new StringBuilder( );
         for( int i = 0; i< m_RowDim; i++ )
         {
            for( int j = 0; j<m_ColDim; j++ )
            {
               if( m_CubeRocks.Contains( Tuple.Create( i, j ) ) )
                  sb.Append( '#' );
               else if( m_RoundRocks.Contains( Tuple.Create( i, j ) ) )
                  sb.Append( 'O' );
               else
                  sb.Append( '.' );
            }
            sb.Append( "\n" );
         }
         return sb.ToString( );
      }

      protected void Cycle( )
      {
         Tilt( TILDIRECTION.NORTH );
         Tilt( TILDIRECTION.WEST );
         Tilt( TILDIRECTION.SOUTH );
         Tilt( TILDIRECTION.EAST );

         m_Iterations++;
         string state = GetStateString( );
         if( !m_States.Contains( GetStateString( ) ) )
         {
            m_States.Add( state );
         }
         else //This state was found previously
         {
            if( m_FirstCycleState == null )
            {
               m_IterationsBeforeCycling = m_Iterations;
               m_FirstCycleState = state;
               m_CycleCounter = 0;
            }
            else if( m_FirstCycleState == state ) 
            {
               m_Cycles.Add( m_CycleCounter );
               Console.WriteLine( m_CycleCounter ); //Write the cycle to console.
               m_CycleCounter = 0;
            }
            m_CycleCounter++;
         }
      }

      protected long FindCycle( )
      {
         while( m_Cycles.Count < 10 )
            Cycle( );

         bool isEqual = true;
         for( int i = 0; i<m_Cycles.Count; i++ )
            isEqual &= ( m_Cycles[0] == m_Cycles[i] );

         if( !isEqual )
            throw new Exception( );
         return m_Cycles[0];
      }

      public long FindState( int targetCycles )
      {
         long cycle = FindCycle( );
         long restCycles = ( targetCycles - m_IterationsBeforeCycling ) % cycle;
         for( int i = 0; i<restCycles; i++ )
            Cycle( );
         return GetWeightOnNorthBeam( );
      }

      protected long GetWeightOnNorthBeam( )
      {
         long ans1 = 0;
         foreach( Tuple<int, int> t in m_RoundRocks )
            ans1 += (m_RowDim - t.Item1);
         return ans1;
      }


      public void Tilt( TILDIRECTION direction )
      {
         if( direction == TILDIRECTION.NORTH )
         {
            for( int colIdx = 0; colIdx < m_ColDim; colIdx++ )
            {
               bool hasFreeSpace = false;
               Tuple<int,int> freeSpaceIdx = null;
               for( int rowIdx = 0; rowIdx < m_RowDim; rowIdx++ )
               {
                  if( m_CubeRocks.Contains( new Tuple<int,int>( rowIdx, colIdx ) ) )
                  {
                     if( hasFreeSpace )
                     {
                        hasFreeSpace = false;
                        freeSpaceIdx = null;
                     }
                     continue;
                  }

                  if( m_RoundRocks.Contains( new Tuple<int, int>( rowIdx, colIdx ) ) )
                  {
                     if( hasFreeSpace )
                     {
                        m_RoundRocks.Remove( new Tuple<int, int>( rowIdx, colIdx ) );
                        m_RoundRocks.Add( freeSpaceIdx );
                        hasFreeSpace = true;
                        freeSpaceIdx = Tuple.Create( freeSpaceIdx.Item1 + 1, colIdx );
                     }
                     continue;
                  }

               //If the code reached here, we know this is an empty space. Declare it if we dont have free space
                  if( !hasFreeSpace )
                  {
                     hasFreeSpace = true;
                     freeSpaceIdx = Tuple.Create( rowIdx, colIdx );

                  }
               }
            }

         }
         else if( direction == TILDIRECTION.SOUTH )
         {
            for( int colIdx = 0; colIdx < m_ColDim; colIdx++ )
            {
               bool hasFreeSpace = false;
               Tuple<int,int> freeSpaceIdx = null;
               for( int rowIdx = m_RowDim - 1; rowIdx >= 0; rowIdx-- )
               {
                  if( m_CubeRocks.Contains( new Tuple<int,int>( rowIdx, colIdx ) ) )
                  {
                     if( hasFreeSpace )
                     {
                        hasFreeSpace = false;
                        freeSpaceIdx = null;
                     }
                     continue;
                  }

                  if( m_RoundRocks.Contains( new Tuple<int, int>( rowIdx, colIdx ) ) )
                  {
                     if( hasFreeSpace )
                     {
                        m_RoundRocks.Remove( new Tuple<int, int>( rowIdx, colIdx ) );
                        m_RoundRocks.Add( freeSpaceIdx );
                        hasFreeSpace = true;
                        freeSpaceIdx = Tuple.Create( freeSpaceIdx.Item1 - 1, colIdx );
                     }
                     continue;
                  }

               //If the code reached here, we know this is an empty space. Declare it if we dont have free space
                  if( !hasFreeSpace )
                  {
                     hasFreeSpace = true;
                     freeSpaceIdx = Tuple.Create( rowIdx, colIdx );

                  }
               }
            }
         }
         else if( direction == TILDIRECTION.WEST )
         {
            for( int rowIdx = 0; rowIdx < m_RowDim; rowIdx++ )
            {
               bool hasFreeSpace = false;
               Tuple<int,int> freeSpaceIdx = null;
               for( int colIdx = 0; colIdx < m_ColDim; colIdx++ )
               {
                  if( m_CubeRocks.Contains( new Tuple<int,int>( rowIdx, colIdx ) ) )
                  {
                     if( hasFreeSpace )
                     {
                        hasFreeSpace = false;
                        freeSpaceIdx = null;
                     }
                     continue;
                  }

                  if( m_RoundRocks.Contains( new Tuple<int, int>( rowIdx, colIdx ) ) )
                  {
                     if( hasFreeSpace )
                     {
                        m_RoundRocks.Remove( new Tuple<int, int>( rowIdx, colIdx ) );
                        m_RoundRocks.Add( freeSpaceIdx );
                        hasFreeSpace = true;
                        freeSpaceIdx = Tuple.Create( rowIdx, freeSpaceIdx.Item2 + 1);
                     }
                     continue;
                  }

               //If the code reached here, we know this is an empty space. Declare it if we dont have free space
                  if( !hasFreeSpace )
                  {
                     hasFreeSpace = true;
                     freeSpaceIdx = Tuple.Create( rowIdx, colIdx );
                  }
               }
            }
         }
         else if( direction == TILDIRECTION.EAST )
         {
            for( int rowIdx = 0; rowIdx < m_RowDim; rowIdx++ )
            {
               bool hasFreeSpace = false;
               Tuple<int,int> freeSpaceIdx = null;
               for( int colIdx = m_ColDim - 1; colIdx >= 0; colIdx-- )
               {
                  if( m_CubeRocks.Contains( new Tuple<int,int>( rowIdx, colIdx ) ) )
                  {
                     if( hasFreeSpace )
                     {
                        hasFreeSpace = false;
                        freeSpaceIdx = null;
                     }
                     continue;
                  }

                  if( m_RoundRocks.Contains( new Tuple<int, int>( rowIdx, colIdx ) ) )
                  {
                     if( hasFreeSpace )
                     {
                        m_RoundRocks.Remove( new Tuple<int, int>( rowIdx, colIdx ) );
                        m_RoundRocks.Add( freeSpaceIdx );
                        hasFreeSpace = true;
                        freeSpaceIdx = Tuple.Create( rowIdx, freeSpaceIdx.Item2 - 1);
                     }
                     continue;
                  }

               //If the code reached here, we know this is an empty space. Declare it if we dont have free space
                  if( !hasFreeSpace )
                  {
                     hasFreeSpace = true;
                     freeSpaceIdx = Tuple.Create( rowIdx, colIdx );
                  }
               }
            }
         }


      }


   }
}
