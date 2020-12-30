using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{

   /// <summary>
   /// A single instruction for the docking software in dec14. Can carry out itself on a memory dictionary
   /// </summary>
   public class DockInstruction
   {

      /*ENUMS*/
      #region
      public enum DOCKOPERATION
      {
         MEMORYWRITE,
      }
      #endregion


      /*MEMBERS*/
      #region

      protected DockMask m_Mask = null;
      protected DOCKOPERATION m_Operation;
      protected long m_Adress = -1;
      protected long m_Value = -1;
      protected bool m_MaskAdress;

      #endregion


      /*CONSTRUCTORS*/
      #region


      /// <summary>
      /// Creates a dock instruction from a string. The mask is sent as an input
      /// </summary>
      /// <param name="ins"></param>
      /// <param name="mask"></param>
      public DockInstruction( string ins, DockMask mask, bool maskAdress )
      {

      //First, set the mask. This is created on the outside
         m_Mask = mask;

      //Set the bool that decides if the adress list should be masked as well
         m_MaskAdress = maskAdress;

      //Split the string..
         string[ ] split = ins.Split( new char[ ] { ' ' } );

      //Set operation
         string operationString = split[0].Substring( 0, 3 );
         if( operationString == "mem" )
            m_Operation = DOCKOPERATION.MEMORYWRITE;
         else
            throw new Exception( );

      //Set memory adress.
         string adressString = split[0].Substring( 4, split[0].Length - 5 );
         long adress;
         bool parsedAdd = long.TryParse( adressString, out adress );
         if( !parsedAdd )
            throw new Exception( );
         m_Adress = adress;

      //Set the value..
         long val;
         bool parsedVal = long.TryParse( split[2], out val );
         if( !parsedVal )
            throw new Exception( );
         m_Value = val;

      }
      #endregion

      /*PROPERTIES*/
      #region

      public DockMask Mask => m_Mask;
      public DOCKOPERATION Operation => m_Operation;
      public long Adress => m_Adress;
      public long Value => m_Value;

      #endregion


   /*METHODS*/
      #region

      /// <summary>
      /// Carries out this instruction on the memory dictionary.
      /// </summary>
      /// <param name="memory"></param>
      public void CarryOutInstruction( Dictionary<long, long> memory )
      {
         if( m_Operation != DOCKOPERATION.MEMORYWRITE )
            throw new Exception( );

      //Flag for part 1
         if( !m_MaskAdress )
         {
         //Since we\re only writing, remove the stuff thats already there
            if( memory.ContainsKey( m_Adress ) )
               memory.Remove( m_Adress );

         //Create the string that should be written to memory..
            string binaryNumber = Convert.ToString( m_Value, 2 );

         //Mask it with this mask..
            string masked = m_Mask.UseMaskOnString( binaryNumber );

         //Convert back to an normal long
            long value = Convert.ToInt64( masked, 2 );

         //Add the masked integer to memory..
            memory.Add( m_Adress, value );
         }
         else
         {
            //For part 2
            //This part does not modify the values with the mask, instead it collects all the possible adresses from the masked string and writes the current value to that adress.

         //Create a binary representation of the adress..
            string binaryAdress = Convert.ToString( m_Adress, 2 );

         //Get the list of adresses from the mask..
            List<long> listOfAdresses = m_Mask.GetAllAdressesFromMask( binaryAdress );

         //Loop over all the adress positions and overwrite the values here..
            foreach( long l in listOfAdresses )
            {
            //Remove it first..
               if( memory.ContainsKey( l ) )
                  memory.Remove( l );

            //Add new value to the memory.
               memory.Add( l, m_Value );
            }


         }

      }

   #endregion

   }


}
