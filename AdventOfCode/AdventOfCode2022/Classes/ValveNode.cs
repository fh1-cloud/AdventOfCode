﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class ValveNode
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region

      public int FlowRate { get; set; }
      public HashSet<string> Connections { get; set; }
      public string Name { get; set; }
   #endregion

   /*MEMBERS*/
   #region
   #endregion

   /*CONSTRUCTORS*/
   #region
   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region

      public static long P1( string[] input )
      {

      //Create tree structure

         Dictionary<string,ValveNode> allNodes = new Dictionary<string, ValveNode>( );
         foreach( string s in input )
         {
            string name = s.Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries )[1];
            int flowRate = int.Parse( s.Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries )[4].Split( new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries )[1].Trim( ';' ) );
            HashSet<string> connections = s.Split( new char[ ] { ';' }, StringSplitOptions.RemoveEmptyEntries )[1].Trim( ',' ).Split( new char[ ] { ' ' }, StringSplitOptions.RemoveEmptyEntries ).Skip( 4 ).Select( x => x.Trim( ',' ) ).ToHashSet( );
            allNodes.Add( name, new ValveNode{ Name = name, Connections = connections, FlowRate = flowRate } );
         }


      //Start the valve opening chain
         ValveNode startNode = allNodes["AA"];
         List<long> pressureReleases = new List<long>( );
         HashSet<string> openedValves = new HashSet<string>( );
         HashSet<string> valvesWithPressureLeft = allNodes.Where( y => y.Value.FlowRate > 0 ).Select( x => x.Key ).ToHashSet( );


      //Start walking..


         allNodes["AA"].OpenValves( allNodes, 0, 0, openedValves, valvesWithPressureLeft, pressureReleases );

         long maxValue = pressureReleases.Max( );
         return maxValue;
      }


      public void OpenValves( Dictionary<string,ValveNode> allNodes, long currentPressureRelease, long minutesSpent, HashSet<string> openedValves, HashSet<string> valvesWithPressureLeft, List<long> pressureReleases )
      {
         if( minutesSpent >= 30 || valvesWithPressureLeft.Count == 0 )
         {
            pressureReleases.Add( currentPressureRelease );
            return;
         }
         else //We have minutes left. Continue moving.
         {
         //Just moving on is always an option.
            foreach( string n in this.Connections )
            {
               long timeSpent = minutesSpent + 1;
               allNodes[n].OpenValves( allNodes, currentPressureRelease, timeSpent, openedValves, valvesWithPressureLeft, pressureReleases );
            }

         //If this valve isnt opened yet, we have the option to open this valve or not
            if( !openedValves.Contains( this.Name ) && this.FlowRate > 0 )
            {
               
            //Open valve and move
               long openedMinutesSpent = minutesSpent + 1; //Cost of opening valve
               long thisPressureRelease = ( 30 - minutesSpent ) * this.FlowRate;
               long newPressureReleasee = currentPressureRelease + thisPressureRelease;

            //Create a copy of the visited set and continue
               HashSet<string> newOpenedValves = new HashSet<string>( openedValves );
               newOpenedValves.Add( this.Name );

               HashSet<string> nodesWithPressureLeft = new HashSet<string>( valvesWithPressureLeft );
               nodesWithPressureLeft.Remove( this.Name );

            //Exit if this made it go over.
               if( openedMinutesSpent >= 30 || valvesWithPressureLeft.Count == 0 )
               {
                  pressureReleases.Add( newPressureReleasee );
                  return;
               }
               else //Continue moving
               {
                  foreach( string n in this.Connections )
                  {
                     long timeSpent = openedMinutesSpent + 1;
                     allNodes[n].OpenValves( allNodes, newPressureReleasee, timeSpent, newOpenedValves, nodesWithPressureLeft, pressureReleases );
                  }
               }



            }

         }



      }


         //https://github.com/Bpendragon/AdventOfCodeCSharp/blob/9fd66/AdventOfCode/Solutions/Year2022/Day16-Solution.cs
        ///// <summary>
        ///// Recursively check all paths to find all the possible outputs.
        ///// </summary>
        ///// <param name="node">Node you are at, specifically teh index of the name from impNodes</param>
        ///// <param name="time">Time remaining</param>
        ///// <param name="state">int/bitmask of valves that are turned on</param>
        ///// <param name="flow">Total flow (calculated from each valve as its turned on)</param>
        ///// <param name="cache">Key is state (see above) value is max flow achieved in that state</param>
        //private void Visit(int node, int time, int state, int flow, Dictionary<int, int> cache)
        //{
        //    cache[state] = int.Max(cache.GetValueOrDefault(state, 0), flow); //Are we at a better point with the current valves turned on than last time we were at this point? if so, update value
        //    for(int i = 0; i < impValves.Count; i++) //For all valves
        //    {
        //        var newTime = time - impDists[node, i] - 1; //time remaining is time minus walking time, minus 1 minute to open valve
        //        if ((valveMasks[i] & state) != 0 || newTime <= 0) continue; //Don't go to the same valve twice, don't go to a valve if it means we run out of time.
        //        Visit(i, newTime, state | valveMasks[i], flow + (newTime * Valves[impValves[i]].Flowrate), cache);
        //        //Go to new valve, update state so it's turned on, add it's flow, repeat everything above.
        //    }
        //}


        //protected override object SolvePartOne()
        //{
        //    Dictionary<int, int> cache = new();
        //    Visit(0, 30, 0, 0, cache);
        //    return cache.Values.Max();
        //}


   #endregion

   /*METHODS*/
   #region


      


   #endregion


   }
}
