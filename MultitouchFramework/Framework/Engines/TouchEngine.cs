using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using TouchlibWrapper;
using NUIGroup.MultitouchFramework.Controls;

using System.Diagnostics;

//done for now
namespace NUIGroup
{
    namespace MultitouchFramework
    {
        namespace Engines
        {
            /// <summary>
            /// Handles all touches, and registers all events
            /// 
            /// Note: This class is a singleton and provides registering/deregistering
            /// of classes from anywhere and delegates all touch events to relevant
            /// classes
            /// </summary>
            public sealed class TouchEngine
            {
                #region Private Members
                RoutedEvent[] _events = null;

                TouchData data;
                TouchEventArgs args;

                static TouchEngine _instance = null;
                static readonly object _padlock = new object();

                //the base window that'll be used for hit testing
                private UIElement _trackBase;

                //linked list holding the registered classes
                private LinkedList<Type> _classList = new LinkedList<Type>();

                //to map the touchdata received from touchlib to actual screen coordinates
                private double _screenHeight;
                private double _screenWidth;
                #endregion

                #region Public Members
                //delegates for events
                public delegate void fingerDownHandler(object sender, TouchEventArgs e);
                public delegate void fingerUpdateHandler(object sender, TouchEventArgs e);
                public delegate void fingerUpHandler(object sender, TouchEventArgs e);

                #endregion

                #region Constructor
                /// <summary>
                /// Private Constructor, singleton to enable registering of classes from 
                /// anywhere
                /// </summary>
                private TouchEngine()
                {
                    _screenHeight = SystemParameters.PrimaryScreenHeight;
                    _screenWidth = SystemParameters.PrimaryScreenWidth;

                    CsTI cs = CsTI.Instance();

                    cs.fingerDown += new TouchlibWrapper.fingerDownHandler(cs_fingerDown);
                    cs.fingerUpdate += new TouchlibWrapper.fingerUpdateHandler(cs_fingerUpdate);
                    cs.fingerUp += new TouchlibWrapper.fingerUpHandler(cs_fingerUp);
                }
                #endregion

                #region Singleton
                /// <summary>
                /// Returns instance of TouchEngine as it is a singleton class
                /// </summary>
                public static TouchEngine Instance
                {
                    get
                    {
                        lock (_padlock)
                        {
                            if (_instance == null)
                            {
                                _instance = new TouchEngine();
                            }
                            return _instance;
                        }
                    }
                }
                #endregion

