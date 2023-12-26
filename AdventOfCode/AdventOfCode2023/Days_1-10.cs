using AdventOfCode2023.Classes;
using AdventOfCodeLib;
using AdventOfCodeLib.Classes;
using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
   public partial class Days
   {

/*STATIC METHODS*/
#region
      public static void Dec10( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         PipePart[ ,] grid = new PipePart[inp.Length, inp[0].Length];
         IntegerPair startLoc = null;
         for( int i = 0; i<inp.Length; i++ )
         {
            for( int j = 0; j<inp[i].Length; j++ )
            {
               grid[i, j] = new PipePart( inp[i][j] );
               if( inp[i][j] == 'S' )
                  startLoc = new IntegerPair( i, j );
            }
         }

      //P1
         PipeTraverser traveler = new PipeTraverser( startLoc, grid );
         bool foundEnd = false;
         while( !foundEnd )
            foundEnd = traveler.Move( grid );
         long ans1 = traveler.CycleLength / 2;

      //P2
         traveler.ReplaceStartSymbol( grid );
         int isInside = 0;
         for( int i = 0; i<grid.GetLength( 0 ); i++ )
            for( int j = 0; j<grid.GetLength( 1 ); j++ )
               isInside += grid[i,j].SetInsideStatus( i, j, grid ) ? 1 : 0;

         Console.WriteLine( isInside );
      }

      public static void Dec09( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         long totalSum = 0;
         for( int i = 0; i < inp.Length; i++ )
            totalSum += new OasisNumberSeries( inp[i] ).AddValueToSeries( );
         Console.WriteLine( totalSum );
      }

      public static void Dec08( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         Regex childRx = new Regex( @"\(([^()]*)\)" );
         Dictionary<string,DesertNode> nodes = new Dictionary<string, DesertNode>( );

      //Create tree
         for( int i = 2; i < inp.Length; i++ )
            nodes.Add( inp[i].Split( new char[ ] { ' ' }, StringSplitOptions.RemoveEmptyEntries )[0], new DesertNode( inp[i].Split( new char[ ] { ' ' }, StringSplitOptions.RemoveEmptyEntries )[0] ) );
         for( int i = 2; i < inp.Length; i++ )
         {
            string par = inp[i].Split( new char[] { ' ' } , StringSplitOptions.RemoveEmptyEntries )[0];
            string[ ] children = childRx.Match( inp[i] ).Value.Substring( 1, childRx.Match( inp[i] ).Value.Length - 2 ).Split( new char[]{ ',' }, StringSplitOptions.RemoveEmptyEntries );
            nodes[par].LChild = nodes[children[0].Trim( )];
            nodes[par].RChild = nodes[children[1].Trim( )];
         }
      //Part 1
         long ans1 = DesertNode.FindCycle( inp[0], nodes["AAA"], nodes["ZZZ"] );

      //Part 2
         long ans2 = 1;
         foreach( KeyValuePair<string,DesertNode> kvp in nodes )
            if( kvp.Key[kvp.Key.Length-1] == 'A' )
               ans2 = UMath.LCM( ans2, DesertNode.FindCycle( inp[0], kvp.Value) );

         Console.WriteLine( ans2.ToString( ) );
      }

      public static void Dec07( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         List<CamelCardHand> hands = new List<CamelCardHand>( );
         for( int i = 0; i<inp.Length; i++ )
            hands.Add( new CamelCardHand( inp[i], false ) );

         long ans = CamelCardHand.GetTotalWinnings( hands );
         Console.WriteLine( ans );
      }


      public static void Dec06( )
      {
      //Parse the text file to a string..
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         string[ ] ts = inp[0].Split( new char[ ] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
         string[ ] ds = inp[1].Split( new char[ ] { ' ' }, StringSplitOptions.RemoveEmptyEntries );

      //Part 1
         long errorMargin = 1;
         StringBuilder sbTime = new StringBuilder( );
         StringBuilder sbDist = new StringBuilder( );
         for( int i = 1; i<ts.Length; i++ )
         {
            errorMargin *= new BoatRace( long.Parse( ts[i] ), long.Parse( ds[i] ) ).GetNumberOfWaysToWin( );
            sbTime.Append( ts[i] );
            sbDist.Append( ds[i] );
         }

      //Part 2
         long ans2 = new BoatRace( long.Parse( sbTime.ToString( ) ), long.Parse( sbDist.ToString( ) ) ).GetNumberOfWaysToWin( );
         Console.WriteLine( ans2 );
      }

      public static void Dec05( )
      {
      //Parse the text file to a string..
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );

      //Part1
         List<long> seedList = null;
         SeedMapper map = new SeedMapper( inp, out seedList );
         List<long> locationNumbers = new List<long>( );
         foreach( long seed in seedList )
         {
            long location = map.GetSeedLocationNumberP1( seed );
            locationNumbers.Add( location );
         }

      //Part 2
         URange1D completeRange = new URange1D( new List<URangeSingle1D>( ) );
         for( int i = 0; i<seedList.Count-1; i=i+2 )
         {
            URangeSingle1D range1 = new URangeSingle1D( seedList[i], seedList[i] + seedList[i+1] - 1 );
            List<URangeSingle1D> rangeList = new List<URangeSingle1D>( );
            rangeList.Add( range1 );
            URange1D thisSeedRange = new URange1D( rangeList );
            URange1D endRangesForThisSeed = map.GetRangeFromSeedRange( thisSeedRange );
            completeRange.Add( endRangesForThisSeed );
         }
         long ans = completeRange.GetMinValue( );
         Console.WriteLine( ans );
      }

      public static void Dec04( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         List<ScratchCard> cards = new List<ScratchCard>( );

      //Part 1
         long totalScore = 0;
         foreach( string line in inp )
         {
            ScratchCard card = new ScratchCard( line );
            cards.Add( card );
            totalScore += card.Score;
         }

      //Part 2
         foreach( ScratchCard c in cards )
            c.PlayGame( cards );
         long scratchCards = cards.Select( x => x.Instances ).ToList( ).Sum( );

         Console.WriteLine( scratchCards );
      }

      public static void Dec03( )
      {
      //Parse the text file to a string..
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         object[ ,] grid = new object[inp.Length,inp[0].Length];
         for( int i = 0; i<inp.Length; i++ )
         {
            for( int j = 0; j<inp[0].Length; j++ )
            {
               if( inp[i][j] != '.' )
                  grid[i,j] = inp[i][j];
               else
                  grid[i,j] = null;
            }
         }
         long ans1 = GridChecker.GetSerialNumberSum( grid, out long ans2 );
         Console.WriteLine( ans2 );
      }


      public static void Dec02( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         List<CubeGame> games = new List<CubeGame>( );
         foreach( string s in inp )
            games.Add( new CubeGame( s ) );

      //Part1
         long validIdSum = games.Where( p => p.IsValid( ) ).ToList( ).Select( x => x.Number ).ToList( ).Sum( );
         Console.WriteLine( "Part 1: " + validIdSum.ToString( ) );

      //Part2
         long powerSum1 = games.Where( p => p != null ).ToList( ).Select( x => x.GetPower( ) ).ToList( ).Sum( );
         Console.WriteLine( "Part 2: " + powerSum1.ToString( ) );
      }

      public static void Dec01( )
      {
      //Parse the text file to a string..
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         long ans = 0;
         foreach( string s in inp )
         {
            StringBuilder sb = new StringBuilder( );

         //First occurence..
            for( int i = 0; i<s.Length; i++ )
            {
               bool succ = int.TryParse( s[i].ToString( ), out int p );
            //Check if digit is number..
               if( succ )
               {
                  sb.Append( p.ToString( ) );
                  break;
               }
            //Check if this is a start of a numerlaterlal..
               bool succ2 = NumerLiteral.CheckIfNumerliteralStart( s, i, out p );
               if( succ2 )
               {
                  sb.Append( p.ToString( ) );
                  break;
               }
            }
         //Last occurence
            for( int i = s.Length-1; i>= 0; i-- )
            {
               bool succ = int.TryParse( s[i].ToString( ), out int p );
               if( succ )
               {
                  sb.Append( p.ToString( ) );
                  break;
               }

            //Check if this is a start of a numerlaterlal..
               bool succ2 = NumerLiteral.CheckIfNumerliteralEnd( s, i, out p );
               if( succ2 )
               {
                  sb.Append( p.ToString( ) );
                  break;
               }

            }
         //Parse num..
            int thisAns = int.Parse( sb.ToString( ) );
            Console.WriteLine( thisAns );
            ans += thisAns;
         }
         Console.WriteLine( ans );
      }

#endregion

   }




}
