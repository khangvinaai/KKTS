
//Hiển thị cơ quan trong tạo yêu cầu cấp tài khoản
$("#loadCanBoChuaCoTaiKhoan").click(() => {

    FnBegin();
    var d;
    d = $("#dataTableYeuCau").DataTable({
        "info": false,
        "bFilter": false,
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
        dom: 'Bfrtip',
        "ajax": {
            "url": "/NV_CapTaiKhoan/YeuCauCapTaiKhoan",
            "type": "POST",
            "datatype": "json"
        },
        processing: true,
        serverSide: true,
        "columns": [
            { "data": "Ma_CanBo" },
            { "data": "HoTen" },
            {
                "data": "DoB"
            },
            {
                "data": "SoCCCD"
            },
            {
                "data": "Ten"
            },
            {
                "data": "Ten_ChucVu_ChucDanh"
            },
            {
                "data": "tk", "render": function (data, type, row) {
                    if (data != false) {
                        return `<i class="fa fa-times-circle" style="color: red;" aria-hidden="true"></i>`
                    } else {
                        return `<i class="bi bi-x-circle-fill"></i>`
                    }   
                }
            }
        ]
    });
    var info = d.page.info();
    d.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
        cell.innerHTML = i + 1 + info.start;
    });
    setTimeout(FnSuccess, 2000);
})


function FnBegin() {
    $('.loading_DanhSach').removeClass('d-none');
}

function FnSuccess() {
    $('.loading_DanhSach').addClass('d-none');
   
}

function Failure() {
    $('.loading_Add').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Yêu cầu cấp tài khoản không thành công',
        timer: 2000,
        showConfirmButton: false,
    })
}


function FnBegin_YeuCau() {
    $('.loading_DanhSach').removeClass('d-none');
}

function FnSuccess_YeuCau(data) {
    if (data == true) {
        $('.loading_Add').addClass('d-none');
        $('.closeform').click();
        Swal.fire({
            icon: 'success',
            title: 'Thành Công',
            text: ` đã yêu cầu cấp tài khoản thành công`,
            timer: 2000,
            showConfirmButton: false,
        })
        location.reload()
    } else {
        $('.loading_Add').addClass('d-none');
        Swal.fire({
            icon: 'error',
            title: 'Có lỗi xảy ra',
            text: 'Yêu cầu cấp tài khoản không thành công',
            timer: 2000,
            showConfirmButton: false,

        })
    }
    
}

function Failure_YeuCau(data) {
    $('.loading_Add').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Yêu cầu cấp tài khoản không thành công',
        timer: 2000,
        showConfirmButton: false,

    })
}

var t;
//Tìm kiếm 
$("#search_btn").click(() => {
    var searchValue = $('#Filter_CTK').val().trim()
    t.column(1).search(searchValue);
    t.draw()
})

$("#Filter_CTK").keypress(function (e) {
    if (e.keyCode == 13) {
        t.columns(1).search($("#Filter_CTK").val());
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
            "url": "/NV_CapTaiKhoan/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "ID" },
            { "data": "NguoiGui" },
            {
                "data": "NgayGui", "render": function(data, type, row, meta) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss');
                }
             },
            {
                "data": "NguoiCap", "render": function (data, type, row, meta) {
                    if (data!="")
                    {
                        return data
                    } else {
                        return "Đang chờ cấp..."
                    }
                }
                 },
            {
                "data": "NgayCap", "render": function (data, type, row, meta) {
                    if (data != null) {
                        return moment(data).format('DD/MM/YYYY hh:mm:ss');
                    } else {
                        return "Đang chờ cấp..."
                    }
                } },
            {
                "data": "TrangThai", "render": function (data, type, row) {
                    if (data == 1) {
                        return `<span class="badge badge-success">Đã Duyệt</span>
                                `
                    }else
                    {
                        return `<span class="badge badge-danger ">Đang yêu cầu</span>
                                `
                    }

                }  },
            {
                "data": { "FileCap": "FileCap", "ID": "ID" }, "render": function (data, type, row) {
                    if (data.FileCap != null) {
                        return ` <a class="btn btn-outline-info btn-sm" href="/NV_CapTaiKhoan/Download?id=${data.ID}">
                                <i class="fa fa-download" aria-hidden="true"></i> ${data.FileCap}
                            </a>
                            `
                    }else
                    {
                        return `Đang chờ cấp`
                    }
                   
                } },
            {
                "data": "ID", "render": function (data, type, row) {
                    return ` <a style="cursor:pointer" class="btn-edit" pattern="edit" href="/NV_CapTaiKhoan/ChiTietCapTaiKhoan/${data}"><span class="fa fa-pager"></span> Xem chi tiết</a>
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



$(document).ready(function () {
    loadDataTable();

    jQuery.validator.addMethod("NotAllowFirstSpace", function (value, element) {
        return this.optional(element) || /^\S{1}/.test(value);
    }, "Kí tự đầu tiên không được có khoảng trắng.");

    jQuery.validator.addMethod("NotAllowSpecial", function (value, element) {
        return this.optional(element) || /^[A-Za-z0-9_.]+$/.test(value);
    }, "Không được phép có kí tự đặc biệt.");

 
})

