using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoListAsync.Models
{
    public partial class ToDoListTable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="待辦事項")]
        public string Task { get; set; } = null!;
    }
}
