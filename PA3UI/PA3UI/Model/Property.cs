using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PA3UI.ui
{
    public interface Property
    {
        /// <summary>
        /// Updates the visual part of properties on the board
        /// </summary>
        /// <param name="level">
        /// Valid values for:
        ///     Streets: -1 to 5
        ///     RailRoad -1 to 0
        /// </param>
        void SetDevelopmentOfProperty(int level);
    }
}
