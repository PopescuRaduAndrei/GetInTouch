using GetInTouch.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GetInTouch.Logic.ViewModels.History
{
    public class HistoryViewModel
    {
        public IEnumerable<HistoryModel> Histories { get; set; }
    }
}
