using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class DirectoryFolder
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected string m_Name = null;
      protected List<DirectoryFolder> m_Directories = new List<DirectoryFolder>( );
      protected List<DirectoryFile> m_Files = new List<DirectoryFile>( );
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor. Creates a folder with a name..
      /// </summary>
      /// <param name="name"></param>
      public DirectoryFolder( string name )
      {
         m_Name = name;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public List<DirectoryFolder> SubFolders { get { return m_Directories; } }
      public List<DirectoryFile> Files { get { return m_Files; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Gets the size of the folder..
      /// </summary>
      /// <returns></returns>
      public long GetSizeOfFolder( )
      {
         long totSize = 0;

      //Add the size of the files in this folder..
         foreach( DirectoryFile file in m_Files )
            totSize += file.Size;

      //Loop over all subdirectories..
         foreach( DirectoryFolder folder in m_Directories )
            totSize += folder.GetSizeOfFolder( );

      //Return the total size..
         return totSize;
      }
   #endregion


   }
}
