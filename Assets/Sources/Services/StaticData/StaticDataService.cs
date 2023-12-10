using System.Collections.Generic;
using System.Linq;
using Sources.StaticData.UI;
using Sources.UI;
using UnityEngine;

namespace Sources.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowId, WindowConfig> _windows;
        
        public void LoadData() => 
            LoadWindowsData();

        public WindowConfig GetWindowById(WindowId id) => 
            _windows.TryGetValue(id, out WindowConfig data) ? data : null;

        private void LoadWindowsData()
        {
            _windows = Resources
                .Load<WindowsStaticData>("StaticData/Windows/WindowsData")
                .Windows
                .ToDictionary(x => x.WindowId, x => x);
        }
    }
}