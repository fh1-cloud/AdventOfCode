using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using static AdventOfCode2023.Classes.MachinePartSorter;

namespace AdventOfCode2023.Classes
{
   internal class MachinePartSorter
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
      internal class MachinePartSorterRuleSingle
      {
         internal MachinePartSorterRuleSingle( string rule )
         {
            string[ ] splitRule = rule.Split( new char[ ] { '<', '>' } );
            if( splitRule.Length == 2 ) //If length of two, we know this is a limit rule..
            {
               string[ ] right = splitRule[1].Split( ':' );
               this.RuleSymbol = splitRule[0];
               this.Value = int.Parse( right[0] );
               this.DestinationIfTrue = right[1];
               this.ComparisonSymbol = rule[1];
            }
            else
            {
               this.DestinationIfTrue = rule;
               this.RuleSymbol = String.Empty;
               this.Value = -1;
               this.ComparisonSymbol = 'F';
            }
         }
         internal string RuleSymbol { get; set; } 
         internal char ComparisonSymbol { get; set; }
         internal int Value { get; set; }
         internal string DestinationIfTrue { get; set; }
      }


      internal class MachinePartSorterRuleSet
      {
         internal MachinePartSorterRuleSet( string pattern )
         {
            Name = pattern.Split( '{' )[0];
            this.Rules = new List<MachinePartSorterRuleSingle>( );
            List<string> rules = pattern.Split( '{' )[1].TrimEnd( '}' ).Split( ',' ).ToList( );
            foreach( string rule in rules )
               this.Rules.Add( new MachinePartSorterRuleSingle( rule ) );
         }
         internal string Name { get; set; }
         internal List<MachinePartSorterRuleSingle> Rules { get; set; }
      }

   #endregion

   /*MEMBERS*/
   #region
      internal Dictionary<string,MachinePartSorterRuleSet> m_RuleSet = null;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public MachinePartSorter( List<string[]> input )
      {
         m_RuleSet = new Dictionary<string, MachinePartSorterRuleSet>( );
         foreach( string w in input[0] )
         {
            MachinePartSorterRuleSet workFlow = new MachinePartSorterRuleSet( w );
            m_RuleSet.Add( workFlow.Name, workFlow );
         }
      }


      /// <summary>
      /// Adjusts the ranges and returns the number of unique combinatinos for this range if it is accepted
      /// </summary>
      /// <param name="position"></param>
      /// <param name="ranges"></param>
      /// <returns></returns>
      long AdjustRanges( string position, Dictionary<string, URange1D> ranges )
      {

      //If we reached a position thats A, we know this is accepted. Return the product of all the ranges. 
         if( position == "A" )
         {
            long thisAns = 1;
            foreach( KeyValuePair<string, URange1D> kvp in ranges )
               thisAns *= ( kvp.Value.GetMaxValue( ) - kvp.Value.GetMinValue( ) + 1 );
            return thisAns;
         }
         else if( position == "R" ) //We reject all these ranges. No product to return
         {
            return 0;
         }

      //Declare the result for this rule looop
         long result = 0;
         MachinePartSorterRuleSet workflow = m_RuleSet[position];
         foreach( MachinePartSorterRuleSingle rule in workflow.Rules )
         {

            long min = 0;
            long max = 0;
            if( !string.IsNullOrEmpty( rule.RuleSymbol ) ) //If this is empty, there are no ranges for this rule. i.e this is a rule just to pass pass on to a new position
            {
               URange1D tR = ranges[rule.RuleSymbol];
               min = tR.GetMinValue( );
               max = tR.GetMaxValue( );
            }

            if( rule.ComparisonSymbol == '<' )
            {
               if( max < rule.Value ) // Entire range fits. Recursively process the next workflow.
               {
                  result += AdjustRanges( rule.DestinationIfTrue, ranges );
                  return result;
               }
               else if( min < rule.Value ) // Range needs to be split.
               {
               // Recursively process the fitting range into the next workflow.
                  Dictionary<string, URange1D> newRanges = CreateRangeCopy( ranges );        //Create a copy of the ranges
                  newRanges[rule.RuleSymbol] = URange1D.CreateSingleRange( min, rule.Value - 1 );  //Split the new range at the edge
                  result += AdjustRanges( rule.DestinationIfTrue, newRanges );                    //Recursively call the children of this rule..

               //Use the leftovers to continue processing
                  ranges[rule.RuleSymbol] = URange1D.CreateSingleRange( rule.Value, max );
               }
            }
            else if( rule.ComparisonSymbol == '>' )
            {
               if( min > rule.Value ) // Entire range fits. Recursively process the next workflow.
               {
                  result += AdjustRanges( rule.DestinationIfTrue, ranges );
                  return result;
               }
               else if( max > rule.Value ) // Range needs to be split.
               {
               // Recursively process the fitting range into the next workflow.
                  Dictionary<string, URange1D> newRanges = CreateRangeCopy( ranges );        //Create a copy of the ranges
                  newRanges[rule.RuleSymbol] = URange1D.CreateSingleRange( rule.Value + 1, max );  //Split the new range at the edge
                  result += AdjustRanges( rule.DestinationIfTrue, newRanges );                    //Recursively call children
                  ranges[rule.RuleSymbol] = URange1D.CreateSingleRange( min, rule.Value );         // Use the remaining range for the next rule.
               }
            }
            else //the rule is non existent, thiss is a rule to pass on to next.
            {
               // No conditions - recursively process the next workflow.
               result += AdjustRanges( rule.DestinationIfTrue, ranges );
               break;
            }
         }
         return result;
      }
      
      /// <summary>
      /// Creates a dictionary with position range copy
      /// </summary>
      /// <param name="range"></param>
      /// <returns></returns>
      public static Dictionary<string,URange1D> CreateRangeCopy( Dictionary<string, URange1D> range )
      {
         Dictionary<string, URange1D> result = new Dictionary<string,URange1D>();
         foreach( var kvp in range )
            result.Add( kvp.Key, kvp.Value.DeepCopy( ) );
         return result;
      }

      public long P2( )
      {

      //Create all the ranges for each character
         Dictionary<string, URange1D> candidates = new Dictionary<string, URange1D>
         {
            ["x"] = URange1D.CreateSingleRange( 1, 4000 ),
            ["m"] = URange1D.CreateSingleRange( 1, 4000 ),
            ["a"] = URange1D.CreateSingleRange( 1, 4000 ),
            ["s"] = URange1D.CreateSingleRange( 1, 4000 )
         };

         long ans = AdjustRanges( "in", candidates );
         return ans;
      }


      #endregion

   }
}
