using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalloFlix.Models;

[Table("Movie")]
public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MyProperty { get; set; }

    [Display(Name ="Titulo Original")]
    [Required(ErrorMessage ="Por favor,informe o título original")]
    [StringLength(100,ErrorMessage ="Otítulo original deve possuir no maximo 100 caracteres")]
    public string OriginalTitle { get; set; }

    
    [Display(Name ="sinopse ")]
    [StringLength(8000,ErrorMessage ="A sinopse deve conter no máximo 8000 caracteres")]
    public string Synopsis { get; set; }
      
    [Column(TypeName = "Year")]
    [Display(Name ="Ano de Estreia")]
    [Required(ErrorMessage ="Por favor,informe o ano de estreia do filme")]
    public Int16 MovieYear { get; set; }

   [Display (Name="Duração (Em minutos)")]
   [Required(ErrorMessage="Por favor, informe a duração")]

   public Int16 Duration {get;set;}

   [Display(Name ="Classificação Etária")]
   [Required(ErrorMessage = "Por Favor,informe a classificação etária")]
   public byte AgeRating {get;set;}
  
    [StringLength(200)]
    [Display(Name = "Foto")]
    public string Image {get;set;}

    [NotMapped]
    [Display(Name = "Duração")]

    public string HourDuration {get {
        return TimeSpan.FromMinutes(Duration).ToString(@"%h'h 'm'min'");
    }}
      public ICollection<MovieGenre> Genres {get;set;}
}