var dt;
var FILEDINHKEM = false;
var XUAT = false;
var XEM = false;
var THEM = false;


var getRole = "CBKKTSTN";
var getCoQuan = "";
$.get("/HT_NhomTaiKhoan/GetRole/", (data1) => {
    getRole = data1;
})
$.get("/HT_NhomTaiKhoan/GetCoQuan/", (data) => {
    getCoQuan = data;
})

$.get("/DM_CoQuanDonVi/GetCoQuanSearch", data => {
    $.each(data, (index, item) => {
        $("#CoQuan").append(`<option value="${item.Ma_CoQuan_DonVi}">${item.TenCoQuan} </option>`)
    })
})



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
            text: data.message,
            timer: 2000,
            showConfirmButton: false,
        })
        dt.draw();
    }
    else {
        Swal.fire({
            icon: data.status,
            title: 'Cảnh Báo',
            text: data.message,
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

function FnBegin() {
    $('.loading_new').removeClass('d-none');
}

function FnSuccess(data) {
    $('.loading_new').addClass('d-none')

    if (data.status == "success") {
        $('.closeform').click()
    }
   
    Swal.fire({
        icon: data.status,
        title: data.title,
        text: data.message,
        timer: 2000,
        showConfirmButton: false,
    })
    dt.draw();
}


function Failure(data) {
    $('.loading_new').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có Lỗi',
        text: `Lỗi Hệ Thống`,
        timer: 2000,
        showConfirmButton: false,
    })
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
            "url": "/NV_BaoCaoKetQuaKeKhai/LoadDataBaoCao",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            { className: "cn", "targets": [6] }
        ],
        "columns": [
            { "data": "ID" },
            { "data": "TenBaoCao" },
            { "data": "Ten" },
            { "data": "Ten_KeKhai" },
            { "data": "NamBaoCao" },
            {
                "data": { "TrangThai": "TrangThai", "TenFile": "TenFile" }, "render": function (data, type, row) {
                    if (data.TrangThai == true) {
                        return `<span class="badge badge-success">Hoàn Thành Báo Cáo</span>`
                    }
                    return `<span class="badge badge-warning">Đang Tiến Hành</span>`
                }
            },

            {
                "data": { "LoaiKeHoachKeKhai": "LoaiKeHoachKeKhai", "NamBaoCao": "NamBaoCao", "ID": "ID", "TrangThai": "TrangThai", "TenFile": "TenFile", "ID_CoQuan": "ID_CoQuan", "Ma_Loai_KeKhai": "Ma_Loai_KeKhai" }, "render": function (data, type, row) {
                    var taibaocao = ``
                    if (data.Ma_Loai_KeKhai == 3) {
                        taibaocao = `<a class="dropdown-item maubaocao" target="_blank"  href ="/Content/import/MauBaoCaoKetQuaKeKhaiLanDau.doc">
                                                Tải Mẫu Báo Cáo
                                            </a>`
                    } else {
                        taibaocao = ` <a class="dropdown-item maubaocao" target="_blank"  href ="/Content/import/MauBaoCaoKetQuaKeKhai.doc">
                                                Tải Mẫu Báo Cáo
                                            </a>`
                    }
                    if (getRole == "NDDTTT" || getRole == "ADMIN") {
                        if (data.TrangThai == false || data.TrangThai == null) {
                            
                            if (data.ID_CoQuan != getCoQuan) {
                                return `<div class="dropleft">
                                <button class="btn btn-outline-info btn-sm" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                <i class="fa-solid fa-ellipsis-vertical"></i>
                                </button>
                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                ${taibaocao}
                                <a class="dropdown-item" data-model-id="${data.ID}" data-toggle="modal" data-target="#add-dk"  onclick="DinhKemFile(this)">
                                    Đính Kèm Báo Cáo
                                </a>  
                                <a class="dropdown-item " onclick={completeKeKhai(${data.ID})} >
                                    Hoàn Thành Báo cáo
                                </a>
                                </div>
                            </div>`
                            } else {
                                
                                return `<div class="dropleft">
                                <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                <i class="fa-solid fa-ellipsis-vertical"></i>
                                </button>
                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                 
                                ${taibaocao}
                                <a class="dropdown-item" data-model-id="${data.ID}" data-toggle="modal" data-target="#add-dk"  onclick="DinhKemFile(this)">
                                    Đính Kèm Báo Cáo
                                </a>  
                                <a class="dropdown-item" onclick={completeKeKhai(${data.ID})} >
                                    Hoàn Thành Báo cáo
                                </a>
                                </div>
                            </div>`
                            }
                            
                        }
                        else {

                            var row_filedinhkem = ``
                            if (FILEDINHKEM) {
                                row_filedinhkem = `<a class="dropdown-item maubaocao" target="_blank"  href ="/Content/uploads/${data.TenFile}">
                                                        Tải Báo Cáo
                                                    </a>
                                                    `
                            }
                            if (row_filedinhkem != ``) {
                                return `<div class="dropleft">
                                  <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                    <i class="fa-solid fa-ellipsis-vertical"></i>
                                  </button>
                                  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_filedinhkem}
                                  </div>
                                </div>`
                            }
                            else return `<div class="dropleft">
                                  <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                    <i class="fa-solid fa-ellipsis-vertical"></i>
                                  </button>
                                  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_filedinhkem}
                                  </div>
                                </div>`
                        }
                    } else {
                        if (data.TrangThai == false || data.TrangThai == null) {

                           
                            return `<div class="dropleft">
                                <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                <i class="fa-solid fa-ellipsis-vertical"></i>
                                </button>
                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                ${taibaocao}
                                <a class="dropdown-item" data-model-id="${data.ID}" data-toggle="modal" data-target="#add-dk"  onclick="DinhKemFile(this)">
                                    Đính Kèm Báo Cáo
                                </a>  
                                <a class="dropdown-item" onclick={completeKeKhai(${data.ID})} >
                                    Hoàn Thành Báo cáo
                                </a>
                                </div>
                            </div>`
                        }
                        else {

                            var row_filedinhkem = ``
                            if (FILEDINHKEM) {
                                row_filedinhkem = `<a class="dropdown-item" target="_blank" href ="/Content/uploads/${data.TenFile}">
                                                   Tải Báo Cáo
                                                </a> 
                                                ${taibaocao}`
                            }
                            if (row_filedinhkem != ``) {
                                return `<div class="dropleft">
                                  <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                    <i class="fa-solid fa-ellipsis-vertical"></i>
                                  </button>
                                  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_filedinhkem}
                                  </div>
                                </div>`
                            }
                            else return `<div class="dropleft">
                                  <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                    <i class="fa-solid fa-ellipsis-vertical"></i>
                                  </button>
                                  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                       ${row_filedinhkem}
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
    })

    if (!THEM) {
        $('#THEM').remove()
    }
    if (!XUAT) {
        dt.buttons().disable();
    }

    //Kết thúc chỉnh sửa  23-2-2022
}

function DinhKemFile(obj) {
    var ele = $(obj);
    var MaBaoCao = ele.data("model-id");
    $('#MaBaoCao').val(MaBaoCao);
    $('#DaDinhKem').empty();
    $.ajax({
        url: `/NV_BaoCaoKetQuaKeKhai/GetFileDinhKem/${MaBaoCao}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (response) {

            if (response != "") {
                var row = `<div class="col-12">
                            <div class="form-group">
                                <label>Bản Báo Cáo Đã Đính Kèm<span style="color: red"> *</span></label>
                                <span><a target="_blank" href ="/Content/uploads/${response}">${response}</a></span>
                            </div>
                        </div>`
                $('#DaDinhKem').append(row)
            }
          
        }
    })

}

function completeKeKhai(id) {
    $.ajax({
        url: `/NV_BaoCaoKetQuaKeKhai/HoanThanhBaoCao?ID=${id}`,
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

function print_BaoCao(obj) {
    var ele = $(obj);
    var LoaiKeHoachKeKhai = ele.data("model-id").split("-")[0];
    var NamBaoCao = ele.data("model-id").split("-")[1];

    $.ajax({
        url: `/NV_BaoCaoKetQuaKeKhai/InBanBaoCao?LoaiKeHoachKeKhai=${LoaiKeHoachKeKhai}&NamBaoCao=${NamBaoCao}`,
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
            window.open("/bao-cao-ket-qua-ke-khai/ban-in/" + response)
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

async function CheckQuyen() {
    await $.get("/Home/GetQuyen", { MenuCode: "NV_BaoCaoTienDo" }, (data) => {
        if (data.includes("XEM")) {
            XEM = true;
        }
        if (data.includes("FILEDINHKEM")) {
            FILEDINHKEM = true;
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
    await CheckQuyen()
    loadDataTable();

   
    $("#LoaiKeHoachKeKhai").select2();

    $("#CoQuan").select2();
    $("#LoaiKeKhai").select2();
     
  
    $(".DM_Create").validate({
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
            TenBaoCao: {
                required: true,
            },
            NamBaoCao: {
                required: true,
            },
            LoaiKeHoachKeKhai: {
                required: true,
                min: 1,
            },

        },
        messages: {
            TenBaoCao: {
                required: "Tên báo cáo chức danh không được để trống",
            }, 
            NamBaoCao: {
                required: "Năm báo cáo không được để trống",
            },
            LoaiKeHoachKeKhai: {
                required: "Loại kê khai không được để trống",
                min: "Bạn chưa chọn loại kê khai"
            },
          

        }
    })

    $("#search_btn").click(() => {
        var searchValue = $('#Filter').val().trim()
        dt.column(0).search(searchValue);
        dt.draw()
    })

    $("#Filter").keypress(function (e) {
        if (e.keyCode == 13) {
            dt.columns(0).search($("#Filter").val());
            dt.draw();
        }
    });

    $("#CoQuan").change(() => {
        dt.columns(0).search($("#Filter").val());
        dt.columns(1).search($("#CoQuan").val());
        dt.columns(2).search($("#LoaiKeKhai").val());
        dt.columns(3).search($("#keHoachNam").val());
        dt.draw();
    });
    $("#LoaiKeKhai").change(() => {
        dt.columns(0).search($("#Filter").val());
        dt.columns(1).search($("#CoQuan").val());
        dt.columns(2).search($("#LoaiKeKhai").val());
        dt.columns(3).search($("#keHoachNam").val());
        dt.draw();
    });
    $("#keHoachNam").change(() => {
        dt.columns(0).search($("#Filter").val());
        dt.columns(1).search($("#CoQuan").val());
        dt.columns(2).search($("#LoaiKeKhai").val());
        dt.columns(3).search($("#keHoachNam").val());
        dt.draw();
    });


})

