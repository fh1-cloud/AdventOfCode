using AdventOfCodeLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Classes
{
   public class PolymerAtom
   {

   /*ENUMS*/
   #region

      /// <summary>
      /// Enum that represents the polarity of this atom. Upper or lower case
      /// </summary>
      public enum POLARITY
      {
         UPPER,
         LOWER,
      }

   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected char m_Value;          //The character value of this atom
      protected POLARITY m_Polarity;   //The polarity of this atom
      protected bool m_Alive = true;   //The status of this atom. If it is anihilated or not.

   #endregion

   /*CONSTRUCTORS*/
   #region
   
      /// <summary>
      /// Default constructor
      /// </summary>
      /// <param name="val">The character value for this polymer. Upper or lowercase decides the polarity</param>
      public PolymerAtom( char val )
      {
         m_Value = val;
         if( val.IsUpper( ) )
            m_Polarity = POLARITY.UPPER;
         else
            m_Polarity = POLARITY.LOWER;
      }

   #endregion

   /*PROPERTIES*/
   #region
      public PolymerAtom NextAtom { get; set; }    //The atom next to this atom. Does not make sense if the atom is dead.
      public char Symbol => m_Value;               //Get the char value of this atom
      public bool Status { get { return m_Alive; } set { m_Alive = value; } }    //Get or set the status of this polymer atom.
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Checks if these two anihilate each other.
      /// </summary>
      /// <param name="atom"></param>
      /// <returns></returns>
      public bool DoesAnihilateEachother( PolymerAtom atom )
      {

      //First check if polarity is opposite..
         if( atom.m_Polarity != m_Polarity )
            if( atom.m_Value.ToString( ).ToLower( ) == m_Value.ToString( ).ToLower( ) )
               return true;

      //If the code reached this point, they dont react.
         return false;
      }

   #endregion

   /*STATIC METHODS*/
   #region
   #endregion


   }
}
