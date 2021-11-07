using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{
   public class FuelReaction
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region


      protected string m_Name = null;
      protected long m_Count = 0;
      protected Dictionary<string,long> m_Components = new Dictionary<string,long>( ); //The name of the ingredient with the count needed.

   #endregion

   /*CONSTRUCTORS*/
   #region

      public FuelReaction( string inp )
      {
      //Get the main component
         string[] sp1 = inp.Split( '=' );
         string[] sp2 = sp1[1].Split( ' ' );
         long mainPartCount = long.Parse( sp2[1] );
         m_Count = mainPartCount;
         m_Name = sp2[2] ;

      //Get the various component
         string[] sp3 = sp1[0].Split( ' ' );

         int currIdx = 0;
         while( true )
         {
            long thisCount = long.Parse( sp3[currIdx] );
            string thisName = sp3[currIdx+1];
            if( thisName.Contains( ','.ToString( ) ) )
               thisName = thisName.Substring( 0, thisName.Length - 1 );

            m_Components.Add( thisName, thisCount );

            currIdx += 2;
            if( currIdx >= sp3.Length-1 )
               break;
         }
      }


   #endregion

   /*PROPERTIES*/
   #region

      public string Name => m_Name;
      public long Count => m_Count;

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      public Dictionary<string,long> GetComponents( )
      {
         return m_Components;
      }



      public void ConsumeReagent( Dictionary<string,long> allReagents )
      {
      //Tries to consume the current reaction. Create the children such that the method is can consume it.
         if( !allReagents.ContainsKey( m_Name ) )
            allReagents.Add( m_Name, 0 );

      //Runs this reaction and creates the neccessary reagents for the children below
         foreach( KeyValuePair<string,long> reag in m_Components )
         {


         }


      }




   #endregion

   /*STATIC METHODS*/
   #region
   #endregion


   }
}
