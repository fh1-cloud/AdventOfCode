using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class MirrorField
   {

   /*MEMBERS*/
   #region
      protected Dictionary<Tuple<int,int>, char> m_Field = new Dictionary<Tuple<int, int>, char>( );
      protected HashSet<Tuple<int,int>> m_EnergizedTiles = new HashSet<Tuple<int, int>>( );
      protected HashSet<Tuple<int,int,MirrorFieldBeam.BEAMDIRECTION>> m_Cache = new HashSet<Tuple<int, int, MirrorFieldBeam.BEAMDIRECTION>>( );
      protected int m_FieldHeight = -1;
      protected int m_FieldWidth = -1;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public MirrorField( string[] field )
      {
         m_FieldHeight = field.Length;
         m_FieldWidth = field[0].Length;
         for( int i = 0; i<field.Length; i++ )
         {
            for( int j = 0; j<field[i].Length; j++ )
            {
               if( field[i][j] == '.' )
                  continue;
               m_Field.Add( Tuple.Create( i, j ), field[i][j] );
            }
         }
      }
   #endregion

   /*PROPERTIES*/
   #region
      public int Height { get { return m_FieldHeight; } }
      public int Width { get { return m_FieldWidth; } }
   #endregion

   /*METHODS*/
   #region

      public long GetNumberOfEnergizedTiles( )
      {
         return m_EnergizedTiles.Count;
      }

      public void PropagateRay( MirrorFieldBeam beam )
      {
         bool beamExists = true;
         while( beamExists )
         {
         //Move the beam.
            beamExists = beam.Move( );

            if( !beamExists )
               return;

         //Energize tile..
            if( beamExists && !m_EnergizedTiles.Contains( beam.Position ) )
               m_EnergizedTiles.Add( beam.Position );
            if( !m_Field.ContainsKey( beam.Position ) )
               continue;

         //Act
            if( m_Field[beam.Position] == '\\' || m_Field[beam.Position] == '/' )
            {
               beam.Turn( m_Field[beam.Position] );
            }
            else if( ( m_Field[beam.Position] == '-' && ( beam.Direction == MirrorFieldBeam.BEAMDIRECTION.UP || beam.Direction == MirrorFieldBeam.BEAMDIRECTION.DOWN ) ) || ( ( m_Field[beam.Position] == '|' && ( beam.Direction == MirrorFieldBeam.BEAMDIRECTION.LEFT || beam.Direction == MirrorFieldBeam.BEAMDIRECTION.RIGHT ) ) ) )
            {
               ( MirrorFieldBeam splitBeam1, MirrorFieldBeam splitBeam2 ) = MirrorFieldBeam.Split( beam );
               if( !m_Cache.Contains( splitBeam1.PositionWithDirection ) )
               {
                  m_Cache.Add( splitBeam1.PositionWithDirection );
                  PropagateRay( splitBeam1 );
               }
               if( !m_Cache.Contains( splitBeam2.PositionWithDirection ) )
               {
                  m_Cache.Add( splitBeam2.PositionWithDirection );
                  PropagateRay( splitBeam2 );
               }
               beamExists = false;
            }
         }
      }
   #endregion



   }
}
