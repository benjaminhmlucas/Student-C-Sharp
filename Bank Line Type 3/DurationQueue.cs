using System;
using System.Collections.Generic;
using System.Text;

namespace LucasProgram3_Multi_WaitTime {
    public class DurationQueue<T> : Queue<T> {
        public int totalLineDuration { get; set; }

        public DurationQueue() {
            Queue<T> custQ = new Queue<T>();
            totalLineDuration = 0;
        }
    }
}
