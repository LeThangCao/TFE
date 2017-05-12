using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasterStats.Model
{
    public class PlatFormDataRepository
    {
        public List<PlatformsItem> ListPlatform = new List<PlatformsItem>();
        public ObservableCollection<PlatformsItem> PlatFormInfo { get; set; }

        public PlatFormDataRepository()
        {
            PlatFormInfo = GetPlatFormInfo();
        }

        public ObservableCollection<PlatformsItem> GetPlatFormInfo()
        {
            var data = new ObservableCollection<PlatformsItem>();
            foreach (var p in ListPlatform)
            {
                data.Add(new PlatformsItem()
                {
                    PlayerPlatForm = p.PlayerPlatForm,
                    Count = p.Count
                });
            }
            return data;
        }
    }
}
