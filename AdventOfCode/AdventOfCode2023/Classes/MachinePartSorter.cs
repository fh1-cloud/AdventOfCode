using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   internal class MachinePartSorter
   {

   /*LOCAL CLASSES*/
   #region
      internal class MachinePartSorterRuleSingle
      {

         internal SORTEROPERATOR m_Operator;
         internal string m_SuccessLocation = null;
         internal MachinePartSorterRuleSingle( string inp )
         {


         }

         internal bool EmployRule( )
         {

            return false;

         }
      }

      internal class MachinePartSorterRuleSet
      {
         
         Dictionary<char,MachinePartSorterRuleSingle> m_Rules = new Dictionary<char,MachinePartSorterRuleSingle>();
         internal MachinePartSorterRuleSet( string set )
         {

         }

         internal bool EmployRuleSet( )
         {
            return false;

         }


      }

      internal class MachinePart
      {
         
         internal MachinePart( string inp )
         {

         //Split here


         }

         internal long X { get; set; }
         internal long M { get; set; }
         internal long A { get; set; }
         internal long S { get; set; }
         internal long GetRatingSum( ) { return this.X + this.M + this.A + this.S; }
      }


   #endregion

   /*ENUMS*/
   #region
      internal enum SORTEROPERATOR
      {
         LESSTHAN,
         GREATERTHAN
      }
   #endregion

   /*MEMBERS*/
   #region
      internal Dictionary<string,MachinePartSorterRuleSet> m_Rules = new Dictionary<string, MachinePartSorterRuleSet>( );
      internal List<MachinePart> m_AcceptedParts = new List<MachinePart>( );
      internal List<MachinePart> m_RejectedParts = new List<MachinePart>( );
   #endregion

   /*CONSTRUCTORS*/
   #region
      internal MachinePartSorter( string[] rules )
      {

      //Parse the line and split into rules

      }
   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      internal long P1( )
      {
         long ans1 = 0;
         foreach( MachinePart p in m_AcceptedParts )
            ans1 += p.GetRatingSum( );
         return ans1;
      }


      internal void Sort( MachinePart part )
      {



      }
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   }
}
