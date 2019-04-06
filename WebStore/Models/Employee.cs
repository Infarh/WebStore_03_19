﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Models
{
    public class Employee
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя"), Required(ErrorMessage = "Имя является обязательным")]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия"), Required(ErrorMessage = "Фамилия является обязательной")]
        [MinLength(2)]
        public string SurName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }
    }
}
