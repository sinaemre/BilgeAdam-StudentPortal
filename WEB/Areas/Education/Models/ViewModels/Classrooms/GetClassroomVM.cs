﻿using ApplicationCore.Consts;

namespace WEB.Areas.Education.Models.ViewModels.Classrooms
{
    public class GetClassroomVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string TeacherName { get; set; }
        public int ClassroomSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}