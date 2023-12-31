using AdventOfCodeLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   internal class SnowMachinePartKargers
   {

      public static long P1( string[] input )
      {

         Random r = new Random( 4 );
      //Run kargers algorithm until it finds one with only three edges.
         ( int cutSize, int c1, int c2 ) = FindCut( input, r );
         while( cutSize != 3 )
            ( cutSize, c1, c2 ) = FindCut( input, r );

         return c1 * c2;

      }

      // https://en.wikipedia.org/wiki/Karger%27s_algorithm
      // Karger's algorithm finds a cut of a graph and returns its size. 
      // It's not necessarily the minimal cut, because it's a randomized algorithm 
      // but it's 'likely' to find the minimal cut in reasonable time. 
      // The algorithm is extended to return the sizes of the two components 
      // separated by the cut as well.
      public static ( int size, int c1, int c2 ) FindCut( string[] input, Random r )
      {

      //Create a new copy of the graph..
         Dictionary<string, List<string>> graph = Parse( input );
         Dictionary<string, int> clusterSizes = graph.Keys.ToDictionary( a => a, _ => 1 ); //Create a dictionary with the merge ID as key, and the merged size as a value

         // decrease the the number of nodes by one. First select two nodes u 
         // and v connected with an edge. Introduce a new node that inherits 
         // every edge going out of these (excluding the edges between them). 
         // Set the new nodes' component size to the sum of the component 
         // sizes of u and v. Remove u and v from the graph.
         for( var id = 0; graph.Count > 2; id++ )
         {

         //Select a node at random
            int randomIdx = r.Next( graph.Count );
            string node1 = graph.Keys.ElementAt( randomIdx );

         //Select a random child of this node
            string node2 = graph[node1][ r.Next( graph[node1].Count ) ];
            
         //Merge the two nodes into one
            string key = MergeNodesIntoNew( graph, node1, node2, id );

         //Add the clustersize to the dictionary
            clusterSizes[key] = clusterSizes[node1] + clusterSizes[node2];
         }

         // two nodes remain with some edges between them, the number of those 
         // edges equals to the size of the cut. Component size tells the number 
         // of nodes in the two sides created by the cut.
         string nodeA = graph.Keys.First( );
         string nodeB = graph.Keys.Last( );
         int cutSize = graph[nodeA].Count( );
         return ( cutSize, clusterSizes[nodeA], clusterSizes[nodeB] );

      }

      //Merges the old node in to the new node
      public static string MergeNodesIntoNew( Dictionary<string,List<string>> graph, string oldNode1, string oldNode2, int id )
      {
      //Declare list of new children
         List<string> newChildren = new List<string>( );
         string newName = "merge-" + id;

      //Collect new children and add to list
         foreach( string node in graph[oldNode1] )
         {
            if( node == oldNode2 )
               continue;
            newChildren.Add( node );
         }
         foreach( string node in graph[oldNode2] )
         {
            if( node == oldNode1 )
               continue;
            newChildren.Add( node );
         }

      //Change name of all occurences of the old nodes to the new id..
         foreach( KeyValuePair<string, List<string>> kvp in graph )
            for( int i = 0; i<kvp.Value.Count; i++ )
               if( kvp.Value[i] == oldNode1 || kvp.Value[i] == oldNode2 )
                  kvp.Value[i] = newName;

      //Remove the old nodes from the graph now that they have been merged
         graph.Remove( oldNode1 );
         graph.Remove( oldNode2 );
         graph.Add( newName, newChildren );

         return newName;
      }
      

      //Parse input array to a graph
      public static Dictionary<string, List<string>> Parse( string[] input )
      {
         Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>( );
         foreach( string line in input )
         {
            string[] spl = line.Split( new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries );
            string[] c = spl[1].Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
            foreach( string cs in c )
            {
               RegisterEdge( graph, spl[0], cs );
               RegisterEdge( graph, cs, spl[0] );
            }
         }
         return graph;
      }

      /// Creates an edge between two nodes in the graph
      public static void RegisterEdge( Dictionary<string, List<string>> graph, string node1, string node2 )
      {
         if( !graph.ContainsKey( node1 ) )
            graph.Add( node1, new List<string>( ) );
        graph[node1].Add( node2 );
      }

   }
}
