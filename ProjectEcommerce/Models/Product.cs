using System.ComponentModel.DataAnnotations;

namespace ProjectEcommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display( Name = "Nombre" )]
        [Required]
        public string Name { get; set; }
        [Display( Name = "Descripción" )]
        public string? Description { get; set; }
        [Required]
        [Range( 0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero" )]
        [Display( Name = "Precio" )]
        [DisplayFormat( DataFormatString = "{0:C}", ApplyFormatInEditMode = false )]
        public decimal Price { get; set; }
        [Display( Name = "Imagen" )]
        public string? ImageUrl { get; set; }
        [Display( Name = "Cantidad" )]
        public int Stock { get; set; }
        [Display( Name = "Ranking" )]
        public float Rank { get; set; }

        [Display( Name = "Cantidad a eliminar del stock" )]
        public int ReduceStock { get; set; } = 0;
    }
}
