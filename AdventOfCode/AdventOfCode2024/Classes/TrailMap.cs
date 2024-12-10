using AdventOfCodeLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdventOfCode2024.Classes
{
   public class TrailMap
   {
   /*LOCAL CLASSES*/
   #region

      public class TrailMapNode
      {
         public TrailMapNode( int val, int rowPos, int colPos )
         {
            this.Val = val;
            this.Pos = ( rowPos, colPos );
            this.Visited = false;
         }
         public int Val { get; set; }
         public ( int Row, int Col ) Pos { get; set; }
         public bool Visited { get; set; }

         /// <summary>
         /// Gets the number of distinct paths for part 2..
         /// </summary>
         /// <param name="map"></param>
         /// <param name="distinctPaths"></param>
         public void GetDistinctPaths( TrailMap map, ref int distinctPaths )
         {
            if( this.Val == 9 )
               distinctPaths++;
            else
            {
               HashSet<TrailMapNode> neighbours = map.GetAccessibleNeighboursP2( this );
               foreach( TrailMapNode n in neighbours )
                  n.GetDistinctPaths( map, ref distinctPaths );
            }
         }

      }
   #endregion

   /*MEMBERS*/
   #region

      public TrailMapNode[ , ] m_MapNodes = null;
      public int m_RowCount = -1;
      public int m_ColCount = -1;
      public HashSet<(int,int)> m_ZeroNodes = new HashSet<(int, int)>( );

   #endregion

   /*CONSTRUCTORS*/
   #region


      public TrailMap( string[ ] inp )
      {
         m_MapNodes = new TrailMapNode[inp.Length, inp[0].Length];
         m_RowCount = inp.Length;
         m_ColCount = inp[0].Length;
         for( int i = 0; i < inp.Length; i++ )
         {
            for( int j = 0; j < inp[i].Length; j++ )
            {
               int value = 99;
               if( inp[i][j] != '.' )
                  value = int.Parse( inp[i][j].ToString( ) );
               m_MapNodes[i, j] = new TrailMapNode( value, i, j );
               if( value == 0 )
                  m_ZeroNodes.Add( ( i, j ) );
            }
         }
      }

   #endregion

   /*METHODS*/
   #region


      public HashSet<TrailMapNode> GetAccessibleNeighboursP2( TrailMapNode node )
      {
      //Collect list of neighbours..
         List<(int,int)> neighbours = new List<(int, int)>( );
         if( node.Pos.Row < m_MapNodes.GetLength( 0 ) - 1 )
            neighbours.Add( (node.Pos.Row + 1, node.Pos.Col) );
         if( node.Pos.Row > 0 )
            neighbours.Add( (node.Pos.Row - 1, node.Pos.Col ) );
         if( node.Pos.Col < m_MapNodes.GetLength( 1 ) - 1 )
            neighbours.Add( (node.Pos.Row, node.Pos.Col + 1 ) );
         if( node.Pos.Col > 0 )
            neighbours.Add( (node.Pos.Row, node.Pos.Col - 1 ) );

      //Return hash set of unvisited neighbours..
         HashSet<TrailMapNode> ret = new HashSet<TrailMapNode>( );
         foreach( (int, int) l in neighbours )
         {
            TrailMapNode neighbour = m_MapNodes[ l.Item1, l.Item2 ];
            bool isAccessible = ( neighbour.Val == node.Val + 1 );
            if( isAccessible )
               ret.Add( m_MapNodes[l.Item1, l.Item2 ] );
         }
         return ret;
      }

      public HashSet<(int,int)> GetAccessibleUnvisitedNeighboursP1( TrailMapNode node )
      {
      //Collect list of neighbours..
         List<(int,int)> neighbours = new List<(int, int)>( );
         if( node.Pos.Row < m_MapNodes.GetLength( 0 ) - 1 )
            neighbours.Add( (node.Pos.Row + 1, node.Pos.Col) );
         if( node.Pos.Row > 0 )
            neighbours.Add( (node.Pos.Row - 1, node.Pos.Col ) );
         if( node.Pos.Col < m_MapNodes.GetLength( 1 ) - 1 )
            neighbours.Add( (node.Pos.Row, node.Pos.Col + 1 ) );
         if( node.Pos.Col > 0 )
            neighbours.Add( (node.Pos.Row, node.Pos.Col - 1 ) );

      //Return hash set of unvisited neighbours..
         HashSet<(int,int)> ret = new HashSet<(int, int)>( );
         foreach( (int, int) l in neighbours )
         {
            TrailMapNode neighbour = m_MapNodes[ l.Item1, l.Item2 ];
            bool isAccessible = ( neighbour.Val == node.Val + 1 );
            if( neighbour.Visited == false && isAccessible )
               ret.Add( l );
         }
         return ret;
      }

      
      public int P2( )
      {
      //Create a copy of the trail heads for looping..
         HashSet<(int,int)> unvisitedTrailHeads = new HashSet<(int, int)>( m_ZeroNodes );

      //Declare the dictionary of trailhead with scores..
         Dictionary<TrailMapNode,int> trailHeadRating = new Dictionary<TrailMapNode, int>( );
         while( unvisitedTrailHeads.Count > 0 )
         {
         //Since we cant go downwards, we only get the next train head in the list..
            ( int row, int col ) thisTrailHeadIdx = unvisitedTrailHeads.First( );
            TrailMapNode trailHead = m_MapNodes[thisTrailHeadIdx.row, thisTrailHeadIdx.col ];
            unvisitedTrailHeads.Remove( thisTrailHeadIdx );

         //Branch from this node..
            int distinctPaths = 0;
            trailHead.GetDistinctPaths( this, ref distinctPaths );
            trailHeadRating.Add( trailHead, distinctPaths );
         }

      //Sum up the rating of each trail head
         int ans = 0;
         foreach( KeyValuePair<TrailMapNode, int> kvp in trailHeadRating )
            ans += kvp.Value;
         return ans;
      }

      public void ResetVisitedStatus( )
      {
         for( int i = 0; i < m_RowCount; i++ )
            for( int j = 0; j < m_ColCount; j++ )
               if( m_MapNodes[i,j].Val == 0 )
                  continue;
               else
                  m_MapNodes[i,j].Visited = false;
      }
      public int P1( )
      {

      //Create a copy of the trail heads for looping..
         HashSet<(int,int)> unvisitedTrailHeads = new HashSet<(int, int)>( m_ZeroNodes );
         
      //Declare the dictionary of trailhead with scores..
         Dictionary<TrailMapNode,int> trailHeadWithScores = new Dictionary<TrailMapNode, int>( );

         while( unvisitedTrailHeads.Count > 0 )
         {
         //Reset visit status..
            ResetVisitedStatus( );
            ( int row, int col ) thisTrailHeadIdx = unvisitedTrailHeads.First( );
            if( m_MapNodes[thisTrailHeadIdx.row, thisTrailHeadIdx.col].Visited == true )
            {
               unvisitedTrailHeads.Remove( thisTrailHeadIdx );
               continue;
            }

         //If the code reached this point, we know that we are at a trailhead that hasnt been visited yet..
            HashSet<(int,int)> unvisitedNodes = new HashSet<(int, int)>( );
            HashSet<(int,int)> peaksEncountered = new HashSet<(int, int)>( );
            HashSet<(int,int)> trailHeadsEncountered = new HashSet<(int, int)>( );
            unvisitedNodes.Add( m_MapNodes[thisTrailHeadIdx.row, thisTrailHeadIdx.col].Pos );

            while( unvisitedNodes.Count > 0 )
            {

            //First, check if this is a peak, if so add it to the encountered peaks..
               ( int row, int col ) thisNodeLocation = unvisitedNodes.First( );
               TrailMapNode thisNode = m_MapNodes[thisNodeLocation.Item1, thisNodeLocation.Item2 ];

            //Check if this is a peak
               if( thisNode.Val == 9 )
                  peaksEncountered.Add( thisNode.Pos );
               else if( thisNode.Val == 0 )
                  trailHeadsEncountered.Add( thisNode.Pos );

               thisNode.Visited = true;
               foreach( ( int, int ) n in GetAccessibleUnvisitedNeighboursP1( thisNode ) )
                  unvisitedNodes.Add( n );
               unvisitedNodes.Remove( ( thisNodeLocation ) );
            }

         //WHen the codea reached this point, add the score to each trailhead if there were any..
            foreach( ( int, int ) the in trailHeadsEncountered )
               trailHeadWithScores.Add( m_MapNodes[ the.Item1, the.Item2 ], peaksEncountered.Count );
         }

      //Sum up the socres of each trail head
         int ans = 0;
         foreach( KeyValuePair<TrailMapNode, int> kvp in trailHeadWithScores )
            ans += kvp.Value;
         return ans;
      }

   #endregion
 
   }
}
