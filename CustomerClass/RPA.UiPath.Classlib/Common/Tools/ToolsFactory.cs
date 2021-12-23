using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA.UiPath.Classlib.Common.Tools
{
    public class ToolsFactory
    {
        private static object _obj = new object();
        private static Logger _logger;
        public static Logger Logger
        {
            get
            {
                if (_logger == null)
                {
                    lock (_obj)
                    {
                        if (_logger == null)
                        {
                            _logger = new Logger();
                        }
                    }
                }
                return _logger;
            }
        }
    }
}
