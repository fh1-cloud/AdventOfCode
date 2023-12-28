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

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region

      public class Position
      {
         public Position( int r, int c )
         { 
            this.Row = r; 
            this.Col = c; 
         }
         public int Row { get; set; }
         public int Col { get; set; }

         public static List<Tuple<int,int>> GetNeighbours( int r, int c )
         {
            List<Tuple<int,int>> retList = new List<Tuple<int, int>>( );
            retList.Add( Tuple.Create( r, c - 1 ) );
            retList.Add( Tuple.Create( r, c + 1 ) );
            retList.Add( Tuple.Create( r + 1, c ) );
            retList.Add( Tuple.Create( r - 1, c ) );
            return retList;
         }
      }


      public class GardenPath
      {
         public GardenPath( int startRow, int startCol )
         {
            this.StartLocation = Tuple.Create( startRow, startCol );
            this.Length = 0;
            this.VisitedLocations = new HashSet<(int, int)>( );
            this.IsExplored = false;
         }

         public int Length { get; set; }
         public bool IsExplored { get; set; }
         public Tuple<int,int> StartLocation { get; set; }
         public Tuple<int,int> EndLocation { get; set; }
         public HashSet<(int,int)> VisitedLocations { get; set; }
         public string GetKey( ) { return $"{this.StartLocation.Item1},{this.StartLocation.Item2},{this.EndLocation.Item1},{this.EndLocation.Item2}"; }
         public void Explore( char[,] map )
         {
         //Return if this is already explored
            if( this.IsExplored )
               return;

         //Declare initial variables
            bool foundEnd = false;
            int cRow = StartLocation.Item1;
            int cCol = StartLocation.Item2;
            VisitedLocations.Add( ( cRow, cCol ) );

         //Find the start location and move one.
            if( map[cRow,cCol] == '>' )
               cCol++;
            else if( map[cRow,cCol] == 'v' )
               cRow++;
            Length++;

            Tuple<int,int> lastPos = Tuple.Create( cRow, cCol );
            this.VisitedLocations.Add( ( cRow, cCol ) );
            while( !foundEnd )
            {
               List<Tuple<int,int>> cand = Position.GetNeighbours( lastPos.Item1, lastPos.Item2 );
               bool foundMove = false;
               foreach( Tuple<int, int> candidate in cand )
               {
                  if( map[candidate.Item1,candidate.Item2] != '.' || VisitedLocations.Contains( ( candidate.Item1, candidate.Item2 ) ) )
                     continue;

               //If the code reached here, we should move.
                  lastPos = candidate;
                  foundMove = true;
                  break;
               }

               if( foundMove )
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
                     this.EndLocation = v;
                     foundEnd = true;
                     break;
                  }
               }
            }

         //If the code reached here, it is done exploring
            this.IsExplored = true;

         }

      }


      public class GardenMapNode
      {
         public GardenMapNode( int rowLoc, int colLoc, int id )
         {
            this.RowIdx = rowLoc;
            this.ColIdx = colLoc;
            this.LeadsTo = new Dictionary<GardenMapNode, GardenPath>( );
            this.StartPoints = new HashSet<(int, int)>( );
            this.EndPoints = new HashSet<(int, int)>( );
            this.IsGlobalStart = false;
            this.IsGlobalEnd = false;
            this.Id = id;
         }
         public int Id { get; set; }
         public int RowIdx { get; set; }
         public int ColIdx { get; set; }
         public bool IsGlobalStart { get; set; }
         public bool IsGlobalEnd { get; set; }
         public Dictionary<GardenMapNode,GardenPath> LeadsTo { get; set; }
         public HashSet<(int, int)> StartPoints { get; set; }
         public HashSet<(int, int)> EndPoints { get; set; }

      }


   #endregion 
   /*MEMBERS*/
   #region

      protected char[,] m_Map = null;
      protected HashSet< ( int row, int col )> m_Visited = new HashSet<(int row, int col)>( );

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

      public long FindLongestPath( )
      {

      //Start by replacing the start and end with other characters.
         m_Map[m_StartRowIdx, m_StartColIdx] = 'v';
         m_Map[m_EndRowIdx, m_EndColIdx] = 'v';

      //Start by creating a list of all main nodes..
         int runningId = 0;
         HashSet<GardenMapNode> allNodes = new HashSet<GardenMapNode>( );
         GardenMapNode start = new GardenMapNode( m_StartRowIdx, m_StartColIdx, runningId++ );
         start.StartPoints.Add( ( m_StartRowIdx, m_StartColIdx ) );
         start.IsGlobalStart = true;
         allNodes.Add( start );

         for( int i = 1; i<m_Map.GetLength( 0 ) - 1; i++ )
         {
            for( int j = 1; j<m_Map.GetLength( 1 ) - 1; j++ )
            {
               if( m_Map[i,j] != '.' )
                  continue;

            //Create position

               List<Tuple<int,int>> n = Position.GetNeighbours( i, j );
               int count = 0;
               foreach( Tuple<int, int> p in n )
                  if( m_Map[p.Item1,p.Item2] == 'v' || m_Map[p.Item1,p.Item2] == '>' )
                     count++;
               
            //Create start and endpoint for garden nodes..
               if( count >= 2 )
               {
                  GardenMapNode node = new GardenMapNode( i, j, runningId++ );
                  foreach( Tuple<int, int> p in n )
                  {
                     if( p.Item1 == i-1 && m_Map[p.Item1,p.Item2] == 'v' )
                        node.EndPoints.Add( ( p.Item1, p.Item2 ) );
                     else if( p.Item2 == j-1 && m_Map[p.Item1,p.Item2] == '>' )
                        node.EndPoints.Add( ( p.Item1, p.Item2 ) );
                     else if( p.Item1 == i+1 && m_Map[p.Item1,p.Item2] == 'v' )
                        node.StartPoints.Add( ( p.Item1, p.Item2 ) );
                     else if( p.Item2 == j+1 && m_Map[p.Item1,p.Item2] == '>' )
                        node.StartPoints.Add( ( p.Item1, p.Item2 ) );
                  }

               //Add the node to the set of all nodes..
                  allNodes.Add( node );
               }
            }
         }
         GardenMapNode end = new GardenMapNode( m_EndRowIdx, m_EndColIdx, runningId++ );
         end.EndPoints.Add( ( m_EndRowIdx, m_EndColIdx ) );
         end.IsGlobalEnd = true;
         allNodes.Add( end );

      //If the code reached here, we have created all the nodes. Now we need to loop through all the nodes and explore the startpoints
         foreach( GardenMapNode node in allNodes )
         {

            foreach( ( int r, int c) starts in node.StartPoints )
            {
            //Create path..
               GardenPath thisPath = new GardenPath( starts.r, starts.c );
               thisPath.Explore( m_Map );

            //Find the node it connects to
               bool foundEnd = false;
               foreach( GardenMapNode connection in allNodes )
               {
                  if( connection == node )
                     continue;

                  if( connection.EndPoints.Contains( ( thisPath.EndLocation.Item1, thisPath.EndLocation.Item2 ) ) )
                  {
                     node.LeadsTo.Add( connection, thisPath );
                     foundEnd = true;
                     break;
                  }
               }
               if( !foundEnd )
                  throw new Exception( );
            }
         } //End path exploring

      //Now we have all the nodes with all the weights, find the longest path in the tree
         List<long> allPaths = new List<long>( );
         HashSet<GardenMapNode> visited = new HashSet<GardenMapNode>( );
         visited.Add( start );
         long currentLength = 0;
         FindPath( visited, start, currentLength, allPaths );

         StringBuilder sb = new StringBuilder( );
         foreach( var s in allPaths )
         {
            sb.Append( s.ToString( ) + ", " );
         }
         Console.Write( sb.ToString( ) );

         return allPaths.Max( );
      }

      protected static void FindPath( HashSet<GardenMapNode> visited, GardenMapNode currentNode, long currentLength, List<long> paths )
      {
         foreach( KeyValuePair<GardenMapNode, GardenPath> c in currentNode.LeadsTo )
         {
            long newLength = currentLength + c.Value.Length + 2; //2 is the length of each node
            if( c.Key.IsGlobalEnd )
            {
               paths.Add( newLength - 2 ); //Subtract two for start and endnode..
            }

            if( !visited.Contains( c.Key ) )
            {

            //Copy the hashset
               HashSet<GardenMapNode> newVisited = new HashSet<GardenMapNode>( visited );
               newVisited.Add( c.Key );
               FindPath( newVisited, c.Key, newLength, paths );
            }
         }
      }


   #endregion

   }
}