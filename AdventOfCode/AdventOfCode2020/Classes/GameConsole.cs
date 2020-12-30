using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{

   /// <summary>
   /// A class that represents the hanheld game console used in dec08 of advent of code
   /// </summary>
   public class GameConsole
   {


   /*ENUM*/
      #region


      /// <summary>
      /// An enum that describes what type of instruction abortion that was encountered
      /// </summary>
      public enum GAMECONSOLEABORTION
      {
         DUPLICATEINSTRUCTION,         //The game console aborted because is read an previously used instruction
         ENDOFINSTRUCTION              //The game console aborted because it reached an instruction below the instruction line
      }


      public enum GAMECONSOLEINSTRUCTION
      {
         NOP = 1,    //No operation. The line below it is executed next 
         ACC = 2,     //The accumulator is incremented by the value, then the next line is excecuted next
         JMP = 3     //Jump to a specified line.
      }

      #endregion


   /*LOCAL CLASSES*/
      #region

      /// <summary>
      /// A game console instruction with a corresponding value;
      /// </summary>
      public class GameConsoleInstruction
      {
         public GameConsoleInstruction( GAMECONSOLEINSTRUCTION ins, int va )
         {
            this.Instruction = ins;
            this.InstructionValue = va;
            this.HasBeenRun = false;
         }

         public GAMECONSOLEINSTRUCTION Instruction { get; set; }
         public int InstructionValue { get; set; }
         public bool HasBeenRun { get; set; }
      }

      #endregion



   /*MEMBERS*/
      #region

      protected long m_Accumulator = 0;
      protected GameConsoleInstruction[] m_Instructions;
      protected int m_Position;

      #endregion




   /*CONSTRUCTORS*/
      #region


      /// <summary>
      /// Default constructor
      /// </summary>
      /// <param name="instruction"></param>
      public GameConsole( string[] instruction )
      {

      //Declare the array of instructions
         m_Instructions = new GameConsoleInstruction[instruction.Length];

      //Create the game console instruction
         for( int i = 0; i<instruction.Length; i++ )
         {
            string thisString = instruction[i];
            string[] spl = thisString.Split( new char[] { ' ' } );
            GAMECONSOLEINSTRUCTION ins = GAMECONSOLEINSTRUCTION.NOP;
            if( spl[0] == "nop" )
               ins = GAMECONSOLEINSTRUCTION.NOP;
            else if( spl[0] == "acc" )
               ins = GAMECONSOLEINSTRUCTION.ACC;
            else if( spl[0] == "jmp" )
               ins = GAMECONSOLEINSTRUCTION.JMP;
            else
               throw new Exception( );

         //Parse the instruction value
            int v;
            bool parsed = int.TryParse( spl[1], out v );
            if( !parsed )
               throw new Exception( );

            GameConsoleInstruction newIns = new GameConsoleInstruction( ins, v );
            m_Instructions[i] = newIns;
         }

      }


      #endregion



      /*PROPERTIES*/
      #region

      /// <summary>
      /// Gets the value of the accumulator
      /// </summary>
      public long Accumulator
      {
         get
         {
            return m_Accumulator;
         }
      }


      /// <summary>
      /// Gets the game console instructions array for permutating
      /// </summary>
      public GameConsoleInstruction[] Instructions
      {
         get
         {
            return m_Instructions;
         }
      }

      #endregion

      /*METHODS*/
      #region


      /// <summary>
      /// Excecute an operation in the operation array
      /// </summary>
      /// <returns></returns>
      public GAMECONSOLEABORTION? Run( )
      {

         //Check if the position is outside the instruction list..
         if( m_Position == m_Instructions.Length )
            return GAMECONSOLEABORTION.ENDOFINSTRUCTION;

      //Check if the instruction has been run before..
         GameConsoleInstruction thisInstruction = m_Instructions[m_Position];

      //Check if the line has been run before..
         if( thisInstruction.HasBeenRun )
            return GAMECONSOLEABORTION.DUPLICATEINSTRUCTION;
         else
         {
            if( thisInstruction.Instruction == GAMECONSOLEINSTRUCTION.NOP )
               GameConsoleOperationNOP( );
            else if( thisInstruction.Instruction == GAMECONSOLEINSTRUCTION.JMP )
               GameConsoleOperationJMP( thisInstruction.InstructionValue );
            else if( thisInstruction.Instruction == GAMECONSOLEINSTRUCTION.ACC )
               GameConsoleOperationACC( thisInstruction.InstructionValue );

            thisInstruction.HasBeenRun = true;

         //Return null so the instructions continue to run
            return null;
         }
      }


      /// <summary>
      /// Operation for the NOP operator of the game console. This should increase the position only and return.
      /// </summary>
      private void GameConsoleOperationNOP( )
      {
         m_Position++;
      }

      /// <summary>
      /// Operation for the JMP operator of the jump console. This should cause the console to jump to the next line
      /// </summary>
      /// <param name="val"></param>
      private void GameConsoleOperationJMP( int val )
      {
         m_Position = m_Position + val;
      }

      /// <summary>
      /// Operation for the ACC operator for the game console. This should increase the accumulator by the given value and go to the next position
      /// </summary>
      /// <param name="val"></param>
      private void GameConsoleOperationACC( int val )
      {
         m_Accumulator += val;
         m_Position++;
      }

      #endregion



   }
}
