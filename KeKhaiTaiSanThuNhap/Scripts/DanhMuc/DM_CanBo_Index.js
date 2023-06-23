var dtt;

var SUA = false;
var XOA = false;
var XUAT = false;
var XEM = false;
var THEM = false;

var getRole = "CBKKTSTN";
$.get("/HT_NhomTaiKhoan/GetRole/", (data1) => {
    getRole = data1;
})


function FnBegin() {
    $('.loading_newcanbo').removeClass('d-none');
}

function FnSuccess(data) {
    $('.loading_newcanbo').addClass('d-none');
    $('.closeform').click();
    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Cán bộ đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    dtt.draw()
}

function Failure(data) {
    $('.loading_newcanbo').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Thêm mới cán bộ không thành công',
        timer: 2000,
        showConfirmButton: false,

    })
}

function FnBegin_edt() {
    $('.loading_editcanbo').removeClass('d-none');
}

function FnSuccess_edt(data) {
    $('.loading_editcanbo').addClass('d-none');
    $('.closeform').click();
    if (data != false) {

        Swal.fire({
            icon: 'success',
            title: 'Thành Công',
            text: `Cán bộ đã được Sửa thành công`,
            timer: 2000,
            showConfirmButton: false,
        })
        dtt.draw()

    } else {
        Swal.fire({
            icon: 'error',
            title: 'Có lỗi',
            text: `Cán bộ đã có trong danh sách kê khai, vui lòng kiểm tra lại`,
            timer: 2000,
            showConfirmButton: false,
        })
        dtt.draw()
    }

}

function Failure_edt(data) {
    $('.loading_editcanbo').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Sửa mới cán bộ không thành công',
        timer: 2000,
        showConfirmButton: false,

    })
}


function FnBegin_TT() {
    $('.loading_newcanbo').removeClass('d-none');
}

function FnSuccess_TT(data) {
    $('.loading_newcanbo').addClass('d-none');
    $('.closeform').click();
    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Cán bộ đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    dtt.draw()
}

function Failure(data) {
    $('.loading_newcanbo').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Thêm mới cán bộ không thành công',
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
    if (data.success == 0) {
        Swal.fire({
            icon: 'error',
            title: 'Lỗi',
            text: `Số cán bộ Không Hợp Lệ: ${data.fail}`,
            timer: 3000,
            showConfirmButton: false
        }).then(() => {
            window.location.replace(`/DM_CanBo/Download?tenfile=${data.TenFileKetQua.split('.')[0]}`)
        })

    }
    else if (data.fail == 0) {
        Swal.fire({
            icon: 'success',
            title: 'Thành Công',
            text: `Số cán bộ được thêm thành công: ${data.success}`,
            timer: 3000,
            showConfirmButton: false
        }).then(() => {
            window.location.replace(`/DM_CanBo/Download?tenfile=${data.TenFileKetQua.split('.')[0]}`)
        })
        dtt.draw()
    }
    else {
        Swal.fire({
            icon: 'warning',
            title: '',
            text: `Đã thêm: ${data.success} || Không Hợp Lệ: ${data.fail} `,
            timer: 3000,
            showConfirmButton: false
        }).then(() => {
            window.location.replace(`/DM_CanBo/Download?tenfile=${data.TenFileKetQua.split('.')[0]}`)
        })
        dtt.draw()
    }
    
}

function Failure_import(data) {
    $('.loading_newcanbo').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Lỗi',
        text: `File Import bị lỗi, vui lòng kiểm tra lại`,
        timer: 3000,
        showConfirmButton: false
    })
}


function CV_FnBegin() {
    $('.loading_newchucvu').removeClass('d-none');
}

function CV_FnSuccess(data) {
    $('.loading_newchucvu').addClass('d-none');
    $('.closeform1').click();

    $(`#Ma_ChucVu_ChucDanh`).select2("val", "")

    $("#Ma_ChucVu_ChucDanh").append(`<option selected value ="${data.Ma_ChucVu_ChucDanh}">${data.Ten_ChucVu_ChucDanh}</option>`)

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Chức vụ chức danh đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })

}

