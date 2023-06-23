
var dt, dt1, dt2, dt3;
var XEM = false;
var SUA = false;
var THEM = false;
var XUAT = false;
var FILEDINHKEM = false;
var XEMCHITIET = false;
var LAPDANHSACH = false;
var LAPKEHOACH = false;

async function CheckQuyen() {
    await $.get("/Home/GetQuyen", { MenuCode: "NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh" }, (data) => {
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
        if (data.includes("LAPDANHSACH")) {
            LAPDANHSACH = true;
        }
        if (data.includes("XEMCHITIET")) {
            XEMCHITIET = true;
        }
        if (data.includes("LAPDANHSACH")) {
            LAPDANHSACH = true;
        }
        if (data.includes("SUA")) {
            SUA = true;
        }
        if (data.includes("LAPKEHOACH")) {
            LAPKEHOACH = true;
        }
    })
}

function FnBegin_KHXM() {
    $('.loading_new').removeClass('d-none');
    $("#btn_add").attr("disabled", true);
}

function FnSuccess_KHXM(data) {

    $('#ID_KeHoach').val('')
    $('#SoKeHoach').val('')
    $('#ID_DanhSachCanBo').val('')
    $('#NoiDungKeHoach').val('')
    $('#NgayLapKeHoach').val('')
    $('#DaDinhKem').empty()

    $('.loading_new').addClass('d-none');
    $('.closeform').click();
    dt.draw()
    Swal.fire({
        icon: data.status,
        title: data.title,
        text: data.message,
        timer: 2000,
        showConfirmButton: false,
    })
}

function Failure_KHXM(data) {
    $('.loading_new').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi',
        text: 'Lỗi Hệ Thống',
        timer: 2000,
        showConfirmButton: false,
    })
}

function LapKeHoach(obj) {

    $('#lap-ke-hoach-title').text("Lập Kế Hoạch Xác Minh")

    if ($.fn.DataTable.isDataTable("#dataTable_DSCQ")) {
        $('#dataTable_DSCQ').DataTable().clear().destroy();
    }

    var ele = $(obj);
    var ID_DanhSach = ele.data("model-id");
    $('#ID_DanhSachCanBo').val(ID_DanhSach)

    dt1 = $("#dataTable_DSCQ").DataTable({
        "lengthChange": false,
        "info": false,
        "searching": false,
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
        "autoWidth": false,
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": `/NV_LapKeHoachXacMinh/LoadDataCoQuan/${ID_DanhSach}`,
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "SoLuong" },
            { "data": "Ten_Loai_CQDV" },
            { "data": "Ten" },
            { "data": "SoLuong" },
        ]
    });

    dt1.on('draw.dt', function () {
        var info = dt.page.info();
        dt1.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });
}

function CapNhatKeHoach(obj) {


    $('#lap-ke-hoach-title').text("Cập Nhật Kế Hoạch Xác Minh")

    

    if ($.fn.DataTable.isDataTable("#dataTable_DSCQ")) {
        $('#dataTable_DSCQ').DataTable().clear().destroy();
    }

    var ele = $(obj);
    var ID_DanhSach = ele.data("model-id");
    $('#ID_DanhSachCanBo').val(ID_DanhSach)

    dt1 = $("#dataTable_DSCQ").DataTable({
        "lengthChange": false,
        "info": false,
        "searching": false,
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
        "autoWidth": false,
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": `/NV_LapKeHoachXacMinh/LoadDataCoQuan/${ID_DanhSach}`,
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "SoLuong" },
            { "data": "Ten_Loai_CQDV" },
            { "data": "Ten" },
            { "data": "SoLuong" },
        ]
    });

    dt1.on('draw.dt', function () {
        var info = dt.page.info();
        dt1.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });

    $.get("/NV_LapKeHoachXacMinh/GetKeHoachXacMinh/", { ID_DanhSach: ID_DanhSach }, (data) => {
        $('#DaDinhKem').empty();

        $('#ID_KeHoach').val(data.ID_KeHoach)
        $('#SoKeHoach').val(data.SoKeHoach)
        $('#ID_DanhSachCanBo').val(data.ID_DanhSachCanBo)
        $('#NoiDungKeHoach').val(data.NoiDungKeHoach)
        $('#NgayLapKeHoach').val(moment(data.NgayLapKeHoach).format("YYYY-MM-DD"))
        $('#DaDinhKem').append(`<a href="/content/uploads/${data.FileKeHoach}" target="_blank">${data.FileKeHoach}</a>`)

    })
}

