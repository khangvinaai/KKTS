var t = 2;

var t1 = 6;

var dtt;

$.get("/DM_PhuongXa/GetTinhThanh/", (data) => {
    $("#Ma_TinhThanh").append(`<option value = "">Chọn</option>`)
    for (let i = 0; i < t1; i++) {
        $(`#Ma_TinhThanhTN${i}`).append(`<option value = "">Chọn</option>`)
    }

    $.each(data, (index, value) => {
        $("#Ma_TinhThanh").append(`<option value ="${value.MaTinhThanh}">${value.TenTinhThanh}</option>`)
        for (let i = 0; i < t1; i++) {
            $(`#Ma_TinhThanhTN${i}`).append(`<option value ="${value.MaTinhThanh}">${value.TenTinhThanh}</option>`)

        }
    })
})

$.get("/DM_CoQuanDonVi/GetCoQuan/", (data) => {
    $("#Ma_CoQuan_DonVi").append(`<option value = "">Chọn</option>`)

    $.each(data, (index, value) => {
        $("#Ma_CoQuan_DonVi").append(`<option value ="${value.MaCoQuan}">${value.TenCoQuan}</option>`)
    })
})

$.get("/DM_ChucVu_ChucDanh/GetChucVu/", (data) => {
    $("#Ma_ChucVu_ChucDanh").append(`<option value = "">Chọn</option>`)

    $.each(data, (index, value) => {
        $("#Ma_ChucVu_ChucDanh").append(`<option value ="${value.MaChucVu}">${value.TenChucVu}</option>`)
    })
})

$("#clsAddRow").click(() => {
    $(`.tn${t}`).removeClass("d-none");
    t++;
})

//chức năng Tìm kiếm 
$("#search_btn").click(() => {
    dtt.columns(0).search($('#Filter_TenCanBo').val());
    dtt.draw()
})
$("#Filter_TenCanBo").keypress(function (e) {
    if (e.keyCode == 13) {
        dtt.columns(0).search($('#Filter_TenCanBo').val());
        //if (dtt.columns(0).search($("#Filter_TenCanBo").val()) != null)
        //    dtt.columns(0).search($("#Filter_TenCanBo").val());
        //else {

        //}
        dtt.draw();
    }
});

function loadDataTable() {
    dtt = $("#dataTable").DataTable({
        "lengthChange": false,
        "info": false,
        "searching": true,
        "language": {

            "search": "",
            "info": "Tổng số _TOTAL_ hàng",
            "infoEmpty": "",
            "infoFiltered": "",
            "paginate": {
                "next": "»",
                "previous": "«"
            },
            "processing": `Đang tải dữ liệu`,

            searchPlaceholder: "Tìm...",
            zeroRecords: "Không tìm thấy kết quả",

        },
        dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-file-excel"></i>',
                extend: 'excel',
                className: 'btn btn-outline-primary btn-sm mt-2 ml-3'
            },
            {
                text: '<i class="fa fa-file-pdf"></i>',
                extend: 'pdf',
                className: 'btn btn-outline-primary btn-sm mt-2'
            },
            {
                text: '<i class="fa fa-print"></i>',
                extend: 'print',
                className: 'btn btn-outline-primary btn-sm mt-2'
            }
        ],

        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/DM_CanBo/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "HoTen" },
            { "data": "HoTen" },
            { "data": "DoB" },
            { "data": "DiaChiThuongTru" },
            { "data": "SoCCCD" },
            { "data": "Ten" },
            { "data": "Ten_ChucVu_ChucDanh" },
            {
                "data": "TK", "render": (data, type, row) => {
                    if (data) {
                        return "Đã cấp tài khoản"
                    }
                    else {
                        return "Chưa cấp tài khoản"
                    }
                }
            },
            {
                "data": "Ma_CanBo", "render": function (data, type, row) {
                    return `
                                    <a class="btn btn-outline-info btn-sm" href="DM_CanBo/Edit/${data}">
                                        <i class="fas fa-pencil-alt"></i>
                                    </a>
                                    <button class="btn btn-outline-danger btn-sm delete" data-model-id="${data}" onclick="Delete(this)">
                                        <i class="fas fa-trash"></i>
                                    </button>
                                    `

                }
            },
        ]

    });

    dtt.on('draw.dt', function () {
        var info = dtt.page.info();
        dtt.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });
}