function CV_Failure(data) {
    $('.loading_newchucvu').addClass('d-none');
    $('.closeform1').click();
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Thêm mới chức vụ chức danh không thành công',
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
                            text: `Cán bộ đã được xóa`,
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

function loadDataTable() {
    dtt = $("#dataTable").DataTable({
        "lengthChange": false,
        "info": true,
        "searching": true,
        "language": {

            "search": "",
            "info": "Tổng số _TOTAL_ cán bộ",
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
                className: 'btn btn-outline-primary btn-sm mt-2 ml-3',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6]
                }
            },
            {
                text: '<i class="fa fa-file-pdf"></i>',
                extend: 'pdf',
                className: 'btn btn-outline-primary btn-sm mt-2',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6]
                }
            },
            {
                text: '<i class="fa fa-print"></i>',
                extend: 'print',
                className: 'btn btn-outline-primary btn-sm mt-2',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6]
                }
            }
        ],

        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/DM_CanBo/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [7] }
        ],
        "columns": [
            { "data": "HoTen" },
            {
                "data": { "HoTen": "HoTen", "Ma_CanBo": "Ma_CanBo" } , "render": function (data, type, row) {
                    if (getRole == "NDDCSBN" || getRole == "NDDTTT") {
                        return `<b><a href="/ke-khai-tai-san/xem-chi-tiet-ban-ke-khai/${data.Ma_CanBo}">${data.HoTen}</a></b>`
                    } else {
                        return data.HoTen
                    }
                }
            },
            {
                "data": "DoB"
            },
            { "data": "DiaChiThuongTru" },
            { "data": "SoCCCD" },
            { "data": "Ten" },
            { "data": "Ten_ChucVu_ChucDanh" },
            {
                "data": "Ma_CanBo", "render": function (data, type, row) {



                    var row_sua = ``
                    var row_xoa = ``
                    if (SUA) {
                        row_sua = `<span class="dropdown-item" onclick="Edit(this)" data-model-id="${data}" data-toggle="modal" data-target="#edit-sh">
                                            Cập Nhật
                                        </span>`
                    }
                    if (XOA) {
                        row_xoa = `<span class="dropdown-item" data-model-id="${data}" onclick="Delete(this)">
                                            Xóa Dữ Liệu
                                        </span>`
                    }

                    if (row_sua != `` || row_xoa != ``) {
                        return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_sua}${row_xoa}
                                      </div>`
                    }
                    else return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_sua}${row_xoa}
                                      </div>`

                }
            },
        ]

    });


  
    /*href="/can-bo/cap-nhat/${data}"*/

    dtt.on('draw.dt', function () {
        var info = dtt.page.info();
        dtt.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });

    if (!THEM) {
        $('#THEM').remove()
    }
    if (!XUAT) {
        dtt.buttons().disable();
    }


}


