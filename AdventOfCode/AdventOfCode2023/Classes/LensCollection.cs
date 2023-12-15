using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class LensCollection
   {
   /*MEMBERS*/
   #region
      protected Dictionary<long, List<Tuple<string,long>>> m_LenseBoxes = new Dictionary<long, List<Tuple<string,long>>>();
   #endregion

   /*CONSTRUCTORS*/
   #region
      public LensCollection( string[] lensPlacements )
      {
         for( int i = 0; i<lensPlacements.Length; i++ )
         {
            if( lensPlacements[i].Contains( '=' ) )
            {
               string[ ] s = lensPlacements[i].Split( '=' );
               PlaceLens( GetHash( s[0] ), s[0], long.Parse( s[1] ) );
            }
            else if( lensPlacements[i].Contains( '-' ) )
            {
               string[ ] s = lensPlacements[i].Split( '-' );
               RemoveLens( GetHash( s[0] ), s[0] );
            }
         }
      }
   #endregion
      public long GetFocusingPower( )
      {
         long totalFocusingPower = 0;
         foreach( KeyValuePair<long, List<Tuple<string, long>>> kvp in m_LenseBoxes )
            foreach( Tuple<string,long> tuple in kvp.Value )
               totalFocusingPower += ( kvp.Key + 1 ) * ( kvp.Value.IndexOf( tuple ) + 1 ) * tuple.Item2;
         return totalFocusingPower;
      }

      protected void PlaceLens( long boxNum, string label, long focalLength )
      {
         if( !m_LenseBoxes.ContainsKey( boxNum ) )
            m_LenseBoxes.Add( boxNum, new List<Tuple<string,long>>( ) );

         int foundItIdx = FindLensInBox( m_LenseBoxes[boxNum], label );
         if( foundItIdx == -1 ) 
            m_LenseBoxes[boxNum].Add( Tuple.Create( label, focalLength ) );
         else //Did find it, remove old lens
         {
            m_LenseBoxes[boxNum].RemoveAt( foundItIdx );
            m_LenseBoxes[boxNum].Insert( foundItIdx, Tuple.Create( label, focalLength ) );
         }
      }

      protected void RemoveLens( long boxNum, string label )
      {
         if( !m_LenseBoxes.ContainsKey( boxNum ) )
            m_LenseBoxes.Add( boxNum, new List<Tuple<string,long>>( ) );

         int foundItIdx = FindLensInBox( m_LenseBoxes[boxNum], label );
         if( foundItIdx != -1 ) 
         {
            m_LenseBoxes[boxNum].RemoveAt( foundItIdx );
            if( m_LenseBoxes[boxNum].Count == 0 )
               m_LenseBoxes.Remove( boxNum );
         }

      }

      protected int FindLensInBox( List<Tuple<string,long>> thisList, string thisLabel )
      {
         int foundItIdx = -1;
         for( int i = 0; i<thisList.Count; i++ )
            if( thisList[i].Item1 == thisLabel )
            {
               foundItIdx = i;
               break;
            }

         return foundItIdx;
      }


   /*STATIC METHODS*/
   #region
      public static long GetHash( string inp )
      {
         long ans = 0;
         for( int i = 0; i<inp.Length; i++ )
         {
            ans += ( long ) inp[i];
            ans = ( ans*17 ) % 256;
         }
         return ans;
      }

   #endregion

   }
}
