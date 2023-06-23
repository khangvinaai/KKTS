var dt;
function loadDataTable() {
    dt = $("#dataTable").DataTable({
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
            "url": "/DM_Loai_CoQuan_DonVi/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [2] }
        ],
        "columns": [
            { "data": "Ten_Loai_CQDV" },
            { "data": "Ten_Loai_CQDV" },
            {
                "data": "Ma_Loai_CQDV", "render": function (data, type, row) {
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
    dt.on('draw.dt', function () {
        var info = dt.page.info();
        dt.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });
    dt.draw();
}

function FnBegin_Edit() {
    $('.loading_editloaicoquan').removeClass('d-none');
}

function FnSuccess_Edit(data) {
    $('.loading_editloaicoquan').addClass('d-none')
    $('.closeform').click()

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Cập nhật loại cơ quan đơn vị thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    dt.draw();
}

function Failure_Edit(data) {
    $('.loading_editloaicoquan').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi',
        text: `Cập nhật loại cơ quan đơn vị không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}

function FnBegin() {
    $('.loading_newloaicoquan').removeClass('d-none');
}

function FnSuccess(data) {
    $('.loading_newloaicoquan').addClass('d-none');
    $('.closeform').click();

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Loại cơ quan đơn vị đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    dt.draw();
}

function Failure(data) {
    $('.loading_newloaicoquan').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Thêm mới loại cơ quan đơn vị không thành công',
        timer: 2000,
        showConfirmButton: false,

    })
}

function Edit(obj) {
    var ele = $(obj);
    var id = ele.data("model-id");
    var url = `/DM_Loai_CoQuan_DonVi/GetLoaiCoQuan/`
    $.get(url, { id: id }, (data) => {
        $('#Ma_Loai_CQDV_Edt').val(data.Ma_Loai_CQDV)
        $('#Ten_Loai_CQDV_Edt').val(data.Ten_Loai_CQDV)
    })
};

function Delete(obj) {
    var ele = $(obj);
    var Ma_Loai_CQDV = ele.data("model-id");
    var url = `/DM_Loai_CoQuan_DonVi/delete/${Ma_Loai_CQDV}`
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
            $.post(url, { id: Ma_Loai_CQDV })
                .done(function (data) {
                    if (data != false) {
                        dt.draw();
                        Swal.fire({
                            icon: 'success',
                            title: 'Thành Công',
                            text: `Loại cơ quan đơn vị đã được xóa`,
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

$(document).ready(function () {
    loadDataTable();

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
            Ten_Loai_CQDV: {
                required: true,
            },
        },
        messages: {
            Ten_Loai_CQDV: {
                required: "Tên loại cơ quan đơn vị không được để trống",
            }
        }

    });

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
            Ten_Loai_CQDV: {
                required: true,
            },
        },
        messages: {
            Ten_Loai_CQDV: {
                required: "Tên loại cơ quan đơn vị không được để trống",
            }
        }

    });
})
