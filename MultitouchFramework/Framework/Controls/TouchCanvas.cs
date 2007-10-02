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
        namespace Controls
        {
            /// <summary>
            /// Canvas class that responds to touch events
            /// </summary>
            public class TouchCanvas : Canvas
            {
                #region Properties
                /// <summary>
                /// Sets/Gets if the instance can be dragged
                /// </summary>
                public bool isDraggable
                {
                    get { return _draggable; }
                    set { _draggable = value; }
                }

                /// <summary>
                /// Sets/Gets if the instance can be rotated
                /// </summary>
                public bool isRotateable
                {
                    get { return _rotateable; }
                    set { _rotateable = value; }
                }
                /// <summary>
                /// Sets/Gets if the instance can be scaled
                /// </summary>
                public bool isScalable
                {
                    get { return _scaleable; }
                    set { _scaleable = value; }
                }

                /// <summary>
                /// Returns the amount of fingers on the element
                /// </summary>
                public int fingerCount
                {
                    get { return _fingerList.Count; }
                }
                #endregion

                #region Private Members
                //private properties
                private bool _draggable = true;
                private bool _rotateable = true;
                private bool _scaleable = true;

                //list of fingers on this canvas
                private TouchList _fingerList = new TouchList();

                //transform tools
                private ScaleTransform _scaleTrans = new ScaleTransform();
                private RotateTransform _rotateTrans = new RotateTransform();
                private TranslateTransform _transTransform = new TranslateTransform();
                private TransformGroup _transGroup = new TransformGroup();
                #endregion

                #region Public Members
                //eventhandler variables
                public static readonly RoutedEvent fingerDownEvent = EventManager.RegisterRoutedEvent("fingerDown", RoutingStrategy.Bubble, typeof(fingerDownHandler), typeof(TouchCanvas));
                public static readonly RoutedEvent fingerUpEvent = EventManager.RegisterRoutedEvent("fingerUp", RoutingStrategy.Bubble, typeof(fingerUpHandler), typeof(TouchCanvas));
                public static readonly RoutedEvent fingerUpdateEvent = EventManager.RegisterRoutedEvent("fingerUpdate", RoutingStrategy.Bubble, typeof(fingerUpdateHandler), typeof(TouchCanvas));

                public delegate void fingerDownHandler(object sender, TouchEventArgs e);
                public delegate void fingerUpdateHandler(object sender, TouchEventArgs e);
                public delegate void fingerUpHandler(object sender, TouchEventArgs e);
                #endregion

                #region Contructor
                /// <summary>
                /// Standard Public Constructor
                /// 
                /// Sets up the event handlers
                /// </summary>
                public TouchCanvas()
                {
                    try
                    {
                        TouchEngine.Instance.registerClass(typeof(TouchCanvas));
                        _fingerList = new TouchList();
                        this.fingerDown += new fingerDownHandler(TouchCanvas_fingerDown);
                        this.fingerUp += new fingerUpHandler(TouchCanvas_fingerUp);
                        this.fingerUpdate += new fingerUpdateHandler(TouchCanvas_fingerUpdate);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message + "\nStackTrace:\n" + e.StackTrace);
                    }
                }
                #endregion

                #region Event Handlers
                /// <summary>
                /// This is called on the instance when it gets touched
                /// </summary>
                /// <param name="sender">The canvas that sent the event</param>
                /// <param name="e">The Custom touch event arguments</param>
                void TouchCanvas_fingerDown(object sender, TouchEventArgs e)
                {
                    _fingerList.Add(e.data);
                }
                
                /// <summary>
                /// This is called on the instance when it a finger is lifted from it
                /// </summary>
                /// <param name="sender">The canvas that sent the event</param>
                /// <param name="e">The Custom touch event arguments</param>
                void TouchCanvas_fingerUp(object sender, TouchEventArgs e)
                {
                    _fingerList.Remove(e.data);
                }

                /// <summary>
                /// This is called on the instance when it has been touched, and the finger
                /// was moved
                /// </summary>
                /// <param name="sender">The canvas that sent the event</param>
                /// <param name="e">The Custom touch event arguments</param>
                void TouchCanvas_fingerUpdate(object sender, TouchEventArgs e)
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

                #region Helper Methods
                //move of grid
                ///TODO: move grid
                ///TODO: add touch areas
                #endregion
            }
        }
    }
}
