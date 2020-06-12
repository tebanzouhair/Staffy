using System.ComponentModel.DataAnnotations;

namespace EF_MVC.Models.Sttafy
{
    public class Pole
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Nom du Départment")]
        public string Name { get; set; }
        [Display(Name = "Entité")]
        public int EntiteID { get; set; }
        [Required]
        [Display(Name = "Manager")]
        public int? CollaborateurID { get; set; }

        public Collaborateur Manager { get; set; }
    }
}