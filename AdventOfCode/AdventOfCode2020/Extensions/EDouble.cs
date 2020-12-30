using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Extensions
{
   public static class EDouble
   {

      public static double ToRad( this double val )
      {
         return val * 2.0 * Math.PI / 360.0;
      }

      public static double ToDeg( this double val )
      {
         return val*360.0/(2.0*Math.PI );
      }


   }
}
