using System;
using System.Collections.Generic;
using System.Text;

using NUIGroup.MultitouchFramework;

namespace NUIGroup
{
    namespace MultitouchFramework
    {
        namespace Engines
        {
            /// <summary>
            /// TODO: Implement the interface properly
            /// </summary>
            interface ITouchListener
            {
                void onFingerDown(object sender, TouchEventArgs e);
                void onFingerUp(object sender, TouchEventArgs e);
                void onFingerUpdate(object sender, TouchEventArgs e);
            }
        }
    }
}
