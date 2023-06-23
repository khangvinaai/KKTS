var dtt1, dtt2, dtt3;
var url = window.location.href.split('/')
var MaKeHoachKeKhai = url[url.length - 1]
var loaiKeKhai;

function loadData1() {
    dtt1 = $("#dataTable").DataTable({
        "lengthChange": false,
        "info": true,
        "searching": true,
        "padging": false,
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

        'columnDefs': [
            {
                'targets': 0,
                'checkboxes': true
            },

        ],


        "ajax": {
            "url": `/NV_DanhSachCanBoKeKhai/LoadData?id=${url[url.length - 1]}`,
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "Ma_CanBo" },
            { "data": "HoTen" },
            { "data": "DoB" },
            { "data": "Ten_ChucVu_ChucDanh" },
            { "data": "Ten" },
        ]

    });


}
function loadData2() {

    dtt2 = $("#dataTable2").DataTable({
        "lengthChange": false,
        "info": true,
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


        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": `/NV_DanhSachCanBoKeKhai/LoadData2?id=${url[url.length - 1]}`,
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [7, 8] }
        ],
        "columns": [
            {
                "data": "Ma_CanBo"
            },
            {
                "data": { "Ma_CanBo": "Ma_CanBo", "HoTen": "HoTen" }, "render": function (data, type, row, meta) {
                    return data.HoTen
                },
            },
            { "data": "SoCCCD" },
            { "data": "Ten_ChucVu_ChucDanh" },
            { "data": "Ten" },
            { "data": "TenKeHoachKeKhai" },
            { "data": "KeHoachNam" },
            {
                "data": { "isKeKhai": "isKeKhai", "MaKeHoachKeKhai": "MaKeHoachKeKhai", "Ma_CanBo": "Ma_CanBo" }, "render": (data, type, row, meta) => {

                    if (data.isKeKhai == false) {
                        return ` <span class="badge badge-danger">Chưa kê khai</span>`
                    } else {
                        return `<span class="badge badge-success">Đã kê khai</span>`
                    }
                }
            },
            {
                "data": { "FileDinhKem": "FileDinhKem", "isKeKhai": "isKeKhai", "Ma_CanBo": "Ma_CanBo", "TrangThai": "TrangThai", "Ma_CanBo": "Ma_CanBo" }, "render": (data, type, row, meta) => {

                    if (data.TrangThai == true && data.isKeKhai == false) {
                        return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <a class="dropdown-item" data-toggle="modal" data-target="#ThongBao" data-model-macoquan="" data-model-loai="0"  data-model-macanbo="${data.Ma_CanBo}" onclick="ThongBao(this)" >
                                            Gửi Thông Báo
                                        </a>  
                                        <a class="dropdown-item" data-toggle="modal" data-target="#ThongBao" data-model-macoquan="" data-model-loai="1"  data-model-macanbo="${data.Ma_CanBo}" onclick="ThongBao(this)">
                                            Gửi Email
                                        </a> 
                                      </div>
                                    </div>`
                    }
                    else if (data.TrangThai == true && data.isKeKhai == true) {
                        return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <a class="dropdown-item" href="/content/uploads/${data.FileDinhKem}" target="_blank">
                                            Tải Bản Kê Khai
                                        </a>  
                                        <a class="dropdown-item" data-toggle="modal" data-target="#ThongBao" data-model-macoquan="" data-model-loai="0"   data-model-macanbo="${data.Ma_CanBo}" onclick="ThongBao(this)" >
                                            Gửi Thông Báo
                                        </a>  
                                        <a class="dropdown-item"  data-toggle="modal" data-target="#ThongBao" data-model-macoquan="" data-model-loai="1"  data-model-macanbo="${data.Ma_CanBo}" onclick="ThongBao(this)" >
                                            Gửi Email
                                        </a> 
                                      </div>
                                    </div>`
                    }
                    else if (data.TrangThai == false) {
                        return `
                                <button class="btn btn-outline-danger btn-sm delete" data-model-id="${data.Ma_CanBo}" onclick="Delete(this)">
                                    <i class="fas fa-trash"></i>
                                </button>
                                `
                    }

                }
            },

        ]

    });

    dtt2.on('draw.dt', function () {
        var info = dtt2.page.info();
        dtt2.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });

}
function loadData3() {

    dtt3 = $("#dataTable3").DataTable({
        "lengthChange": false,
        "info": true,
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


        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": `/NV_DanhSachCanBoKeKhai/LoadData3?id=${url[url.length - 1]}`,
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [7, 8] }
        ],
        "columns": [
            {
                "data": "Ma_CanBo"
            },
            {
                "data": { "Ma_CanBo": "Ma_CanBo", "HoTen": "HoTen" }, "render": function (data, type, row, meta) {
                    return data.HoTen
                },
            },
            { "data": "SoCCCD" },
            { "data": "Ten_ChucVu_ChucDanh" },
            { "data": "Ten" },
            { "data": "TenKeHoachKeKhai" },
            { "data": "KeHoachNam" },
            {
                "data": { "isKeKhai": "isKeKhai", "MaKeHoachKeKhai": "MaKeHoachKeKhai", "Ma_CanBo": "Ma_CanBo" }, "render": (data, type, row, meta) => {

                    if (data.isKeKhai == false) {
                        return ` <span class="badge badge-danger">Chưa kê khai</span>`
                    } else {
                        return `<span class="badge badge-success">Đã kê khai</span>`
                    }
                }
            },
            {
                "data": { "FileDinhKem": "FileDinhKem", "isKeKhai": "isKeKhai", "Ma_CanBo": "Ma_CanBo", "TrangThai": "TrangThai", }, "render": (data, type, row, meta) => {

                    if (data.TrangThai == true && data.isKeKhai == false) {
                        return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <a class="dropdown-item" data-toggle="modal" data-target="#ThongBao" data-model-macoquan="" data-model-loai="0"  data-model-macanbo="${data.Ma_CanBo}" onclick="ThongBao(this)" >
                                            Gửi Thông Báo
                                        </a>  
                                        <a class="dropdown-item" data-toggle="modal" data-target="#ThongBao" data-model-macoquan="" data-model-loai="1"  data-model-macanbo="${data.Ma_CanBo}" onclick="ThongBao(this)"  >
                                            Gửi Email
                                        </a> 
                                      </div>
                                    </div>`
                    }
                    else if (data.TrangThai == true && data.isKeKhai == true) {
                        return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <a class="dropdown-item" href="/content/uploads/${data.FileDinhKem}" target="_blank">
                                            Tải Bản Kê Khai
                                        </a>  
                                        <a class="dropdown-item" data-toggle="modal" data-target="#ThongBao" data-model-macoquan="" data-model-loai="0"  data-model-macanbo="${data.Ma_CanBo}" onclick="ThongBao(this)" >
                                            Gửi Thông Báo
                                        </a>  
                                        <a class="dropdown-item" data-toggle="modal" data-target="#ThongBao" data-model-macoquan="" data-model-loai="1"  data-model-macanbo="${data.Ma_CanBo}" onclick="ThongBao(this)"  >
                                            Gửi Email
                                        </a> 
                                      </div>
                                    </div>`
                    }
                    else if (data.TrangThai == false) {
                        return `
                                <button class="btn btn-outline-danger btn-sm delete" data-model-id="${data.Ma_CanBo}" onclick="Delete(this)">
                                    <i class="fas fa-trash"></i>
                                </button>
                                `
                    }

                }
            },

        ]

    });

    dtt3.on('draw.dt', function () {
        var info = dtt3.page.info();
        dtt3.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });
}


