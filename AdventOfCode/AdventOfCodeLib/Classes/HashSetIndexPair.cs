using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Classes
{
   public class HashSetIndexPair
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected HashSet<IntegerPair> m_IntegerPairs = new HashSet<IntegerPair>( );
   #endregion

   /*CONSTRUCTORS*/
   #region
      public HashSetIndexPair( )
      {

      }
   #endregion

   /*PROPERTIES*/
   #region
      public HashSet<IntegerPair> Pairs { get { return m_IntegerPairs; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Adds the integer pair to the hash set..
      /// </summary>
      /// <param name="i"></param>
      /// <param name="j"></param>
      /// <exception cref="Exception"></exception>
      public void Add( int i, int j )
      {
         if( !Contains( i, j ) )
            m_IntegerPairs.Add( new IntegerPair( i, j ) );
         else
            throw new Exception( );
      }

      /// <summary>
      /// A method that checks if the integer pair is present in the hash set..
      /// </summary>
      /// <param name="i">The first integer..</param>
      /// <param name="j">The second integer..</param>
      /// <returns></returns>
      public bool Contains( int i, int j )
      {
      //Loop over all the integer pairs in the set..
         foreach( IntegerPair pair in m_IntegerPairs )
            if( pair.RowIdx == i && pair.ColIdx == j )
               return true;

      //If the code reached this point, it is not contained in the set..
         return false;
      }

      /// <summary>
      /// Removes an integer pair from the hash set if it exists, if it does not exist, it does nothing..
      /// </summary>
      /// <param name="i"></param>
      /// <param name="j"></param>
      public void Remove( int i, int j )
      {
      //Loop over all the integer pairs in the set..
         IntegerPair foundPair = null;
         foreach( IntegerPair pair in m_IntegerPairs )
         {
            if( pair.RowIdx == i && pair.ColIdx == j )
            {
               foundPair = pair;
               break;
            }
         }

      //Remove if found..
         if( foundPair != null )
            m_IntegerPairs.Remove( foundPair );
      }

      /// <summary>
      /// Clears the hash set of all the integer pairs..
      /// </summary>
      public void Clear( )
      {
         m_IntegerPairs.Clear( );
      }
   #endregion


   }
}
