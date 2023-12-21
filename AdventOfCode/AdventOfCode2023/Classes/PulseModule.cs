using AdventOfCodeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{

   /*ENUMS*/
   #region
   internal enum MODULEPULSE
   {
      LOWPULSE,
      HIGHPULSE,
   }
   #endregion

   /*LOCAL CLASSES*/
   #region

   internal class PulseModuleOutput : PulseModuleBase
   {
      protected PulseModuleOutput( ) : base( "output" ) { }
      internal override void Pulse( MODULEPULSE pulse, string sender, Queue<Tuple<string, MODULEPULSE?, string>> queue , Dictionary<string, (long, bool)> rxSourceCounts ) { return; }
      public static PulseModuleOutput CreateOutputModule( ) { return new PulseModuleOutput( ); }
   }


   internal class PulseModuleBroadcaster : PulseModuleBase
   {
      internal PulseModuleBroadcaster( string inp ) : base( inp ) { }
      internal override void Pulse( MODULEPULSE pulse, string sender, Queue<Tuple<string, MODULEPULSE?, string>> queue , Dictionary<string, (long, bool)> rxSourceCounts )
      {
         foreach( string child in m_DestinationModules )
         {
            queue.Enqueue( Tuple.Create( child, (MODULEPULSE?) pulse, m_Name ) );
            if( pulse == MODULEPULSE.LOWPULSE )
               LowPulses++;
            else if( pulse == MODULEPULSE.HIGHPULSE )
               HighPulses++;
         }
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
      internal override void Pulse( MODULEPULSE pulse, string sender, Queue<Tuple<string, MODULEPULSE?, string>> queue , Dictionary<string, (long, bool)> rxSourceCounts )
      {
         this.PulseMemory[sender] = pulse;
         bool isAllHigh = true;
         foreach( KeyValuePair<string, MODULEPULSE> kvp in this.PulseMemory )
         {
            if( kvp.Value == MODULEPULSE.LOWPULSE )
            {
               isAllHigh = false;
               break;
            }
         }
         MODULEPULSE? sentPulse = null;
         if( isAllHigh )
            sentPulse = MODULEPULSE.LOWPULSE;
         else
            sentPulse=MODULEPULSE.HIGHPULSE;

         foreach( string child in m_DestinationModules )
         {
            queue.Enqueue( Tuple.Create( child, (MODULEPULSE?) sentPulse, m_Name ) );
            if( sentPulse == MODULEPULSE.LOWPULSE )
               LowPulses++;
            else if( sentPulse == MODULEPULSE.HIGHPULSE )
               HighPulses++;

         //If the child name is RX, we know we are at the conjunction right above rx. If the recieved pulse is high, we store the count of buttons
            if( child == "rx" )
               if( pulse == MODULEPULSE.HIGHPULSE )
                  if( rxSourceCounts[sender].Item2 == false )
                     rxSourceCounts[sender] = ( ButtonCount, true );
         }
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
      internal override void Pulse( MODULEPULSE pulse, string sender, Queue<Tuple<string, MODULEPULSE?, string>> queue, Dictionary<string, ( long, bool )> rxSourceCounts )
      {
         if( pulse == MODULEPULSE.HIGHPULSE ) //Return null if it recieved a highpulse
            return;
         this.State = !this.State;
         MODULEPULSE? sentPulse = null;
         if( this.State == false ) //Old state was true, it just switched off
            sentPulse = MODULEPULSE.LOWPULSE;
         else //Old state was false, it was just switched on
            sentPulse = MODULEPULSE.HIGHPULSE;
         foreach( string child in m_DestinationModules )
         {
            queue.Enqueue( Tuple.Create( child, (MODULEPULSE?) sentPulse, m_Name ) );
            if( sentPulse == MODULEPULSE.LOWPULSE )
               LowPulses++;
            else if( sentPulse == MODULEPULSE.HIGHPULSE )
               HighPulses++;
         }
      }
   }

   #endregion

   internal abstract class PulseModuleBase
   {

   /*MEMBERS*/
   #region
      protected List<string> m_DestinationModules = new List<string>( );
      protected string m_Name = null;
   #endregion

   /*CONSTRUCTORS*/
   #region

      static PulseModuleBase( )
      {
         LowPulses = 0;
         HighPulses = 0;
         Report = false;
         ButtonCount = 0;
      }

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

   #endregion

   /*PROPERTIES*/
   #region
      public string Name => m_Name;
      public List<string> DestinationModules => m_DestinationModules;   
      public static long LowPulses { get; set; }
      public static long HighPulses { get; set; }  
      public static long ButtonCount { get; set; }
      public static bool Report { get; set; }

   #endregion

   /*STATIC METHODS*/
   #region
      internal static long P1( )
      {
         Console.WriteLine( "" );
         Console.WriteLine( $"LowPulses: {LowPulses}" );
         Console.WriteLine( $"HigPulses: {HighPulses}" );
         long ans = LowPulses*HighPulses;
         Console.WriteLine( $"{LowPulses}*{HighPulses}={ans}" );
         Console.WriteLine( "" );
         return ans;
      }


      public static bool PushButton( Dictionary<string, PulseModuleBase> allModules, Dictionary<string, (long , bool)> rxSourceInputCounts )
      {
      //From reverse engineering the input, it can be seen that the last module above rx, is a conjunction module. 
      //In order to send a low pulse to rx, all of its inputs must be sending a high pulse.
      //Find the module that has rx as a child..
      //Increment buttonpulse
         MODULEPULSE buttonPulse = MODULEPULSE.LOWPULSE;
         LowPulses++;
         ButtonCount++;

      //Start the pulse chain..
         Queue<Tuple<string, MODULEPULSE?, string>> queue = new Queue<Tuple<string, MODULEPULSE?, string>>( ); //Who to pulse, signal, sender
         queue.Enqueue( Tuple.Create( "broadcaster", ( MODULEPULSE? )buttonPulse, "button" ) );

         while( queue.Count > 0 )
         {
            //If all the rx source have fired a signal, we know we're done.
            //We know that all of the inputs to the conjunction that sends pulses to rx have sent out a high pulse.
            //Now we need to find the LCM of the counts for each input in order to find the first time that all of the inputs
            //Have sent out a high pulse, which in turn would cause RX to recieve a low pulse
            if( rxSourceInputCounts.All( rxs => rxs.Value.Item2 ) )
               return true;

         //Deque next signal..
            ( string candidateName, MODULEPULSE? rawSignal, string sender ) = queue.Dequeue( );

         //Skip if it doesnt exist. Should not fire
            if( !allModules.ContainsKey( candidateName ) )
               continue;

            allModules[candidateName].Pulse( ( MODULEPULSE ) rawSignal, sender, queue, rxSourceInputCounts );
         }

         return false;
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
                  if( !thisConjunctionModule.PulseMemory.ContainsKey( otherModules.Key ) )
                     thisConjunctionModule.PulseMemory.Add( otherModules.Key, MODULEPULSE.LOWPULSE );
            }
         }
      }
   #endregion

   /*METHODS*/
   #region
      internal abstract void Pulse( MODULEPULSE pulse, string sender, Queue<Tuple<string, MODULEPULSE?, string>> queue, Dictionary<string, ( long, bool )> rxSourceCounts );
   #endregion

   }
}
