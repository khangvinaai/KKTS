
var dt;
var SUA = false;
var XOA = false;
var XUAT = false;
var XEM = false;
var THEM = false;

function loadDataTable() {
    dt = $("#dataTable").DataTable({
        "lengthChange": false,
        "info": true,
        "searching": true,
        "language": {
            "search": "",
            "info": "Tổng số _TOTAL_ cơ quan",
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
                className: 'btn btn-outline-primary btn-sm mt-2 ml-3',
                exportOptions: {
                    columns: [1,2]
                }
            },
            {
                text: '<i class="fa fa-file-pdf"></i>',
                extend: 'pdf',
                className: 'btn btn-outline-primary btn-sm mt-2',
                exportOptions: {
                    columns: [1,2]
                }
            },
            {
                text: '<i class="fa fa-print"></i>',
                extend: 'print',
                className: 'btn btn-outline-primary btn-sm mt-2',
                exportOptions: {
                    columns: [1,2]
                }
            }
        ],

        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/DM_CoQuanDonVi/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [3] }
        ],
        "columns": [
            { "data": "Ten_Loai_CQDV" },
            { "data": "Ten_Loai_CQDV" },
            { "data": "Ten" },
            {
                "data": "Ma_CoQuan_DonVi", "render": function (data, type, row) {
    
                    var row_sua = ``
                    var row_xoa = ``
                    if (SUA) {
                        row_sua = `<span class="dropdown-item" onclick="Edit(this)" data-model-id="${data}" data-toggle="modal" data-target="#add-sh1">
                                            Cập Nhật
                                        </span>`
                    }
                    if (XOA) {
                        row_xoa = `<span class="dropdown-item" data-model-id="${data}" onclick="Delete(this)">
                                            Xóa Dữ Liệu
                                        </span>`
                    }

                    if (row_sua != `` || row_xoa != ``) {
                        return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_sua}${row_xoa}
                                      </div>`
                    }
                    else return `<div class="dropleft">
                                      <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                        <i class="fa-solid fa-ellipsis-vertical"></i>
                                      </button>
                                      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_sua}${row_xoa}
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
     
}

function FnBegin_Edit() {
    $('.loading_editcoquan').removeClass('d-none');
}

function FnSuccess_Edit(data) {
    $('.loading_editcoquan').addClass('d-none')
    $('.closeform').click()
    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Cập nhật danh mục cơ quan đơn vị`,
        timer: 2000,
        showConfirmButton: false,
    })
    dt.draw();
}

function Failure_Edit(data) {
    $('.loading_editcoquan').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi',
        text: `Cập nhật danh mục cơ quan đơn vị không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}

function FnBegin() {
    $('.loading_newcoquan').removeClass('d-none');
}

function FnSuccess(data) {
    $('.loading_newcoquan').addClass('d-none');
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
    $('.loading_newcoquan').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Thêm mới không thành công',
        timer: 2000,
        showConfirmButton: false,
    })
}

function Edit(obj) {
    var ele = $(obj);
    var Ma_CoQuan_DonVi = ele.data("model-id");
    var url = `/DM_CoQuanDonVi/GetSuaCoQuanDonVi/`
    $.get(url, { id: Ma_CoQuan_DonVi }, (data) => {
        $('#Ma_CoQuan_DonVi_Edt').val(data.Ma_CoQuan_DonVi)
        $('#Ten_Edt').val(data.Ten)
        $('#MaLoai_CoQuan_DonVi_Edt').val(data.MaLoai_CoQuan_DonVi)
        $('#MaLoai_CoQuan_DonVi_Edt').change()
    })
};

function Delete(obj) {
    var ele = $(obj);
    var Ma_CoQuan_DonVi = ele.data("model-id");
    var url = `/DM_CoQuanDonVi/delete`
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
            $.post(url, { id: Ma_CoQuan_DonVi })
                .done(function (data) {

                    if (data != false) {
                        dt.rows(`#row_${data.Ma_CoQuan_DonVi}`)
                            .remove()
                            .draw();
                        Swal.fire({
                            icon: 'success',
                            title: 'Thành Công',
                            text: `Cơ quan đơn vị đã được xóa`,
                            timer: 2000,
                            showConfirmButton: false,
                        })
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Không thành công',
                            text: `Xảy ra lỗi`,
                            timer: 2000,
                            showConfirmButton: false,

                        })
                    }

                })
                .fail(function (data) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Không thành công',
                        text: `Xảy ra lỗi`,
                        timer: 2000,
                        showConfirmButton: false,
                    })
                });
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {

        }
    })

};

async function CheckQuyen() {
    await $.get("/Home/GetQuyen", { MenuCode: "DM_CoQuan" }, (data) => {
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

        
    })
}

$(document).ready(async function () {

    await CheckQuyen();

    loadDataTable();

    $.get("/DM_Loai_CoQuan_DonVi/GetLoaiCoQuanDonVi", (data) => {
        var row = `<option value="">Chọn Loại Cơ Quan - Đơn Vị</option>`
        $.each(data, (index, value) => {
            row = row + `<option value ="${value.MaLoaiCoQuan}">${value.TenLoaiCoQuan}</option>`
        })
        $("#MaLoai_CoQuan_DonVi").append(row)
        $("#MaLoai_CoQuan_DonVi_Edt").append(row)
    })

    $("#MaLoai_CoQuan_DonVi").select2()
    $("#MaLoai_CoQuan_DonVi_Edt").select2()

    $("#search_btn").click(() => {
        var searchValue = $('#Filter').val().trim()
        dt.column(1).search(searchValue);
        dt.draw()
    })

    $("#Filter").keypress(function (e) {
        if (e.keyCode == 13) {
            dt.columns(1).search($("#Filter").val());
            dt.draw();
        }
    });

})


