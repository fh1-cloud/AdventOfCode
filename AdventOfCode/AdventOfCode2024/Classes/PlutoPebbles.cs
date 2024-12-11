using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Classes
{
   public class PlutoPebbles
   {


      //Key insight here is that we only need to keep track of how many, not in the order they are in

      public Dictionary<long,long> m_Pebbles = new Dictionary<long, long>( ); //A dictionary that keeps track of every stone with a certain number.


      /// <summary>
      /// Create the dictionary of pebbles. Only keep track of the count of each with a specific number.
      /// </summary>
      /// <param name="inp"></param>
      public PlutoPebbles( string[] inp )
      {
      //Parse input and create the linked list.
         string[] spl = inp[0].Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
         for( int i = 0; i<spl.Length; i++ )
         {
            long val = long.Parse( spl[i] );
            if( !m_Pebbles.ContainsKey( val ) )
               m_Pebbles[ val ] = 0;

            m_Pebbles[ val ] = m_Pebbles[ val ] + 1;
         }

      }

      public BigInteger P1( )
      {
         Dictionary<long, long> stones = GetStonesAfterIteration( 75 );
         return stones.Values.Sum( );
      }

   
      private Dictionary<long,long> GetStonesAfterIteration( int iteration )
      {

         //Loop for each iteration
         for( int i = 0; i<iteration; i++ )
         {

            //A dictionary with all the modifications to the main pebbles dictionary. Created like this so we dont change the number of pebbles while we iterate over it..
            Dictionary<long,long> modifications = new Dictionary<long, long>{ { 1, 0 } };

            foreach( KeyValuePair<long, long> stone in m_Pebbles )
            {


               //If the number on the stone is zero, we change all those stones to value 1
               if( stone.Key == 0 )
                  AddStoneToModifiedList( 1, stone.Value, modifications );
               else if( stone.Key.ToString( ).Length % 2 == 0 )
               {

               //Split into two values..
                  string stoneString = stone.Key.ToString( );
                  long leftStone = long.Parse( stoneString[..( stoneString.Length / 2 )] );
                  long rightStone = long.Parse( stoneString[( stoneString.Length / 2 )..] );

               //Add to modified list for each value..
                  AddStoneToModifiedList( leftStone, stone.Value, modifications );
                  AddStoneToModifiedList( rightStone, stone.Value, modifications );
               }
               else //Add new key to dictionary
                  AddStoneToModifiedList( stone.Key*2024, stone.Value, modifications );

            //Remove the pebble from the dictionary since we already fixed it
               m_Pebbles.Remove( stone.Key );

            }

            foreach( KeyValuePair<long, long> modification in modifications )
               m_Pebbles[modification.Key] = modification.Value;


         }
         return m_Pebbles.Where( x => x.Value > 0 ).ToDictionary( x => x.Key, x => x.Value );

      }

      private static void AddStoneToModifiedList( long key, long value, Dictionary<long, long> modifications )
      {
      //Try to add it to the dictionary of modifications. If it existed, we add the value to the existing, if it doesnt exist, we add it
         if( !modifications.TryAdd( key, value ) )
            modifications[key] += value;
      }


   }
}
