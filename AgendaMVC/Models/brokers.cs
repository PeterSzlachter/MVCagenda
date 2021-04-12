//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgendaMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public partial class brokers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public brokers()
        {
            this.appointments = new HashSet<appointments>();
        }

        public IEnumerable<SelectListItem> BrokerList { get; set; }
        public int idBroker { get; set; }
        [Required(ErrorMessage = "Erreur ! Entrer un nom")]
        [Display(Name = "Nom")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Erreur ! Entrer un prénom")]
        [Display(Name = "Prénom")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "Erreur ! Entrer une adresse mail valide")]
        [EmailAddress]
        [Display(Name = "Courriel")]
        public string mail { get; set; }

        [Required(ErrorMessage = "Erreur ! Entrer un numéro de téléphone valide")]
        [Phone]
        [Display(Name = "Numéro de téléphone")]
        public string phoneNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointments> appointments { get; set; }
    }
}