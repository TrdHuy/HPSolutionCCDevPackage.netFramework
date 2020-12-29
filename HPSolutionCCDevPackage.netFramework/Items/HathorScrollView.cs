using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HPSolutionCCDevPackage.netFramework
{
    public class HathorScrollView : ScrollViewer
    {
        public HathorScrollView()
        {
            DefaultStyleKey = typeof(HathorScrollView);
            IsTabStop = false;
        }
    }
}
