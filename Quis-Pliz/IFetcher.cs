using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quis_Pliz
{
    internal interface IFetcher
    {
        public QuizPart[] fetchDate();
    }
}
