using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdventOfCode2018.Classes;
using AdventOfCodeLib.Extensions;

namespace AdventOfCode2018
{
   public class Days
   {


      public static void Dec06( )
      {
         //string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Dec06.txt" );
         string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Temp1.txt" );

         ChronalArea area = new ChronalArea(inp);





      //PART1

         long ans1 = 0;
         Console.WriteLine( "Part 1: " + ans1 );




      //PART 2

         long ans2 = 0;
         Console.WriteLine( "Part 2: " + ans2 );


      }







      public static void Dec05( )
      {

         string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Dec05.txt" );
         //string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Temp1.txt" );

      //PART1


      //Declare the list of candidates. Probably the whole alphabet
         string alphabet = "abcdefghijklmnopqrstuvwxyz";
         char[] candidates = alphabet.ToCharArray( );
         Dictionary<char,int> polymerLengths = new Dictionary<char, int>( );

      //Loop over all the candidates in the char array and calculate the polymer length after they have been removed
         for( int k = 0; k < candidates.Length; k++ )
         {
         //Create all the polymer atoms..
            string polymer = inp[0];
            List<PolymerAtom> atoms = new List<PolymerAtom>( );

         //Check the char before creating the polymer..
            
            PolymerAtom prev = null;
            for( int i = 0; i < polymer.Length; i++ )
            {
            //Check if the polymer should be skipped..
               if( polymer[i] == candidates[k] || polymer[i].ToString( ).ToLower( ) == candidates[k].ToString( ) )
                  continue;

            //Create the new polymer atom..
               PolymerAtom thisAtom = new PolymerAtom( polymer[i] );

            //Set this atom as the next in chain after last one
               if( prev != null )
                  prev.NextAtom = thisAtom;

            //Add to list
               atoms.Add( thisAtom );

            //Update the previous atom so that it is linked..
               prev = thisAtom;
            }

         //Start the reactions..
            bool reacting = true;
            while( reacting )
            {
               bool somethingReacted = false;

            //Find the first polymer in the list that is not anihilated..
               PolymerAtom firstAtom = null;
               for( int i = 0; i<atoms.Count; i++ )
               {
                  if( atoms[i].Status )
                  {
                     firstAtom = atoms[i];
                     break;
                  }
               }

            //Now we just need to loop through the linked list and remove stuff..
               PolymerAtom previousAtom = null;
               PolymerAtom currentAtom = firstAtom;
               while( currentAtom != null )
               {
               //Throw if the current atom is dead. Should not happen
                  if( currentAtom.Status == false )
                     throw new Exception( );

               //Check for anihilation
                  bool anihilation = false;
                  if( currentAtom.NextAtom != null )
                     anihilation = currentAtom.DoesAnihilateEachother( currentAtom.NextAtom );

               //If the anihilate. set status to dead and update the linked list..
                  if( anihilation )
                  {
                     currentAtom.Status = false;
                     currentAtom.NextAtom.Status = false;
                     PolymerAtom leftSide = previousAtom;
                     PolymerAtom rightSide = currentAtom.NextAtom.NextAtom;

                     if( leftSide != null )
                        leftSide.NextAtom = rightSide;

                  //If something reacted, we need to break out of the current while looop and start the reactions all over..
                     somethingReacted = true;

                     break;
                  }
                  else
                  {
                  //Get the next current atom..
                     previousAtom = currentAtom;
                     currentAtom = currentAtom.NextAtom;
                  }

               }
            //Check if something reacted..
               if( !somethingReacted )
                  reacting = false;
            }

         //Find the number of atoms left when the calculation is complete..
            int nOfAtomsLeft = atoms.Where( x => x.Status == true ).ToList( ).Count;
            polymerLengths.Add( candidates[k], nOfAtomsLeft );

         //Write status to window for debug purposes..
            Console.WriteLine( "Candidate: " + candidates[k].ToString( ) + "  Number of atoms in chain: " + nOfAtomsLeft );

         }

      //PART 2
         //Find the shortest polymer in the list..
         int ans2 = polymerLengths.Select( x => x.Value ).ToList( ).Min( );
         Console.WriteLine( "Part 2: " + ans2 );
      }


