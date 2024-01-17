using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   internal class MineralRobot
   {

   /*ENUMS*/
   #region
      public enum ROBOTRESOURCE
      {
         GEODE = 0,
         OBSIDIAN = 1,
         CLAY = 2,
         ORE = 3,
      }



   #endregion

   /*LOCAL CLASSES*/
   #region


      public class RobotBlueprint
      {

         protected Dictionary<ROBOTRESOURCE,List<( ROBOTRESOURCE, int )>> m_ProductionCosts; //Key = robot to create. Value.Item1 = resource, Value.Item2 = amount of item1 resource

         public RobotBlueprint( string inp )
         {
            m_ProductionCosts = new Dictionary<ROBOTRESOURCE, List<(ROBOTRESOURCE, int)>>( );

            string[] spl0 = inp.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
            this.ID = int.Parse( spl0[1].Trim( ':' ) );
            string[] spl1 = inp.Split( new char[] { 'E' }, StringSplitOptions.RemoveEmptyEntries );

            int oreRobotOreCost = int.Parse( spl1[1].Split( new char[] { ' ' } )[4] );
            m_ProductionCosts.Add( ROBOTRESOURCE.ORE, new List<(ROBOTRESOURCE, int)>{ (ROBOTRESOURCE.ORE, oreRobotOreCost ) } );

            int clayRobotOreCost = int.Parse( spl1[2].Split( new char[] { ' ' } )[4] );
            m_ProductionCosts.Add( ROBOTRESOURCE.CLAY, new List<(ROBOTRESOURCE, int)>{ (ROBOTRESOURCE.ORE, clayRobotOreCost) } );

            int obsidianRobotOreCost = int.Parse( spl1[3].Split( new char[] { ' ' } )[4] );
            int obsidianRobotClayCost = int.Parse( spl1[3].Split( new char[] { ' ' } )[7] );
            m_ProductionCosts.Add( ROBOTRESOURCE.OBSIDIAN, new List<(ROBOTRESOURCE, int)>{ (ROBOTRESOURCE.ORE, obsidianRobotOreCost), ( ROBOTRESOURCE.CLAY, obsidianRobotClayCost ) } );

            int geodeRobotOreCost = int.Parse( spl1[4].Split( new char[] { ' ' } )[4] );
            int geodeRobotObsidianCost = int.Parse( spl1[4].Split( new char[] { ' ' } )[7] );
            m_ProductionCosts.Add( ROBOTRESOURCE.GEODE, new List<(ROBOTRESOURCE, int)>{ (ROBOTRESOURCE.ORE, geodeRobotOreCost), ( ROBOTRESOURCE.OBSIDIAN, geodeRobotObsidianCost ) } );

         }

         public int ID { get; private set; }

         //Checks if its possible to create a specific type of robot.
         public bool CanCreateRobot( ROBOTRESOURCE typeToCreate, Dictionary<ROBOTRESOURCE,int> availableResources )
         {
            List<(ROBOTRESOURCE, int)> costs = m_ProductionCosts[typeToCreate];

         //Check if there is enough available resources..
            bool canCreate = true;
            foreach( (ROBOTRESOURCE Pc, int Vc ) c in costs )
            {
               if( availableResources[c.Pc] < c.Vc )
               {
                  canCreate = false;
                  break;
               }
            }
            return canCreate;
         }


         public ROBOTRESOURCE CreateRobot( ROBOTRESOURCE typeToCreate, Dictionary<ROBOTRESOURCE,int> availableResources )
         {
            foreach( (ROBOTRESOURCE Pc, int Vc ) c in m_ProductionCosts[typeToCreate] )
               availableResources[c.Pc] = availableResources[c.Pc] - c.Vc;
            return typeToCreate;
         }



      }

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

   /*METHODS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region

      public static long P1( string[] inp )
      {

      //Create all blueprints..
         List<RobotBlueprint> bluePrints = new List<RobotBlueprint>( );
         foreach( string s in inp )
         {
            RobotBlueprint b = new RobotBlueprint( s );
            bluePrints.Add( b );
         }

      //Start collecting chain for one robot..
         List<long> bluePrintQuality = new List<long>( );
         for( int i = 0; i<bluePrints.Count; i++ )
         {
            int timeLeft = 24;
            Dictionary<ROBOTRESOURCE,int> resources = new Dictionary<ROBOTRESOURCE, int>{ { ROBOTRESOURCE.ORE, 0 }, { ROBOTRESOURCE.CLAY, 0 }, { ROBOTRESOURCE.OBSIDIAN, 0 }, { ROBOTRESOURCE.GEODE, 0 } };
            Dictionary<ROBOTRESOURCE,int> robots = new Dictionary<ROBOTRESOURCE, int>{ { ROBOTRESOURCE.ORE, 1 }, { ROBOTRESOURCE.CLAY, 0 }, { ROBOTRESOURCE.OBSIDIAN, 0 }, { ROBOTRESOURCE.GEODE, 0 } };
            RobotBlueprint bluePrintToUse = bluePrints[i];
            List<int> geodeList = new List<int>( );
            Dictionary<string,int> cache = new Dictionary<string, int>( );

            PassTime( timeLeft, bluePrintToUse, resources, robots, geodeList, cache );

            int maxGeodes = geodeList.Max( );
            int quality = bluePrints[i].ID*maxGeodes;

            Console.WriteLine( $"Blueprint: {bluePrints[i].ID}. Max geodes = {maxGeodes}, quality = {quality}" );
            bluePrintQuality.Add( quality );
         }

         return bluePrintQuality.Sum( );
      }

      public static string GetCacheString( int timeLeft, Dictionary<ROBOTRESOURCE,int> resources, Dictionary<ROBOTRESOURCE,int> robots )
      {
         return $"{timeLeft},{resources[ROBOTRESOURCE.ORE]},{resources[ROBOTRESOURCE.CLAY]},{resources[ROBOTRESOURCE.OBSIDIAN]},{robots[ROBOTRESOURCE.ORE]},{robots[ROBOTRESOURCE.CLAY]},{robots[ROBOTRESOURCE.OBSIDIAN]},{robots[ROBOTRESOURCE.GEODE]}";
      }




      public static void PassTime( int timeLeft, RobotBlueprint bluePrint, Dictionary<ROBOTRESOURCE, int> resources, Dictionary<ROBOTRESOURCE, int> robots, List<int> geodes, Dictionary<string,int> cache )
      {
      //OPTIMIZATIONS!
      //Low hanging fruit: recursing over build decisions, not individual time steps. So I never skip any time doing nothing for no reason - either build, or what exactly how long it needs to save up and build.
      //No building robots past the max amount for that resource - once you get 20 clay per time step and the most expensive thing to buy is 20 clay then there's no use building more.
      //If 1 time unit left, or less, do nothing. I saw it spent a lot of time deciding what to build in the last time two units.

      //First, check if there is no time left, if so, just return.
         if( timeLeft <= 0 )
         {
            geodes.Add( resources[ROBOTRESOURCE.GEODE] );
            return;
         }

         string mem = GetCacheString( timeLeft, resources, robots );
         if( cache.ContainsKey( mem ) )
         {
            if( cache[mem] < resources[ROBOTRESOURCE.GEODE] ) //We\ve been here before, but with less resources. Update cache and continue
               cache[mem] = resources[ROBOTRESOURCE.GEODE];
            else //Weve been here before, with more resources. Quit branch
               return;
         }
         else //we havent been here before, create new cache
         {
            cache[mem] = resources[ROBOTRESOURCE.GEODE];
         }

      //Check if we reached this point before with the same amount of resources, robots and time left, with less geodes. If so, kill branch.

      //Add resorces from existing robots..
         Dictionary<ROBOTRESOURCE,int> newResources = new Dictionary<ROBOTRESOURCE, int>( resources );
         foreach( KeyValuePair<ROBOTRESOURCE, int> kvpr in robots )
            newResources[kvpr.Key] += kvpr.Value;

      //Check what type of robots we can build with the available resources
         HashSet<ROBOTRESOURCE> possibleCreations = new HashSet<ROBOTRESOURCE>( );
         if( timeLeft > 1 )
         {
            for( int i = 0; i<4; i++ )
               if( bluePrint.CanCreateRobot( ( ROBOTRESOURCE ) i, resources ) )
                  possibleCreations.Add( ( ROBOTRESOURCE ) i );
         }

      //Pass time..
         timeLeft = timeLeft - 1;


      //Create a branch that doesnt create anything.
         Dictionary<ROBOTRESOURCE,int> zeroBranchResources = new Dictionary<ROBOTRESOURCE, int>( newResources );
         Dictionary<ROBOTRESOURCE,int> zeroBranchRobots = new Dictionary<ROBOTRESOURCE, int>( robots );
         PassTime( timeLeft, bluePrint, zeroBranchResources, zeroBranchRobots, geodes , cache );

         if( timeLeft > 1 )// No need to create a new robot if there is only 1 minute left, it cannot produce anything
         {
         //For all the potential creations, create a robot and pass time..
            foreach( ROBOTRESOURCE r in possibleCreations )
            {

            //Create a copy of the resources after we created this robot
               Dictionary<ROBOTRESOURCE,int> resourcesAterCreation = new Dictionary<ROBOTRESOURCE, int>( newResources );

            //Create a copy of the list of robots
               Dictionary<ROBOTRESOURCE,int> newRobots = new Dictionary<ROBOTRESOURCE, int>( robots );

            //Create the new robot and add it to the new list of robots.
               ROBOTRESOURCE newRobot = bluePrint.CreateRobot( r, resourcesAterCreation );
               newRobots[newRobot] += 1;

            //Pass time for this branch.
               PassTime( timeLeft, bluePrint, resourcesAterCreation, newRobots, geodes , cache );

            //If we create a geode here, this is the most useful branch. Stop creating more.
               if( r == ROBOTRESOURCE.GEODE )
                  break;

            }

         }


      }




        #endregion

    }

}
