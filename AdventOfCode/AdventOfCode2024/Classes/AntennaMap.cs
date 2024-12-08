using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Classes
{
   public class AntennaMap
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
      public class Antenna
      {
         public Antenna( int row, int col, char symbol )
         {
            this.Location = ( row, col );
            this.Symbol = symbol;
         }

         public char Symbol { get; set; }
         public ( int Row, int Col ) Location { get; set; }

         public ( int DeltaRow, int DeltaCol ) GetDistBetweenAntennas( Antenna other )
         {
            int dRow = other.Location.Row - this.Location.Row;
            int dCol = other.Location.Col - this.Location.Col;
            return ( dRow, dCol );
         }


      }
   #endregion

   /*MEMBERS*/
   #region
      protected HashSet<(int Row, int Col)> m_AntiNodes = new HashSet<(int Row, int Col)>( );
      protected Dictionary<char, List<Antenna>> m_TypeAntennaMap = new Dictionary<char, List<Antenna>>( );
      protected int m_RowCount = -1;
      protected int m_ColCount = -1;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public AntennaMap( string[] inp )
      {
         m_RowCount = inp.Length;
         m_ColCount = inp[0].Length;

         for( int i = 0; i<inp.Length; i++ )
         {
            for( int j = 0; j<inp[0].Length; j++ )
            {
               if( inp[i][j] == '.' || inp[i][j] == '#' )
                  continue;
               else
               {
               //Create the list in the antennamap
                  if( !m_TypeAntennaMap.ContainsKey(inp[i][j] ) )
                     m_TypeAntennaMap.Add( inp[i][j], new List<Antenna>( ) );

               //Create the antenna and add to the list
                  m_TypeAntennaMap[inp[i][j]].Add( new Antenna( i, j, inp[i][j] ) );
               }
            }

         }
      }
   #endregion

   /*METHODS*/
   #region

      public Dictionary<(int,int),Antenna> GetAllAntennas( )
      {
         Dictionary<( int RowPos, int ColPos ),Antenna> result = new Dictionary<(int RowPos, int ColPos), Antenna>( );
         foreach( KeyValuePair<char, List<Antenna>> kvp in m_TypeAntennaMap )
            foreach( Antenna a in kvp.Value )
               result.Add( a.Location, a );
         return result;
      }


      public void PrintState( )
      {

         Dictionary<(int, int), Antenna> allAntennas = GetAllAntennas( );
         for( int i = 0; i<m_RowCount; i++ )
         {
            StringBuilder sb = new StringBuilder( );
            for( int j = 0; j<m_ColCount; j++ )
            {

            //Check if there are any antennas at this location..
               if( allAntennas.ContainsKey( ( i, j ) ) )
                  sb.Append( allAntennas[ ( i, j ) ].Symbol.ToString( ) );
               else if( m_AntiNodes.Contains( ( i, j ) ) )
                  sb.Append( "#" );
               else
                  sb.Append( "." );
            }

            Console.WriteLine( sb.ToString( ) );
         }
      }

      public int P1( )
      {

      //Loop over all antennas..
         foreach( KeyValuePair<char, List<Antenna>> kvp in m_TypeAntennaMap )
         {

         //Loop over each antenna of this type and create the antinode if it doesnt already exist..
            for( int i = 0; i<kvp.Value.Count-1; i++ )
            {
               for( int j = i+1; j<kvp.Value.Count; j++ )
               {
                  ( int DeltaRow, int DeltaCol ) dist = kvp.Value[i].GetDistBetweenAntennas( kvp.Value[j] );

               //First antinode position..
                  int node1Row = kvp.Value[j].Location.Row + dist.DeltaRow;
                  int node1Col = kvp.Value[j].Location.Col + dist.DeltaCol;

                  int node2Row = kvp.Value[i].Location.Row - dist.DeltaRow;
                  int node2Col = kvp.Value[i].Location.Col - dist.DeltaCol;

                  if( node1Row >= 0 && node1Col >= 0 && node1Row < m_RowCount && node1Col < m_ColCount )
                     m_AntiNodes.Add( ( node1Row, node1Col ) );

                  if( node2Row >= 0 && node2Col >= 0 && node2Row < m_RowCount && node2Col < m_ColCount )
                     m_AntiNodes.Add( ( node2Row, node2Col ) );
               }
            }
         }
         return m_AntiNodes.Count;
      }


      public int P2( )
      {
      //Loop over all antennas..
         foreach( KeyValuePair<char, List<Antenna>> kvp in m_TypeAntennaMap )
         {
         //Loop over each antenna of this type and create the antinode if it doesnt already exist..
            for( int i = 0; i<kvp.Value.Count-1; i++ )
            {
               for( int j = i+1; j<kvp.Value.Count; j++ )
               {
                  ( int DeltaRow, int DeltaCol ) dist = kvp.Value[i].GetDistBetweenAntennas( kvp.Value[j] );
                  bool addedSomething = true;
                  int distMultiPlierPos = 1;
                  int distMultiPlierNeg = -1;
                  while( addedSomething )
                  {
                     int node1Row = kvp.Value[i].Location.Row + distMultiPlierPos * dist.DeltaRow;
                     int node1Col = kvp.Value[i].Location.Col + distMultiPlierPos * dist.DeltaCol;
                     int node2Row = kvp.Value[j].Location.Row + distMultiPlierNeg * dist.DeltaRow;
                     int node2Col = kvp.Value[j].Location.Col + distMultiPlierNeg * dist.DeltaCol;
                     if( node1Row >= 0 && node1Col >= 0 && node1Row < m_RowCount && node1Col < m_ColCount )
                     {
                        m_AntiNodes.Add( ( node1Row, node1Col ) );
                        addedSomething = true;
                     }
                     else
                        addedSomething = false;

                     if( node2Row >= 0 && node2Col >= 0 && node2Row < m_RowCount && node2Col < m_ColCount )
                     {
                        m_AntiNodes.Add( ( node2Row, node2Col ) );
                        addedSomething = true;
                     }
                     else
                        addedSomething = false;

                     distMultiPlierPos++;
                     distMultiPlierNeg--;
                  }
               }
            }
         }

         return m_AntiNodes.Count;

      }
   #endregion








   }
}
