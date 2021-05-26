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

      /// <summary>
      /// An enum that describes the modes for an operation to the intcode computer
      /// </summary>
      public enum MODE
      {
         POSITION,
         IMMEDIATE,
         RELATIVE
      }

   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected long[] m_Values;
      protected long m_CurrentPosition = 0;
      protected long m_RelativeBase = 0;


      protected long m_InputIdx = 0;  //A number that counts the number of times the software hits an input instruction. This also serves as the index for where the inputs should be gotten from.
      protected long[] m_Input = null;

      protected bool m_ShouldSaveOutput = false;
      protected long m_OutputSignal = -1;

      protected bool m_ReachedEnd = false;

      protected long m_NumberOfOperations = 0;        //A counter that tracks how many operations that have been carried out.

   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Initializes an intcode computer with a comma seperated instruction line
      /// </summary>
      /// <param name="inp"></param>
      public IntcodeComputer( string inp, bool shouldSaveOutput = false )
      {
      //Split string by comma
         string[] split = inp.Split( new char[] { ',' } );
         m_Values = new long[100000];

      //Add the instruction to the array of instructions
         for( int i = 0; i < split.Length; i++ )
            m_Values[i] = long.Parse( split[i] );

      //Set the save parameter..
         m_ShouldSaveOutput = shouldSaveOutput;
      }

      /// <summary>
      /// Initializes an intcode computer with a list of inputs. If the computer should save output, the output is saved rather than written to console. if inputs array is null, the software will ask for input from the user.
      /// </summary>
      /// <param name="inp">The programme</param>
      /// <param name="inputs">The input. If null the console will ask for the input each time an opcode 3 is reached </param>
      /// <param name="shouldSaveOutput">A flag that decides if the programme should save the output to the output-signal variable, or if it should print it to the console.</param>
      public IntcodeComputer( string inp, long[] inputs, bool shouldSaveOutput ) : this( inp )
      {
      //Checks number of inputs.
         m_Input = inputs;
         m_ShouldSaveOutput = shouldSaveOutput;
      }


   #endregion

   /*PROPERTIES*/
   #region

      public long Output => m_OutputSignal;        //Gets the last output signal written to the output signal variable
      public bool ReachedEnd => m_ReachedEnd;      //A flag that is set to true if the intcode computer reached opcode 99;

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
      public void RunIntCode( int startIdx = 0, bool haltAtOutput = false )
      {
      //Sets the starting position for this intcode computer
         if( startIdx != -1 )
            m_CurrentPosition = startIdx;

      //Runs the intcode computer until it reaches an halting point
         bool keepRunning = true;
         while( keepRunning )
         {
            keepRunning = RunInstruction( haltAtOutput );
            m_NumberOfOperations++;
         }

      }


      /// <summary>
      /// Runs the intcode computer without an input parameter. Resets the input parameter. If the "restartPosition" is true, the intcode current position is reset.
      /// </summary>
      /// <param name="haltAtOutput"></param>
      /// <param name="restartPosition"></param>
      public void RunIntCode( bool haltAtOutput, bool restartPosition )
      {
         m_Input = null;
         m_InputIdx = 0;

         if( restartPosition )
            RunIntCode( 0, haltAtOutput );
         else
            RunIntCode( -1, haltAtOutput ); 
      }


      /// <summary>
      /// Run the intcode computer with the chosen inputs.
      /// </summary>
      /// <param name="inputs"></param>
      public void RunIntCode( long[] inputs )
      {
      //Restart the input index and set the input.
         m_InputIdx = 0;
         m_Input = inputs;

      //Run the intcode computer until it reached an halting point.
         bool keepRunning = true;
         bool haltAtOutput = true;
         while( keepRunning )
         {
            keepRunning = RunInstruction( haltAtOutput );
            m_NumberOfOperations++;
         }

      }


      /// <summary>
      /// Runs the current at the current index. If a termination opcode was reached, it returns false
      /// </summary>
      /// <returns></returns>
      private bool RunInstruction( bool haltAtOutput = false )
      {
         //long opcode = m_Values[m_CurrentPosition];

         MODE[] modes;
         long operation = m_Values[m_CurrentPosition];
         long opcode = GetOpCodeWithModes( operation, out modes );

         if( opcode == 1 )
         {
            Opcode1( modes );
         }
         else if( opcode == 2 )
         {
            Opcode2( modes );
         }
         else if( opcode == 3 ) //Write operation
         {
            Opcode3( modes );
         }
         else if( opcode == 4 )     //Print/outout operation
         {
            Opcode4( modes );

         //If the intcode computer should stop running after it hit an output, this is where it returns.
            if( haltAtOutput )
               return false;
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
         else if( opcode == 9 )
         {
            Opcode9( modes );
         }
         else if( opcode == 99 )
         {
            m_ReachedEnd = true;
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

         long newVal = val1 + val2;
         SetValue( m_CurrentPosition + 3, newVal, modes[2] );

         m_CurrentPosition += 4;
      }

      /// <summary>
      /// Opcode 2. multiplies two numbers together and stores them in a new position. Then increments the current position by 4
      /// </summary>
      private void Opcode2( MODE[] modes )
      {
         long val1 = GetValue( m_CurrentPosition + 1, modes[0] );
         long val2 = GetValue( m_CurrentPosition + 2, modes[1] );

         long newVal = val1*val2;
         SetValue( m_CurrentPosition+3, newVal, modes[2] );

         m_CurrentPosition += 4;
      }


      /// <summary>
      /// Opcode 3. Takes a single integer as input, and saves it to the position given by its only parameter. For example the instruction 3,50 would take an input value and store it at adress 50.
      /// </summary>
      private void Opcode3( MODE[] modes )
      {
      //Parameters that are written to will never be inn immediate mode.
         if( modes[0] == MODE.IMMEDIATE )
            throw new Exception( );


      //If the input parameter array is null, the user is asked to provide the data.
         long inpVal;
         if( m_Input == null )
         {
            Console.WriteLine( "Input parameter:" );
            string input = Console.ReadLine( );
            inpVal = long.Parse( input );
         }
         else //The input parameter is not null, this means that the intcode computer was started with some parameter data.
         {
            inpVal = m_Input[m_InputIdx];
            m_InputIdx++;
         }

      //Write parameter.
         SetValue( m_CurrentPosition + 1, inpVal, modes[0] );

      //Increment position.
         m_CurrentPosition += 2;
      }


      /// <summary>
      /// Opcode 4. Outputs the value of its only parameter. For example, the instruction 4,50 woul ouput the calue at address 50.
      /// </summary>
      /// <param name="adress"></param>
      private void Opcode4( MODE[] modes )
      {
         long whatToWrite = GetValue( m_CurrentPosition + 1, modes[0] );

         if( m_ShouldSaveOutput )
            m_OutputSignal = whatToWrite;
         else
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
            SetValue( m_CurrentPosition + 3, 1, modes[2] );
         else
            SetValue( m_CurrentPosition + 3, 0, modes[2] );

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
            SetValue( m_CurrentPosition + 3, 1, modes[2] );
         else
            SetValue( m_CurrentPosition + 3, 0, modes[2] );

         m_CurrentPosition += 4;
      }


      /// <summary>
      /// Opcode 9. Adjusts the relative base by the value of its only parameter. The relative base increases (or decreases if the value is negative) by the value of the parameter
      /// </summary>
      /// <param name="modes"></param>
      private void Opcode9( MODE[] modes )
      {
         long val1 = GetValue( m_CurrentPosition + 1, modes[0] );
         m_RelativeBase += val1;
         m_CurrentPosition += 2;
      }


      /// <summary>
      /// Sets the values in the memory.
      /// </summary>
      /// <param name="idx"></param>
      /// <param name="value"></param>
      /// <param name="mode"></param>
      private void SetValue( long idx, long value, MODE mode )
      {
         if( mode == MODE.POSITION )
            m_Values[m_Values[idx]] = value;
         else if( mode == MODE.RELATIVE )
            m_Values[m_Values[idx]+m_RelativeBase] = value;
         else if( mode == MODE.IMMEDIATE )
            throw new Exception( );
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
         else if( mode == MODE.RELATIVE )
            return m_Values[m_Values[idx] + m_RelativeBase ];
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
         MODE[] modes = new MODE[3];

         int arrIdx = 0;
         for( int i = modeString.Length-1; i>=0; i-- )
         {
            if( modeString[i] == '1' )
               modes[arrIdx++] = MODE.IMMEDIATE;
            else if( modeString[i] == '2' )
               modes[arrIdx++] = MODE.RELATIVE;
            else if( modeString[i] == '0' )
               modes[arrIdx++] = MODE.POSITION;
            else
               throw new Exception( );
         }
         return modes;
      }


      /// <summary>
      /// A method that creates all possible phase settings combinations and returns them in a list.
      /// </summary>
      /// <param name="start"></param>
      /// <param name="end"></param>
      /// <returns></returns>
      public static List<long[]> GetPhaseSettings( bool partTwo )
      {
         if( !partTwo )
         {
            List<long[]> phase = new List<long[]>( );
            for( long i = 0; i < 5; i++ )
               for( long j = 0; j < 5; j++ )
                  for( long k = 0; k < 5; k++ )
                     for( long l = 0; l < 5; l++ )
                        for( long m = 0; m < 5; m++ )
                           if( i == j || i == k || i == l || i == m || j == k || j == l || j == m || k == l || k == m || l == m )
                              continue;
                           else
                              phase.Add( new long[5] { i, j, k, l, m } );
            return phase;
         }
         else
         {
            List<long[]> phase = new List<long[]>( );
            for( long i = 5; i < 10; i++ )
               for( long j = 5; j < 10; j++ )
                  for( long k = 5; k < 10; k++ )
                     for( long l = 5; l < 10; l++ )
                        for( long m = 5; m < 10; m++ )
                           if( i == j || i == k || i == l || i == m || j == k || j == l || j == m || k == l || k == m || l == m )
                              continue;
                           else
                              phase.Add( new long[5] { i, j, k, l, m } );
            return phase;
         }
      }

   #endregion




   }
}
