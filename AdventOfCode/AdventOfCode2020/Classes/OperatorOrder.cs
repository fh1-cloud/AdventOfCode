using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{

   /// <summary>
   /// A class that does math calculations with a different operator order than for regular math...
   /// </summary>
   public class OperatorOrder
   {

   /*ENUMS*/
   #region
      public enum OPERATION
      {
         PLUS = 0,
         MULTIPLICATION = 1
      }
   #endregion



   /*MEMBERS*/
   #region
      public static readonly char[] m_Operators = new char[] { '+', '*' };
      public static readonly char[] m_Seperators = new char[] { '(', ')' };
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

   /*METHODS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region




      /// <summary>
      /// A method that finds the other end of the paranthesis in this expression, then returns the inner expression as a string for evaluation
      /// </summary>
      /// <param name="exp"></param>
      /// <returns></returns>
      public static string ExtractExpression( string exp, int startIdx, out int endIdx )
      {

      //Find the other end of this parantheses
         int counter = 1;
         endIdx = -1;
         for( int i = startIdx+1; i< exp.Length; i++ )
         {
            if( exp[i] == '(' )
               counter++;
            else if( exp[i] == ')' )
               counter--;
            if( counter == 0 )
            {
               endIdx = i;
               break;
            }
         }
         string innerExp = exp.Substring( startIdx + 1, endIdx - startIdx - 1 );
         return innerExp;

      }



      /// <summary>
      /// Checks if the input character is an operator. IF so, return true and set the operator
      /// </summary>
      /// <param name="thisChar"></param>
      /// <param name="oper"></param>
      /// <returns></returns>
      public static bool CharIsOperator( char thisChar, out OPERATION? oper )
      {
         oper = null;
         bool foundOperator = false;
         for( int i = 0; i < m_Operators.Length; i++ )
         {
            if( thisChar == m_Operators[i] )
            {
               foundOperator = true;
               oper = ( OPERATION ) i;
               break;
            }
         }
         return foundOperator;
      }



      /// <summary>
      /// Evaluates expression with plus before multiplication
      /// </summary>
      /// <param name="exp"></param>
      /// <returns></returns>
      public static long EvaluateExpressionPart2( string exp )
      {
      //First, group expression by paranthese
         int idxOfG = exp.IndexOf( '(' );

      //There is no paranthesis, evaluate expression with method..
         if( idxOfG == -1 )
            return EvaluateExpressionWithoutParanthesePart2( exp );
         else
         {
         //Group expression by paranthese..
            StringBuilder sb = new StringBuilder( );

            int currIdx = 0;
            int lastStartIdx = 0;
            while( true )
            {
            //Check for the current expression length..
               if( currIdx > exp.Length-1 )
               {
                  if( lastStartIdx != currIdx )
                     sb.Append( exp.Substring( lastStartIdx, currIdx - lastStartIdx ) );
                  break;

               }

               char thisChar = exp[currIdx];
               if( thisChar == '(' )
               {

               //Add everything up to this point..
                  sb.Append( exp.Substring( lastStartIdx, currIdx-lastStartIdx ) );

                  int endIdx;
                  string inner = ExtractExpression( exp, currIdx, out endIdx );
                  long partialAnd = EvaluateExpressionPart2( inner );

                  sb.Append( partialAnd.ToString( ) );
                  currIdx = endIdx + 1;
                  lastStartIdx = currIdx;
                  continue;
               }
               else
                  currIdx++;

            }

            return EvaluateExpressionWithoutParanthesePart2( sb.ToString( ) );
         }


      }



      /// <summary>
      /// EValuates an expression after all parantheses has been removed. Plus goes before multiplication
      /// </summary>
      /// <param name="exp"></param>
      /// <returns></returns>
      public static long EvaluateExpressionWithoutParanthesePart2( string exp )
      {
      //Split string by space. Order the operators..
         string[] split = exp.Split( new char[] { ' ' } );

      //Loop through the split and parse all the numbers..
         List<long> numbersInExpression = new List<long>( );
         List<OPERATION> operationsInExpression = new List<OPERATION>( );

         for( int i = 0; i < split.Length; i = i + 2 )
            numbersInExpression.Add( long.Parse( split[i] ) );

      //Get all the operations..
         for( int i = 1; i < split.Length; i=i+2 )
         {
            OPERATION? thisOper;
            CharIsOperator( split[i][0], out thisOper );
            operationsInExpression.Add( ( OPERATION ) thisOper );
         }

      //Do the additions..
         List<long> incrementalNumbers = new List<long>( numbersInExpression );
         List<OPERATION> incrementalOperations = new List<OPERATION>( operationsInExpression );

      //Loop through the list. Stop at the first plus operator you can find, add the two numbers and add it to the list. Remove the two previous numbers and remove the operator. THen repeat until no plus operator was found
         while( true )
         {

            int addIdx = -1;
            for( int i = 0; i < incrementalOperations.Count; i++ )
            {
               if( incrementalOperations[i] == OPERATION.PLUS )
               {
                  addIdx = i;
                  break;
               }
            }
         //No additino was found. There is only multiplication left to do for the numbers..
            if( addIdx == -1 )
               break;


         //When the code reached this point. two numbers should be added together.
            long number1 = incrementalNumbers[addIdx];
            long number2 = incrementalNumbers[addIdx + 1];

         //evaluate the two..
            long added = EvaluateNumbers( number1, number2, OPERATION.PLUS );

         //Remove the two numbers and insert the computed one..
            incrementalNumbers.RemoveAt( addIdx );
            incrementalNumbers.RemoveAt( addIdx );
            incrementalNumbers.Insert( addIdx, added );

         //Remove this operator.
            incrementalOperations.RemoveAt( addIdx );
         }

      //Do the Multiplications. At this point, there is only multiplication of the rest of the numbers left.
         long ans = 1;
         foreach( long l in incrementalNumbers )
            ans *= l;

      //Return the answer..
         return ans;

      }




      /// <summary>
      /// A method that evaluates the expression with a different operation order. If the expression contains parantheses, it will split the expression and call itself.. 
      /// </summary>
      /// <param name="exp"></param>
      /// <returns></returns>
      public static long EvaluateExpressionPart1( string exp )
      {

      //Declare the current expression value
         long currentValue = 0;
         OPERATION lastOperation = OPERATION.PLUS;
         int currentIdx = 0;

      //Loop through expression..
         while( true )
         {
         //Check for the current expression length..
            if( currentIdx > exp.Length-1 )
               break;

         //Declare current character for simplicity.
            char thisChar = exp[currentIdx];

         //CHeck if the current symbol is a space, if so, increment the current index
            if( exp[currentIdx] == ' ' )
            {
               currentIdx++;
               continue;
            }

         //Check if the current index is an operator
            OPERATION? operIfFound;
            bool foundOperator = CharIsOperator( thisChar, out operIfFound );
            if( foundOperator )
            {
               lastOperation = ( OPERATION ) operIfFound;
               currentIdx++;
               continue;
            }

         //Check if the current index is a number. If so, find the end of the number and parse it. Evaluate with the last operator, set next index and continue
            long dummy;
            bool thisParse = long.TryParse( thisChar.ToString( ), out dummy );
            if( thisParse )
            {
            //The parse was successful. Find the other end of the number..
               bool endOfNumber = false;
               int startIdx = currentIdx;
               int endIdx = -1;
               while( !endOfNumber )
               {
                  currentIdx++;
                  if( currentIdx < exp.Length )
                  {
                     thisChar = exp[currentIdx];
                     thisParse = long.TryParse( thisChar.ToString( ), out dummy );
                  }
                  else
                     thisParse = false;

                  if( !thisParse )
                  {
                     endIdx = currentIdx - 1;
                     endOfNumber = true;
                  }
               }

            //Extract substring and parse..
               int length = endIdx - startIdx + 1;
               string thisNumberString = exp.Substring( startIdx, length );
               long thisNum = long.Parse( thisNumberString );

            //Evaluate expression..
               currentValue = EvaluateNumbers( currentValue, thisNum, lastOperation );

            //The index was incremented in the while loop. Just skip to next..
               continue;
            }

         //Check if the current index is a paranthesis, if so, extract the expression and call this method by recursion to get the value of the inner expression. Then evaluate it by the last operator..
            if( thisChar == '(' )
            {
               int endIdx;
               string inner = ExtractExpression( exp, currentIdx, out endIdx );
               long value = EvaluateExpressionPart1( inner );
               currentValue = EvaluateNumbers( currentValue, value, lastOperation );
               currentIdx = endIdx+1;
               continue;
            }

         }

      //Return the value so far..
         return currentValue;

      }


      /// <summary>
      /// Evaluates an expression..
      /// </summary>
      /// <param name="prevVal"></param>
      /// <param name="thisVal"></param>
      /// <param name="operation"></param>
      /// <returns></returns>
      public static long EvaluateNumbers( long prevVal, long thisVal, OPERATION operation )
      {
         if( operation == OPERATION.PLUS )
            return prevVal + thisVal;
         else if( operation == OPERATION.MULTIPLICATION )
            return prevVal * thisVal;
         else
            throw new Exception( );
      }



   #endregion


   }
}
