using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Classes
{
   public class DiskFragmenter
   {


      public class DiskFile
      {

         public DiskFile( int id )
         {
            this.FileId = id;
         }

         public DiskFile( DiskFile oldFile )
         {
            this.FileId = oldFile.FileId;
         }

         public int FileId { get; set; }
      }

      public DiskFile[] m_Disk = null;

      public DiskFragmenter( string inp )
      {
      //Create all the disk files and add them to the disk..
         List<DiskFile> files = new List<DiskFile>( );
         int currId = 0;
         int currentPos = 0;
         for( int i = 0; i<inp.Length; i= i+2 )
         {
         //Create the file..
            DiskFile f = new DiskFile( currId++ );

            int startLoc = currentPos;
            int blockSize = int.Parse( inp[i].ToString( ) );
            int endLoc = currentPos + int.Parse( inp[i].ToString( ) );

            for( int j = 0; j < blockSize; j++ )
               files.Add( new DiskFile( f ) );

            int freeSpace = 0;
            if( i< inp.Length-1 )
            {
               freeSpace = int.Parse( inp[i+1].ToString( ) );
               for( int j = 0; j<freeSpace; j++ )
                  files.Add( null );

            }

            currentPos = currentPos + blockSize + freeSpace;
         }

         m_Disk = files.ToArray( );

      }


      public int FindFirstFreeSpaceOnDisk( )
      {
         for( int i = 0; i<m_Disk.Length; i++ )
            if( m_Disk[i] == null )
               return i;

         return m_Disk.Length;
      }

      public bool FindFirstFreeSpaceIntervalOnDisk( int length, out int startIdx )
      {
         int counter = 0;
         startIdx = -1;
         for( int i = 0; i<m_Disk.Length; i++ )
         {
            if( m_Disk[i] == null )
               counter++;
            else
               counter = 0;

            if( counter == length )
            {
               startIdx = i - counter + 1;
               return true;
            }
         }

      //If the code reached this point, there are no free spaces of that length..
         return false;

      }

      public BigInteger P1( )
      {
      //Defragment the memory block..
         bool movedSomething = false;
         do
         {

         //Find the first available free space from the left..
            long freeIdx = FindFirstFreeSpaceOnDisk( );

         //Find the last entry on disk..
            DiskFile toMove = null;
            int fileIdx = 0;
            for( int i = m_Disk.Length - 1; i >= 0; i-- )
            {
               if( m_Disk[i] != null )
               {
                  toMove = m_Disk[i];
                  fileIdx = i;
                  break;
               }
            }

            if( freeIdx < fileIdx )
            {
               movedSomething = true;
               m_Disk[freeIdx] = toMove;
               m_Disk[fileIdx] = null;
            }
            else
               movedSomething = false;

         } while( movedSomething == true );


      //Calculate checksum of block
         BigInteger checkSum = 0;

         for( int i = 0; i<m_Disk.Length; i++ )
            if( ( m_Disk[i] != null ) )
               checkSum += m_Disk[i].FileId*i;

         return checkSum;
      }

      public BigInteger P2( )
      {
         bool movedSomething = false;
         HashSet<int> movedIds = new HashSet<int>();
         do
         {
         //Reset moved counter..
            movedSomething = false;

         //Find the files to move. First fint the correct ID.
            for( int i = m_Disk.Length - 1; i>= 0; i-- )
            {
            //Find the first that havent been tried to be moved..
               if( m_Disk[i] != null && !movedIds.Contains( m_Disk[i].FileId ) )
               {

               //Collect the ones with the same id..
                  int thisId = m_Disk[i].FileId;
                  int thisIdx = i;
                  HashSet<int> idxToMove = new HashSet<int>( );
                  while( true )
                  {
                     if( thisIdx >= 0 && m_Disk[thisIdx] != null && m_Disk[thisIdx].FileId == m_Disk[i].FileId )
                     {
                        idxToMove.Add( thisIdx );
                        thisIdx--;
                     }
                     else
                        break;
                  }

               //Find the first available space..
                  if( FindFirstFreeSpaceIntervalOnDisk( idxToMove.Count, out int emptyStartIdx ) )
                  {

                  //Check if this index is lower that the current lowest idx..
                     if( emptyStartIdx < idxToMove.Min( ) )
                     {
                        int nToMove = idxToMove.Count;
                        for( int k = 0; k<nToMove; k++ )
                        {
                           int lastIdx = idxToMove.Last( );
                           m_Disk[emptyStartIdx+k] = m_Disk[lastIdx];
                           m_Disk[lastIdx] = null;
                           idxToMove.Remove( lastIdx );
                        }

                     }

                  }

               //We tried to move this fileID. Add to the moved IDs hash set so we skip it next time..
                  movedIds.Add( thisId );

               //We tried to move something, set the flag to true..
                  movedSomething = true;
                  break;
               }
            }

         } while( movedSomething == true );


      //Calculate checksum of block
         BigInteger checkSum = 0;
         for( int i = 0; i<m_Disk.Length; i++ )
            if( ( m_Disk[i] != null ) )
               checkSum += m_Disk[i].FileId*i;
         return checkSum;
      }


      public void PrintState( )
      {
         StringBuilder sb = new StringBuilder();
         for( int i = 0; i<m_Disk.Length; i++ )
         {
            if( ( m_Disk[i] != null ) )
               sb.Append( m_Disk[i].FileId.ToString( ) );
            else
               sb.Append( "." );
         }
         Console.WriteLine( sb.ToString( ) );
      }




   }
}
