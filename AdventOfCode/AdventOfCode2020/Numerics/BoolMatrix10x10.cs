using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Numerics
{


   
   /// <summary>
   /// A matrix used for the jigsaw puzzles in Day 10
   /// </summary>
   public class BoolMatrix10x10
   {


   /*MEMBERS*/
   #region
      protected bool[,] m_Values = new bool[10, 10];
   #endregion


   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Initializes a bool matrix from a string input
      /// </summary>
      /// <param name="inp"></param>
      public BoolMatrix10x10( string[] inp )
      {
      //Check length of array
         if( inp.Length != 10 )
            throw new Exception( );

      //Check length of each string
         foreach( string s in inp )
            if( s.Length != 10 )
               throw new Exception( );

      //Initialize the values..
         for( int i = 0; i < 10; i++ )
         {
            for( int j = 0; j < 10; j++ )
            {
               if( inp[i][j] == '.' )
                  m_Values[i, j] = false;
               else if( inp[i][j] == '#' )
                  m_Values[i, j] = true;
               else
                  throw new Exception( );
            }
         }


      }

   #endregion



   /*PROPERTIES*/
   #region
   #endregion


   /*OPERATORS*/
   #region

      public bool this[int i, int j]
      {
         get
         {
            return m_Values[i, j];
         }
         protected set
         {
            m_Values[i, j] = value;
         }
      }

   #endregion


   /*METHODS*/
   #region




   #endregion

   /*STATIC METHODS*/
   #region
   #endregion


   }
}
