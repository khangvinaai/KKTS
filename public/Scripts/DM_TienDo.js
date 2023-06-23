var t;

//Tìm kiếm theo Tên Cơ quan
$("#search_btn").click(() => {
    var searchValue = $('#Filter_QLSDT').val().trim()
    t.column(1).search(searchValue);
    t.draw()
})



$("#Filter_QLSDT").keypress(function (e) {
    if (e.keyCode == 13) {
        t.columns(1).search($("#Filter_QLSDT").val());
        t.draw();
    }
});

function loadDataTable() {

    t = $("#dataTable").DataTable({
        "lengthChange": false,
        "info": false,

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
            "url": "/DM_TienDo/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "TenTienDo" },
            { "data": "TenTienDo" },
            { "data": "GhiChu" },
            {
                "data": "TrangThai", "render": function (data, type, row) {

                    if (data == true) return `<div class="icheck-primary d-inline pr-5">
                                        <input type="checkbox"  disable=true checked>
                                        <label for="TrangThai">
                                            Đang sử dụng
                                        </label>
                                    </div>`

                    else return `<div class="icheck-primary d-inline pr-5">
                                        <input type="checkbox"  disable=true>
                                        <label for="TrangThai">
                                            Đang sử dụng
                                        </label>
                                    </div>`

                }
            },
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

            {
                "data": "ID", "render": function (data, type, row) {
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
        ]

    });
    t.on('draw.dt', function () {
        var info = t.page.info();
        t.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });

}

function FnBegin_Edit() {
    $('.loading_edittiendo').removeClass('d-none');
}

function FnSuccess_Edit(data) {
    $('.loading_edittiendo').addClass('d-none')
    $('.closeform').click()

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `tiến độ đã sửa thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    t.draw();
}

function Failure_Edit(data) {
    $('.loading_edittiendo').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi',
        text: `Sửa tiến độ không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}

function FnBegin() {
    $('.loading_newtiendo').removeClass('d-none');
}

function FnSuccess(data) {

    $('.loading_newtiendo').addClass('d-none');
    $('.closeform').click();

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `tiến độ ${data} đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    t.draw();
}

function Failure(data) {
    $('.loading_newdantoc').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Tên tiến độ đã tồn tại',
        text: data,
        timer: 2000,
        showConfirmButton: false,

    })
}

function Delete(obj) {
    var ele = $(obj);
    var matiendo = ele.data("model-id");
    var url = `/DM_TienDo/delete/${matiendo}`
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
            $.post(url, { id: matiendo })
                .done(function (data) {
                    console.log(data)
                    if (data != false) {
                        t.draw();
                        Swal.fire({
                            icon: 'success',
                            title: 'Thành Công',
                            text: `tiến độ ${data.TenTienDo} đã được xóa`,
                            timer: 2000,
                            showConfirmButton: false,

                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Không thành công',
                            text: `tiến độ này đang sử dụng ở nhiều nơi không thể xóa`,
                            timer: 2000,
                            showConfirmButton: false,

                        })
                    }

                })
                .fail(function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `tiến độ này đang sử dụng ở nhiều nơi không thể xóa`,
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
    var Matiendo = ele.data("model-id");
    var url = `/DM_TienDo/GetSuaTienDo/`
    $.get(url, { id: Matiendo })
        .done(function (data) {

            $("#sua").empty()
            var row = ``
            var row2 = ``

            var row1 = `
                             

                                <div class="form-group row">
                                    <label for="cnsh" class="col-sm-3 col-form-label">Tên tiến độ</label>
                                    <div class="col-sm-9">
                                        <input type="hidden" disable="true" class="form-control" id="ID" name="ID" value="${data.ID}">
                                        <input type="text" class="form-control" id="TenTienDo" name="TenTienDo" placeholder="nhập tên tiến độ"  value="${data.TenTienDo}">
                                        <input type="hidden" class="form-control" id="TenTienDoEdit" name="TenTienDoEdit" value="${data.TenTienDo}">
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label for="cnsh" class="col-sm-3 col-form-label">Ghi Chú</label>
                                    <div class="col-sm-9">
                                        <input type="text" class="form-control" id="GhiChu" name="GhiChu" placeholder="nhập ghi chú"  value="${data.GhiChu}">
                                    </div>
                                </div>
                                  



                                `
            if (data.TrangThai == true) row1 = row1 + `<div class="form-group row">
                                <label for="cnsh" class="col-sm-3 col-form-label">Trạng Thái</label>
                                <div class="col-sm-9">
                                    <div class="icheck-primary d-inline pr-5">
                                        <input type="checkbox" id="TrangThai1" name="TrangThai"  onclick="$(this).val(this.checked ? true : false);" value="${data.TrangThai}" checked >
                                        <label for="TrangThai1">
                                            Đang sử dụng
                                        </label>
                                    </div>
                                </div>
                            </div>`
            else row1 = row1 + `<div class="form-group row">
                                <label for="cnsh" class="col-sm-3 col-form-label">Trạng Thái</label>
                                <div class="col-sm-9">
                                    <div class="icheck-primary d-inline pr-5">
                                        <input type="checkbox" id="TrangThai2" name="TrangThai"  onclick="$(this).val(this.checked ? true : false);" value="${data.TrangThai}" >
                                        <label for="TrangThai2">
                                            Đang sử dụng
                                        </label>
                                    </div>
                                </div>
                            </div>`
            $('#sua').prepend(row1);








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

    var form = $(".formtiendo").validate({
        onfocusout: function (element) {
            $(element).valid();
        },
        invalidHandler: function (form, validator) {
            validator.focusInvalid();
            Swal.fire({
                icon: 'error',
                title: 'Xuất hiện lỗi',
                text: `Thêm tiến độ không thành công`,
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
            TenTienDo: {
                required: true,
                NotAllowFirstSpace: true,
                NotAllowSpecial: true,
                remote: {
                    url: "/DM_TienDo/CheckTienDo/",
                    type: "post",
                }
            },

        },
        messages: {
            Matiendo: {
                required: "Mã tiến độ không được để trống",
            },
            MatKhau: {
                required: "Mật khẩu không được để trống",
                minlength: "Mật khẩu tối thiểu 6 kí tự",
            },
            TenTienDo: {
                required: "Tên tiến độ không được để trống",
                remote: "Tên tiến độ đã tồn tại",

            },
        }
    });

    var formedit = $(".formtiendoEdit").validate({
        onfocusout: function (element) {
            $(element).valid();
        },
        invalidHandler: function (form, validator) {
            validator.focusInvalid();
            Swal.fire({
                icon: 'error',
                title: 'Xuất hiện lỗi',
                text: `cập nhật tiến độ không thành công`,
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
            TenTienDo: {
                required: true,
                NotAllowFirstSpace: true,
                NotAllowSpecial: true,
                remote: {
                    url: "/DM_TienDo/CheckTenTienDoEdit/",
                    data: { TenTienDoEdit: function () { return $('#TenTienDoEdit').val(); } },
                    type: "post",
                }
            },

        },
        messages: {
            Matiendo: {
                required: "Mã tiến độ không được để trống",
            },
            MatKhau: {
                required: "Mật khẩu không được để trống",
                minlength: "Mật khẩu tối thiểu 6 kí tự",
            },
            TenTienDo: {
                required: "Tên tiến độ không được để trống",
                remote: "Tên tiến độ đã tồn tại",

            },
        }
    });

    $(".closeform").click(function () {

        $(':input', '.formtiendo')
            .not(':button, :submit, :reset, :hidden')
            .val('')
            .prop('checked', false)
            .prop('selected', false)
            .removeClass('is-invalid')
            .removeClass('is-valid')
        form.resetForm()
    })
})