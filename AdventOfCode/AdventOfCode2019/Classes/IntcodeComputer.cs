using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{
   /*ENUMS*/
   #region
      public enum MODE
      {
         POSITION,
         IMMEDIATE,
      }
   #endregion


   public class IntcodeComputer
   {


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
         //long opcode = m_Values[m_CurrentPosition];

         MODE[] modes;
         long opcode = GetOpCodeWithModes( m_Values[m_CurrentPosition], out modes );

         if( opcode == 1 )
         {
            Opcode1( modes );
         }
         else if( opcode == 2 )
         {
            Opcode2( modes );
         }
         else if( opcode == 3 )
         {
            Opcode3( );
         }
         else if( opcode == 4 )
         {
            Opcode4( modes );
         }
         else if( opcode == 5 )
         {
            Opcode5( modes );
         }
         else if( opcode == 6 )
         {
            Opcode6( modes );
         }
         else if( opcode == 7 )
         {
            Opcode7( modes );
         }
         else if( opcode == 8 )
         {
            Opcode8( modes );
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
      private void Opcode1( MODE[] modes )
      {
         long val1 = GetValue( m_CurrentPosition + 1, modes[0] );
         long val2 = GetValue( m_CurrentPosition + 2, modes[1] );

         //long val1 = m_Values[m_Values[m_CurrentPosition + 1]];
         //long val2 = m_Values[m_Values[m_CurrentPosition + 2]];

         m_Values[m_Values[m_CurrentPosition + 3]] = val1 + val2;
         m_CurrentPosition += 4;
      }

      /// <summary>
      /// Opcode 2. multiplies two numbers together and stores them in a new position. Then increments the current position by 4
      /// </summary>
      private void Opcode2( MODE[] modes )
      {

         long val1 = GetValue( m_CurrentPosition + 1, modes[0] );
         long val2 = GetValue( m_CurrentPosition + 2, modes[1] );

         //long val1 = m_Values[m_Values[m_CurrentPosition + 1]];
         //long val2 = m_Values[m_Values[m_CurrentPosition + 2]];

         m_Values[m_Values[m_CurrentPosition + 3]] = val1 * val2;
         m_CurrentPosition += 4;
      }


      /// <summary>
      /// Opcode 3. Takes a single integer as input, and saves it to the position given by its only parameter. For example the instruction 3,50 would take an input value and store it at adress 50.
      /// </summary>
      private void Opcode3( )
      {
         long address = m_Values[m_CurrentPosition + 1];

      //Convert to long..
         Console.WriteLine( "Input parameter:" );
         string input = Console.ReadLine( );
         long convertVal = long.Parse( input );

      //Add values..
         m_Values[address] = convertVal;

      //Increment position.
         m_CurrentPosition += 2;
      }


      /// <summary>
      /// Opcode 4. Outputs the value of its only parameter. For example, the instruction 4,50 woul ouput the calue at address 50.
      /// </summary>
      /// <param name="adress"></param>
      private void Opcode4( MODE[] modes )
      {
         long whatToWrite;
         if( modes[0] == MODE.POSITION )
            whatToWrite = m_Values[m_Values[m_CurrentPosition + 1]];
         else
            whatToWrite = m_Values[m_CurrentPosition]; 

         Console.WriteLine( whatToWrite );

      //Increment position
         m_CurrentPosition += 2;
      }


      /// <summary>
      /// Opcode 5 is jump if true. If the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise it does nothing
      /// </summary>
      /// <param name="modes"></param>
      private void Opcode5( MODE[] modes )
      {
         long val1 = GetValue( m_CurrentPosition + 1, modes[0] );
         long val2 = GetValue( m_CurrentPosition + 2, modes[1] );

         if( val1 != 0 )
            m_CurrentPosition = val2;
         else
            m_CurrentPosition += 3;
      }

      /// <summary>
      /// Opcode 6 is jump if false. If the first parameter is zero, it sets the instruction pointer to the calue from the second parameter. Otherwise it does nothing
      /// </summary>
      /// <param name="modes"></param>
      private void Opcode6( MODE[] modes )
      {
         long val1 = GetValue( m_CurrentPosition + 1, modes[0] );
         long val2 = GetValue( m_CurrentPosition + 2, modes[1] );

         if( val1 == 0 )
            m_CurrentPosition = val2;
         else
            m_CurrentPosition += 3;
      }


      /// <summary>
      /// Opcode 7 is less than. If the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise it stores 0.
      /// </summary>
      /// <param name="modes"></param>
      private void Opcode7( MODE[] modes )
      {
         long val1 = GetValue( m_CurrentPosition + 1, modes[0] );
         long val2 = GetValue( m_CurrentPosition + 2, modes[1] );

         if( val1 < val2 )
            m_Values[m_Values[m_CurrentPosition + 3]] = 1;
         else
            m_Values[m_Values[m_CurrentPosition + 3]] = 0;

         m_CurrentPosition += 4;
      }

      /// <summary>
      /// Opcode 8 equals . If the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise it stores 0.
      /// </summary>
      /// <param name="modes"></param>
      private void Opcode8( MODE[] modes )
      {
         long val1 = GetValue( m_CurrentPosition + 1, modes[0] );
         long val2 = GetValue( m_CurrentPosition + 2, modes[1] );

         if( val1 == val2 )
            m_Values[m_Values[m_CurrentPosition + 3]] = 1;
         else
            m_Values[m_Values[m_CurrentPosition + 3]] = 0;

         m_CurrentPosition += 4;
      }




      /// <summary>
      /// Gets the value, based on the mode of the index. If the mode is POSITION the value of the address in the position is returned. if the mode is IMMEDIATE, the value at the adress is returned.
      /// </summary>
      /// <param name="idx"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      private long GetValue( long idx, MODE mode )
      {
         if( mode == MODE.POSITION )
            return m_Values[m_Values[idx]];
         else if( mode == MODE.IMMEDIATE )
            return m_Values[idx];
         else
            throw new Exception( );
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


      /// <summary>
      /// Infers the Opcode from a long together with the modes.
      /// </summary>
      /// <param name="code"></param>
      /// <param name="modes"></param>
      /// <returns></returns>
      public static long GetOpCodeWithModes( long code, out MODE[] modes )
      {

      //Extract the two digit rightmost values..
         string codeString = code.ToString( );
         if( codeString.Length <= 2 )
         {
            long opCode = long.Parse( codeString );
            modes = GetModesFromInstruction( opCode, "" );
            return long.Parse( codeString );
         }
         else
         {
         //Create the substring of the rightmost two numbers
            string valString = codeString.Substring( 0, codeString.Length - 2 );
            string opString = codeString.Substring( codeString.Length - 2, 2 );
            long opCode = long.Parse( opString );
            modes = GetModesFromInstruction( opCode, valString );
            return opCode;
         }

      }


      /// <summary>
      /// Gets the modes for the operation from an opcode.
      /// </summary>
      /// <param name="opcode">The opcode.</param>
      /// <param name="modeString">The modes for the parameters. modes[0] is for parameter 1 etc.</param>
      /// <returns></returns>
      public static MODE[] GetModesFromInstruction( long opcode, string modeString )
      {
      //This method needs to know about the lengths of variables of the opcodes..
         MODE[] modes = null;
         if( opcode == 1 || opcode == 2 || opcode == 5 || opcode == 6 || opcode == 7 || opcode == 8 )
            modes = new MODE[2];
         else if( opcode == 3 || opcode == 4 )
            modes = new MODE[1];

         int arrIdx = 0;
         for( int i = modeString.Length-1; i>=0; i-- )
         {
            if( modeString[i] == '1' )
               modes[arrIdx++] = MODE.IMMEDIATE;
            else if( modeString[i] == '0' )
               modes[arrIdx++] = MODE.POSITION;
            else
               throw new Exception( );
         }
         return modes;
      }
   #endregion




   }
}