function print_bankkCBkk(obj) {
    var ele = $(obj);
    var MaCanBo = ele.data("model-id");

    $.ajax({
        url: `/NV_KeKhai_TSTN/InBanKeKhaiCanBo?MaKeHoachKeKhai=${MaKeHoachKeKhai}&MaCanBo=${MaCanBo}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        beforeSend: function () {
            $(`#btn_download_bankekhai_${MaCanBo}`).addClass("d-none")
            $(`#loading_btnPrint_bankkCBkk_${MaCanBo}`).removeClass("d-none")
        },
        success: function (response) {
            $(`#btn_download_bankekhai_${MaCanBo}`).removeClass("d-none")
            $(`#loading_btnPrint_bankkCBkk_${MaCanBo}`).addClass("d-none")
            window.open("/ke-khai-tai-san/ban-in/" + response)
        },
        error: function (response) {
            Swal.fire({
                icon: 'error',
                title: 'Thất Bại',
                text: 'Lỗi hệ thống',
                timer: 2000,
                showConfirmButton: false,
            }).then(() => {
                $(`#btn_download_bankekhai_${MaCanBo}`).removeClass("d-none")
                $(`#loading_btnPrint_bankkCBkk_${MaCanBo}`).addClass("d-none")
            })
        }
    });

}

