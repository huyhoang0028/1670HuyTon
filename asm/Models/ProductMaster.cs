namespace asm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("ProductMaster")]
    public partial class ProductMaster
    {
        public ProductMaster()
        {
            Image = "~/Content/img/add.png";
        }
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        public double Price { get; set; }

        [Required]
        public string Descriptions { get; set; }

        [Required]
        [Display(Name ="Select Image")]
        public string Image { get; set; }

        public int Available { get; set; }


        // Use to post image to database
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
    public class ProductDbcontext : DbContext
    {

        public ProductDbcontext()
            : base("DefaultConnection")
        {

        }

        public static ProductDbcontext Create()
        {
            return new ProductDbcontext();
        }
        public DbSet<ProductMaster> ProductMasters { get; set; }

        public System.Data.Entity.DbSet<asm.Models.Cart> Carts { get; set; }
    }
}
