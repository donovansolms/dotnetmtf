using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NUIGroup
{
    namespace MultitouchFramework
    {
        /// <summary>
        /// The .NET version of the Touchlib TouchData struct
        /// (including the object the finger touched)
        /// </summary>
        public class TouchData
        {

            #region Properties
            /// <summary>
            /// The element touched by the finger
            /// </summary>
            public UIElement touchedElement
            {
                get { return _touchedElement; }
                set { this._touchedElement = value; }
            }
            /// <summary>
            /// The ID of the finger
            /// </summary>
            public int ID
            {
                get { return _ID; }
                set { this._ID = value; }
            }
            /// <summary>
            /// The tagID of the fiducial
            /// </summary>
            public int tagID
            {
                get { return _tagID; }
                set { this._tagID = value; }
            }
            /// <summary>
            /// The X coordinate of the finger
            /// </summary>
            public float X
            {
                get { return _X; }
                set { this._X = value; }

            }
            /// <summary>
            /// The Y coordinate of the finger
            /// </summary>
            public float Y
            {
                get { return _Y; }
                set { this._Y = value; }
            }
            /// <summary>
            /// TODO: get description
            /// </summary>
            public float angle
            {
                get { return _angle; }
                set { this._angle = value; }
            }
            /// <summary>
            /// The area of the finger
            /// </summary>
            public float area
            {
                get { return _area; }
                set { this._area = value; }
            }
            /// <summary>
            /// The height of the fiducial
            /// </summary>
            public float height
            {
                get { return _height; }
                set { this._height = value; }
            }
            /// <summary>
            /// The width of the fiducial
            /// </summary>
            public float width
            {
                get { return _width; }
                set { this._width = value; }
            }
            /// <summary>
            /// The deltaX for finger movement
            /// </summary>
            public float dX
            {
                get { return _dX; }
                set { this._dX = value; }
            }
            /// <summary>
            /// The deltaY for finger movement
            /// </summary>
            public float dY
            {
                get { return _dY; }
                set { this._dY = value; }
            }
            #endregion

            #region Private Members
            private UIElement _touchedElement;

            private int _ID;
            private int _tagID;
            private float _X;
            private float _Y;
            private float _angle;
            private float _area;
            private float _height;
            private float _width;
            private float _dX;
            private float _dY;
            #endregion

            #region Constructor
            /// <summary>
            /// Standard public constructor
            /// </summary>
            /// <param name="elem">The element the finger touched</param>
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
            public TouchData(UIElement elem, int ID, int tagID, float X, float Y, float angle, float area, float height, float width, float dX, float dY)
            {
                this._touchedElement = elem;
                this._ID = ID;
                this._tagID = tagID;
                this._X = X;
                this._Y = Y;
                this._angle = angle;
                this._area = area;
                this._height = height;
                this._width = width;
                this._dX = dX;
                this._dY = dY;
            }
            #endregion

            #region Overrides
            /// <summary>
            /// Returns the string representation of the data
            /// </summary>
            /// <returns>The string</returns>
            public override string ToString()
            {
                return "Finger ID: " + ID + " X: " + X + " Y: " + Y;
            }
            #endregion

        }
    }
}
