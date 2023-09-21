using System;
using System.ComponentModel.DataAnnotations;
using ToDoApp.Enums;

namespace ToDoApp.DTOs
{
    public class ToDoDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public Status Status { get; set; }

        public bool isDeleted { get; set; }

    }
}
