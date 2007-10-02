using System;
using System.Collections.Generic;
using System.Text;

namespace NUIGroup
{
    namespace MultitouchFramework
    {
        /// <summary>
        /// Custom LinkedList for TouchData to avoid having to code the remove
        /// in every file
        /// </summary>
        public class TouchList
        {
            #region Properties
            /// <summary>
            /// Returns the amount of fingers in the list
            /// </summary>
            public int Count
            {
                get { return _fingerList.Count; }
            }
            #endregion

            #region Private Members
            //NOTE: This is a quick fix till I move TouchData into an IEnumerable class
            private LinkedList<TouchData> _fingerList;
            #endregion

            #region Constructor
            /// <summary>
            /// Standard public constructor
            /// </summary>
            public TouchList()
            {
                _fingerList = new LinkedList<TouchData>();
            }
            #endregion

            #region Methods
            /// <summary>
            /// Removes a finger from the list
            /// </summary>
            /// <param name="remove">The finger to remove</param>
            public void Remove(TouchData remove)
            {
                foreach (TouchData data in _fingerList)
                {
                    if (remove.ID == data.ID)
                    {
                        _fingerList.Remove(data);
                        break;
                    }
                }
            }

            /// <summary>
            /// Adds a finger to the list
            /// </summary>
            /// <param name="add">The finger to add</param>
            public void Add(TouchData add)
            {
                _fingerList.AddLast(add);
            }

            /// <summary>
            /// Checks if a finger is in the list
            /// </summary>
            /// <param name="contains">The finger to check</param>
            /// <returns>True if list contains the finger, otherwise false</returns>
            public bool Contains(TouchData contains)
            {
                foreach (TouchData data in _fingerList)
                {
                    if (contains.ID == data.ID)
                    {
                        return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// Finds a finger based on ID
            /// </summary>
            /// <param name="data">The finger to be searched for</param>
            /// <returns>The finger data if found, if not null</returns>
            public TouchData FindFinger(TouchData find)
            {
                if (_fingerList.Count != 0)
                {
                    foreach (TouchData data in _fingerList)
                    {
                        if (find.ID == data.ID)
                        {
                            return data;
                        }
                    }
                }
                return null;
            }

            /// <summary>
            /// Updates the x and y coordinates of the finger in the list
            /// </summary>
            /// <param name="update">The finger to update</param>
            public void UpdateFinger(TouchData update)
            {
                TouchData data = FindFinger(update);
                Remove(data);
                data.X = update.X;
                data.Y = update.Y;
                data.touchedElement = update.touchedElement;
                data.width = update.width;
                data.height = update.height;
                Add(data);
            }
            #endregion
        }
    }
}
