﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2022.Classes;
using System.Windows.Forms;
using AdventOfCodeLib.Extensions;
using System.Security.Cryptography.X509Certificates;
using AdventOfCodeLib.Numerics;
using DotNetty.Common.Utilities;

namespace AdventOfCode2022
{
   public class Days
   {
      /// <summary>
      /// 
      /// </summary>
      public static void Dec25( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec24( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec23( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec22( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec21( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec20( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec19( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec18( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec17( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec16( )
      {



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec15( )
      {
      //Read input and parse..
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec14.txt" );
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );

         //Create all the sensors..
         List<Sensor> sensors = new List<Sensor>( );
         foreach( string s in inp )
         {
            Sensor t = new Sensor( s );
            sensors.Add( t );
         }
         int minX = sensors.Select( x => x.MinX ).ToArray( ).Min( );
         int maxX = sensors.Select( x => x.MaxX ).ToArray( ).Max( );

      //int targetHeight = 2000000;
         int targetHeight = 10;

      //Create a list of all the intersection lines..
         bool[] isOccupied = new bool[maxX - minX];
         foreach( Sensor s in sensors )
            s.IntersectCoverageWithHeight( targetHeight, intersectionPoints );





      //Find the answer..
         long ans = 0;
         Console.WriteLine( "Ans: " + ans );
         Clipboard.SetDataObject( ans );


      }





      /// <summary>
      /// 
      /// </summary>
      public static void Dec14( )
      {
      //Read input and parse..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec14.txt" );

      //Create the map..
         SandLabyrinth lab = new SandLabyrinth( inp );
         long placedGrains = 0;
         while( true )
         {
            bool laidToRest = lab.AddSandgrain( );
            if( laidToRest )
               placedGrains++;
            else
               break;
         }
      //Print the labyrinth..
         lab.PrintGrid( );

      //Find the answer..
         Console.WriteLine( "Ans: " + placedGrains );
         Clipboard.SetDataObject( placedGrains );

      }

      /// <summary>
      /// Dec13
      /// </summary>
      public static void Dec13( )
      {
      //Read input and parse..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec13.txt" );

      //Part 1
         int packetPairIdx = 1;
         List<int> idxOfOkPackets = new List<int>( );
         List<Packet> packets = new List<Packet>( );
         for( int i = 0; i<inp.Length; i=i+3 )
         {
         //Create packets..
            Packet pack1 = new Packet( inp[i] );
            Packet pack2 = new Packet( inp[i+1] );

         //Add to list of packets for sorting in part two.
            packets.Add( pack1 );
            packets.Add( pack2 );

         //Check the packet if it is in the right order..
            Packet.PACKETCHECK check = Packet.PacketsAreInRightOrder( pack1, pack2 );

         //Add indices if it is in order..
            if( check == Packet.PACKETCHECK.OK )
               idxOfOkPackets.Add( packetPairIdx );

         //This cannot be inconclusive, they are probably equal. throw if so..
            if( check == Packet.PACKETCHECK.INCONCLUSIVE )
               throw new Exception( );

         //Increment the packet index..
            packetPairIdx++;
         }

      //Add the divider packets for part 2
         Packet divider1 = new Packet( "[[2]]" );
         Packet divider2 = new Packet( "[[6]]" );
         packets.Add( divider1 );
         packets.Add( divider2 );
           
      //Part1
         int ans1 = idxOfOkPackets.Sum( );

      //Part 2. Sort the packets.. We have already created the method that checks where it should be inserted. Loop over list and add to a sorted list..
         //Declare the sorted list..
         List<Packet> sortedPackets = new List<Packet>( );
         List<Packet> unSortedPackets = new List<Packet>( packets );
         sortedPackets.Add( packets[0] );
         unSortedPackets.Remove( packets[0] );

      //SOrt the packets..
         while( unSortedPackets.Count > 0 )
         {
         //Unpack the current packet..
            Packet packetToBeSorted = unSortedPackets.First( );
         //Loop over all the packets in the sorted list and check if it should be before or after..
            int newIdx = -1;
            foreach( Packet p in sortedPackets )
            {
               Packet.PACKETCHECK isInOrder = Packet.PacketsAreInRightOrder( p, packetToBeSorted );
               if( isInOrder == Packet.PACKETCHECK.OK )
               {
               //CHeck if this is the last packet in the sorted list, if so add it at the end..
                  if( sortedPackets.IndexOf( p ) == sortedPackets.Count - 1 )
                     newIdx = sortedPackets.Count;
                  else
                     continue;
               }
               else if( isInOrder == Packet.PACKETCHECK.NOTOK )
               {
                  newIdx = sortedPackets.IndexOf( p );
                  break;
               }
               else if( isInOrder == Packet.PACKETCHECK.INCONCLUSIVE )
                  throw new Exception( );
            }

         //Insert the packet to be sorted at correct index..
            sortedPackets.Insert( newIdx, packetToBeSorted );

         //Remove the packet from the list of unsorted packets..
            unSortedPackets.Remove( packetToBeSorted );
         }

      //Print the sorted list..
         foreach( Packet p in sortedPackets )
            Console.WriteLine( p.GetString( ) );
         Console.WriteLine( );

      //Find the index of the divider packets..
         int idxDiv1 = sortedPackets.IndexOf( divider1 ) + 1;
         int idxDiv2 = sortedPackets.IndexOf( divider2 ) + 1;
         long ans2 = idxDiv1 * idxDiv2;

      //Find the answer..
         Console.WriteLine( "Ans: " + ans2 );
         Clipboard.SetDataObject( ans2 );
      }

      /// <summary>
      /// Dec12
      /// </summary>
      public static void Dec12( )
      {
      //Read input and parse..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec12.txt" );

      //Translate to a char array..
         int nRows = inp.Length;
         int nCols = inp[0].Length;
         MapNode[,] terrain = new MapNode[nRows, nCols];
         HashSet<MapNode> unvisited = new HashSet<MapNode>( );
         List<MapNode> potentialStarters = new List<MapNode>( );
         int startRowIdx = -1;
         int startColIdx = -1;
         int endRowIdx = -1;
         int endColIdx = -1;
         for( int i = 0; i<terrain.GetLength( 0 ); i++ )
         {
            for( int j = 0; j<terrain.GetLength( 1 ); j++ )
            {
               MapNode thisNode = new MapNode( inp[i][j], i, j );
               terrain[i,j] = thisNode;
               unvisited.Add( thisNode );
               if( terrain[i,j].NodeValue == 'S' )
               {
                  startRowIdx = i;
                  startColIdx = j;
                  potentialStarters.Add( thisNode );
               }
               else if( terrain[i,j].NodeValue == 'E' )
               {
                  endRowIdx = i;
                  endColIdx = j;
               }
               else if( j==0 && thisNode.NodeValue == 'a' )
                  potentialStarters.Add( thisNode );
            }
         }
      //Set the starting distance from the start to 0;
         MapNode start = terrain[ startRowIdx, startColIdx];
         start.NodeValue = 'a';
         MapNode end = terrain[endRowIdx, endColIdx];
         end.NodeValue = 'z';

      //Declare the shortest path so far..
         long shortestPathSoFar = long.MaxValue;

      //Loop over all the potential starters..
         foreach( MapNode startNode in potentialStarters )
         {
         //Create a new copy of the terrain objects first.. this is to not cause errors later..
            MapNode[,] terrainDeepCopy = new MapNode[terrain.GetLength( 0 ), terrain.GetLength( 1 ) ];
            HashSet<MapNode> unvisitedCopy = new HashSet<MapNode>( );
            for( int i = 0; i < terrain.GetLength( 0 ); i++ )
            {
               for( int j = 0; j < terrain.GetLength( 1 ); j++ )
               {
                  MapNode thisNode = new MapNode( terrain[i,j] );
                  terrainDeepCopy[i,j] = thisNode;
                  unvisitedCopy.Add( thisNode );
               }
            }

         //Declare the current node..
            MapNode currentNode = terrainDeepCopy[startNode.RowIdx, startNode.ColIdx];
            currentNode.ShortestRoadTo = 0;

         //Start the loop..
            while( true )
            {
            //Consider all the unvisited neighbours and calculate the tentative distance through the current node..
            //Collect all the neighbours..
               List<MapNode> unvisitedNeighbours = MapNode.GetNeighbours( currentNode, terrainDeepCopy );

            //Calculate the tentative distance to each of these nodes..
               foreach( MapNode neighbour in unvisitedNeighbours )
               {
                  long distance = currentNode.ShortestRoadTo + 1;
                  if( neighbour.ShortestRoadTo > distance )
                     neighbour.ShortestRoadTo = distance;
               }

            //Mark the current node as visited..
               currentNode.Visited = true;

            //When done considering all the unvisited neighbours of the current node, mark the node as visited and remove it from the unvisited set
               unvisitedCopy.Remove( currentNode );

            //When the destination node is marked as visited, exit
               if( unvisitedCopy.Count == 0 )
                  break;

            //The next node to visit should be the neighbour with the currently smallest distance from the initial node..
               //Find the minimum unvisited path value..
               long minVal = unvisitedCopy.Select( x => x.ShortestRoadTo ).ToList( ).Min( );

               //Find this node..
               currentNode = unvisitedCopy.Where( x => x.ShortestRoadTo == minVal ).FirstOrDefault( );
            }

         //We have broken the while loop, witch means that we have the path weight for the final node here..
            MapNode endNode = terrainDeepCopy[endRowIdx, endColIdx];
            Console.WriteLine( "Length from node " + startNode.RowIdx + ", " + startNode.ColIdx + ": " + endNode.ShortestRoadTo );

         //Check for the shortest path so far..
            if( endNode.ShortestRoadTo < shortestPathSoFar )
               shortestPathSoFar = endNode.ShortestRoadTo;
         }

      //Find the answer..
         Console.WriteLine( "Ans: " + shortestPathSoFar );
         Clipboard.SetDataObject( shortestPathSoFar );
      }


      /// <summary>
      /// Dec11
      /// </summary>
      public static void Dec11( )
      {
      //Read input and parse..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec11.txt" );

      //Create all the monkeys from input..
         List<string[ ]> splitMonkeys = GlobalMethods.SplitStringArrayByEmptyLine( inp );
         Dictionary<int, Monkey> allMonkeys = new Dictionary<int, Monkey>( );
         foreach( string[] monkey in splitMonkeys )
         {
            Monkey thisMonkey = new Monkey( monkey );
            allMonkeys.Add( thisMonkey.ID, thisMonkey );
         }

      //Find the monkeymodder to reduce the value by. This is the divisor for all the monkeys multiplied together to make sure that the remainder remainds the same..
         long monkeyModder = 1;
         foreach( var kvp in allMonkeys )
         {
            monkeyModder *= kvp.Value.Divisor;
         }
         Monkey.MonkeyModder = monkeyModder;

      //Make the monkeys take turns..
         int numberOfTurns = 10000;
         for( int i = 1; i<=numberOfTurns; i++ )
         {
            foreach( KeyValuePair<int, Monkey> kvp in allMonkeys )
               kvp.Value.TakeTurn( allMonkeys, true );

            if( i == 1 || i == 20 || i%1000 == 0 )
            {
            //Print status..
               Monkey.PrintStatusPart2( i, allMonkeys );
            }
         }

      //Print the answers..
         //Get a list of number of inspections..
         List<long> inspections = allMonkeys.Select( x => x.Value ).ToList( ).Select( y => y.Inspections ).ToList( );
         inspections.Sort( );
         inspections.Reverse( );
         long monkeyBusiness = inspections[0] * inspections[1];

         Console.WriteLine( "Ans: " + monkeyBusiness );
         Clipboard.SetDataObject( monkeyBusiness );
      }


      /// <summary>
      /// Dec10
      /// </summary>
      public static void Dec10( )
      {
      //Read input and parse..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec10.txt" );

         //Loop over the input list and create the instructionrs..
         CathodeRayCPU cpu = new CathodeRayCPU( 1, true );
         for( int i = 0; i<inp.Length; i++ )
         {
            string[] split = inp[i].Split( new char[]{' ' }, StringSplitOptions.RemoveEmptyEntries );
            CathodeRayCPU.INSTRUCTION? ins = null;
            long? inp1Val = null;
            if( split[0] == "noop" )
               ins = CathodeRayCPU.INSTRUCTION.NOOP;
            else if( split[0] == "addx" )
            {
               ins = CathodeRayCPU.INSTRUCTION.ADDX;
               inp1Val = long.Parse( split[1] );
            }

         //Carry out the instruction on the cpu..
            cpu.CarryOutInstructionPart2( (CathodeRayCPU.INSTRUCTION) ins, true, inp1Val );
         }
      //Print the answer
         Console.Write( cpu.GetScreenContent( ) );
      }

      /// <summary>
      /// Dec09
      /// </summary>
      public static void Dec09( )
      {
      //Read input and parse..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec09.txt" );

         RopeKnot head  = new RopeKnot( null,  0, new UVector2D( 0.0, 0.0 ) );
         RopeKnot knot1 = new RopeKnot( head,  1, new UVector2D( 0.0, 0.0 ) );
         RopeKnot knot2 = new RopeKnot( knot1, 2, new UVector2D( 0.0, 0.0 ) );
         RopeKnot knot3 = new RopeKnot( knot2, 3, new UVector2D( 0.0, 0.0 ) );
         RopeKnot knot4 = new RopeKnot( knot3, 4, new UVector2D( 0.0, 0.0 ) );
         RopeKnot knot5 = new RopeKnot( knot4, 5, new UVector2D( 0.0, 0.0 ) );
         RopeKnot knot6 = new RopeKnot( knot5, 6, new UVector2D( 0.0, 0.0 ) );
         RopeKnot knot7 = new RopeKnot( knot6, 7, new UVector2D( 0.0, 0.0 ) );
         RopeKnot knot8 = new RopeKnot( knot7, 8, new UVector2D( 0.0, 0.0 ) );
         RopeKnot knot9 = new RopeKnot( knot8, 9, new UVector2D( 0.0, 0.0 ) );
         head.Child = knot1;
         knot1.Child = knot2;
         knot2.Child = knot3;
         knot3.Child = knot4;
         knot4.Child = knot5;
         knot5.Child = knot6;
         knot6.Child = knot7;
         knot7.Child = knot8;
         knot8.Child = knot9;

      //Loop over instructions and parse..
         List<RopeKnotMovementPair> instructions = new List<RopeKnotMovementPair>( );

         for( int i = 0; i < inp.Length; i++ )
         //for( int i = 0; i<3; i++ )
         {
            string[] split = inp[i].Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries );
            RopeKnot.MOVEDIRECTION? dir = null;
            if( split[0][0] == 'R' )
               dir = RopeKnot.MOVEDIRECTION.RIGHT;
            else if( split[0][0] == 'L' )
               dir = RopeKnot.MOVEDIRECTION.LEFT;
            else if( split[0][0] == 'U' )
               dir = RopeKnot.MOVEDIRECTION.UP;
            else if( split[0][0] == 'D' )
               dir = RopeKnot.MOVEDIRECTION.DOWN;
            else
               throw new Exception( );
            int repeats = int.Parse( split[1] );
            instructions.Add( new RopeKnotMovementPair( (RopeKnot.MOVEDIRECTION) dir, repeats ) );
         }
         
      //Carry out instructions for the rope knot movement pair..
         foreach( RopeKnotMovementPair ins in instructions )
            for( int i = 0; i<ins.Repeats; i++ )
               head.MoveKnot( ins.Direction );

      //Find the number of unique entries in the tail..
         int nOfUniquePositions = knot9.GetNumberOfUniquePositions( );

      //Print the answer
         Console.WriteLine( "Ans: " + nOfUniquePositions );
         Clipboard.SetDataObject( nOfUniquePositions );
      }

      /// <summary>
      /// Dec08
      /// </summary>
      public static void Dec08( )
      {
      //Read input and parse..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec08.txt" );

      //Create forest..
         TreeWithPosition[ ,] forest = new TreeWithPosition[inp.Length,inp[0].ToString().Length];
         for( int i = 0; i< inp.Length; i++ )
            for( int j = 0; j<forest.GetLength( 1 ); j++ )
               forest[i,j] = new TreeWithPosition( int.Parse( inp[i][j].ToString( ) ), i, j );

      //Loop over the visible trees and calculate visible trees..
         long visible = 0;
         for( int i = 0; i< inp.Length; i++ )
         {
            for( int j = 0; j<forest.GetLength( 1 ); j++ )
            {
               forest[i,j].SetIsVisible( forest );
               if( forest[i,j].IsVisible )
                  visible++;
            }
         }

      //Loop over and se the scenic score..
         long maxScenicScore = -1;
         TreeWithPosition maxTree = null;
         for( int i = 0; i< inp.Length; i++ )
         {
            for( int j = 0; j<forest.GetLength( 1 ); j++ )
            {
               forest[i,j].SetScenicScore( forest );
               if( forest[i,j].ScenicScore > maxScenicScore )
               {
                  maxScenicScore = forest[i,j].ScenicScore;
                  maxTree = forest[i,j];
               }
            }
         }
      //Print the answer
         Console.WriteLine( "Ans: " + maxScenicScore );
         Clipboard.SetDataObject( maxScenicScore );
      }

      /// <summary>
      /// De07c
      /// </summary>
      public static void Dec07( )
      {
      //Read input..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec07.txt" );

      //Loop over and create all the folders..
         DirectoryFolder rootFolder = new DirectoryFolder( "/", null );
         DirectoryFolder currentFolder = rootFolder;
         for( int i = 1; i<inp.Length; i++ )
         {
         //If the command is a list command, extract the listed strings and create the subdirectories and files..
            if( inp[i].Substring( 0, 4 ) == "$ ls" )
            {
            //Extract substring that does not contain a $
               List<string> listedParts = new List<string>( );
               int localIdx = i + 1;
               while( true )
               {
               //Exit this loop if the next line contains a command..
                  if( localIdx > inp.Length-1 || inp[localIdx][0] == '$' )
                     break;

               //If the code reached this point, this is not a command. Create the local file in the directory
                  listedParts.Add( inp[localIdx] );

               //Increment the local index..
                  localIdx++;
               }

            //Create the files and folder that was extracted from the command..
               foreach( string s in listedParts )
               {
                  if( s.Substring( 0, 3 ) == "dir" ) //The string is a listed directory..
                  {
                     string folderName = s.Substring( 4 );
                     if( !currentFolder.SubFolders.ContainsKey( folderName ) )
                        currentFolder.SubFolders.Add( folderName, new DirectoryFolder( folderName, currentFolder ) );
                  }
                  else //The string is a listed file, create the file in the current folder..
                  {
                     string[] splitter = s.Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries );
                     long fileSize = long.Parse( splitter[0] );
                     if( !currentFolder.Files.ContainsKey( splitter[1] ) )
                        currentFolder.Files.Add( splitter[1], new DirectoryFile( splitter[1], fileSize ) );
                  }
               }

            //Set the local index to one less because the i is incremented
               i = localIdx-1;
               continue;
            }
            else if( inp[i].Substring( 0, 4 ) == "$ cd" ) //Set the current directory as something else and increment one..
            {
               if( inp[i].Length >= 7 && inp[i].Substring( 5, 2 ) == ".." ) //Navigate up one level..
               {
                  currentFolder = currentFolder.Parent;
               }
               else //Set the current folder to a sub folder of this folder. Look for it and set to current folder..
               {
                  string folderName = inp[i].Substring( 5 );
                  currentFolder = currentFolder.GetFolder( folderName );
                  if( currentFolder == null )
                     throw new Exception( );
               }
            }
            else
            {
               throw new Exception( );
            }
         }

      //Loop over all the directories in the subdirectory. Find all the folders that are larger than a certain value..
         List<DirectoryFolder> sizeLimitFolder = new List<DirectoryFolder>();
         rootFolder.CollectLargeDirectories( sizeLimitFolder );
         long sizeSum = sizeLimitFolder.Select( x => x.GetSizeOfFolder( ) ).Sum( );

      //File system size:
         long totalSystemSize = 70000000;
         long usedSpace = rootFolder.GetSizeOfFolder( );
         long freeSpace = totalSystemSize - usedSpace;
         long targetSize = 30000000 - freeSpace;

         List<DirectoryFolder> foldersThatAreLargeEnough = new List<DirectoryFolder>( );
         rootFolder.CollectLargeDirectories( foldersThatAreLargeEnough, targetSize );

      //Order the folders by size..
         List<DirectoryFolder> orderedFolders = foldersThatAreLargeEnough.OrderBy( x => x.GetSizeOfFolder( ) ).ToList( );

      //Get the size of the folder that is just large enough..
         long targetFolderSize = orderedFolders[0].GetSizeOfFolder( );

      //Write answer..
         Console.WriteLine( "Ans: " + targetFolderSize );
         Clipboard.SetDataObject( targetFolderSize );


      }

      /// <summary>
      /// De06c
      /// </summary>
      public static void Dec06( )
      {
      //Read input..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec06.txt" );
         string message = inp[0];
         string current4 = message.Substring( 0, 14 );
         int firstIdx = -1;
         for( int i = 14; i<message.Length; i++ )
         {
            bool isUnique = current4.ConsistsOfUniqueCharacters( );
            if( isUnique )
            {
               firstIdx = i;
               break;
            }

         //Remove the first index of the string and add the last..
            current4 = current4.Remove( 0, 1 );
            current4 += message[i];
         }

      //Write answer..
         Console.WriteLine( "Ans: " + firstIdx );
         Clipboard.SetDataObject( firstIdx );
      }

      /// <summary>
      /// Dec05
      /// </summary>
      public static void Dec05( )
      {
      //Read input..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec05.txt" );

      //Find the IDX line..
         int idxLine = -1;
         for( int i = 0; i<inp.Length; i++ )
         {
            string[] parts= Array.ConvertAll( inp[i].Split(','), p => p.Trim());
            if( parts.Length == 1 && parts[0].Equals( "" ) )
            {
               idxLine = i-1;
               break;
            }
         }

      //Create all the stacks..
         string[] stackIdxSplit = Array.ConvertAll( inp[idxLine].Split(','), p => p.Trim())[0].Split( ' ' );
         List<int> numbers = new List<int>( );
         foreach( string s in stackIdxSplit )
         {
            bool parsed = int.TryParse( s, out int num );
            if( parsed )
               numbers.Add( num );
         }

         List<CrateStack> stackList = new List<CrateStack>( );
         foreach( int stack in numbers )
            stackList.Add( new CrateStack( stack ) );

         string firstLine = inp[0];
         for( int i = 0; i<firstLine.Length; i++ )
         {
         //Look downwards for the first uppercase letter. If nothing is found, skip to next column..
            List<char> cratesInStack = new List<char>( );
            bool foundOne = false;
            for( int j = 0; j<= idxLine; j++ )
            {
               if( inp[j][i].IsUpperCaseLetter( ) )
               {
                  cratesInStack.Add( inp[j][i] );
                  foundOne = true;
               }
            }

         //Continue if we found a crate stack..
            if( !foundOne )
               continue;

         //Find the number of the crate stack to add to from parsing the index line..
            CrateStack current = stackList[ int.Parse( inp[idxLine][i].ToString( ) ) - 1 ];

         //Reverse the order of the crates to stack in correct order..
            cratesInStack.Reverse( );

         //Add to stack..
            foreach( char c in cratesInStack )
               current.Stack.Push( c );
         }

      //Parse the instructions and carry out..
         int instructionStartIdx = idxLine + 2;
         List<CrateMoveInstruction> instructions = new List<CrateMoveInstruction>( );
         for( int i = instructionStartIdx; i<inp.Length; i++ )
         {
            CrateMoveInstruction ins = new CrateMoveInstruction( inp[i] );
            ins.CarryOutInstruction( stackList );
         }

      //Write the answer..
         StringBuilder sb = new StringBuilder( );
         foreach( CrateStack cs in stackList )
            sb.Append( cs.Stack.Peek( ).ToString( ) );

      //Write answer..
         Console.WriteLine( "Ans: " + sb.ToString( ) );
         Clipboard.SetText( sb.ToString( ) );

      }

      /// <summary>
      /// Dec04
      /// </summary>
      public static void Dec04( )
      {
      //Read input..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec04.txt" );

      //Split by comma..
         long ans = 0;
         foreach( string s in inp )
         {
            string[] splt = s.Split( ',' );
            CleaningElf c1 = new CleaningElf( splt[0] );
            CleaningElf c2 = new CleaningElf( splt[1] );
            if( CleaningElf.DoesOverlap( c1, c2 ) )
               ans++;
         }
         Console.WriteLine( "Ans: " + ans.ToString( ) );
         Clipboard.SetText( ans.ToString( ) );

      }

      /// <summary>
      /// Dec03
      /// </summary>
      public static void Dec03( )
      {
      //Parse input file..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec03.txt" );

      //Loop over items and create rucksacks..
         long ans = 0;
         List<Rucksack> sacks = new List<Rucksack>( );
         for( int i = 0; i<inp.Length; i++ )
            sacks.Add( new Rucksack( inp[i] ) );

      //Find badge items..
         for( int i = 0; i<sacks.Count; i = i+3 )
         {
            long badgeScore = sacks[i].FindBadgeValue( sacks[i+1], sacks[i+2] );
            ans += badgeScore;
         }

      //Compact solution
         //var score = File.ReadLines("input.txt")
         //    .Select(e => e.ToCharArray())
         //    .Chunk(3)
         //    .Select(e => e[0].Intersect(e[1]).Intersect(e[2]).First())
         //    .Select(e => char.IsUpper(e) ? e - 38 : e - 96)
         //    .Sum();
         //Console.WriteLine(score);

      //Loop over the
         Console.WriteLine( "Ans: " + ans.ToString( ) );
         Clipboard.SetText( ans.ToString( ) );
      }


      /// <summary>
      /// Dec2
      /// </summary>
      public static void Dec02( )
      {
      //Parse input file..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec02.txt" );

         long ans = 0;
         for( int i = 0; i<inp.Length; i++ )
         {
            string[] split = inp[i].Split( ' ' );
            RockPaperScissors thisGame = new RockPaperScissors( split[0][0], split[1][0] );
            ans += thisGame.Score;
         }
         Console.WriteLine( "Total score: " + ans.ToString( ) );
         Clipboard.SetDataObject( ans.ToString( ) );

      }

      /// <summary>
      /// Dec1
      /// </summary>
      public static void Dec01( )
      {
      //Parse input file..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec01.txt" );
         
      //Create elfs..
         ElfWithCalories currentElf = new ElfWithCalories( );
         List<ElfWithCalories> allElves = new List<ElfWithCalories>( );
         allElves.Add( currentElf );
         for( int i = 0; i < inp.GetLength( 0 ); i++ )
         {
         //Create new elf if reached end..
            if( inp[i].Length == 0 )
            {
               currentElf = new ElfWithCalories( );
               allElves.Add( currentElf );
               continue;
            }

         //Add the food item..
            currentElf.Food.Add( int.Parse( inp[i] ) );
         }
         List<ElfWithCalories> sorted = allElves.OrderBy( x => x.TotalCalories ).ToList( );
         sorted.Reverse( );
         long totSum = sorted[0].TotalCalories + sorted[1].TotalCalories + sorted[2].TotalCalories;
         Console.WriteLine( "Max calories of top three elves: " + totSum.ToString( ) );
      }

   }
}
