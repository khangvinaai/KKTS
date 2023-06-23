

// kết thúc chỉnh sửa
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

        //Hoàng chỉnh sửa option tỉnh, huyện 23-2-2022
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/NV_LapKeHoachKeKhai/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "MaKeHoachKeKhai" },
            { "data": "TenKeHoachKeKhai" },
           
            { "data": "KeHoachNam" },
            {
                "data": "ThoiGianBatDau", "render": function (data, type, row, meta) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            {
                "data": "ThoiGianKetThuc", "render": function (data, type, row, meta) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            {
                "data": { "NghiDinh": "NghiDinh", "MaKeHoachKeKhai": "MaKeHoachKeKhai" }, "render": function (data, type, row, meta) {
                    return `<a class="btn btn-outline-info btn-sm" href="/NV_LapKeHoachKeKhai/Download?id=${data.MaKeHoachKeKhai}">
                                <i class="fa fa-download" aria-hidden="true"></i> ${data.NghiDinh}
                            </a>`
                } },
            {
                "data": "Ma_CoQuan_DonVi", "render": function (data, type, row) {
                    return `
                            <button class="btn btn-outline-success btn-sm" data-model-id="${data}" data-toggle="modal" data-target="#add-sh1">
                                Xem Chi Tiết
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
    //Kết thúc chỉnh sửa  23-2-2022
}



//Hoàng chỉnh sửa Clear data form thêm mới 23-2-2022
function clearDataAdd() {
    $("#TenKeHoachKeKhai").val("")
    $("#NghiDinh").val("")
    $("#KeHoachNam").val("")
    $("#ThoiGianBatDau").val("")
    $("#ThoiGianKetThuc").val("")
}
//Kết thúc chỉnh sửa  23-2-2022


// thông báo thêm Cơ Quan
function FnBegin() {
    $('.loading_lkh').removeClass('d-none');
}

function FnSuccess(data) {
    clearDataAdd()
    $('.loading_lkh').addClass('d-none');
    $('.closeform').click();

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Cơ Quan Đơn Vị đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    dt.draw();
}

function Failure(data) {
    $('.loading_lkh').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Thêm mới không thành công',
        timer: 2000,
        showConfirmButton: false,
    })
}


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


    $("#TenKeHoachKeKhai").val("")
    $("#NghiDinh").val("")
    $("#KeHoachNam").val("")
    $("#ThoiGianBatDau").val("")
    $("#ThoiGianKetThuc").val("")
    //validate form thêm dữ liệu
    $(".formNV_LKH").validate({
        ignore: [],
        errorElement: 'div',
        rules: {
            "TenKeHoachKeKhai": {
                required: true,
            },
            "NghiDinh": {
                required: true,
            },
            "KeHoachNam": {
                required: true,
                number: true,
            },
            "ThoiGianBatDau": {
                required: true,
            },
            "ThoiGianKetThuc": {
                required: true,
            },
        },
        messages: {
            TenKeHoachKeKhai: "Vui lòng nhập Tên Kế Hoạch",
            NghiDinh: "Vui lòng Nhập Nghị Định",
            KeHoachNam: {
                required: "Vui lòng nhập Kế Hoạch Năm ",
                number: "Kế Hoạch Năm vui lòng nhập số",
            },
            ThoiGianBatDau: {
                required: "Vui lòng nhập Thời Gian Bắt Đầu",
            },
            ThoiGianBatDau: {
                required: "Vui lòng nhập Thời Gian Kết Thúc",
            },


        },
       
    });

    // auto focus input
    $('.modal').on('shown.bs.modal', function () {
        $(this).find('[autofocus]').focus();
    });
})


