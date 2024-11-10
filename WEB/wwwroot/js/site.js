// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
setTimeout(() => {
    $(".notification").fadeOut("slow")
}, 3000)

$("#uploadStudentImage").on("change", function () {
    if (this.files && this.files[0]) {
        $("#imageStudent").attr("src", URL.createObjectURL(this.files[0]))
    }
})

function loadTeachersByCourseId(courseId, teacherSelectedId, classroomSelectedId = null) {
    console.log(courseId, teacherSelectedId, classroomSelectedId)
    if (courseId) {
        $.ajax({
            url: '/Education/Teachers/GetTeachersByCourseId',
            type: 'GET',
            data: { courseId: courseId },
            success: function (data) {
                // console.log(data)
                var teacherSelect = $("#" + teacherSelectedId);
                teacherSelect.removeAttr('disabled');
                teacherSelect.empty();
                if (classroomSelected !== null) {
                    var classroomSelected = $("#" + classroomSelectedId);
                    classroomSelected.removeAttr('disabled');
                    classroomSelected.empty();
                }
                if (data.length === 0) {
                    teacherSelect.append('<option disabled selected value="0">Bu kursta henüz eğitmen yoktur!</option>');
                    teacherSelect.attr('disabled', 'disabled');
                } else {
                    teacherSelect.append('<option disabled selected value="0">Lütfen eğitmen seçiniz!</option>');
                    $.each(data, function (index, teacher) {
                        teacherSelect.append(`<option value="${teacher.id}">${teacher.fullName}</option>`);
                    })
                }
                
            }
        })
    }
}
function loadClassroomsByTeacherId(teacherId, classroomSelectedId) {
    //console.log("ben geldim.")
    if (teacherId) {
        $.ajax({
            url: '/Education/Classrooms/GetClassroomsByTeacherId',
            type: 'GET',
            data: { teacherId: teacherId },
            success: function (data) {
                //console.log(data)
                var classroomSelect = $("#" + classroomSelectedId);
                classroomSelect.removeAttr('disabled');
                classroomSelect.empty();
                if (data.length === 0) {
                    classroomSelect.append('<option disabled selected value="0">Bu eğitmenin henüz sınıfı yoktur!</option>');
                    classroomSelect.attr('disabled', 'disabled')

                } else {
                    classroomSelect.append('<option disabled selected value="0">Lütfen sınıf seçiniz!</option>');
                    $.each(data, function (index, classroom) {
                        classroomSelect.append(`<option value="${classroom.id}">${classroom.name}</option>`);
                    })
                }
            }
        })
    }
}

