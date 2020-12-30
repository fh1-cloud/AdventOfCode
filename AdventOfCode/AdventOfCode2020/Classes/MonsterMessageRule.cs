using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class MonsterMessageRule
   {


   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected int m_ID;
      protected List<int[]> m_RuleSet;
      protected string m_MessageValue = null;

   #endregion

   /*CONSTRUCTORS*/
   #region


      /// <summary>
      /// Creates a new rule for the messages 
      /// </summary>
      /// <param name="inp"></param>
      public MonsterMessageRule( string inp )
      {
      //Initializes a typo
         m_RuleSet = new List<int[]>( );

      //Get the ID.
         string[] colSplit = inp.Split( new char[] { ':' } );
         int id;
         bool idParse = int.TryParse( colSplit[0], out id );
         if( !idParse )
            throw new Exception( );
         m_ID = id;

      //Split by pipe
         string[] ruleSplit = colSplit[1].Split( '|' );
         for( int i = 0; i<ruleSplit.Length;i++ )
         {
         //This is a regular rule..
            if( ruleSplit[0].IndexOf( '"' ) == -1 )
            {
               string[] refSplit = ruleSplit[i].Split( new char[] { ' ' } );

               List<int> newRuleSet = new List<int>( );
               for( int j = 0; j < refSplit.Length; j++ )
               {
                  if( refSplit[j] == "" )
                     continue;
                  newRuleSet.Add( int.Parse( refSplit[j] ) );
               }
               m_RuleSet.Add( newRuleSet.ToArray() );
            }
            else
            {
               m_MessageValue = colSplit[1][2].ToString( );
            }
         }

      }

      public MonsterMessageRule( )
      {


      }

   #endregion

   /*PROPERTIES*/
   #region

      public string Message
      {
         get
         {
            return m_MessageValue;
         }
         set
         {
            m_MessageValue = value;
         }
      }

      public List<int[]> RuleSet
      {
         get
         {
            return m_RuleSet;
         }
         set
         {
            m_RuleSet = value; ;
         }
      }

      public int ID
      {
         get
         {
            return m_ID;
         }
         set
         {
            m_ID = value;
         }
      }

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region




      /// <summary>
      /// Creates a reges from the rule and all its children rules
      /// </summary>
      /// <returns></returns>
      public string CreateRegexPattern( Dictionary<int, MonsterMessageRule> allRules )
      {
         if( m_MessageValue != null )
            return m_MessageValue;
         else
         {
            string msg = "";
            for( int i = 0; i < m_RuleSet.Count; i++ )
            {
               if( i == 0 )
                  msg += "(";
               
            //One individual rule set, consits of a lists of rules
               int[] rules = m_RuleSet[i];

               for( int j = 0; j < m_RuleSet[i].Length; j++ )
               {
               //Declare the current rule.
                  int nextRule = rules[j];
                  msg += allRules[nextRule].CreateRegexPattern( allRules );
               }

               if( i == m_RuleSet.Count - 1 )
                  msg += ")";
               else
                  msg += "|";

            }

            return msg;
         }
      }




      /// <summary>
      /// Gets all the possible strings for this rule.
      /// </summary>
      /// <param name="prefix"></param>
      /// <param name="allRules"></param>
      /// <param name="maxLength"></param>
      /// <returns></returns>
      public List<string> GetPossibleStringsForThisRule( string prefix, Dictionary<int, MonsterMessageRule> allRules, int maxLength = 100 )
      {
         List<string> possibleStrings = new List<string>( );

      //Return this string if possible.
         if( m_MessageValue != null )
         {
            string onlyString = prefix += m_MessageValue;
            possibleStrings.Add( onlyString );
         }
         else //This rule contains other rules. Loop through the other rules and add them..
         {
            //Loop over the rule sets here.
            for( int i = 0; i < m_RuleSet.Count; i++ )
            {
            //One individual rule set, consits of a lists of rules
               int[] rules = m_RuleSet[i];

               List<string> possibleStringsForThisRuleSet = null;
               for( int j = 0; j < m_RuleSet[i].Length; j++ )
               {

               //Declare the current rule.
                  int nextRule = rules[j];

                  if( j == 0 ) //The first rule for this rule set. The possible strings for this rule set needs to be initialized
                  {
                     List<string> preliminaryStrings = allRules[nextRule].GetPossibleStringsForThisRule( prefix, allRules, maxLength );
                     possibleStringsForThisRuleSet = new List<string>( );
                     possibleStringsForThisRuleSet.AddRange( preliminaryStrings );
                  }
                  else if( j > 0 )
                  {
                  //Need to take all the strings created for the first rule, and apply them the next rules
                     List<string> newList = new List<string>( );
                     foreach( string s in possibleStringsForThisRuleSet )
                        newList.AddRange( allRules[nextRule].GetPossibleStringsForThisRule( s, allRules, maxLength ) );

                     possibleStringsForThisRuleSet = newList;
                  }
               }
            //When reached this point, the rule set is completed. Add the possible strings to the list of returning strings
               foreach( string s in possibleStringsForThisRuleSet )
                  possibleStrings.Add( s );
            }

         }
         return possibleStrings;
      }




      #endregion

      /*STATIC METHODS*/
      #region


      #endregion

   }
}