function Edit(obj) {
    var ele = $(obj);
    var MaTaiKhoan = ele.data("model-id");
    var url = `/DM_CanBo/GetSuaCanBo/`


 
    $.get("/DM_CoQuanDonVi/GetCoQuan/", (data) => {
        $("#Ma_CoQuan_DonVi_edt").empty();
        
        $.each(data, (index, value) => {
            $("#Ma_CoQuan_DonVi_edt").append(`<option value ="${value.MaCoQuan}">${value.TenCoQuan}</option>`)
        })
    })

    $.get("/DM_ChucVu_ChucDanh/GetChucVu/", (data) => {
        $("#Ma_ChucVu_ChucDanh_edt").empty();
        $("#Ma_ChucVu_ChucDanh_edt").append("<option value=''>chọn chức vụ/chức danh</option>")
        $.each(data, (index, value) => {
            $("#Ma_ChucVu_ChucDanh_edt").append(`<option value ="${value.MaChucVu}">${value.TenChucVu}</option>`)
        })
    })

    $.get("/HT_NhomTaiKhoan/GetNhomTaiKhoan/", (data1) => {
        $("#MaNhomTaiKhoan_edt").empty();
        $("#MaNhomTaiKhoan_edt").append("<option value=''>chọn nhóm tài khoản</option>")
        $.each(data1, (index, value) => {
            $("#MaNhomTaiKhoan_edt").append(`<option value = "${value.MaNhomTaiKhoan}">${value.TenNhomTaiKhoan}</option>`)
        })
    })
    $.get(url, { id: MaTaiKhoan })
        .done(function (data) {
            var valNgayCap = "1999-01-01"
            var valDoB = "1999-01-01"
            if (data.nguoikekhai.DoB != null) {
                var DoBV = data.nguoikekhai.DoB.split("/");
                valDoB = `${DoBV[2]}-${DoBV[1]}-${DoBV[0]}`
            } 

            if (data.nguoikekhai.NgayCap != null) {
                var NgayCapV = data.nguoikekhai.NgayCap.split("/");
                valNgayCap = `${NgayCapV[2]}-${NgayCapV[1]}-${NgayCapV[0]}`;
            } 

            $("#Ma_CanBo_edt").val(data.nguoikekhai.Ma_CanBo)
            $("#HoTen_edt").val(data.nguoikekhai.HoTen)
            $("#DoB_edt").val(valDoB)
            $("#Email_edt").val(data.nguoikekhai.Email)
            $("#DiaChiThuongTru_edt").val(data.nguoikekhai.DiaChiThuongTru)
            $("#SoCCCD_edt").val(data.nguoikekhai.SoCCCD)
            $("#NgayCap_edt").val(valNgayCap)
            $("#NoiCap_edt").val(data.nguoikekhai.NoiCap)
            $("#TenTaiKhoan_edt").val(data.nguoikekhai.TenTaiKhoan)
            $("#ID_tk").val(data.nguoikekhai.ID_TaiKhoan)
           
            $("#MatKhau_edt").val(data.nguoikekhai.MatKhau)
            $("#Ma_ChucVu_ChucDanh_edt").val(data.nguoikekhai.Ma_ChucVu_ChucDanh)
            $("#Ma_ChucVu_ChucDanh_edt").change()
            $("#MaNhomTaiKhoan_edt").val(data.nguoikekhai.MaNhomTaiKhoan)
            $("#MaNhomTaiKhoan_edt").change()
            $("#Ma_CoQuan_DonVi_edt").val(data.nguoikekhai.Ma_CoQuan_DonVi)
            $("#Ma_CoQuan_DonVi_edt").change()
          

        })
   

};

$('#RandomTaiKhoan').click(() => {
    if ($('#HoTen').val() == "") {
        Swal.fire({
            icon: 'warning',
            title: 'Cảnh Báo',
            text: 'Hãy nhập đầy đủ họ tên để sử dụng chức năng này',
            timer: 2000,
            showConfirmButton: false,
        })
    }
    else {
        $.post('/DM_CanBo/RandomTaiKhoan', { HoTen: $('#HoTen').val() })
            .done(function (data) {
                $('#TenTaiKhoan').val(data)
            })
            .fail(function (data) {
                Swal.fire({
                    icon: 'error',
                    title: 'Không thành công',
                    text: `Lấy dữ liệu tài khoản không thành công`,
                    timer: 2000,
                    showConfirmButton: false,
                })
            });
    }
})

$("#ExportCanBo").click(() => {
    $.post('/DM_CanBo/ExportCanBo')
        .done(function (data) {
            window.location.replace(`/DM_CanBo/Download?tenfile=${data}`)
        }).then(() => {
           
        })
        .fail(function (data) {
            Swal.fire({
                icon: 'error',
                title: 'Không thành công',
                text: `Không xuất được danh sách cán bộ`,
                timer: 2000,
                showConfirmButton: false,
            })
        });
})

