//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeethCabinet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Consultation
    {
        public int ConsultationID { get; set; }               
        public int PatientID { get; set; }        
        public int PersonnelID { get; set; }
        public string Type { get; set; }
        public string Observation { get; set; }
        public Nullable<System.DateTime> DateConsultation { get; set; }
    
        public virtual Patient Patient { get; set; }
        public virtual Personnel Personnel { get; set; }
    }
}