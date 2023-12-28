using AdventOfCodeLib.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class GardenMap
   {

   /*LOCAL CLASSES*/
   #region

      public class GardenPath
      {
         public GardenPath( int startRow, int startCol, char[,] map, HashSet<GardenMapNode> allNodes )
         {
            this.Ends = new HashSet<Tuple<int, int>>( );
            this.Ends.Add( Tuple.Create( startRow, startCol ) );
            this.Length = 1;
            this.VisitedLocations = new HashSet<(int, int)>( );
            this.Nodes = new HashSet<GardenMapNode>( );
            this.Explore( map, allNodes );
         }

         public int Length { get; private set; }
         public HashSet<Tuple<int,int>> Ends { get; private set; }
         public HashSet<GardenMapNode> Nodes { get; private set; }
         public HashSet<(int,int)> VisitedLocations { get; private set; }
         private void Explore( char[,] map, HashSet<GardenMapNode> allNodes )
         {
         //Declare initial variables
            bool foundEnd = false;
            int cRow = this.Ends.FirstOrDefault( ).Item1;
            int cCol = this.Ends.FirstOrDefault( ).Item2;
            VisitedLocations.Add( ( cRow, cCol ) );

         //Find the garden node that has this point as a connection point
            this.Nodes.Add( allNodes.Where( x => x.ConnectionPoints.Contains( (cRow, cCol) ) ).ToList( ).First( ) );

         //Find the start location and move one.
            if( map[cRow,cCol] == '>' )
               cCol++;
            else if( map[cRow,cCol] == 'v' )
               cRow++;

            Tuple<int,int> lastPos = Tuple.Create( cRow, cCol );
            this.VisitedLocations.Add( ( cRow, cCol ) );
            while( !foundEnd )
            {
               List<Tuple<int,int>> cand = GetNeighbours( lastPos.Item1, lastPos.Item2 );
               bool foundMove = false;
               foreach( Tuple<int, int> candidate in cand )
               {
                  if( map[candidate.Item1,candidate.Item2] != '.' || VisitedLocations.Contains( ( candidate.Item1, candidate.Item2 ) ) )
                     continue;

                  lastPos = candidate; //If the code reached here, we should move to next period
                  foundMove = true;
                  break;
               }
               if( foundMove ) //Move
               {
                  VisitedLocations.Add( ( lastPos.Item1, lastPos.Item2 ) );
                  this.Length++;
                  continue;
               }

            //If the code reached here, we found no new locations to move to. Check for exits.
               foreach( Tuple<int, int> v in cand )
               {
                  if( map[v.Item1,v.Item2] == 'v' || map[v.Item1,v.Item2] == '>' )
                  {
                     this.Length++;
                     this.Ends.Add( Tuple.Create( v.Item1, v.Item2 ) );
                     foundEnd = true;
                     this.Nodes.Add( allNodes.Where( x => x.ConnectionPoints.Contains( (v.Item1, v.Item2) ) ).ToList( ).First( ) );
                     break;
                  }
               }
            }
         }
      }

      public class GardenMapNode
      {
         public GardenMapNode( bool isGlobalEnd = false )
         {
            this.LeadsTo = new Dictionary<GardenMapNode, GardenPath>( );
            this.IsGlobalEnd = isGlobalEnd;
            this.ConnectionPoints = new HashSet<(int, int)>( );
         }
         public bool IsGlobalEnd { get; private set; }
         public Dictionary<GardenMapNode,GardenPath> LeadsTo { get; set; }
         public HashSet<(int, int)> ConnectionPoints { get; set; }
      }


   #endregion 
   /*MEMBERS*/
   #region
      protected char[,] m_Map = null;
      protected int m_StartRowIdx = -1;
      protected int m_EndRowIdx = -1;
      protected int m_StartColIdx = -1;
      protected int m_EndColIdx = -1;

   #endregion

   /*CONSTRUCTORS*/
   #region
      public GardenMap( string[] inp )
      {
         m_Map = new char[inp.Length,inp[0].Length];
         for( int i = 0; i<inp.Length; i++ )
            for( int j = 0; j<inp[i].Length; j++ )
               m_Map[i,j] = inp[i][j];
         m_StartRowIdx = 0;
         m_StartColIdx = 1;
         m_EndRowIdx = m_Map.GetLength( 0 ) - 1;
         m_EndColIdx = m_Map.GetLength( 1 ) - 2;
      }
   #endregion

   /*STATIC METHODS*/
   #region

      //Finds the path from the current node to the endnode
      protected static void FindPath( HashSet<GardenMapNode> visited, GardenMapNode currentNode, long currentLength, List<long> paths )
      {
         foreach( KeyValuePair<GardenMapNode, GardenPath> c in currentNode.LeadsTo )
         {
            long newLength = currentLength + c.Value.Length + 2; //2 is the length of each node
            if( c.Key.IsGlobalEnd )
               paths.Add( newLength - 2 ); //Subtract two for start and endnode..

            if( !visited.Contains( c.Key ) )
            {
               HashSet<GardenMapNode> newVisited = new HashSet<GardenMapNode>( visited );
               newVisited.Add( c.Key );
               FindPath( newVisited, c.Key, newLength, paths );
            }
         }
      }

      //Find index for neighbour
      private static List<Tuple<int,int>> GetNeighbours( int r, int c )
      {
         return new List<Tuple<int, int>> { Tuple.Create( r, c - 1 ), Tuple.Create( r, c + 1 ), Tuple.Create( r + 1, c ), Tuple.Create( r - 1, c ) };
      }

   #endregion

   /*METHODS*/
   #region

      public long FindLongestPath( )
      {
      //Start by replacing the start and end with other characters.
         m_Map[m_StartRowIdx, m_StartColIdx] = 'v';
         m_Map[m_EndRowIdx, m_EndColIdx] = 'v';

      //Start by creating a list of all main nodes..
         HashSet<GardenMapNode> allNodes = new HashSet<GardenMapNode>( );
         GardenMapNode start = new GardenMapNode( );
         start.ConnectionPoints.Add( ( m_StartRowIdx, m_StartColIdx ) );
         allNodes.Add( start );

         for( int i = 1; i<m_Map.GetLength( 0 ) - 1; i++ )
         {
            for( int j = 1; j<m_Map.GetLength( 1 ) - 1; j++ )
            {
               if( m_Map[i,j] != '.' )
                  continue;

            //Get neighbours
               List<Tuple<int,int>> n = GetNeighbours( i, j );
               int count = 0;
               foreach( Tuple<int, int> p in n )
                  if( m_Map[p.Item1,p.Item2] == 'v' || m_Map[p.Item1,p.Item2] == '>' )
                     count++;
               
            //Create connection points for garden nodes
               if( count >= 2 )
               {
                  GardenMapNode node = new GardenMapNode( );
                  foreach( Tuple<int, int> p in n )
                     if( m_Map[p.Item1,p.Item2] == 'v' || m_Map[p.Item1,p.Item2] == '>' )
                        node.ConnectionPoints.Add( ( p.Item1, p.Item2 ) );

               //Add the node to the set of all nodes..
                  allNodes.Add( node );
               }
            }
         }
         GardenMapNode end = new GardenMapNode( true );
         end.ConnectionPoints.Add( ( m_EndRowIdx, m_EndColIdx ) );
         allNodes.Add( end );

      //If the code reached here, we have created all the nodes. Now we need to loop through all the nodes and explore the startpoints
         HashSet<(int,int)> exploredEnds = new HashSet<(int, int)>( );
         HashSet<GardenPath> allPaths = new HashSet<GardenPath>( );
         foreach( GardenMapNode node in allNodes )
         {
            foreach( ( int R, int C) pt in node.ConnectionPoints )
            {
            //Check if a path with this endpoint already exist
               if( exploredEnds.Contains( pt ) )
                  continue;

            //If the code reached this point, this path is not created yet. Create it
               GardenPath p = new GardenPath( pt.R, pt.C, m_Map, allNodes );
               allPaths.Add( p );

            //Add start and endpoint to the hashed set so we dont explore it twice
               exploredEnds.Add( ( p.Ends.First( ).Item1, p.Ends.First( ).Item2 ) );
               exploredEnds.Add( ( p.Ends.Last( ).Item1, p.Ends.Last( ).Item2 ) );

            //Add node gonnection
               if( !p.Nodes.First( ).LeadsTo.ContainsKey( p.Nodes.Last( ) ) )
                  p.Nodes.First( ).LeadsTo.Add( p.Nodes.Last( ), p );
               if( !p.Nodes.Last( ).LeadsTo.ContainsKey( p.Nodes.First( ) ) ) 
                  p.Nodes.Last( ).LeadsTo.Add( p.Nodes.First( ), p );

            }
         } //End path exploring

      //Now we have all the nodes with all the weights, find the longest path in the tree
         List<long> allPathLengths = new List<long>( );
         HashSet<GardenMapNode> visited = new HashSet<GardenMapNode>{ start };
         FindPath( visited, start, 0, allPathLengths );
         return allPathLengths.Max( );
      }

   #endregion

   }
}