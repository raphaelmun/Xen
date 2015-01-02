using System;
using Microsoft.Xna.Framework;

namespace XenGameBase
{
    public static class GameTimeUtility
    {
        public static GameTime GameTimeZero
        {
#if SILVERLIGHT
            get
            {
                GameTime gameTime = new GameTime();
                gameTime.Update( new TimeSpan( 0 ) );
                return gameTime;
            }
#else
            get { return new GameTime( new TimeSpan( 0 ), new TimeSpan( 0 ) ); }
#endif
        }

        public static TimeSpan Zero
        {
            get{ return CreateTimeSpan( 0, 0 ); }
        }

        public static TimeSpan OneMillisecond
        {
            get{ return CreateTimeSpan( 0, 1 ); }
        }

        public static TimeSpan OneSecond
        {
            get{ return CreateTimeSpan( 1, 0 ); }
        }

        public static TimeSpan TwoSeconds
        {
            get{ return CreateTimeSpan( 2, 0 ); }
        }

        public static TimeSpan CreateTimeSpan( int seconds, int milliseconds )
        {
            return new TimeSpan( 0, 0, 0, seconds, milliseconds );
        }
    }
}
