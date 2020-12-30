using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{

   /// <summary>
   /// A class that represents the exhibit of hex tiles 
   /// </summary>
   public class HexTileExhibit
   {

   /*MEMBERS*/
   #region
      protected Dictionary<KeyValuePair<double, double>, HexTile> m_Tiles;
      protected HashSet<KeyValuePair<double,double>> m_TilesWithAllNeighbours = new HashSet<KeyValuePair<double, double>>( );
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Creates a hex tile exhibit from a dictionary of created tiles.
      /// </summary>
      /// <param name="createdTiles"></param>
      public HexTileExhibit( Dictionary<KeyValuePair<double,double>,HexTile> createdTiles )
      {
         m_Tiles = createdTiles;
      }



      /// <summary>
      /// Creates a deep copy of a hex tile exhibit
      /// </summary>
      /// <param name="oldSet"></param>
      public HexTileExhibit( HexTileExhibit oldSet )
      {
         m_Tiles = new Dictionary<KeyValuePair<double, double>, HexTile>( );
         foreach( KeyValuePair<KeyValuePair<double, double>, HexTile> o in oldSet.m_Tiles )
         {
            HexTile newHex = new HexTile( o.Value );
            m_Tiles.Add( o.Key, newHex );
         }

         m_TilesWithAllNeighbours = new HashSet<KeyValuePair<double, double>>( );

      }


   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Prints the debugging state to the console window
      /// </summary>
      public void PrintTextState( int day )
      {
         int nOfBlackTiles = m_Tiles.Where( x => x.Value.State == HexTile.HEXCOLOR.BLACK ).ToList( ).Count;
         Console.WriteLine( "Day " + day.ToString( ) + ":  " + nOfBlackTiles.ToString( ) );
      }


      /// <summary>
      /// Run one simulation of the hexes
      /// </summary>
      public void RunOneStateChange( )
      {
      //Creates the neighbouring tiles..
         CreateNonExistantNeighbourTiles( this );

      //Store the current state in the oldSet variable to check for flips. Then flip the state in the current state.
         HexTileExhibit oldSet = new HexTileExhibit( this );

      //First, loop through the old set and create all the neighbours that does not exist yet.
         foreach( KeyValuePair<KeyValuePair<double, double>, HexTile> o in oldSet.m_Tiles )
         {
            List<HexTile> neighbours = GetNeighbours( oldSet, o.Value );

            //COunt the number of black neighbours
            int blackNeighbours = 0;
            foreach( HexTile n in neighbours )
            {
               if( n == null )
                  continue;
               if( n.State == HexTile.HEXCOLOR.BLACK )
                  blackNeighbours++;
            }

            if( o.Value.State == HexTile.HEXCOLOR.BLACK )
            {
               if( blackNeighbours == 0 || blackNeighbours > 2 )
                  m_Tiles[o.Key].State = HexTile.HEXCOLOR.WHITE;
            }
            else if( o.Value.State == HexTile.HEXCOLOR.WHITE )
            {
               if( blackNeighbours == 2 )
                  m_Tiles[o.Key].State = HexTile.HEXCOLOR.BLACK;
            }
         }

      }


   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Gets the neighbours for this hex tile
      /// </summary>
      /// <param name="mainTile"></param>
      /// <returns></returns>
      public static List<HexTile> GetNeighbours( HexTileExhibit collection, HexTile mainTile )
      {

      //Declare the returning list of tiles.
         List<HexTile> retTiles = new List<HexTile>( );

      //Look for a tile adjacent of this tile.
         List<KeyValuePair<double, double>> ienum = GetNeighbourCoordinates( mainTile );
         foreach( KeyValuePair<double, double> coor in ienum )
         {
            if( !collection.m_Tiles.ContainsKey( coor ) )
               retTiles.Add( null );
            else
               retTiles.Add( collection.m_Tiles[coor] );

         }
      //return the list of tiles..
         return retTiles;
      }


      /// <summary>
      /// Gets a list of coordinates for the neighbours for this tile
      /// </summary>
      /// <param name="mainTile"></param>
      /// <returns></returns>
      public static List<KeyValuePair<double, double>> GetNeighbourCoordinates( HexTile mainTile )
      {
      //Find the coordinates to look for in the dictionary
         KeyValuePair<double, double> west = new KeyValuePair<double, double>( mainTile.Coordinates.Key, mainTile.Coordinates.Value - 1.0 );
         KeyValuePair<double, double> east = new KeyValuePair<double, double>( mainTile.Coordinates.Key, mainTile.Coordinates.Value + 1.0 );
         KeyValuePair<double, double> northeast = new KeyValuePair<double, double>( mainTile.Coordinates.Key-1.0, mainTile.Coordinates.Value + 0.5 );
         KeyValuePair<double, double> northwest = new KeyValuePair<double, double>( mainTile.Coordinates.Key-1.0, mainTile.Coordinates.Value - 0.5 );
         KeyValuePair<double, double> southeast = new KeyValuePair<double, double>( mainTile.Coordinates.Key+1.0, mainTile.Coordinates.Value + 0.5 );
         KeyValuePair<double, double> southwest = new KeyValuePair<double, double>( mainTile.Coordinates.Key+1.0, mainTile.Coordinates.Value - 0.5 );

      //Look for a tile adjacent of this tile.
         List<KeyValuePair<double, double>> ienum = new List<KeyValuePair<double, double>>( );
         ienum.Add( west );
         ienum.Add( east );
         ienum.Add( northeast );
         ienum.Add( northwest );
         ienum.Add( southeast );
         ienum.Add( southwest );

      //Return the list..
         return ienum;
      }


      /// <summary>
      /// Initializes neighbours of currently existing tiles. Initializes them as white. THen returns a list of the newly created non existant tiles for the ignore list
      /// </summary>
      /// <param name="collection"></param>
      public static void CreateNonExistantNeighbourTiles( HexTileExhibit collection )
      {
      //Create a copy of the collection because of iterating over the tiles..
         HexTileExhibit ienum = new HexTileExhibit( collection );

      //Loop through all the tiles in the dictionary and create non existant tiles.
         foreach( KeyValuePair<KeyValuePair<double, double>, HexTile> o in ienum.m_Tiles )
         {
            if( collection.m_TilesWithAllNeighbours.Contains( o.Key ) )
               continue;

         //Get the list of coordinates for the neighbours
            List<KeyValuePair<double, double>> neighbourCoord = GetNeighbourCoordinates( o.Value );

         //Loop through the list and create the tiles that does not exist. Set the color to white
            foreach( KeyValuePair<double, double> coord in neighbourCoord )
               if( !collection.m_Tiles.ContainsKey( coord ) )
                  collection.m_Tiles.Add( coord, new HexTile( HexTile.HEXCOLOR.WHITE, coord ) );

         //Now we have added all the neighbours to this tile.
            collection.m_TilesWithAllNeighbours.Add( o.Key );
         }

      }




   #endregion




   }
}
