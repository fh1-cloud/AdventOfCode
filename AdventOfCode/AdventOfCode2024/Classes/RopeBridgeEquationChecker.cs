using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Classes
{
   public class RopeBridgeEquationChecker
   {

   /*LOCAL CLASSES*/
   #region

      /// <summary>
      /// Represents a single equation
      /// </summary>
      public class RopeBridgeEquation
      {
         //Constructor
         public RopeBridgeEquation( BigInteger total, List<BigInteger> factors )
         {
            this.Factors = new List<BigInteger>( );
            foreach( BigInteger i in factors )
               this.Factors.Add( i );
            this.Total = total;
         }
         //Properties
         public BigInteger Total { get; set; }
         public List<BigInteger> Factors { get; set; }


         /// <summary>
         /// Checks if the total sum of this equation is a possible outcome for this equation
         /// </summary>
         /// <param name="concatenate"></param>
         /// <returns></returns>
         public bool TotalIsPossibleOutcome( bool concatenate = true )
         {
         //Calculate all possible outcomes..
            HashSet<BigInteger> possibleOutcomes = new HashSet<BigInteger>( );

         //Add first entry of the factors..
            possibleOutcomes.Add( this.Factors[0] );
            for( int i = 1; i<this.Factors.Count; i++ )
            {
               HashSet<BigInteger> newSums = new HashSet<BigInteger>( );
               foreach( BigInteger outcome in possibleOutcomes )
               {
               //Do all three operations for every possible outcome..
                  newSums.Add( outcome * this.Factors[i] );
                  newSums.Add( outcome + this.Factors[i] );
                  if( concatenate )
                     newSums.Add( BigInteger.Parse( outcome + this.Factors[i].ToString( ) ) );
               }
               possibleOutcomes = newSums;
            }
            return possibleOutcomes.Contains( this.Total );
         }

      }
   #endregion

   /*MEMBERS*/
   #region
      List<RopeBridgeEquation> m_Equations = new List<RopeBridgeEquation>( );
   #endregion

   /*CONSTRUCTORS*/
   #region

      public RopeBridgeEquationChecker( string[] inp )
      {
      //Parse input..
         for( int i = 0; i< inp.Length; i++ )
         {
            string[] colSplit = inp[i].Split( new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries );
            BigInteger total = long.Parse( colSplit[0] );
            List<BigInteger> factors = new List<BigInteger>( );
            string[] facSplit = colSplit[1].Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries );
            for( int j = 0; j<facSplit.Length; j++ )
               factors.Add( int.Parse( facSplit[j] ) );
            m_Equations.Add( new RopeBridgeEquation( total, factors ) );
         }
      }

   #endregion

   /*METHODS*/
   #region

      public BigInteger P1( )
      {
      //Loop over all equations and check if its possible that it has the correct sum..
         BigInteger calibSum = 0;
         for( int i = 0; i<m_Equations.Count; i++ )
            if( m_Equations[i].TotalIsPossibleOutcome( false ) )
               calibSum += m_Equations[i].Total;

         return calibSum;
      }

      public BigInteger P2( )
      {
      //Loop over all equations and check if its possible that it has the correct sum..
         BigInteger calibSum = 0;
         for( int i = 0; i<m_Equations.Count; i++ )
            if( m_Equations[i].TotalIsPossibleOutcome( true ) )
               calibSum += m_Equations[i].Total;

         return calibSum;
      }

   #endregion


   }
}
