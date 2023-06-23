var t;
var url = window.location.href.split('/')
var id = url[url.length - 1]

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
            "url": `/NV_KeKhai_TSTN/LoadDataDetailCanBo/${id}`,
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [9] }
        ],
        "columns": [
            { "data": "MaKeHoachKeKhai" },
            { "data": "TenKeHoachKeKhai" },
            { "data": "HoTen" },
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
                "data": { "FileDinhKem": "FileDinhKem", "Ma_KeKhai": "Ma_KeKhai", "MaKeHoachKeKhai": "MaKeHoachKeKhai", "TrangThaiKK": "TrangThaiKK", "ThoiGianKetThuc": "ThoiGianKetThuc", "suakk": "suakk" }, "render": (data, type, row) => {
                    if (data.ThoiGianKetThuc.replace('/Date(', '').replace(')/', '') < Date.now()) {
                        return `<span class="badge badge-danger">Đã hết thời gian kê khai</span>`
                    }
                    else {

                        if (data.TrangThaiKK == true && data.completekk == true) {
                            return `<span class="badge badge-success">Hoàn thành bản kê khai</span>`
                        }
                        else if (data.TrangThaiKK == true && data.completekk == false) {

                            return `<span class="badge badge-warning">Đang tiến Hành</span>`
                        }
                        else {
                            return `<span class="badge badge-danger">Chưa kê khai</span>`
                        }
                    }

                }
            },
            {
                "data": { "FileDinhKem": "FileDinhKem", "Ma_KeKhai": "Ma_KeKhai", "MaKeHoachKeKhai": "MaKeHoachKeKhai", "TrangThaiKK": "TrangThaiKK", "ThoiGianKetThuc": "ThoiGianKetThuc" }, "render": (data, type, row) => {

                    if (data.ThoiGianKetThuc.replace('/Date(', '').replace(')/', '') < Date.now()) {

                        return `<div class="dropleft">
                              <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                <i class="fa-solid fa-ellipsis-vertical"></i>
                              </button>
                              <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                <a class="dropdown-item" href="#">
                                    Đã Hết Hạn Kê Khai
                                </a>    
                              </div>
                            </div>`
                    }
                    else {
                        if (data.TrangThaiKK == true && data.completekk == true) {

                            var row_bankekhai = ``


                            row_bankekhai = `<a class="dropdown-item" target="_blank" href ="/Content/uploads/${data.FileDinhKem}">
                                                    Tải Bản Kê Khai
                                                </a>`

                            return `<div class="dropleft">
                                          <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                            <i class="fa-solid fa-ellipsis-vertical"></i>
                                          </button>
                                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                            ${row_bankekhai}
                                          </div>
                                        </div>`

                        } else {
                            return `<div class="dropleft">
                                          <button class="btn btn-outline-info btn-sm" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                            <i class="fa-solid fa-ellipsis-vertical"></i>
                                          </button>
                                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                            <a class="dropdown-item" target="_blank" href ="/Content/uploads/${data.FileDinhKem}">
                                                    Tải Bản Kê Khai
                                                </a>
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



$(document).ready( function () {

    loadDataTable();

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

