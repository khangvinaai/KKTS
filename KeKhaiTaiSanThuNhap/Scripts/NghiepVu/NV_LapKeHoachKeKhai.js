var dt;

var XEM = false;
var THEM = false;
var XUAT = false;
var XEMCHITIET = false;
var FILEDINHKEM = false;


var year = new Date().getFullYear();
var min = `${year-5}-01-01`
var max = `${year+5}-12-31`

//$('#ThoiGianBatDau').attr("min", min);
//$('#ThoiGianKetThuc').attr("max", max);

//$('#ThoiGianKetThuc').attr("min", min);
//$('#ThoiGianBatDau').attr("max", max);


function checkloaikekhai() {
    $("#KKTT").empty()
    if ($("#Ma_Loai_KeKhai").val() == 4) {
        $.post("/NV_LapKeHoachKeKhai/CheckLoaiKeHoachKeKhai", { KeHoachNam: $("#KeHoachNam").val(), LoaiKeKhai: $("#Ma_Loai_KeKhai").val() })
            .done(function (data) {
                if (data.type == "warning") {
                    min = data.thoigianbatdau;
                    max = data.thoigianketthuc;

                    $('#ThoiGianBatDau').attr("min", min);
                    $('#ThoiGianKetThuc').attr("max", max);
                    $('#ThoiGianKetThuc').attr("min", min);
                    $('#ThoiGianBatDau').attr("max", max);


                    $('#ThoiGianBatDau').val(min);
                    $('#ThoiGianKetThuc').val(max);


                    var row = ` <div class="col-12 d-flex">
                                            <hr style="width: 20%" />
                                            <span style="font-size: 17px;">Kế Hoạch Kê Khai Hằng Năm của Tỉnh Năm ${$("#KeHoachNam").val()}</span>
                                            <hr style="width: 20%" />
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Bắt Đầu Kê khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianBatDauKKTT" name="ThoiGianBatDauKKTT" value=${data.thoigianbatdau}>
                                            </div>
                                        </div>

                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Kết Thúc Kê Khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianKetThucKKTT" name="ThoiGianKetThucKKTT" value=${data.thoigianketthuc}>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="form-group">
                                                <label>Đính Kèm Tệp<span style="color: red"> *</span></label>
                                                <span><a href ="/Content/uploads/${data.file}" target="_blank">${data.file}<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-earmark-arrow-down-fill" viewBox="0 0 16 16">
  <path d="M9.293 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V4.707A1 1 0 0 0 13.707 4L10 .293A1 1 0 0 0 9.293 0zM9.5 3.5v-2l3 3h-2a1 1 0 0 1-1-1zm-1 4v3.793l1.146-1.147a.5.5 0 0 1 .708.708l-2 2a.5.5 0 0 1-.708 0l-2-2a.5.5 0 0 1 .708-.708L7.5 11.293V7.5a.5.5 0 0 1 1 0z"/>
</svg></a></span>
                                            </div>
                                        </div>`
                    $("#KKTT").append(row)

                    Swal.fire({
                        icon: 'warning',
                        title: `Thông Báo`,
                        text: data.message,
                        timer: 5000,
                        showConfirmButton: false,
                    })
                }
                else if (data.type == "error") {
                    Swal.fire({
                        icon: 'warning',
                        title: `Quay Lại Sau`,
                        text: data.message,
                        timer: 5000,
                        showConfirmButton: false,
                    }).then(() => {
                        $("#Ma_Loai_KeKhai").select2("val", "3");
                    })
                }
            })
            .fail(function (data) {
                Swal.fire({
                    icon: 'error',
                    title: 'Không thành công',
                    text: `Hệ thống bị lỗi, không thể lấy dữ liệu`,
                    timer: 2000,
                    showConfirmButton: false,

                })
            });
    }
    else if ($("#Ma_Loai_KeKhai").val() == 3) {
        $.post("/NV_LapKeHoachKeKhai/CheckLoaiKeHoachKeKhai", { KeHoachNam: $("#KeHoachNam").val(), LoaiKeKhai: $("#Ma_Loai_KeKhai").val() })
            .done(function (data) {
                if (data.type == "warning") {
                    var row = ` <div class="col-12 d-flex">
                                            <hr style="width: 20%" />
                                            <span style="font-size: 17px;">Kế Hoạch Kê Khai Lần Đầu của Tỉnh Năm 2021</span>
                                            <hr style="width: 20%" />
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Bắt Đầu Kê khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianBatDauKKTT" name="ThoiGianBatDauKKTT" value=${data.thoigianbatdau}>
                                            </div>
                                        </div>

                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Kết Thúc Kê Khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianKetThucKKTT" name="ThoiGianKetThucKKTT" value=${data.thoigianketthuc}>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="form-group">
                                                <label>Đính Kèm Tệp<span style="color: red"> *</span></label>
                                                <span><a href ="/Content/uploads/${data.file}" target="_blank">${data.file}<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-earmark-arrow-down-fill" viewBox="0 0 16 16">
  <path d="M9.293 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V4.707A1 1 0 0 0 13.707 4L10 .293A1 1 0 0 0 9.293 0zM9.5 3.5v-2l3 3h-2a1 1 0 0 1-1-1zm-1 4v3.793l1.146-1.147a.5.5 0 0 1 .708.708l-2 2a.5.5 0 0 1-.708 0l-2-2a.5.5 0 0 1 .708-.708L7.5 11.293V7.5a.5.5 0 0 1 1 0z"/>
</svg></a></span>
                                            </div>
                                        </div>`
                    $("#KKTT").append(row)

                    Swal.fire({
                        icon: 'warning',
                        title: `Thông Báo`,
                        text: data.message,
                        timer: 5000,
                        showConfirmButton: false,
                    })
                }
                else if (data.type == "error") {
                    Swal.fire({
                        icon: 'warning',
                        title: `Quay Lại Sau`,
                        text: data.message,
                        timer: 5000,
                        showConfirmButton: false,
                    }).then(() => {
                        $("#Ma_Loai_KeKhai").select2("val", "12");
                    })
                }
            })
            .fail(function (data) {
                Swal.fire({
                    icon: 'error',
                    title: 'Không thành công',
                    text: `Hệ thống bị lỗi, không thể lấy dữ liệu`,
                    timer: 2000,
                    showConfirmButton: false,

                })
            });
    }

}
$("#THEM").click(() => {
    checkloaikekhai()
})

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
            "url": "/NV_LapKeHoachKeKhai/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [8] },
        ],
        "columns": [
            { "data": "MaKeHoachKeKhai" },
            { "data": "Ten" },
            {
                "data": "TenKeHoachKeKhai", "render": function (data, type, row, meta) {
                    return `<b>${data}</b>`
                }
            },
            {
                "data": { "NguoiTao": "NguoiTao","Ten":"Ten" }, "render": function (data, type, row, meta) {
                    if (data.NguoiTao == "NDDTTT") {
                        return "Kế hoạch kê khai Tỉnh"
                    } else {
                        return ""
                    }
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
                    else{
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
                "data": { "MaKeHoachKeKhai":"MaKeHoachKeKhai","NghiDinh":"NghiDinh"}, "render": function (data, type, row, meta) {

                    var row_fildinhkem = ``
                    var row_xemchitiet = ``

                    if (XEMCHITIET) {
                        row_xemchitiet = ` <a class="dropdown-item" data-model-id="${data.MaKeHoachKeKhai}" data-toggle="modal" data-target="#add-sh1" onclick="viewdetail(this)" >
                                                Xem chi tiết
                                            </a>`
                    }
                    if (FILEDINHKEM) {
                        row_fildinhkem = `<a class="dropdown-item" target="_blank" href="/Content/Uploads/${data.NghiDinh}" target="_blank">
                                        File Đính Kèm
                                    </a>`
                    }
                            
                    if (row_fildinhkem != `` || row_xemchitiet != ``) {
                        return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_fildinhkem}${row_xemchitiet}
                                      </div>`
                    }
                    else return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_fildinhkem}${row_xemchitiet}
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

    if (!THEM) {
        $('#THEM').remove()
    }
    if (!XUAT) {
        dt.buttons().disable();
    }

    //Kết thúc chỉnh sửa  23-2-2022
}

function FnBegin() {
    $('.loading_lkh').removeClass('d-none');
}

function FnSuccess(data) {
    $('.loading_lkh').addClass('d-none');
   

    if (data.status) {
        $('.closeform').click();
        Swal.fire({
            icon: 'success',
            title: 'Thành Công',
            text: data.message,
            timer: 2000,
            showConfirmButton: false,
        })
        dt.draw();
    }
    else {
        Swal.fire({
            icon: 'warning',
            title: 'Kế Hoạch Đã Được Lập',
            text: data.message,
            timer: 2000,
            showConfirmButton: false,
        })
    }
   
}

function Failure(data) {
    $('.loading_lkh').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Thêm mới kế hoạch kê khai tài sản không thành công',
        timer: 2000,
        showConfirmButton: false,
    })
}


async function CheckQuyen() {
    await $.get("/Home/GetQuyen", { MenuCode: "NV_LapKeHoachKeKhai" }, (data) => {
        if (data.includes("XEM")) {
            XEM = true;
        }
        if (data.includes("THEM")) {
            THEM = true;
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
    })
}

$(document).ready(async function () {

    await CheckQuyen();

    loadDataTable();
    $("#LoaiKeKhai").select2()
    $("#MaCoQuanDonVi_search").select2()
    $("#MaCoQuanDonVi_search").change(() => {
        dt.columns(1).search($('#LoaiKeKhai').val());
        dt.columns(2).search($('#keHoachNam').val());
        dt.columns(0).search($('#Filter').val());
        dt.columns(3).search($('#MaCoQuanDonVi_search').val());
        dt.draw()
    })



    $("#LoaiKeKhai").change(() => {
        dt.columns(1).search($('#LoaiKeKhai').val());
        dt.columns(2).search($('#keHoachNam').val());
        dt.columns(0).search($('#Filter').val());
        dt.columns(3).search($('#MaCoQuanDonVi_search').val());
        dt.draw()
    })
    $("#keHoachNam").change(() => {
        dt.columns(1).search($('#LoaiKeKhai').val());
        dt.columns(2).search($('#keHoachNam').val());
        dt.columns(0).search($('#Filter').val());
        dt.columns(3).search($('#MaCoQuanDonVi_search').val());
        dt.draw()
    })

  
    $("#Ma_Loai_KeKhai").select2().on('change', function () {
        $("#KKTT").empty()

        min = `${year-5}-01-01`
        max = `${year+5}-12-31`

        $('#ThoiGianBatDau').attr("min", min);
        $('#ThoiGianKetThuc').attr("max", max);

        $('#ThoiGianKetThuc').attr("min", min);
        $('#ThoiGianBatDau').attr("max", max);

        $('#ThoiGianBatDau').val('');
        $('#ThoiGianKetThuc').val('');
        if ($(this).val() == 4) {
            $.post("/NV_LapKeHoachKeKhai/CheckLoaiKeHoachKeKhai", { KeHoachNam: $("#KeHoachNam").val(), LoaiKeKhai: $(this).val() })
                .done(function (data) {
                    if (data.type == "warning") {
                        min = data.thoigianbatdau;
                        max = data.thoigianketthuc;

                        $('#ThoiGianBatDau').attr("min", min);
                        $('#ThoiGianKetThuc').attr("max", max);
                        $('#ThoiGianKetThuc').attr("min", min);
                        $('#ThoiGianBatDau').attr("max", max);


                        $('#ThoiGianBatDau').val(min);
                        $('#ThoiGianKetThuc').val(max);


                        var row = ` <div class="col-12 d-flex">
                                            <hr style="width: 20%" />
                                            <span style="font-size: 17px;">Kế Hoạch Kê Khai Hằng Năm của Tỉnh Năm ${$("#KeHoachNam").val()}</span>
                                            <hr style="width: 20%" />
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Bắt Đầu Kê khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianBatDauKKTT" name="ThoiGianBatDauKKTT" value=${data.thoigianbatdau}>
                                            </div>
                                        </div>

                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Kết Thúc Kê Khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianKetThucKKTT" name="ThoiGianKetThucKKTT" value=${data.thoigianketthuc}>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="form-group">
                                                <label>Đính Kèm Tệp<span style="color: red"> *</span></label>
                                                <span><a href ="/Content/uploads/${data.file}" target="_blank">${data.file}<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-earmark-arrow-down-fill" viewBox="0 0 16 16">
  <path d="M9.293 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V4.707A1 1 0 0 0 13.707 4L10 .293A1 1 0 0 0 9.293 0zM9.5 3.5v-2l3 3h-2a1 1 0 0 1-1-1zm-1 4v3.793l1.146-1.147a.5.5 0 0 1 .708.708l-2 2a.5.5 0 0 1-.708 0l-2-2a.5.5 0 0 1 .708-.708L7.5 11.293V7.5a.5.5 0 0 1 1 0z"/>
</svg></a></span>
                                            </div>
                                        </div>`
                        $("#KKTT").append(row)

                        Swal.fire({
                            icon: 'warning',
                            title: `Thông Báo`,
                            text: data.message,
                            timer: 5000,
                            showConfirmButton: false,
                        })
                    }
                    else if (data.type == "error") {
                        Swal.fire({
                            icon: 'warning',
                            title: `Quay Lại Sau`,
                            text: data.message,
                            timer: 5000,
                            showConfirmButton: false,
                        }).then(() => {
                            $("#Ma_Loai_KeKhai").select2("val", "3");
                        })
                    }
                })
                .fail(function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `Hệ thống bị lỗi, không thể lấy dữ liệu`,
                        timer: 2000,
                        showConfirmButton: false,

                    })
                });
        }
        else if ($(this).val() == 3) {
            $.post("/NV_LapKeHoachKeKhai/CheckLoaiKeHoachKeKhai", { KeHoachNam: $("#KeHoachNam").val(), LoaiKeKhai: $(this).val() })
                .done(function (data) {
                    if (data.type == "warning") {
                        var row = ` <div class="col-12 d-flex">
                                            <hr style="width: 20%" />
                                            <span style="font-size: 17px;">Kế Hoạch Kê Khai Lần Đầu của Tỉnh Năm 2021</span>
                                            <hr style="width: 20%" />
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Bắt Đầu Kê khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianBatDauKKTT" name="ThoiGianBatDauKKTT" value=${data.thoigianbatdau}>
                                            </div>
                                        </div>

                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Kết Thúc Kê Khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianKetThucKKTT" name="ThoiGianKetThucKKTT" value=${data.thoigianketthuc}>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="form-group">
                                                <label>Đính Kèm Tệp<span style="color: red"> *</span></label>
                                                <span><a href ="/Content/uploads/${data.file}" target="_blank">${data.file}<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-earmark-arrow-down-fill" viewBox="0 0 16 16">
  <path d="M9.293 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V4.707A1 1 0 0 0 13.707 4L10 .293A1 1 0 0 0 9.293 0zM9.5 3.5v-2l3 3h-2a1 1 0 0 1-1-1zm-1 4v3.793l1.146-1.147a.5.5 0 0 1 .708.708l-2 2a.5.5 0 0 1-.708 0l-2-2a.5.5 0 0 1 .708-.708L7.5 11.293V7.5a.5.5 0 0 1 1 0z"/>
</svg></a></span>
                                            </div>
                                        </div>`
                        $("#KKTT").append(row)

                        Swal.fire({
                            icon: 'warning',
                            title: `Thông Báo`,
                            text: data.message,
                            timer: 5000,
                            showConfirmButton: false,
                        })
                    }
                    else if (data.type == "error") {
                        Swal.fire({
                            icon: 'warning',
                            title: `Quay Lại Sau`,
                            text: data.message,
                            timer: 5000,
                            showConfirmButton: false,
                        }).then(() => {
                            $("#Ma_Loai_KeKhai").select2("val", "12");
                        })
                    }
                })
                .fail(function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `Hệ thống bị lỗi, không thể lấy dữ liệu`,
                        timer: 2000,
                        showConfirmButton: false,

                    })
                });
        }
    });
    $("#KeHoachNam").change(() => {
        $("#KKTT").empty()

        min = `${year-5}-01-01`
        max = `${year+5}-12-31`

        //$('#ThoiGianBatDau').attr("min", min);
        //$('#ThoiGianKetThuc').attr("max", max);

        //$('#ThoiGianKetThuc').attr("min", min);
        //$('#ThoiGianBatDau').attr("max", max);

        $('#ThoiGianBatDau').val('');
        $('#ThoiGianKetThuc').val('');
        if ($("#Ma_Loai_KeKhai").val() == 4) {
            $.post("/NV_LapKeHoachKeKhai/CheckLoaiKeHoachKeKhai", { KeHoachNam: $("#KeHoachNam").val(), LoaiKeKhai: 4 })
                .done(function (data) {
                    if (data.type == "warning") {
                        min = data.thoigianbatdau;
                        max = data.thoigianketthuc;
                        $('#ThoiGianBatDau').val(min);
                        $('#ThoiGianKetThuc').val(max);

                        $('#ThoiGianBatDau').attr("min", min);
                        $('#ThoiGianKetThuc').attr("max", max);

                        $('#ThoiGianKetThuc').attr("min", min);
                        $('#ThoiGianBatDau').attr("max", max);

                        var row = ` <div class="col-12 d-flex">
                                            <hr style="width: 20%" />
                                            <span style="font-size: 17px;">Kế Hoạch Kê Khai Hằng Năm Của Tỉnh Năm ${$("#KeHoachNam").val()}</span>
                                            <hr style="width: 20%" />
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Bắt Đầu Kê khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianBatDauKKTT" name="ThoiGianBatDauKKTT" value=${data.thoigianbatdau}>
                                            </div>
                                        </div>

                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Kết Thúc Kê Khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianKetThucKKTT" name="ThoiGianKetThucKKTT" value=${data.thoigianketthuc}>
                                            </div>
                                        </div>
                                            <div class="col-12">
                                            <div class="form-group">
                                                <label>Đính Kèm Tệp<span style="color: red"> *</span></label>
                                                <span><a href ="/Content/uploads/${data.file}" target="_blank">${data.file}<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-earmark-arrow-down-fill" viewBox="0 0 16 16">
  <path d="M9.293 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V4.707A1 1 0 0 0 13.707 4L10 .293A1 1 0 0 0 9.293 0zM9.5 3.5v-2l3 3h-2a1 1 0 0 1-1-1zm-1 4v3.793l1.146-1.147a.5.5 0 0 1 .708.708l-2 2a.5.5 0 0 1-.708 0l-2-2a.5.5 0 0 1 .708-.708L7.5 11.293V7.5a.5.5 0 0 1 1 0z"/>
</svg></a></span>
                                                <input class="form-control" type="file" id=" NghiDinh" name="NghiDinh" style="height: auto; ">
                                            </div>
                                        </div>`
                        $("#KKTT").append(row)

                        Swal.fire({
                            icon: 'warning',
                            title: `Thông Báo`,
                            text: data.message,
                            timer: 5000,
                            showConfirmButton: false,
                        })
                    }
                    else if (data.type == "error") {
                        Swal.fire({
                            icon: 'warning',
                            title: `Quay Lại Sau`,
                            text: data.message,
                            timer: 5000,
                            showConfirmButton: false,
                        }).then(() => {
                            $("#Ma_Loai_KeKhai").select2("val", "3");
                        })
                    }
                })
                .fail(function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `Hệ thống bị lỗi, không thể lấy dữ liệu`,
                        timer: 2000,
                        showConfirmButton: false,

                    })
                });
        }
    })

    $.validator.addMethod("minDate", function (value, element) {
        var inputDate = new Date(value);
        var curDate = new Date(min);
        if (inputDate > curDate)
            return true;
        return false;
    }, `Thời gian bắt đầu kê khai được nhỏ hơn ${moment(min).format("DD/MM/YYYY")}`);  

    $.validator.addMethod("maxDate", function (value, element) {
        var curDate = new Date();
        var inputDate = new Date(max);
        if (inputDate < curDate)
            return true;
        return false;
    }, `Thời gian kết thúc kê khai không được lớn hơn ${moment(max).format("DD/MM/YYYY")}`); 


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
            TenKeHoachKeKhai: {
                required: true,
            },
            KeHoachNam: {
                required: true,
            },
            ThoiGianBatDau: {
                required: true,
            },
            ThoiGianKetThuc: {
                required: true,
            },
            ThoiGianBatDauCongKhai: {
                required: true,
            },
            ThoiGianKetThucCongKhai: {
                required: true,
            },
            NghiDinh: {
                required: true
            }
        },
        messages: {
            TenKeHoachKeKhai: {
                required: "Tên kế hoạch kê khai không được bỏ trống",
            },
            KeHoachNam: {
                required: "Năm kế hoạch không được để trống",
            },
            ThoiGianBatDau: {
                required: "Thời gian bắt đầu không được để trống",
                min: "Thời gian bắt đầu kê khai không hợp lệ",
                max: "Thời gian bắt đầu kê khai không hợp lệ"
            },
            ThoiGianKetThuc: {
                required: "Thời gian kết thúc không được để trống",
                min: "Thời gian kết thúc kê khai không hợp lệ",
                max: "Thời gian kết thúc kê khai không hợp lệ"
            },
            ThoiGianBatDauCongKhai: {
                required: "Thời gian bắt đầu công khai không được để trống",
                
                
            },
            ThoiGianKetThucCongKhai: {
                required: "Thời gian kết thúc công khai không được để trống",
            },
            NghiDinh: {
                required: "File đính kèm không được để trống"
            }

        }
    })


    

    $("#search_btn").click(() => {
        dt.columns(1).search($('#LoaiKeKhai').val());
        dt.columns(2).search($('#keHoachNam').val());
        dt.columns(0).search($('#Filter').val());
        dt.draw()
    })
    $("#Filter").keypress(function (e) {
        if (e.keyCode == 13) {
            dt.columns(0).search($('#Filter').val());
            dt.draw();
        }
    });
})

function viewdetail(obj){
    var ele = $(obj);
    var MaKeHoach = ele.data("model-id");
    var url = `/NV_LapKeHoachKeKhai/LoadDataDetail`
   
    $.get(url, { id: MaKeHoach }, (data) => {
        
        var ThoiGianBatDau = moment(data.ThoiGianBatDau).format('YYYY-MM-DD') ;
        var ThoiGianKetThuc = moment(data.ThoiGianKetThuc).format('YYYY-MM-DD');
        var ThoiGianBatDauCongKhai = moment(data.ThoiGianBatDauCongKhai).format('YYYY-MM-DD');
        var ThoiGianKetThucCongKhai = moment(data.ThoiGianKetThucCongKhai).format('YYYY-MM-DD');

        $('#TenKeHoachKeKhai_Detail').val(data.TenKeHoachKeKhai)
        $('#KeHoachNam_Detail').val(data.KeHoachNam)
        $('#ThoiGianBatDau_Detail').val(ThoiGianBatDau)
        $('#ThoiGianKetThuc_Detail').val(ThoiGianKetThuc)
        $('#ThoiGianBatDauCongKhai_Detail').val(ThoiGianBatDauCongKhai)
        $('#ThoiGianKetThucCongKhai_Detail').val(ThoiGianKetThucCongKhai)
        $('#Ma_Loai_KeKhai_Detail').val(data.Ma_Loai_KeKhai)
        $('#NghiDinh_Detail').empty();
        if (FILEDINHKEM) {
            $('#NghiDinh_Detail').append(`<a href="Content/uploads/${data.NghiDinh}" target="_blank">${data.NghiDinh}<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-earmark-arrow-down-fill" viewBox="0 0 16 16">
  <path d="M9.293 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V4.707A1 1 0 0 0 13.707 4L10 .293A1 1 0 0 0 9.293 0zM9.5 3.5v-2l3 3h-2a1 1 0 0 1-1-1zm-1 4v3.793l1.146-1.147a.5.5 0 0 1 .708.708l-2 2a.5.5 0 0 1-.708 0l-2-2a.5.5 0 0 1 .708-.708L7.5 11.293V7.5a.5.5 0 0 1 1 0z"/>
</svg></a>`)
        }
        var kehoachnam = data.KeHoachNam
        var nguoitao = data.nguoitao
        $("#KKTT_Detail").empty()
        if ((data.Ma_Loai_KeKhai == 4 || data.Ma_Loai_KeKhai == 5) && nguoitao != "NDDTTT") {
            $.post("/NV_LapKeHoachKeKhai/CheckLoaiKeHoachKeKhai", { KeHoachNam: data.KeHoachNam, LoaiKeKhai: 4 })
                .done(function (data) {
                    if (data.type == "warning") {
                        min = data.thoigianbatdau;
                        max = data.thoigianketthuc;

                        $('#ThoiGianBatDau').attr("min", min);
                        $('#ThoiGianKetThuc').attr("max", max);
                        $('#ThoiGianKetThuc').attr("min", min);
                        $('#ThoiGianBatDau').attr("max", max);



                        $('#ThoiGianBatDau').val(min);
                        $('#ThoiGianKetThuc').val(max);


                        var row = ` <div class="col-12 d-flex">
                                            <hr style="width: 20%" />
                                            <span style="font-size: 17px;">Kế Hoạch Kê Khai Hằng Năm Của Tỉnh Năm ${kehoachnam}</span>
                                            <hr style="width: 20%" />
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Bắt Đầu Kê khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianBatDauKKTT" name="ThoiGianBatDauKKTT" value=${data.thoigianbatdau}>
                                            </div>
                                        </div>

                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Kết Thúc Kê Khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianKetThucKKTT" name="ThoiGianKetThucKKTT" value=${data.thoigianketthuc}>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="form-group">
                                                <label>Đính Kèm Tệp<span style="color: red"> *</span></label>
                                                <span><a href ="/Content/uploads/${data.file}" target="_blank">${data.file}<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-earmark-arrow-down-fill" viewBox="0 0 16 16">
  <path d="M9.293 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V4.707A1 1 0 0 0 13.707 4L10 .293A1 1 0 0 0 9.293 0zM9.5 3.5v-2l3 3h-2a1 1 0 0 1-1-1zm-1 4v3.793l1.146-1.147a.5.5 0 0 1 .708.708l-2 2a.5.5 0 0 1-.708 0l-2-2a.5.5 0 0 1 .708-.708L7.5 11.293V7.5a.5.5 0 0 1 1 0z"/>
</svg></a></span>
                                            </div>
                                        </div>`
                        $("#KKTT_Detail").append(row)
                    } else {

                    }
                })
        } else if (nguoitao != "NDDTTT") {
            $.post("/NV_LapKeHoachKeKhai/CheckLoaiKeHoachKeKhai", { KeHoachNam: data.KeHoachNam, LoaiKeKhai: data.Ma_Loai_KeKhai })
                .done(function (data) {
                    console.log(data)
                    if (data.type == "warning") {
                        min = data.thoigianbatdau;
                        max = data.thoigianketthuc;

                        $('#ThoiGianBatDau').attr("min", min);
                        $('#ThoiGianKetThuc').attr("max", max);
                        $('#ThoiGianKetThuc').attr("min", min);
                        $('#ThoiGianBatDau').attr("max", max);



                        $('#ThoiGianBatDau').val(min);
                        $('#ThoiGianKetThuc').val(max);


                        var row = ` <div class="col-12 d-flex">
                                            <hr style="width: 20%" />
                                            <span style="font-size: 17px;">Kế Hoạch Kê Khai Lần Đầu của Tỉnh Năm 2021</span>
                                            <hr style="width: 20%" />
                                        </div>
                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Bắt Đầu Kê khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianBatDauKKTT" name="ThoiGianBatDauKKTT" value=${data.thoigianbatdau}>
                                            </div>
                                        </div>

                                        <div class="col-6">
                                            <div class="form-group">
                                                <label>Thời Gian Kết Thúc Kê Khai<span style="color: red"> *</span></label>
                                                <input type="date" readonly class="form-control" id="ThoiGianKetThucKKTT" name="ThoiGianKetThucKKTT" value=${data.thoigianketthuc}>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="form-group">
                                                <label>Đính Kèm Tệp<span style="color: red"> *</span></label>
                                                <span><a href ="/Content/uploads/${data.file}" target="_blank">${data.file}<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-earmark-arrow-down-fill" viewBox="0 0 16 16">
  <path d="M9.293 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V4.707A1 1 0 0 0 13.707 4L10 .293A1 1 0 0 0 9.293 0zM9.5 3.5v-2l3 3h-2a1 1 0 0 1-1-1zm-1 4v3.793l1.146-1.147a.5.5 0 0 1 .708.708l-2 2a.5.5 0 0 1-.708 0l-2-2a.5.5 0 0 1 .708-.708L7.5 11.293V7.5a.5.5 0 0 1 1 0z"/>
</svg></a></span>
                                            </div>
                                        </div>`
                        $("#KKTT_Detail").append(row)
                    } else {

                    }
                })
        }
    })
}