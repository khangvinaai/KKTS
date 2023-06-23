var dt;
var XEM = false;
var THEM = false;
var XUAT = false;
var FILEDINHKEM = false;
var XEMCHITIET = false;
var LAPDANHSACH = false;

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
    })
}


function FnBegin() {
    $('.loading_new').removeClass('d-none');
    $("#btn_add").attr("disabled", true);
}

function FnSuccess(data) {
    $('.loading_new').addClass('d-none');

    if (data.status == "success") {
        $('.closeform').click();    
    }
    
    Swal.fire({
        icon: data.status,
        title: data.title,
        text: data.message,
        timer: 2000,
        showConfirmButton: false,
    })
    dt.draw()
}

function Failure(data) {
    $('.loading_new').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Lỗi Hệ Thống',
        timer: 2000,
        showConfirmButton: false,

    })
}

function FnBegin_DinhKem() {
    $('.loading_dk').removeClass('d-none');
    $('#submit_dinhkem').prop('disabled', true);
}

function FnSuccess_DinhKem(data) {
    $('.loading_dk').addClass('d-none');
    $('#submit_dinhkem').prop('disabled', false);
    $('.closeform').click();
    $('#FileDinhKem').val('');
    if (data.status == "success") {
        Swal.fire({
            icon: data.status,
            title: 'Thành Công',
            text: "Đã Đính Kèm Danh Sách Thành Công",
            timer: 2000,
            showConfirmButton: false,
        })
        dt.draw();
    }
    else {
        Swal.fire({
            icon: data.status,
            title: 'Cảnh Báo',
            text: 'Không Thể Đính Kèm File Danh Sách',
            timer: 2000,
            showConfirmButton: false,
        })
        dt.draw();
    }

}

function Failure_DinhKem(data) {
    $('.loading_newcoquan').addClass('d-none');
    $('#submit_dinhkem').prop('disabled', false);
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Lỗi Hệ Thống',
        timer: 2000,
        showConfirmButton: false,
    })
}

