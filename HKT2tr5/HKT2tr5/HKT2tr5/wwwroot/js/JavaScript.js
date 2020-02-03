$(document).ready(function() {
    $('.nsx').change(function() {
        $('.getdongxe').show();
        $('.getmauxe').show();
    });
});

getXeByMauXe = function () {

};

getDongXes = function(e) {
    var id = $(e).val();
    $.ajax({
        url: '/Xes/GetDongXes/' + id,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
            if (data.status === 1) {
                var response = data.response;
                $('.DongXe').empty();
                $.each(response, function (index, value) {
                    $('.DongXe').append("<option value='" + value.id + "'>" + value.name + "</option>");
                    $('.getmauxe').show();
                    $.ajax({
                        url: '/Xes/GetMauXes/' + value.id,
                        method: 'GET',
                        dataType: 'json',
                        contentType: 'application/json',
                        success: function (data) {
                            if (data.status === 1) {
                                var response = data.response;
                                $('.MauXe').empty();
                                $.each(response, function (index, mauxe) {
                                    $('.MauXe').append("<option value='" + mauxe.id + "'>" + mauxe.tenMau + "</option>");
                                });
                            }
                        }
                    });
                });
                
            }
        }
    });
}

getMauXes = function (e) {
    $('#getmauxe').show();
    var id = $(e).val();
    $.ajax({
        url: '/Xes/GetMauXes/' + id,
        method: 'GET',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.status === 1) {
                var response = data.response;
                $('.MauXe').empty();
                $.each(response, function (index, value) {
                    $('.MauXe').append("<option value='" + value.id + "'>" + value.tenMau + "</option>");
                });
            }
        }
    });
}

searchAll = function () {
    $('.searchAllDetails').toggle();
};