function FnBegin() {
    $('.loading_newcanbo').removeClass('d-none');
}

function FnSuccess(data) {
    
    $('.loading_newcanbo').addClass('d-none');
    $('.closeform').click();
    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `${data} đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    location.reload()
}

function Failure(data) {
    $('.loading_newcanbo').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Thêm mới bệnh nhân không thành công',
        timer: 2000,
        showConfirmButton: false,

    })
}


function FnBegin_import() {
    $('.loading_newcanbo').removeClass('d-none');
}

function FnSuccess_import(data) {

    $('.loading_newcanbo').addClass('d-none');
    $('.closeform').click();
    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `${data} đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    location.reload()
}

function Failure_import(data) {
    $('.loading_newcanbo').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Thêm mới bệnh nhân không thành công',
        timer: 2000,
        showConfirmButton: false,

    })
}

function Delete(obj) {
    var ele = $(obj);
    var Ma_CanBo = ele.data("model-id");
    var url = `/DM_CanBo/delete/${Ma_CanBo}`
    swal.fire({
        title: 'Bạn có chắc?',
        text: "Nếu xóa sẽ không thể khôi phục!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Có, Hãy xóa!',
        cancelButtonText: 'Không, Quay lại!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.post(url, { id: Ma_CanBo })
                .done(function (data) {

                    if (data != false) {
                        dtt.draw();
                        Swal.fire({
                            icon: 'success',
                            title: 'Thành Công',
                            text: `${data.HoTen} đã được xóa`,
                            timer: 2000,
                            showConfirmButton: false,

                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Không thành công',
                            text: `dữ liệu đang sử dụng không thể xóa`,
                            timer: 2000,
                            showConfirmButton: false,

                        })
                    }

                })
                .fail(function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `dữ liệu đang sử dụng không thể xóa`,
                        timer: 2000,
                        showConfirmButton: false,
                    })
                });
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {

        }
    })

};