function HoanThanhKeHoach(obj) {
    var ele = $(obj);
    var ID_DanhSach = ele.data("model-id");
    $.ajax({
        url: `/NV_LapKeHoachXacMinh/HoanThanhKeHoach/${ID_DanhSach}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        beforeSend: function () {
           
        },
        success: function (response) {
            dt.draw();
            Swal.fire({
                icon: response.status,
                title: response.title,
                text: response.message,
                timer: 2000,
                showConfirmButton: false,
            })

        },
        error: function (response) {
            Swal.fire({
                icon: 'error',
                title: 'Thất Bại',
                text: 'Lỗi hệ thống',
                timer: 2000,
                showConfirmButton: false,
            }).then(() => {

            })
        }
    });
}

function XemChiTiet(obj) {

    var ele = $(obj);
    var ID_DanhSach = ele.data("model-id");

    if ($.fn.DataTable.isDataTable("#dataTable_DSCQ1")) {
        $('#dataTable_DSCQ1').DataTable().clear().destroy();
    }

    if ($.fn.DataTable.isDataTable("#dataTable_DSCQ2")) {
        $('#dataTable_DSCQ2').DataTable().clear().destroy();
    }

    dt2 = $("#dataTable_DSCQ1").DataTable({
        "lengthChange": false,
        "info": false,
        "searching": false,
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
        "autoWidth": false,
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": `/NV_LapKeHoachXacMinh/LoadDataCoQuan/${ID_DanhSach}`,
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "SoLuong" },
            { "data": "Ten_Loai_CQDV" },
            { "data": "Ten" },
            { "data": "SoLuong" },
        ]
    });

    dt2.on('draw.dt', function () {
        var info = dt.page.info();
        dt2.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });


    dt3 = $("#dataTable_DSCQ2").DataTable({
        "lengthChange": false,
        "info": false,
        "searching": false,
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
        "columnDefs": [
            { className: "cn", "targets": [4] }
        ],
        "autoWidth": false,
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": `/NV_LapKeHoachXacMinh/LoadDataCanBo/${ID_DanhSach}`,
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "Ma_CanBo" },
            { "data": "HoTen" },
            { "data": "Ten" },
            { "data": "Ten_ChucVu_ChucDanh" },
            {
                "data": { "FileDinhKem": "FileDinhKem"}, "render": function (data, type, row) {
                    return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <a class="dropdown-item" href="/content/uploads/${data.FileDinhKem}" target="_blank">
                                            Tải Bản Kê Khai
                                        </a>    
                                      </div>
                                    </div>`
                   
                }
            },
        ]
    });

    dt3.on('draw.dt', function () {
        var info = dt.page.info();
        dt3.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });

    $.get("/NV_LapKeHoachXacMinh/GetKeHoachXacMinh/", { ID_DanhSach: ID_DanhSach }, (data) => {
        $('#DaDinhKem_xct').empty();
        $('#SoKeHoach_xct').val(data.SoKeHoach)
        $('#NoiDungKeHoach_xct').val(data.NoiDungKeHoach)
        $('#NgayLapKeHoach_xct').val(moment(data.NgayLapKeHoach).format("YYYY-MM-DD"))
        $('#DaDinhKem_xct').append(`<a href="/content/uploads/${data.FileKeHoach}" target="_blank">${data.FileKeHoach}</a>`)

    })
}

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
            "url": "/NV_LapKeHoachXacMinh/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [5] }
        ],
        "columns": [
            { "data": "ID_DanhSach" },
            { "data": "TenDanhSach" },
            {
                "data": "NgayLapDanhSach", "render": function (data, type, row) {
                    return moment(data).format("DD/MM/YYYY")
                }
            },
            { "data": "KeHoachNam" },
            {
                "data": { "FileDinhKem": "FileDinhKem", "TrangThaiKH": "TrangThaiKH", "TrangThaiDS": "TrangThaiDS" }, "render": function (data, type, row) {

                    if (data.TrangThaiKH == 0) {
                        return `<span class="badge badge-danger">Chưa Lập Kế Hoạch</span>`
                    }
                    else if (data.TrangThaiKH != 0 && data.TrangThaiDS == false) {
                        return `<span class="badge badge-warning">Đang Tiến Hành</span>`
                    }
                    else {
                        return `<span class="badge badge-success">Kế Hoạch Đã Được Lập</span>`
                    }
                }
            },
            {
                "data": { "ID_DanhSach": "ID_DanhSach", "TrangThaiKH": "TrangThaiKH", "FileDinhKem": "FileDinhKem", "TrangThaiDS":"TrangThaiDS" }, "render": function (data, type, row, meta) {
                    var row_xemchitiet = ``
                    var row_sua = ``
                    var row_them = ``
                    var row_lapkehoach = ``

                    if (XEMCHITIET) {
                        row_xemchitiet = ` <a class="dropdown-item" data-model-id="${data.ID_DanhSach}" onclick="XemChiTiet(this)" data-toggle="modal" data-target="#KHXM-sh">
                                            Xem Chi Tiết Kế Hoạch
                                        </a>`
                    }

                    if (SUA) {
                        row_sua = ` <a class="dropdown-item" data-model-id="${data.ID_DanhSach}" onclick="CapNhatKeHoach(this)" data-toggle="modal" data-target="#addKHXM-sh">
                                            Cập Nhật Kế Hoạch
                                        </a>`
                    }
                    if (THEM) {
                        row_them = `<a class="dropdown-item" data-model-id="${data.ID_DanhSach}" onclick="LapKeHoach(this)" data-toggle="modal" data-target="#addKHXM-sh">
                                            Lập Kế Hoạch
                                        </a>`
                    }
                    if (LAPDANHSACH) {
                        row_lapkehoach = `<a class="dropdown-item" data-model-id="${data.ID_DanhSach}" onclick="LapKeHoach(this)" data-toggle="modal" data-target="#addKHXM-sh">
                                            Lập Kế Hoạch
                                        </a>`
                    }


                    if (data.TrangThaiKH == 0) {
                        return `<div class="dropleft">
                                    <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        ${row_lapkehoach}
                                        <a class="dropdown-item" href="/content/uploads/${data.FileDinhKem}" target="_blank">
                                            Tải File Danh Sách
                                        </a>
                                    </div>
                                </div>`
                    }
                    else if (data.TrangThaiKH != 0 && data.TrangThaiDS == false) {
                        return `<div class="dropleft">
                                    <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        ${row_sua}
                                        <a class="dropdown-item" data-model-id="${data.ID_DanhSach}" onclick="HoanThanhKeHoach(this)">
                                            Hoàn Thành Kế Hoạch
                                        </a>
                                        <a class="dropdown-item" href="/content/uploads/${data.FileDinhKem}" target="_blank">
                                            Tải File Danh Sách
                                        </a>
                                    </div>
                                </div>`
                    }
                    else {
                        return `<div class="dropleft">
                                    <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_xemchitiet}
                                        <a class="dropdown-item" href="/content/uploads/${data.FileDinhKem}" target="_blank">
                                            Tải File Danh Sách
                                        </a>
                                    </div>
                                </div>`
                    }
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

    if (!THEM) {
        $('#THEM').remove()
    }
    if (!XUAT) {
        dt.buttons().disable();
    }
}

$(document).ready(async function () {
    await CheckQuyen()
    loadDataTable();

    $(".NV_Create").validate({
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
            SoKeHoach: {
                required: true,
            },
            NoiDungKeHoach: {
                required: true,
            },
            NgayLapKeHoach: {
                required: true,
            },
            FileKeHoach: {
                required: true,
            },

        },
        messages: {
            SoKeHoach: {
                required: "Số Kế Hoạch Không Được Để Trống",
            },
            NoiDungKeHoach: {
                required: "Nội Dung Kế Hoạch Không Được Để Trống",
            },
            NgayLapKeHoach: {
                required: "Ngày Lập Kế Hoạch Không Hợp lệ",
            },
            FileKeHoach: {
                required: "File Kế Hoạch Chưa Được Đính Kèm",
            },

        }
    })

    $("#search_btn").click(() => {
        var searchValue = $('#Filter').val().trim()
        dt.column(0).search(searchValue);
        dt.draw()
    })

    $("#Filter").keypress(function (e) {
        if (e.keyCode == 13) {
            dt.columns(0).search($("#Filter").val());
            dt.draw();
        }
    });
})





