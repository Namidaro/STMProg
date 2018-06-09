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
            ProcessorList.Add("STM32F100Series", "stm32f103rbt6.cfg");
            ProcessorList.Add("STM32F400Series", "stm32f417vgt6.cfg");
        }    

    }
}
