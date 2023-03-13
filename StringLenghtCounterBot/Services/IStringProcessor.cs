using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringLenghtCounterBot.Services
{
    internal interface IStringProcessor
    {
        string ProcessString(string operationCode, string inputString);
    }
}
