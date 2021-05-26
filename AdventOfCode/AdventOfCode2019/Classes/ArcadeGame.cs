using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{

   /// <summary>
   /// AOC 2019 D13
   /// </summary>
   public class ArcadeGame
   {
   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected char[,] m_Screen = null;
      protected long m_ScreenHeight = -1;
      protected long m_ScreenWidth = -1;

      protected static char[] m_ColorMap = new char[5]{ ' ', '=', '#', '_', 'O' };

      protected long m_CurrentScore = 0;

      protected long m_BallX = 0;
      protected long m_BallY = 0;

   #endregion

   /*CONSTRUCTORS*/
   #region

      public ArcadeGame( long height, long width )
      {
         m_ScreenHeight = height;
         m_ScreenWidth = width;

         m_Screen = new char[height, width];
         for( int i = 0; i< m_ScreenHeight; i++ )
         {
            for( int j = 0; j< m_ScreenWidth; j++ )
            {
               m_Screen[i,j] = m_ColorMap[0];
            }
         }
      }


   #endregion

   /*PROPERTIES*/
   #region

      public long Score
      {
         get { return m_CurrentScore; }
         set { m_CurrentScore = value; }
      }

   #endregion

   /*OPERATORS*/
   #region

      public char this[ long i, long j ]
      {
         get
         {
            if( i>m_ScreenHeight-1 || j>m_ScreenWidth )
               throw new IndexOutOfRangeException( );
            return m_Screen[i,j];
         }
         set
         {
            if( i>m_ScreenHeight-1 || j>m_ScreenWidth )
               throw new IndexOutOfRangeException( );

            if( value == m_ColorMap[4] )
            {
               m_BallX = i;
               m_BallY = j;
            }
            m_Screen[i,j] = value;
         }
      }


   #endregion

   /*METHODS*/
   #region

      public void RenderScreen( )
      {
         //Print current score..
         Console.WriteLine( "SCORE: " + m_CurrentScore );
         for( int i = 0; i<m_ScreenHeight; i++ )
         {
            StringBuilder row = new StringBuilder( );
            for( int j = 0; j<m_ScreenWidth; j++ )
               row.Append( m_Screen[i,j].ToString( ) );

            Console.WriteLine( row.ToString( ) );
         }
      }


      public long GetNumberOfBlocks( )
      {
         long nOfBlocks = 0;
         for( int i = 0; i<m_ScreenHeight; i++ )
            for( int j = 0; j<m_ScreenWidth; j++ )
               if( m_Screen[i,j] == m_ColorMap[2] )
                  nOfBlocks++;

         return nOfBlocks;
      }

   #endregion

   /*STATIC METHODS*/
   #region
      
      public static char GetSymbol( long num )
      {
         return m_ColorMap[num];
      }

   #endregion
   }
}
