using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterStats.Model
{
    public class PlayerDataRepository
    {
        public List<PlayerItem> ListPlayer = new List<PlayerItem>();
        public ObservableCollection<PlayerItem> PlayerInfo { get; set; }

        public PlayerDataRepository()
        {
            PlayerInfo = GetPlayerInfo();
        }

        public ObservableCollection<PlayerItem> GetPlayerInfo()
        {
            var data = new ObservableCollection<PlayerItem>();
            foreach (var p in ListPlayer)
            {
                data.Add(new PlayerItem()
                {
                    Player = p.Player,
                    Count = p.Count
                });
            }
            return data;
        }
    }
}
