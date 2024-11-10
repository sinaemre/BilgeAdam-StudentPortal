﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Classrooms
{
    public class UpdateClassroomVM
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Display(Name = "Sınıf")]
        public string? Name { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Eğitmen")]
        public Guid? TeacherId { get; set; }

        [Display(Name = "Kurs")]
        public Guid? CourseId { get; set; }

        public SelectList? Courses { get; set; }
        public SelectList? Teachers { get; set; }
    }
}
