using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Classes
{
   public class HashSetUVector2D
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected HashSet<UVector2D> m_UnderlyingSet = new HashSet<UVector2D>( );
   #endregion

   /*CONSTRUCTORS*/
   #region

      public HashSetUVector2D( )
      {

      }

   #endregion

   /*PROPERTIES*/
   #region
      public HashSet<UVector2D> UnderlyingSet { get{ return m_UnderlyingSet; } }
      public int Count { get { return m_UnderlyingSet.Count; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   /*METHODS*/
   #region
      public void Add( UVector2D val )
      {
      //Check if it contains it already, if so throw
         List<UVector2D> xMatchers = m_UnderlyingSet.Where( x => x.X == val.X ).ToList( );
         List<UVector2D> yMatchers = xMatchers.Where( y => y.Y == val.Y ).ToList( );
         if( yMatchers.Count == 0 )
            m_UnderlyingSet.Add( val );
         else
            throw new Exception( ); //DUPLICATE VECTOR ALREADY PRESENT
      }

      public bool Contains( UVector2D val )
      {
      //Check if it contains it already, if so throw
         List<UVector2D> xMatchers = m_UnderlyingSet.Where( x => x.X == val.X ).ToList( );
         List<UVector2D> yMatchers = xMatchers.Where( y => y.Y == val.Y ).ToList( );
         if( yMatchers.Count == 1 )
            return true;
         else if( yMatchers.Count == 0 )
            return false;
         else
            throw new Exception( ); //Duplicate inside hashset?
      }

   #endregion



   }
}
