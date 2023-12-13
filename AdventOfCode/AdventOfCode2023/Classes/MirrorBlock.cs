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
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region


      protected static Dictionary<string,bool> m_Cache = new Dictionary<string, bool>( );

      protected string[] m_Block = null;
      protected string[] m_ColStrings = null;
      protected int m_RowDim;
      protected int m_ColDim;
   #endregion

   /*CONSTRUCTORS*/
   #region

      static MirrorBlock( )
      {

      }


      public MirrorBlock( string[] inp )
      {
         m_RowDim = inp.Length;
         m_ColDim = inp[0].Length;
         m_Block = new string[inp.Length];
         for( int i = 0; i<inp.Length; i++ )
            m_Block[i] = inp[i];

         m_ColStrings = new string[m_Block[0].Length];
         for( int i = 0; i<m_ColStrings.Length; i++ )
         {
            StringBuilder sb = new StringBuilder( );
            for( int rowIdx = 0; rowIdx < inp.Length; rowIdx++ )
            {
               sb.Append( inp[rowIdx][i].ToString( ) );
            }
            m_ColStrings[i] = sb.ToString( );
         }

      }
   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   /*METHODS*/
   #region

      public long GetAnsP1( )
      {

      //Loop over all possible mirror locations for this string.
         List<int> vertCand = new List<int>( );
         for( int mirrorIdx = 1; mirrorIdx<m_Block[0].Length; mirrorIdx++ )
         {
            int valCheck = Math.Min( mirrorIdx, m_ColDim - mirrorIdx );

         //Extract substring
            string s = m_Block[0].Substring( mirrorIdx - valCheck, 2*valCheck );
            if( IsMirroredByHalfMain( s ) )
               vertCand.Add( mirrorIdx );
         }

         List<long> vertMirrors = new List<long>( );
         while( vertCand.Count > 0 )
         {
            int thisCand=vertCand[0];
            bool isMirror = true;
            for( int i = 1; i<m_Block.Length; i++ )
            {
               int valCheck = Math.Min( thisCand, m_ColDim - thisCand );
               string s = m_Block[i].Substring( thisCand - valCheck, 2*valCheck );
               if( !IsMirroredByHalfMain( s ) )
               {
                  isMirror = false;
                  break;
               }
            }
            if( isMirror )
               vertMirrors.Add( thisCand );

         //Remove this candidate and continue
            vertCand.RemoveAt( 0 );
         }

         List<int> horCand = new List<int>( );
         for( int mirrorIdx = 1; mirrorIdx<m_ColStrings[0].Length; mirrorIdx++ )
         {
            int valCheck = Math.Min( mirrorIdx, m_RowDim - mirrorIdx );

         //Extract substring
            string s = m_ColStrings[0].Substring( mirrorIdx - valCheck, 2*valCheck );
            if( IsMirroredByHalfMain( s ) )
               horCand.Add( mirrorIdx );
         }

         List<long> horMirrors = new List<long>( );
         while( horCand.Count > 0 )
         {

            int thisCand=horCand[0];
            bool isMirror = true;
            for( int i = 1; i<m_ColStrings.Length; i++ )
            {
               int valCheck = Math.Min( thisCand, m_RowDim - thisCand );
               string s = m_ColStrings[i].Substring( thisCand - valCheck, 2*valCheck );
               if( !IsMirroredByHalfMain( s ) )
               {
                  isMirror = false;
                  break;
               }
            }
            if( isMirror )
               horMirrors.Add( thisCand );

            horCand.RemoveAt( 0 );
         }


         long ans1 = 0;
         foreach( int i in vertMirrors )
            ans1 += i;
         foreach( int i in horMirrors )
            ans1 += i*100;

         return ans1;

      }


      protected bool IsMirroredByHalfMain( string s )
      {

      //Try to get from cache..
         if( m_Cache.TryGetValue( s, out bool isMirrored ) )
            return isMirrored;


      //If the code reached here, we havent computed this one yet. Check for mirrors
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