function Delete(obj) {

    var ele = $(obj);
    var Ma_CanBo = ele.data("model-id");

    var url = `/NV_DanhSachCanBoKeKhai/deleteDanhSach/`
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
            $.post(url, { Ma_CanBo: Ma_CanBo, MaKeHoachKeKhai: MaKeHoachKeKhai })
                .done(function (data) {
                    if (data.isConfirmed != false) {

                        Swal.fire({
                            icon: 'success',
                            title: 'Thành Công',
                            text: `${data.message}`,
                            timer: 2000,
                            showConfirmButton: false,
                        })
                        dtt1.ajax.reload();
                        dtt2.ajax.reload();
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Không thành công',
                            text: `${data.message}`,
                            timer: 2000,
                            showConfirmButton: false,
                        })
                    }

                })
                .fail(function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `Lỗi hệ thống không thể xóa`,
                        timer: 2000,
                        showConfirmButton: false,
                    })
                });
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {

        }
    })

}

function Delete1(obj) {

    var ele = $(obj);
    var Ma_CanBo = ele.data("model-id");

    var url = `/NV_DanhSachCanBoKeKhai/deleteDanhSach1/`
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
            $.post(url, { Ma_CanBo: Ma_CanBo, MaKeHoachKeKhai: MaKeHoachKeKhai })
                .done(function (data) {
                    if (data.isConfirmed != false) {

                        Swal.fire({
                            icon: 'success',
                            title: 'Thành Công',
                            text: `${data.message}`,
                            timer: 2000,
                            showConfirmButton: false,
                        })
                        dtt1.ajax.reload();
                        dtt2.ajax.reload();
                        dtt3.ajax.reload();
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Không thành công',
                            text: `${data.message}`,
                            timer: 2000,
                            showConfirmButton: false,
                        })
                    }

                })
                .fail(function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `Lỗi hệ thống không thể xóa`,
                        timer: 2000,
                        showConfirmButton: false,
                    })
                });
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {

        }
    })

}

$("#btnPrint_ds").click(() => {
    $.ajax({
        url: `/NV_DanhSachCanBoKeKhai/BanInDanhSachCanBo/${url[url.length - 1]}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        beforeSend: function () {
            $("#btnPrint_ds").addClass("d-none")
            $("#loading_btnPrint_dsCBHN").removeClass("d-none")
        },
        success: function (response) {
            $("#btnPrint_ds").removeClass("d-none")
            $("#loading_btnPrint_dsCBHN").addClass("d-none")
            window.open("/danh-sach-can-bo-ke-khai/ban-in/" + response)
        },
        error: function (response) {
            Swal.fire({
                icon: 'error',
                title: 'Thất Bại',
                text: 'Lỗi hệ thống',
                timer: 2000,
                showConfirmButton: false,
            }).then(() => {
                $("#btnPrint_ds").removeClass("d-none")
                $("#loading_btnPrint_dsCBHN").addClass("d-none")
            })


        }
    });
})

$("#btnPrint_dsbs").click(() => {
    $.ajax({
        url: `/NV_DanhSachCanBoKeKhai/BanInDanhSachCanBo1/${url[url.length - 1]}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        beforeSend: function () {
            $("#btnPrint_dsbs").addClass("d-none")
            $("#loading_btnPrint_dsCBBS").removeClass("d-none")
        },
        success: function (response) {
            $("#btnPrint_dsbs").removeClass("d-none")
            $("#loading_btnPrint_dsCBBS").addClass("d-none")
            window.open("/danh-sach-can-bo-ke-khai/ban-in/" + response)
        },
        error: function (response) {
            Swal.fire({
                icon: 'error',
                title: 'Thất Bại',
                text: 'Lỗi hệ thống',
                timer: 2000,
                showConfirmButton: false,
            }).then(() => {
                $("#btnPrint_dsbs").removeClass("d-none")
                $("#loading_btnPrint_dsCBBS").addClass("d-none")
            })


        }
    });
})

