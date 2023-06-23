var t_edt = 0;
var t1 = 6;

$.get("/DM_PhuongXa/GetTinhThanh/", (data) => {
    $("#Ma_TinhThanh").append(`<option value = "">Chọn</option>`)
    $("#Ma_TinhThanh_edt").append(`<option value = "">Chọn</option>`)
    for (let i = 0; i < t1; i++) {

        $(`#Ma_TinhThanhTN${i}_edt`).append(`<option value = "">Chọn</option>`)
    }

    $.each(data, (index, value) => {

        $("#Ma_TinhThanh_edt").append(`<option value ="${value.MaTinhThanh}">${value.TenTinhThanh}</option>`)
        for (let i = 0; i < t1; i++) {

            $(`#Ma_TinhThanhTN${i}_edt`).append(`<option value ="${value.MaTinhThanh}">${value.TenTinhThanh}</option>`)
        }
    })
})





//$.get("/DM_CoQuanDonVi/GetCoQuanTheoID/", { id: data.Ma_CoQuan_DonVi }, (item) => {
//    $(`#Ma_CoQuan_DonVi_edt`).val(item)
//})



$("#clsAddRow_collapseThanNhan_edt").click(() => {
    $(`.tn${t_edt}_edt`).removeClass("d-none");
    t_edt++;
})


function FnBegin_Edit() {
    $('.loading_editcanbo').removeClass('d-none');
}

function FnSuccess_Edit(data) {
    
    $('.loading_editcanbo').addClass('d-none')
    $('.closeform').click()

    Swal.fire({
        icon: 'success',
        title: 'Thành công',
        text: `Thông tin cán bộ đã được cập nhật`,
        timer: 2000,
        showConfirmButton: false,
    }).then(() => { location.reload(); })
}

