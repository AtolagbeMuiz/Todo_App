using System.ComponentModel.DataAnnotations;
using System;
using ToDoAPI.Enums;

namespace ToDoAPI.DTOs
{
    public class ToDoDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }
        public bool isDeleted { get; set; }

    }
}
