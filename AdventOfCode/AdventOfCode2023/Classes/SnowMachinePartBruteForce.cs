using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class SnowMachinePartBruteForce
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
   #endregion

   /*CONSTRUCTORS*/
   #region
      protected SnowMachinePartBruteForce( string name )
      {
         this.Connections = new HashSet<string>( );
         this.Name = name;
      }

      protected SnowMachinePartBruteForce( SnowMachinePartBruteForce oldPart )
      {
         this.Name = oldPart.Name;
         this.Connections = new HashSet<string>( oldPart.Connections );
      }

   #endregion

   /*PROPERTIES*/
   #region
      public string Name { get; set; }
      public HashSet<string> Connections { get; set; }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region


      public static void CreatePartsFromLine( string line, Dictionary<string, SnowMachinePartBruteForce> allParts )
      {
         string[] spl = line.Split( new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries );

      //Try to create the first one if it doesnt exist..
         SnowMachinePartBruteForce mainPart = null;
         if( !allParts.ContainsKey( spl[0] ) )
         {
            mainPart = new SnowMachinePartBruteForce( spl[0] );
            allParts.Add( mainPart.Name, mainPart );
         }
         else
            mainPart = allParts[spl[0]];

      //For each children, create if it doesnt exist..
         string[] spl2 = spl[1].Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
         foreach( string s in spl2 )
         {

         //Create child if it doesnt exist..
            SnowMachinePartBruteForce child = null;
            if( !allParts.ContainsKey( s ) )
            {
               child = new SnowMachinePartBruteForce( s );
               allParts.Add( child.Name, child );
            }
            else
               child = allParts[s];

         //Create connection to this part if it doesnt already exist..
            if( !mainPart.Connections.Contains( child.Name ) )
               mainPart.Connections.Add( child.Name );

         //Create two way connection from child to main part just in case..
            if( !child.Connections.Contains( mainPart.Name ) )
               child.Connections.Add( mainPart.Name );
         }


      }


      public static long BruteForceP1( Dictionary<string,SnowMachinePartBruteForce> allParts )
      {
      //Get a list of all the connections..
         List<Tuple<string, string>> allConnections = GetAllConnections( allParts );

         for( int i = 0; i< allConnections.Count; i++ )
         {
            for( int j = i + 1; j < allConnections.Count; j++ )
            {
               for( int k = j + 1; k < allConnections.Count; k++ )
               {
                  Dictionary<string, SnowMachinePartBruteForce> dc = DeepCopy( allParts );
                  SeverConnection( dc, allConnections[i], allConnections[j], allConnections[k] );
                  List<HashSet<string>> groups = GetGroups( dc );
                  if( groups.Count == 2 )
                     return groups[0].Count*groups[1].Count;
               }
            }
         }

      //If the code reached here, throw
         throw new Exception( );
      }

      protected static void SeverConnection( Dictionary<string,SnowMachinePartBruteForce> allParts, Tuple<string,string> c1, Tuple<string,string> c2, Tuple<string,string> c3 )
      {
      //c1
         allParts[c1.Item1].Connections.Remove( c1.Item2 );
         allParts[c1.Item2].Connections.Remove( c1.Item1 );

      //c2
         allParts[c2.Item1].Connections.Remove( c2.Item2 );
         allParts[c2.Item2].Connections.Remove( c2.Item1 );

      //c3
         allParts[c3.Item1].Connections.Remove( c3.Item2 );
         allParts[c3.Item2].Connections.Remove( c3.Item1 );
      }

      /// <summary>
      /// Gets all connections in the graph
      /// </summary>
      /// <param name="allParts"></param>
      /// <returns></returns>
      protected static List<Tuple<string,string>> GetAllConnections( Dictionary<string,SnowMachinePartBruteForce> allParts )
      {
      //Create hash set of all connections
         HashSet<(string,string)> allConnections = new HashSet<(string, string)>( );
         foreach( KeyValuePair<string, SnowMachinePartBruteForce> kvp in allParts )
            foreach( string s in kvp.Value.Connections )
               if( !allConnections.Contains( ( kvp.Key, s ) ) && !allConnections.Contains( ( s, kvp.Key ) ) )
                  allConnections.Add( ( kvp.Key, s ) );

      //Create the tuple list
         List<Tuple<string,string>> c = new List<Tuple<string, string>>( );
         foreach( (string I1, string I2) s in allConnections )
            c.Add( Tuple.Create( s.I1, s.I2 ) );

         return c;
      }

      /// <summary>
      /// Creates a deep copy of all the machine parts
      /// </summary>
      /// <param name="dictToCopy"></param>
      /// <returns></returns>
      protected static Dictionary<string,SnowMachinePartBruteForce> DeepCopy( Dictionary<string,SnowMachinePartBruteForce> dictToCopy )
      {
         Dictionary<string, SnowMachinePartBruteForce> ret = new Dictionary<string, SnowMachinePartBruteForce>( );
         foreach( var kvp in dictToCopy )
            ret.Add( kvp.Key, new SnowMachinePartBruteForce( kvp.Value ) );
         return ret;
      }


      /// <summary>
      /// Gets distinct groups of the graph
      /// </summary>
      /// <param name="allParts"></param>
      /// <returns></returns>
      protected static List<HashSet<string>> GetGroups( Dictionary<string,SnowMachinePartBruteForce> allParts )
      {
      //Create the list of all distinct groups
         List<HashSet<string>> groups = new List<HashSet<string>>( );

      //Loop over all the parts and try to create groups from every part

      //Create a copy of the all parts dictionary..
         foreach( KeyValuePair<string, SnowMachinePartBruteForce> kvp in allParts )
         {
         //First, check if this part is found in another group..
            bool foundIt = false;
            foreach( HashSet<string> g in groups )
            {
               if( g.Contains( kvp.Key ) )
               {
                  foundIt = true;
                  break;
               }
            }
            if( !foundIt ) //We didnt find it in any of the groups. Create a new group
            {
            //Create a new dictionary..
               HashSet<string> newGroup = new HashSet<string>( );
               kvp.Value.PopulateDictionaryOfGroup( newGroup, allParts );
               groups.Add( newGroup );
            }

         }

         return groups;
      }

      /// <summary>
      /// Create a dictionary of all the parts in this group
      /// </summary>
      /// <param name="partsInGroup"></param>
      protected void PopulateDictionaryOfGroup( HashSet<string> partsInGroup, Dictionary<string,SnowMachinePartBruteForce> allParts )
      {
      //Try to add this.
         if( !partsInGroup.Contains( this.Name ) )
            partsInGroup.Add( this.Name );
         else //Return if this is already added.
            return;

      //Do the same for all children..
         foreach( string p in this.Connections )
            allParts[p].PopulateDictionaryOfGroup( partsInGroup, allParts );
      }



   #endregion

   /*METHODS*/
   #region


   #endregion


   }
}
