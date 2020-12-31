using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{
   public class IntcodeComputer
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected long[] m_Values;
      protected long m_CurrentPosition = 0;

   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Initializes an intcode computer with a comma seperated instruction line
      /// </summary>
      /// <param name="inp"></param>
      public IntcodeComputer( string inp )
      {
      //Split string by comma
         string[] split = inp.Split( new char[] { ',' } );
         m_Values = new long[split.Length];

      //Add the instruction to the array of instructions
         for( int i = 0; i < split.Length; i++ )
            m_Values[i] = long.Parse( split[i] );

      }



   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region

      /// <summary>
      /// Gets the value at the chosen index in the intcode computer
      /// </summary>
      /// <param name="idx"></param>
      /// <returns></returns>
      public long this[ int idx ]
      {
         get
         {
            if( idx > m_Values.Length - 1 )
               throw new Exception( );
            else
               return m_Values[idx];
         }
         set
         {
            if( idx > m_Values.Length - 1 )
               throw new Exception( );
            else
               m_Values[idx] = value;
         }
      }



   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Runs the intcode computer at the chosen start index
      /// </summary>
      /// <param name="startIdx"></param>
      public void RunIntcode( int startIdx = 0 )
      {
      //Sets the starting position for this intcode computer
         m_CurrentPosition = startIdx;

      //Runs the intcode computer until it reaches an halting point
         bool keepRunning = true;
         while( keepRunning )
         {
            keepRunning = RunInstruction( );
         }

      }


      /// <summary>
      /// Runs the current at the current index. If a termination opcode was reached, it returns false
      /// </summary>
      /// <returns></returns>
      private bool RunInstruction( )
      {
         long opcode = m_Values[m_CurrentPosition];
         if( opcode == 1 )
         {
            Opcode1( );
         }
         else if( opcode == 2 )
         {
            Opcode2( );
         }
         else if( opcode == 99 )
         {
            return false;
         }
         else
            throw new Exception( );

      //Return true since the computer should keep on running
         return true;
      }



      /// <summary>
      /// Opcode 1. Adds two numbers together and stores them in a new position. Then increments the current position by 4
      /// </summary>
      private void Opcode1( )
      {
         long val1 = m_Values[m_Values[m_CurrentPosition + 1]];
         long val2 = m_Values[m_Values[m_CurrentPosition + 2]];
         m_Values[m_Values[m_CurrentPosition + 3]] = val1 + val2;
         m_CurrentPosition += 4;
      }

      /// <summary>
      /// Opcode 2. multiplies two numbers together and stores them in a new position. Then increments the current position by 4
      /// </summary>
      private void Opcode2( )
      {
         long val1 = m_Values[m_Values[m_CurrentPosition + 1]];
         long val2 = m_Values[m_Values[m_CurrentPosition + 2]];
         m_Values[m_Values[m_CurrentPosition + 3]] = val1 * val2;
         m_CurrentPosition += 4;
      }




      /// <summary>
      /// Prints the current state to the console.
      /// </summary>
      public void PrintState()
      {
         string print = "";

         for( int i = 0; i < m_Values.Length; i++ )
         {
            print += m_Values[i].ToString( );
            if( i != m_Values.Length - 1 )
               print += ",";
         }
         Console.WriteLine( print );
      }


   #endregion

   /*STATIC METHODS*/
   #region
   #endregion




   }
}
