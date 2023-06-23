// Lấy Dữ liệu phân quyền từ LoadData
var Auth;


$.get("/DM_Loai_CoQuan_DonVi/GetLoaiCoQuanDonVi", (data) => {
    $.each(data, (index, value) => {
        $("#MaLoai_CoQuan_DonVi").append(`<option value ="${value.MaLoaiCoQuan}">${value.TenLoaiCoQuan}</option>`)
    })
})
//Tìm kiếm theo Tên Cơ quan
$("#search_btn").click(() => {
    var searchValue = $('#Filter_QLSDT').val().trim()
    dt.column(1).search(searchValue);
    dt.draw()
})



$("#Filter_QLSDT").keypress(function (e) {
    if (e.keyCode == 13) {
        dt.columns(1).search($("#Filter_QLSDT").val());
        dt.draw();
    }
});

// auto Focus select option


// Chức năng validate input
function ValidateInput(inputSeleted) {
    console.log(inputSeleted)
    inputSeleted.addEventListener('blur', (event) => {
        console.log("blur")
    });
}


// kết thúc chỉnh sửa
var dt;


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

        //Hoàng chỉnh sửa option tỉnh, huyện 23-2-2022
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/DM_CoQuanDonVi/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Ten_Loai_CQDV" },
            { "data": "Ten_Loai_CQDV" },
            { "data": "Ten" },
            {
                "data": "Ma_CoQuan_DonVi", "render": function (data, type, row) {
                    return `
                            <button class="btn btn-outline-info btn-sm " onclick="Edit(this)" data-model-id="${data}" data-toggle="modal" data-target="#add-sh1">
                                <i class="fas fa-pencil-alt"></i>
                            </button>
                            <button class="btn btn-outline-danger btn-sm delete" data-model-id="${data}" onclick="Delete(this)">
                                <i class="fas fa-trash"></i>
                            </button>
                            `
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
}



//Hoàng chỉnh sửa Clear data form thêm mới 23-2-2022
function clearDataAdd() {
    $("#Ten").val("")
    $("#DiaChi").val("")
    $("#MST").val("")
    $("#SoDienThoai").val("")
    $("#Ma_TinhThanh option:selected").remove()
    $("#Ma_QuanHuyen option").remove()
    $("#Ma_QuanHuyen").append("<option value=''>Chọn</option>")
    $("#Ma_PhuongXa option").remove()
    $("#Ma_PhuongXa").append("<option value=''>Chọn</option>")
    $("#MaLoai_CoQuan_DonVi").append("<option value=''>Chọn</option>")
}
//Kết thúc chỉnh sửa  23-2-2022

//Edit
function FnBegin_Edit() {
    $('.loading_editcoquan').removeClass('d-none');
}

function FnSuccess_Edit(data) {
    $('.loading_editcoquan').addClass('d-none')
    $('.closeform').click()

    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: ` Sửa thành công`,
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
        text: `Sửa không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}

// thông báo thêm Cơ Quan
function FnBegin() {
    $('.loading_newcoquan').removeClass('d-none');
}

function FnSuccess(data) {
    clearDataAdd()
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
    $.get(url, { id: Ma_CoQuan_DonVi })
        .done(function (data) {
            
            $("#sua").empty()
            var row = ``
            var row2 = ``
            var row1 = `<div class="row">
                            <input type="hidden" disable="true" class="form-control" id="Ma_CoQuan_DonVi" name="Ma_CoQuan_DonVi" value="${data.Ma_CoQuan_DonVi}">
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Tên Cơ Quan - Đơn Vị</label>

                                    <input type="text" class="form-control" placeholder="nhập tên cơ quan" id="Ten_edit" name="Ten" value="${data.Ten}">
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Loại Đơn vị</label>
                                    <select class="form-control" name="MaLoai_CoQuan_DonVi" id="MaLoai_CoQuan_DonVi_edit" style="width: 100%; height: 100%">
                                        <option value="">Chọn</option>
                                    </select>
                                </div>
                            </div>

                            
                        </div>
                                `
            $('#sua').prepend(row1);

            //CheckValidate form sửa Cơ quan cán bộ
            
            // load dữ liệu loại cơ quan
            $.get("/DM_Loai_CoQuan_DonVi/GetLoaiCoQuanDonVi", (item) => {
                $.each(item, (index, value) => {
                    if (value.MaLoaiCoQuan == data.MaLoai_CoQuan_DonVi) {
                        $("#MaLoai_CoQuan_DonVi_edit").append(`<option value ="${value.MaLoaiCoQuan}" selected>${value.TenLoaiCoQuan}</option>`)
                    } else {
                        $("#MaLoai_CoQuan_DonVi_edit").append(`<option value ="${value.MaLoaiCoQuan}">${value.TenLoaiCoQuan}</option>`)
                    }

                })
            })

            $("#MaLoai_CoQuan_DonVi_edit").select2()
            
        })
};



function Delete(obj) {
    var ele = $(obj);
    var Ma_CoQuan_DonVi = ele.data("model-id");
    var url = `/DM_CoQuanDonVi/delete/${Ma_CoQuan_DonVi}`
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
                            text: `${data.Ten} đã được xóa`,
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

////Lấy dữ liệu phân quyền để kiểm tra
//var GetAuth = (() => {
//    //lấy Route, Kiểm tra route, kiểm tra chức năng, kiểm tra và Trạng thái
//    var s1 = window.location.href.split("/");
//    var getRoute = `/${s1[3]}`;

//    $.post("/DM_CoQuanDonVi/GetAuth", { route: getRoute }, (data) => {
       
//        $.each(data, (index, value) => {
//            //Kiểm tra chức năng Xem
//            if ( value.teTenChucNang == "Xem" && value.TrangThai == false) {
//                //Redirect về trang khác

//            }
//            //Kiểm tra chức năng thêm
//            if ( value.teTenChucNang == "Thêm" && value.TrangThai == false) {
//                //khóa chức năng thêm
//                $(".input-group-append").remove();
//            }
//            //Kiểm tra chức năng sửa
//            if ( value.teTenChucNang == "Xóa" && value.TrangThai == false) {
//                //khóa chức năng Xóa

//            }
//            //Kiểm tra chức năng Xóa
//            if (value.teTenChucNang == "Sửa" && value.TrangThai == false) {
//                //khóa chức năng Sửa

//            }

//        })
//    })
//})

$(document).ready(function () {
    
    loadDataTable();
    jQuery.validator.addMethod("NotAllowNumber", function (value, element) {
        return this.optional(element) || /^([^0-9]*)$/.test(value);
    }, "Không được phép có chữ số.");

    jQuery.validator.addMethod("NotAllowFirstSpace", function (value, element) {
        return this.optional(element) || /^\S{1}/.test(value);
    }, "Kí tự đầu tiên không được có khoảng trắng.");

    jQuery.validator.addMethod("NotAllowSpecial", function (value, element) {
        return this.optional(element) || /^[A-Za-z0-9_.]+$/.test(value);
    }, "Không được phép có kí tự đặc biệt.");
    
 
    $("#MaLoai_CoQuan_DonVi").select2()

    //autofocus selected
    $(document).on('select2:open', () => {
        document.querySelector('.select2-search__field').focus();
    });

    //validate form thêm dữ liệu
    $(".formDM_CanBo").validate({
        ignore: [],
        errorElement: 'div',
        rules: {
            "Ten": {
                required: true,
            },
            "MaLoai_CoQuan_DonVi": {
                required: true,
            },
           
          

        },
        messages: {
            Ten: "Vui lòng nhập Dữ liệu",
            MaLoai_CoQuan_DonVi: "Vui lòng chọn Loại Đơn vị",
            diachi: {
                required: "Vui lòng nhập địa chỉ",
            },
          
           
        },
        errorPlacement: function (error, element) {
            if (element.is("select")) {
                error.appendTo(element.parent(this))
            } else {
                error.insertAfter(element);
            }
        }
    });
  
    // auto focus input
    $('.modal').on('shown.bs.modal', function () {
        $(this).find('[autofocus]').focus();
    });

    $(document).on( "#btThemCoQuan", function () {
        $('[autofocus]', this).focus();
    });



   

   //Phân quyền hệ thống
    GetAuth();
})


