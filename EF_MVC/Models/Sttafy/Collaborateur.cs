using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_MVC.Models.Sttafy
{
    public class Collaborateur
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("FirstName")]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Display(Name = "Nom Complet")]
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }

        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress]
        [Display(Name = "Adresse Email")]
        public string EmailAddress { get; set; }

        public int DepartmentID;

        public Boolean IsManager;
    }
}