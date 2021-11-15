using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
   public class FabricPatch
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected int m_ID = -1;
      protected int m_RowStart = -1;
      protected int m_ColStart = -1;
      protected int m_RowCount = -1;
      protected int m_ColCount = -1;

   #endregion

   /*CONSTRUCTORS*/
   #region


      /// <summary>
      /// Creates a fabric patch from the input string..
      /// </summary>
      /// <param name="input"></param>
      public FabricPatch( string input, int[,] wholePatch )
      {

      //Split the string by space..
         string[] splitStr = input.Split( new char[]{ ' ' } );
         
      //Parse the id. If it fails it will throw.
         string idString = splitStr[0].Substring( 1 );
         m_ID = int.Parse( idString );

      //Parse the start position string..
         string[] startPosString = splitStr[2].Split( new char[] { ',' } );
         m_ColStart = int.Parse( startPosString[0] );
         m_RowStart = int.Parse( startPosString[1].Substring( 0, startPosString[1].Length - 1 ) );

      //Parse the dimension string..
         string[] dimString = splitStr[3].Split( new char[]{ 'x' } );
         m_ColCount = int.Parse( dimString[0] );
         m_RowCount = int.Parse( dimString[1] );

      //Fill patch
         FillPatch( wholePatch );

      }

   #endregion

   /*PROPERTIES*/
   #region

      public int ID => m_ID;


   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Returns a bool indicating wether or not the patch occupies the patch
      /// </summary>
      /// <param name="rowIdx"></param>
      /// <param name="colIdx"></param>
      /// <returns></returns>
      protected void FillPatch( int[,] wholePatch )
      {
      //Loop through and check if it occupies
         for( int i = m_RowStart; i<m_RowStart + m_RowCount; i++ )
            for( int j = m_ColStart; j < m_ColStart + m_ColCount; j++ )
               wholePatch[i,j]++;
      }


      /// <summary>
      /// Returns a bool indicating if this fabric patch overlaps with another patch. It checks the locations in the array and returns true if it finds a number that is larger than one.
      /// </summary>
      /// <param name="wholePatch"></param>
      /// <returns></returns>
      public bool MyPatchOverlap( int[,] wholePatch )
      {
      //Loop over this patch
         for( int i = m_RowStart; i<m_RowStart + m_RowCount; i++ )
            for( int j = m_ColStart; j < m_ColStart + m_ColCount; j++ )
               if( wholePatch[i,j] >= 2 )
                  return true;
         
      //If reached this point, it doesnt overlap..
         return false;

      }


   #endregion

   /*STATIC METHODS*/
   #region
   #endregion


   }
}
