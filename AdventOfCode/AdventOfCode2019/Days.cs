using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdventOfCode2019.Classes;
using AdventOfCodeLib;
using AdventOfCodeLib.Numerics;

namespace AdventOfCode2019
{
   public class Days
   {



   /*STATIC METHODS*/
   #region


      public static void Dec14( )
      {
         //string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Dec14.txt" );
         string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Temp01.txt" );

      //Create all the recipes..
         HashSet<FuelReaction> recipes = new HashSet<FuelReaction>();
         foreach( string s in inp )
            recipes.Add( new FuelReaction( s ) );

      //Find the recipe where FUEL is created..
         FuelReaction mainReaction = recipes.Where( x => x.Name == "C" ).FirstOrDefault( );

         Dictionary<string,long> availableReagents = new Dictionary<string, long>( );



         //long test = fuelRecipe.GetOreCountForRecipe( recipes, out long oreCount );

         //Console.WriteLine( oreCount );

      }




      public static void Dec13( )
      {
         string inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Dec13.txt" )[0];

      //Create the intcode computer..
         ArcadeGame game = new ArcadeGame( 22, 40 );
         IntcodeComputer ic = new IntcodeComputer( inp, true );
         bool singleScreen = false;
         while( !singleScreen )
         {
            ic.RunIntCode( true, false );
            long horIdx = ic.Output;
            ic.RunIntCode( true, false );
            long vertIdx = ic.Output; 
            ic.RunIntCode( true, false );
            long blockType = ic.Output;

            if( horIdx == -1 && vertIdx == 0 )
               game.Score = blockType;
            else
               game[vertIdx,horIdx] = ArcadeGame.GetSymbol( blockType );

            game.RenderScreen( );
            singleScreen = ic.ReachedEnd;
         }
      }

