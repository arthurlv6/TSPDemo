using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TSP.Shared
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        public T ToModel<T>(IMapper mapper) where T : BaseModel
        {
            return mapper.Map<T>(this);
        }
    }
    public class SubSystem:BaseEntity
    {
        public IList<SubMenuItem> SubMenuItems { get; set; }
        // tsp, revlution, greenhaven and staff
        public int Order { get; set; }
    }
    public class SubMenuItem : BaseEntity
    {
        public int SubSystemId { get; set; }
        public IList<SubItemDetail> SubItemDetails { get; set; }
        // for tsp we have home, team, gallery, about us and current project
        public int Order { get; set; }
    }
    public class SubItemDetail : BaseEntity
    {
        public int SubMenuItemId { get; set; }
        [MaxLength(1000)]
        public string Title { get; set; }
        [MaxLength(10000)]
        public string Paragraph { get; set; }
        [MaxLength(1000)]
        public string Image { get; set; }
        public int Order { get; set; }
        public bool Disabled { get; set; }
    }
    public class ContactUs: BaseEntity
    {
        [Required(ErrorMessage = "Please enter your email.")]
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(5000)]
        [Required(ErrorMessage = "Please enter your message.")]
        public string Message { get; set; }
    }
}