function completeDanhSach(id) {
    $.ajax({
        url: `/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/HoanThanhDanhSach/${id}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        beforeSend: function () {
            //$(`#btn_download_bankekhai_${MaKeKhai}`).addClass("d-none")
            //$(`#loading_btnPrint_bankkCBkk_${MaKeKhai}`).removeClass("d-none")
        },
        success: function (response) {
            $('.loading_dk').addClass('d-none');
            $('#submit_dinhkem').prop('disabled', false);
            $('.closeform').click();
            $('#FileDinhKem').val('');
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
            "url": "/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/LoadDataKeHoachXacMinh_DanhSachCanBoXacMinh",
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
                "data": { "TrangThai": "TrangThai", "FileDinhKem": "FileDinhKem", "TrangThaiDS":"TrangThaiDS" }, "render": function (data, type, row) {

                    if (data.TrangThai == false && data.FileDinhKem == '' && data.TrangThaiDS == 0) {
                        return `<span class="badge badge-danger">Chưa Lập Danh Sách</span>`
                    }
                    else if (data.TrangThai == true) {
                        return `<span class="badge badge-success">Hoàn Thành Danh Sách</span>`
                    }
                    else {
                        return `<span class="badge badge-warning">Đang Tiến Hành</span>`
                    }
                }
            },
          
            {
                "data": { "ID_DanhSach": "ID_DanhSach", "TrangThai": "TrangThai", "TrangThaiDS": "TrangThaiDS", "FileDinhKem": "FileDinhKem" }, "render": function (data, type, row, meta) {

                    var row_fildinhkem = ``
                    var row_lapdanhsach = ``
                    var row_xuatdanhsach = ``

                    if (LAPDANHSACH) {
                        row_lapdanhsach = ` <a class="dropdown-item" href="/danh-sach-can-bo-xac-minh/lap-danh-sach/${data.ID_DanhSach}">
                                            Lập Danh Sách
                                        </a>`
                    }
                    if (FILEDINHKEM) {
                        row_fildinhkem = `  <a class="dropdown-item" href="/content/uploads/${data.FileDinhKem}" target="_blank">
                                                Tải File Danh Sách
                                            </a>`
                    }
                    if (XUAT) {
                        row_xuatdanhsach = ` <a class="dropdown-item" onclick="print_DanhSach(this)" data-model-id="${data.ID_DanhSach}">
                                    Xuất Danh Sách
                                </a>`
                    }



                    if (data.TrangThaiDS == 0) {
                        if (row_lapdanhsach == "") {
                            console.log(1)
                            return `<div class="dropleft">
                                    <button class="btn btn-outline-info btn-sm" type="button" disabled id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                    </button>
                                </div>`
                        } else {
                            console.log(2)
                            return `<div class="dropleft">
                                    <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        ${row_lapdanhsach}
                                    </div>
                                </div>`
                        }
                        
                    }
                    else if (data.TrangThaiDS != 0 && data.TrangThai == false) {
                        console.log(3)
                        return `<div class="dropleft">
                            <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                            <i class="fa-solid fa-ellipsis-vertical"></i>
                            </button>
                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                               ${row_xuatdanhsach}
                                <a class="dropdown-item" data-model-id="${data.ID_DanhSach}" data-toggle="modal" data-target="#add-dk"  onclick="DinhKemFile(this)">
                                    Đính Kèm Danh Sách
                                </a>  
                                <a class="dropdown-item" onclick={completeDanhSach(${data.ID_DanhSach})} >
                                    Hoàn Thành Danh Sách
                                </a>
                            </div>
                        </div>`
                    }
                    else {
                        if (row_fildinhkem == '') {
                            console.log(4)
                            return `<div class="dropleft">
                            <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                            <i class="fa-solid fa-ellipsis-vertical"></i>
                            </button>
                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                <a class="dropdown-item" onclick="print_DanhSach(this)" data-model-id="${data.ID_DanhSach}">
                                    Xem Danh Sách
                                </a>
                            </div>
                            
                        </div>`
                        } else {
                            console.log(5)
                            return `<div class="dropleft">
                            <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                            <i class="fa-solid fa-ellipsis-vertical"></i>
                            </button>
                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                ${row_fildinhkem}
                            </div>
                        </div>`
                        }
                        
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
    //Kết thúc chỉnh sửa  23-2-2022
    if (!THEM) {
        $('#THEM').remove()
    }
    if (!XUAT) {
        dt.buttons().disable();
    }

}

function print_DanhSach(obj) {
    var ele = $(obj);
    var MaDanhSach = ele.data("model-id");
    $.ajax({
        url: `/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/InDanhSach/${MaDanhSach}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        beforeSend: function () {
            //$(`#btn_download_bankekhai_${MaKeKhai}`).addClass("d-none")
            //$(`#loading_btnPrint_bankkCBkk_${MaKeKhai}`).removeClass("d-none")
        },
        success: function (response) {
            //$(`#btn_download_bankekhai_${MaKeKhai}`).removeClass("d-none")
            //$(`#loading_btnPrint_bankkCBkk_${MaKeKhai}`).addClass("d-none")
            window.open("/danh-sach-can-bo-xac-minh/ban-in/" + response)
        },
        error: function (response) {
            Swal.fire({
                icon: 'error',
                title: 'Thất Bại',
                text: 'Lỗi hệ thống',
                timer: 2000,
                showConfirmButton: false,
            }).then(() => {
                //$(`#btn_download_bankekhai_${MaKeKhai}`).removeClass("d-none")
                //$(`#loading_btnPrint_bankkCBkk_${MaKeKhai}`).addClass("d-none")
            })
        }
    });
}


//function print_DanhSachThongKe(obj) {
//    var ele = $(obj);
//    var MaDanhSach = ele.data("model-id");
//    $.ajax({
//        url: `/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/InDanhSachThongKe/${MaDanhSach}`,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        type: "POST",
//        beforeSend: function () {
//            //$(`#btn_download_bankekhai_${MaKeKhai}`).addClass("d-none")
//            //$(`#loading_btnPrint_bankkCBkk_${MaKeKhai}`).removeClass("d-none")
//        },
//        success: function (response) {
//            //$(`#btn_download_bankekhai_${MaKeKhai}`).removeClass("d-none")
//            //$(`#loading_btnPrint_bankkCBkk_${MaKeKhai}`).addClass("d-none")
//            window.open("/danh-sach-can-bo-xac-minh/ban-in/" + response)
//        },
//        error: function (response) {
//            Swal.fire({
//                icon: 'error',
//                title: 'Thất Bại',
//                text: 'Lỗi hệ thống',
//                timer: 2000,
//                showConfirmButton: false,
//            }).then(() => {
//                //$(`#btn_download_bankekhai_${MaKeKhai}`).removeClass("d-none")
//                //$(`#loading_btnPrint_bankkCBkk_${MaKeKhai}`).addClass("d-none")
//            })
//        }
//    });
//}




function DinhKemFile(obj) {
    var ele = $(obj);
    var ID_DanhSach = ele.data("model-id");
    $('#ID_DanhSach').val(ID_DanhSach);
    $('#DaDinhKem').empty();
    $.ajax({
        url: `/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/GetFileDinhKem/${ID_DanhSach}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (response) {

            if (response != "") {
                var row = `<div class="col-12">
                            <div class="form-group">
                                <label>Danh Sách Đã Đính Kèm<span style="color: red"> *</span></label>
                                <span><a target="_blank" href ="/Content/uploads/${response}">${response}</a></span>
                            </div>
                        </div>`
                $('#DaDinhKem').append(row)
            }

        }
    })

}

$(document).ready( async function () {
    await CheckQuyen();
    loadDataTable();

    $("#search_btn").click(() => {
        var searchValue = $('#Filter').val().trim()
        dt.column(0).search(searchValue);
        dt.draw()
    })

    $("#Filter").keypress( function (e) {
        if (e.keyCode == 13) {
            dt.columns(0).search($("#Filter").val());
            dt.draw();
        }
    });

    

})





