


var dt;

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
            "url": "/DM_ChucVu_ChucDanh/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [

            { "data": "Ten_ChucVu_ChucDanh" },
            { "data": "Ten_ChucVu_ChucDanh" },
            {
                "data": "Ma_ChucVu_ChucDanh", "render": function (data, type, row) {
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
    dt.draw()


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
        text: `Sửa thành công`,
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
        text: `Sửa không thành công`,
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
        text: `${data} đã được thêm thành công`,
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
        text: 'Thêm mới bệnh nhân không thành công',
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

            $("#sua").empty()
            var row = ``
            var row2 = ``

            var row1 = `<div class="form-group row">
                            <label for="cnsh" class="col-sm-3 col-form-label">Tên Chức vụ Chức Danh <span style="color: red"> *</span></label>
                            <div class="col-sm-9">
                                <input type="hidden" disable="true" class="form-control" id="Ma_ChucVu_ChucDanh" name="Ma_ChucVu_ChucDanh" value="${data.Ma_ChucVu_ChucDanh}">
                                <input type="text" class="form-control" placeholder="nhập tên chức vụ" id="Ten_ChucVu_ChucDanh" name="Ten_ChucVu_ChucDanh" value="${data.Ten_ChucVu_ChucDanh}">
                            </div>
                        </div>
                        `
            $('#sua').prepend(row1);
        })
};



function Delete(obj) {
    var ele = $(obj);
    var Ma_ChucVu_ChucDanh = ele.data("model-id");
    var url = `/DM_ChucVu_ChucDanh/delete/${Ma_ChucVu_ChucDanh}`
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
                        dt.rows(`#row_${data.Ma_ChucVu_ChucDanh}`)
                            .remove()
                            .draw();
                        Swal.fire({
                            icon: 'success',
                            title: 'Thành Công',
                            text: `${data.Ten} đã được xóa`,
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
    jQuery.validator.addMethod("NotAllowNumber", function (value, element) {
        return this.optional(element) || /^([^0-9]*)$/.test(value);
    }, "Không được phép có chữ số.");

    jQuery.validator.addMethod("NotAllowFirstSpace", function (value, element) {
        return this.optional(element) || /^\S{1}/.test(value);
    }, "Kí tự đầu tiên không được có khoảng trắng.");

    jQuery.validator.addMethod("NotAllowSpecial", function (value, element) {
        return this.optional(element) || /^[A-Za-z0-9_.]+$/.test(value);
    }, "Không được phép có kí tự đặc biệt.");


    //validate form thêm dữ liệu
    $("#formChucDanh").validate({
        ignore: [],
        errorElement: 'div',
        rules: {
            "Ten_ChucVu_ChucDanh": {    
                required: true,
            }
        },
        messages: {
            Ten_ChucVu_ChucDanh: "Vui lòng nhập Dữ liệu",
        },
    })
    //Tìm kiếm 
    $("#search_btn").click(() => {
        var searchValue = $('#Filter_QLSDT').val().trim()
        dt.column(1).search(searchValue);
        dt.draw()
    })

    $("#Filter_QLSDT").keypress(function (e) {
        if (e.keyCode == 13) {
            dt.columns(1).search($("#Filter_QLSDT").val());
            dt.draw();
        }
    });

})