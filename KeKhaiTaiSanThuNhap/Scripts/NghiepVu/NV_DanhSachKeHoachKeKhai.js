var t;

var XEM = false;
var XUAT = false;
var SUA = false;
var THEM = false;

function FnBegin_DinhKem() {
    $('.loading_dk').removeClass('d-none');
    $('#submit_dinhkem').prop('disabled',true);
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
            text: data.message,
            timer: 2000,
            showConfirmButton: false,
        })
        t.draw();
    }
    else {
        Swal.fire({
            icon: data.status,
            title: 'Cảnh Báo',
            text: data.message,
            timer: 2000,
            showConfirmButton: false,
        })
        t.draw();
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


function viewdetail(obj) {
    var ele = $(obj);
    var MaKeHoach = ele.data("model-id");
    var url = `/NV_LapKeHoachKeKhai/LoadDataDetail`
    console.log("detail")
    $.get(url, { id: MaKeHoach }, (data) => {
        var ThoiGianBatDau = moment(data.ThoiGianBatDau).format('YYYY-MM-DD');
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
        $('#NghiDinhTTT_Detail').empty();

        $('#NghiDinh_Detail').append(`<a href="Content/uploads/${data.NghiDinh}" target="_blank">${data.NghiDinh}</a>`)
        $('#NghiDinhTTT_Detail').append(`<a href="Content/uploads/${data.NghiDinhTTT}" target="_blank">${data.NghiDinhTTT}</a>`)
        var kehoachnam = data.KeHoachNam
        $("#KKTT").empty()
        if (data.Ma_Loai_KeKhai == 4 || data.Ma_Loai_KeKhai == 5) {
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
                                                <span><a href ="/Content/uploads/${data.file}" target="_blank">${data.file}</a></span>
                                            </div>
                                        </div>`
                        $("#KKTT").append(row)
                    } else {

                    }
                })
        } else if (data.NguoiTao != "NDDTTT") {
            $.post("/NV_LapKeHoachKeKhai/CheckLoaiKeHoachKeKhai", { KeHoachNam: data.KeHoachNam, LoaiKeKhai: data.Ma_Loai_KeKhai })
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
                    } else {

                    }
                })
        }
    })
}

function loadDataTable() {
    t = $("#dataTable").DataTable({
        "lengthChange": false,
        "info": false,
        "ordering": false,
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
            "url": `/NV_KeKhai_TSTN/LoadDataKeHoachKeKhai`,
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [8] }
        ],
        "columns": [
            { "data": "MaKeHoachKeKhai" },
            { "data": "TenKeHoachKeKhai" },
            { "data": "loaiKK" },
            {
                "data": "ThoiGianBatDau", "render": (data, type, row) => {
                    Number.prototype.padLeft = function (base, chr) {
                        var len = (String(base || 10).length - String(this).length) + 1;
                        return len > 0 ? new Array(len).join(chr || '0') + this : this;
                    }

                    var datemili = data.toString();
                    var mili = datemili.substring(6, 19);
                    var d = new Date(parseInt(mili));
                    var dformat = [d.getDate().padLeft(), (d.getMonth() + 1).padLeft(),
                        d.getFullYear()].join('/');
                    return dformat;
                }
            },
            {
                "data": "ThoiGianKetThuc", "render": (data, type, row) => {
                    Number.prototype.padLeft = function (base, chr) {
                        var len = (String(base || 10).length - String(this).length) + 1;
                        return len > 0 ? new Array(len).join(chr || '0') + this : this;
                    }

                    var datemili = data.toString();
                    var mili = datemili.substring(6, 19);
                    var d = new Date(parseInt(mili));
                    var dformat = [d.getDate().padLeft(), (d.getMonth() + 1).padLeft(),
                    d.getFullYear()].join('/');
                    return dformat;
                }
            },
            { "data": "Ten" },
            { "data": "KeHoachNam" },
           
            {
                "data": { "FileDinhKem":"FileDinhKem","Ma_KeKhai": "Ma_KeKhai", "MaKeHoachKeKhai": "MaKeHoachKeKhai", "TrangThaiKK": "TrangThaiKK", "ThoiGianKetThuc": "ThoiGianKetThuc", "suakk": "suakk" }, "render": (data, type, row) => {
                    
                        if (data.TrangThaiKK == true && data.completekk == true) {
                            return `<span class="badge badge-success">Hoàn thành bản kê khai</span>`
                        }
                        else if (data.TrangThaiKK == true && data.completekk == false) {
                            if (data.ThoiGianKetThuc.replace('/Date(', '').replace(')/', '') < Date.now()) {
                                return `<span class="badge badge-danger">Đã hết thời gian kê khai</span>`
                            }
                            else {
                                return `<span class="badge badge-warning">Đang tiến Hành</span>`
                            }
                        }   
                        else {
                            if (data.ThoiGianKetThuc.replace('/Date(', '').replace(')/', '') < Date.now()) {
                                return `<span class="badge badge-danger">Đã hết thời gian kê khai</span>`
                            }
                            else {

                                return `<span class="badge badge-danger">Chưa kê khai</span>`
                            }
                        }
                    
                  
                }
            },
            {
                "data": { "FileDinhKem": "FileDinhKem", "Ma_KeKhai": "Ma_KeKhai", "MaLoaiKeKhai":"MaLoaiKeKhai", "MaKeHoachKeKhai": "MaKeHoachKeKhai", "TrangThaiKK": "TrangThaiKK", "ThoiGianKetThuc": "ThoiGianKetThuc", "KeHoachNam": "KeHoachNam" }, "render": (data, type, row) => {
                  
                  
                    if (data.TrangThaiKK == true && data.completekk == true) {

                        var row_bankekhai = ``

                        if (THEM) {
                            row_bankekhai = `<a class="dropdown-item" target="_blank" href ="/Content/uploads/${data.FileDinhKem}">
                                                Tải Bản Kê Khai
                                            </a>
                                            `
                        }
                        if (row_bankekhai != ``) {
                            return `<div class="dropleft">
                                        <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                        </button>
                                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <a class="dropdown-item" data-model-id="${data.MaKeHoachKeKhai}" data-toggle="modal" data-target="#xemchitiet-sh1" onclick="viewdetail(this)" >
                                            Xem Kế Hoạch Kê Khai
                                        </a>
                                        ${row_bankekhai}
                                        </div>
                                    </div>`
                        }
                        else return `<div class="dropleft">
                                        <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                        </button>
                                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        ${row_bankekhai}
                                        </div>
                                    </div>`
                           
                    }
                    else if (data.TrangThaiKK == true && data.completekk == false) {
                        if (data.ThoiGianKetThuc.replace('/Date(', '').replace(')/', '') < Date.now() && data.MaLoaiKeKhai != 3 && data.KeHoachNam > 2023) {

                            return `<div class="dropleft">
                                        <button class="btn btn-outline-info btn-sm" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                        </button>
                                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <a class="dropdown-item" href="#">
                                            Đã Hết Hạn Kê Khai
                                        </a>    
                                            <a class="dropdown-item" data-model-id="${data.MaKeHoachKeKhai}" data-toggle="modal" data-target="#add-sh1" onclick="viewdetail(this)" >
                                            Xem Kế Hoạch Kê Khai
                                        </a>
                                        </div>
                                    </div>`
                        }
                        else {
                            var row_sua = ``
                            if (SUA) {
                                row_sua = `
                                        <a class="dropdown-item" href="/ke-khai-tai-san/cap-nhat/${data.Ma_KeKhai}" >
                                            Cập Nhật Bản Kê Khai
                                        </a>
                                        `
                            }
                            return `<div class="dropleft">
                                        <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                        </button>
                                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <a class="dropdown-item" data-model-id="${data.MaKeHoachKeKhai}" data-toggle="modal" data-target="#xemchitiet-sh1" onclick="viewdetail(this)" >
                                            Xem Kế Hoạch Kê Khai
                                        </a>
                                            ${row_sua}
                                        <a class="dropdown-item" onclick="print_bankkCBkk(this)" data-model-id="${data.Ma_KeKhai}">
                                            Xem Trước Bản In
                                        </a> 
                                        <a class="dropdown-item" data-model-id="${data.Ma_KeKhai}" data-toggle="modal" data-target="#add-dk"  onclick="DinhKemFile(this)">
                                            Đính Kèm Bản Kê Khai Có Chữ Ký
                                        </a>  
                                        <a class="dropdown-item" onclick={completeKeKhai(${data.Ma_KeKhai})} >
                                            Hoàn Thành và Gửi Bản Kê Khai
                                        </a>
                                        </div>
                                    </div>`
                        }
                    }
                           
                    else {
                        if (data.ThoiGianKetThuc.replace('/Date(', '').replace(')/', '') < Date.now() && data.MaLoaiKeKhai != 3 && data.KeHoachNam > 2023) {

                            return `<div class="dropleft">
                                        <button class="btn btn-outline-info btn-sm" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                        </button>
                                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                        <a class="dropdown-item" href="#">
                                            Đã Hết Hạn Kê Khai
                                        </a>    
                                            <a class="dropdown-item" data-model-id="${data.MaKeHoachKeKhai}" data-toggle="modal" data-target="#add-sh1" onclick="viewdetail(this)" >
                                            Xem Kế Hoạch Kê Khai
                                        </a>
                                        </div>
                                    </div>`
                        }
                        else {
                            var row_them = ``

                            if (THEM) {
                                row_them = `<a class="dropdown-item" href="/ke-khai-tai-san/lap-ban-ke-khai/${data.MaKeHoachKeKhai}" >
                                        Lập Bản Kê Khai
                                        </a>`
                            }

                            if (row_them != ``) {
                                return `<div class="dropleft">
                                    <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                    <i class="fa-solid fa-ellipsis-vertical"></i>
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                    <a class="dropdown-item" data-model-id="${data.MaKeHoachKeKhai}" data-toggle="modal" data-target="#xemchitiet-sh1" onclick="viewdetail(this)" >
                                        Xem Kế Hoạch Kê Khai
                                    </a>
                                    ${row_them}
                                    </div>
                                </div>`
                            }
                            else return `<div class="dropleft">
                                            <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                            <i class="fa-solid fa-ellipsis-vertical"></i>
                                            </button>
                                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                            <a class="dropdown-item" data-model-id="${data.MaKeHoachKeKhai}" data-toggle="modal" data-target="#xemchitiet-sh1" onclick="viewdetail(this)" >
                                                Xem Kế Hoạch Kê Khai
                                            </a>
                                            ${row_them}
                                            </div>
                                        </div>`
                        }
                    }
                    
                    
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

    if (!XUAT) {
        t.buttons().disable();
    }
}


function print_bankkCBkk(obj) {
    var ele = $(obj);
    var MaKeKhai = ele.data("model-id");

    $.ajax({
        url: `/NV_KeKhai_TSTN/InBanKeKhaiCanBo?id=${MaKeKhai}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        beforeSend: function () {
            $(`#btn_download_bankekhai_${MaKeKhai}`).addClass("d-none")
            $(`#loading_btnPrint_bankkCBkk_${MaKeKhai}`).removeClass("d-none")
        },
        success: function (response) {
            $(`#btn_download_bankekhai_${MaKeKhai}`).removeClass("d-none")
            $(`#loading_btnPrint_bankkCBkk_${MaKeKhai}`).addClass("d-none")
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
                $(`#btn_download_bankekhai_${MaKeKhai}`).removeClass("d-none")
                $(`#loading_btnPrint_bankkCBkk_${MaKeKhai}`).addClass("d-none")
            })
        }
    });

}