      public static void Dec12( )
      {
         string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Dec12.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Temp01.txt" );

      //Create moons..
         List<Moon> moons = new List<Moon>( );
         foreach( string s in inp )
            moons.Add( new Moon( s ) );

      //Create all the moon pairs for applying acceleration..
         List<KeyValuePair<Moon,Moon>> moonPairs = new List<KeyValuePair<Moon, Moon>>( );
         moonPairs.Add( new KeyValuePair<Moon, Moon>( moons[0], moons[1] ) );
         moonPairs.Add( new KeyValuePair<Moon, Moon>( moons[0], moons[2] ) );
         moonPairs.Add( new KeyValuePair<Moon, Moon>( moons[0], moons[3] ) );

         moonPairs.Add( new KeyValuePair<Moon, Moon>( moons[1], moons[2] ) );
         moonPairs.Add( new KeyValuePair<Moon, Moon>( moons[1], moons[3] ) );

         moonPairs.Add( new KeyValuePair<Moon, Moon>( moons[2], moons[3] ) );

      //Create simulating loop.

         long nOfSteps = 0;
         long maxSteps = 500000;

      ////Print first state.
      //   Console.WriteLine( );
      //   Console.WriteLine( "After " + nOfSteps + " steps: " );

      //   foreach( Moon m in moons )
      //      Console.WriteLine( m.PrintState( ) );

         double[] XZeroPos = new double[4];
         double[] YZeroPos = new double[4];
         double[] ZZeroPos = new double[4];
         for( int i = 0; i<4; i++ )
         {
            XZeroPos[i] = moons[i].XCoor;
            YZeroPos[i] = moons[i].YCoor;
            ZZeroPos[i] = moons[i].ZCoor;
         }


      //Simulate..
         List<long> cyclicX = new List<long>();
         List<long> cyclicY = new List<long>();
         List<long> cyclicZ = new List<long>();
         while( true )
         {
            
         //Apply gravity
            foreach( KeyValuePair<Moon, Moon> p in moonPairs )
               Moon.ApplyGravityOnPair( p );

         //Apply velocity
            foreach( Moon m in moons )
               m.ApplyVelocity( );

            nOfSteps++;

         ////Print state..
         //   Console.WriteLine( );
         //   Console.WriteLine( "After " + nOfSteps + " steps: " );
         //   foreach( Moon m in moons )
         //      Console.WriteLine( m.PrintState( ) );

            
         //Check X
            bool vZeroX = true;
            for( int i = 0; i<4; i++ )
            {
               if( !vZeroX )
                  break;
               vZeroX &= moons[i].XVel == 0;
            }
            if( vZeroX )
            {
               bool pZeroX = true;
               for( int i = 0; i<4; i++ )
                  pZeroX &= moons[i].XCoor == XZeroPos[i];
               if( pZeroX )
                  cyclicX.Add( nOfSteps );
            }

         //Check Y
            bool vZeroY = true;
            for( int i = 0; i<4; i++ )
            {
               if( !vZeroY )
                  break;
               vZeroY &= moons[i].YVel == 0;
            }
            if( vZeroY )
            {
               bool pZeroY = true;
               for( int i = 0; i<4; i++ )
                  pZeroY &= moons[i].YCoor == YZeroPos[i];
               if( pZeroY )
                  cyclicY.Add( nOfSteps );
            }
            
         //Check Z
            bool vZeroZ = true;
            for( int i = 0; i<4; i++ )
            {
               if( !vZeroZ )
                  break;
               vZeroZ &= moons[i].ZVel == 0;
            }
            if( vZeroZ )
            {
               bool pZeroZ = true;
               for( int i = 0; i<4; i++ )
                  pZeroZ &= moons[i].ZCoor == ZZeroPos[i];
               if( pZeroZ )
                  cyclicZ.Add( nOfSteps );
            }


            if( nOfSteps == maxSteps )
               break;
         }

      //Calculate total energy
         double totEnergy = 0.0;
         foreach( Moon m in moons )
            totEnergy += m.GetPotentialEnergy( ) * m.GetKineticEnergy( );

         Console.WriteLine( totEnergy );

      //Find the repeating state.

         //Each direction can be simulated independently. 
         //Store the state where all the velocities are zero in the x direction by simulating forward as candidates. Only check position if this is true.
         //Save the cycle length for the x direction
         //Repeat for Y and Z direction.
         //Find the LCM (Least common multiple) of the three numbers.

         long interM = UMath.LCM( cyclicY[0], cyclicX[0] );
         long ans = UMath.LCM( cyclicZ[0], interM );

         Console.WriteLine( ans );
      }



