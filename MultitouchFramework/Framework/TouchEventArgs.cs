using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NUIGroup
{
    namespace MultitouchFramework
    {
        /// <summary>
        /// Custom Event Arguments class for use in Multitouch Event handlers
        /// </summary>
        public class TouchEventArgs : RoutedEventArgs
        {
            #region Properties
            /// <summary>
            /// Gets/Sets the TouchData value of the argument
            /// </summary>
            public TouchData data
            {
                get { return _data; }
                set { this._data = value; }
            }
            #endregion

            #region Private Members
            private TouchData _data;
            #endregion

            #region Constructor
            /// <summary>
            /// Standard public constructor
            /// </summary>
            /// <param name="data">TouchData for the Event Arguments</param>
            public TouchEventArgs(TouchData data)
            {
                this._data = data;
            }
            #endregion
        }
    }
}
