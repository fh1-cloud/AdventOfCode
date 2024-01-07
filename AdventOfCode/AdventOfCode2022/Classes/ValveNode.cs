using AdventOfCodeLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class ValveNode
   {

   /*MEMBERS*/
   #region
      protected static HashSet<string> ValvesWithPressure;
      protected static Dictionary<string,ValveNode> AllNodes;
   #endregion

   /*CONSTRUCTORS*/
   #region

      static ValveNode( )
      {
         ValvesWithPressure = new HashSet<string>( );
      }

      public ValveNode( )
      {
         this.LenghtoToValves = new Dictionary<string, long>( );
         this.DirectConnections = new HashSet<string>( );
      }
   #endregion


   /*PROPERTIES*/
   #region
      public int FlowRate { get; set; }
      public Dictionary<string,long> LenghtoToValves { get; set; }
      public string Name { get; set; }
      public HashSet<string> DirectConnections { get; set; }
   #endregion

   /*STATIC METHODS*/
   #region

      public static long P2( )
      {
      //P2 //Run through all visits in 26 steps, and find the max of two disjoined sets in the cache
         Dictionary<string, long> cache = new Dictionary<string, long>( );
         AllNodes["AA"].Visit( AllNodes, 0, 26, new HashSet<string>( ), cache );

         long maxPressure = 0;
         foreach( KeyValuePair<string, long> kvp in cache )
            foreach( KeyValuePair<string, long> kvp2 in cache )
               if( IsDisjointed( kvp.Key, kvp2.Key ) )
                  maxPressure = Math.Max( maxPressure, kvp.Value + kvp2.Value );
         return maxPressure;

      }

      public static long P1( )
      {
      //P1
         Dictionary<string, long> cache = new Dictionary<string, long>( );
         AllNodes["AA"].Visit( AllNodes, 0, 30, new HashSet<string>( ), cache );
         long ans = cache.Values.Max( );
         return ans;
      }


      public static bool IsDisjointed( string s1, string s2 )
      {
      //The set is too large to be completely disjointed. Check if they have activated the same node, if so, abort.
         for( int i = 0; i<s1.Length; i++ )
            if( s1[i] == s2[i] && s1[i] == '1' ) 
               return false;

         return true;
      }


      public static void Initialize( string[] inp )
      {
      //Create all the nodes
         AllNodes = new Dictionary<string, ValveNode>( );
         foreach( string s in inp )
         {
            string name = s.Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries )[1];
            int flowRate = int.Parse( s.Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries )[4].Split( new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries )[1].Trim( ';' ) );
            HashSet<string> connectedTo = s.Split( new char[ ] { ';' }, StringSplitOptions.RemoveEmptyEntries )[1].Trim( ',' ).Split( new char[ ] { ' ' }, StringSplitOptions.RemoveEmptyEntries ).Skip( 4 ).Select( x => x.Trim( ',' ) ).ToHashSet( );
            AllNodes.Add( name, new ValveNode{ Name = name, DirectConnections = connectedTo, FlowRate = flowRate } );
         }

      //Create map nodes of all the nodes with a 1 minute connectino to each neighbour
         Dictionary<string,MapNode> nodes = new Dictionary<string, MapNode>( );
         foreach( KeyValuePair<string, ValveNode> kvp in AllNodes )
            nodes.Add( kvp.Key, new MapNode( 1 ) );
         foreach( KeyValuePair<string, ValveNode> kvp in AllNodes )
            foreach( string n in kvp.Value.DirectConnections )
               nodes[kvp.Key].AddConnection( nodes[n] );

      //Find the shortes path to all nodes with a non-zero pressure. Set it on the dictionary of all the nodes.
         HashSet<MapNode> allMapNodes = nodes.Select( x => x.Value ).ToHashSet( );
         foreach( KeyValuePair<string, MapNode> mapNode in nodes )
         {
            foreach( KeyValuePair<string, MapNode> kvp2 in nodes )
            {
               if( mapNode.Key == kvp2.Key || AllNodes[kvp2.Key].FlowRate == 0 )
                  continue;
               long path = MapNode.GetShortestPathBetweenNodes( mapNode.Value, kvp2.Value, allMapNodes );
               AllNodes[mapNode.Key].LenghtoToValves[kvp2.Key] = path;
            }
         }
         ValvesWithPressure = AllNodes.Where( x => x.Value.FlowRate > 0 ).ToList( ).Select( y => y.Key ).ToHashSet( );
      }


      //Visits nodes
      private void Visit( Dictionary<string,ValveNode> allNodes, long flow, long timeLeft, HashSet<string> visitedNodes, Dictionary<string,long> cache )
      {
      //update cache..
         string mask = GetBitMask( visitedNodes );
         cache[mask] = Math.Max( cache.GetValueOrDefault( mask, 0 ), flow );

      //Visit all nodes in the compressed graph..
         foreach( KeyValuePair<string, long> n in allNodes[this.Name].LenghtoToValves )
         {
            long newTime = timeLeft - n.Value - 1;    //Time to walk and time to open.
            if( newTime < 0 || visitedNodes.Contains( n.Key ) ) //Skip if no time left to go there//Skip if we already visited this node in this branch
               continue;

         //If the code reached here, we need to visit it.
            HashSet<string> newVisited = new HashSet<string>( visitedNodes );
            newVisited.Add( n.Key );
            allNodes[n.Key].Visit( allNodes, flow + ( newTime*allNodes[n.Key].FlowRate ), newTime, newVisited, cache );
         }

      }


      //Gets bitmask
      private static string GetBitMask( HashSet<string> openedValves )
      {
         StringBuilder sb = new StringBuilder( );
         foreach( string s in ValvesWithPressure )
            if( openedValves.Contains( s ) )
               sb.Append( "1" );
            else
               sb.Append( "0" );

         return sb.ToString( );
      }

      #endregion



   }
}
