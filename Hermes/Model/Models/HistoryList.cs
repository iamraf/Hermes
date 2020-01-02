using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    class HistoryList
    {
        public int View { get; }
        public int Category { get; }

        public HistoryList(int view, int category)
        {
            View = view;
            Category = category;
        }
    }
}
