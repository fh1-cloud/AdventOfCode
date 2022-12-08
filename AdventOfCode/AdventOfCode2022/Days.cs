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



      }
      /// <summary>
      /// Dec09
      /// </summary>
      public static void Dec09( )
      {



      }
      /// <summary>
      /// Dec08
      /// </summary>
      public static void Dec08( )
      {
      //Read input and parse..
         int[ ] inp = GlobalMethods.GetInputIntArray( "..\\..\\Inputs\\Dec08.txt" );
         //int[ ] inp = GlobalMethods.GetInputIntArray( "..\\..\\Inputs\\Temp01.txt" );





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