      public static void Dec11( )
      {
         string inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Dec11.txt" )[0];
         //string inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Temp01.txt" )[0];
         //string inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Temp02.txt" )[0];

      //Set the canvas size
         long canvasHeight = 50;
         long canvasWidth = 90;

      //Create an intcode computer and the hull painting robot
         IntcodeComputer ic = new IntcodeComputer( inp, true );
         HullPaintingRobot rob = new HullPaintingRobot( ( long ) ( canvasWidth*0.5 ), ( long ) ( canvasHeight*0.5 ) );

      //Declare the loop variable..
         bool didReachEnd = false;

      //Declare the canvas..
         HullPaintingRobot.COLOR[,] canvas = new HullPaintingRobot.COLOR[canvasWidth,canvasHeight];

      //Run the intcode computer with the input zero..
         ic.RunIntCode( new long[1]{ 1 } );

      //First output should be the color that this hex should be painted to.
         long colorToPaint = ic.Output;

      //Run the intcode computer with no input until next output. This output should be the turn signal.
         ic.RunIntCode( true, false );
         long turnSignal = ic.Output;

      //Get the new color of this position by asking the robot to paint it.
         rob.PaintPosition( colorToPaint, canvas );

      //Turn the robot and go forward.
         rob.Turn( turnSignal );
         HullPaintingRobot.COLOR newPositionColor = rob.GoForward( canvas );

      //Check if the intcode reached its end..
         didReachEnd = ic.ReachedEnd;

      //Declare the minimum and maximum x and y.
         long minX = rob.X;
         long maxX = rob.X;
         long minY = rob.Y;
         long maxY = rob.Y;

      //Update the minmax..
         rob.UpdateMinMaxPosition( minX, maxX, minY, maxY, out minX, out maxX, out minY, out maxY );

      //Do it all over again until the intcode reached point 99.
         while( !didReachEnd )
         {

            ic.RunIntCode( new long[1]{ (long) newPositionColor } );
            colorToPaint = ic.Output;
            ic.RunIntCode( true, false );
            turnSignal = ic.Output;

            rob.PaintPosition( colorToPaint, canvas );
            rob.Turn( turnSignal );
            newPositionColor = rob.GoForward( canvas );

         //Update the minmax..
            rob.UpdateMinMaxPosition( minX, maxX, minY, maxY, out minX, out maxX, out minY, out maxY );

            didReachEnd = ic.ReachedEnd;

         }

         long placesPainted = rob.PlacesPainted.Count;
         long xDim = maxX - minX;
         long yDim = maxY - minY;
         Console.WriteLine( placesPainted );

      //Create the canvas and paint.
         for( int rowIdx = (int) ( canvasHeight-1 ); rowIdx>= 0; rowIdx-- )
         {
            StringBuilder line = new StringBuilder( );
            for( int colIdx = 0; colIdx<canvasWidth; colIdx++ )
            {
               if( canvas[colIdx,rowIdx] == HullPaintingRobot.COLOR.BLACK )
                  line.Append( ' ' );
               else
                  line.Append( '@' );
            }
            Console.WriteLine( line.ToString( ) );
         }

      }


      public static void Dec10( )
      {
         string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Dec10.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Temp01.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Temp02.txt" );

         //List of all the astroids.
         List<Astroid> allAstroids = new List<Astroid>( );

      //Create all the astroids.
         for( int rowIdx = 0; rowIdx<inp.Length; rowIdx++ )
         {
            string thisLine = inp[rowIdx];
            for( int colIdx = 0; colIdx<inp[0].Length; colIdx++ )
            {
               if( thisLine[colIdx] == '#' )
                  allAstroids.Add( new Astroid( rowIdx, colIdx ) );
            }
         }

      //Get the dictionary..
         Astroid maxAstroid = null;
         long maxNum = 0;
         foreach( Astroid astroid in allAstroids )
         {
            long thisNum = astroid.NumberOfAstroidsICanSee( allAstroids );
            if( maxAstroid == null )
            {
               maxAstroid = astroid;
               maxNum = thisNum;
            }
            else
            {
               if( thisNum > maxNum )
               {
                  maxAstroid = astroid;
                  maxNum = thisNum;
               }
            }
         }

      //Write answer for part 1..
         Console.WriteLine( maxNum + " astroids seen from astroid (" + maxAstroid.ColIdx + "," + maxAstroid.RowIdx + ")" );

      //PART TWO

      //Get the angled dictionary from the max astroid.
         Dictionary<double, List<Astroid>> dict = Astroid.GetAngleDictionaryForAstroid( maxAstroid, allAstroids );

      //Sort the dictionary by key..
         SortedDictionary<double, List<Astroid>> sortedDict = new SortedDictionary<double, List<Astroid>>( dict );


         double prevValue = 90;

         int nOfRemovedAstroids = 0;
         Astroid n200th = null;
         while( sortedDict.Count > 0 )
         {
            List<Astroid> astroidList = sortedDict[prevValue];


         //Find the closest astroid in this list.
            if( astroidList.Count == 0 )
               throw new Exception( );

         //Find the closest astroid and remove.
            Astroid closest = null;
            if( astroidList.Count == 1 )
            {
               closest = astroidList[0];
               sortedDict.Remove( prevValue );
               nOfRemovedAstroids++;
            }
            else
            {
               closest = astroidList[0];
               double dist = Astroid.GetDistance( maxAstroid, closest );

            //Find the closest astroid.
               for( int i = 1; i < astroidList.Count; i++ )
               {
                  Astroid thisAstroid = astroidList[i];
                  double thisDist = Astroid.GetDistance( maxAstroid, thisAstroid );
                  if( thisDist < dist )
                  {
                     closest = thisAstroid;
                     dist = thisDist;
                  }
               }

            //Only remove the entry from the list.. and not from the dictionary
               astroidList.Remove( closest );
               nOfRemovedAstroids++;
            }

         //Print the vaporized astroid list..
            Console.WriteLine( "The " + nOfRemovedAstroids + " to remove is at ( " + closest.ColIdx + "," + closest.RowIdx + " )" );

         //Find angle..
            prevValue = Astroid.GetNextAngleInDictionary( sortedDict, prevValue );

         //Save the 200th astroid..
            if( nOfRemovedAstroids == 200 )
               n200th = closest;
         }

      //Print the 200th astroid
         long ans = n200th.ColIdx * 100 + n200th.RowIdx;
         Console.WriteLine( "Answer: " + ans );

      }


