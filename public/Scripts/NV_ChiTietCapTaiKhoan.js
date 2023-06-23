

var s1 = window.location.href.split("/");
var idd = s1[s1.length - 1];

$.get("/NV_CapTaiKhoan/detailTaiKhoan/", { id: idd }, (data) => {
    console.log(data)
    if (data[0].TrangThai == false) {
        $("#xacNhanCapTaiKhoan").prop('disabled', false)
    } else {
        $("#xacNhanCapTaiKhoan").prop('disabled', true)
        $("#xacNhanCapTaiKhoan").html('Đã Cấp');
    }
})


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
            "url": `/NV_CapTaiKhoan_ChiTiet/loadData/${idd}`,
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "tenCB" },
            { "data": "tenCanBo" },
            {
                "data": "DoB"
            },
           
            {
                "data": "tenCoQuan"
            },
            {
                "data": "ten_ChucVu_ChucDanh"
            },
            {
                "data": "nguoiGui"
            },
            {
                "data": "NgayGui", "render": function (data, type, row, meta) {
                    return moment(data).format('DD/MM/YYYY hh:mm:ss');
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


//Xác nhận cấp tài khoản tự động
// 
var pathname = window.location.pathname.split('/');
var idYeuCau = parseInt(pathname[pathname.length - 1])
$("#xacNhanCapTaiKhoan").click(() => {
    $.get("/NV_CapTaiKhoan_ChiTiet/XacNhanCapTaiKhoan/", { id: idYeuCau }, (data) => {
        console.log(document.location.host);
        if (data == "True") {
            FnSuccess_Duyet()
            setTimeout(function () { window.location.replace("https://"+document.location.host + "/NV_CapTaiKhoan/") }, 500);
        } else {
            FnSuccess_Duyet()
        }
    })
})

function FnBegin_Duyet() {
    $('.loading_Duyet').removeClass('d-none');
}

function FnSuccess_Duyet() {
    $('.loading_Duyet').addClass('d-none')
    $('.closeform').click()

    Swal.fire({
        icon: 'success',
        title: 'Thành công',
        text: `Đã cấp tài khoản thành công`,
        timer: 2000,
        showConfirmButton: false,
    }).then(() => {  })
}

function Failure_Duyet() {
    $('.loading_Duyet').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi',
        text: `Cấp tài khoản không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
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

