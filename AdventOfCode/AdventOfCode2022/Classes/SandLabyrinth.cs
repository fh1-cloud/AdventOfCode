using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class SandLabyrinth
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      //Static members
      protected static char m_SymbolRock = '#';
      protected static char m_SymbolVacant = '.';
      protected static char m_SymbolSand = 'o';

      //Non static members
      protected Dictionary<KeyValuePair<int,int>,char> m_OccupiedSlots = new Dictionary<KeyValuePair<int,int>,char>();
      protected int m_MinX = -1;
      protected int m_MaxX = -1;
      protected int m_MinY = 0;
      protected int m_MaxY = -1;
      protected int m_GrainFunnelStart = 500;

   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// LOop over all the input strings and create the rock obstacles..
      /// </summary>
      /// <param name="obstactes"></param>
      public SandLabyrinth( string[] rockVectors, bool part2 = true )
      {
      //Loop over all the rock vectors..
         foreach( string s in rockVectors )
         {
         //SPlit by space. Because of the arrow, each odd entry is a new coordinate..
            string[] splitter = s.Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries );
            List<UVector2D> vertices = new List<UVector2D>( );

         //Create the vertices of the rock labyrinth..
            for( int i =0; i < splitter.Length; i=i+2 )
            {
               string[] numSplit = splitter[i].Split( new char[ ]{ ',' }, StringSplitOptions.RemoveEmptyEntries );
               int xCor = int.Parse( numSplit[0] );
               int yCor = int.Parse( numSplit[1] );
               vertices.Add( new UVector2D( xCor, yCor ) );
            }

         //Loop over all the vertices and create the lines..
            for( int i = 0; i<vertices.Count-1; i++ )
            {
               UVector2D startPoint = vertices[i];
               UVector2D endPoint = vertices[i+1];

            //The vector is in the y direction..
               if( startPoint.X - endPoint.X == 0 )
               {
               //Make sure the points are in the correct order..
                  if( startPoint.Y > endPoint.Y )
                  {
                     UVector2D temp = startPoint;
                     startPoint = endPoint;
                     endPoint = temp;
                  }

                  int xCor = ( int ) startPoint.X;
                  for( int j = ( int ) startPoint.Y; j<= endPoint.Y; j++ )
                  {
                     int yCor = j;
                     KeyValuePair<int,int> coordinate = new KeyValuePair<int, int>( xCor, j );
                     if( !m_OccupiedSlots.ContainsKey( coordinate ) )
                        m_OccupiedSlots.Add( coordinate, m_SymbolRock );
                  }
               }
               else //THe vector is in the x direction..
               {
                  if( startPoint.X > endPoint.X )
                  {
                     UVector2D temp = startPoint;
                     startPoint = endPoint;
                     endPoint = temp;
                  }

                  int yCor = ( int ) startPoint.Y;
                  for( int j = ( int ) startPoint.X; j<= endPoint.X; j++ )
                  {
                     int xCor = j;
                     KeyValuePair<int,int> coordinate = new KeyValuePair<int, int>( j, yCor );
                     if( !m_OccupiedSlots.ContainsKey( coordinate ) )
                        m_OccupiedSlots.Add( coordinate, m_SymbolRock );
                  }
               }
            }

         } //End foreach

      //Find the minimum and maximum x-coordinate of the obstaces. Used to check if the grain of sand fell outside..
         int xMin = 100000;
         int xMax = -100000;
         int yMax = -100000;
         foreach( KeyValuePair<KeyValuePair<int, int>, char> v in m_OccupiedSlots )
         {
            if( v.Key.Key < xMin )
               xMin = v.Key.Key;
            if( v.Key.Key > xMax )
               xMax = v.Key.Key;
            if( v.Key.Value > yMax )
               yMax = v.Key.Value;
         }
         m_MaxX = xMax;
         m_MinX = xMin;
         m_MaxY = yMax;

      //Add floor if part 2..
         if( part2 )
         {
            m_MaxY = m_MaxY+2;

         //Add a really large line at the bottom...
            m_MinX = m_MinX - m_MaxY;
            m_MaxX = m_MaxX + m_MaxY;

         //Add the obstacle..
            for( int i = m_MinX; i<m_MaxX; i++ )
               m_OccupiedSlots.Add( new KeyValuePair<int, int>( i, m_MaxY ), m_SymbolRock );
         }

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


      /// <summary>
      /// Adds a sand grain to the labyrinth. Returns if the grain was not placed..
      /// </summary>
      /// <returns></returns>
      public bool AddSandgrain( bool part2 = true )
      {
      //Adds a grain of sand, returns a bool indicating wheter or not it fell through the labyrinth or not..

      //Check the index 
         int xCur = 500;
         int yCur = 0;

         while( true )
         {
         //Check if there is a sand grain at the top..
            if( part2 )
            {
               if( m_OccupiedSlots.ContainsKey( new KeyValuePair<int, int>( 500, 0 ) ) )
                  return false;
            }
            else //Part 1. Check if it is outside
            {
               if( yCur > m_MaxY )
                  return false;
            }

         //Check below the current slot..
            KeyValuePair<int,int> below = new KeyValuePair<int, int>( xCur, yCur+1);
            if( !m_OccupiedSlots.ContainsKey( below ) )
            {
               yCur++;
               continue;
            }

         //Check below and to the left of the current slot
            KeyValuePair<int,int> belowLeft = new KeyValuePair<int, int>( xCur-1, yCur+1);
            if( !m_OccupiedSlots.ContainsKey( belowLeft ) )
            {
               xCur--;
               yCur++;
               continue;
            }

         //Check below and to the right of the current slot
            KeyValuePair<int,int> belowRight = new KeyValuePair<int, int>( xCur+1, yCur+1);
            if( !m_OccupiedSlots.ContainsKey( belowRight ) )
            {
               xCur++;
               yCur++;
               continue;
            }

         //If the code reached this point, it has to be placed..
            if( yCur < m_MaxY )
            {
               KeyValuePair<int,int> thisPosition = new KeyValuePair<int, int>( xCur, yCur );
               m_OccupiedSlots.Add( thisPosition, m_SymbolSand );
            //The sand grain was placed. Return true
               return true;
            }
         }
      }


      /// <summary>
      /// Prints the current sand labyrinth to the console..
      /// </summary>
      public void PrintGrid( )
      {

      //Draw the grid..
         int offset = 0;
         for( int rows = -1; rows<m_MaxY+1; rows++ )
         {
            StringBuilder sb = new StringBuilder( );
            for( int cols = m_MinX - offset; cols <= m_MaxX+offset; cols++ )
            {
               if( rows == -1 )
               {
                  if( cols == m_GrainFunnelStart )
                     sb.Append( '|'.ToString( ) );
                  else
                     sb.Append( ' '.ToString( ) );
               }
               else
               {
               //Checks if the location is occupied..
                  KeyValuePair<int,int> location = new KeyValuePair<int,int>( cols, rows );
                  if( m_OccupiedSlots.ContainsKey( location ) )
                     sb.Append( m_OccupiedSlots[location].ToString( ) );
                  else
                     sb.Append( m_SymbolVacant.ToString( ) );
               }
            }

         //Write the line to the console..
            Console.WriteLine( sb.ToString( ) );
         }

      //Write empty line at the end..
         Console.WriteLine( );
      }
   #endregion


   }
}