      public static void Dec04( )
      {

         string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Dec04.txt" );
         //string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Temp1.txt" );

      //Parse the input strings and put them in a dictionary..
         SortedDictionary<DateTime, string> sortedInputs = new SortedDictionary<DateTime, string>( );

         for( int i = 0; i < inp.Length; i++ )
         {
         //DateTimeString
            string dateString = inp[i].Substring( 1, 16 );
            int year = int.Parse( dateString.Substring( 0, 4 ) );
            int month = int.Parse( dateString.Substring( 5,2 ) );
            int day = int.Parse( dateString.Substring( 8, 2 ) );
            int hour = int.Parse( dateString.Substring( 11, 2 ) );
            int minute = int.Parse( dateString.Substring( 14,2 ) );

         //Create the datetime..
            DateTime thisTime = new DateTime( year, month, day, hour, minute, 0 );

         //Rest of this string..
            string restOfString = inp[i].Substring( 19 );

         //Add to the sorted inputs..
            sortedInputs.Add( thisTime, restOfString );
         }

      //The dates are stored from early to last. Now, create all the guards..
         Dictionary<int, Guard> guards = new Dictionary<int, Guard>( );
         Guard currentGuard = null;
         foreach( KeyValuePair<DateTime, string> kvp in sortedInputs )
         {
            string[] splitArr = kvp.Value.Split( new char[]{ ' ' } );
            if( kvp.Value[0] == 'G' )
            {
               int id = int.Parse( ( splitArr[1] ).Substring( 1 ) );
               if( !guards.ContainsKey( id ) )
               {
                  Guard newGuard = new Guard( id );
                  guards.Add( newGuard.ID, newGuard );
                  currentGuard = newGuard;
               }
               else
                  currentGuard = guards[id];

               currentGuard.SetStatus( kvp.Key, Guard.STATUS.AWAKE );
            }
            else if( kvp.Value[0] == 'w' )
               currentGuard.SetStatus( kvp.Key, Guard.STATUS.AWAKE );
            else if( kvp.Value[0] == 'f' )
               currentGuard.SetStatus( kvp.Key, Guard.STATUS.SLEEP );
         }

      //Get the answers..
         long maxTotalMinutes = guards.Select( x => x.Value.GetNumberOfMinutesSleeping( ) ).ToList( ).Max( );
         int guardID = guards.Where( x => x.Value.GetNumberOfMinutesSleeping( ) == maxTotalMinutes ).FirstOrDefault( ).Value.ID;
         int maxMinute = guards[guardID].GetMinuteWithMostSleep( );
         int ans1 = maxMinute*guardID;

      //Write the answer..
         Console.WriteLine( "Part 1: " + ans1 );

      //PART TWO
      //Find the minute where any given guard is asleep the most

         int maxGuardID = -1;
         int maxCount = -1;
         foreach( var kvp in guards )
         {
            int[] thisSleepArray = kvp.Value.GetSleepMinuteArray( );
            int thisMaxCount = thisSleepArray.Max( );
            if( maxCount < thisMaxCount )
            {
               maxGuardID = kvp.Key;
               maxCount = thisMaxCount;
            }
         }

      //Get the minute array for the max guard.
         int maxSleepMinute = guards[ maxGuardID ].GetMinuteWithMostSleep( );
         int ans2 = maxGuardID * maxSleepMinute;
         Console.WriteLine( "Part 2: " + ans2 );


      }


      public static void Dec03( )
      {

      //Part1
         string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Dec03.txt" );
         int areaWidth = 1099;
         int areaHeight = 1099;

         //string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Temp1.txt" );
         //int areaWidth = 11;
         //int areaHeight = 11;

         //Create the whole area.
         int[,] wholeArea = new int[areaHeight,areaWidth];
         for( int i = 0; i < areaHeight; i++ )
            for( int j = 0; j < areaWidth; j++ )
               wholeArea[i,j] = 0;

      //Create all the patches..
         Dictionary<int, FabricPatch> patches = new Dictionary<int, FabricPatch>( );
         for( int i = 0; i < inp.Length; i++ )
         {
            FabricPatch thisPatch = new FabricPatch( inp[i], wholeArea );
            patches.Add( thisPatch.ID, thisPatch );
         }

      //Count number of positions that have more than one entry..
         int nOfIndicesWithMultiple = 0;
         for( int i = 0; i < areaHeight; i++ )
            for( int j = 0; j < areaWidth; j++ )
               if( wholeArea[i,j] >= 2 )
                  nOfIndicesWithMultiple++;

      ////Print the whole thing..
      //   for( int i = 0; i < areaHeight; i++ )
      //   {
      //      StringBuilder sb = new StringBuilder( );
      //      for( int j = 0; j < areaWidth; j++ )
      //         sb.Append( wholeArea[i,j] );

      //      Console.WriteLine( sb.ToString( ) );
      //   }

         Console.WriteLine( "Part 1: " + nOfIndicesWithMultiple );

         int id = patches.Where( x => x.Value.MyPatchOverlap( wholeArea ) == false ).ToList( ).FirstOrDefault( ).Key;
         Console.WriteLine( "Part 2: " + id );
         

      }


