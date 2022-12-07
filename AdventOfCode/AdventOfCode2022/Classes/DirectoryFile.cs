using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class DirectoryFile
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
      protected long m_Size = -1;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public DirectoryFile( string name, long size )
      {
         m_Size = size;
         m_Name = name;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public string Name { get { return m_Name; } }
      public long Size { get { return m_Size; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   /*METHODS*/
   #region
   #endregion


   }
}
