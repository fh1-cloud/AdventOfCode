using AdventOfCodeLib.Extensions;
using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{

   /// <summary>
   /// Represents exactly one astroid in the field.
   /// </summary>
   public class Astroid
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected int m_RowIdx = -1;
      protected int m_ColIdx = -1;

      #endregion

      /*CONSTRUCTORS*/
      #region

      public Astroid( int rowIdx, int colIdx )
      {
         m_RowIdx = rowIdx;
         m_ColIdx = colIdx;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public int RowIdx => m_RowIdx;
      public int ColIdx => m_ColIdx;

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region


      /// <summary>
      /// Gets the number of entries in the angled dictionary. AKA how many different astroids this astroid can see.
      /// </summary>
      /// <param name="allAstroids"></param>
      /// <returns></returns>
      public long NumberOfAstroidsICanSee( List<Astroid> allAstroids )
      {
         return GetAngleDictionaryForAstroid( this, allAstroids ).Count;
      }


      public bool CanISeeAstroidInField( Astroid lookFor, List<Astroid> allAstroids )
      {

      //Get the angled dictionary for this astroid.
         Dictionary<double, List<Astroid>> angles = GetAngleDictionaryForAstroid( this, allAstroids );

      //Get the angle between this astroid and the one we're looking for.
         double angle = this.GetAngleBetweenAstroid( lookFor );

      //Get the list from the dictionary.. If it doesnt exist, something is wrong and it should throw here.
         List<Astroid> beam = angles[angle];

      //Find the closest astroid.
         Astroid closest = beam[0];
         double currLow = GetDistance( this, closest );

         for( int i = 1; i<beam.Count; i ++ )
         {
            double dist = GetDistance( this, beam[i] );
            if( dist < currLow )
            {
               closest = beam[i];
               currLow = dist;
            }
         }

      //Return if the closest is the one we are looking for.
         return closest == lookFor;
      }


      public UVector2D GetPosition( )
      {
         return new UVector2D( m_ColIdx, m_RowIdx );
      }


      /// <summary>
      /// Gets the angle between this astroid and the passed astroid. Measured from the positive X direction counter clockwise
      /// </summary>
      /// <param name="a2"></param>
      /// <returns></returns>
      public double GetAngleBetweenAstroid( Astroid a2 )
      {
         UVector2D zero1 = UVector2D.UnitX( );
         UVector2D v2 = ( a2.GetPosition( ) - GetPosition( ) ).Normalize( );

         double angle = Math.Acos( zero1*v2 );
         if( a2.GetPosition( ).Y > this.RowIdx )
            angle += Math.PI;

         if( angle >= Math.PI * 2.0 )
            angle = angle - Math.PI * 2.0;
         

         return angle.ToDeg( );
      }




   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Gets the angle dictionary for other astroids relative to this astroid. 
      /// </summary>
      /// <param name="main">The astroid where the angles are calculated from</param>
      /// <param name="allAstroids">All astroids in the field</param>
      /// <returns></returns>
      public static Dictionary<double,List<Astroid>> GetAngleDictionaryForAstroid( Astroid main, List<Astroid> allAstroids )
      {
         Dictionary<double, List<Astroid>> anglesForAllOthers = new Dictionary<double, List<Astroid>>();
         foreach( Astroid astroid in allAstroids )
         {
            if( main == astroid )
               continue;

         //Get the angle between the main astroid and other astroid
            double angle = main.GetAngleBetweenAstroid( astroid );
            angle = Math.Round( angle, 8 );
         //GEt the list. If it doesnt exist, this is the first one.
            List<Astroid> thisList;

         //Check the dictionary for angles that are very close.
            //double adjustedAngle = GetAstroidAngleCloseToThisAngle( angle, 0.06, anglesForAllOthers );
            //double angleToAdd;
            //if( adjustedAngle.IsNaN( ) )
            //   angleToAdd = angle;
            //else
            //   angleToAdd = adjustedAngle;

            double angleToAdd = angle;
            
               
            if( !anglesForAllOthers.ContainsKey( angleToAdd ) )
            {
               thisList = new List<Astroid>();
               anglesForAllOthers.Add( angleToAdd, thisList );
            }
            else
               thisList = anglesForAllOthers[angleToAdd];

         //Add the other astroid to the list, and consequently the dictionary.
            thisList.Add( astroid );
         }

         return anglesForAllOthers;
      }


      public static double GetNextAngleInDictionary( SortedDictionary<double,List<Astroid>> sortedDict, double thisAngle )
      {
      //Gets the next angle in dictionary from this angle. The lazer rotates clockwise, so it needs to find the next one under.
         if( sortedDict.Count == 0 )
            return double.NaN;

      //Find the next key in the dictionary with a value below the current
         double minKey = thisAngle;


      //Collect the keyvaluepairs that have a key below the input angle. If there are none, it needs to be reset.
         List<KeyValuePair<double,List<Astroid>>> restLists = new List<KeyValuePair<double, List<Astroid>>>();
         foreach( var kvp in sortedDict )
            if( kvp.Key < thisAngle )
               restLists.Add( kvp );
         if( restLists.Count == 0 )
            thisAngle = 360.01;
            
         double closestAngle = thisAngle;
         foreach( KeyValuePair<double, List<Astroid>> kvp in sortedDict )
            if( kvp.Key < thisAngle )
               closestAngle = kvp.Key;

         return closestAngle;
      }





      /// <summary>
      /// Gets the distance between two astroids.
      /// </summary>
      /// <param name="a1"></param>
      /// <param name="a2"></param>
      /// <returns></returns>
      public static double GetDistance( Astroid a1, Astroid a2 )
      {
         UVector2D vec = new UVector2D( a1.ColIdx-a2.ColIdx, a1.RowIdx-a2.RowIdx );
         return vec.GetLength( );
      }



   #endregion


   }
}
