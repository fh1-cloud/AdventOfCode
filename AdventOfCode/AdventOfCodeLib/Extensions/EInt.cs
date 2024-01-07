﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Extensions
{
   public static class EInt
   {

      //Removes a specified row AND column from a multidimensional array.
      public static int[ , ] TrimArray( this int[ , ] originalArray, int rowToRemove, int columnToRemove )
      {
         int[ , ] result = new int[originalArray.GetLength( 0 ) - 1, originalArray.GetLength( 1 ) - 1];

         for( int i = 0, j = 0; i < originalArray.GetLength( 0 ); i++ )
         {
            if( i == rowToRemove )
               continue;

            for( int k = 0, u = 0; k < originalArray.GetLength( 1 ); k++ )
            {
               if( k == columnToRemove )
                  continue;

               result[j, u] = originalArray[i, k];
               u++;
            }
            j++;
         }

         return result;
      }



   }
}