      public static void Dec09( )
      {
         string inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Dec09.txt" )[0];
         IntcodeComputer ic = new IntcodeComputer( inp );
         ic.RunIntCode( );
      }

      public static void Dec08( )
      {
         string inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Dec08.txt" )[0];

         //string inp = "123456789012";
         //Image img = new Image( 2, 3, inp );
         //Create image consisting of layers from the input string.
         Image img = new Image( 6, 25, inp );

      //Count the number of zero occurences in the layer..
         long layNum = -1;
         long minZeros = 999999;
         foreach( KeyValuePair<long, ImageLayer> lay in img.Layers )
         {
            int nOfZeros = lay.Value.CountNumberOfOccurencesOfDigitInLayer( 0 );
            if( nOfZeros < minZeros )
            {
               layNum = lay.Key;
               minZeros = nOfZeros;
            }
         }
         ImageLayer minZeroLayer = img.Layers[(int) layNum];
         long nOf1 = minZeroLayer.CountNumberOfOccurencesOfDigitInLayer( 1 );
         long nOf2 = minZeroLayer.CountNumberOfOccurencesOfDigitInLayer( 2 );
         long ans = nOf1*nOf2;

      //Print the number..
         //Console.WriteLine( "Part 1: " + ans );

         img.PrintImage( null );
      }


      public static void Dec07( )
      {

      //Parse input
         string inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Dec07.txt" )[0];


      //Get a list of phase setting arrays.
         List<long[]> phaseSettings = IntcodeComputer.GetPhaseSettings( false );

         IntcodeComputer ic = null;

         long highSignal = 0;
         long[] highPhaseSet = null;
         foreach( long[] loopPhase in phaseSettings )
         {

            long prevSignal = 0;
            for( int i = 0; i<loopPhase.Length; i ++ )
            {
               long[] thisIntcodeComputerInput = new long[2] { loopPhase[i], prevSignal };
               ic = new IntcodeComputer( inp, thisIntcodeComputerInput, true );
               ic.RunIntCode( );
               prevSignal = ic.Output;
            }
            if( ic.Output > highSignal )
            {
               highPhaseSet = loopPhase;
               highSignal = ic.Output;
            }
         }
         Console.WriteLine( "Part 1: " + highSignal );


      //Part two
         phaseSettings = IntcodeComputer.GetPhaseSettings( true );
         highSignal = 0;
         highPhaseSet = null;

         //string programme = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
         //string programme = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";

         foreach( long[] phase in phaseSettings )
         {
         //Create the thrusters with correct phase for first looop.
            List<IntcodeComputer> thrusters = new List<IntcodeComputer>( );

            long lastOutput = 0;
            IntcodeComputer currentThruster = null;
            for( int i = 0; i < 5; i++ )
            {
               currentThruster = new IntcodeComputer( inp, new long[2] { phase[i], lastOutput }, true );
               thrusters.Add( currentThruster );
               currentThruster.RunIntCode( true, true);
               lastOutput = currentThruster.Output;
            }

            currentThruster = thrusters[0];
            long finalOutput = -1;
            while( true )
            {

            //Run the thruster with new input..
               currentThruster.RunIntCode( new long[]{ lastOutput } );
               lastOutput = currentThruster.Output;

            //Check if the current thruster is E and if it reached state 99.
               if( thrusters.IndexOf( currentThruster ) == 4 && currentThruster.ReachedEnd )
               {
                  finalOutput = currentThruster.Output;
                  break;
               }

            //Find the next current thruster.
               if( thrusters.IndexOf( currentThruster ) == 4 )
                  currentThruster = thrusters[0];
               else
                  currentThruster = thrusters[thrusters.IndexOf( currentThruster ) + 1];

            }

         //When the code reached this point, it has produced a final output.
            if( finalOutput > highSignal )
            {
               highSignal = finalOutput;
               highPhaseSet = phase;
            }

         }
         Console.WriteLine( "Part 2: " + highSignal + " for phase combination {" + highPhaseSet[0] + "," + highPhaseSet[1] + "," + highPhaseSet[2] + "," + highPhaseSet[3] + "," + highPhaseSet[4] + "}" );

      }


