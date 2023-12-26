using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class MirrorFieldBeam
   {

   /*ENUMS*/
   #region
      public enum BEAMDIRECTION
      {
         UP,
         DOWN,
         RIGHT,
         LEFT
      }
   #endregion

   /*MEMBERS*/
   #region
      protected int m_RowIdx = -1;
      protected int m_ColIdx = -1;
      protected BEAMDIRECTION m_Direction;
      protected int m_FieldHeight = -1;
      protected int m_FieldWidth = -1;
      protected Tuple<int, int> m_CurrentPosition = null;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public MirrorFieldBeam( int startRowIdx, int startColIdx, BEAMDIRECTION startDirection, int fieldHeight, int fieldWidth )
      {
         m_RowIdx = startRowIdx;
         m_ColIdx = startColIdx;
         m_Direction = startDirection;
         m_FieldHeight = fieldHeight;
         m_FieldWidth = fieldWidth;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public int RowIdx { get { return m_RowIdx; } set { m_RowIdx = value; } }
      public int ColIdx { get { return m_ColIdx; } set { m_ColIdx = value; } }
      public BEAMDIRECTION Direction { get { return m_Direction; } set { m_Direction = value; } }
      public Tuple<int,int> Position { get { return m_CurrentPosition; } }
      public Tuple<int,int,BEAMDIRECTION> PositionWithDirection { get { return Tuple.Create( m_RowIdx, m_ColIdx, m_Direction ); } }
   #endregion

   /*METHODS*/
   #region

      public bool Move( )
      {
         if( m_Direction == BEAMDIRECTION.UP )
         {
            m_RowIdx--;
            if( m_RowIdx < 0 )
               return false;
         }
         else if( m_Direction == BEAMDIRECTION.DOWN )
         {
            m_RowIdx++;
            if( m_RowIdx > m_FieldHeight - 1 )
               return false;
         }
         else if( m_Direction == BEAMDIRECTION.LEFT )
         {
            m_ColIdx--;
            if( m_ColIdx < 0 )
               return false;
         }
         else if( m_Direction == BEAMDIRECTION.RIGHT )
         {
            m_ColIdx++;
            if( m_ColIdx > m_FieldWidth - 1 )
               return false;
         }
         m_CurrentPosition = Tuple.Create( m_RowIdx, m_ColIdx );
         return true;
      }

      public void Turn( char symb )
      {
         if( symb == '\\' )
         {
            if( m_Direction == BEAMDIRECTION.UP )
               m_Direction = BEAMDIRECTION.LEFT;
            else if( m_Direction == BEAMDIRECTION.DOWN )
               m_Direction = BEAMDIRECTION.RIGHT;
            else if( m_Direction == BEAMDIRECTION.LEFT )
               m_Direction = BEAMDIRECTION.UP;
            else if( m_Direction == BEAMDIRECTION.RIGHT )
               m_Direction = BEAMDIRECTION.DOWN;
         }
         else if( symb == '/' )
         {
            if( m_Direction == BEAMDIRECTION.UP )
               m_Direction = BEAMDIRECTION.RIGHT;
            else if( m_Direction == BEAMDIRECTION.DOWN )
               m_Direction = BEAMDIRECTION.LEFT;
            else if( m_Direction == BEAMDIRECTION.LEFT )
               m_Direction = BEAMDIRECTION.DOWN;
            else if( m_Direction == BEAMDIRECTION.RIGHT )
               m_Direction = BEAMDIRECTION.UP;
         }
      }

      public static ( MirrorFieldBeam, MirrorFieldBeam ) Split( MirrorFieldBeam baseBeam )
      {
         BEAMDIRECTION? dir1 = null;
         BEAMDIRECTION? dir2 = null;
         if( baseBeam.Direction == BEAMDIRECTION.UP || baseBeam.Direction == BEAMDIRECTION.DOWN )
         {
            dir1 = BEAMDIRECTION.LEFT;
            dir2 = BEAMDIRECTION.RIGHT;
         }
         else if( baseBeam.Direction == BEAMDIRECTION.RIGHT || baseBeam.Direction == BEAMDIRECTION.LEFT )
         {
            dir1 = BEAMDIRECTION.UP;
            dir2 = BEAMDIRECTION.DOWN;
         }
         return( new MirrorFieldBeam( baseBeam.RowIdx, baseBeam.ColIdx, ( BEAMDIRECTION ) dir1, baseBeam.m_FieldHeight, baseBeam.m_FieldWidth ), new MirrorFieldBeam( baseBeam.RowIdx, baseBeam.ColIdx, ( BEAMDIRECTION ) dir2, baseBeam.m_FieldHeight, baseBeam.m_FieldWidth ) );
      }

   #endregion

   }


}
