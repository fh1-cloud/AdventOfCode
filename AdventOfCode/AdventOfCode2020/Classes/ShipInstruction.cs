using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class ShipInstruction
   {


      public enum SHIPINSTRUCTION
      {
         MOVENORTH,
         MOVESOUTH,
         MOVEEAST,
         MOVEWEST,
         MOVEFORWARD,
         TURNLEFT,
         TURNRIGHT,
      }


      protected double m_Value;
      protected SHIPINSTRUCTION m_Instruction;

      public ShipInstruction( string line )
      {
         if( line[0] == 'N' )
            m_Instruction = SHIPINSTRUCTION.MOVENORTH;
         else if( line[0] == 'S' )
            m_Instruction = SHIPINSTRUCTION.MOVESOUTH;
         else if( line[0] == 'E' )
            m_Instruction = SHIPINSTRUCTION.MOVEEAST;
         else if( line[0] == 'W' )
            m_Instruction = SHIPINSTRUCTION.MOVEWEST;
         else if( line[0] == 'L' )
            m_Instruction = SHIPINSTRUCTION.TURNLEFT;
         else if( line[0] == 'R' )
            m_Instruction = SHIPINSTRUCTION.TURNRIGHT;
         else if( line[0] == 'F' )
            m_Instruction = SHIPINSTRUCTION.MOVEFORWARD;
         else
            throw new Exception( );

         //Parse the value..
         string sub = line.Substring( 1 );
         double val;
         bool succ = double.TryParse( sub, out val );
         if( !succ )
            throw new Exception( );
         m_Value = val;
      }

      public double Value => m_Value;
      public SHIPINSTRUCTION Instruction => m_Instruction;
   }

}