function DinhKemFile(obj) {
    var ele = $(obj);
    var MaKeKhai = ele.data("model-id");
    $('#MaKeKhai').val(MaKeKhai);
    $('#DaDinhKem').empty();
    $.ajax({
        url: `/NV_KeKhai_TSTN/GetFileDinhKem/${MaKeKhai}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (response) {
            var row = `<div class="col-12">
                            <div class="form-group">
                                <label>Bản Kê Khai Đã Đính Kèm<span style="color: red"> *</span></label>
                                <span><a target="_blank" href ="/Content/uploads/${response}">${response}</a></span>
                            </div>
                        </div>`
            $('#DaDinhKem').append(row)
        }
    })
       


}

function completeKeKhai(MaKeHoachKeKhai) {
    swal.fire({
        title: 'Xác Nhận Hoàn Thành, Gửi Bản Kê Khai',
        text: "Vui Lòng Kiểm Tra Kỹ, Nếu Gửi Đi Sẽ Không Được Sửa Đổi.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Có, Hãy Gửi!',
        cancelButtonText: 'Không, Quay lại!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/NV_KeKhai_TSTN/completeKeKhai",
                data: { MaKeKhai: MaKeHoachKeKhai },
                dataType: "json",
                success: function (response) {
                    if (response.status == "success") {
                        Swal.fire({
                            icon: response.status,
                            title: 'Thành Công',
                            text: response.message,
                            timer: 2000,
                            showConfirmButton: false,
                        })
                        t.draw();
                    }
                    else if (response.status == "warning") {
                        Swal.fire({
                            icon: response.status,
                            title: 'Cảnh Báo',
                            text: response.message,
                            timer: 2000,
                            showConfirmButton: false,
                        })

                    } else {
                        Swal.fire({
                            icon: response.status,
                            title: 'Lỗi',
                            text: response.message,
                            timer: 2000,
                            showConfirmButton: false,
                        })
                    }
                },
                error: function (error) {
                    Swal.fire({
                        icon: "error",
                        title: 'Lỗi',
                        text: "Lỗi Hệ Thống",
                        timer: 2000,
                        showConfirmButton: false,
                    })
                }
            });
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {

        }
    })
}

async function CheckQuyen() {
    await $.get("/Home/GetQuyen", { MenuCode: "NV_KeKhaiTaiSan" }, (data) => {
        if (data.includes("XEM")) {
            XEM = true;
        }
        if (data.includes("THEM")) {
            THEM = true;
        }
        if (data.includes("XUAT")) {
            XUAT = true;
        }
        if (data.includes("SUA")) {
            SUA = true;
        }
    })
}

$(document).ready(async function () {
    await CheckQuyen();
    loadDataTable();


    $("#Xem_KeKhaiHangNam").click(() => {

        t.columns(1).search(4);
        t.columns(2).search(Date.now.year);
        t.draw()
    })

    $("#Xem_KeKhaiBoSung").click(() => {
        t.columns(1).search(5);
        t.columns(2).search(Date.now.year);
        t.draw()
    })

    $("#Xem_KeKhaiQuaHan").click(() => {
        t.columns(1).search(5);
        t.columns(2).search(Date.now.year);
        t.draw()
    })
    $("#search").click(() => {
        t.columns(0).search($('#Filter').val());
        t.draw()
    })
    $("#Filter").keypress(function (e) {
        if (e.keyCode == 13) {
            t.columns(0).search($('#Filter').val());
            t.draw();
        }
    });

})

