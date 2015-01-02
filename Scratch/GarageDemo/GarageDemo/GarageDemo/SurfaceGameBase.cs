using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Surface;
using Microsoft.Surface.Core;
using XenGameBase;
using Xen2D;

namespace GarageDemo
{
    public class SurfaceGameBase : GameBase
    {
        public static ContactTarget ContactTarget;
        protected UserOrientation currentOrientation = UserOrientation.Bottom;
        protected bool applicationLoadCompleteSignalled;
        protected Matrix screenTransform = Matrix.Identity;

        // application state: Activated, Previewed, Deactivated,
        // start in Activated state
        protected bool isApplicationActivated = true;
        protected bool isApplicationPreviewed;

        public SurfaceGameBase()
        {
            BackgroundColor = new Color( 81, 81, 81 );
        }

        /// <summary>
        /// Allows the app to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            InitializeSurfaceInput();

            // Set the application's orientation based on the current launcher orientation
            currentOrientation = ApplicationLauncher.Orientation;

            // Subscribe to surface application activation events
            ApplicationLauncher.ApplicationActivated += OnApplicationActivated;
            ApplicationLauncher.ApplicationPreviewed += OnApplicationPreviewed;
            ApplicationLauncher.ApplicationDeactivated += OnApplicationDeactivated;

            // Setup the UI to transform if the UI is rotated.
            if( currentOrientation == UserOrientation.Top )
            {
                // Create a rotation matrix to orient the screen so it is viewed correctly when the user orientation is 180 degress different.
                Matrix rotation = Matrix.CreateRotationZ( MathHelper.ToRadians( 180 ) );
                Matrix translation = Matrix.CreateTranslation( GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0 );

                screenTransform = rotation * translation;
            }

            base.Initialize();
            SetWindowOnSurface();
        }

        /// <summary>
        /// Moves and sizes the window to cover the input surface.
        /// </summary>
        private void SetWindowOnSurface()
        {
            System.Diagnostics.Debug.Assert( Window.Handle != System.IntPtr.Zero,
                "Window initialization must be complete before SetWindowOnSurface is called" );
            if( Window.Handle == System.IntPtr.Zero )
                return;

            // We don't want to run in full-screen mode because we need
            // overlapped windows, so instead run in windowed mode
            // and resize to take up the whole surface with no border.

            // Make sure the graphics device has the correct back buffer size.
            InteractiveSurface interactiveSurface = InteractiveSurface.DefaultInteractiveSurface;
            if( interactiveSurface != null )
            {
                Globals.Graphics.PreferredBackBufferWidth = interactiveSurface.Width;
                Globals.Graphics.PreferredBackBufferHeight = interactiveSurface.Height;
                Globals.Graphics.ApplyChanges();

                // Remove the border and position the window.
                Program.RemoveBorder( Window.Handle );
                Program.PositionWindow( Window );
            }
        }

        /// <summary>
        /// Initializes the surface input system. This should be called after any window
        /// initialization is done, and should only be called once.
        /// </summary>
        private void InitializeSurfaceInput()
        {
            System.Diagnostics.Debug.Assert( Window.Handle != System.IntPtr.Zero,
                "Window initialization must be complete before InitializeSurfaceInput is called" );
            if( Window.Handle == System.IntPtr.Zero )
                return;
            System.Diagnostics.Debug.Assert( ContactTarget == null,
                "Surface input already initialized" );
            if( ContactTarget != null )
                return;

            // Create a target for surface input.
            ContactTarget = new ContactTarget( Window.Handle, EventThreadChoice.OnBackgroundThread );
            ContactTarget.EnableInput();
        }

        /// <summary>
        /// This is called when the app should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime )
        {
            if( !applicationLoadCompleteSignalled )
            {
                // Dismiss the loading screen now that we are starting to draw
                ApplicationLauncher.SignalApplicationLoadComplete();
                applicationLoadCompleteSignalled = true;
            }

            //TODO: Rotate the UI based on the value of screenTransform here if desired

            //TODO: Add your drawing code here
            //TODO: Avoid any expensive logic if application is neither active nor previewed

            base.Draw( gameTime );
        }

        /// <summary>
        /// This is called when application has been activated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnApplicationActivated( object sender, EventArgs e )
        {
            // update application state
            isApplicationActivated = true;
            isApplicationPreviewed = false;

            //TODO: Enable audio, animations here

            //TODO: Optionally enable raw image here
        }

        /// <summary>
        /// This is called when application is in preview mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnApplicationPreviewed( object sender, EventArgs e )
        {
            // update application state
            isApplicationActivated = false;
            isApplicationPreviewed = true;

            //TODO: Disable audio here if it is enabled

            //TODO: Optionally enable animations here
        }

        /// <summary>
        ///  This is called when application has been deactivated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnApplicationDeactivated( object sender, EventArgs e )
        {
            // update application state
            isApplicationActivated = false;
            isApplicationPreviewed = false;

            //TODO: Disable audio, animations here

            //TODO: Disable raw image if it's enabled
        }

        #region IDisposable

        protected override void Dispose( bool disposing )
        {
            try
            {
                if( disposing )
                {
                    IDisposable graphicsDispose = Globals.Graphics as IDisposable;
                    if( graphicsDispose != null )
                    {
                        graphicsDispose.Dispose();
                        graphicsDispose = null;
                    }

                    if( ContactTarget != null )
                    {
                        ContactTarget.Dispose();
                        ContactTarget = null;
                    }
                }
            }
            finally
            {
                base.Dispose( disposing );
            }
        }

        #endregion

    }
}
