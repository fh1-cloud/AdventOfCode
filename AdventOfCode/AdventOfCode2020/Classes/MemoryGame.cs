using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   
   /// <summary>
   /// A class that represents the memory game used in december 15 of AOC.
   /// </summary>
   public class MemoryGame
   {


   /*MEMBERS*/
   #region
      public Dictionary<long, long> m_Memory = new Dictionary<long, long>( ); //A dictionary of the previous spoken numbers, where the key is the number, and the value is the last turn it was last spoken
      public long m_LastNumberInInitializer;
   #endregion



   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructors
      /// </summary>
      /// <param name="startNumbers"></param>
      public MemoryGame( long[ ] startNumbers )
      {

      //Set the values in the dictionary
         for( int i = 0; i < startNumbers.Length-1; i++ )
         {
            if( m_Memory.ContainsKey( startNumbers[i] ) )
               throw new Exception( );
            else
               m_Memory.Add( startNumbers[i], i + 1 );
         }

      //Set the last number spoken to the last number in the list..
         m_LastNumberInInitializer = startNumbers[startNumbers.Length - 1];
      }



   #endregion

   /*PROPERTIES*/
   #region

      /// <summary>
      /// Gets the last number in the initializer list
      /// </summary>
      public long LastNumberInInitializer
      {
         get
         {
            return m_LastNumberInInitializer;
         }
      }

   #endregion

   /*METHODS*/
   #region
      
      /// <summary>
      /// Gets the next number in the sequence. Also, stores the previous number
      /// </summary>
      /// <param name="turnNumber"></param>
      /// <param name="lastNumberSpoken"></param>
      /// <returns></returns>
      public long GetNextNumberInGame( long turnNumber, long lastNumberSpoken )
      {
         long thisNumber;

      //The number contains the last number spoken.
         if( m_Memory.ContainsKey( lastNumberSpoken ) )
            thisNumber = turnNumber - 1 - m_Memory[lastNumberSpoken];
         else
            thisNumber = 0;

      //Add last number spoken to the memory, now that we can overwrite whats in the dictionary
         if( m_Memory.ContainsKey( lastNumberSpoken ) )
            m_Memory.Remove( lastNumberSpoken );

         m_Memory.Add( lastNumberSpoken, turnNumber - 1);

         return thisNumber;
      }

   #endregion



   }
}
