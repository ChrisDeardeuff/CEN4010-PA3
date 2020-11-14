using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PA3UI.ui
{
    public abstract class Property : UserControl
    {
        /// <summary>
        /// Updates the visual part of properties on the board
        /// </summary>
        /// <param name="level">
        /// Valid values for:
        ///     Streets: -1 to 5
        ///     RailRoad -1 to 0
        ///     For the rest not valid
        ///     This method has no checking if the value is valid and only updates the visual on the board</param>
        public abstract void SetDevelopmentOfProperty(int level);
    }
}
