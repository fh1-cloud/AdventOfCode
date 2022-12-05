using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class CrateMoveInstruction
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected int m_MoveCount = -1;
      protected int m_MoveFromIdx = -1;
      protected int m_MoveToIdx = -1;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public CrateMoveInstruction( string ins )
      {
         string[] splitter = ins.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );

         m_MoveCount = int.Parse( splitter[1] );
         m_MoveFromIdx = int.Parse( splitter[3] ) - 1;
         m_MoveToIdx = int.Parse( splitter[5] ) - 1;
      }
   #endregion

   /*PROPERTIES*/
   #region
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
      /// CArries out the instruction on the crate stacks..
      /// </summary>
      /// <param name="stacks"></param>
      public void CarryOutInstruction( List<CrateStack> stacks )
      {
         List<char> cratesToMove = new List<char>( );
         for( int i = 0; i< m_MoveCount; i++ )
         {
            char movingCrate = stacks[m_MoveFromIdx].Stack.Pop( );
            cratesToMove.Add( movingCrate );
         }
         cratesToMove.Reverse( ); 
         foreach( char c in cratesToMove )
         {
            stacks[m_MoveToIdx].Stack.Push( c );
         }
      }
   #endregion


   }
}
