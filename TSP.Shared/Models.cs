using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace TSP.Shared
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class PatchUpdate
    {
        public string op { get; set; }
        public string path { get; set; }
        public string value { get; set; }
    }
    public enum PatchUpdateItem
    {
        Name,
        Title,
        Paragraph,
        Image,
        Order,
        Disabled
    }
    public class PaginationModel
    {
        public int Page { get; set; } = 1;
        public int QuantityPerPage { get; set; } = 10;
    }
    public class PageDataModel<T>
    {
        public List<T> Data { get; set; }
        public int PageQuantity { get; set; }
    }
    public class SubSystemModel:BaseModel
    {
        public int Order { get; set; }
        public IList<SubMenuItemModel> SubMenuItems { get; set; }
    }
    public class SubMenuItemModel : BaseModel
    {
        public SubSystem SubSystem { get; set; }
        public int SubSystemId { get; set; }
        public int Order { get; set; }
        public string TabHeader { get; set; }
        public string TabDetail { get; set; }
        public string TabHeaderSelect { get; set; }
        public IList<SubItemDetailModel> SubItemDetails { get; set; }
    }
    public class SubItemDetailModel : BaseModel
    {
        public int SubMenuItemId { get; set; }
        public string Title { get; set; }
        public string Paragraph { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
        public bool Disabled { get; set; }
        public SubMenuItemModel SubMenuItem { get; set; }
        public string IsShowClass { get; set; } = "d-none";
    }
    public class ContactUsModel : BaseModel
    {
        [Required(ErrorMessage = "Please enter your email.")]
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(5000)]
        [Required(ErrorMessage = "Please enter your message.")]
        public string Message { get; set; }
    }
    public class UploadProductLinkModel
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
        public Stream Image { get; set; }
        public string ImageName { get; set; }
    }
    public class AddDetailModel
    {
        public int Id { get; set; }// work around
        public int MenuId { get; set; }
        [Required]
        public string Name { get; set; }
    }
    public class SampleAPIArgs
    {
        public string reCAPTCHAResponse { get; set; }
    }
}
