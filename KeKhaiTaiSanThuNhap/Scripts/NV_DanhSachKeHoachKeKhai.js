const params = new URLSearchParams(window.location.search)
var id = Object.fromEntries(params.entries());
var t;


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
                } },
            { "data": "KeHoachNam" },
            { "data": "Ten" },
            {
                "data": { "TrangThai": "TrangThai", "ThoiGianKetThuc": "ThoiGianKetThuc", "daKhoa": "daKhoa" }, "render": (data, type, row) => {
                    if (data.daKhoa == false) {
                        return `
                                <span class="badge badge-info">Đang lập danh sách</span>
                                `
                    }
                    if (data.TrangThai != true && data.ThoiGianKetThuc.replace('/Date(', '').replace(')/', '') > Date.now()) {
                        return `
                                <span class="badge badge-success">Đang tiến Hành</span>
                                `
                    } else {
                        return `
                                <span class="badge badge-danger">Đã hết thời gian kê khai</span>
                                `
                    }
                }
            },
            {
                "data": { "MaKeHoachKeKhai": "MaKeHoachKeKhai", "TrangThaiKK": "TrangThaiKK", "ThoiGianKetThuc": "ThoiGianKetThuc", "suakk": "suakk" }, "render": (data, type, row) => {
                    if (data.daKhoa == false) {
                        return ``
                    }



                    if (data.TrangThaiKK == true) {
                        return `<span class="badge badge-primary">Đã kê khai <i class="fa-solid fa-check"></i></span>`
                    }
                    if ( data.ThoiGianKetThuc.replace('/Date(', '').replace(')/', '') < Date.now()) {
                        return `<button disabled class="btn btn-info">Đã hết hạn</button>`
                    } else {

                        if (data.suakk == true && data.completekk == false) {

                            return `<a onclick={edit(${data.MaKeHoachKeKhai})} class="btn btn-info">Sửa kê khai</a>
                                    <button type="button"class="btn btn-success" onclick={completeKeKhai(${data.MaKeHoachKeKhai})}>Hoàn thành</button>
                                    `
                        } else if (data.suakk == false && data.completekk == false) {
                            return `<span class="badge badge-primary">Đã kê khai <i class="fa-solid fa-check"></i></span>`
                        } else if (data.suakk == true && data.completekk == true) {
                            return `<a href="/NV_KeKhai_TSTN/LapBanKeKhai?id=${data.MaKeHoachKeKhai}" class="btn btn-danger"> Kê khai</a>`
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

function edit(MaKeHoachKeKhai) {
    if (MaKeHoachKeKhai == null) {

    } else {
        $.post(`/NV_KeKhai_TSTN/getMaKeKhaiByKeHoachKeKhai?MaKeHoachKeKhai=${MaKeHoachKeKhai}`, (data) => {
            window.location.replace(`/NV_KeKhai_TSTN/edit/${data}`)
        })
    }
}

function completeKeKhai(MaKeHoachKeKhai) {
    console.log(MaKeHoachKeKhai);
    if (MaKeHoachKeKhai == null) {

    } else {
        $.ajax({
            type: "POST",
            url: "/NV_KeKhai_TSTN/completeKeKhai",
            data: { MaKeHoachKeKhai: MaKeHoachKeKhai},
            dataType: "json",
            success: function (response) {
                if (response == true) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Thành Công',
                        text: `Đã hàng thành bản kê khai`,
                        timer: 2000,
                        showConfirmButton: false,
                    })
                    t.draw();
                } else {
                    console.log("lỗi")
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
}

function Failure_Edit(data) {
    $('.loading_edittaikhoan').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi',
        text: `Sửa tài khoản không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}

function FnBegin() {
    $('.loading_newtaikhoan').removeClass('d-none');
}

function FnSuccess(data) {

    $('.loading_newtaikhoan').addClass('d-none');
    $('.closeform').click();

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Tài Khoản ${data} đã được thêm thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
    t.draw();
}

function Failure(data) {
    $('.loading_newdantoc').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Tên tài khoản đã tồn tại',
        text: data,
        timer: 2000,
        showConfirmButton: false,

    })
}

//Lấy dữ liệu phân quyền để kiểm tra
var GetAuth = (() => {


    //lấy Route, Kiểm tra route, kiểm tra chức năng, kiểm tra và Trạng thái
    var s1 = window.location.href.split("/");
    var getRoute = `/${s1[3]}`;

    $.post("/DM_CoQuanDonVi/GetAuth", { route: getRoute }, (data) => {

        $.each(data, (index, value) => {
            //Kiểm tra chức năng Xem
            if (value.TenChucNang == "Xem" && value.TrangThai == false) {
                //Redirect về trang khác
                window.location.replace("./");
            }
            //Kiểm tra chức năng thêm
            if (value.TenChucNang == "Thêm" && value.TrangThai == false) {
                //khóa chức năng thêm
                $(".input-group-append").remove()
            }
            //Kiểm tra chức năng sửa
            if (value.TenChucNang == "Xóa" && value.TrangThai == false) {
                //khóa chức năng Xóa
                $(".btn-delete").remove()
            }
            //Kiểm tra chức năng Xóa
            if (value.TenChucNang == "Sửa" && value.TrangThai == false) {
                //khóa chức năng Sửa
                $(".btn-edit").remove()
            }

        })
    })
})
var UserID = 0;


$.post("/NV_KeKhai_TSTN/getuser", (data) => {
    UserID = data;
})
$(document).ready(function () {

    loadDataTable();


    jQuery.validator.addMethod("NotAllowFirstSpace", function (value, element) {
        return this.optional(element) || /^\S{1}/.test(value);
    }, "Kí tự đầu tiên không được có khoảng trắng.");

    jQuery.validator.addMethod("NotAllowSpecial", function (value, element) {
        return this.optional(element) || /^[A-Za-z0-9_.]+$/.test(value);
    }, "Không được phép có kí tự đặc biệt.");



    $(".closeform").click(function () {

        $(':input', '.formTaiKhoan')
            .not(':button, :submit, :reset, :hidden')
            .val('')
            .prop('checked', false)
            .prop('selected', false)
            .removeClass('is-invalid')
            .removeClass('is-valid')
        form.resetForm()
    })
    GetAuth();
})