function Failure_Edit(data) {
    $('.loading_editcanbo').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi',
        text: `Sửa không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}
var listcoquan = new Object;
var listchucvu = new Object;
$.get("/dm_coquandonvi/getcoquan/", (data) => {
    listcoquan = Object.assign(data)
})
$.get("/DM_ChucVu_ChucDanh/GetChucVu/", (data) => {
    listchucvu = Object.assign(data)
})

var s1 = window.location.href.split("/")

var role;
$.post('/DM_CanBo/GetRole', async (data) => {
    role = await data
    
})
$.get("/DM_CanBo/GetSuaCanBo/", { id: s1[s1.length - 1] }).done( async function (data) {

    
    $.each(listcoquan, function (index, coquan) {
        if (role == "ADMIN") {
            $("#Ma_CoQuan_DonVi_edt").append(`<option value ="${data.nguoikekhai.Ma_CoQuan_DonVi}">${coquan.TenCoQuan}</option>`)
            $("#Ma_CoQuan_DonVi_edt").val(data.nguoikekhai.Ma_CoQuan_DonVi)
            $('#Ma_CoQuan_DonVi_edt').change()
        } else if (coquan.MaCoQuan == data.nguoikekhai.Ma_CoQuan_DonVi) {
            
            $("#Ma_CoQuan_DonVi_edt").append(`<option value ="${data.nguoikekhai.Ma_CoQuan_DonVi}">${coquan.TenCoQuan}</option>`)
            $("#Ma_CoQuan_DonVi_edt").val(data.nguoikekhai.Ma_CoQuan_DonVi)
            $('#Ma_CoQuan_DonVi_edt').change()
        }
    })
    $.each(listchucvu, function (index, chucvu) {
        if (role == "ADMIN") {
            $("#Ma_ChucVu_ChucDanh_edt").append(`<option value ="${data.nguoikekhai.Ma_ChucVu_ChucDanh}">${chucvu.TenChucVu}</option>`)
            $("#Ma_ChucVu_ChucDanh_edt").val(data.nguoikekhai.Ma_ChucVu_ChucDanh)
            $('#Ma_ChucVu_ChucDanh_edt').change()
        } else if (chucvu.MaChucVu == data.nguoikekhai.Ma_ChucVu_ChucDanh) {
            $("#Ma_ChucVu_ChucDanh_edt").append(`<option value ="${data.nguoikekhai.Ma_ChucVu_ChucDanh}">${chucvu.TenChucVu}</option>`)
            $("#Ma_ChucVu_ChucDanh_edt").val(data.nguoikekhai.Ma_ChucVu_ChucDanh)
            $('#Ma_ChucVu_ChucDanh_edt').change()
        }
    })


    $('#HoTen_edt').val(data.nguoikekhai.HoTen)
    $('#Ma_CanBo_edt').val(data.nguoikekhai.Ma_CanBo)
    $('#DoB_edt').val(data.nguoikekhai.DoB)

    var parts1 = data.nguoikekhai.DoB.split("/");
    var parts2 = data.nguoikekhai.NgayCap.split("/");

    $('#DoB_edt').val(`${parts1[2]}-${parts1[1]}-${parts1[0]}`)

    $('#DiaChiThuongTru_edt').val(data.nguoikekhai.DiaChiThuongTru)

    $('#SoCCCD_edt').val(data.nguoikekhai.SoCCCD)

    $('#NgayCap_edt').val(`${parts2[2]}-${parts2[1]}-${parts2[0]}`)

    $('#NoiCap_edt').val(data.nguoikekhai.NoiCap)

    $('#Ma_TinhThanh_edt').val(data.nguoikekhai.Ma_TinhThanh)
    $('#Ma_TinhThanh_edt').change()

   
    //$("#Ma_CoQuan_DonVi_edt").val(data.nguoikekhai.Ma_CoQuan_DonVi)
    //$('#Ma_CoQuan_DonVi_edt').change()

    

    $.get("/DM_PhuongXa/GetQuanHuyen", { id: data.nguoikekhai.Ma_TinhThanh }, function (data1) {
        $("#Ma_QuanHuyen_edt").empty()

        $.each(data1, function (index, row) {
            $("#Ma_QuanHuyen_edt").append("<option value='" + row.MaQuanHuyen + "'>" + row.TenQuanHuyen + "</option>")
        })

        $('#Ma_QuanHuyen_edt').val(data.nguoikekhai.Ma_QuanHuyen)
        $('#Ma_QuanHuyen_edt').change()


        $.get("/DM_PhuongXa/GetPhuongXa", { id: data.nguoikekhai.Ma_QuanHuyen }, function (data2) {
            $("#Ma_PhuongXa_TT_edt").empty()
            $.each(data2, function (index, row) {
                $("#Ma_PhuongXa_TT_edt").append("<option value='" + row.MaPhuongXa + "'>" + row.TenPhuongXa + "</option>")
            })

            $('#Ma_PhuongXa_TT_edt').val(data.nguoikekhai.Ma_PhuongXa)
            $('#Ma_PhuongXa_TT_edt').change()
        })
    })

    t_edt = data.thannhan.length

    $.each(data.thannhan, (index, value) => {
        $(`.tn${index}_edt`).removeClass('d-none')
        $(`#HoTenThanNhan${index}_edt`).val(value.HoTenThanNhan)
        $(`#Ma_CanBo_ThanNhan${index}_edt`).val(value.Ma_CanBo_ThanNhan)
        $(`#DoB${index}_edt`)

        var parts1 = value.DoBTN.split("/");
        var parts2 = value.NgayCapTN.split("/");

        $(`#DoBTN${index}_edt`).val(`${parts1[2]}-${parts1[1]}-${parts1[0]}`)
        $(`#NgayCapTN${index}_edt`).val(`${parts2[2]}-${parts2[1]}-${parts2[0]}`)
        $(`#DiaChiThuongTruTN${index}_edt`).val(value.DiaChiThuongTruTN)

        $(`#Ma_TinhThanhTN${index}_edt`).val(value.Ma_TinhThanhTN)
        $(`#Ma_TinhThanhTN${index}_edt`).change()

        $.get("/DM_PhuongXa/GetQuanHuyen", { id: value.Ma_TinhThanhTN }, function (data1) {
            $(`#Ma_QuanHuyenTN${index}_edt`).empty()

            $.each(data1, function (index1, row) {
                $(`#Ma_QuanHuyenTN${index}_edt`).append("<option value='" + row.MaQuanHuyen + "'>" + row.TenQuanHuyen + "</option>")
            })

            $(`#Ma_QuanHuyenTN${index}_edt`).val(value.Ma_QuanHuyenTN)
            $(`#Ma_QuanHuyenTN${index}_edt`).change()


            $.get("/DM_PhuongXa/GetPhuongXa", { id: value.Ma_QuanHuyenTN }, function (data2) {
                $(`#Ma_PhuongXa_TTTN${index}_edt`).empty()
                $.each(data2, function (index2, row) {
                    $(`#Ma_PhuongXa_TTTN${index}_edt`).append("<option value='" + row.MaPhuongXa + "'>" + row.TenPhuongXa + "</option>")
                })

                $(`#Ma_PhuongXa_TTTN${index}_edt`).val(value.Ma_PhuongXa_TTTN)
                $(`#Ma_PhuongXa_TTTN${index}_edt`).change()
            })
        })


        $(`#SoCCCDTN${index}_edt`).val(value.SoCCCDTN)

        $(`#NoiCapTN${index}_edt`).val(value.NoiCapTN)

       
        

        $(`#VaiTroThanNhan${index}_edt`).val(value.VaiTroThanNhan)
        $(`#VaiTroThanNhan${index}_edt`).change()

    })

})

