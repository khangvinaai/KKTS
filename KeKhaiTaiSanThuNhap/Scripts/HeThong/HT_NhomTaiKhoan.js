
function changecheck() {

    if ($("#TrangThai1").prop("checked")) {
    $("#TrangThai1").attr('checked', '')
        $("#TrangThai1").prop('value', 'true')
    }
    else {
    $("#TrangThai1").removeAttr('checked');
        $("#TrangThai1").prop('value', 'false')

    }
}

$("#them").click(() => {
    $.get("/DM_CanBo/GetCanBo/", (data) => {
        $("#Ma_CanBo").empty();
        $.each(data, (index, value) => {
            if (value.Ten == "vinaai") {

                $("#Ma_CanBo").append(`<option selected value = "${value.Ma_CanBo}">${value.HoTen}</option>`)
            }
            else {

                $("#Ma_CanBo").append(`<option value = "${value.Ma_CanBo}">${value.HoTen}</option>`)
            }
        });
    });
})
var t;

$("#search_btn").click(() => {
    var searchValue = $('#Filter_NTK').val().trim()
    t.column(1).search(searchValue);
    t.draw()
})

$("#Filter_NTK").keypress(function (e) {
    if (e.keyCode == 13) {
        t.columns(1).search($("#Filter_NTK").val());
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
                "url": "/HT_NhomTaiKhoan/LoadData",
                "type": "POST",
                "datatype": "json"
            },
            "columnDefs": [
                { className: "cn", "targets": [3] },
                { className: "cn", "targets": [8] },
            ],
            "columns": [
                { "data": "TenNhomTaiKhoan" },
                { "data": "TenNhomTaiKhoan" },
                { "data": "Note" },
                {
                    "data": "TrangThai", "render": function (data, type, row) {
                        var btn = `<div class="icheck-primary d-inline">
                                        <input type="checkbox" >
                                        <label>
                                          Không dùng
                                        </label>
                                      </div>`
                        if (data) {
                            btn = ` <div class="icheck-primary d-inline">
                                        <input type="checkbox" checked="checked" >
                                        <label>
                                          Sử dụng
                                        </label>
                                      </div>`
                        }
                        return btn
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
                    "data": "MaNhomTaiKhoan", "render": function (data, type, row) {
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
            t.column(0, {search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
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
            text: `Nhóm tài khoản đã sửa thành công`,
            timer: 2000,
            showConfirmButton: false,
        })
        t.draw()
    }

    function Failure_Edit(data) {
        $('.loading_edittaikhoan').addClass('d-none');
        Swal.fire({
        icon: 'error',
            title: 'Có lỗi',
            text: `Sửa nhóm tài khoản không thành công`,
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
            text: `Nhóm tài Khoản ${data} đã được thêm thành công`,
            timer: 2000,
            showConfirmButton: false,
        })
        t.draw()
    }

    function Failure(data) {
        $('.loading_newdantoc').addClass('d-none');
        Swal.fire({
        icon: 'error',
            title: 'Tên nhóm tài khoản đã tồn tại',
            text: data,
            timer: 2000,
            showConfirmButton: false,

        })
    }

    function Delete(obj) {
        var ele = $(obj);
        var MaNhomTaiKhoan = ele.data("model-id");
        var url = `/HT_NhomTaiKhoan/delete/${MaNhomTaiKhoan}`
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
        $.post(url, { id: MaNhomTaiKhoan })
            .done(function (data) {
                console.log(data)
                if (data != false) {
                    t.draw()
                    Swal.fire({
                        icon: 'success',
                        title: 'Thành Công',
                        text: `Nhóm tài khoản ${data.TenNhomTaiKhoan} đã được xóa`,
                        timer: 2000,
                        showConfirmButton: false,

                    })
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `Nhóm tài khoản này đang sử dụng ở nhiều nơi không thể xóa`,
                        timer: 2000,
                        showConfirmButton: false,

                    })
                }

            })
            .fail(function (data) {
                Swal.fire({
                    icon: 'error',
                    title: 'Không thành công',
                    text: `Nhóm tài khoản này đang sử dụng ở nhiều nơi không thể xóa`,
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
        var url = `/HT_NhomTaiKhoan/GetSuaNhomTaiKhoan/`
        $.get(url, {id: MaTaiKhoan })
            .done(function (data) {

        $("#sua").empty()

                if (!data.TrangThai) {
        row = ` <div class="form-group row edit">
                                    <label for="cnsh" class="col-sm-3 col-form-label">Trạng Thái</label>
                                    <div class="col-sm-9">
                                        <div class="icheck-primary d-inline" >
                                            <input type="checkbox" id="TrangThai1" name="TrangThai" value="false" onchange="changecheck()">
                                            <label for="TrangThai1" >
                                                Sử dụng
                                            </label>
                                        </div>
                                    </div>
                                </div>`

    }

                else {
        row = ` <div class="form-group row edit">
                                    <label for="cnsh" class="col-sm-3 col-form-label">Trạng Thái</label>
                                    <div class="col-sm-9">
                                        <div class="icheck-primary d-inline" >
                                            <input type="checkbox" id="TrangThai1" name="TrangThai" value="true" checked="checked"  onchange="changecheck()">
                                            <label for="TrangThai1" >
                                                Sử dụng
                                            </label>
                                        </div>
                                    </div>
                                </div>`
    }

                var row1 = `
                               <div class="form-group row">
        <label for="cnsh" class="col-sm-3 col-form-label">Tên Nhóm Tài Khoản</label>
        <div class="col-sm-9">
            <input type="hidden" disable="true" class="form-control" id="MaNhomTaiKhoan" name="MaNhomTaiKhoan" value="${data.MaNhomTaiKhoan}">
                <input type="text" class="form-control" id="TenNhomTaiKhoan" name="TenNhomTaiKhoan" value="${data.TenNhomTaiKhoan}" placeholder="nhập tên nhóm tài khoản">
                    <input type="hidden" disable="true" class="form-control" id="TenNhomTaiKhoanEdit" name="TenNhomTaiKhoanEdit" value="${data.TenNhomTaiKhoan}">
                                </div>
                            </div>
                <div class="form-group row">
                    <label for="cnsh" class="col-sm-3 col-form-label">Mã tk</label>
                    <div class="col-sm-9">
                        <input type="text" class="form-control" id="MaTaiKhoan" name="MaTaiKhoan" value="${data.MaTaiKhoan}" placeholder="mã tài khoản">
                                </div>
                    </div>
                    <div class="form-group row">
                        <label for="cnsh" class="col-sm-3 col-form-label">Note</label>
                        <div class="col-sm-9">
                            <input type="text" class="form-control" id="Note" name="Note" value="${data.Note != null ? data.Note:""}" placeholder="Ghi chú">
                                </div>
                    </div>
                           ${row}



                                `
                $('#sua').prepend(row1);






            })
    };

    $(document).ready(function () {

                        $("input[type=checkbox]").change(function () {
                            if ($(this).prop("checked")) {
                                $(this).attr('checked', 'checked')
                                $(this).attr('value', 'true')

                            }
                            else {
                                $(this).removeAttr('checked');
                                $(this).attr('value', 'false')

                            }
                        })


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

                        MaTaiKhoan: {required: true,},
                TenNhomTaiKhoan: {
                        required: true,
                    NotAllowFirstSpace: true,
                    remote: {
                        url: "/HT_NhomTaiKhoan/CheckNhomTaiKhoan/",
                        type: "post",
                    }
                },

            },
            messages: {
                        MaTaiKhoan: {
                        required: "Mã tài khoản không được để trống",
                },
                TenNhomTaiKhoan: {
                        required: "Tên nhóm tài khoản không được để trống",
                    remote: "Tên nhóm tài khoản đã tồn tại",

                },
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
                    minlength: 8,
                },
                TenTaiKhoan: {
                        required: true,
                    NotAllowFirstSpace: true,
                    NotAllowSpecial: true,
                    remote: {
                        url: "/HT_TaiKhoan/CheckTenTaiKhoanEdit/",
                        data: {TenTaiKhoanEdit: function () { return $('#TenTaiKhoanEdit').val(); } },
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
                    minlength: "Mật khẩu tối thiểu 8 kí tự",
                },
                TenTaiKhoan: {
                        required: "Tên tài khoản không được để trống",
                    remote: "Tên tài khoản đã tồn tại",

                },
            }
        });

        $(".closeform").click(function () {

        })
    })

