var banner = banner || {};



student.drawTable = function () {
    $.ajax({
        url: '/Home/Gets',
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                var response = data.response;
                $.each(response, function (index, value) {
                    $('#tbStudent').append("<tr>" +
                        "<td>" + value.studentId + "</td>" +
                        "<td>" + value.fullName + "</td>" +
                        "<td>" + value.sex + "</td>" +
                        "<td>" + value.dob + "</td>" +
                        "<td>" + value.className + "</td>" +
                        "<td>" +
                        "<a href='javascript:void(0);' class ='btn btn-dark' onclick=student.getDetail('" + value.studentId + "')>Edit </a>" +
                        "<a href='javascript:void(0);' class ='btn btn-danger'>Delete</a>" +
                        "</td>" +
                        "</tr>"
                    );
                });
            }
        }
    });
}

student.init = function () {
    student.drawTable();
}

$(document).ready(function () {
    student.init();
});