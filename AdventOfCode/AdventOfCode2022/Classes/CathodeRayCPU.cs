using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class CathodeRayCPU
   {

   /*ENUMS*/
   #region
      public enum INSTRUCTION
      {
         NOOP,
         ADDX
      }
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected static HashSet<long> CyclesToCheckStrength = new HashSet<long>( );
      protected static bool[,] Canvas = null;
      protected static int CanvasCols = 40;
      protected static int CanvasRows = 6;
      protected static char PixelLit = '#';
      protected static char PixelOff = '.';

      protected long m_Cycles = 0;
      protected long m_XRegister = 0;
      protected bool m_PrintStatus = false;

      protected List<long> m_SignalStrengths = new List<long>( );
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Declares the important cycles to check the signal strengths..
      /// </summary>
      static CathodeRayCPU( )
      {
      //Declare the canvas..
         Canvas = new bool[CanvasRows, CanvasCols];
         for( int i = 0; i < CanvasRows; i++ )
            for( int j = 0; j < CanvasCols; j++ )
               Canvas[i,j] = false;

         CyclesToCheckStrength.Add( 20 );
         CyclesToCheckStrength.Add( 60 );
         CyclesToCheckStrength.Add( 100 );
         CyclesToCheckStrength.Add( 140 );
         CyclesToCheckStrength.Add( 180 );
         CyclesToCheckStrength.Add( 220 );
      }

      /// <summary>
      /// Default constructor. Initializes a cpu with the starting value of the x register as the input. Cycles is set to zero
      /// </summary>
      /// <param name="xRegisterStart"></param>
      public CathodeRayCPU( long xRegisterStart, bool printStatus )
      {
         m_XRegister = xRegisterStart;
         m_PrintStatus = printStatus;
      }


   #endregion

   /*PROPERTIES*/
   #region
      public long XRegister { get { return m_XRegister; } }
      public long Cycles { get { return m_Cycles; } set { m_Cycles = value; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// A method that returns the current canvas position from the current input cycle..
      /// </summary>
      /// <param name="currentCycle"></param>
      /// <returns></returns>
      public static ( int, int ) GetCanvasPositionFromCycle( int currentCycle )
      {
      //Find the rowcount..
         int rowIdx = currentCycle/40;
         int colIdx = currentCycle%40-1;

      //Return the rows and columns..
         return ( rowIdx, colIdx );
      }
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Event handler for the cycles changed event.. This will check if the current cycle is in the list of accepted values and store the signal strength..
      /// </summary>
      /// <param name="currentCycle"></param>
      private void CheckSignalStrength( )
      {
      //Check if the current cycle is in the accepted values..
         if( CyclesToCheckStrength.Contains( m_Cycles ) )
         {
         //Add the signal strength..
            m_SignalStrengths.Add( ( m_Cycles ) * m_XRegister );

         //Print status if requested..
            if( m_PrintStatus )
               Console.WriteLine( "During the " + m_Cycles.ToString( ) + " cycle the X register is " + m_XRegister.ToString( ) + ". The signal strength is " + m_Cycles.ToString( ) + " * " + m_XRegister.ToString( ) + " = " + m_SignalStrengths[m_SignalStrengths.Count - 1].ToString( ) );
         }
      }

      /// <summary>
      /// Carries out the instructions..
      /// </summary>
      /// <param name="ins">The instruction to be carried out..</param>
      /// <param name="inpVal1">The input value for the instruction, if any</param>
      public void CarryOutInstructionPart2( INSTRUCTION ins, bool printStatus = false, long? inpVal1 = null )
      {
         if( ins == INSTRUCTION.NOOP )
         {
            m_Cycles++;
            PaintIfMatch( );
         }
         else if( ins == INSTRUCTION.ADDX && inpVal1 != null )
         {
            m_Cycles++;
            PaintIfMatch( );
            m_Cycles++;
            PaintIfMatch( );
            m_XRegister += ( long ) inpVal1; //Cycles are finished. increment the x-register..
         }
         else
            throw new Exception( );
      }

      /// <summary>
      /// Paint the pixel if it is a match..
      /// </summary>
      public void PaintIfMatch( )
      {
      //Draw the Pixel IF the x-position +-1 is equal to the cycle number..
         if( (m_Cycles%40)- m_XRegister == 2 || (m_Cycles%40)- m_XRegister == 1 || (m_Cycles%40)- m_XRegister == 0 )
         {
            ( int rowIdx, int colIdx ) = GetCanvasPositionFromCycle( (int ) m_Cycles );
            if( colIdx != -1 )
               Canvas[rowIdx, colIdx] = true;
         }
      }

      /// <summary>
      /// Writes the screen..
      /// </summary>
      public string GetScreenContent( )
      {
      //Declare the canvas..
         StringBuilder total = new StringBuilder( );
         for( int i = 0; i < CanvasRows; i++ )
         {
            StringBuilder currentRow = new StringBuilder( );
            for( int j = 0; j < CanvasCols; j++ )
            {
               if( Canvas[i,j] )
                  currentRow.Append( PixelLit );
               else
                  currentRow.Append( PixelOff );
            }
            total.AppendLine( currentRow.ToString( ) );
         }
         return total.ToString( );
      }

      /// <summary>
      /// Carries out the instructions..
      /// </summary>
      /// <param name="ins">The instruction to be carried out..</param>
      /// <param name="inpVal1">The input value for the instruction, if any</param>
      public void CarryOutInstructionPart1( INSTRUCTION ins, bool printStatus = false, long? inpVal1 = null )
      {
         if( ins == INSTRUCTION.NOOP )
         {
            m_Cycles++;
            CheckSignalStrength( );
         }
         else if( ins == INSTRUCTION.ADDX && inpVal1 != null )
         {
            m_Cycles++;
            CheckSignalStrength( );
            m_Cycles++;
            CheckSignalStrength( );
            m_XRegister += ( long ) inpVal1; //Cycles are finished. increment the x-register..

         }
         else
         {
            throw new Exception( );
         }
      }

      /// <summary>
      /// Gets the total signal strength for output..
      /// </summary>
      /// <returns></returns>
      public long GetTotalSignalStrength( )
      {
         return m_SignalStrengths.Sum( );
      }


   #endregion
   }
}
