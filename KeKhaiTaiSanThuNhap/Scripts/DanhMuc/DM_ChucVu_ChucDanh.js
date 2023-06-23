
var dt;
var SUA = false;
var XOA = false;
var XUAT = false;
var XEM = false;


function loadDataTable() {

    dt = $("#dataTable").DataTable({
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
                className: 'btn btn-outline-primary btn-sm mt-2 ml-3',
                exportOptions: {
                    columns: [1]
                }
            },
            {
                text: '<i class="fa fa-file-pdf"></i>',
                extend: 'pdf',
                className: 'btn btn-outline-primary btn-sm mt-2',
                exportOptions: {
                    columns: [1]
                }
            },
            {
                text: '<i class="fa fa-print"></i>',
                extend: 'print',
                className: 'btn btn-outline-primary btn-sm mt-2',
                exportOptions: {
                    columns: [1]
                }
            }
        ],
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/DM_ChucVu_ChucDanh/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [2] }
        ],
        "columns": [

            { "data": "Ten_ChucVu_ChucDanh" },
            { "data": "Ten_ChucVu_ChucDanh" },
            {
                "data": "Ma_ChucVu_ChucDanh", "render": function (data, type, row) {

                    var row_sua = ``
                    var row_xoa = ``

                    if (SUA) {
                        row_sua = `<span class="dropdown-item" onclick="Edit(this)" data-model-id="${data}" data-toggle="modal" data-target="#add-sh1">
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
    dt.on('draw.dt', function () {
        var info = dt.page.info();
        dt.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });
    dt.draw()

    if (!XUAT) {
        dt.buttons().disable();
    }
}

function FnBegin_Edit() {
    $('.loading_editchucvu').removeClass('d-none');
}

function FnSuccess_Edit(data) {
    $('.loading_editchucvu').addClass('d-none')
    $('.closeform').click()

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Cập nhật chức vụ chức danh thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    dt.draw();
}

function Failure_Edit(data) {
    $('.loading_editchucvu').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi',
        text: `Cập nhật chức vụ chức danh không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}

function FnBegin() {
    $('.loading_newchucvu').removeClass('d-none');
}

function FnSuccess(data) {
    $('.loading_newchucvu').addClass('d-none');
    $('.closeform').click();

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Chức vụ chức danh đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    dt.draw();
}

function Failure(data) {
    $('.loading_newchucvu').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Thêm mới chức vụ chức danh không thành công',
        timer: 2000,
        showConfirmButton: false,

    })
}

function Edit(obj) {
    var ele = $(obj);
    var MaTaiKhoan = ele.data("model-id");
    var url = `/DM_ChucVu_ChucDanh/GetSuaChucVu/`
    $.get(url, { id: MaTaiKhoan })
        .done(function (data) {
            $("#Ma_ChucVu_ChucDanh_edt").val(data.Ma_ChucVu_ChucDanh)
            $("#Ten_ChucVu_ChucDanh_edt").val(data.Ten_ChucVu_ChucDanh)
        })
};

function Delete(obj) {
    var ele = $(obj);
    var Ma_ChucVu_ChucDanh = ele.data("model-id");
    var url = `/DM_ChucVu_ChucDanh/delete`
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
            $.post(url, { id: Ma_ChucVu_ChucDanh })
                .done(function (data) {

                    if (data != false) {
                        dt.draw();
                        Swal.fire({
                            icon: 'success',
                            title: 'Thành Công',
                            text: `Chức vụ chức danh đã được xóa`,
                            timer: 2000,
                            showConfirmButton: false,

                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Không thành công',
                            text: `Xảy ra lỗi`,
                            timer: 2000,
                            showConfirmButton: false,

                        })
                    }

                })
                .fail(function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `Xảy ra lỗi`,
                        timer: 2000,
                        showConfirmButton: false,
                    })
                });
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {

        }
    })

};


async function CheckQuyen() {
    await $.get("/Home/GetQuyen", { MenuCode: "DM_ChucVu" }, (data) => {
        if (data.includes("XEM")) {
            XEM = true;
        }
        if (data.includes("SUA")) {
            SUA = true;
        }
        if (data.includes("XOA")) {
            XOA = true;
        }
        if (data.includes("XUAT")) {
            XUAT = true;
        }

      
    })
}

$(document).ready(async function () {

    await CheckQuyen()

    loadDataTable();

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
            Ten_ChucVu_ChucDanh: {
                required: true,
            }
        },
        messages: {
            Ten_ChucVu_ChucDanh: {
                required: "Chức vụ chức danh không được để trống",
            }

        }
    })
    $(".DM_Edt").validate({
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
            Ma_ChucVu_ChucDanh_edt: {
                required: true,
            },
            Ten_ChucVu_ChucDanh: {
                required: true,
            },
            
        },
        messages: {
            Ma_ChucVu_ChucDanh_edt: {
                required: "Mã chức vụ chức danh Không được để trống",
            },
            Ten_ChucVu_ChucDanh: {
                required: "Chức vụ chức danh không được để trống",
            }

        }
    })
    //Tìm kiếm 
    $("#search_btn").click(() => {
        var searchValue = $('#Filter').val().trim()
        dt.column(1).search(searchValue);
        dt.draw()
    })

    $("#Filter").keypress(function (e) {
        if (e.keyCode == 13) {
            dt.columns(1).search($("#Filter").val());
            dt.draw();
        }
    });

})