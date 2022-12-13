using AdventOfCodeLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class MapNode
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected bool m_IsVisited = false;
      protected char m_CharValue;
      protected long m_ShortestRoadTo = long.MaxValue;
      protected int m_RowIdx = -1;
      protected int m_ColIdx = -1;
      protected long m_ShortestStartingPath = long.MaxValue;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// default constructor..
      /// </summary>
      /// <param name="c"></param>
      /// <param name="rowIdx"></param>
      /// <param name="colIdx"></param>
      public MapNode( char c, int rowIdx, int colIdx )
      {
         m_CharValue = c;
         m_IsVisited = false;
         m_RowIdx = rowIdx;
         m_ColIdx = colIdx;
      }

      /// <summary>
      /// Copy constructor..
      /// </summary>
      /// <param name="oldNode"></param>
      public MapNode( MapNode oldNode )
      {
         m_IsVisited = oldNode.m_IsVisited;
         m_CharValue = oldNode.m_CharValue;
         m_ShortestRoadTo = oldNode.m_ShortestRoadTo;
         m_RowIdx = oldNode.m_RowIdx;
         m_ColIdx = oldNode.m_ColIdx;
         m_ShortestStartingPath = oldNode.m_ShortestStartingPath;
      }


   #endregion

   /*PROPERTIES*/
   #region
      public bool Visited { get { return m_IsVisited; } set { m_IsVisited = value; } }
      public char NodeValue { get { return m_CharValue; } set { m_CharValue = value; } }
      public long ShortestRoadTo { get { return m_ShortestRoadTo; } set { m_ShortestRoadTo = value; } }
      public int RowIdx { get { return m_RowIdx; } }
      public int ColIdx { get { return m_ColIdx; } }
      public long StartingPathLength { get { return m_ShortestStartingPath; } set { m_ShortestStartingPath = value; } }

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Prints the heatmap to the console..
      /// </summary>
      /// <param name="terrain"></param>
      public static void PrintHeatmap( MapNode[,] terrain )
      {
         for( int i = 0; i< terrain.GetLength( 0 ); i++ )
         {
            StringBuilder sb = new StringBuilder( );
            for( int j = 0; j < terrain.GetLength( 1 ); j++ )
            {
               sb.Append( string.Format( "{0,4}", terrain[i,j].ShortestRoadTo ) );
            }
            Console.WriteLine( sb.ToString( ) );
         }
      }

      /// <summary>
      /// Creates a list of the unvisited neighbours of the current node..
      /// </summary>
      /// <param name="terrain"></param>
      /// <returns></returns>
      public static List<MapNode> GetNeighbours( MapNode currNode, MapNode[,] terrain )
      {
      //Declare the returning list of valid neighbours..
         List<MapNode> validNeighbours = new List<MapNode>( );

      //Add all the indexes that are adjacent. Then check if it is possible to go there..
         List<MapNode> potentialNeighbours = new List<MapNode>( );
         if( currNode.RowIdx != 0 )
            potentialNeighbours.Add( terrain[currNode.RowIdx - 1, currNode.ColIdx] );
         if( currNode.RowIdx != terrain.GetLength( 0 ) - 1 )
            potentialNeighbours.Add( terrain[currNode.RowIdx + 1, currNode.ColIdx] );
         if( currNode.ColIdx != 0 )
            potentialNeighbours.Add( terrain[currNode.RowIdx, currNode.ColIdx-1] );
         if( currNode.ColIdx != terrain.GetLength( 1 ) - 1 )
            potentialNeighbours.Add( terrain[currNode.RowIdx, currNode.ColIdx+1] );

         if( currNode.NodeValue == 'S' )
         {
            foreach( MapNode n in potentialNeighbours )
               validNeighbours.Add( n );
         }
         else
         {
            foreach( MapNode n in potentialNeighbours )
            {
               if( n.NodeValue == 'E' )
                  validNeighbours.Add( n );
               else if( ( ( int ) n.NodeValue - ( int ) currNode.NodeValue ) <= 1 && !n.m_IsVisited )
                  validNeighbours.Add( n );
            }
         }

      //Return the completed set of neighbours..
         return validNeighbours;

      }
   #endregion

   /*METHODS*/
   #region
   #endregion

   }
}
