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
            /// Button class that responds to touch events
            /// </summary>
            public class TouchButton : Button
            {
                #region Properties
                //no drag/scale/rotate properties for a button as it can only be touched to press it

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
                public static readonly RoutedEvent fingerDownEvent = EventManager.RegisterRoutedEvent("fingerDown", RoutingStrategy.Bubble, typeof(fingerDownHandler), typeof(TouchButton));
                public static readonly RoutedEvent fingerUpEvent = EventManager.RegisterRoutedEvent("fingerUp", RoutingStrategy.Bubble, typeof(fingerUpHandler), typeof(TouchButton));
                public static readonly RoutedEvent fingerUpdateEvent = EventManager.RegisterRoutedEvent("fingerUpdate", RoutingStrategy.Bubble, typeof(fingerUpdateHandler), typeof(TouchButton));

                public delegate void fingerDownHandler(object sender, TouchEventArgs e);
                public delegate void fingerUpdateHandler(object sender, TouchEventArgs e);
                public delegate void fingerUpHandler(object sender, TouchEventArgs e);
                #endregion

                #region Contructors
                /// <summary>
                /// Standard Public Constructor
                /// 
                /// Sets up the event handlers
                /// </summary>
                public TouchButton()
                {
                    try
                    {
                        TouchEngine.Instance.registerClass(typeof(TouchButton));
                        _fingerList = new TouchList();
                        this.fingerDown += new fingerDownHandler(TouchButton_fingerDown);
                        this.fingerUp += new fingerUpHandler(TouchButton_fingerUp);
                        this.fingerUpdate += new fingerUpdateHandler(TouchButton_fingerUpdate);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message + "\nStackTrace:\n" + e.StackTrace);
                    }
                }

                /// <summary>
                /// Contructor that sets the button text
                /// </summary>
                /// <param name="content">Text to put in the button</param>
                public TouchButton(String content)
                {
                    try
                    {
                        TouchEngine.Instance.registerClass(typeof(TouchButton));
                        _fingerList = new TouchList();
                        this.fingerDown += new fingerDownHandler(TouchButton_fingerDown);
                        this.fingerUp += new fingerUpHandler(TouchButton_fingerUp);
                        this.fingerUpdate += new fingerUpdateHandler(TouchButton_fingerUpdate);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message + "\nStackTrace:\n" + e.StackTrace);
                    }

                    this.Content = content;
                }
                #endregion

                #region Event Handlers
                /// <summary>
                /// This is called on the instance when it gets touched
                /// </summary>
                /// <param name="sender">The button that sent the event</param>
                /// <param name="e">The Custom touch event arguments</param>
                void TouchButton_fingerDown(object sender, TouchEventArgs e)
                {
                    _fingerList.Add(e.data);
                }

                /// <summary>
                /// This is called on the instance when it a finger is lifted from it
                /// </summary>
                /// <param name="sender">The button that sent the event</param>
                /// <param name="e">The Custom touch event arguments</param>
                void TouchButton_fingerUp(object sender, TouchEventArgs e)
                {
                    _fingerList.Remove(e.data);
                }

                /// <summary>
                /// This is called on the instance when it has been touched, and the finger
                /// was moved
                /// </summary>
                /// <param name="sender">The button that sent the event</param>
                /// <param name="e">The Custom touch event arguments</param>
                void TouchButton_fingerUpdate(object sender, TouchEventArgs e)
                {
                    //no need to update the finger as it can only be pressed not dragged
                    
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
}
