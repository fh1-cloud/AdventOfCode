using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeLib.Classes;

namespace AdventOfCode2023.Classes
{
   public class DigPlan
   {

   /*LOCAL CLASSES*/
   #region

      internal class DigInstruction
      {
         internal DigInstruction( string line, bool p1 = false )
         {
            string[] spl = line.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
            if( p1 )
            {
               this.DigLength = long.Parse( spl[1] );
               this.Direction = spl[0][0];
            }
            else //P2
            {
               this.DigLength = long.Parse( spl[2].Substring( 2, spl[2].Length - 4 ), System.Globalization.NumberStyles.HexNumber );
               char numDir = spl[2].Substring( spl[2].Length - 2, 1 )[0];
               if( numDir == '0' )
                  this.Direction = 'R';
               else if( numDir == '1' )
                  this.Direction = 'D';
               else if( numDir == '2' )
                  this.Direction = 'L';
               else if( numDir == '3' )
                  this.Direction = 'U';
            }
         }
         internal char Direction { get; set; }
         internal long DigLength { get; set; }
         internal LongPair GetNextVertex( LongPair lastPoint )
         {
            if( this.Direction == 'U' )
               return new LongPair( lastPoint.RowIdx - DigLength, lastPoint.ColIdx );
            else if( this.Direction == 'D' )
               return new LongPair( lastPoint.RowIdx + DigLength, lastPoint.ColIdx );
            else if( this.Direction == 'R' )
               return new LongPair( lastPoint.RowIdx, lastPoint.ColIdx + DigLength );
            else if( this.Direction == 'L' )
               return new LongPair( lastPoint.RowIdx, lastPoint.ColIdx - DigLength );

            throw new Exception( );
         }
      }
   #endregion

   /*MEMBERS*/
   #region
      internal List<DigInstruction> m_Instructions = new List<DigInstruction>( );
      internal List<LongPair> m_Vertices = new List<LongPair>( );
   #endregion

   /*CONSTRUCTORS*/
   #region
      public DigPlan( List<string> input, bool p1 = false )
      {
         m_Vertices.Add( new LongPair( 0,0 ) );
         for( int i = 0; i < input.Count; i++ )
         {
            DigInstruction ins = new DigInstruction( input[i], p1 );
            m_Vertices.Add( ins.GetNextVertex( m_Vertices.LastOrDefault( ) ) );
         }

      }

      public long GetTotalArea( )
      {
      //Calculate total area by using shoelace formula
         long totalArea = 0;
         long perimeter = 0;
         for( int i = 0; i < m_Vertices.Count-1; i++ )
         {
            int firstIdx = i;
            int secondIdx = i + 1;
            
            totalArea += LongPair.Det( m_Vertices[firstIdx], m_Vertices[secondIdx] );
            perimeter += ( long ) new AdventOfCodeLib.Numerics.UVector2D( new AdventOfCodeLib.Numerics.UVector2D( m_Vertices[firstIdx].ColIdx, m_Vertices[firstIdx].RowIdx), new AdventOfCodeLib.Numerics.UVector2D( m_Vertices[secondIdx].ColIdx, m_Vertices[secondIdx].RowIdx ) ).GetLength( );
         }
         totalArea = totalArea/2;

      //Calculate interior by using picks theorem
         long interior = totalArea - perimeter/2 + 1;
         return interior + perimeter;
      }


   #endregion


   }
}