      public static void Dec06( )
      {
      //Parse input
         string[] inp = GlobalMethods.GetInputStringArray( @"..\\..\\Inputs\\Dec06.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Temp01.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Temp02.txt" );

      //Declare a dictionary with the name of the object as the key and the object itself as the value
         Dictionary<string,OrbitObject> objectsInOrbit = new Dictionary<string,OrbitObject>( );
         foreach( string s in inp )
         {
            string[] spl = s.Split( ')' );
            string name1 = spl[0];
            string name2 = spl[1];

         //Create the parent if it doesnt exist.
            if( !objectsInOrbit.ContainsKey( name1 ) )
            {
               OrbitObject o = new OrbitObject( name1 );
               objectsInOrbit.Add( name1, o );
            }

         //Create the child if it doesnt exist. If it exists, add the parent.
            if( !objectsInOrbit.ContainsKey( name2 ) )
            {
               OrbitObject o = new OrbitObject( name2 );
               o.Parent = objectsInOrbit[name1];
               objectsInOrbit.Add( name2, o );
            }
            else
               objectsInOrbit[name2].Parent = objectsInOrbit[name1];

         }

      //Loop over all the objects created and could the orbits..
         long nOfOrbits = 0;
         foreach( KeyValuePair<string, OrbitObject> kvp in objectsInOrbit )
            nOfOrbits += kvp.Value.CountOrbitsAbove( );

      //Write output..
         Console.WriteLine( nOfOrbits );


      //Calculate the distance between YOU and SAN
         List<OrbitObject> aboveYou = new List<OrbitObject>( );
         List<OrbitObject> aboveSan = new List<OrbitObject>( );

         OrbitObject you = objectsInOrbit["YOU"];
         OrbitObject san = objectsInOrbit["SAN"];

         you.GetChainAbove( ref aboveYou );
         san.GetChainAbove( ref aboveSan );


      //Find the first common orbitable object between the two.
         OrbitObject leastCommon = null;
         bool foundIt = false;
         for( int i = 1; i < aboveYou.Count; i++ )
         {
            var thisYou = aboveYou[i];
            for( int j = 1; j < aboveSan.Count; j++ )
            {
               var thisSan = aboveSan[j];
               if( thisYou.Name == thisSan.Name )
               {
                  leastCommon = thisYou;
                  foundIt = true;
                  break;
               }
            }
            if( foundIt )
               break;
         }

         long nAboveYouToLeastCommon = you.CountOrbitsAbove( leastCommon ) - 2;
         long nAboveSanToLeastCommon = san.CountOrbitsAbove( leastCommon ) - 2;

         long ans = nAboveSanToLeastCommon + nAboveYouToLeastCommon;
         Console.WriteLine( ans );

      }