      public static void Dec02( )
      {

      //Part1
         string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Dec02.txt" );
         //string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Temp1.txt" );
         //string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Temp2.txt" );

      //Loop over all the input strings.
         long nOfTwoCount = 0;
         long nOfThreeCount = 0;
         for( int i = 0; i<inp.Length; i++ )
         {
         //Declare current string
            string thisString = inp[i];

         //Create the dictionary of letter counts..
            Dictionary<char,int> letterCounts = new Dictionary<char, int>( );
            int j = 0;
            char curChar = thisString[j];
            while( j<inp[i].Length )
            {
               if( !letterCounts.ContainsKey( curChar ) )
               {
                  int count = thisString.CountOccurencesOfLetter( curChar );
                  letterCounts.Add( curChar, count );
               }
               j++;

               if( j >= thisString.Length )
                  break;
               else
                  curChar=thisString[j];

            }

         //Loop through the dictionary and look for lettercounts that are exactly one time..
            int this2 = letterCounts.Where( x => x.Value == 2 ).ToList( ).Count( );
            int this3 = letterCounts.Where( x => x.Value == 3 ).ToList( ).Count( );

            if( this2 >= 1 )
               this2 = 1;
            if( this3 >= 1 )
               this3 = 1;

            nOfTwoCount += this2;
            nOfThreeCount += this3;
         }
         long checksum = nOfTwoCount * nOfThreeCount;
         Console.WriteLine( "Part 1: " + checksum );
         //Clipboard.SetText( checksum.ToString( ) );


      //PART 2
      //FIND THE TWO LETTERS THAT HAVE THE MOST COMMON LETTERS. THEN REMOVE THE LETTERS THAT DIFFER

      //Loop over all the strings..
         Dictionary<int, List<Tuple<string,string>>> commonLetterCount = new Dictionary<int, List<Tuple<string, string>>>( );
         for( int i = 0; i<inp.Length; i++ )
         {

         //Declare the main string..
            string thisComparer = inp[i];

         //Loop over all strings below this string in the list. ALl the others have been checked already
            for( int j = i + 1; j < inp.Length; j++ )
            {
            //Declare candidate string..
               string thisCandidate = inp[j];

            //Count number of common indices
               int commonIndices = thisComparer.CountCommonPositions( thisCandidate );

            //Create a tuple pair..
               Tuple<string, string> thisPair = new Tuple<string, string>( thisComparer, thisCandidate );

            //Find the list to add the tupe to
               List<Tuple<string,string>> thisList = null;
               if( !commonLetterCount.ContainsKey( commonIndices ) )
               {
                  thisList = new List<Tuple<string, string>>( );
                  commonLetterCount.Add( commonIndices, thisList );
               }
               else
                  thisList = commonLetterCount[commonIndices];

            //Add string tuple to the list..
               thisList.Add( thisPair );
            }
         }

      //Find the minimum string pair. Hopefully only one pair..
         int minCommonCount = commonLetterCount.Select( x => x.Key ).ToList( ).Max( );
         KeyValuePair<int, List<Tuple<string, string>>> pair = commonLetterCount.Where( x => x.Key == minCommonCount ).FirstOrDefault( );

      //Create the new string by combining the two string with common letters..
         StringBuilder sb = new StringBuilder( );
         string first = pair.Value[0].Item1;
         string second = pair.Value[0].Item2;
         for( int i = 0; i < first.Length; i++ )
            if( first[i] == second[i] )
               sb.Append( first[i].ToString( ) );

      //Write answer
         Console.WriteLine( "Part 2 " + sb.ToString( ) );
         Clipboard.SetText( sb.ToString( ) );


      }




      /// <summary>
      /// Dec01
      /// </summary>
      public static void Dec01()
      {

         string[] inp = Program.GetInputStringArray( @"..\\..\\Inputs\\Dec01.txt" );

         //Part1
         long sum = 0;
         for( int i = 0; i<inp.Length; i++ )
         {

            string opString = inp[i][0].ToString( );
            long val = long.Parse( inp[i].Substring( 1 ) );
            if( opString == "+" )
               sum += val;
            else if( opString == "-" )
               sum -= val;
            else
               throw new Exception( );
         }

         Console.WriteLine( sum );
         Clipboard.SetText( sum.ToString( ) );

      //Part2
         HashSet<long> frequencies = new HashSet<long>( );
         bool foundDuplicate = false;
         int currIdx = 0;
         sum  = 0;
         while( !foundDuplicate )
         {

            string opString = inp[currIdx][0].ToString( );
            long val = long.Parse( inp[currIdx].Substring( 1 ) );
            if( opString == "+" )
               sum += val;
            else if( opString == "-" )
               sum -= val;
            else
               throw new Exception( );

            if( !frequencies.Contains( sum ) )
            {
               frequencies.Add( sum );
               currIdx++;
               if( currIdx > inp.Length-1 )
                  currIdx = 0;
            }
            else
            {
               foundDuplicate = true;
            }
         }

         Console.WriteLine( sum );
         Clipboard.SetText( sum.ToString( ) );


      }


   }
}
