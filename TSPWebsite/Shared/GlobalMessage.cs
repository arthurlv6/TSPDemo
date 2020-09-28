using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSPWebsite
{
    public class GlobalMessage
    {
        public event Action OnChange;
        public string Show { get; set; } = "d-block";
        public void SetMessage(string val)
        {
            Show = val;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
