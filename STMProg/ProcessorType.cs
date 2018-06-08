using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace STMProg
{
    public class ProcessorType
    {
        public SortedList<string, string> ProcessorList = new SortedList<string, string>();

        public ProcessorType()
        {
            ProcessorList.Add("STM103", "stm32f103rbt6.cfg");
            ProcessorList.Add("STM417", "stm32f417vgt6.cfg");
        }    





    }
}
