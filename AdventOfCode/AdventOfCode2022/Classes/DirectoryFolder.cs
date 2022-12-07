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
      protected Dictionary<string,DirectoryFolder> m_Directories = new Dictionary<string,DirectoryFolder>();
      protected Dictionary<string,DirectoryFile> m_Files = new Dictionary<string, DirectoryFile>( );
      protected DirectoryFolder m_Parent = null;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor. Creates a folder with a name..
      /// </summary>
      /// <param name="name"></param>
      public DirectoryFolder( string name, DirectoryFolder parent )
      {
         m_Name = name;
         m_Parent = parent;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public Dictionary<string,DirectoryFolder> SubFolders { get { return m_Directories; } }
      public Dictionary<string,DirectoryFile> Files { get { return m_Files; } }
      public DirectoryFolder Parent { get { return m_Parent; } }
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
         foreach( KeyValuePair<string,DirectoryFile> file in m_Files )
            totSize += file.Value.Size;

      //Loop over all subdirectories..
         foreach( KeyValuePair<string,DirectoryFolder> folder in m_Directories )
            totSize += folder.Value.GetSizeOfFolder( );

      //Return the total size..
         return totSize;
      }

      /// <summary>
      /// A method that collects folders of at most the size limit..
      /// </summary>
      /// <param name="largeFolders">A list of the large folders..</param>
      /// <param name="sizeLimit"></param>
      public void CollectLargeDirectories( List<DirectoryFolder> largeFolders, long sizeLimit = 100000 )
      {
      //Get the size of this folder..
         long thisFolderSize = this.GetSizeOfFolder( );

      //Check if this folder should be added..
         if( thisFolderSize > sizeLimit )
            largeFolders.Add( this );

      //Loop over all subdirectories and add the folder if it is large enough..
         foreach( KeyValuePair<string,DirectoryFolder> kvp in m_Directories )
            kvp.Value.CollectLargeDirectories( largeFolders, sizeLimit );
      }

      /// <summary>
      /// Gets a subfolder of this folder..
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public DirectoryFolder GetFolder( string name )
      {
         if( !m_Directories.ContainsKey( name ) )
            return null;
         else
            return m_Directories[name];
      }

   #endregion


   }
}
