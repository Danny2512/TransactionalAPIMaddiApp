﻿using System.ComponentModel.DataAnnotations;

namespace TransactionalAPIMaddiApp.Models
{
    public class ProcedureViewModel
    {
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string Procedure { get; set; }
    }
}
