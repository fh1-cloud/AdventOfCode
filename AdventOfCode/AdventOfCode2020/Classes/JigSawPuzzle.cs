using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class JigSawPuzzle
   {

   /*MEMBERS*/
   #region
      protected Dictionary<int, JigSaw> m_Pieces = new Dictionary<int, JigSaw>( );
      protected JigSaw[,] m_CompletedPuzzle;

      protected Dictionary<int, JigSaw> m_PlacedPieces = new Dictionary<int, JigSaw>( );
      protected Dictionary<int, JigSaw> m_UnplacedPieces;

      protected char[,] m_Intermediate;

      protected List<KeyValuePair<int, int>> m_SeaMonsterPattern = new List<KeyValuePair<int, int>>( );

   #endregion


   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Initializes a JigSawPuzzle from a list of pieces.
      /// </summary>
      /// <param name="pieces"></param>
      public JigSawPuzzle( List<JigSaw> pieces )
      {
         double dim = Math.Sqrt( pieces.Count );
         m_CompletedPuzzle = new JigSaw[( int ) dim, ( int ) dim];
         m_Intermediate = new char[( int ) (dim*3), ( int ) (dim*3)];

         foreach( JigSaw j in pieces )
            m_Pieces.Add( j.ID, j );

         m_UnplacedPieces = new Dictionary<int, JigSaw>( m_Pieces );


      //Defines the sea monster offsets
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 0, 18 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 1, 0 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 1, 5 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 1, 6 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 1, 11 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 1, 12 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 1, 17 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 1, 18 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 1, 19 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 2, 1 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 2, 4 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 2, 7 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 2, 10 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 2, 13 ) );
         m_SeaMonsterPattern.Add( new KeyValuePair<int, int>( 2, 16 ) );

      }

   #endregion

   /*PROPERTIES*/
   #region


      /// <summary>
      /// Indexer for the completed puzzle
      /// </summary>
      /// <param name="i"></param>
      /// <param name="j"></param>
      /// <returns></returns>
      public JigSaw this[int i, int j]
      {
         get
         {
            if( i > m_CompletedPuzzle.GetLength( 0 ) - 1 || j > m_CompletedPuzzle.GetLength( 1 ) )
               throw new Exception( );
            else
               return m_CompletedPuzzle[i, j];
         }
      }


      #endregion


   /*METHODS*/
   #region


      /// <summary>
      /// Gets the dimension of the completed puzzle in number of jigsaws
      /// </summary>
      /// <returns></returns>
      public (int, int) GetDimensions( )
      {
         return (m_CompletedPuzzle.GetLength( 0 ), m_CompletedPuzzle.GetLength( 1 ));
      }

      /// <summary>
      /// Places the jigsaw piece at the current location. Updates the dictionary of placed and unplaced pieces, and updates the free edges of the piece and the indexes of the piece.
      /// </summary>
      /// <param name="piece"></param>
      /// <param name="rowIdx"></param>
      /// <param name="colIdx"></param>
      private void PlacePiece( JigSaw piece, int rowIdx, int colIdx )
      {
      //If it tries to place something that has already been placed, crash
         if( m_PlacedPieces.ContainsKey( piece.ID ) )
            throw new Exception( );

      //Remove the jigsaw from the unplaced pieces..
         m_UnplacedPieces.Remove( piece.ID );

      //Add the piece to the placed pieces..
         m_PlacedPieces.Add( piece.ID, piece );

      //Update the index of the piece..
         piece.RowIdx = rowIdx;
         piece.ColIdx = colIdx;

      //Check if there is a piece below, if it is, set the free state to false
         JigSaw piecebelow = m_PlacedPieces.Where( x => x.Value.RowIdx == rowIdx + 1 ).Where( y => y.Value.ColIdx == colIdx ).Select( z => z.Value ).ToList( ).FirstOrDefault( );
         if( piecebelow != null )
         {
            piece.SetEdgeState( JigSaw.EDGE.BOTTOM, false );
            piecebelow.SetEdgeState( JigSaw.EDGE.TOP, false );
         }

         JigSaw pieceover = m_PlacedPieces.Where( x => x.Value.RowIdx == rowIdx - 1).Where( y => y.Value.ColIdx == colIdx ).Select( z => z.Value ).ToList( ).FirstOrDefault( );
         if( pieceover != null )
         {
            piece.SetEdgeState( JigSaw.EDGE.TOP, false );
            pieceover.SetEdgeState( JigSaw.EDGE.BOTTOM, false );
         }

         JigSaw right = m_PlacedPieces.Where( x => x.Value.RowIdx == rowIdx ).Where( y => y.Value.ColIdx == colIdx + 1 ).Select( z => z.Value ).ToList( ).FirstOrDefault( );
         if( right != null )
         {
            piece.SetEdgeState( JigSaw.EDGE.RIGHT, false );
            right.SetEdgeState( JigSaw.EDGE.LEFT, false );
         }
         
         JigSaw left = m_PlacedPieces.Where( x => x.Value.RowIdx == rowIdx ).Where( y => y.Value.ColIdx == colIdx - 1).Select( z => z.Value ).ToList( ).FirstOrDefault( );
         if( left != null )
         {
            piece.SetEdgeState( JigSaw.EDGE.LEFT, false );
            left.SetEdgeState( JigSaw.EDGE.RIGHT, false );
         }

      }



      /// <summary>
      /// Solves the jigsaw puzzle
      /// </summary>
      /// <param name="firstGuess"></param>
      /// <returns></returns>
      public bool SolvePuzzle( JigSaw firstGuess )
      {

         Console.WriteLine( "Restarting with piece " + firstGuess.ID + " as first piece.. " );

      //Place an initial piece. Just get the first piece.
         //KeyValuePair<int, JigSaw> first = ( );
         this.PlacePiece( firstGuess, 6, 6 );
         m_Intermediate[6, 6] = '#';

         bool placedSomething = true;

      //Look for a piece to place, and place it loop. When all pieces are placed, remove this
         while( placedSomething )
         {
            placedSomething = false;

         //Collect the possible placements..
            HashSet<KeyValuePair<int, int>> possiblePlacements = GetPossibleNextPlacements( );

         //Loop over the possible placements and look for a tile that fits..
            foreach( KeyValuePair<int, int> placements in possiblePlacements )
            {

            //Get the pieces that it also needs to match with..
               Dictionary<JigSaw, JigSaw.EDGE> matchers = GetPuzzlePiecesToMatchBySide( placements.Key, placements.Value );

               List<JigSaw> candidates = new List<JigSaw>( );
               foreach( KeyValuePair<int, JigSaw> piece in m_UnplacedPieces )
               {
                  bool match = JigSawPuzzle.TransformAndMatch( matchers, piece.Value );
                  if( match )
                     candidates.Add( piece.Value );
               }
               if( candidates.Count == 1 )
               {
                  this.PlacePiece( candidates[0], placements.Key, placements.Value );
                  //Console.WriteLine( "Placed piece " + candidates[0].ID + " ROW " + placements.Key + " COL " + placements.Value );
                  m_Intermediate[placements.Key, placements.Value] = '#';
                  PrintIntermediate( );
                  placedSomething = true;
                  break;
               }
            }


         //Check if this should break...
            if( m_PlacedPieces.Count == m_Pieces.Count )
            {
               Console.WriteLine( "Placed all pieces" );
               CompletePuzzle( );
               string[] completePuzzle = PrintCompletePuzzle( false );
               LookForSeaMonsters( completePuzzle );
               return true;
            }

         }

         return false;

      }


      /// <summary>
      /// Used to print the intermediate state of the tiles
      /// </summary>
      private void PrintIntermediate()
      {
         //return;
         //System.Threading.Thread.Sleep(  );
         //Console.Clear( );
         StringBuilder sb = new StringBuilder( );
         for( int i = 0; i < m_Intermediate.GetLength( 0 ); i++ )
         {
            string thisLine = "";
            for( int j = 0; j < m_Intermediate.GetLength( 1 ); j++ )
            {
               if( m_Intermediate[i, j] == '#' )
                  thisLine += m_Intermediate[i, j];
               else
                  thisLine += ' ';
            }
            sb.Append( thisLine );
            sb.Append( "\n" );
         }

         Console.Write( sb.ToString( ) );
      }

      /// <summary>
      /// Completes the puzzles from the placed pieces..
      /// </summary>
      private void CompletePuzzle()
      {
         //
         int minRowIdx = m_PlacedPieces.Where( x => x.Value.RowIdx == m_PlacedPieces.Select( y => y.Value.RowIdx ).ToList( ).Min( ) ).FirstOrDefault( ).Value.RowIdx;
         int minColIdx = m_PlacedPieces.Where( x => x.Value.ColIdx == m_PlacedPieces.Select( y => y.Value.ColIdx ).ToList( ).Min( ) ).FirstOrDefault( ).Value.ColIdx;
         int rowCounter = 0;
         int colCounter = 0;
         for( int i = minRowIdx; i< minRowIdx + m_CompletedPuzzle.GetLength( 0 ); i++ )
         {
            colCounter = 0;
            for( int j = minColIdx; j< minColIdx + m_CompletedPuzzle.GetLength( 1 ); j++ )
            {
            //Find the correct tile to place..
               JigSaw thisPiece = m_PlacedPieces.Where( x => x.Value.RowIdx == i ).Where( y => y.Value.ColIdx == j ).ToList( ).FirstOrDefault( ).Value;
               m_CompletedPuzzle[rowCounter, colCounter] = thisPiece;
               colCounter++;
            }
            rowCounter++;
         }
      }



      /// <summary>
      /// 
      /// </summary>
      /// <param name="picture"></param>
      private void LookForSeaMonsters( string[] picture )
      {
      //Looks for sea monsters. If it didnt finy any, rotate or flip the picture..
         string[] temporaryPicture = ( string[] ) picture.Clone( );
         int nOfMonsters = 0;
         for( int i = 0; i < 8; i++ )
         {
            nOfMonsters = FoundSeaMonsterInSingle( temporaryPicture );
            if( nOfMonsters > 0 )
               break;
            else
            {
               if( i == 4 )
               {
                  temporaryPicture = Flip( temporaryPicture );
                  temporaryPicture = Rotate( temporaryPicture );
               }
               else
               {
                  temporaryPicture = Rotate( temporaryPicture );
               }
            }
         }

      //Show the picture in the console..
         Console.WriteLine( "Found " + nOfMonsters + " sea monsters in the picture" );

      //Print the picture..
         StringBuilder sb = new StringBuilder( );
         for( int i = 0; i < temporaryPicture.GetLength( 0 ); i++ )
            sb.AppendLine( temporaryPicture[i] );

      //Write to console..
         Console.WriteLine( " " );
         Console.Write( sb.ToString( ) );

      //Count the rest of the hashes that are not sea monsters..
         long nOfHash = 0;
         foreach( string s in temporaryPicture )
            foreach( char c in s.ToCharArray() )
               if( c == '#' )
                  nOfHash++;

      //Write the answer
         Console.WriteLine( " " );
         Console.WriteLine( "Answer for part 2: " + nOfHash );
      }


      /// <summary>
      /// Look for a sea monster in a single picture frame
      /// </summary>
      /// <param name="picture"></param>
      /// <returns></returns>
      private int FoundSeaMonsterInSingle( string[] picture )
      {
      //Convert to char array.. loop throug indexes
         char[,] pictureArray = new char[picture.GetLength( 0 ), picture.GetLength( 0 ) ];
         for( int i = 0; i < picture.GetLength(0); i++ )
         {
            char[] thisLine = picture[i].ToCharArray( );
            for( int j = 0; j < picture.GetLength(0); j++ )
               pictureArray[i, j] = thisLine[j];
         }

         int nOfSeaMonstersFound = 0;
      //Loop through all the indexes and chech for a seamonser
         for( int i = 0; i < picture.GetLength(0); i++ )
         {
            for( int j = 0; j < picture.GetLength(0); j++ )
            {
               bool foundOne = FindSeamonsterInSingleAndReplace( pictureArray, i, j );
               if( foundOne )
                  nOfSeaMonstersFound++;
            }
         }

      //If it found some monsters, change the picture array back for prints.
         if( nOfSeaMonstersFound > 0 )
         {
            for( int i = 0; i < pictureArray.GetLength( 0 ); i++ )
            {
               string thisLine = "";
               for( int j = 0; j < pictureArray.GetLength( 1 ); j++ )
                  thisLine += pictureArray[i, j];

               picture[i] = thisLine;
            }
         }

      //return the number of seat monsters
         return nOfSeaMonstersFound;
      }



      /// <summary>
      /// Look for a sea monster pattern in this orientation
      /// </summary>
      /// <param name="pictureArray"></param>
      /// <param name="rowIdx"></param>
      /// <param name="colIdx"></param>
      /// <returns></returns>
      private bool FindSeamonsterInSingleAndReplace( char[,] pictureArray, int rowIdx, int colIdx )
      {
      //Check the hash..
         bool foundIt = true;
         foreach( KeyValuePair<int, int> kvp in m_SeaMonsterPattern )
         {
            try
            {
               char c = pictureArray[rowIdx + kvp.Key, colIdx + kvp.Value];
               foundIt &= Hash( c );
               if( !foundIt )
                  return false;
            }
            catch( IndexOutOfRangeException )
            {
               return false;
            }

         }

      //If the code reached this point, a sea monster was found. Replace the characters in the picture array with zeros
         foreach( KeyValuePair<int, int> kvp in m_SeaMonsterPattern )
            pictureArray[rowIdx + kvp.Key, colIdx + kvp.Value] = '0';

         return true;

      }

      /// <summary>
      /// Checks if the character is a hash..
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      private bool Hash( char c )
      {
         if( c == '#' )
            return true;
         else
            return false;
      }
      
      /// <summary>
      /// Get complete puzzle as a string array...
      /// </summary>
      /// <param name="includeBorders"></param>
      /// <returns></returns>
      public string[] PrintCompletePuzzle( bool includeBorders )
      {
         List<string> picture = new List<string>( );
         for( int i = 0; i < m_CompletedPuzzle.GetLength( 0 ); i++ )
         {
            List<string[]> tilesInOneRow = new List<string[]>( );
            
            for( int j = 0; j < m_CompletedPuzzle.GetLength( 1 ); j++ )
               tilesInOneRow.Add( m_CompletedPuzzle[i, j].GetStringArray( includeBorders) );
            for( int row = 0; row<tilesInOneRow[0].GetLength( 0 ); row++ )
            {
               string thisRow = "";
               for( int col = 0; col<tilesInOneRow.Count; col++ )
               {
                  thisRow += tilesInOneRow[col][row];
               }
               picture.Add( thisRow );
            }
         }
         return picture.ToArray( );
      }


   #endregion

   /*STATIC METHODS*/
   #region


      /// <summary>
      /// Gets a dictionary of tiles that is placed around the input location.
      /// </summary>
      /// <param name="rowIdx"></param>
      /// <param name="colIdx"></param>
      /// <returns></returns>
      public Dictionary<JigSaw, JigSaw.EDGE> GetPuzzlePiecesToMatchBySide( int rowIdx, int colIdx )
      {
      //declare returning dictionary. This is the jigsaws to try to match with for the given position. it also follows what side of the placed piece we should match with,
      // Candidates should match 
         Dictionary<JigSaw, JigSaw.EDGE> retDir = new Dictionary<JigSaw, JigSaw.EDGE>( );

      //Try to find some of the neighbouring jigsaws..
         //The one below that s
         JigSaw addPiece = m_PlacedPieces.Where( x => x.Value.ColIdx == colIdx && x.Value.RowIdx == rowIdx - 1 ).Select( y => y.Value ) .FirstOrDefault( );
         if( addPiece != null )
            retDir.Add( addPiece, JigSaw.EDGE.BOTTOM );

         //Bottom piece
         addPiece = m_PlacedPieces.Where( x => x.Value.ColIdx == colIdx && x.Value.RowIdx == rowIdx + 1 ).Select( y => y.Value ) .FirstOrDefault( );
         if( addPiece != null )
            retDir.Add( addPiece, JigSaw.EDGE.TOP );

         //left piece
         addPiece = m_PlacedPieces.Where( x => x.Value.ColIdx == colIdx - 1 && x.Value.RowIdx == rowIdx ).Select( y => y.Value ) .FirstOrDefault( );
         if( addPiece != null )
            retDir.Add( addPiece, JigSaw.EDGE.RIGHT );

         //right piece
         addPiece = m_PlacedPieces.Where( x => x.Value.ColIdx == colIdx + 1 && x.Value.RowIdx == rowIdx ).Select( y => y.Value ) .FirstOrDefault( );
         if( addPiece != null )
            retDir.Add( addPiece, JigSaw.EDGE.LEFT );

      //Return the completed dictionary
         return retDir;
      }


      /// <summary>
      /// A method that tries to match the candidate in the given spot. The input dictionary is the placed pieces on all of the sides.
      /// </summary>
      /// <param name="placedPiecesToMatchWith"></param>
      /// <param name="candidate"></param>
      /// <returns></returns>
      public static bool TransformAndMatch( Dictionary<JigSaw, JigSaw.EDGE> placedPiecesToMatchWith, JigSaw candidate )
      {
      //Validation
         if( placedPiecesToMatchWith.Count < 0 || placedPiecesToMatchWith.Count > 4 )
            throw new Exception( );

      //Loop through the transformed sides and try to match with the placed pieces..
         for( int i = 0; i < 8; i++ )
         {
            bool thisOrientationMatch = true;

            foreach( KeyValuePair<JigSaw, JigSaw.EDGE> kvp in placedPiecesToMatchWith )
            {
               bool thisMatches = kvp.Key.EdgesMatch( candidate, kvp.Value );
               if( !thisMatches )
               {
                  thisOrientationMatch = false;
                  break;
               }
            }
            if( !thisOrientationMatch )
            {
            //This orientation did not match. Transform it, and check the next state..
               candidate.TransformToNextState( );
               continue;
            }
            else
            {
            //The orientation matched for all the pieces adjacent. Return true..
               return true;
            }
         }

      //If the code reached this point, its not a match. Return false.
         return false;

      }


      /// <summary>
      /// Returns a list of possible placements for the next jigsaw..
      /// </summary>
      /// <returns></returns>
      public HashSet<KeyValuePair<int, int>> GetPossibleNextPlacements( )
      {
      //Declare the list of placements..
         HashSet<KeyValuePair<int, int>> possiblePlacements = new HashSet<KeyValuePair<int, int>>( );

      //Loop through all the placed pieces and add the placements of the free spots to the list
         foreach( KeyValuePair<int, JigSaw> kvp in m_PlacedPieces )
         {
            List<KeyValuePair<int, int>> freeNeighbours = kvp.Value.GetIndexOfFreeSpots( );
            foreach( KeyValuePair<int, int> kvp2 in freeNeighbours )
               possiblePlacements.Add( kvp2 );
         }
      //Return the hashset..
         return possiblePlacements;
      }



      /// <summary>
      /// Returns a new jigsaw puzzle piece like this just flipped at the x axis
      /// </summary>
      /// <returns></returns>
      private static string[] Flip( string[] picture )
      {
         //Flips the jigsaw across the horizontal axis.
         string[] reversed = new string[picture.GetLength( 0 )];
         for( int i = 0; i < picture.GetLength( 0 ); i++ )
         {
            char[] thisReversed = picture[i].ToCharArray( ).Reverse( ).ToArray( );
            string newString = new string( thisReversed );
            reversed[i] = newString;
         }

         return reversed;
      }


      /// <summary>
      /// Rotates the current state of the jigsaw 90 degrees Clockwise rotation
      /// </summary>
      private static string[] Rotate( string[] picture )
      {

         int dim = picture.GetLength( 0 );

         //Create char array..
         char[,] tempArray = new char[dim, dim];

         for( int i = 0; i < dim; i++ )
         {
            char[] otherTemp = picture[i].ToCharArray( );
            for( int j = 0; j < dim; j++ )
               tempArray[i, j] = otherTemp[j];
         }

         // Traverse each cycle
         for( int i = 0; i < dim / 2; i++ )
         {
            for( int j = i; j < dim - i - 1; j++ )
            {
               // Swap elements of each cycle
               char temp = tempArray[i, j];
               tempArray[i, j] = tempArray[dim - 1 - j, i];
               tempArray[dim - 1 - j, i] = tempArray[dim - 1 - i, dim - 1 - j];
               tempArray[dim - 1 - i, dim - 1 - j] = tempArray[j, dim - 1 - i];
               tempArray[j, dim - 1 - i] = temp;
            }
         }

      //Create a new string array..
         string[] retArr = new string[dim];
         for( int i = 0; i < dim; i++ )
         {
            char[] temp2 = new char[dim];
            for( int j = 0; j < dim; j++ )
               temp2[j] = tempArray[i, j];

            string thisLine = new string( temp2 );
            retArr[i] = thisLine;
         }

      //Return the array..
         return retArr;


      }





   #endregion


   }
}
