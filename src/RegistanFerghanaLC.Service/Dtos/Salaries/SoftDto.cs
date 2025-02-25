﻿using RegistanFerghanaLC.Service.Common.Utils;
using RegistanFerghanaLC.Service.ViewModels.SalaryViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistanFerghanaLC.Service.Dtos.Salaries
{
    public class SoftDto
    {
        [Required(ErrorMessage = "Please enter the start date")]
        public string StartDate { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the end date")]
        public string EndDate { get; set; } = string.Empty;
        public PagedList<SalaryBaseViewModel>? Salaries { get; set; }
        public int ChoosenPage { get; set; } = 1;
    }
}
