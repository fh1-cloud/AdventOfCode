using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class DesertNode
   {

   /*CONSTRUCTORS*/
   #region
      public DesertNode( string name )
      {
         this.Name = name;
         if( this.Name[this.Name.Length-1] == 'Z' )
            this.IsEndNodeP2 = true;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public DesertNode RChild { get; set; }
      public DesertNode LChild { get; set; }
      public string Name { get; protected set; }
      public bool IsEndNodeP2 { get; protected set; }
   #endregion

   /*STATIC METHODS*/
   #region

      public static long FindCycle( string nav, DesertNode start, DesertNode endNode = null )
      {
         int runningIdx = 0;
         long nStep = 0;
         DesertNode currN = start;
         while( true )
         {
            char i = nav[ runningIdx++ % nav.Length ];
            if( i == 'L' ) //Get next 
               currN = currN.LChild;
            else if( i == 'R' )
               currN = currN.RChild;

            nStep++; //Increment and exit condition
            if( endNode == null && currN.IsEndNodeP2 )
               break;
            else if( endNode != null && currN == endNode )
               break;
         }
         return nStep;
      }

   #endregion

   }
}
