using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
   public class Days
   {

      /*STATIC METHODS*/
      #region




      public static void Dec25()
      {
         //PART1:    
         //Parse the text file to a string..

         ////Main input
         long cardPublicKey = 2069194;
         long doorPublicKey = 16426071;

         //Test input
         //long cardPublicKey = 5764801;
         //long doorPublicKey = 17807724;


         long divider = 20201227;
         long cardSubject = 7;
         long doorSubject = 7;

         long cardLoopSize = 0;
         long doorLoopSize = 0;

         long cardVal = 1;
         long doorVal = 1;

         while( cardVal != cardPublicKey )
         {
            cardVal *= cardSubject;
            cardVal = cardVal % divider;
            cardLoopSize++;
         }

         while( doorVal != doorPublicKey )
         {
            doorVal *= doorSubject;
            doorVal = doorVal % divider;
            doorLoopSize++;
         }

         long encryptionKey1 = TransformSubjectNumber( doorLoopSize, cardVal );
         long encryptionKey2 = TransformSubjectNumber( cardLoopSize, doorVal );


         if( encryptionKey1 != encryptionKey2 )
            throw new Exception( );

      //Remove the ones that are definitely not an allergen
         Console.WriteLine( "Answer for part 1: " + encryptionKey1 );

      }


      public static long TransformSubjectNumber( long loopsize, long subjectNumber )
      {
         long startVal = 1;
         for( int i = 0; i < loopsize; i++ )
         {
            startVal *= subjectNumber;
            startVal = startVal % 20201227;
         }
         return startVal;

      }



      public static void Dec24()
      {
      //Parse the text file to a string..
         string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec24.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );

      //PART1
         Dictionary<KeyValuePair<double, double>, HexTile> allTiles = new Dictionary<KeyValuePair<double, double>, HexTile>( );
         foreach( string s in inp )
         {
         //Find the coordinates for this tile
            KeyValuePair<double, double> thisCoord = HexTile.GetCoordinatesForTileInput( s );

         //Check if this tile already exists, if not, create it. If it does, swap the state
            if( !allTiles.ContainsKey( thisCoord ) )
               allTiles.Add( thisCoord, new HexTile( s ) );
            else
               allTiles[thisCoord].SwapState( );
         }

      //Find the count of black tiles..
         long nOfBlackTiles = allTiles.Where( x => x.Value.State == HexTile.HEXCOLOR.BLACK ).ToList( ).Count;

      //Remove the ones that are definitely not an allergen
         Console.WriteLine( "Answer for part 1: " + nOfBlackTiles );

      //PART2
      //Create exhibit
         HexTileExhibit exhibit = new HexTileExhibit( allTiles );
         int nOfDays = 100;

         //Run simulation
         for( int i = 0; i < nOfDays; i++ )
            exhibit.RunOneStateChange( );

         exhibit.PrintTextState( nOfDays );

      }




      public static void Dec23()
      {
      //PART1:    
      //Parse the text file to a string..
         string inp = "712643589";        //The main input
         //string inp = "389125467";            //The test input
         //string inp = "123456789";
         //string inp = "654321";

         CrabCupGameP1 cg = new CrabCupGameP1( inp );
         int nOfRounds = 5;
         for( int i = 0; i < nOfRounds; i++ )
         {
            //cg.PrintState( );
            cg.PlayRound( );
         }
         cg.PrintState( );

      //PART2
         int totalNumberOfCups = 1000000;
         //int totalNumberOfCups = inp.Length;
         CrabCupGameP2 cg2 = new CrabCupGameP2( inp, totalNumberOfCups - inp.Length );

         nOfRounds = 10000000;
         //nOfRounds = 10;
         for( int i = 0; i<nOfRounds; i++ )
         {
            cg2.PlayRound( );
         }

      //Print the answer
         Dictionary<long, CrabCupGameP2.Cup> cups = cg2.Cups;
         CrabCupGameP2.Cup c1 = cups[1].Next;
         CrabCupGameP2.Cup c2 = c1.Next;
         long ans = (long) c1.Value * (long) c2.Value;

      //Remove the ones that are definitely not an allergen
         Console.WriteLine( "Answer for part 2: " + ans );
      }


      public static void Dec22()
      {
   //PART1:    
      //Parse the text file to a string..
         string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec22.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );

      //Split the input in to the two arrays. One for each player
         List<string> p1 = new List<string>( );
         List<string> p2 = new List<string>( );
         bool foundBlank = false;
         for( int i = 0; i < inp.Length; i++ )
         {
            if( inp[i] == "" )
            {
               foundBlank = true;
               continue;
            }

            List<string> inpList;
            if( !foundBlank )
               inpList = p1;
            else
               inpList = p2;

            inpList.Add( inp[i] );
         }
         CardGamePlayer player1 = new CardGamePlayer( p1.ToArray( ) );
         CardGamePlayer player2 = new CardGamePlayer( p2.ToArray( ) );

      //Start the card game.
         while( true )
         {
            bool playOn = CardGamePlayer.PlayOneRoundPART1( player1, player2 );
            if( !playOn )
               break;
         }

      //Get the point of both players
         long p1Score = player1.GetScore( );
         long p2Score = player2.GetScore( );
         long ans1 = Math.Max( p1Score, p2Score );

      //Write answer to part one 
         Console.WriteLine( "Answer for part 1: " + ans1 );

      //PART2
      //Initialize new players..
         player1 = new CardGamePlayer( p1.ToArray( ) );
         player2 = new CardGamePlayer( p2.ToArray( ) );

      //Start the game and determine a winner.
         CardGamePlayer winner = CardGamePlayer.PlayGameOfRecursives( player1, player2 );

      //Calculate the score..
         p1Score = player1.GetScore( );
         p2Score = player2.GetScore( );
         long ans2 = Math.Max( p1Score, p2Score );

      //Remove the ones that are definitely not an allergen
         Console.WriteLine( "Answer for part 2: " + ans2 );
      }



      public static void Dec21()
      {
      //PART1:    

      //Parse the text file to a string..
         string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Dec21.txt" );
         //string[] inp = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );

      //Dictionary with different allergens and their a list of the food candidates
         Dictionary<string, List<string>> allergenWithFoodCandidates = new Dictionary<string, List<string>>( );
         List<Ingredient> allIngredients = new List<Ingredient>( );
         List<MenuItem> menuItems = new List<MenuItem>( );

      //Loop over the menu items
         foreach( string s in inp )
         {
            string[] splitt = s.Split( new char[] { ' ' } );
            bool foundSep = false;
            List<string> foodCandidates = new List<string>( );
            List<string> allergens = new List<string>( );
         //Go through the ingredient list and create an ingredient if it doesnt already exist
            for( int i = 0; i < splitt.Length; i++ )
            {
               
            //The contains line was not found. THis is still a memnu item and not an ingredient
               if( !foundSep )
               {
                  string thisName = splitt[i];
                  if( splitt[i].Contains( '(' ) )
                  {
                     foundSep = true;
                     continue;
                  }
                  if( !foodCandidates.Contains( thisName ) )
                     foodCandidates.Add( thisName );

               //Create an ingredient and add it to the list of all ingredients
                  Ingredient ingredient = allIngredients.Where( x => x.Name == thisName ).FirstOrDefault( );
                  if( ingredient == null )
                     allIngredients.Add( new Ingredient( thisName ) );
               }
               else
               {
               //Find the allergen name, remove the comma
                  string thisName = splitt[i];
                  string thisAllergen = null;
                  if( thisName.Contains( ',' ) )
                     thisAllergen = thisName.Substring( 0, thisName.Length - 1 );
                  else
                     thisAllergen = thisName;

                  if( thisAllergen.Contains( ')' ) )
                     thisAllergen = thisAllergen.Substring( 0, thisAllergen.Length - 1 );

                  allergens.Add( thisAllergen );
               //Add the allergen to the list if it doesnt exist.
                  if( !allergenWithFoodCandidates.ContainsKey( thisAllergen ) )
                     allergenWithFoodCandidates.Add( thisAllergen, new List<string>( foodCandidates ) );
                  else
                  {
                     List<string> foodCandidatesForThis = allergenWithFoodCandidates[thisAllergen];
                     List<string> newFoods = new List<string>( );
                     foreach( string oldFood in foodCandidates )
                        if( !foodCandidatesForThis.Contains( oldFood ) )
                           newFoods.Add( oldFood );

                     foodCandidatesForThis.AddRange( newFoods );
                  }
               }
            }
         //Create a new menu item..
            List<Ingredient> ingr = new List<Ingredient>( );
            foreach( string t in foodCandidates )
               ingr.Add( new Ingredient( t ) );

            MenuItem m = new MenuItem( ingr, allergens );
            menuItems.Add( m );
         }

      //Ingredients with no allergens..
         List<Ingredient> ingredientsWithout = new List<Ingredient>( );
         for( int i = 0; i < menuItems.Count; i++ )
         {
         //Declare the current menu item
            MenuItem mi = menuItems[i];

         //Get list of allergens in this menu item
            List<string> allergens = mi.Allergens;
            List<Ingredient> ingredient = mi.Ingredients;

            foreach( KeyValuePair<string, List<string>> kvp in allergenWithFoodCandidates )
            {
            //Skip to next if this allergen candidate is not listed in the menu items.
               if( !allergens.Contains( kvp.Key ) )
                  continue;

            //Declare a list of stuff to remove after iterating over collection
               List<string> candidatesToRemove = new List<string>( );
               
            //Loop over every single ingredient in this allergens candidates. Remove it from this list if it is NOT present.
               foreach( string singleIngredient in kvp.Value )
               {
                  Ingredient checkedIngredient = ingredient.Where( x => x.Name == singleIngredient ).FirstOrDefault( );
                  if( checkedIngredient == null )
                     candidatesToRemove.Add( singleIngredient );
               }
            //Remove those from the collection
               foreach( string c in candidatesToRemove )
                  kvp.Value.Remove( c );
            }

         }

      //Now we have a list of all the menu items with the correct food candidates. Any food item that is not in this list cannot possibly be a food item.
      //Compile a list of all the food items that are not in any of the candidate list.
         foreach( Ingredient ingredient in allIngredients )
         {
            bool foundIt = false;
            foreach( KeyValuePair<string, List<string>> kvp in allergenWithFoodCandidates )
            {
               foreach( string checker in kvp.Value )
               {
               //We found the ingredient in a candidate list. This can possibly be a allergen
                  if( ingredient.Name == checker )
                  {
                     foundIt = true;
                     break;
                  }
               }
               if( foundIt )
                  break;
            }
         //If we did not find it in the list of candidates at all, add it to the list of foods without allergens
            if( !foundIt )
               ingredientsWithout.Add( ingredient );
         }

      //Count the number of times these ingredients appear..
         int nOfAppear = 0;
         foreach( MenuItem mi in menuItems )
            foreach( Ingredient ingredientsInMenuItem in mi.Ingredients )
               foreach( Ingredient ingredientWithout in ingredientsWithout )
                  if( ingredientsInMenuItem.Name == ingredientWithout.Name )
                     nOfAppear++;

      //Remove the ones that are definitely not an allergen
         long ans1 = nOfAppear;
         //Write answer to part one 
         Console.WriteLine( "Answer for part 1: " + ans1 );

   //PART2:    //Find the ingredients in the list
      //Find the allergen with only one candidate. Add that to the hash set of value pairs and remove from the other candidate lists
         HashSet<KeyValuePair<string, Ingredient>> ingredientAllergenPair = new HashSet<KeyValuePair<string, Ingredient>>( );

      //Find what ingredient is each allergen pair
         while( true )
         {

         //Add pair to the hash set.
            KeyValuePair<string, List<string>>? nextPair = ( KeyValuePair<string, List<string>> ) allergenWithFoodCandidates.Where( x => x.Value.Count == 1 ).ToList( ).FirstOrDefault( );

            //Get the next pair in the list
            var thisObject = allergenWithFoodCandidates.Where( x => x.Value.Count == 1 ).FirstOrDefault( );
            if( thisObject.Key == null )
               break;

            string thisAllergen = allergenWithFoodCandidates.Where( x => x.Value.Count == 1 ).FirstOrDefault( ).Key;
            string thisIngredient = allergenWithFoodCandidates[thisAllergen][0];
            ingredientAllergenPair.Add( new KeyValuePair<string, Ingredient>( thisAllergen, new Ingredient( thisIngredient ) ) );

         //Check if loop should be quit.
            if( nextPair == null )
               break;

         //Remove candidate from the candidate list.
            foreach( KeyValuePair<string, List<string>> kvp in allergenWithFoodCandidates )
               if( kvp.Value.Contains( thisIngredient ) )
                  kvp.Value.Remove( thisIngredient );
         }

      //Now we have found what ingredient is what allergen. Sort alphabetically by allergen and seperate by commas
         string ans2 = MenuItem.ArrangeByAllergen( ingredientAllergenPair.ToList( ) );

      //Write answer to part two 
         Console.WriteLine( "Answer for part 2: " + ans2 );
         Clipboard.SetText( ans2 );

      }

      public static void Dec20()
      {
      //PART1:    

      //Parse the text file to a string..
         StreamReader sr = new StreamReader( @"Inputs\\Dec20.txt" );
         //StreamReader sr = new StreamReader( @"Inputs\\Temp.txt" );
         //StreamReader sr = new StreamReader( @"Inputs\\Temp2.txt" );
         //StreamReader sr = new StreamReader( @"Inputs\\Temp3.txt" );

         List<string[]> pieces = new List<string[]>( );
         while( !sr.EndOfStream )
         {
            string line = sr.ReadLine( );
            if( line[0] == 'T' )
            {
               List<string> thisPiece = new List<string>( );
               thisPiece.Add( line );
               while( !sr.EndOfStream )
               {
                  line = sr.ReadLine( );
                  if( line == "" )
                     break;
                  else
                     thisPiece.Add( line );
               }
               pieces.Add( thisPiece.ToArray( ) );
            }
         }
         List<JigSaw> pp = new List<JigSaw>( );
         foreach( string[] s in pieces )
            pp.Add( new JigSaw( s ) );

         JigSawPuzzle puzzle = new JigSawPuzzle( pp );
         puzzle.SolvePuzzle( pp[0] );
         (int rows, int columns) = puzzle.GetDimensions( );
         long id1 = puzzle[0, 0].ID;
         long id2 = puzzle[rows-1, 0].ID;
         long id3 = puzzle[0, columns-1].ID;
         long id4 = puzzle[rows-1, columns-1].ID;
         long ans1 = id1 * id2 * id3 * id4;
         Console.WriteLine( "Answer for part 1: " + ans1 );
      }

      public static void Dec19()
      {
      
      //PART1:    
      //Parse the text file to a string..
         StreamReader sr = new StreamReader( @"Inputs\\Dec19.txt" );
         //StreamReader sr = new StreamReader( @"Inputs\\Temp.txt" );
         //StreamReader sr = new StreamReader( @"Inputs\\Temp2.txt" );
         //StreamReader sr = new StreamReader( @"Inputs\\Temp3.txt" );

         Dictionary<int, MonsterMessageRule> rules = new Dictionary<int, MonsterMessageRule>( );
         while( !sr.EndOfStream )
         {
            string line = sr.ReadLine( );
            if( line == "" )
            {
               break;
            }
            MonsterMessageRule rule = new MonsterMessageRule( line );
            rules.Add( rule.ID, rule );
         }

      //Get the unique messages from the input
         HashSet<string> uniqueMessages = new HashSet<string>( );
         while( !sr.EndOfStream )
         {
            string line = sr.ReadLine( );
            uniqueMessages.Add( line );
         }


         string pattern = "^" + rules[0].CreateRegexPattern( rules ) + "$";
         int count = 0;
         foreach( string s in uniqueMessages )
         {
            bool b = Regex.IsMatch( s, pattern );
            if( b )
               count++;
         }
         long ans1 = count;
         Console.WriteLine( "Answer for part 1: " + ans1 );


      //PART2
      //Replace rule 8 and rule 11.
         List<string> possibleRule8 = new List<string>( );
         int nOfRepeats = 10;
         string thisString = "";
         for( int i = 0; i < nOfRepeats; i++ )
         {
            if( i == 0 )
               thisString += "8:" ;
            if( i > 0 )
               thisString += " |";
            for( int j = 0; j <= i; j++ )
               thisString += " 42";

            possibleRule8.Add( thisString );
         }

         List<string> possibleRule11 = new List<string>( );
         thisString = "";
         for( int i = 0; i < nOfRepeats; i++ )
         {
            if( i == 0 )
               thisString += "11:" ;
            if( i > 0 )
               thisString += " |";
            for( int j = 0; j <= i; j++ )
               thisString += " 42";
            for( int j = 0; j <= i; j++ )
               thisString += " 31";

            possibleRule11.Add( thisString );
         }

         //string newRule8  = "8: 42 | 42 42 | 42 42 42 | 42 42 42 42 | 42 42 42 42 42 | 42 42 42 42 42 42 | 42 42 42 42 42 42 42 | 42 42 42 42 42 42 42 42";
         //string newRule11 = "11: 42 31 | 42 42 31 31 | 42 42 42 31 31 31 | 42 42 42 42 31 31 31 31 | 42 42 42 42 42 31 31 31 31 31 | 42 42 42 42 42 42 31 31 31 31 31";

         int ruleCounter = 0;
         int prevAns = 0;
         while( true )
         {
         //Remove the rules with infinite loops..
            rules.Remove( 8 );
            rules.Remove( 11 );

            //MonsterMessageRule r8 = new MonsterMessageRule( possibleRule8[ruleCounter] );
            //MonsterMessageRule r11 = new MonsterMessageRule( possibleRule11[ruleCounter] );

            MonsterMessageRule r8 = new MonsterMessageRule( possibleRule8[ruleCounter] );
            MonsterMessageRule r11 = new MonsterMessageRule( possibleRule11[ruleCounter] );

         //Increment the rule counter and add the new rule..
            rules.Add( 8, r8 );
            rules.Add( 11, r11 );

            pattern = "^" + rules[0].CreateRegexPattern( rules ) + "$";
            count = 0;
            foreach( string s in uniqueMessages )
            {
               bool b = Regex.IsMatch( s, pattern );
               if( b )
                  count++;
            }

            if( prevAns == count )
               break;
            else
            {
               prevAns = count;
               ruleCounter++;
            }

         }

         long ans2 = count;
         Console.WriteLine( "Answer for part 2: " + ans2 + ", number of loops = " + ( ruleCounter-1).ToString() );



      }


      public static void Dec19Old( )
      {

      //PART1:    
      //Parse the text file to a string..
         //StreamReader sr = new StreamReader( @"Inputs\\Dec19.txt" );
         StreamReader sr = new StreamReader( @"Inputs\\Temp.txt" );
         //StreamReader sr = new StreamReader( @"Inputs\\Temp2.txt" );

         Dictionary<int, MonsterMessageRule> rules = new Dictionary<int, MonsterMessageRule>( );
         while( !sr.EndOfStream )
         {
            string line = sr.ReadLine( );
            if( line == "" )
            {
               break;
            }
            MonsterMessageRule rule = new MonsterMessageRule( line );
            rules.Add( rule.ID, rule );
         }

      //Get the unique messages from the input
         HashSet<string> uniqueMessages = new HashSet<string>( );
         while( !sr.EndOfStream )
         {
            string line = sr.ReadLine( );
            uniqueMessages.Add( line );
         }


         int part = 2;

         if( part == 1 )
         {
         //Possible strings for rule 0
            List<string> possibleStrings = rules[0].GetPossibleStringsForThisRule( "", rules );
            HashSet<string> uniquePossibleStrings = new HashSet<string>( );
            foreach( string s in possibleStrings )
            {
               if( !uniquePossibleStrings.Contains( s ) )
                  uniquePossibleStrings.Add( s );
            }

         //Collect all the strings from the messages that match rule 0..
            long count = 0;
            foreach( string m in uniqueMessages )
               if( uniquePossibleStrings.Contains( m ) )
                  count++;

            long ans1 = count;
            Console.WriteLine( "Answer for part 1: " + ans1 );

         }
         else
         {

         //PART2:    
         //Replace rule 8 and rule 11.
            bool update = true;
            if( update )
            {
               rules.Remove( 8 );
               rules.Remove( 11 );

               //// OLD              8: 42 //Replace the call with enough repeats to get the full strings..
               //string newRule8  = "8: 42 | 8 42";

               //// OLD             "11: 42 31
               //string newRule11 = "11: 42 31 | 42 11 31";


               string newRule8  = "8: 42 | 42 42 | 42 42 42";
               string newRule11 = "11: 42 31 | 42 42 31 31";

               //42: 100 129 | 121  91
               //31: 108  91 |  85 129

               MonsterMessageRule r8 = new MonsterMessageRule( newRule8 );
               MonsterMessageRule r11 = new MonsterMessageRule( newRule11 );
               rules.Add( r8.ID, r8 );
               rules.Add( r11.ID, r11 );
            }

         //FInd the maximum length of the messages to match, then pass this down through the layers. IF the prefix for the string is above this length, stop the call stack and return nothing. This only applies to rules 8 and 11;
         //Runs out of memory. Creating all possible strings is not possible.
            List<string> possibleStrings = rules[0].GetPossibleStringsForThisRule( "", rules, 50 );

            HashSet<string> uniquePossibleStrings = new HashSet<string>( );
            foreach( string s in possibleStrings )
            {
               if( !uniquePossibleStrings.Contains( s ) )
                  uniquePossibleStrings.Add( s );
            }

         //Collect all the strings from the messages that match rule 0..
            long count = 0;
            foreach( string m in uniqueMessages )
               if( uniquePossibleStrings.Contains( m ) )
                  count++;


            long ans2 = count;
            //Write answer to part one 
            Console.WriteLine( "Answer for part 2: " + ans2 );
         }





      }

      public static void Dec18( )
      {

      //PART1:    
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec18.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp2.txt" );
         //string thisLine = "2 * 3 + (4 * 5)";
         //string thisLine = "5 + ( 8 * 3 + 9 + 3 * 4 * 3 )";
         //string thisLine = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";


         List<long> answers = new List<long>( );
         foreach( string line in input )
            answers.Add( OperatorOrder.EvaluateExpressionPart1( line ) );

         long ans1 = answers.Sum( );

         Console.WriteLine( "Answer for part 1: " + ans1 );

         //PART2:    
         //string thisLine = "2 * 3 + 4 * 5";
         //string thisLine = "2 * 3 + (4 * 5) + 3";
         //string thisLine = "1 + (2 * 3) + (4 * (5 + 6))";
         //string thisLine = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";

         answers = new List<long>( );
         foreach( string line in input )
            answers.Add( OperatorOrder.EvaluateExpressionPart2( line ) );
         long ans2 = answers.Sum( );
         //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + ans2 );
      }


      public static void Dec17( )
      {

      //PART1:    
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec17.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp2.txt" );

      //Create a cube collection
         CubeCollection cc = new CubeCollection( input );
         int nOfCycles = 6;
         for( int i = 0; i < nOfCycles; i++ )
         {
            //cc.PrintCubeState( );
            cc.Cycle( );
         }

         long ans1 = cc.ActiveCubesInCollection;
         Console.WriteLine( "Answer for part 1: " + ans1 );

         //PART2:    
         //mg = new MemoryGame( inp );
         //currentNumber = mg.LastNumberInInitializer;
         //for( int i = inp.Length+1; i < 30000001; i++ )
         //   currentNumber = mg.GetNextNumberInGame( i, currentNumber );
         long ans2 = 0;

         //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + ans2 );
      }






      public static void Dec16( )
      {

      //PART1:    
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec16.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp2.txt" );

      //Create the train ticket validater
         TrainTicketValidater validater = new TrainTicketValidater( input );

      //Create the personal train ticket
         TrainTicket myTicket = null;
         for( int i = 0; i < input.Length; i++ )
         {
            string[] splitter = input[i].Split( new char[] { ':' } );
            if( splitter[0] == "your ticket" )
            {
               myTicket = new TrainTicket( input[i + 1] );
               break;
            }
         }

      //Create all other tickets
         bool foundNearby = false;
         List<TrainTicket> nearbyTickets = new List<TrainTicket>( );
         for( int i = 0; i < input.Length; i++ )
         {
            if( !foundNearby )
            {
               string[] splitter = input[i].Split( new char[] { ':' } );
               if( splitter[0] == "nearby tickets" )
                  foundNearby = true;
            }
            else
            {
               nearbyTickets.Add( new TrainTicket( input[i] ) );
            }
         }


         //Loop over all the nearby tickets and check for the sum of the invalid values..
         long totalErrorSum = 0;
         List<TrainTicket> invalidTickets = new List<TrainTicket>( );
         foreach( TrainTicket ot in nearbyTickets )
         {
            List<int> theseInvalidNumbers = ot.GetInvalidNumbers( validater );
            if( theseInvalidNumbers.Count > 0 )
               invalidTickets.Add( ot );
            totalErrorSum += theseInvalidNumbers.Sum( );
         }

      //Write answer to part one 
         long ans1 = totalErrorSum; 
         Console.WriteLine( "Answer for part 1: " + ans1 );
         Clipboard.SetText( ans1.ToString( ) );

      //PART2:    

      //Remove the invalid tickets from the list of nearby tickets
         List<TrainTicket> validNearbyTickets = new List<TrainTicket>( );
         foreach( TrainTicket t in nearbyTickets )
            if( !invalidTickets.Contains( t ) )
               validNearbyTickets.Add( t );

         validNearbyTickets.Add( myTicket );

      //Validate the category for each ticket individually
         Dictionary<string, int> myTicketValues = new Dictionary<string, int>( );

      //Find all the categories..
         List<Tuple<string, int>> ignoreList = new List<Tuple<string, int>>( );
         while( true )
         {
            Tuple<string, int> nextCategoryWithIndexPair = validater.FindNextCategoryIndexPair( validNearbyTickets, ignoreList );

            //Add the values from the returning categoryindex pair to the dictionary on my ticket
            if( nextCategoryWithIndexPair == null )
               throw new Exception( );
            myTicket.AddCategoryWithValue( nextCategoryWithIndexPair.Item1, myTicket.Values[nextCategoryWithIndexPair.Item2] );

            //Add the category to the ignore list for the next parsing round..
            ignoreList.Add( nextCategoryWithIndexPair );

            //Break out of loop if all the categories have been found, e.g -> The number of categories in the ignore list is equal to the number of values.
            if( myTicket.GetNumberOfDecidedCategories( )== validater.NumberOfCategories )
               break;
         }

         long ans2 = myTicket.GetAnswerForPartTwoForMyTicked( );

      //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + ans2 );
         Clipboard.SetText( ans2.ToString() );

      }


      public static void Dec15( )
      {

      //PART1:    
         string input = "0,12,6,13,20,1,17";

         //Split by comma
         string[ ] split = input.Split( new char[ ] { ',' } );
         long[] inp = new long[split.Length];
         for( int i = 0; i < inp.Length; i++ )
            inp[i] = long.Parse( split[i] );

         //Create a new memory game
         MemoryGame mg = new MemoryGame( inp );
         long currentNumber = mg.LastNumberInInitializer;
         for( int i = inp.Length+1; i < 2021; i++ )
            currentNumber = mg.GetNextNumberInGame( i, currentNumber );
         
         //Write answer to part one 
         long ans1 = currentNumber;
         Console.WriteLine( "Answer for part 1: " + ans1 );

      //PART2:    
         mg = new MemoryGame( inp );
         currentNumber = mg.LastNumberInInitializer;
         for( int i = inp.Length+1; i < 30000001; i++ )
            currentNumber = mg.GetNextNumberInGame( i, currentNumber );
         long ans2 = currentNumber;

      //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + ans2 );
      }



      public static void Dec14( )
      {

      //PART1:    
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec14.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp2.txt" );

         DockingSoftware ds = new DockingSoftware( input, false );
         ds.RunSoftware( );

      //Write answer to part one 
         long ans1 = ds.GetTotalSumOfMemoryEntries( );
         Console.WriteLine( "Answer for part 1: " + ans1 );
         Clipboard.SetText( ans1.ToString( ) );

      //PART2:    
         DockingSoftware ds2 = new DockingSoftware( input, true );
         ds2.RunSoftware( );

         long ans2 = ds2.GetTotalSumOfMemoryEntries( );

      //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + ans2 );
         Clipboard.SetText( ans2.ToString() );

      }





      public static void Dec13( )
      {

      //PART1:    
      //Parse the text file to a string..
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec13.txt" );
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );


      //Get the timestamp of when i arrive..
         long arrivalTime = long.Parse( input[0] );

      //Create the busses
         List<Bus> busses = new List<Bus>( );
         string[] ids = input[1].Split( new char[] { ',' } );

         int offset = 0;
         foreach( string s in ids )
         {
            if( s[0] == 'x' )
            {
               offset++;
               continue;
            }

            int v;
            bool su = int.TryParse( s, out v );
            if( !su )
               throw new Exception( );
            busses.Add( new Bus( v, offset++ ) );
         }

      //Find the bus with the closest departure time..
         Bus myBuss = null;
         long timeToWait = long.MaxValue;
         foreach( Bus b in busses )
         {
            if( myBuss == null )
               myBuss = b;

            long thisTimeToWait;
            long timeStamp = b.GetNextDepartureAfterTime( arrivalTime, out thisTimeToWait );
            if( thisTimeToWait < timeToWait )
            {
               timeToWait = thisTimeToWait;
               myBuss = b;
            }

         }



      //Write answer to part one 
         long ans1 = myBuss.ID * timeToWait;
         Console.WriteLine( "Answer for part 1: " + ans1 );
         Clipboard.SetText( ans1.ToString( ) );

         //PART2:    

         ////BRUTE FORCE SOLUTION...
         //bool foundTime = false;
         //long competitionTimeStamp = 0;
         //List<Bus> p2Busses = new List<Bus>( );
         //foreach( Bus b in busses )
         //   p2Busses.Add( b );

         ////p2Busses = p2Busses.OrderBy( x => x.CycleTime ).ToList( );
         ////p2Busses.Reverse( );

         //long repeatedPattern = p2Busses[0].CycleTime;
         //competitionTimeStamp -= p2Busses[0].Offset;
         //while( !foundTime )
         //{
         //   competitionTimeStamp += repeatedPattern;
         //   bool satisfiedForAllBusses = true;
         //   for( int i = 1; i < p2Busses.Count; i++ )
         //   {
         //      if( !p2Busses[i].DoesSatisfyDeparture( competitionTimeStamp, p2Busses[i].Offset ) )
         //      {
         //         satisfiedForAllBusses = false;
         //         repeatedPattern = repeatedPattern * p2Busses[i].CycleTime;
         //         break;
         //      }
         //   }
         //   if( satisfiedForAllBusses )
         //   {
         //      foundTime = true;
         //   }
         //}
         ////END

         //Try something smarter. //CHINESE REMAINER THEOREM
         long repeater = 1;
         long competitionTimeStamp = 1;
         foreach( Bus b in busses )
         {
            //Calculate the new timestamp
            competitionTimeStamp = Bus.CalculateDepartureTimeP2( competitionTimeStamp, repeater, b.CycleTime, b.Offset );

            //This pattern will repeat itself  every x time where x is the product of the bus IDs. Only need to check multiples of the divisors up to this point for the next bus. All other points will not repeat itself
            //Only need to check the current time stamp, plus this repeating time.
            repeater = repeater * b.CycleTime;
         }

         long ans2 = competitionTimeStamp;

      //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + ans2 );
         Clipboard.SetText( ans2.ToString() );

      }





      public static void Dec12( )
      {

      //PART1:    
         //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec12.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );

         List<ShipInstruction> instructions = new List<ShipInstruction>( );
         foreach( string l in input )
            instructions.Add( new ShipInstruction( l ) );

      //Create ship.
         Ship ship = new Ship( );
         foreach( ShipInstruction ins in instructions )
            ship.CarryOutInstruction( ins );

      //Write answer to part one 
         double ans1 = ship.GetManhattanDistance( );
         Console.WriteLine( "Answer for part 1: " + ans1 );
         Clipboard.SetText( ans1.ToString( ) );

      //PART2:    
         Ship wShip = new Ship( true );
         foreach( ShipInstruction ins in instructions )
            wShip.CarryOutInstruction( ins );

      //Find the manhattan distance
         double ans2 = wShip.GetManhattanDistance( );

      //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + ans2 );
         Clipboard.SetText( ans2.ToString() );


      }


      public static void Dec11( )
      {

         //PART1:    
         SeatingSystem ss = new SeatingSystem( );
         ss.UpdateSeatingStatusUntilStable( );
         int ans2 = ss.OccupiedSeats;

      ////Write answer to part one 
      //   Console.WriteLine( "Answer for part 1: " + ans1 );
      //   Clipboard.SetText( ans1.ToString() );

      //PART2:    
      //Create the adapter array..

      //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + ans2 );

      }


      public static void Dec10( )
      {

      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec10.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );


      //Create a list of adapters
         List<Adapter> adapters = new List<Adapter>( );
         foreach( string s in input )
            adapters.Add( new Adapter( s ) );

      //Find the maximum adapter voltage
         long myAdapterVoltage = adapters.Select( x => x.Joltage ).ToList( ).Max( ) + 3;

      //Add the outlet..
         adapters.Add( new Adapter( "0" ) );

      //Add the input adapter
         adapters.Add( new Adapter( myAdapterVoltage.ToString( ) ) );

      //SOrt the list..
         List<Adapter> sortedList = adapters.OrderBy( x => x.Joltage ).ToList( );

         List<Adapter> joltDiff1 = new List<Adapter>( );
         List<Adapter> joltDiff2 = new List<Adapter>( );
         List<Adapter> joltDiff3 = new List<Adapter>( );

         for( int i = 1; i < sortedList.Count; i++ )
         {
            long joltDiff = sortedList[i].Joltage - sortedList[i - 1].Joltage;
            if( joltDiff == 1 )
               joltDiff1.Add( sortedList[i] );
            if( joltDiff == 2 )
               joltDiff2.Add( sortedList[i] );
            if( joltDiff == 3 )
               joltDiff3.Add( sortedList[i] );
         }

         long ans = joltDiff1.Count * joltDiff3.Count;

      //PART1:    
         //Write answer to part one 
         Console.WriteLine( "Answer for part 1: " + ans );

      //PART2:    
      //Create the adapter array..
         long[] inpArr = sortedList.Select( x => x.Joltage ).ToArray( );
         long ans2 = Adapter.FindPathwaysToEnd(inpArr, 0 );

      //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + ans2 );
         Clipboard.SetText( ans2.ToString() );

      }


      public static void Dec09( )
      {
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec09.txt" );
         //string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Temp.txt" );

         SeatScreen ss = new SeatScreen( input );
         long val = ss.FindInvalidNumber( 25 );

      //PART1:    
         //Write answer to part one 
         Console.WriteLine( "Answer for part 1: " + val );

      //PART2:    
         long ans2 = ss.FindContiguousSetThatSumsToTargetThenReturnSumOfMinAndMax( val );

      //Write answer to part two 
         Console.WriteLine( "Answer for part 2: " + ans2 );
      }

      public static void Dec08( )
      {
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec08.txt" );
         //string[] input = Methods.GetInputStringArray( @"Inputs\\Temp.txt" );


      //PART1:    
         GameConsole org = new GameConsole( input );

         GameConsole.GAMECONSOLEABORTION? exitType = null;
         while( exitType == null )
            exitType = org.Run( );

      //Find the final value of the accumulator
         long accumulator = org.Accumulator;

      //Write answer to part one 
         Console.WriteLine( "Answer for part 1: " + accumulator );




      //PART2

      //Create the game console.
         GameConsole originalConsole = new GameConsole( input );

      //Declare the fixed console..
         GameConsole fixedConsole = null;
         int permIdx = -1;
      //loop through the instructions and permuate a jmp to a nop and vica versa
         for( int i=0; i<originalConsole.Instructions.Length; i++ )
         {
         //Find the instruction in the original console that we are fixing
            GameConsole.GameConsoleInstruction ins = originalConsole.Instructions[i];

            //Check if this should be permutated
            if( ins.Instruction == GameConsole.GAMECONSOLEINSTRUCTION.JMP || ins.Instruction == GameConsole.GAMECONSOLEINSTRUCTION.NOP )
            {

            //Create a new "fixed" console from the original input
               fixedConsole = new GameConsole( input );

            //Find the new instruction.
               GameConsole.GAMECONSOLEINSTRUCTION newInstruction;
               if( ins.Instruction == GameConsole.GAMECONSOLEINSTRUCTION.JMP )
                  newInstruction = GameConsole.GAMECONSOLEINSTRUCTION.NOP;
               else
                  newInstruction = GameConsole.GAMECONSOLEINSTRUCTION.JMP;

            //Change the instruction on the fixed console..
               fixedConsole.Instructions[i].Instruction = newInstruction;
               
            //Try to run the new console with the fixed instruction
               exitType = null;
               while( exitType == null )
                  exitType = fixedConsole.Run( );

               if( exitType == GameConsole.GAMECONSOLEABORTION.ENDOFINSTRUCTION )
               {
                  permIdx = i;
                  break;
               }
            }
            else
               continue;

         }

      //Find the value of the accumulator in the fixed console
         long accp2 = fixedConsole.Accumulator;

      //Write answer to part two 
         Console.WriteLine( "Answer for part 2: " + accp2 );
         Console.WriteLine( "The instruction that was permutated had index: " + permIdx );
      }





      public static void Dec07( )
      {
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec07.txt" );
         //string[] input = Methods.GetInputStringArray( @"Inputs\\Temp.txt" );

         List<Bag> allBags = new List<Bag>( );
         foreach( string s in input )
         {
            Bag newForm = new Bag( s );
            allBags.Add( newForm );
         }

      //Find the golden bag..
         Bag goldenBag = allBags.Where( x => x.ToString( ) == "shiny gold" ).FirstOrDefault( );

      //Declare list of golden bag parents
         List<Bag> goldenParents = goldenBag.GetImmediateParents( allBags );

      //Loop through and find parents. Stop when no other parents were found
         bool foundAtLeastOneParent = true;

         while( foundAtLeastOneParent )
         {
         //Start a list if newly added parents
            List<Bag> newlyAddedParents = new List<Bag>( );
            foreach( Bag bag in goldenParents )
            {
               List<Bag> theseParents = bag.GetImmediateParents( allBags );
               foreach( Bag immediateParent in theseParents )
                  if( !goldenParents.Contains( immediateParent ) && !newlyAddedParents.Contains( immediateParent ) )
                     newlyAddedParents.Add( immediateParent );
            }
         //Add the newly added parents..
            goldenParents.AddRange( newlyAddedParents );

         //Check if any parents were added. if not, do not continue as there arent any other parents.
            if( newlyAddedParents.Count == 0 )
               foundAtLeastOneParent = false;
         }

      //PART1:    
         //Write answer to part one 
         Console.WriteLine( "Answer for part 1: " + goldenParents.Count );

      //PART2:    
      //Count number of children in the shiny gold bag. Substract one because it counts the golden bag itself
         int nOfChildren = Bag.GetNumberOfBagsForAllChildrenInlcusive( goldenBag, allBags )-1;

         //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + nOfChildren );
      }











      public static void Dec06( )
      {
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec06.txt" );
         //string[] input = Methods.GetInputStringArray( @"Inputs\\Temp.txt" );

      //PART1:    
         List<string[]> rawData = GlobalMethods.SplitStringArrayByEmptyLine( input );
         List<CustomDeclarationForm> forms = new List<CustomDeclarationForm>( );
         foreach( string[] s in rawData )
         {
            CustomDeclarationForm newForm = new CustomDeclarationForm( s );
            forms.Add( newForm );
         }

      //Count number of positives..
         int totalYes = 0;
         foreach( CustomDeclarationForm f in forms )
         {
            int thisAns = f.GetNumberOfPositivesFromForm( );
            totalYes = totalYes + thisAns;
         }

         ////Write answer to part one 
         //   Console.WriteLine( "Answer for part 1: " + totalYes );

         //PART2:    
         //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + totalYes );
      }

      /// <summary>
      /// Day 5 method
      /// </summary>
      public static void Dec05( )
      {
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec05.txt" );
         //string[] input = Methods.GetInputStringArray( @"Inputs\\Temp.txt" );


      //PART1:    FIND THE HIGHEST ID
         List<BoardingPass> boardingPasses = new List<BoardingPass>( );
         foreach( string s in input )
            boardingPasses.Add( BoardingPass.CreateBoardingPassFromString( s ) );

      //Find the highest id
         int maxId = boardingPasses.Select( x => x.SeatID ).Max( );

      //Write answer to part one 
         Console.WriteLine( "Answer for part 1: " + maxId );


      //PART2:    
         Flight thisFlight = new Flight( );

      //Loop through all the boarding passes and populate the flight..
         foreach( BoardingPass p in boardingPasses )
            thisFlight.InsertPassenger( p );

      //Find the vacant seat in the flight..
         List<Tuple<int, int>> availableSeats = thisFlight.FindVacantSeatsInFlight( );

      //Check for the number of available seats. Should be only one
         if( availableSeats.Count != 1 )
            throw new Exception( );

      //Create seat row.
         int seatRow = availableSeats[0].Item1;
         int seatColumn = availableSeats[0].Item2;

      //Calculate the ID of my Boarding pass..
         int seatID = seatRow * 8 + seatColumn;

      //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + seatID );
      }



      /// <summary>
      /// Day 4 method
      /// </summary>
      public static void Dec04( )
      {
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec04.txt" );
         //string[] input = Methods.GetInputStringArray( @"Inputs\\Temp.txt" );

      //PART1
      //Collect the individual passport data in the list.
         List<string[]> rawPassportData = new List<string[]>( );
         int lastEmptyIdx = -1;
         for( int i = 0; i < input.Length + 1; i++ )
         {
            if( i == input.Length || input[i].Equals( "" ) )
            {
               int nOfLines = i - lastEmptyIdx-1;
               string[] thisRawData = new string[nOfLines];
               for( int j = 0; j < nOfLines; j++ )
               {
                  thisRawData[j] = input[j + lastEmptyIdx+1];
               }
               rawPassportData.Add( thisRawData );
               lastEmptyIdx = i;
            }
         }

      //Create passports from the raw data..
         List<Passport> passPorts = new List<Passport>( );
         foreach( string[] rd in rawPassportData )
         {
            Passport thisPassport = Passport.CreateFromStringArray( rd, false );
            passPorts.Add( thisPassport );
         }

         List<Passport> validPassPorts = new List<Passport>( );
         //Collect the valid passports..
         foreach( Passport p in passPorts )
         {
            bool isValid = p.ValidatePassport( false, true );
            if( isValid )
               validPassPorts.Add( p );
         }

      //Write answer to part one 
         Console.WriteLine( "Answer for part 1: " + validPassPorts.Count );


      //PART2
         passPorts = new List<Passport>( );
         foreach( string[] rd in rawPassportData )
         {
            Passport thisPassport = Passport.CreateFromStringArray( rd, true );
            passPorts.Add( thisPassport );
         }

         validPassPorts = new List<Passport>( );
         //COllect the valid passports
         foreach( Passport p in passPorts )
         {
            bool isValid = p.ValidatePassport( false, false );
            if( isValid )
               validPassPorts.Add( p );
         }

      //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + validPassPorts.Count );

      }




      /// <summary>
      /// Day 3 methods
      /// </summary>
      public static void Dec03( )
      {
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec03.txt" );

         int nOfTrees = 0;

      //PART1
         int colIdx = 3;
         int rowIdx = 1;
         int lengthOfRepeatedSlope = input[0].Length;

         while( rowIdx < input.Length )
         {
            string line = input[rowIdx];

            char symb = line[colIdx];
            if( symb.Equals( '#' ) )
               nOfTrees++;

            colIdx = colIdx + 3;
            colIdx = colIdx % lengthOfRepeatedSlope;
            rowIdx++;
         }
         
      //Write answer to part one 
         Console.WriteLine( "Answer for part 1: " + nOfTrees );


      //PART2
         List<Tuple<int, int>> increments = new List<Tuple<int, int>>( );
         increments.Add( new Tuple<int, int>( 1, 1 ) );
         increments.Add( new Tuple<int, int>( 1, 3 ) );
         increments.Add( new Tuple<int, int>( 1, 5 ) );
         increments.Add( new Tuple<int, int>( 1, 7 ) );
         increments.Add( new Tuple<int, int>( 2, 1 ) );
         int[] nOfTreesArr = new int[increments.Count]; 
         int thisArrIdx = 0;
         foreach( Tuple<int, int> incrementPair in increments )
         {
            int thisNumberOfTrees = 0;
            rowIdx = incrementPair.Item1;
            colIdx = incrementPair.Item2;
            while( rowIdx < input.Length )
            {
               string line = input[rowIdx];
               char symb = line[colIdx];
               if( symb.Equals( '#' ) )
                  thisNumberOfTrees++;

               colIdx = colIdx + incrementPair.Item2;
               colIdx = colIdx % lengthOfRepeatedSlope;
               rowIdx = rowIdx + incrementPair.Item1;
            }
            nOfTreesArr[thisArrIdx++] = thisNumberOfTrees;
         }

      //Because of precicion, the value must be representet by a double..
         double prod = 1;
         for( int i = 0; i < nOfTreesArr.Length; i++ )
            prod *= nOfTreesArr[i];

      //Write answer to part two 
         Console.WriteLine( "Answer for part 2: " + prod );
      }



      /// <summary>
      /// Day 2 methods
      /// </summary>
      public static void Dec02( )
      {
      //Parse the text file to a string..
         string[] input = GlobalMethods.GetInputStringArray( @"Inputs\\Dec02.txt" );


      //PART 1
      //Declare list of valid passwords
         List<string> validPasswords = new List<string>( );
         foreach( string line in input )
         {

         //Split string by space first..
            string[] spaceSplitLine = line.Split( new char[] { ' ' } );

         //Get minimum and maximum values..
            string[] dashSplit = spaceSplitLine[0].Split( new char[] { '-' } );
            int minOccurence = int.Parse( dashSplit[0] );
            int maxOccurence = int.Parse( dashSplit[1] ); 

         //Get the checked character
            char charVal = spaceSplitLine[1][0];

         //Get the password..
            string password = spaceSplitLine[2];

         //Count occurences and check
            int occurences = GlobalMethods.CountOccurenceOfCharInString( password, charVal  );

         //Check and add
            if( occurences >= minOccurence && occurences <= maxOccurence )
               validPasswords.Add( password );
         }

      //Write answer to part one 
         Console.WriteLine( "Answer for part 1: " + validPasswords.Count );


      //PART 2
         validPasswords = new List<string>( );
         foreach( string line in input )
         {
         //Split string by space first..
            string[] spaceSplitLine = line.Split( new char[] { ' ' } );

         //Get minimum and maximum values..
            string[] dashSplit = spaceSplitLine[0].Split( new char[] { '-' } );
            int pos1 = int.Parse( dashSplit[0] );
            int pos2 = int.Parse( dashSplit[1] );

         //Get the checked character
            char charVal = spaceSplitLine[1][0];

         //Get the password..
            string password = spaceSplitLine[2];

         //Declare list of positions.. Subtract one because the position is not zero indexed
            int[] positions = new int[] { pos1-1, pos2-1};

            int occurences = GlobalMethods.CountOccurenceOfCharInString( password, charVal, positions );

         //Check and add
            if( occurences == 1 )
               validPasswords.Add( password );
         }

      //Write answer to part one 
         Console.WriteLine( "Answer for part 2: " + validPasswords.Count );

      }



      /// <summary>
      /// Day one method..
      /// </summary>
      public static void Dec01( )
      {

      //Parse the text file to a string..
         int[] input = GlobalMethods.GetInputIntArray( @"Inputs\\Dec01_1.txt" );

         int idx1 = -1;
         int idx2 = -1;
         int idx3 = -1;
         int targetSum = 2020;

         for( int i = 0; i < input.Length; i++ )
         {
            if( input[i] > targetSum )
               continue;
            for( int j = i+1; j < input.Length; j++ )
            {
               if( input[j] > targetSum )
                  continue;
               for( int k = j + 1; k < input.Length; k++ )
               {
                  if( input[k] > targetSum )
                     continue;

                  int sum = input[i] + input[j] + input[k];
                  if( sum == targetSum )
                  {
                     idx1 = i; 
                     idx2 = j;
                     idx3 = k;
                  }
               }
            }
         }
         double prod = input[idx1] * input[idx2] * input[idx3];;
         Console.WriteLine( prod );
      }




      #endregion


   }
}
