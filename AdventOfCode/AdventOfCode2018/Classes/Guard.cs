using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
   public class Guard
   {

   /*ENUMS*/
   #region

      /// <summary>
      /// Enum representing the status for this guard.
      /// </summary>
      public enum STATUS
      {
         AWAKE,
         SLEEP,
      }


   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected int m_ID = -1;
      protected SortedDictionary<DateTime,STATUS> m_Status = new SortedDictionary<DateTime, STATUS>( );  //The status for this guard sorted by times.
      protected STATUS m_CurrentStatus = STATUS.AWAKE;   //The current status for this guard. Only used during status flags to check if the parsing of the dates were correct
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor
      /// </summary>
      /// <param name="id"></param>
      public Guard( int id )
      {
         m_ID = id;
      }


   #endregion

   /*PROPERTIES*/
   #region
      public int ID => m_ID;
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region


      /// <summary>
      /// Gets an array over all the minutes in the hour, where the value is number of times this guard was asleep at that minute
      /// </summary>
      /// <returns></returns>
      public int[] GetSleepMinuteArray( )
      {
      //Create the array of minutes.
         int[] minuteArray = new int[60];
         for( int i = 0; i < 60; i++ )
            minuteArray[i] = 0;

      //Loop over sorted statuses
         foreach( var kvp in m_Status )
         {
            if( kvp.Value == STATUS.AWAKE )
               continue;
            else if( kvp.Value == STATUS.SLEEP )
            {
            //Unpack the sleeptime
               DateTime sleepyTime = kvp.Key;

            //Get the next one in the dictionary (lazy, but use linq.. )
               KeyValuePair<DateTime, STATUS> wakyKvp = m_Status.Where( x => x.Key > kvp.Key ).ToList( ).FirstOrDefault( );
               //Throw if if next one is also sleep. Should not be possible
               if( wakyKvp.Value == STATUS.SLEEP )
                  throw new Exception( );

            //Loop from startindex to endindex..
               int startIdx = ( int ) ( sleepyTime.Minute );
               int endIdx = ( int ) ( wakyKvp.Key.Minute );

               for( int i = startIdx; i<endIdx; i++ )
                  minuteArray[i]++;

            //Calculate sleeping time.
               long thisNumberOfMinutes = ( long ) ( ( wakyKvp.Key - sleepyTime ).TotalMinutes );
            }
         }

      //Return the whole array.
         return minuteArray;
      }



      /// <summary>
      /// Gets the minute where this guard is the most asleep.
      /// </summary>
      /// <returns></returns>
      public int GetMinuteWithMostSleep( )
      {
      //Create the array of minutes.
         int[] minuteArray = new int[60];
         for( int i = 0; i < 60; i++ )
            minuteArray[i] = 0;

      //Loop over sorted statuses
         foreach( var kvp in m_Status )
         {
            if( kvp.Value == STATUS.AWAKE )
               continue;
            else if( kvp.Value == STATUS.SLEEP )
            {
            //Unpack the sleeptime
               DateTime sleepyTime = kvp.Key;

            //Get the next one in the dictionary (lazy, but use linq.. )
               KeyValuePair<DateTime, STATUS> wakyKvp = m_Status.Where( x => x.Key > kvp.Key ).ToList( ).FirstOrDefault( );
               //Throw if if next one is also sleep. Should not be possible
               if( wakyKvp.Value == STATUS.SLEEP )
                  throw new Exception( );

            //Loop from startindex to endindex..
               int startIdx = ( int ) ( sleepyTime.Minute );
               int endIdx = ( int ) ( wakyKvp.Key.Minute );

               for( int i = startIdx; i<endIdx; i++ )
                  minuteArray[i]++;

            //Calculate sleeping time.
               long thisNumberOfMinutes = ( long ) ( ( wakyKvp.Key - sleepyTime ).TotalMinutes );
            }
         }

      //Get the maximum entry in the array.
         int idx = minuteArray.ToList( ).IndexOf( minuteArray.Max( ) );
         return idx;
      }

      /// <summary>
      /// Gets the number of minutes this guard is asleep
      /// </summary>
      /// <returns></returns>
      public long GetNumberOfMinutesSleeping( )
      {
         long numberOfMinutes = 0;
      //Loop over sorted statuses
         foreach( var kvp in m_Status )
         {
            if( kvp.Value == STATUS.AWAKE )
               continue;
            else if( kvp.Value == STATUS.SLEEP )
            {
            //Unpack the sleeptime
               DateTime sleepyTime = kvp.Key;

            //Get the next one in the dictionary (lazy, but use linq.. )
               KeyValuePair<DateTime, STATUS> wakyKvp = m_Status.Where( x => x.Key > kvp.Key ).ToList( ).FirstOrDefault( );
               //Throw if if next one is also sleep. Should not be possible
               if( wakyKvp.Value == STATUS.SLEEP )
                  throw new Exception( );

            //Calculate sleeping time.
               long thisNumberOfMinutes = ( long ) ( ( wakyKvp.Key - sleepyTime ).TotalMinutes );

            //Check if this number of minutes is more than two hours. Then throw, Should not be possible.
               if( thisNumberOfMinutes > 90 )
                  throw new Exception( );

               numberOfMinutes += thisNumberOfMinutes;
            }
         }

      //Return the total number of minutes
         return numberOfMinutes;
      }


      /// <summary>
      /// Sets the status for this guard.
      /// </summary>
      /// <param name="time">The time for this action</param>
      /// <param name="status">The status for this action</param>
      public void SetStatus( DateTime time, STATUS status )
      {
      //Tries to set the status of this guard. It gets the previous status for this day. 
         if( status == STATUS.SLEEP )
         {
         //If the status is sleeping, check if the guard has arrived at work. If not, throw.
            TimeSpan twoHours = new TimeSpan( 0, 2, 0, 0 );
            int timeFromArrival = m_Status.Where( x => x.Key > time - twoHours ).ToList( ).Count;
            if( timeFromArrival == 0 )
               throw new Exception( );
            if( status == m_CurrentStatus && status == STATUS.SLEEP )
               throw new Exception( );
         }

      //Add the status..
         m_Status.Add( time, status );
         m_CurrentStatus = status;
      }
      

   #endregion

   /*STATIC METHODS*/
   #region
   #endregion


   }
}
