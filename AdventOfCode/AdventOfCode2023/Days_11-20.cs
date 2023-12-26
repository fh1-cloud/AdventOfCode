using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2023.Classes;
using AdventOfCodeLib.Classes;

namespace AdventOfCode2023
{
   public partial class Days
   {

   /*STATIC METHODS*/
   #region

      public static void Dec20( )
      {

      //Parse the text file to a string..
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         Dictionary<string, PulseModuleBase> modules = new Dictionary<string, PulseModuleBase>( );
         foreach( string s in inp )
         {
            PulseModuleBase thisModule = PulseModuleBase.CreateModuleFromInput( s );
            modules.Add( thisModule.Name, thisModule );
         }

      //Create output module
         PulseModuleOutput outputModule = PulseModuleOutput.CreateOutputModule( );
         modules.Add( outputModule.Name, outputModule );
         PulseModuleBase.PopulateConjunctionModules( modules ); //Populate all the conjunctions..

      //Find the conjunction module that has rx as a child..
         PulseModuleConjunction rxSource = null;
         foreach( KeyValuePair<string, PulseModuleBase> kvp in modules )
         {
            foreach( string c in kvp.Value.DestinationModules )
            {
               if( c == "rx" )
               {
                  rxSource = kvp.Value as PulseModuleConjunction;
                  break;
               }
            }
            if( rxSource != null )
               break;
         }
      //Create a dict of all the sources for the penultimate conjunction
         List<PulseModuleBase> rxSourceInputs = modules.Values.Where( x => x.DestinationModules.Contains( rxSource.Name ) ).ToList( );
         Dictionary<string, (long count, bool done)> rxSourceInputCounts = new Dictionary<string, (long count, bool done)>( );
         foreach( PulseModuleBase rxSourceModule in rxSourceInputs )
            rxSourceInputCounts[rxSourceModule.Name] = (0, false);

         bool foundAllSources = false;
         while( !foundAllSources )
            foundAllSources = PulseModuleBase.PushButton( modules, rxSourceInputCounts );

      //We exited, the number of button presses is in the source dict
         long lcmVal = 1;
         foreach( KeyValuePair<string, (long, bool)> rx in rxSourceInputCounts )
            lcmVal = AdventOfCodeLib.UMath.LCM( lcmVal, rx.Value.Item1 );

         Console.WriteLine( lcmVal );
      }


      public static void Dec19( )
      {
      //Parse the text file to a string..
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         long ans = new MachinePartSorter( GlobalMethods.SplitStringArrayByEmptyLine( inp ) ).P2( );
         Console.WriteLine( ans );
      }


      public static void Dec18( )
      {
      //Parse the text file to a string..
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         long ans = new DigPlan( inp.ToList( ) ).GetTotalArea( );
         Console.WriteLine( ans );
      }


      public static void Dec17( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         int[ ,] grid = new int[ inp.Length, inp[0].Length];
         for( int i = 0; i<inp.Length; i++ )
            for( int j = 0; j<inp[0].Length; j++ )
               grid[i,j] = int.Parse( inp[i][j].ToString( ) );

         LavaPool p = new LavaPool( grid );
         p.P1( );
         p.P2( );
      }


      public static void Dec16( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         long maxPropagation = 0;
         for( int i = 0; i<inp[0].Length; i++ )
         {
            MirrorField f = new MirrorField( inp );
            f.PropagateRay( new MirrorFieldBeam( -1, i, MirrorFieldBeam.BEAMDIRECTION.DOWN, f.Height, f.Width ) );
            maxPropagation = Math.Max( maxPropagation, f.GetNumberOfEnergizedTiles( ) );
         }
         for( int i = 0; i<inp[0].Length; i++ )
         {
            MirrorField f = new MirrorField( inp );
            f.PropagateRay( new MirrorFieldBeam( f.Height, i, MirrorFieldBeam.BEAMDIRECTION.UP, f.Height, f.Width ) );
            maxPropagation = Math.Max( maxPropagation, f.GetNumberOfEnergizedTiles( ) );
         }
         for( int i = 0; i<inp.Length; i++ )
         {
            MirrorField f = new MirrorField( inp );
            f.PropagateRay( new MirrorFieldBeam( i, -1, MirrorFieldBeam.BEAMDIRECTION.RIGHT, f.Height, f.Width ) );
            maxPropagation = Math.Max( maxPropagation, f.GetNumberOfEnergizedTiles( ) );
         }
         for( int i = 0; i<inp.Length; i++ )
         {
            MirrorField f = new MirrorField( inp );
            f.PropagateRay( new MirrorFieldBeam( i, f.Width, MirrorFieldBeam.BEAMDIRECTION.LEFT, f.Height, f.Width ) );
            maxPropagation = Math.Max( maxPropagation, f.GetNumberOfEnergizedTiles( ) );
         }
         long ans = maxPropagation;
         Console.WriteLine( ans );

      }


      public static void Dec15( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" )[0].Split( new char[] { ',' } );
         long ans = new LensCollection( inp ).GetFocusingPower( );
         Console.WriteLine( ans );
      }


      public static void Dec14( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         long ans = new TiltBoard( inp ).FindState( 1000000000 );
         Console.WriteLine( ans );
      }


      public static void Dec13( )
      {
         List<string[]> blocks = GlobalMethods.SplitStringArrayByEmptyLine( GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" ) );
         long ans2 = 0;
         for( int i = 0; i < blocks.Count; i++ )
            ans2 += new MirrorBlock( blocks[i] ).FindSubstitueMirror( );
         Console.WriteLine( ans2 );
      }


      public static void Dec12( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         long ans1 = 0;
         long ans2 = 0;
         foreach( string[] line in inp.Select( x => x.Split( ' ' ) ) )
         {
            ans1 += new HotSpringGroupLine( line[0] + " " + line[1] ).GetNumberOfPossiblePermutations( );
            ans2 += new HotSpringGroupLine( string.Join( "?", Enumerable.Repeat( line[0], 5 ) ) + " " + string.Join( ",", Enumerable.Repeat( line[1], 5 ) ) ).GetNumberOfPossiblePermutations( );
         }
         Console.WriteLine( ans1 );
         Console.WriteLine( ans2 );
      }


      public static void Dec11( )
      {
         string[ ] inp = GlobalMethods.GetInputStringArray( @"..\\..\\..\\Inputs\\" + System.Reflection.MethodBase.GetCurrentMethod( ).Name + ".txt" );
         long totalExpansionWidth = 1000000;
         HashSet<Galaxy> allGalaxies = Galaxy.GetAllGalaxies( inp, totalExpansionWidth );
         int skipCount = 0;
         long dist = 0;
         foreach( Galaxy g in allGalaxies )
         {
            int internalNum = 0;
            foreach( Galaxy o in allGalaxies )
            {
               internalNum++;
               if( g == o || internalNum <= skipCount  )
                  continue;
               dist += Galaxy.GetManhattanDistance( g, o );
            }
            skipCount++;
         }
         long ans = dist;
         Console.WriteLine( ans );
      }

   #endregion

   }
}
