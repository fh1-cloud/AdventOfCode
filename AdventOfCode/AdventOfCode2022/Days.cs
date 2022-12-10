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



      }
      /// <summary>
      /// 
      /// </summary>
      public static void Dec14( )
      {



      }
      /// <summary>
      /// Dec13
      /// </summary>
      public static void Dec13( )
      {



      }
      /// <summary>
      /// Dec12
      /// </summary>
      public static void Dec12( )
      {



      }
      /// <summary>
      /// Dec11
      /// </summary>
      public static void Dec11( )
      {



      }

      /// <summary>
      /// Dec10
      /// </summary>
      public static void Dec10( )
      {
         //Read input and parse..
         string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Dec10.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp02.txt" );

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
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp02.txt" );

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
         //int canvasRows = 21;
         //int canvasCols = 26;
         foreach( RopeKnotMovementPair ins in instructions )
         {
            //Console.WriteLine( "== " + ins.Direction.ToString( ) + " " + ins.Repeats.ToString( ) + " ==" );
            for( int i = 0; i<ins.Repeats; i++ )
            {
               head.MoveKnot( ins.Direction );
               //head.PrintState( canvasRows, canvasCols, ( canvasRows / 2 ), ( canvasCols / 2 ) );
               //head.PrintState( canvasRows, canvasCols, 4, 0 );
               //head.PrintState( canvasRows, canvasCols, 15, 11 );
            }
         }

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
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );

      //Create forest..
         TreeWithPosition[ ,] forest = new TreeWithPosition[inp.Length,inp[0].ToString().Length];
         for( int i = 0; i< inp.Length; i++ )
         {
            for( int j = 0; j<forest.GetLength( 1 ); j++ )
               forest[i,j] = new TreeWithPosition( int.Parse( inp[i][j].ToString( ) ), i, j );
         }

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
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );

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
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );

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
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );

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
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );

         //Split by comma..
         long ans = 0;
         foreach( string s in inp )
         {
            string[] splt = s.Split( ',' );
            CleaningElf c1 = new CleaningElf( splt[0] );
            CleaningElf c2 = new CleaningElf( splt[1] );

            //if( CleaningElf.DoesEncapsulate( c1, c2 ) )
            //   ans++;
            if( CleaningElf.DoesOverlap( c1, c2 ) )
               ans++;
         }

      //Loop over the
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
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );

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
         //string[ ] inp = GlobalMethods.GetInputStringArray( "..\\..\\Inputs\\Temp01.txt" );

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

         //Find max elf..
         //List<long> calList = allElves.Select( x => x.TotalCalories ).ToList( );
         //Console.WriteLine( "Max calories of one elf: " + maxCal.ToString( ) );

         List<ElfWithCalories> sorted = allElves.OrderBy( x => x.TotalCalories ).ToList( );
         sorted.Reverse( );
         long totSum = sorted[0].TotalCalories + sorted[1].TotalCalories + sorted[2].TotalCalories;

         Console.WriteLine( "Max calories of top three elves: " + totSum.ToString( ) );


      }

   }
}
