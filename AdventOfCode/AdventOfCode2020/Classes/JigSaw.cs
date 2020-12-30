using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{

   /// <summary>
   /// A single jigsaw puzzle piece in the AoC DEC 20
   /// </summary>
   public class JigSaw
   {


   /*ENUMS*/
   #region

      /// <summary>
      /// Represents the eight different states a jigsaw can be in. Used to keep track of what state to return to next.
      /// </summary>
      public enum JIGSAWSTATE
      {
         NONE = 0,
         ROT90 = 1,
         ROT180 = 2,
         ROT270 = 3,
         FLIPPED = 4,
         FLIPPEDROT90 = 5,
         FLIPPETROT180 = 6,
         FLIPPEDROT270 = 7
      }

      /// <summary>
      /// References a piece edge when askign for the jigsaw border..
      /// </summary>
      public enum EDGE
      {
         TOP = 0,
         RIGHT = 1,
         BOTTOM = 2,
         LEFT = 3
      }



   #endregion




   /*MEMBERS*/
   #region
      protected int m_ID;
      protected JIGSAWSTATE m_State;
      protected bool m_IsPlaced = false;
      protected Dictionary<EDGE, bool> m_FreeEdges; 

      protected char[,] m_PictureWithFrame = new char[10, 10];

      protected int m_RowIdx = -1;        // The row index of the puzzle piece. Used by the JigSawPuzzle-class to keep track of what other pieces to check matches for..
      protected int m_ColIdx = -1;        // The column index of the puzzle piece.
   #endregion



   /*CONSTRUCTORS*/
   #region
      /// <summary>
      /// Creates a jigsaw from the string input.
      /// </summary>
      /// <param name="inp"></param>
      public JigSaw( string[] inp )
      {
         m_State = JIGSAWSTATE.NONE;
         string[] s = inp[0].Split( new char[] { ' ' } );
         string idString = s[1].Substring( 0, s[1].Length - 1 );
         m_ID = int.Parse( idString );

         m_FreeEdges = new Dictionary<EDGE, bool>( );
         m_FreeEdges.Add( EDGE.TOP, true );
         m_FreeEdges.Add( EDGE.BOTTOM, true );
         m_FreeEdges.Add( EDGE.LEFT, true );
         m_FreeEdges.Add( EDGE.RIGHT, true );
         for( int i = 1; i < inp.Length; i++ )
         {
            char[] thisArr = inp[i].ToCharArray( );
            for( int j = 0; j < thisArr.Length ; j++ )
               m_PictureWithFrame[i - 1, j] = thisArr[j];
         }
      }

   #endregion


   /*PROPERTIES*/
   #region

      /// <summary>
      /// Gets or sets the column index of this puzzle piece
      /// </summary>
      public int ColIdx
      {
         get
         {
            return m_ColIdx; ;
         }
         set
         {
            m_ColIdx = value;
         }
      }

      /// <summary>
      /// Gets or sets the row index of this puzzzle piece
      /// </summary>
      public int RowIdx
      {
         get
         {
            return m_RowIdx;
         }
         set
         {
            m_RowIdx = value;
         }
      }


      /// <summary>
      /// Gets the ID.
      /// </summary>
      public int ID => m_ID;


   #endregion


   /*METHODS*/
   #region

      /// <summary>
      /// Returns a list of integer pairs for available adjacent spots for this jigsaw piece.
      /// </summary>
      /// <returns></returns>
      public List<KeyValuePair<int,int>> GetIndexOfFreeSpots()
      {
         List<KeyValuePair<int, int>> freeSpots = new List<KeyValuePair<int, int>>( );
         foreach( KeyValuePair<EDGE, bool> kvp in m_FreeEdges )
         {
            if( kvp.Value )
            {
               if( kvp.Key == EDGE.TOP )
                  freeSpots.Add( new KeyValuePair<int, int>( m_RowIdx - 1, m_ColIdx ) );
               else if( kvp.Key == EDGE.BOTTOM )
                  freeSpots.Add( new KeyValuePair<int, int>( m_RowIdx + 1, m_ColIdx ) );
               else if( kvp.Key == EDGE.LEFT )
                  freeSpots.Add( new KeyValuePair<int, int>( m_RowIdx, m_ColIdx - 1 ) );
               else if( kvp.Key == EDGE.RIGHT )
                  freeSpots.Add( new KeyValuePair<int, int>( m_RowIdx, m_ColIdx + 1 ) );
               else
                  throw new Exception( );
            }
         }
         return freeSpots;
      }

      /// <summary>
      /// Gets the string array for the current state of the picture
      /// </summary>
      /// <returns></returns>
      public string[ ] GetStringArray( )
      {
         string[ ] ret = new string[m_PictureWithFrame.GetLength( 0 )];
         for( int i = 0; i < m_PictureWithFrame.GetLength( 0 ); i++ )
         {
            string thisLine = "";
            for( int j = 0; j < m_PictureWithFrame.GetLength( 1 ); j++ )
               thisLine += m_PictureWithFrame[i, j];

            ret[i] = thisLine;
         }
         return ret;
      }



      /// <summary>
      /// Sets the edgestate for this puzzle piece..
      /// </summary>
      /// <param name="edge"></param>
      /// <param name="state"></param>
      public void SetEdgeState( EDGE edge, bool state )
      {
         m_FreeEdges[ edge ] = state;
      }


      /// <summary>
      /// Returns a new jigsaw puzzle piece like this just flipped at the x axis
      /// </summary>
      /// <returns></returns>
      private void Flip()
      {
      //Flips the jigsaw across the horizontal axis.
         string[ ] strings = this.GetStringArray( );
         for( int i = 0; i < m_PictureWithFrame.GetLength( 0 ); i++ )
         {
            char[ ] thisReversed = strings[i].ToCharArray( ).Reverse( ).ToArray( );
            for( int j = 0; j < m_PictureWithFrame.GetLength( 1 ); j++ )
               m_PictureWithFrame[i, j] = thisReversed[j];
         }
      }


      /// <summary>
      /// Rotates the current state of the jigsaw 90 degrees Clockwise rotation
      /// </summary>
      private void Rotate( )
      {
         int dim = m_PictureWithFrame.GetLength( 0 );
         // Traverse each cycle
         for( int i = 0; i < dim / 2; i++ )
         {
            for( int j = i; j < dim - i - 1; j++ )
            {
               // Swap elements of each cycle
               char temp = m_PictureWithFrame[i, j];
               m_PictureWithFrame[i, j] = m_PictureWithFrame[dim - 1 - j, i];
               m_PictureWithFrame[dim - 1 - j, i] = m_PictureWithFrame[dim - 1 - i, dim - 1 - j];
               m_PictureWithFrame[dim - 1 - i, dim - 1 - j] = m_PictureWithFrame[j, dim - 1 - i];
               m_PictureWithFrame[j, dim - 1 - i] = temp;
            }
         }
      }



      /// <summary>
      /// Returns the requested edge of this piece. If it is vertical, it is written from top to bottom. Horizontal left to right
      /// </summary>
      /// <param name="edge"></param>
      /// <returns></returns>
      public string GetEdge( EDGE edge )
      {
         string ret = "";
         if( edge == EDGE.TOP )
         {
            for( int i = 0; i < m_PictureWithFrame.GetLength( 1 ); i++ )
               ret += m_PictureWithFrame[0, i];
         }
         else if( edge == EDGE.BOTTOM )
         {
            for( int i = 0; i < m_PictureWithFrame.GetLength( 1 ); i++ )
               ret += m_PictureWithFrame[m_PictureWithFrame.GetLength( 0 ) - 1, i];
         }
         else if( edge == EDGE.LEFT )
         {
            for( int i = 0; i < m_PictureWithFrame.GetLength( 0 ); i++ )
               ret += m_PictureWithFrame[i, 0];
         }
         else if( edge == EDGE.RIGHT )
         {
            for( int i = 0; i < m_PictureWithFrame.GetLength( 0 ); i++ )
               ret += m_PictureWithFrame[i, m_PictureWithFrame.GetLength( 1 ) - 1];
         }
         else
            throw new Exception( );

      //Return the completed string..
         return ret;
      }

      /// <summary>
      /// Checks wheter the input piece edge matches with this piece input edge, such that if this edge is TOP, it tries to match with the other edges bottom row..
      /// </summary>
      /// <param name="otherPiece"></param>
      /// <param name="thisPieceEdge"></param>
      /// <returns></returns>
      public bool EdgesMatch( JigSaw otherPiece, EDGE thisPieceEdge )
      {

         string edgeOfThis = null;
         string edgeOfOther = null;
         if( thisPieceEdge == EDGE.TOP )
         {
            edgeOfThis = this.GetEdge( EDGE.TOP );
            edgeOfOther = otherPiece.GetEdge( EDGE.BOTTOM );
         }
         else if( thisPieceEdge == EDGE.RIGHT )
         {
            edgeOfThis = this.GetEdge( EDGE.RIGHT );
            edgeOfOther = otherPiece.GetEdge( EDGE.LEFT );
         }
         else if( thisPieceEdge == EDGE.BOTTOM )
         {
            edgeOfThis = this.GetEdge( EDGE.BOTTOM );
            edgeOfOther = otherPiece.GetEdge( EDGE.TOP );
         }
         else if( thisPieceEdge == EDGE.LEFT )
         {
            edgeOfThis = this.GetEdge( EDGE.LEFT );
            edgeOfOther = otherPiece.GetEdge( EDGE.RIGHT );
         }
         return edgeOfThis == edgeOfOther;
      }


      /// <summary>
      /// Transforms the jigsaw to the next state.
      /// </summary>
      public void TransformToNextState( )
      {
         if( m_State == JIGSAWSTATE.NONE )
         {
            m_State = JIGSAWSTATE.ROT90;
            Rotate( );
         }
         else if( m_State == JIGSAWSTATE.ROT90 )
         {
            m_State = JIGSAWSTATE.ROT180;
            Rotate( );
         }
         else if( m_State == JIGSAWSTATE.ROT180 )
         {
            m_State = JIGSAWSTATE.ROT270;
            Rotate( );
         }
         else if( m_State == JIGSAWSTATE.ROT270 )
         {
            m_State = JIGSAWSTATE.FLIPPED;
            Rotate( );
            Flip( );
         }
         else if( m_State == JIGSAWSTATE.FLIPPED )
         {
            m_State = JIGSAWSTATE.FLIPPEDROT90;
            Rotate( );
         }
         else if( m_State == JIGSAWSTATE.FLIPPEDROT90 )
         {
            m_State = JIGSAWSTATE.FLIPPETROT180;
            Rotate( );
         }
         else if( m_State == JIGSAWSTATE.FLIPPETROT180 )
         {
            m_State = JIGSAWSTATE.FLIPPEDROT270;
            Rotate( );
         }
         else if( m_State == JIGSAWSTATE.FLIPPEDROT270 )
         {
            m_State = JIGSAWSTATE.NONE;
            Rotate( );
            Flip( );
         }
         else
         {
            throw new Exception( );
         }
      }


      /// <summary>
      /// Prints the full puzzle piece to the console
      /// </summary>
      /// <returns></returns>
      public string Print( )
      {
         StringBuilder sb = new StringBuilder( );
         for( int i = 0; i < m_PictureWithFrame.GetLength( 0 ) ; i++ )
         {
            string thisLine = "";
            for( int j = 0; j < m_PictureWithFrame.GetLength( 1 ); j++ )
            {
               thisLine += m_PictureWithFrame[i, j];
            }
            thisLine += "\n";
            sb.Append( thisLine );
         }

         return sb.ToString( );
      }



      /// <summary>
      /// Gets the string array for this puzzle piece.
      /// </summary>
      /// <param name="borders"></param>
      /// <returns></returns>
      public string[] GetStringArray( bool borders )
      {

         int startIdx = 0;
         int endIdx = m_PictureWithFrame.GetLength( 0 ) - 1;
         if( !borders )
         {
            startIdx = 1;
            endIdx = m_PictureWithFrame.GetLength( 0 ) - 2;
         }
         int arrDim = endIdx - startIdx + 1;
         string[] retArr = new string[arrDim];

         int arrIdx = 0;
         for( int i = startIdx; i <= endIdx; i++ )
         {

            string thisLine = "";
            for( int j = startIdx; j <= endIdx; j++ )
               thisLine += m_PictureWithFrame[i, j];
            retArr[arrIdx++] = thisLine;
         }
         return retArr;
      }


   #endregion

   /*STATIC METHODS*/
   #region







   #endregion



   }
}
