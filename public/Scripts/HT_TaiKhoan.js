
$("#them").click(() => {
    $.get("/DM_CoQuanDonVi/GetCoQuan/", (data) => {
        $("#MaCoQuan").empty();
        $("#MaCoQuan").append(`<option value = "">Chọn cơ quan</option>`);
        $.each(data, (index, value) => {
            $("#MaCoQuan").append(`<option value = "${value.MaCoQuan}">${value.TenCoQuan}</option>`)
        });
    });

    $("#Ma_CanBo").empty();
    $("#Ma_CanBo").append(`<option value = "">Chọn cán bộ</option>`);

    $.get("/HT_NhomTaiKhoan/GetNhomTaiKhoan/", (data1) => {
        $("#MaNhomTaiKhoan").empty();
        $("#MaNhomTaiKhoan").append("<option value=''>Chọn nhóm tài khoản</option>")
        $.each(data1, (index, value) => {
            $("#MaNhomTaiKhoan").append(`<option value = "${value.MaNhomTaiKhoan}">${value.TenNhomTaiKhoan}</option>`)
        })
    })
})
var t;

//Tìm kiếm 
$("#search_btn").click(() => {
    var searchValue = $('#Filter_TK').val().trim()
    t.column(1).search(searchValue);
    t.draw()
})

$("#Filter_TK").keypress(function (e) {
    if (e.keyCode == 13) {
        t.columns(1).search($("#Filter_TK").val());
        t.draw();
    }
});

function loadDataTable() {

    t = $("#dataTable").DataTable({
      
        "lengthChange": false,
        "info": false,
        "serverSide": true,
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
        "ajax": {
            "url": "/HT_TaiKhoan/LoadData",
            "type": "POST",
            "datatype": "json",
            async: true,
        },
        "columns": [
            { "data": "HoTen" },
            { "data": "HoTen" },
            { "data": "TenTaiKhoan" },
            { "data": "MatKhau" },
            { "data": "TenNhomTaiKhoan" },
            { "data": "NguoiTao" },
            {
                "data": "NgayTao", "render": function (data, type, row) {
                    Number.prototype.padLeft = function (base, chr) {
                        var len = (String(base || 10).length - String(this).length) + 1;
                        return len > 0 ? new Array(len).join(chr || '0') + this : this;
                    }

                    var datemili = data.toString();
                    var mili = datemili.substring(6, 19);
                    var d = new Date(parseInt(mili));
                    var dformat = [d.getDate().padLeft(), (d.getMonth() + 1).padLeft(),
                    d.getFullYear()].join('/') +
                        ' ' +
                        [d.getHours().padLeft(),
                        d.getMinutes().padLeft(),
                        d.getSeconds().padLeft()].join(':');
                    return dformat;
                }
            },
            { "data": "NguoiSua" },

            {
                "data": "NgaySua", "render": function (data, type, row) {
                    if (data)
                        return moment(data).format('DD/MM/YYYY hh:mm:ss');
                    else {
                        return "";
                    }


                }
            },
            {
                "data": "MaTaiKhoan", "render": function (data, type, row) {
                    return `
                            <button class="btn btn-outline-info btn-sm " onclick="Edit(this)" data-model-id="${data}" data-toggle="modal" data-target="#add-sh1">
                                <i class="fas fa-pencil-alt"></i>
                            </button>
                            <button class="btn btn-outline-danger btn-sm delete" data-model-id="${data}" onclick="Delete(this)">
                                <i class="fas fa-trash"></i>
                            </button>
                            `

                }
            },
        ],
         

    });
    t.on('draw.dt', function () {
      
        var info = t.page.info();
        t.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });
   
}


function FnBegin_Edit() {
    $('.loading_edittaikhoan').removeClass('d-none');
}

function FnSuccess_Edit(data) {
    $('.loading_edittaikhoan').addClass('d-none')
    $('.closeform').click()

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Tài khoản đã sửa thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    t.draw();
}

