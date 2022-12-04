using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class CleaningElf
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected int m_StartInterval = -1;
      protected int m_EndInterval = -1;
   #endregion

   /*CONSTRUCTORS*/
   #region

      public CleaningElf( string intervalString )
      {
         string[] splt = intervalString.Split( '-' );
         m_StartInterval = int.Parse( splt[0] );
         m_EndInterval = int.Parse( splt[1] );
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


      public static bool DoesOverlap( CleaningElf c1, CleaningElf c2 )
      {
         if( c1.m_StartInterval <= c2.m_EndInterval && c1.m_EndInterval >= c2.m_StartInterval )
            return true;
         else if( c2.m_StartInterval <= c1.m_EndInterval && c2.m_EndInterval >= c1.m_StartInterval )
            return true;
         else
            return false;
      }

      public static bool DoesEncapsulate( CleaningElf c1, CleaningElf c2 )
      {
         if( c1.m_StartInterval <= c2.m_StartInterval && c1.m_EndInterval >= c2.m_EndInterval )
            return true;
         else if( c2.m_StartInterval <= c1.m_StartInterval && c2.m_EndInterval >= c1.m_EndInterval )
            return true;
         else 
            return false;
      }
   #endregion

   /*METHODS*/
   #region
   #endregion

   }
}
