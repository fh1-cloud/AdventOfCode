using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class TiltBoard
   {

   /*LOCAL CLASSES*/
   #region
   #endregion

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

      protected long m_LastCycleWeight = -1;

      protected int m_ColDim = -1;
      protected int m_RowDim = -1;
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

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region


      public void PrintState( )
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
         Console.Write( sb.ToString() );
      }

      public void Cycle( )
      {
         Tilt( TILDIRECTION.NORTH );
         Tilt( TILDIRECTION.WEST );
         Tilt( TILDIRECTION.SOUTH );
         Tilt( TILDIRECTION.EAST );

         //PrintState( );

         long cycleRes = GetWeightOnNorthBeam( );
         long change = m_LastCycleWeight - cycleRes;
         m_LastCycleWeight = cycleRes;
         Console.WriteLine( change );
      }

      public long GetWeightOnNorthBeam( )
      {
         long ans1 = 0;
         foreach( var t in m_RoundRocks )
         {
            long thisAns = (m_RowDim - t.Item1);
            ans1 += thisAns;
         }
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

   #endregion

   /*STATIC METHODS*/
   #region
   #endregion


   }
}
