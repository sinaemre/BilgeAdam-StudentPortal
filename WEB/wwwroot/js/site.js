setTimeout(() => {
    $(".notification").fadeOut("slow")
}, 3000)

setInterval(() => {
    loadEarnings()
}, 1000)

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
async function setClassroomEndDate(inputElement, endDateId) {
    if (endDateId) {
        try {
            var startDate = new Date(inputElement.value);
            var courseId = $("#course-select").val();
            //console.log("CourseId => " + courseId)
            if (!courseId) {
                console.error("Course ID bulunamadı!");
                return;
            }
            const response = await fetch(`/Education/Courses/GetCourseDuration/${courseId}`);
            if (!response.ok) {
                console.error("Hata oluştu!");
                return;
            }
            const totalDay = await response.json();
            const endDate = new Date(startDate);
            endDate.setDate(endDate.getDate() + totalDay);
            const formattedEndDate = endDate.toISOString().split('T')[0];
            $(`#${endDateId}`).val(formattedEndDate);

        } catch (e) {
            console.log(e.error);
        }
    }
}
function changeStartDateAndEndDateStatus(startDateId, endDateId) {
    var startDateInput = $("#" + startDateId);
    startDateInput.removeAttr('disabled');
    startDateInput.empty();

    var endDateInput = $("#" + endDateId);
    endDateInput.removeAttr('disabled');
    endDateInput.empty();
}
async function loadEarnings() {
    const response = await fetch(`/Education/Courses/GetAllCourses`);
    if (!response.ok) {
        console.error("Hata oluştu!");
        return;
    }

    const courses = await response.json();

    var earnings = $("#earnings");
    earnings.empty();

    $.each(courses, function (index, course) {
        earnings.append(`<div class="col-xl-3 col-md-6 mb-4">
        <div class="card border-left-primary shadow h-100 py-2">
            <div class="card-body">
                <div class="row no-gutters align-items-center">
                    <div class="col mr-2">
                        <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                            ${course.name}
                        </div>
                        <div class="h5 mb-0 font-weight-bold text-gray-800">$${course.totalEarning}</div>
                    </div>
                    <div class="col-auto">
                        <i class="fas fa-calendar fa-2x text-gray-300"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>`);
    })

}

//according to loftblog tut
$('.nav li:first').addClass('active');

var showSection = function showSection(section, isAnimate) {
    var
        direction = section.replace(/#/, ''),
        reqSection = $('.section').filter('[data-section="' + direction + '"]'),
        reqSectionPos = reqSection.offset().top - 0;

    if (isAnimate) {
        $('body, html').animate({
            scrollTop: reqSectionPos
        },
            800);
    } else {
        $('body, html').scrollTop(reqSectionPos);
    }

};

var checkSection = function checkSection() {
    $('.section').each(function () {
        var
            $this = $(this),
            topEdge = $this.offset().top - 80,
            bottomEdge = topEdge + $this.height(),
            wScroll = $(window).scrollTop();
        if (topEdge < wScroll && bottomEdge > wScroll) {
            var
                currentId = $this.data('section'),
                reqLink = $('a').filter('[href*=\\#' + currentId + ']');
            reqLink.closest('li').addClass('active').
                siblings().removeClass('active');
        }
    });
};

$('.main-menu, .responsive-menu, .scroll-to-section').on('click', 'a', function (e) {
    e.preventDefault();
    showSection($(this).attr('href'), true);
});

$(window).scroll(function () {
    checkSection();
});