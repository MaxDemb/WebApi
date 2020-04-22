using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class CategoriesView
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}