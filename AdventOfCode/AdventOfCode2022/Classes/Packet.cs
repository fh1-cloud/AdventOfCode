using AdventOfCodeLib.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class Packet
   {

   /*ENUMS*/
   #region

      /// <summary>
      /// AN enum that represents the result of the packet comparisons..
      /// </summary>
      public enum PACKETCHECK
      {
         OK,
         NOTOK,
         INCONCLUSIVE
      }

   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected List<object> m_Data = new List<object>(); //The object can be an integer or another packet.
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Creates a packet from the input string..
      /// </summary>
      /// <param name="packet"></param>
      /// <exception cref="Exception"></exception>
      public Packet( string packet )
      {
      //Check the enclosing packet..
         if( packet[0] != '[' || packet[packet.Length-1] != ']' )
            throw new Exception( );

      //Extract the inner string..
         string inner = packet.Substring( 1, packet.Length - 2 );

      //Loop over inner string and extract subpackets..
         List<string> subPacketsStrings = new List<string>( );

         for( int i = 0; i< inner.Length; i++ )
         {
         //Find the subpacket if any.
            if( inner[i] == '[' )
            {
               int nOfBrackets = 1;
               int endIdx = i;

            //Count the number of brackets to find the matching pair on the other side..
               for( int j = i + 1; j < inner.Length; j++ )
               {
                  if( inner[j] == '[' )
                     nOfBrackets++;
                  if( inner[j] == ']' )
                     nOfBrackets--;

               //We found the matching pair on the other side. Extract the subpacket and create..
                  if( inner[j] == ']' && nOfBrackets == 0 )
                  {
                     string sub = inner.Substring( i, j-i+1 );
                     Packet newPack = new Packet( sub );
                     m_Data.Add( newPack );
                     endIdx = j;
                     break;
                  }
               }

            //Increment to next index..
               i = endIdx;
               continue;
            }
            else if( inner[i].IsInteger( ) ) //THe char is an integer, check the next if it is an integer, if it is parse it and continue..
            {
            //CHeck the next one..
               if( i != inner.Length - 1 )
               {
                  if( inner[i+1] == ',' ) //If integer? Check next to see if this is also an integer. This will only work with double digits max
                     m_Data.Add( int.Parse( inner[i].ToString( ) ) );
                  else if( inner[i+1].IsInteger( ) )
                  {
                     m_Data.Add( int.Parse( inner.Substring( i, 2 ) ) );
                     i++;
                     continue;
                  }
               }
               else
                  m_Data.Add( int.Parse( inner[i].ToString( ) ) );
            }
         }
      }
   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Gets a string representation of this packet..
      /// </summary>
      /// <returns></returns>
      public string GetString( )
      {
      //Declare the string builder..
         StringBuilder sb = new StringBuilder( );

      //Indicate start of packet..
         sb.Append( "[" );
         foreach( object o in m_Data )
         {
         //If o is int, cast and print.
            if( o is int )
               sb.Append( ( ( int ) o ).ToString( ) );
            else if( o is Packet ) //If o is packet, print the inner packet..
            {
               Packet po = o as Packet;
               sb.Append( po.GetString( ) );
            }
         //Add a comma if this is not the last object in the list..
            if( m_Data.IndexOf( o ) != m_Data.Count - 1 )
               sb.Append( "," );
         }
      //Close packet..
         sb.Append( "]" );

      //Return completed string..
         return sb.ToString();
      }
   #endregion

   /*STATIC METHODS*/
   #region


      /// <summary>
      /// Checks if the packets are in the right order by recursion for other packets..
      /// </summary>
      /// <param name="packet1">Packet 1. The left hans side packet</param>
      /// <param name="packet2">Packet 2. The right hand side packet</param>
      /// <returns></returns>
      /// <exception cref="Exception"></exception>
      public static PACKETCHECK PacketsAreInRightOrder( Packet packet1, Packet packet2 )
      {
         int i = 0;
         while( true )
         {
         //Unpack the objects. Check length of the list in this packet..
         //If the left list runs out of objects first, the inputs are in the right order..
         //If the right list runs out of objects to compare first, the inputs are in the wrong order..
            if( i > packet1.m_Data.Count-1 && packet1.m_Data.Count < packet2.m_Data.Count )
               return PACKETCHECK.OK;
            else if( i > packet2.m_Data.Count - 1 && packet2.m_Data.Count < packet1.m_Data.Count )
               return PACKETCHECK.NOTOK;
            else if( packet1.m_Data.Count == packet2.m_Data.Count && i > packet1.m_Data.Count - 1 ) //If the code reached this point, it was inconclusive. We need to return to the upper layer and flag that it is inconclusive..
               return PACKETCHECK.INCONCLUSIVE;

         //Declare objects..
            object o1 = packet1.m_Data[i];
            object o2 = packet2.m_Data[i];

         //If both objects are integers..
            if( o1 is int && o2 is int )
            {
            //Cast to int..
               int o1Val = ( int ) o1;
               int o2Val = ( int ) o2;

            //Check the values of the integers..
               if( o1Val == o2Val )
               {
                  i++;
                  continue;
               }
               else if( o1Val < o2Val )
                  return PACKETCHECK.OK;
               else if( o1Val > o2Val )
                  return PACKETCHECK.NOTOK;
            }
            else if( o1 is Packet && o2 is Packet )
            {
               Packet p1 = o1 as Packet;
               Packet p2 = o2 as Packet;
               PACKETCHECK check = PacketsAreInRightOrder( p1, p2 );
               if( check != PACKETCHECK.INCONCLUSIVE )
                  return check;

            //If inconclusive, continue checking..
               i++;
               continue;
            }
            else //they are alternating.. o1 is an int and o2 is a packet or vica versa..
            {
            //Declare the checking packets..
               Packet p1Packet = null;
               Packet p2Packet = null;

            //Create packets for comparison..
               if( o1 is int ) // o1 is int, o2 is packet
               {
                  string packetInp = "[" + ( ( int ) o1 ).ToString() + "]";
                  p1Packet = new Packet( packetInp );
                  p2Packet = o2 as Packet;
               }
               else if( o2 is int ) //o2 is int, o1 is packet..
               {
                  p1Packet = o1 as Packet;
                  string packetInp = "[" + ( ( int ) o2 ).ToString() + "]";
                  p2Packet = new Packet( packetInp );
               }

            //Return the comparison..
               PACKETCHECK check = Packet.PacketsAreInRightOrder( p1Packet, p2Packet );
               if( check != PACKETCHECK.INCONCLUSIVE )
                  return check;

            //If inconclusive, continue checking..
               i++;
               continue;
            }
         }

      //If the code reached this point, i have no idea what happened. Throw an exception
         throw new Exception( );
      }
   #endregion

   }
}
