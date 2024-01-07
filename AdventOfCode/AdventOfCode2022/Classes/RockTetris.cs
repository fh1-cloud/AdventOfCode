using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class RockTetris
   {

   /*ENUMS*/
   #region

      public enum BRICKSHAPE
      {
         LINE,
         PLUSS,
         J,
         I,
         BLOCK
      }

      public enum MOVEINSTRUCTION
      {
         DOWN,
         LEFT,
         RIGHT
      }

   #endregion

   /*LOCAL CLASSES*/
   #region
      public class RockTetrisBrick
      {

         public RockTetrisBrick( BRICKSHAPE shape )
         {
            OccupiedPositions = new HashSet<(int, int)>( );

            this.Shape = shape;
            if( this.Shape == BRICKSHAPE.LINE )
               this.Width = 4;
            else if( this.Shape == BRICKSHAPE.PLUSS || this.Shape == BRICKSHAPE.J )
               this.Width = 3;
            else if( this.Shape == BRICKSHAPE.BLOCK )
               this.Width = 2;
            else if( this.Shape == BRICKSHAPE.I )
               this.Width = 1;
         }

         public BRICKSHAPE Shape { get; private set; }
         public int Height { get; set; } //Left bottom end for all.
         public int WidthIndex { get; set; }
         public int Width { get; private set; }
         public HashSet<(int,int)> OccupiedPositions { get; set; }

         public void Move( MOVEINSTRUCTION ins )
         {
            if( ins == MOVEINSTRUCTION.DOWN )
               this.Height--;
            else //Movement left or right
            {
               if( ins == MOVEINSTRUCTION.LEFT )
               {
                  if( this.WidthIndex > 0 )
                     this.WidthIndex--;
               }
               else //Instruction is right
               {
                  if( this.WidthIndex + this.Width < 6 )
                     this.WidthIndex++;
               }
            }
         }



      }
   #endregion

   /*MEMBERS*/
   #region
      protected static string Instructions = null;
   #endregion

   /*CONSTRUCTORS*/
   #region

   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region


      public static int ColumnIndexAfterFallingNSteps( int nOfSteps, int startingColIdx )
      {
         int stepsTaken = 0;
         int idx = 0;
         int currentIdx = startingColIdx;
         while( stepsTaken< nOfSteps )
         {
            char ins = Instructions[idx];
            if( ins == '<' )
               currentIdx = Math.Max( 0, currentIdx - 1 );
            if( ins == '>' )
               currentIdx = Math.Min( 6, currentIdx + 1 );

            idx = ( idx + 1 ) % Instructions.Length;
            stepsTaken++;
         }
         return currentIdx;
      }


      public static long P1( string[] inp )
      {

      //Parse instructionrs
         Instructions = inp[0];
         List<MOVEINSTRUCTION> instructions = new List<MOVEINSTRUCTION>( );
         foreach( char c in inp[0] )
         {
            if( c == '<' )
               instructions.Add( MOVEINSTRUCTION.LEFT );
            else if( c == '>' )
               instructions.Add( MOVEINSTRUCTION.RIGHT );
            else
               throw new Exception( );
         }

      //Create base bricks
         Dictionary<int,RockTetrisBrick> baseBricks = new( );
         baseBricks.Add( 0, new RockTetrisBrick( BRICKSHAPE.LINE ) );
         baseBricks.Add( 1, new RockTetrisBrick( BRICKSHAPE.PLUSS ) );
         baseBricks.Add( 2, new RockTetrisBrick( BRICKSHAPE.J ) );
         baseBricks.Add( 3, new RockTetrisBrick( BRICKSHAPE.I ) );
         baseBricks.Add( 4, new RockTetrisBrick( BRICKSHAPE.BLOCK ) );


      }


   #endregion

   /*METHODS*/
   #region
   #endregion

   }
}