function Failure_Edit(data) {
    $('.loading_edittaikhoan').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi',
        text: `Sửa tài khoản không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}

function FnBegin() {
    $('.loading_newtaikhoan').removeClass('d-none');
}

function FnSuccess(data) {

    $('.loading_newtaikhoan').addClass('d-none');
    $('.closeform').click();

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Tài Khoản ${data} đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    t.draw();
}

function Failure(data) {
    $('.loading_newdantoc').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Tên tài khoản đã tồn tại',
        text: data,
        timer: 2000,
        showConfirmButton: false,

    })
}

function Delete(obj) {
    var ele = $(obj);
    var mataikhoan = ele.data("model-id");
    var url = `/HT_TaiKhoan/delete/${mataikhoan}`
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
            $.post(url, { id: mataikhoan })
                .done(function (data) {
                    console.log(data)
                    if (data != false) {
                        t.draw();
                        Swal.fire({
                            icon: 'success',
                            title: 'Thành Công',
                            text: `Tài khoản ${data.TenTaiKhoan} đã được xóa`,
                            timer: 2000,
                            showConfirmButton: false,

                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Không thành công',
                            text: `Tài khoản này đang sử dụng ở nhiều nơi không thể xóa`,
                            timer: 2000,
                            showConfirmButton: false,

                        })
                    }

                })
                .fail(function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `Tài khoản này đang sử dụng ở nhiều nơi không thể xóa`,
                        timer: 2000,
                        showConfirmButton: false,

                    })
                });
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {

        }
    })

};

function Edit(obj) {

    var ele = $(obj);
    var MaTaiKhoan = ele.data("model-id");
    var url = `/HT_TaiKhoan/GetSuaTaiKhoan/`
    $.get(url, { id: MaTaiKhoan })
        .done(function (data) {
            console.log(data)
        })
};

$(document).ready(function () {
    loadDataTable();
 
    jQuery.validator.addMethod("NotAllowFirstSpace", function (value, element) {
        return this.optional(element) || /^\S{1}/.test(value);
    }, "Kí tự đầu tiên không được có khoảng trắng.");

    jQuery.validator.addMethod("NotAllowSpecial", function (value, element) {
        return this.optional(element) || /^[A-Za-z0-9_.]+$/.test(value);
    }, "Không được phép có kí tự đặc biệt.");

    var form = $(".formTaiKhoan").validate({
        onfocusout: function (element) {
            $(element).valid();
        },
        invalidHandler: function (form, validator) {
            validator.focusInvalid();
            Swal.fire({
                icon: 'error',
                title: 'Xuất hiện lỗi',
                text: `Thêm tài khoản không thành công`,
                timer: 2000,
                showConfirmButton: false
            })

        },
        errorClass: "is-invalid",
        validClass: "is-valid",
        rules: {

            MatKhau: {
                required: true,
                minlength: 6,
            },
            TenTaiKhoan: {
                required: true,
                NotAllowFirstSpace: true,
                NotAllowSpecial: true,
                remote: {
                    url: "/HT_TaiKhoan/CheckTaiKhoan/",
                    type: "post",
                }
            },
            Ma_CanBo: { required: true },
            MaNhomTaiKhoan: { required: true },

        },
        messages: {
            MaTaiKhoan: {
                required: "Mã tài khoản không được để trống",
            },
            MatKhau: {
                required: "Mật khẩu không được để trống",
                minlength: "Mật khẩu tối thiểu 6 kí tự",
            },
            TenTaiKhoan: {
                required: "Tên tài khoản không được để trống",
                remote: "Tên tài khoản đã tồn tại",

            },
            Ma_CanBo: { required: "Cán bộ không dược để trống" },
            MaNhomTaiKhoan: { required: "Nhóm tài khoản không được để trống" }
        }
    });

    var formedit = $(".formTaiKhoanEdit").validate({
        onfocusout: function (element) {
            $(element).valid();
        },
        invalidHandler: function (form, validator) {
            validator.focusInvalid();
            Swal.fire({
                icon: 'error',
                title: 'Xuất hiện lỗi',
                text: `cập nhật tài khoản không thành công`,
                timer: 2000,
                showConfirmButton: false
            })

        },
        errorClass: "is-invalid",
        validClass: "is-valid",
        rules: {

            MatKhau: {
                required: true,
                minlength: 6,
            },
            TenTaiKhoan: {
                required: true,
                NotAllowFirstSpace: true,
                NotAllowSpecial: true,
                remote: {
                    url: "/HT_TaiKhoan/CheckTenTaiKhoanEdit/",
                    data: { TenTaiKhoanEdit: function () { return $('#TenTaiKhoanEdit').val(); } },
                    type: "post",
                }
            },

        },
        messages: {
            MaTaiKhoan: {
                required: "Mã tài khoản không được để trống",
            },
            MatKhau: {
                required: "Mật khẩu không được để trống",
                minlength: "Mật khẩu tối thiểu 6 kí tự",
            },
            TenTaiKhoan: {
                required: "Tên tài khoản không được để trống",
                remote: "Tên tài khoản đã tồn tại",

            },
        }
    });

    $(".closeform").click(function () {
        $(':input', '.formTaiKhoan')
            .not(':button, :submit, :reset, :hidden')
            .val('')
            .prop('selected', false)
            .removeClass('is-invalid')
            .removeClass('is-valid')
        form.resetForm()
    })

    $('#Ma_CanBo').select2();
    $('#MaNhomTaiKhoan').select2();
    $('#MaCoQuan').select2().on('change', () => {

        $("#Ma_CanBo").empty();
        $("#Ma_CanBo").append(`<option value = "">Chọn cán bộ</option>`);

        $.get("/DM_CanBo/GetCanBoTaiKhoan/", { MaCoQuan: $('#MaCoQuan').val() }, (data) => {
            $.each(data, (index, value) => {
                $("#Ma_CanBo").append(`<option value = "${value.MaCanBo}">${value.TenChucVu} | ${value.TenCanBo}</option>`)
            });
        });

    })

   
})

