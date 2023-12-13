using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   internal class MirrorBlock
   {
   /*ENUMS*/
   #region
      public enum MIRRORTYPE
      {
         HORIZONTAL,
         VERTICAL
      }
   #endregion

   /*MEMBERS*/
   #region
      protected static Dictionary<string,bool> m_Cache = new Dictionary<string, bool>( );
      protected string[] m_ColStrings = null;
      protected int m_RowDim;
      protected int m_ColDim;
      protected string[] m_Block = null;
      protected long m_OriginalMirrorIndex = -1;
      protected MIRRORTYPE m_OriginalMirrorType = MIRRORTYPE.HORIZONTAL;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public MirrorBlock( string[] inp )
      {
         m_RowDim = inp.Length;
         m_ColDim = inp[0].Length;
         m_Block = new string[inp.Length];
         for( int i = 0; i<inp.Length; i++ )
            m_Block[i] = inp[i];

         m_ColStrings = new string[inp[0].Length];
         for( int i = 0; i<m_ColStrings.Length; i++ )
         {
            StringBuilder sb = new StringBuilder( );
            for( int rowIdx = 0; rowIdx < inp.Length; rowIdx++ )
               sb.Append( inp[rowIdx][i].ToString( ) );
            m_ColStrings[i] = sb.ToString( );
         }
         MIRRORTYPE? t;
         m_OriginalMirrorIndex = CalculateMirror( m_Block, out t  );
         m_OriginalMirrorType = ( MIRRORTYPE ) t;
      }
   #endregion

   /*METHODS*/
   #region
      protected long CalculateMirror( string[] block, out MIRRORTYPE? mirrorType, long ignoreLine = -1, MIRRORTYPE? ignoreType = null )
      {
      //Create column copies so i i can just copy code..
         string[] colBlock = new string[block[0].Length];
         for( int i = 0; i<colBlock.Length; i++ )
         {
            StringBuilder sb = new StringBuilder( );
            for( int rowIdx = 0; rowIdx < block.Length; rowIdx++ )
               sb.Append( block[rowIdx][i].ToString( ) );
            colBlock[i] = sb.ToString( );
         }

      //Find vertical mirrors if any
         List<int> vertCand = new List<int>( );
         for( int mirrorIdx = 1; mirrorIdx< block[0].Length; mirrorIdx++ )
         {
            if( IsMirroredByHalfMain( block[0].Substring( mirrorIdx - Math.Min( mirrorIdx, m_ColDim - mirrorIdx ), 2 * Math.Min( mirrorIdx, m_ColDim - mirrorIdx ) ) ) )
            {
               if( mirrorIdx == ignoreLine && ignoreType == MIRRORTYPE.VERTICAL )
                  continue;
               else
                  vertCand.Add( mirrorIdx );
            }
         }
         List<long> vertMirrors = new List<long>( );
         while( vertCand.Count > 0 )
         {
            bool isMirror = true;
            for( int i = 1; i< block.Length; i++ )
            {
               if( !IsMirroredByHalfMain( block[i].Substring( vertCand[0] - Math.Min( vertCand[0], m_ColDim - vertCand[0] ), 2 * Math.Min( vertCand[0], m_ColDim - vertCand[0] ) ) ) )
               {
                  isMirror = false;
                  break;
               }
            }
            if( isMirror )
               vertMirrors.Add( vertCand[0] );
            vertCand.RemoveAt( 0 );
         }

      //Find horizontal mirror if any
         List<int> horCand = new List<int>( );
         for( int mirrorIdx = 1; mirrorIdx<colBlock[0].Length; mirrorIdx++ )
         {
            if( IsMirroredByHalfMain( colBlock[0].Substring( mirrorIdx - Math.Min( mirrorIdx, m_RowDim - mirrorIdx ), 2 * Math.Min( mirrorIdx, m_RowDim - mirrorIdx ) ) ) )
            {
               if( mirrorIdx == ignoreLine && ignoreType == MIRRORTYPE.HORIZONTAL )
                  continue;
               else
                  horCand.Add( mirrorIdx );
            }
         }
         List<long> horMirrors = new List<long>( );
         while( horCand.Count > 0 )
         {
            bool isMirror = true;
            for( int i = 1; i<colBlock.Length; i++ )
            {
               if( !IsMirroredByHalfMain( colBlock[i].Substring( horCand[0] - Math.Min( horCand[0], m_RowDim - horCand[0] ), 2*Math.Min( horCand[0], m_RowDim - horCand[0] ) ) ) )
               {
                  isMirror = false;
                  break;
               }
            }
            if( isMirror )
               horMirrors.Add( horCand[0] );
            horCand.RemoveAt( 0 );
         }

      //Return mirror and new value
         long retMirrorIdx = -1;
         mirrorType = null;
         if( vertMirrors.Count > 0 )
         {
            retMirrorIdx = vertMirrors[0];
            mirrorType = MIRRORTYPE.VERTICAL;
         }
         if( horMirrors.Count > 0 )
         {
            retMirrorIdx = horMirrors[0];
            mirrorType = MIRRORTYPE.HORIZONTAL;
         }
         return retMirrorIdx;
      }

      public long FindSubstitueMirror( )
      {
         for( int i = 0; i<m_Block.Length; i++ )
         {
            for( int j = 0; j<m_Block[0].Length; j++ )
            {
               string[ ] blockCopy = ( string[ ] ) m_Block.Clone( );
               char opposite = '.';
               if( blockCopy[i][j] == '.' )
                  opposite = '#';

               blockCopy[i] = blockCopy[i].Substring( 0, j ) + opposite.ToString( ) + blockCopy[i].Substring( j+1, blockCopy[i].Length - j - 1 );
               long idx = CalculateMirror( blockCopy, out MIRRORTYPE? thisMirrorType, m_OriginalMirrorIndex, m_OriginalMirrorType );

               if( idx == m_OriginalMirrorIndex && thisMirrorType == m_OriginalMirrorType )
                  continue;
               else if( idx != -1 )
               {
                  long ans;
                  if( thisMirrorType == MIRRORTYPE.VERTICAL )
                     ans = idx;
                  else// if( newMirrorType == MIRRORTYPE.HORIZONTAL )
                     ans = idx*100;
                  return ans;
               }
            }
         }
      //If the code reached this point, crash.
         throw new Exception( );
      }

      protected bool IsMirroredByHalfMain( string s )
      {
         if( m_Cache.TryGetValue( s, out bool isMirrored ) )
            return isMirrored;

         isMirrored = IsMirroredByHalf( s );

      //Memo it
         m_Cache.Add( s, isMirrored );
         return isMirrored;
      }

      protected bool IsMirroredByHalf( string s )
      {
         return ( s.Substring( 0, s.Length / 2 ).Equals( new string( s.Substring( s.Length / 2, s.Length/2 ).Reverse( ).ToArray( ) ) ) );
      }


   #endregion
   }
}
