using Steamworks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Humans
{
    public class CampaignParameters
    {
        public string Id { get; set; } // ??? check if this exists
        public CultureName SelectedCulture;
        public List<Human> Humans { get; set; } // humans that are defined for this campaign
    }
}
