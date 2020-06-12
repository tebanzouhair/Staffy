using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EF_MVC.Models.Sttafy
{
    public class Entite
    {
        public int ID { get; set; }

        [Display(Name = "Nom de l'entité")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Directeur de l'entité")]
        public int? CollaborateurID { get; set; }

        [Display(Name = "Directeur")]
        public Collaborateur Directeur { get; set ; }

        [Display(Name = "Poles")]
        public ICollection<Pole> Poles { get; set; }

    }
}