$(document).ready(function () {
    loadDataTable();

    jQuery.validator.addMethod("NotAllowNumber", function (value, element) {
        return this.optional(element) || /^([^0-9]*)$/.test(value);
    }, "Không được phép có chữ số.");

    jQuery.validator.addMethod("NotAllowFirstSpace", function (value, element) {
        return this.optional(element) || /^\S{1}/.test(value);
    }, "Kí tự đầu tiên không được có khoảng trắng.");

    jQuery.validator.addMethod("NotAllowSpecial", function (value, element) {
        return this.optional(element) || /^[A-Za-z0-9_.]+$/.test(value);
    }, "Không được phép có kí tự đặc biệt.");

    $('#Ma_TinhThanh').select2().on("change", function () {
        $.get("/DM_PhuongXa/GetQuanHuyen", { id: $(this).val() }, function (data) {
            $("#Ma_QuanHuyen").empty()
            $("#Ma_QuanHuyen").append("<option value=''>Chọn</option>")
            $("#Ma_PhuongXa_TT").empty()
            $("#Ma_PhuongXa_TT").append("<option value=''>Chọn</option>")
            $.each(data, function (index, row) {
                $("#Ma_QuanHuyen").append("<option value='" + row.MaQuanHuyen + "'>" + row.TenQuanHuyen + "</option>")
            })
        })
    });

    $('#Ma_TinhThanh_edt').select2().on("change", function () {
        $.get("/DM_PhuongXa/GetQuanHuyen", { id: $(this).val() }, function (data) {
            $("#Ma_QuanHuyen_edt").empty()
            $("#Ma_QuanHuyen_edt").append("<option value=''>Chọn</option>")
            $("#Ma_PhuongXa_TT_edt").empty()
            $("#Ma_PhuongXa_TT_edt").append("<option value=''>Chọn</option>")
            $.each(data, function (index, row) {
                $("#Ma_QuanHuyen_edt").append("<option value='" + row.MaQuanHuyen + "'>" + row.TenQuanHuyen + "</option>")
            })
        })
    });


    $('#Ma_QuanHuyen').select2().on("change", function () {
        $.get("/DM_PhuongXa/GetPhuongXa", { id: $(this).val() }, function (data) {

            $("#Ma_PhuongXa_TT").empty()
            $("#Ma_PhuongXa_TT").append("<option value=''>Chọn</option>")
            $.each(data, function (index, row) {
                $("#Ma_PhuongXa_TT").append("<option value='" + row.MaPhuongXa + "'>" + row.TenPhuongXa + "</option>")
            })
        })
    });


    $('#Ma_QuanHuyen_edt').select2().on("change", function () {
        $.get("/DM_PhuongXa/GetPhuongXa", { id: $(this).val() }, function (data) {

            $("#Ma_PhuongXa_TT_edt").empty()
            $("#Ma_PhuongXa_TT_edt").append("<option value=''>Chọn</option>")
            $.each(data, function (index, row) {
                $("#Ma_PhuongXa_TT_edt").append("<option value='" + row.MaPhuongXa + "'>" + row.TenPhuongXa + "</option>")
            })
        })
    });

    $("#Ma_PhuongXa_TT").select2()
    $("#Ma_PhuongXa_TT_edt").select2()

    for (let i = 0; i < t1; i++) {
        $(`#Ma_TinhThanhTN${i}`).select2().on("change", function () {
            $.get("/DM_PhuongXa/GetQuanHuyen", { id: $(this).val() }, function (data) {
                $(`#Ma_QuanHuyenTN${i}`).empty()
                $(`#Ma_QuanHuyenTN${i}`).append("<option value=''>Chọn</option>")
                $(`#Ma_PhuongXa_TTTN${i}`).empty()
                $(`#Ma_PhuongXa_TTTN${i}`).append("<option value=''>Chọn</option>")
                $.each(data, function (index, row) {
                    $(`#Ma_QuanHuyenTN${i}`).append("<option value='" + row.MaQuanHuyen + "'>" + row.TenQuanHuyen + "</option>")
                })
            })
        });

        $(`#Ma_TinhThanhTN${i}_edt`).select2().on("change", function () {
            $.get("/DM_PhuongXa/GetQuanHuyen", { id: $(this).val() }, function (data) {
                $(`#Ma_QuanHuyenTN${i}_edt`).empty()
                $(`#Ma_QuanHuyenTN${i}_edt`).append("<option value=''>Chọn</option>")
                $(`#Ma_PhuongXa_TTTN${i}_edt`).empty()
                $(`#Ma_PhuongXa_TTTN${i}_edt`).append("<option value=''>Chọn</option>")
                $.each(data, function (index, row) {
                    $(`#Ma_QuanHuyenTN${i}_edt`).append("<option value='" + row.MaQuanHuyen + "'>" + row.TenQuanHuyen + "</option>")
                })
            })
        });


        $(`#Ma_QuanHuyenTN${i}`).select2().on("change", function () {
            $.get("/DM_PhuongXa/GetPhuongXa", { id: $(this).val() }, function (data) {

                $(`#Ma_PhuongXa_TTTN${i}`).empty()
                $(`#Ma_PhuongXa_TTTN${i}`).append("<option value=''>Chọn</option>")
                $.each(data, function (index, row) {
                    $(`#Ma_PhuongXa_TTTN${i}`).append("<option value='" + row.MaPhuongXa + "'>" + row.TenPhuongXa + "</option>")
                })
            })
        });

        $(`#Ma_QuanHuyenTN${i}_edt`).select2().on("change", function () {
            $.get("/DM_PhuongXa/GetPhuongXa", { id: $(this).val() }, function (data) {

                $(`#Ma_PhuongXa_TTTN${i}_edt`).empty()
                $(`#Ma_PhuongXa_TTTN${i}_edt`).append("<option value=''>Chọn</option>")
                $.each(data, function (index, row) {
                    $(`#Ma_PhuongXa_TTTN${i}_edt`).append("<option value='" + row.MaPhuongXa + "'>" + row.TenPhuongXa + "</option>")
                })
            })
        });


        $(`#Ma_PhuongXa_TTTN${i}`).select2()
        $(`#Ma_PhuongXa_TTTN${i}_edt`).select2()

    }
})