using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{


   /// <summary>
   /// Advent of code dec14. Bitmasks
   /// </summary>
   public class DockingSoftware
   {


   /*MEMBERS*/
   #region
      Dictionary<long, long> m_Memory = new Dictionary<long, long>( );
      List<DockInstruction> m_Instructions = new List<DockInstruction>( );
      protected bool m_MaskAdress = false;                                       //A flag that is set if this is part two.. Applies the masks on the memory locations and not on the values themselves

   #endregion

   /*CONSTRUCTORS*/
   #region
      

      /// <summary>
      /// Default constructor. Creates the docking software from its instructions
      /// </summary>
      /// <param name="arr"></param>
      public DockingSoftware( string[ ] arr, bool maskAdress )
      {
         m_MaskAdress = maskAdress;

      //Create the instructions..
         DockMask prevMask = null;
         foreach( string line in arr )
         {
            string branch = line.Substring( 0, 3 );
            if( branch == "mas" )
               prevMask = new DockMask( line, maskAdress );
            else if( branch == "mem" )
               m_Instructions.Add( new DockInstruction( line, prevMask, maskAdress ) );
            else
               throw new Exception( );
         }
      }

      #endregion

   /*PROERTIES*/
      #region

      public List<DockInstruction> Instructions => m_Instructions;

      #endregion


   /*METHODS*/
      #region

      /// <summary>
      /// Runs the software with the instructions provided
      /// </summary>
      public void RunSoftware( )
      {
         foreach( DockInstruction ins in m_Instructions )
            ins.CarryOutInstruction( m_Memory );
      }

      /// <summary>
      /// Returns the total sum of all the values in the memory 
      /// </summary>
      /// <returns></returns>
      public long GetTotalSumOfMemoryEntries( )
      {
         long sum = m_Memory.Select( x => x.Value ).ToList( ).Sum( );
         return sum;
      }


      #endregion


   }




}
