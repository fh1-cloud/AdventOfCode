using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Classes
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
      protected HashSet<MapNode> m_Connections = null;
      protected long m_Value;

   #endregion

   /*CONSTRUCTORS*/
   #region
      public MapNode( long value )
      {
         m_Value = value;
         m_Connections = new HashSet<MapNode>( );
      }
   #endregion

   /*PROPERTIES*/
   #region
      public long Value { get { return m_Value; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Finds the shortest path between two MapNodes by using Djikstras algorithm. A priority is used.
      /// </summary>
      /// <param name="startNode"></param>
      /// <param name="endNode"></param>
      /// <param name="allNodes"></param>
      /// <returns></returns>
      public static long GetShortestPathBetweenNodes( MapNode startNode, MapNode endNode, HashSet<MapNode> allNodes )
      {
         HashSet<MapNode> visitedNodes = new HashSet<MapNode>( );
         SimplePriorityQueue<MapNode> listWithDistances = new SimplePriorityQueue<MapNode>( );
         foreach( MapNode node in allNodes )
            listWithDistances.Enqueue( node, int.MaxValue );
         listWithDistances.UpdatePriority( startNode, 0 );

      //Get the current node. Its the first unvisited node in the sorted list..
         MapNode currentNode = startNode;
         do
         {
         //Get neighbours of current node.
            List<MapNode> neighbours = currentNode.GetNeighbours( );

         //Calculate distance
            foreach( MapNode n in neighbours )
            {
            //Skip if visited.
               if( visitedNodes.Contains( n ) )
                  continue;

            //Calculate the tentative distance for this node. Update neighbours
               long newTent = ( long ) listWithDistances.GetPriority( currentNode ) + n.Value;
               if( newTent <  ( long ) listWithDistances.GetPriority( n ) )
                  listWithDistances.UpdatePriority( n, newTent );
            }
         //Mark the current node as visited for the neighbour function
            visitedNodes.Add( currentNode );

         //Break if this is the endnode. No need to check any more
            if( currentNode == endNode )
               break;

         //Get next node
            listWithDistances.Remove( currentNode );
            if( listWithDistances.Count > 0 )
               currentNode = listWithDistances.First;
            else
               currentNode = null;

         //Exit condition..
            if( currentNode == null  ) //All nodes visited. the shortest distance is the endnode distance..
               break;

         } while( currentNode != null );

      //Return the end node value.
         return ( long ) listWithDistances.GetPriority( endNode );
      }

   #endregion

   /*METHODS*/
   #region
      public void AddConnection( MapNode child )
      {
         if( !m_Connections.Contains( child ) )
            m_Connections.Add( child );
      }

      public List<MapNode> GetNeighbours( )
      {
         List<MapNode> neighbours = new List<MapNode>();
         foreach( MapNode n in m_Connections )
            neighbours.Add( n );
         return neighbours;
      }

   #endregion


   }
}
