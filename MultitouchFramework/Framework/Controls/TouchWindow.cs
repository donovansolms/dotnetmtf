using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using NUIGroup.MultitouchFramework.Engines;

using System.Diagnostics;

//structure done
namespace NUIGroup
{
    namespace MultitouchFramework
    {
        /// <summary>
        /// Window class that responds to touch events
        /// </summary>
        public class TouchWindow : Window
        {
            #region Properties
            //no need for resizing the window as it is the main container

            /// <summary>
            /// Returns the amount of fingers on the element
            /// </summary>
            public int fingerCount
            {
                get { return _fingerList.Count; }
            }
            #endregion

            #region Private Members
            //list of fingers on this canvas
            private TouchList _fingerList = new TouchList();
            #endregion

            #region Public Members
            //eventhandler variables
            public static readonly RoutedEvent fingerDownEvent = EventManager.RegisterRoutedEvent("fingerDown", RoutingStrategy.Bubble, typeof(fingerDownHandler), typeof(TouchWindow));
            public static readonly RoutedEvent fingerUpEvent = EventManager.RegisterRoutedEvent("fingerUp", RoutingStrategy.Bubble, typeof(fingerUpHandler), typeof(TouchWindow));
            public static readonly RoutedEvent fingerUpdateEvent = EventManager.RegisterRoutedEvent("fingerUpdate", RoutingStrategy.Bubble, typeof(fingerUpdateHandler), typeof(TouchWindow));

            public delegate void fingerDownHandler( object sender, TouchEventArgs e );
            public delegate void fingerUpdateHandler( object sender, TouchEventArgs e );
            public delegate void fingerUpHandler( object sender, TouchEventArgs e );
            #endregion

            #region Constructor
            /// <summary>
            /// Standard public constructor for the window
            /// </summary>
            public TouchWindow()
            {
                try
                {
                    TouchEngine.Instance.registerClass(typeof(TouchWindow));
                    _fingerList = new TouchList();
                    this.fingerDown += new fingerDownHandler(TouchWindow_fingerDown);
                    this.fingerUp += new fingerUpHandler(TouchWindow_fingerUp);
                    this.fingerUpdate += new fingerUpdateHandler(TouchWindow_fingerUpdate);
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message + "\nStackTrace:\n" + e.StackTrace);
                }
                
                this.ContentRendered += new EventHandler(TouchWindow_ContentRendered);
            }
            #endregion

            #region Other
            void TouchWindow_ContentRendered(object sender, EventArgs e)
            {
                //starts touchlib when everything onscreen has been rendered
                TouchEngine.Instance.startTracking(this);
                this.Close();
            }
            #endregion

            #region Event Handlers
            /// <summary>
            /// This is called on the instance when it gets touched
            /// </summary>
            /// <param name="sender">The window that sent the event</param>
            /// <param name="e">The Custom touch event arguments</param>
            void TouchWindow_fingerDown( object sender, TouchEventArgs e )
            {
                _fingerList.Add(e.data);
            }

            /// <summary>
            /// This is called on the instance when it a finger is lifted from it
            /// </summary>
            /// <param name="sender">The window that sent the event</param>
            /// <param name="e">The Custom touch event arguments</param>
            void TouchWindow_fingerUp( object sender, TouchEventArgs e )
            {
                _fingerList.Remove(e.data);
            }

            /// <summary>
            /// This is called on the instance when it has been touched, and the finger
            /// was moved
            /// </summary>
            /// <param name="sender">The window that sent the event</param>
            /// <param name="e">The Custom touch event arguments</param>
            void TouchWindow_fingerUpdate( object sender, TouchEventArgs e )
            {
                
            }
            #endregion

            #region Add/Remove Event Handlers
            /// <summary>
            /// Add the fingerDown event to the class
            /// </summary>
            public event fingerDownHandler fingerDown
            {
                add { AddHandler(fingerDownEvent, value); }
                remove { RemoveHandler(fingerDownEvent, value); }
            }

            /// <summary>
            /// Add the fingerUpdate event to the class
            /// </summary>
            public event fingerUpdateHandler fingerUpdate
            {
                add { AddHandler(fingerUpdateEvent, value); }
                remove { RemoveHandler(fingerUpdateEvent, value); }
            }

            /// <summary>
            /// Add the fingerUp event to the class
            /// </summary>
            public event fingerUpHandler fingerUp
            {
                add { AddHandler(fingerUpEvent, value); }
                remove { RemoveHandler(fingerUpEvent, value); }
            }
            #endregion
        }
    }
}