$(document).ready(function () {

    jQuery.validator.addMethod("NotAllowNumber", function (value, element) {
        return this.optional(element) || /^([^0-9]*)$/.test(value);
    }, "Không được phép có chữ số.");

    jQuery.validator.addMethod("NotAllowFirstSpace", function (value, element) {
        return this.optional(element) || /^\S{1}/.test(value);
    }, "Kí tự đầu tiên không được có khoảng trắng.");

    jQuery.validator.addMethod("NotAllowSpecial", function (value, element) {
        return this.optional(element) || /^[A-Za-z0-9_.]+$/.test(value);
    }, "Không được phép có kí tự đặc biệt.");

    $('#Ma_TinhThanh_edt').select2().on("change", function () {
        $.get("/DM_PhuongXa/GetQuanHuyen", { id: $(this).val() }, function (data) {
            $("#Ma_QuanHuyen_edt").empty()
            $("#Ma_QuanHuyen_edt").append("<option value=''>Chọn</option>")
            $("#Ma_PhuongXa_TT_edt").empty()
            $("#Ma_PhuongXa_TT_edt").append("<option value=''>Chọn</option>")
            $.each(data, function (index, row) {
                $("#Ma_QuanHuyen_edt").append("<option value='" + row.MaQuanHuyen + "'>" + row.TenQuanHuyen + "</option>")
            })
        })
    });


    $('#Ma_QuanHuyen_edt').select2().on("change", function () {
        $.get("/DM_PhuongXa/GetPhuongXa", { id: $(this).val() }, function (data) {

            $("#Ma_PhuongXa_TT_edt").empty()
            $("#Ma_PhuongXa_TT_edt").append("<option value=''>Chọn</option>")
            $.each(data, function (index, row) {
                $("#Ma_PhuongXa_TT_edt").append("<option value='" + row.MaPhuongXa + "'>" + row.TenPhuongXa + "</option>")
            })
        })
    });

    $("#Ma_PhuongXa_TT").select2()
    $("#Ma_PhuongXa_TT_edt").select2()

    for (let i = 0; i < t1; i++) {

        $(`#Ma_TinhThanhTN${i}_edt`).select2().on("change", function () {
            $.get("/DM_PhuongXa/GetQuanHuyen", { id: $(this).val() }, function (data) {
                $(`#Ma_QuanHuyenTN${i}_edt`).empty()
                $(`#Ma_QuanHuyenTN${i}_edt`).append("<option value=''>Chọn</option>")
                $(`#Ma_PhuongXa_TTTN${i}_edt`).empty()
                $(`#Ma_PhuongXa_TTTN${i}_edt`).append("<option value=''>Chọn</option>")
                $.each(data, function (index, row) {
                    $(`#Ma_QuanHuyenTN${i}_edt`).append("<option value='" + row.MaQuanHuyen + "'>" + row.TenQuanHuyen + "</option>")
                })
            })
        });

        $(`#Ma_QuanHuyenTN${i}_edt`).select2().on("change", function () {
            $.get("/DM_PhuongXa/GetPhuongXa", { id: $(this).val() }, function (data) {

                $(`#Ma_PhuongXa_TTTN${i}_edt`).empty()
                $(`#Ma_PhuongXa_TTTN${i}_edt`).append("<option value=''>Chọn</option>")
                $.each(data, function (index, row) {
                    $(`#Ma_PhuongXa_TTTN${i}_edt`).append("<option value='" + row.MaPhuongXa + "'>" + row.TenPhuongXa + "</option>")
                })
            })
        });

        $(`#Ma_PhuongXa_TTTN${i}_edt`).select2()


    }
})

