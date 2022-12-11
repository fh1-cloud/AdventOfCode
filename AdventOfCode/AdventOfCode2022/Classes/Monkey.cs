using AdventOfCodeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class Monkey
   {

   /*ENUMS*/
   #region

      /// <summary>
      /// Enum representing the operation operator.
      /// </summary>
      public enum MONKEYOPERATIONOPERATOR
      {
         ADD,
         MULT
      }

      /// <summary>
      /// The operation value for this monkey. This represents what thing to operate on
      /// </summary>
      public enum MONKEYOPERATIONVALUE
      {
         OLD,
         NEW,
         VALUE
      }

   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      public static long MonkeyModder = 1; //The modulo for the new worry level. This is to make sure that we dont reach an overflow. This is calculated by multiplying all the divisors of all the monkeys together to form a modulo that makes sure that the remainder is the same.

      protected int m_ID = -1;
      protected MONKEYOPERATIONOPERATOR m_OperationOperator;
      protected MONKEYOPERATIONVALUE m_OperationValue1;
      protected MONKEYOPERATIONVALUE m_OperationValue2;
      protected long m_OperationValueValue = 0;
      protected long m_DivisorTestValue = 0;
      protected List<long> m_Items = new List<long>( );
      protected int m_MonkeyIDIfTrue = -1;
      protected int m_MonkeyIDIfFalse = -1;
      protected long m_NumberOfTimesInspectedAnItem = 0;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Initializes a monkey from the string input block..
      /// </summary>
      /// <param name="monkeyBlock"></param>
      public Monkey( string[] monkeyBlock )
      {
      //Parse the ID..
         m_ID = int.Parse( monkeyBlock[0].Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries )[1][0].ToString( ) );
         
      //Starting items..
         string[] itemSplit = monkeyBlock[1].Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries );
         List<long> items = new List<long>( );
         for( int i = 2; i<itemSplit.Length; i++ )
         {
            string thisLine = itemSplit[i];
            int nLength = thisLine.Length;
            if( thisLine[thisLine.Length - 1] == ',' )
               nLength--;
            m_Items.Add( long.Parse( thisLine.Substring( 0, nLength ) ) );
         }

      //Operations..
         //Parse operatorvalue 1
         string[] operationSplit = monkeyBlock[2].Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries );
         if( operationSplit[3] == "old" )
            m_OperationValue1 = MONKEYOPERATIONVALUE.OLD;
         else if( operationSplit[3] == "new" )
            m_OperationValue1 = MONKEYOPERATIONVALUE.NEW;
         else 
         {
            bool valParse = long.TryParse( operationSplit[3], out long dummy );
            if( !valParse )
               throw new Exception( );
            else
            {
               m_OperationValueValue = dummy;
               m_OperationValue1 = MONKEYOPERATIONVALUE.VALUE;
            }
         }

         //Parse operator..
         if( operationSplit[4] == "+" )
            m_OperationOperator = MONKEYOPERATIONOPERATOR.ADD;
         else if( operationSplit[4] == "*" )
            m_OperationOperator = MONKEYOPERATIONOPERATOR.MULT;
         else
            throw new Exception( );

         //Parse operatorvalue 2
         if( operationSplit[5] == "old" )
            m_OperationValue2 = MONKEYOPERATIONVALUE.OLD;
         else if( operationSplit[5] == "new" )
            m_OperationValue2 = MONKEYOPERATIONVALUE.NEW;
         else 
         {
            bool valParse = long.TryParse( operationSplit[5], out long dummy );
            if( !valParse )
               throw new Exception( );
            else
            {
               m_OperationValueValue = dummy;
               m_OperationValue2 = MONKEYOPERATIONVALUE.VALUE;
            }
         }

      //Parse test
         m_DivisorTestValue = long.Parse( monkeyBlock[3].Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries )[3] );

      //Parse throwing partners..
         m_MonkeyIDIfTrue = int.Parse( monkeyBlock[4].Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries )[5] );
         m_MonkeyIDIfFalse = int.Parse( monkeyBlock[5].Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries )[5] );

      }
   #endregion

   /*PROPERTIES*/
   #region
      public int ID { get { return m_ID; } }
      public long Inspections { get { return m_NumberOfTimesInspectedAnItem; } }
      public long Divisor { get { return m_DivisorTestValue; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion


   /*METHODS*/
   #region

      /// <summary>
      /// Excecutes this monkeys turn..
      /// </summary>
      /// <param name="allMonkeys"></param>
      public void TakeTurn( Dictionary<int, Monkey> allMonkeys, bool part2 = true )
      {
      //Inspect items one after another..
         List<Tuple<int, Monkey>> itemsToThrowBeforeExiting = new List<Tuple<int, Monkey>>( );

      //Loop over all items..
         for( int i = 0 ; i<m_Items.Count; i++ )
         {
         //Get the new worry level..
            long newWorryLevel = 0;
            if( part2 )
               newWorryLevel = GetNewItemValPart2( m_Items[i] );
            else
               newWorryLevel = GetNewItemValPart1( m_Items[i] );

         //Increment the number of times this monkey inspected something..
            m_NumberOfTimesInspectedAnItem++;


         //Check if this item should be thrown..
            Monkey monkeyToThrowTo = null;
            if( TestItem( newWorryLevel, out long remainder ) )
            {
               monkeyToThrowTo = allMonkeys[m_MonkeyIDIfTrue];
            }
            else
            {
               monkeyToThrowTo = allMonkeys[m_MonkeyIDIfFalse];
            }

         //Set the new worry level to something that is reduced by the multiplier of the divisor of all the monkeys. This is to make sure that the remainder remains the same across two monkeys.
            m_Items[i] = newWorryLevel%MonkeyModder;

         //Add items to the throwing dictionary..
            itemsToThrowBeforeExiting.Add( new Tuple<int, Monkey>( i, monkeyToThrowTo ) );
         }

      //Loop over the throwing items and throw..
         foreach( Tuple<int, Monkey> p in itemsToThrowBeforeExiting )
            p.Item2.m_Items.Add( m_Items[p.Item1] );

      //Empty the items in hand for this monkey. It has thrown them all..
         m_Items.Clear( );
      }

      /// <summary>
      /// Tests what to do with this item..
      /// </summary>
      /// <param name="newWorryLevel"></param>
      /// <returns></returns>
      private bool TestItem( long newWorryLevel, out long remainder )
      {
         remainder = newWorryLevel % m_DivisorTestValue;
         if( remainder == 0 )
         {
            return true;
         }
         else
         {
            return false;
         }
      }


      /// <summary>
      /// A method that calculates the new worry level for this item and returns it..
      /// </summary>
      /// <param name="oldWorryLevel"></param>
      /// <returns></returns>
      private long GetNewItemValPart2( long oldWorryLevel )
      {
         long val1 = 0;
         long val2 = 0;
         long newVal = 0;

      //Value 1
         if( m_OperationValue1 == MONKEYOPERATIONVALUE.OLD )
            val1 = oldWorryLevel;
         else if( m_OperationValue1 == MONKEYOPERATIONVALUE.VALUE )
            val1 = m_OperationValueValue;
         else
            throw new Exception( );

      //Value 2
         if( m_OperationValue2 == MONKEYOPERATIONVALUE.OLD )
            val2 = oldWorryLevel;
         else if( m_OperationValue2 == MONKEYOPERATIONVALUE.VALUE )
            val2 = m_OperationValueValue;
         else
            throw new Exception( );

      //Calculate the new value..
         if( m_OperationOperator == MONKEYOPERATIONOPERATOR.ADD )
            newVal = val1 + val2;
         else if( m_OperationOperator == MONKEYOPERATIONOPERATOR.MULT )
            newVal = val1 * val2; 
         else
            throw new Exception( );

      //Return the new value..
         return newVal;
      }


      /// <summary>
      /// A method that calculates the new worry level for this item and returns it..
      /// </summary>
      /// <param name="oldWorryLevel"></param>
      /// <returns></returns>
      private long GetNewItemValPart1( long oldWorryLevel )
      {
         long val1 = 0;
         long val2 = 0;
         long newVal = 0;

      //Value 1
         if( m_OperationValue1 == MONKEYOPERATIONVALUE.OLD )
            val1 = oldWorryLevel;
         else if( m_OperationValue1 == MONKEYOPERATIONVALUE.VALUE )
            val1 = m_OperationValueValue;
         else
            throw new Exception( );

      //Value 2
         if( m_OperationValue2 == MONKEYOPERATIONVALUE.OLD )
            val2 = oldWorryLevel;
         else if( m_OperationValue2 == MONKEYOPERATIONVALUE.VALUE )
            val2 = m_OperationValueValue;
         else
            throw new Exception( );

      //Calculate the new value..
         if( m_OperationOperator == MONKEYOPERATIONOPERATOR.ADD )
            newVal = val1 + val2;
         else if( m_OperationOperator == MONKEYOPERATIONOPERATOR.MULT )
            newVal = val1 * val2; 
         else
            throw new Exception( );

      //The monkey gets bored with item, divide by three and round down to nearest integer..
         newVal = newVal / 3;

      //Return the new value..
         return newVal;
      }

   #endregion

   /*STATIC METHODS*/
   #region
      /// <summary>
      /// Prints the status of number of swaps for part 2
      /// </summary>
      /// <param name="roundNumber"></param>
      /// <param name="allMonkeys"></param>
      public static void PrintStatusPart2( int roundNumber, Dictionary<int, Monkey> allMonkeys )
      {
         Console.WriteLine( "== After round " + roundNumber + " ==" );
         foreach( KeyValuePair<int, Monkey> kvp in allMonkeys )
         {
         //Build string for this monkey..
            StringBuilder sb = new StringBuilder( );
            sb.Append( "Monkey " + kvp.Key.ToString( ) + " inspected items " + kvp.Value.m_NumberOfTimesInspectedAnItem.ToString( ) + " times. " );

         //Write line for this monkey..
            Console.WriteLine( sb.ToString( ) );
         }

      //Write line seperator..
         Console.WriteLine( "" );
      }

      /// <summary>
      /// Prints the status of what the monkeys are holding after a certain round..
      /// </summary>
      /// <param name="roundNumber"></param>
      /// <param name="allMonkeys"></param>
      public static void PrintStatusPart1( int roundNumber, Dictionary<int, Monkey> allMonkeys )
      {
         Console.WriteLine( "After round " + roundNumber + " the monkeys are holding:" );
         foreach( KeyValuePair<int, Monkey> kvp in allMonkeys )
         {
         //Build string for this monkey..
            StringBuilder sb = new StringBuilder( );
            sb.Append( "Monkey " + kvp.Key.ToString( ) + ": " );
            foreach( long i in kvp.Value.m_Items )
               sb.Append( i.ToString( ) + ", " );

         //Write line for this monkey..
            Console.WriteLine( sb.ToString( ) );
         }

      //Write line seperator..
         Console.WriteLine( "" );
      }
      
   #endregion

   }
}
