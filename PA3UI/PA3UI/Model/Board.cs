using PA3BackEnd.src.Monopoly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PA3UI.ui
{
    public partial class Board : UserControl
    {
        private Fields fields;
        
        private void Initialize() 
        {
            fields = new Fields();
        }
    }
}
