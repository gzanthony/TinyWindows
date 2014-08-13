using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TinyWindows
{
    class ThreadConvert
    {
        private delegate void FlushClient();
        public static int count = 0;

        public void start()
        {
            Thread thread = new Thread(CrossThreadFlush);
        }

        private void CrossThreadFlush()
        {
            while (true)
            {
                
            }
        }

        private void ThreadFunction()
        {

        }
    }
}