                #region Event Handlers
                /// <summary>
                /// Handlers when a finger is pressed from touchlib
                /// </summary>
                /// <param name="ID">ID of finger</param>
                /// <param name="tagID">tagID of fiducial</param>
                /// <param name="X">x coordinate of finger</param>
                /// <param name="Y">y coordinate of finger</param>
                /// <param name="angle">TODO: get description</param>
                /// <param name="area">area of finger</param>
                /// <param name="height">height of fiducial</param>
                /// <param name="width">width of fiducial</param>
                /// <param name="dX">deltaX used for finger movement</param>
                /// <param name="dY">deltaY used for finger movement</param>
                void cs_fingerDown(int ID, int tagID, float X, float Y, float angle, float area, float height, float width, float dX, float dY)
                {
                    /*
                     * finds the element that was touched and sends
                     * the event to the correct element
                     */
                    UIElement elem = getTouchedElement(X * _screenWidth, Y * _screenHeight);
                    if (elem != null)
                    {
                        data = new TouchData(elem, ID, tagID, (float)(X * _screenWidth), (float)(Y * _screenHeight), angle, area, height, width, (float)(dX * _screenWidth), (float)(dY * _screenHeight));
                        args = new TouchEventArgs(data);
                        foreach (Type type in _classList)
                        {
                            if (elem.GetType() == type)
                            {
                                _events = EventManager.GetRoutedEventsForOwner(type);
                                for (int i = 0; i < _events.Length; i++)
                                {
                                    if (_events[i].Name == "fingerDown")
                                    {
                                        args.RoutedEvent = _events[i];
                                        try
                                        {
                                            elem.RaiseEvent(args);
                                        }

                                        catch(Exception ex)
                                        {
                                            //if an event was missed
                                            Debug.WriteLine(ex.StackTrace);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                /// <summary>
                /// Handlers when a finger is lifted from touchlib
                /// </summary>
                /// <param name="ID">ID of finger</param>
                /// <param name="tagID">tagID of fiducial</param>
                /// <param name="X">x coordinate of finger</param>
                /// <param name="Y">y coordinate of finger</param>
                /// <param name="angle">TODO: get description</param>
                /// <param name="area">area of finger</param>
                /// <param name="height">height of fiducial</param>
                /// <param name="width">width of fiducial</param>
                /// <param name="dX">deltaX used for finger movement</param>
                /// <param name="dY">deltaY used for finger movement</param>
                void cs_fingerUp(int ID, int tagID, float X, float Y, float angle, float area, float height, float width, float dX, float dY)
                {
                    /*
                     * finds the element that was touched and sends
                     * the event to the correct element
                     */

                    UIElement elem = getTouchedElement(X * _screenWidth, Y * _screenHeight);
                    if (elem != null)
                    {
                        data = new TouchData(elem, ID, tagID, (float)(X * _screenWidth), (float)(Y * _screenHeight), angle, area, height, width, (float)(dX * _screenWidth), (float)(dY * _screenHeight));
                        args = new TouchEventArgs(data);
                        foreach (Type type in _classList)
                        {
                            if (elem.GetType() == type)
                            {
                                _events = EventManager.GetRoutedEventsForOwner(type);
                                for (int i = 0; i < _events.Length; i++)
                                {
                                    if (_events[i].Name == "fingerUp")
                                    {
                                        args.RoutedEvent = _events[i];
                                        try
                                        {
                                            elem.RaiseEvent(args);
                                        }

                                        catch(Exception ex)
                                        {
                                            //if an event was missed
                                            Debug.WriteLine(ex.StackTrace);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                /// <summary>
                /// Handlers when a finger is moved from touchlib
                /// </summary>
                /// <param name="ID">ID of finger</param>
                /// <param name="tagID">tagID of fiducial</param>
                /// <param name="X">x coordinate of finger</param>
                /// <param name="Y">y coordinate of finger</param>
                /// <param name="angle">TODO: get description</param>
                /// <param name="area">area of finger</param>
                /// <param name="height">height of fiducial</param>
                /// <param name="width">width of fiducial</param>
                /// <param name="dX">deltaX used for finger movement</param>
                /// <param name="dY">deltaY used for finger movement</param>
                void cs_fingerUpdate(int ID, int tagID, float X, float Y, float angle, float area, float height, float width, float dX, float dY)
                {
                    /*
                     * finds the element that was touched and sends
                     * the event to the correct element
                     */

                    UIElement elem = getTouchedElement(X * _screenWidth, Y * _screenHeight);
                    if (elem != null)
                    {
                        data = new TouchData(elem, ID, tagID, (float)(X * _screenWidth), (float)(Y * _screenHeight), angle, area, height, width, (float)(dX * _screenWidth), (float)(dY * _screenHeight));
                        args = new TouchEventArgs(data);
                        foreach (Type type in _classList)
                        {
                            if (elem.GetType() == type)
                            {
                                _events = EventManager.GetRoutedEventsForOwner(type);
                                for (int i = 0; i < _events.Length; i++)
                                {
                                    if (_events[i].Name == "fingerUpdate")
                                    {
                                        args.RoutedEvent = _events[i];
                                        try
                                        {
                                            elem.RaiseEvent(args);
                                        }
                                        
                                        catch(Exception ex)
                                        {
                                            //if an event was missed
                                            Debug.WriteLine(ex.StackTrace);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Register/Deregister
                /// <summary>
                /// Registers the class for receiving touch events
                /// </summary>
                /// <param name="type">The class type to be registered</param>
                public void registerClass(Type type)
                {
                    if (!_classList.Contains(type))
                    {
                        _classList.AddLast(type);
                    }
                }

                /// <summary>
                /// Deregisters the class for receiving touch events
                /// </summary>
                /// <param name="type">The class type to be deregistered</param>
                public void deregisterClass(Type type)
                {
                    if (_classList.Contains(type))
                    {
                        _classList.Remove(type);
                    }
                }
                #endregion

                #region Start Tracking
                /// <summary>
                /// Starts Touchlib. Must be called AFTER content (in window) has been
                /// rendered
                /// </summary>
                /// <param name="trackBase">The element that'll be the base for hit testing</param>
                public void startTracking(UIElement trackBase)
                {
                    Debug.WriteLine("CLASSES REGISTERED : " + _classList.Count);
                    _trackBase = trackBase;
                    CsTI cs = CsTI.Instance();
                    try
                    {
                        cs.startScreen();
                    }
                    //Throws a accessviolationexception for some reason
                    //from the CsTI module, I'll fix it soon
                    catch (AccessViolationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                #endregion

                #region Helper Methods
                /// <summary>
                /// HitTest the application and find the Element that was touched
                /// </summary>
                /// <param name="x">X coordinate of the touch</param>
                /// <param name="y">Y coordinate of the touch</param>
                /// <returns>The UIElement touched, null if no element found</returns>
                UIElement getTouchedElement(double x, double y)
                {
                    UIElement temp = null;

                    Point pt = new Point(Math.Floor(x), Math.Floor(y));

                    HitTestResult result = VisualTreeHelper.HitTest(_trackBase, pt);

                    if (result != null)
                    {
                        DependencyObject obj = (DependencyObject)result.VisualHit;
                        temp = findTouchedUIElement((UIElement)obj);
                        //Debug.WriteLine("TOUCHED ELEMENT: " + temp.GetType().ToString());
                    }
                    return temp;
                }

                /// <summary>
                /// Recursively finds the first touch control by walking up the visual
                /// tree from the top element touched which in most cases won't be a touch control
                /// </summary>
                /// <param name="el">The original element touched</param>
                /// <returns>The actual touch control</returns>
                private UIElement findTouchedUIElement(UIElement el)
                {
                    if ((UIElement)VisualTreeHelper.GetParent(el) != null)
                    {
                        if ((el.GetType().ToString().Contains("Touch")))
                        {
                            return el;
                        }
                        else return findTouchedUIElement((UIElement)VisualTreeHelper.GetParent(el));
                    }
                    return null;
                }
                #endregion
            }
        }
    }
}
