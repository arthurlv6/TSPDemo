using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSPWebsite.Models
{
    public class SubItemDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubMenuItemId { get; set; }
        public string Title { get; set; }
        public string Paragraph { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
        public bool Disabled { get; set; }
    }
}
