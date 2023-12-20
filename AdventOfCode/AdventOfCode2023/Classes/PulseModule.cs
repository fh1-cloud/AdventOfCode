using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{

   internal class PulseModuleOutput : PulseModuleBase
   {
      protected PulseModuleOutput( ) : base( "output" ) { }

      internal override MODULEPULSE? Pulse( MODULEPULSE pulse, string sender, Queue<Tuple<string, MODULEPULSE?, string>> queue )
      {
         return null;
      }
      public static PulseModuleOutput CreateOutputModule( ) { return new PulseModuleOutput( ); }
   }


   internal class PulseModuleBroadcaster : PulseModuleBase
   {
      internal PulseModuleBroadcaster( string inp ) : base( inp ) { }
      internal override MODULEPULSE? Pulse( MODULEPULSE pulse, string sender, Queue<Tuple<string, MODULEPULSE?, string>> queue )
      {
         foreach( string child in m_DestinationModules )
         {
            queue.Enqueue( Tuple.Create( child, (MODULEPULSE?) pulse, m_Name ) );
            Console.WriteLine( child + " " + ( pulse == MODULEPULSE.LOWPULSE ? "-low-> " : "-high-> " ) + "broadcaster" );
         }
         return pulse;
      }
   }

   internal class PulseModuleConjunction : PulseModuleBase
   {


      internal PulseModuleConjunction( string inputLine ) : base( inputLine )
      {
         if( inputLine[0] != '&' )
            throw new Exception( );

         this.PulseMemory = new Dictionary<string, MODULEPULSE>( );
      }
      internal Dictionary<string, MODULEPULSE> PulseMemory { get; set; }
      internal override MODULEPULSE? Pulse( MODULEPULSE pulse, string sender, Queue<Tuple<string, MODULEPULSE?, string>> queue )
      {
      //Assume that the dictionary of connected modules are already present (need to be populated on beforehand)
         this.PulseMemory[sender] = pulse;

         bool isAllHigh = true;
         MODULEPULSE? retPulse = null;
         foreach( KeyValuePair<string, MODULEPULSE> kvp in this.PulseMemory )
         {
            if( kvp.Value == MODULEPULSE.LOWPULSE )
            {
               isAllHigh = false;
               break;
            }
         }
         if( isAllHigh )
            retPulse = MODULEPULSE.LOWPULSE;
         else
            retPulse=MODULEPULSE.HIGHPULSE;

         foreach( string child in m_DestinationModules )
         {
            queue.Enqueue( Tuple.Create( child, (MODULEPULSE?) retPulse, m_Name ) );
            Console.WriteLine( child + " " + ( retPulse == MODULEPULSE.LOWPULSE ? "-low-> " : "-high-> " ) + "broadcaster" );
         }

         return retPulse;
      }
   }

   internal class PulseModuleFlipFlop : PulseModuleBase
   {
      internal PulseModuleFlipFlop( string inputLine ) : base( inputLine )
      {
         if( inputLine[0] != '%' )
            throw new Exception( );

         this.State = false;
      }
      public bool State { get; set; }
      internal override MODULEPULSE? Pulse( MODULEPULSE pulse, string sender, Queue<Tuple<string, MODULEPULSE?, string>> queue )
      {
         if( pulse == MODULEPULSE.HIGHPULSE ) //Return null if it recieved a highpulse
            return null;

         //Pulse is LOWPULSE
         this.State = !this.State;

         MODULEPULSE? retPulse = null;
         if( this.State == false ) //Old state was true, it just switched off
            retPulse = MODULEPULSE.HIGHPULSE;
         else //Old state was false, it was just switched on
            retPulse = MODULEPULSE.LOWPULSE;

         foreach( string child in m_DestinationModules )
         {
            queue.Enqueue( Tuple.Create( child, (MODULEPULSE?) retPulse, m_Name ) );
            Console.WriteLine( child + " " + ( retPulse == MODULEPULSE.LOWPULSE ? "-low-> " : "-high-> " ) + "broadcaster" );
         }

         return retPulse;
      }
   }


   internal abstract class PulseModuleBase
   {
      internal enum MODULEPULSE
      {
         LOWPULSE,
         HIGHPULSE,
      }

   /*MEMBERS*/
   #region
      protected List<string> m_DestinationModules = new List<string>( );
      protected string m_Name = null;
   #endregion

   /*CONSTRUCTORS*/
   #region
      internal PulseModuleBase( string inputLine )
      {
         if( !inputLine.Equals( "output" ) )
         {
            string[] sp = inputLine.Split( new char[] { '>' }, StringSplitOptions.None)[1].TrimStart( ' ' ).Split( new char[] {','}, StringSplitOptions.RemoveEmptyEntries );
            foreach( string sp2 in sp )
               m_DestinationModules.Add( sp2.Trim( ) );
         }
         m_Name = inputLine.Split( new char[] { ' ' }, StringSplitOptions.None )[0].TrimStart( new char[] { '%', '&' } );
      }
      public string Name => m_Name;
      public List<string> DestinationModules => m_DestinationModules;   

   #endregion

      public static long LowPulses { get; set; }
      public static long HighPulses { get; set; }  

      internal static long P1( )
      {
         Console.WriteLine( $"LowPulses: {LowPulses}" );
         Console.WriteLine( $"HigPulses: {HighPulses}" );
         long ans = LowPulses*HighPulses;
         Console.WriteLine( $"{LowPulses}*{HighPulses}={ans}" );
         return ans;
      }

      public static void PushButton( Dictionary<string, PulseModuleBase> allModules )
      {

      //Increment buttonpulse
         MODULEPULSE buttonPulse = MODULEPULSE.LOWPULSE;
         LowPulses++;

      //Send pulse to broadcaster..
         Console.WriteLine( "button" + " " + ( buttonPulse == MODULEPULSE.LOWPULSE ? "-low-> " : "-high-> " ) + "broadcaster" );

      //Start the pulse chain..
         Queue<Tuple<string, MODULEPULSE?, string>> queue = new Queue<Tuple<string, MODULEPULSE?, string>>( ); //Who to pulse, signal, sender
         allModules["broadcaster"].Pulse( buttonPulse, "button", queue );

      //Add the children of the broadcaster to the chains, and write signal sent
         while( queue.Count > 0 )
         {
         //Deque next signal..
            ( string candidateName, MODULEPULSE? rawSignal, string sender ) = queue.Dequeue( );

         //Skip if no signal was sent..
            if( rawSignal == null )
               continue;

            PulseModuleBase thisModule = allModules[candidateName];
            MODULEPULSE signal = ( MODULEPULSE ) rawSignal;

         //Pulse this module..
            MODULEPULSE? ans = thisModule.Pulse( signal, sender, queue );

         //Skip to next if the signal was null
            if( ans == null )
               continue;
         //Print to console what was sent..

            if( ans == MODULEPULSE.LOWPULSE )
               LowPulses++;
            else if( ans == MODULEPULSE.HIGHPULSE )
               HighPulses++;
         }



      }

      public static PulseModuleBase CreateModuleFromInput( string inp )
      {
      //Try to create flipflop
         if( inp[0] == '%' )
            return new PulseModuleFlipFlop( inp );
         if( inp[0] == '&' )
            return new PulseModuleConjunction( inp );
         if( inp.Contains( "broad" ) )
            return new PulseModuleBroadcaster( inp );
         throw new Exception( );
      }
   
      public static void PopulateConjunctionModules( Dictionary<string,PulseModuleBase> allModules )
      {
      //Loop over all modules..
         foreach( KeyValuePair<string, PulseModuleBase> kvp in allModules )
         {
            PulseModuleConjunction thisConjunctionModule = kvp.Value as PulseModuleConjunction; //Cast as conjunction
            if( thisConjunctionModule == null )
               continue;

         //Find connected modules..
            string thisModule = kvp.Key;
         //Loop over all modules except this one, look for modules that has this module as a child, if so, add it to the list of connected modules

            foreach( KeyValuePair<string, PulseModuleBase> otherModules in allModules )
            {
            //Dont do this on itself..
               if( kvp.Key == otherModules.Key )
                  continue;

               if( otherModules.Value.m_DestinationModules.Contains( thisModule ) )
               {
                  if( !thisConjunctionModule.PulseMemory.ContainsKey( otherModules.Key ) )
                  {
                     thisConjunctionModule.PulseMemory.Add( otherModules.Key, MODULEPULSE.LOWPULSE );
                  }
               }
            }
         }
      }

   /*METHODS*/
   #region
      internal abstract MODULEPULSE? Pulse( MODULEPULSE pulse, string sender, Queue<Tuple<string, MODULEPULSE?, string>> queue );
      

   #endregion

   }
}
