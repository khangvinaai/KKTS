
var dt;
var XEM = false;
var XUAT = false;
var FILEDINHKEM = false;
var XEMCHITIET = false;
var LAPDANHSACH = false;

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
            "url": "/NV_LapKeHoachKeKhai/LoadData_DSCanBoKeKhai",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [8] }
        ],
        "columns": [
            { "data": "MaKeHoachKeKhai" },
            {
                "data": { "TenKeHoachKeKhai": "TenKeHoachKeKhai", "MaKeHoachKeKhai": "MaKeHoachKeKhai" }, "render": function (data, type, row, meta) {
                    return `<b>${data.TenKeHoachKeKhai}</b>`
                }
            },
            {
                "data": "Ma_Loai_KeKhai", "render": function (data, type, row, meta) {
                    if (data == 3) {
                        return `Kê Khai Lần Đầu`
                    }
                    else if (data == 4) {
                        return `Kê Khai Hằng Năm`
                    }
                    else {
                        return `Kê Khai Phục Vụ Công Tác Cán Bộ`
                    }

                }
            },
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
                "data": "TrangThaiTonTaiDS", "render": function (data, type, row, meta) {
                   return `${data} Cán Bộ`
                }
            },
            {
                "data": { "TrangThaiTonTaiDS": "TrangThaiTonTaiDS", "TrangThaiDS": "TrangThaiDS"}, "render": function (data, type, row, meta) {

                    if (data.TrangThaiTonTaiDS == 0 && data.TrangThaiDS == false) {
                        return `<span class="badge badge-danger">Chưa Lập Danh Sách</span>`
                    }
                    else if (data.TrangThaiTonTaiDS != 0 && data.TrangThaiDS == false) {
                        return `<span class="badge badge-warning">Đang Lập Danh Sách</span>`
                    }
                    else {
                        return `<span class="badge badge-success">Đã Lập Danh Sách</span>`
                    }
                }
            },
            {
                "data": { "TrangThaiTonTaiDS": "TrangThaiTonTaiDS", "TrangThaiDS": "TrangThaiDS", "MaKeHoachKeKhai": "MaKeHoachKeKhai", "NghiDinh": "NghiDinh"  }, "render": function (data, type, row, meta) {

                    var row_lapdanhsach = ``
                    var row_chinhsua = ``
                    var row_hoanthanh = ``
                    var row_xemchitiet = ``
                    var row_filedinhkem = ``

                    if (FILEDINHKEM) {
                        row_filedinhkem = `<a class="dropdown-item" href="/content/uploads/${data.NghiDinh}" target="_blank">
                                                    File Đính Kèm Kế Hoạch
                                            </a>`
                    }


                    if (data.TrangThaiTonTaiDS == 0 && data.TrangThaiDS == false) {

                        if (LAPDANHSACH) {
                            row_lapdanhsach = `<a class="dropdown-item" href="/danh-sach-can-bo-ke-khai/lap-danh-sach/${data.MaKeHoachKeKhai}" >
                                                    Lập Danh Sách
                                               </a>`
                        }

                        if (row_lapdanhsach != `` || row_filedinhkem != ``) {
                            return `<div class="dropleft">
                              <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                <i class="fa-solid fa-ellipsis-vertical"></i>
                              </button>
                              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                  ${row_lapdanhsach}
                                  ${row_filedinhkem}
                              </div>
                            </div>`
                        }
                        else return `<div class="dropleft">
                              <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                <i class="fa-solid fa-ellipsis-vertical"></i>
                              </button>
                              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                  ${row_lapdanhsach}
                              </div>
                            </div>`
                      
                    }
                    else if (data.TrangThaiTonTaiDS != 0 && data.TrangThaiDS == false) {
                         
                        if (LAPDANHSACH) {
                            row_chinhsua = `<a class="dropdown-item" href="/danh-sach-can-bo-ke-khai/lap-danh-sach/${data.MaKeHoachKeKhai}" >
                                                Chỉnh Sửa Danh Sách
                                            </a>`
                            row_hoanthanh = `<a class="dropdown-item" data-model-id="${data.MaKeHoachKeKhai}" onclick="HoanThanh(this)" >
                                                Hoàn Thành Danh Sách
                                            </a>`
                        }
                        if (row_chinhsua != `` || row_hoanthanh != `` || row_filedinhkem != ``) {
                            return `<div class="dropleft">
                              <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                <i class="fa-solid fa-ellipsis-vertical"></i>
                              </button>
                              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                ${row_chinhsua}   
                                ${row_hoanthanh}
                                ${row_filedinhkem}
                              </div>
                            </div>`
                        }
                        else return `<div class="dropleft">
                              <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                <i class="fa-solid fa-ellipsis-vertical"></i>
                              </button>
                              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                ${row_chinhsua}   
                                ${row_hoanthanh}  
                              </div>
                            </div>`
                       
                    }
                    else {
                        if (XEMCHITIET) {
                            row_xemchitiet = `<a class="dropdown-item" href="/danh-sach-can-bo-ke-khai/lap-danh-sach/${data.MaKeHoachKeKhai}" >
                                                Xem Chi Tiết Danh Sách
                                              </a>`
                        }
                        if (row_xemchitiet != `` || row_filedinhkem != ``) {
                            return `<div class="dropleft">
                              <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                <i class="fa-solid fa-ellipsis-vertical"></i>
                              </button>
                              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                  ${row_xemchitiet}
                                  ${row_filedinhkem}
                              </div>
                            </div>`
                        }
                        else return `<div class="dropleft">
                              <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                <i class="fa-solid fa-ellipsis-vertical"></i>
                              </button>
                              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                  ${row_xemchitiet}   
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

    if (!XUAT) {
        dt.buttons().disable();
    }

}

function HoanThanh(obj) {

    var ele = $(obj);
    var Ma_KeHoach = ele.data("model-id");

    swal.fire({
        title: 'Xác Nhận Hoàn Thành?',
        text: "Vui Lòng Kiểm Tra Trước Khi Hoàn Thành, Sau Khi Hoàn Thành Không Thể Chỉnh Sửa!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Đã Kiểm Tra, Hoàn Thành!',
        cancelButtonText: 'Không, Quay lại!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/NV_DanhSachCanBoKeKhai/HoanThanhDanhSach",
                type: "post",
                dataType: "json",
                data: {
                    MaKeHoachKeKhai: Ma_KeHoach,
                },
                success: function (result) {
                    if (result.status == "success") {
                        Swal.fire({
                            icon: 'success',
                            title: 'Thành Công',
                            text: result.message,
                            timer: 2000,
                            showConfirmButton: false,
                        })
                        dt.draw();
                    }
                    else if (result.status == "warning") {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Cảnh Báo',
                            text: result.message,
                            timer: 2000,
                            showConfirmButton: false,
                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Thất Bại',
                            text: 'Lỗi hệ thống',
                            timer: 2000,
                            showConfirmButton: false,
                        })
                    }
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Thất Bại',
                        text: 'Lỗi hệ thống',
                        timer: 2000,
                        showConfirmButton: false,
                    })
                }
            })
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {

        }
    })
}

async function CheckQuyen() {
    await $.get("/Home/GetQuyen", { MenuCode: "NV_DanhSachCanBoKeKhai" }, (data) => {
        if (data.includes("XEM")) {
            XEM = true;
        }
        if (data.includes("XUAT")) {
            XUAT = true;
        }
        if (data.includes("FILEDINHKEM")) {
            FILEDINHKEM = true;
        }
        if (data.includes("XEMCHITIET")) {
            XEMCHITIET = true;
        }
        if (data.includes("LAPDANHSACH")) {
            LAPDANHSACH = true;
        }
    })
}

$(document).ready(async function () {
    await CheckQuyen();
    loadDataTable(); 
})