      public static void Dec05( )
      {
         string inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec05.txt" )[0];
         //string inp = "3,0,4,0,99";
         //string inp = "1002,4,3,4,33";
         //string inp = "1101,100,-1,4,0"
         //string inp = "3,9,8,9,10,9,4,9,99,-1,8";
         //string inp = "3,3,1107,-1,8,3,4,3,99";
         //string inp = "3,3,1108,-1,8,3,4,3,99";
         //string inp = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9";
         //string inp = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1";
         //string inp = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31, 1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104, 999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";

         IntcodeComputer ic = new IntcodeComputer( inp );
         ic.RunIntCode( );
      }




      public static void Dec04( )
      {
         string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec04.txt" );
         int minValue = int.Parse( inp[0].Split( '-' )[0] );
         int maxValue = int.Parse( inp[0].Split( '-' )[1] );


         List<int> validPassWords = new List<int>( );
         for( int i = minValue+1; i < maxValue; i++ )
            if( PasswordValidater.Validate( i ) )
               validPassWords.Add( i );

         Console.WriteLine( validPassWords.Count );
      }

   
      public static void Dec03()
      {
         string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec03.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Temp02.txt" );

      //Create the two wires.
         Wire a = new Wire( inp[0] );
         Wire b = new Wire( inp[1] );

      //Try to calculate the intersection points..
         List<UVector2D> intPoints = a.GetIntersectionPoints( b, out List<double> signalDelays );

      //Find the one with the minimum manhattan distance.
         double minManhattan = 1.0e12;
         UVector2D minPoint = null;
         foreach( var v in intPoints )
         {
            if( v.GetManhattanLength( ) < minManhattan )
            {
               minPoint = v;
               minManhattan = v.GetManhattanLength( );
            }
         }

      //Print the minimum value.
         Console.WriteLine( minManhattan );

      //PART 2
         double minInterference = signalDelays.Min( );

      //Print the minimum interference
         Console.WriteLine( minInterference );

      }



      public static void Dec02()
      {
         string inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec02.txt" )[0];
         //long[] inp = GlobalMethods.GetInputIntegerArray( @"Inputs\\Temp01.txt" );
         //string inp = "1,0,0,0,99";

         //PART1
         IntcodeComputer c = new IntcodeComputer( inp );
         c[1] = 12;
         c[2] = 2;
         c.RunIntCode( );

         long ans1 = c[0];
         Console.WriteLine( "Part 1: " + ans1 );

      //PART2
         long noun = -1;
         long verb = -1;
         long target = 19690720;
         for( int i = 0; i < 100; i++ )
         {
            for( int j = 0; j < 100; j++ )
            {
            //Create a new intcode computer
               IntcodeComputer thisComputer = new IntcodeComputer( inp );
               thisComputer[1] = i;
               thisComputer[2] = j;

            //Run the intcode computer
               thisComputer.RunIntCode( );

            //Chech the output in position zero. If it matches, break the loop
               if( thisComputer[0] == target )
               {
                  noun = i;
                  verb = j;
                  break;
               }
            }
            if( noun >= 0 )
               break;
         }
         long ans2 = 100 * noun + verb;
         Console.WriteLine( "Part 2: " + ans2 );
         Clipboard.SetText( ans2.ToString( ) );
      }



      public static void Dec01()
      {
         long[] inp = GlobalMethods.GetInputIntegerArray( @"Inputs\\Dec01.txt" );
         //long[] inp = GlobalMethods.GetInputIntegerArray( @"Inputs\\Temp01.txt" );

      //PART1
         long massSoFar = 0;
         foreach( long l in inp )
            massSoFar += l / 3 - 2;
         Console.WriteLine( "Part 1: " + massSoFar );

      //PART2
         long fuelSoFar = 0;
         foreach( long l in inp )
            fuelSoFar += FuelCalculator.GetFuel( l );

         Console.WriteLine( "Part 2: " + fuelSoFar );
         Clipboard.SetText( fuelSoFar.ToString( ) );
      }



   #endregion


   }
}