async function CheckQuyen() {
    await $.get("/Home/GetQuyen", { MenuCode: "DM_CanBo" }, (data) => {
        if (data.includes("XEM")) {
            XEM = true;
        }
        if (data.includes("SUA")) {
            SUA = true;
        }
        if (data.includes("XOA")) {
            XOA = true;
        }
        if (data.includes("THEM")) {
            THEM = true;
        }
        if (data.includes("XUAT")) {
            XUAT = true;
        }


    })
}

$(document).ready(async function () {

    await CheckQuyen()

    loadDataTable();

    $.get("/DM_CoQuanDonVi/GetCoQuan/", (data) => {
        $("#Ma_CoQuan_DonVi").empty();
        $("#Ma_CoQuan_DonVi").append("<option value=''>chọn cơ quan/đơn vị</option>")
        $.each(data, (index, value) => {
            $("#Ma_CoQuan_DonVi").append(`<option value ="${value.MaCoQuan}">${value.TenCoQuan}</option>`)
        })
    })

    $.get("/DM_ChucVu_ChucDanh/GetChucVu/", (data) => {
        $("#Ma_ChucVu_ChucDanh").empty();
        $("#Ma_ChucVu_ChucDanh").append("<option value=''>chọn chức vụ/chức danh</option>")
        $.each(data, (index, value) => {
            $("#Ma_ChucVu_ChucDanh").append(`<option value ="${value.MaChucVu}">${value.TenChucVu}</option>`)
        })
    })

    $.get("/HT_NhomTaiKhoan/GetNhomTaiKhoan/", (data1) => {
        $("#MaNhomTaiKhoan").empty();
        $("#MaNhomTaiKhoan").append("<option value=''>chọn nhóm tài khoản</option>")
        $.each(data1, (index, value) => {
            $("#MaNhomTaiKhoan").append(`<option value = "${value.MaNhomTaiKhoan}">${value.TenNhomTaiKhoan}</option>`)
        })
    })


    jQuery.validator.addMethod("NotAllowFirstSpace", function (value, element) {
        return this.optional(element) || /^\S{1}/.test(value);
    }, "Kí tự đầu tiên không được có khoảng trắng.");

    jQuery.validator.addMethod("NotAllowSpecial", function (value, element) {
        return this.optional(element) || /^[A-Za-z0-9_.]+$/.test(value);
    }, "Không được phép có kí tự đặc biệt.");

    jQuery.validator.addMethod("UserName", function (value, element) {
        return this.optional(element) || /^[a-z][a-z0-9_]{4,29}$/.test(value);
    }, "Tên tài khoản không hợp lệ.");

    $(".DM_Create").validate({
        onfocusout: function (element) {
            $(element).valid();
        },
        invalidHandler: function (form, validator) {
            validator.focusInvalid();
            Swal.fire({
                icon: 'error',
                title: 'Xuất hiện lỗi',
                text: `Vui lòng kiểm tra thông báo lỗi`,
                timer: 2000,
                showConfirmButton: false
            })
        },
        errorClass: "is-invalid",
        validClass: "is-valid",

        rules: {
            HoTen: {
                required: true,
            },
            Email: {
                required: true,
                email: true,
                remote: {
                    url: "/DM_CanBo/EmailTonTai/",
                    type: "post",
                }
            },
            Ma_CoQuan_DonVi: {
                required: true,
            },
            Ma_ChucVu_ChucDanh: {
                required: true,
            },
            MaNhomTaiKhoan: {
                required: true,
            },
            TenTaiKhoan: {
                required: true,
                NotAllowFirstSpace: true,
                NotAllowSpecial: true,
                UserName: true,
                remote: {
                    url: "/HT_TaiKhoan/CheckTaiKhoan/",
                    type: "post",
                }
            },
            MatKhau: {
                required: true,
            }
        },
        messages: {
            HoTen: {
                required: "Tên cán bộ không được để trống",
            },
            Email: {
                required: "Email không được để trống",
                email: "Email chưa hợp lệ",
                remote: "Email đã được sử dụng",
            },
            Ma_CoQuan_DonVi: {
                required: "Cơ quan đơn vị không được để trống",
            },
            Ma_ChucVu_ChucDanh: {
                required: "Chức vụ không được để trống",
            },
            MaNhomTaiKhoan: {
                required: "Nhóm tài khoản không được để trống",
            },
            TenTaiKhoan: {
                required: "Tên tài khoản không được để trống",
                remote: "Tên tài khoản đã tồn tại",
            },
            MatKhau: {
                required: "Mật khẩu không được để trống",
            }
        }
    })
    $(".DM_Edit").validate({
        onfocusout: function (element) {
            $(element).valid();
        },
        invalidHandler: function (form, validator) {
            validator.focusInvalid();
            Swal.fire({
                icon: 'error',
                title: 'Xuất hiện lỗi',
                text: `Vui lòng kiểm tra thông báo lỗi`,
                timer: 2000,
                showConfirmButton: false
            })

        },
        errorClass: "is-invalid",
        validClass: "is-valid",

        rules: {
            HoTen: {
                required: true,
            },
            Email: {
                required: true,
                email: true,
                remote: {
                    url: "/DM_CanBo/EmailTonTai1/",
                    type: "post",
                    dataType: 'json',
                    data: {
                        Ma_CanBo: function () {
                            return $("#Ma_CanBo_edt").val();
                        }
                    }

                }
            },
            Ma_CoQuan_DonVi: {
                required: true,
            },
            Ma_ChucVu_ChucDanh: {
                required: true,
            },
            MaNhomTaiKhoan: {
                required: true,
            },
            TenTaiKhoan: {
                required: true,
                NotAllowFirstSpace: true,
                NotAllowSpecial: true,
                UserName: true,
                remote: {
                    url: "/HT_TaiKhoan/CheckTaiKhoan1/",
                    type: "post",
                    dataType: 'json',
                    data: {
                        Ma_CanBo: function () {
                            return $("#Ma_CanBo_edt").val();
                        }
                    }
                }
            },
            MatKhau: {
                required: true,
            }
        },
        messages: {
            HoTen: {
                required: "Tên cán bộ không được để trống",
            },
            Email: {
                required: "Email không được để trống",
                email: "Email chưa hợp lệ",
                remote: "Email đã được sử dụng",
            },
            Ma_CoQuan_DonVi: {
                required: "Cơ quan đơn vị không được để trống",
            },
            Ma_ChucVu_ChucDanh: {
                required: "Chức vụ không được để trống",
            },
            MaNhomTaiKhoan: {
                required: "Nhóm tài khoản không được để trống",
            },
            TenTaiKhoan: {
                required: "Tên tài khoản không được để trống",
                remote: "Tên tài khoản đã tồn tại",
            },
            MatKhau: {
                required: "Mật khẩu không được để trống",
            }
        }
    })

    $(`#Ma_CoQuan_DonVi`).select2()
    $(`#Ma_ChucVu_ChucDanh`).select2()
    $(`#MaNhomTaiKhoan`).select2()

    $(`#Ma_CoQuan_DonVi_edt`).select2()
    $(`#Ma_ChucVu_ChucDanh_edt`).select2()
    $(`#MaNhomTaiKhoan_edt`).select2()
    
    $("#search_btn").click(() => {
        dtt.columns(0).search($('#Filter').val());
        dtt.draw()
    })
    $("#Filter").keypress(function (e) {
        if (e.keyCode == 13) {
            dtt.columns(0).search($('#Filter').val());
            dtt.draw();
        }
    });
})