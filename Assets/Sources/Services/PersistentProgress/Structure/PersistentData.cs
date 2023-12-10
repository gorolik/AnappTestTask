using System;
using UnityEngine.UI;

namespace Sources.Services.PersistentProgress.Structure
{
    [Serializable]
    public class PersistentData
    {
        public int Tickets = 0;
        public int CompletedLevel = -1;
        public string LastDailyBonusData = DateTime.Now.ToString();
        public int BonusDay = 0;
        public int[] Skins = new [] { 0 };
        public int[] Locations = new [] { 0 };
        
        public event Action<int> TicketsCountChanged;

        public void AddTickets(int count)
        {
            Tickets += count;
            TicketsCountChanged?.Invoke(Tickets);
        }

        public void AddItem(ref int[] array, int id)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = id;
        }
    }
}