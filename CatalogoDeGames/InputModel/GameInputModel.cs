using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogoDeGames.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100,MinimumLength =3,ErrorMessage ="Invalid Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(100,MinimumLength =1,ErrorMessage ="Invalid Producer Name")]
        public string Producer { get; set; }
        [Required]
        [Range(1,1000, ErrorMessage ="Invalid Price")]
        public double Price { get; set; }
    }
}
