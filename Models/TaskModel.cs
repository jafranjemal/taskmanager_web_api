using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManagerAPI.Models
{
    //Entites
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title must be between 1 and 100 characters", MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Due date is required")]
        [FutureDate(ErrorMessage = "Due date must be in the future")]
        public DateTime DueDate { get; set; }
    }

    // Custom validation attribute to ensure due date is in the future
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dueDate = Convert.ToDateTime(value);
            return dueDate > DateTime.Now;
        }
    }
}