$(document).ready(function () {
    loadData1()
    loadData2()
    loadData3()

    if ($('#MaLoaiKeKhai').val() != 0) {
        $('#MaLoaiKeKhai').select2()
    }


    $("#search_1").click(() => {
        var searchValue = $('#Filter_data1').val()
        dtt1.search(searchValue).draw();
    })
    $("#Filter_data1").keypress(function (e) {
        if (e.keyCode == 13) {
            dtt1.search($("#Filter_data1").val()).draw();
        }
    });

    $("#search_2").click(() => {
        var searchValue = $('#Filter_data2').val().trim()
        dtt2.column(0).search(searchValue);
        dtt2.draw()
    })
    $("#Filter_data2").keypress(function (e) {
        if (e.keyCode == 13) {
            dtt2.columns(0).search($("#Filter_data2").val());
            dtt2.draw();
        }
    });

    $("#search_3").click(() => {
        var searchValue = $('#Filter_data3').val().trim()
        dtt3.column(0).search(searchValue);
        dtt3.draw()
    })
    $("#Filter_data3").keypress(function (e) {
        if (e.keyCode == 13) {
            dtt3.columns(0).search($("#Filter_data3").val());
            dtt3.draw();
        }
    });


    $("#LuuDanhSach").click(() => {
        var selected = [];

        var rows_selected = dtt1.column(0).checkboxes.selected();
        $.each(rows_selected, function (index, rowId) {
            selected.push(rowId);
        });

        $.ajax({
            url: "/NV_DanhSachCanBoKeKhai/LuuDanhSachCanBoKeKhai",
            type: "post",
            dataType: "json",
            data: {
                MaLoaiKeKhai: $('#MaLoaiKeKhai').val(),
                MaCanBo: selected,
                MaKeHoachKeKhai: MaKeHoachKeKhai,
            },
            beforeSend: function () {

                $("#LuuDanhSach").addClass("d-none")
                $("#loading_savedanhsach").removeClass("d-none")

            },
            success: function (result) {
                if (result.status == "success") {

                    if ($('#MaLoaiKeKhai').val() != 0) {
                        dtt1.ajax.reload();
                        dtt2.ajax.reload();
                        dtt3.ajax.reload();
                    }
                    else {
                        dtt1.ajax.reload();
                        dtt2.ajax.reload();
                    }
                    Swal.fire({
                        icon: result.status,
                        title: 'Thành Công',
                        text: result.message,
                        timer: 2000,
                        showConfirmButton: false,
                    })
                }
                else if (result.status == "warning") {
                    Swal.fire({
                        icon: result.status,
                        title: 'Cảnh Báo',
                        text: result.message,
                        timer: 2000,
                        showConfirmButton: false,
                    })
                }
                else {
                    Swal.fire({
                        icon: result.status,
                        title: 'Lỗi',
                        text: result.message,
                        timer: 2000,
                        showConfirmButton: false,
                    })
                }

                $("#LuuDanhSach").removeClass("d-none")
                $("#loading_savedanhsach").addClass("d-none")

            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Thất Bại',
                    text: 'Lỗi hệ thống',
                    timer: 2000,
                    showConfirmButton: false,
                })
                $("#LuuDanhSach").removeClass("d-none")
                $("#loading_savedanhsach").addClass("d-none")
            }
        });
    })


})
