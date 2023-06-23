
var s1 = window.location.href.split("/")

var demcon = 0;
function clearform1(obj) {
    var ele = $(obj);
    
    ele.parent().remove()

    var rowVoChong = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                    <div class="clear" onclick="clearform1(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Họ Và Tên</label>
                                            <input type="text" class="form-control" placeholder="nhập họ và tên" id="HoTenThanNhan0_edt" name="HoTenThanNhan[]">
                                            <input type="text" class="form-control" id="Ma_CanBo_ThanNhan0_edt" name="Ma_CanBo_ThanNhan" hidden>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Ngày sinh</label>
                                            <input type="date" class="form-control" id="DoBTN0_edt" name="DoBTN[]">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nghề Nghiệp</label>
                                            <input type="text" class="form-control" placeholder="nhập nghề nghiệp" id="NgheNghiep0_edt" name="NgheNghiep[]">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nơi Làm Việc</label>
                                            <input type="text" class="form-control" placeholder="nhập nơi làm việc" id="NoiLamViec0_edt" name="NoiLamViec[]">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nơi thường trú</label>
                                            <input type="text" class="form-control" placeholder="nhập Nơi thường trú" id="DiaChiThuongTruTN0_edt" name="DiaChiThuongTruTN[]">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Số CMND/CCCD</label>
                                            <input type="text" class="form-control" placeholder="nhập số CMND/CCCD" id="SoCCCDTN0_edt" name="SoCCCDTN[]">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Ngày Cấp</label>
                                            <input type="date" class="form-control" id="NgayCapTN0_edt" name="NgayCapTN[]">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nơi Cấp</label>
                                            <input type="text" class="form-control" placeholder="nơi cấp" id="NoiCapTN0_edt" name="NoiCapTN[]">
                                        </div>
                                    </div>
                                    <div class="col-3" hidden>
                                        <div class="form-group">
                                            <label>Thân nhân là</label>
                                            <select class="form-control" name="VaiTroThanNhan[]" id="VaiTroThanNhan0_edt" style="width: 100%; height: 100%">
                                                <option value="Vợ Hoặc Chồng">Vợ Hoặc Chồng</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>`
    $("#collapseThanNhan").append(rowVoChong)
}

function clearform(obj) {
    var ele = $(obj);
    ele.parent().remove()
    
    console.log(demcon)
    if (demcon == 1) {
        var rowCon = `<div class="row" style="padding-top: 1%; position: relative; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                    <div class="clear" onclick="clearform(this)"  style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Họ Và Tên</label>
                                            <input type="text" class="form-control" placeholder="nhập họ và tên" name="HoTenThanNhan[]">
                                            <input type="text" class="form-control" name="Ma_CanBo_ThanNhan" hidden>
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Ngày sinh</label>
                                            <input type="date" class="form-control" id="DoB" name="DoBTN[]">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Nghề Nghiệp</label>
                                            <input type="text" class="form-control" placeholder="nhập nghề nghiệp" id="NgheNghiep" name="NgheNghiep[]">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Nơi Làm Việc</label>
                                            <input type="text" class="form-control" placeholder="nhập nơi làm việc" id="NoiLamViec" name="NoiLamViec[]">
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Nơi thường trú</label>
                                            <input type="text" class="form-control" placeholder="nhập Nơi thường trú" id="DiaChiThuongTru" name="DiaChiThuongTruTN[]">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Số CMND/CCCD</label>
                                            <input type="text" class="form-control" placeholder="nhập số CMND/CCCD" id="SoCCCD" name="SoCCCDTN[]">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Ngày Cấp</label>
                                            <input type="date" class="form-control" id="NgayCap" name="NgayCapTN[]">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Nơi Cấp</label>
                                            <input type="text" class="form-control" placeholder="nơi cấp" id="NoiCap" name="NoiCapTN[]">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Thân nhân là</label>
                                            <select class="form-control" name="VaiTroThanNhan[]" id="VaiTroThanNhan" style="width: 100%; height: 100%">
                                                <option value="Con">Con</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>`
        $("#collapseThanNhanCon").append(rowCon)
    } else {
        demcon--;
    }
   
}

$(".clsAddRow").click(() => {
    demcon++;
    $("#collapseThanNhanCon").append(`<div class="row" style="padding-top: 1%; position: relative; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                    <div class="clear" onclick="clearform(this)"  style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Họ Và Tên</label>
                                            <input type="text" class="form-control" placeholder="nhập họ và tên" name="HoTenThanNhan[]">
                                            <input type="text" class="form-control" name="Ma_CanBo_ThanNhan" hidden>
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Ngày sinh</label>
                                            <input type="date" class="form-control" id="DoB" name="DoBTN[]">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Nghề Nghiệp</label>
                                            <input type="text" class="form-control" placeholder="nhập nghề nghiệp" id="NgheNghiep" name="NgheNghiep[]">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Nơi Làm Việc</label>
                                            <input type="text" class="form-control" placeholder="nhập nơi làm việc" id="NoiLamViec" name="NoiLamViec[]">
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Nơi thường trú</label>
                                            <input type="text" class="form-control" placeholder="nhập Nơi thường trú" id="DiaChiThuongTru" name="DiaChiThuongTruTN[]">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Số CMND/CCCD</label>
                                            <input type="text" class="form-control" placeholder="nhập số CMND/CCCD" id="SoCCCD" name="SoCCCDTN[]">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Ngày Cấp</label>
                                            <input type="date" class="form-control" id="NgayCap" name="NgayCapTN[]">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Nơi Cấp</label>
                                            <input type="text" class="form-control" placeholder="nơi cấp" id="NoiCap" name="NoiCapTN[]">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Thân nhân là</label>
                                            <select class="form-control" name="VaiTroThanNhan[]" id="VaiTroThanNhan" style="width: 100%; height: 100%">
                                                <option value="Con">Con</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>`)
})

function FnBegin_Edit() {
    $('.loading_editcanbo').removeClass('d-none');
}

function FnSuccess_Edit(data) {
    $('.loading_editcanbo').addClass('d-none')
    $('.closeform').click()
    if (data != false) {
        Swal.fire({
            icon: 'success',
            title: 'Thành công',
            text: `Thông tin cán bộ đã được cập nhật`,
            timer: 2000,
            showConfirmButton: false,
        })
        window.location.href = "/ke-khai-tai-san";

    } else {
        Swal.fire({
            icon: 'error',
            title: 'Có lỗi',
            text: `Cán bộ đã có trong danh sách kê khai, vui lòng kiểm tra lại`,
            timer: 2000,
            showConfirmButton: false,
        })
    }
   
    
    
}

function Failure_Edit(data) {
    $('.loading_editcanbo').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi',
        text: `Cập nhật thông tin cán bộ không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}




$(document).ready(function () {
    $(".DM_Edt").validate({
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
            HoTen: {
                required: true,
            },
            Email: {
                required: true,
                email: true,
                remote: {
                    url: "/DM_CanBo/EmailTonTai1/",
                    type: "post",
                    dataType: 'json',
                    data: {
                        Ma_CanBo: function () {
                            return $("#Ma_CanBo_edt").val();
                        }
                    }

                }
            },
            DoB: {
                required: true,
                max:  `${new Date().toLocaleDateString('sv')}`
            },
            DiaChiThuongTru: {
                required: true,
            },
            SoCCCD: {
                required: true,
            },
            NgayCap: {
                required: true,
                max: `${new Date().toLocaleDateString('sv')}`
            },
        },
        messages: {
            HoTen: {
                required: "Tên cán bộ không được để trống",
            },
            Email: {
                required: "Email không được để trống",
                email: "Email chưa hợp lệ",
                remote: "Email đã được sử dụng",
            },
            DoB: {
                required: "Ngày sinh Không được bỏ trống",
                max: "Ngày sinh không được quá ngày hiện tại",
            },
            DiaChiThuongTru: {
                required: "Địa Chỉ Thường trú Không được bỏ trống",
                
            },
            SoCCCD: {
                required: "Số căn cước công dân/Chứng minh nhân dân Không được bỏ trống",
            },
            NgayCap: {
                required: "Ngày cấp CCCD/CMND Không được bỏ trống",
                max: "Ngày cấp CCCD/CMND không được quá ngày hiện tại",
            },
        }
    })
   

     $.get("/dm_coquandonvi/getcoquan/", (data) => {
        $.each(data, function (index, value) {
            $("#Ma_CoQuan_DonVi_edt").append(`<option value ="${value.MaCoQuan}">${value.TenCoQuan}</option>`)
       })
    })
    $.get("/DM_ChucVu_ChucDanh/GetChucVu/", (data) => {
        $.each(data, function (index, value) {
            $("#Ma_ChucVu_ChucDanh_edt").append(`<option value ="${value.MaChucVu}">${value.TenChucVu}</option>`)
        })
    })
    $("#clsAddRow_collapseThanNhan_edt").click(() => {
       
    })
  
    $("#Ma_PhuongXa_TT").select2()
    $("#Ma_PhuongXa_TT_edt").select2()
    $('#Ma_CoQuan_DonVi_edt').select2()
    $('#Ma_ChucVu_ChucDanh_edt').select2()


    $.get("/DM_CanBo/GetSuaCanBo/", { id: s1[s1.length - 1] }).done(async function (data) {
        $("#Email_edt").val(data.nguoikekhai.Email)

        $('#HoTen_edt').val(data.nguoikekhai.HoTen)
        $('#Ma_CanBo_edt').val(data.nguoikekhai.Ma_CanBo)
        $('#DoB_edt').val(data.nguoikekhai.DoB)

        var parts1 = ''
        var parts2 = ""
        if (data.nguoikekhai.DoB != null) {
            var parts1 = data.nguoikekhai.DoB.split("/");
        }

        if (data.nguoikekhai.NgayCap != null) {
            var parts2 = data.nguoikekhai.NgayCap.split("/");
        }

        $('#DoB_edt').val(`${parts1[2]}-${parts1[1]}-${parts1[0]}`)

        $("#Ma_CoQuan_DonVi_edt").val(data.nguoikekhai.Ma_CoQuan_DonVi)
        $('#Ma_CoQuan_DonVi_edt').change()
       
        $("#Ma_ChucVu_ChucDanh_edt").val(data.nguoikekhai.Ma_ChucVu_ChucDanh)
        $('#Ma_ChucVu_ChucDanh_edt').change()



        $('#DiaChiThuongTru_edt').val(data.nguoikekhai.DiaChiThuongTru)

        $('#SoCCCD_edt').val(data.nguoikekhai.SoCCCD)

        $('#NgayCap_edt').val(`${parts2[2]}-${parts2[1]}-${parts2[0]}`)

        $('#NoiCap_edt').val(data.nguoikekhai.NoiCap)


        t_edt = data.thannhan.length

        var flagVoChong = false
        var flagCon = false
        $.each(data.thannhan, (index, value) => {
            console.log(value.VaiTroThanNhan)
            if (value.VaiTroThanNhan == "Vợ Hoặc Chồng") {
               
                $(`#DoB${index}_edt`)

                var parts1 = value.DoBTN.split("/");
                var parts2 = value.NgayCapTN.split("/");

                $(`#DoBTN${index}_edt`).val(``)

                var rowVoChong = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                    <div class="clear" onclick="clearform1(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Họ Và Tên</label>
                                            <input type="text" class="form-control" placeholder="nhập họ và tên" id="HoTenThanNhan" name="HoTenThanNhan[]" value="${value.HoTenThanNhan}">
                                            <input type="text" class="form-control" id="Ma_CanBo_ThanNhan" name="Ma_CanBo_ThanNhan" hidden value="${value.Ma_CanBo_ThanNhan}">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Ngày sinh</label>
                                            <input type="date" class="form-control" id="DoBTN" name="DoBTN[]" value="${parts1[2]}-${parts1[1]}-${parts1[0]}">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nghề Nghiệp</label>
                                            <input type="text" class="form-control" placeholder="nhập nghề nghiệp" id="NgheNghiep" name="NgheNghiep[]" value="${value.NgheNghiep}">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nơi Làm Việc</label>
                                            <input type="text" class="form-control" placeholder="nhập nơi làm việc" id="NoiLamViec0_edt" name="NoiLamViec[]"  value="${value.NoiLamViec}" >
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nơi thường trú</label>
                                            <input type="text" class="form-control" placeholder="nhập Nơi thường trú" id="DiaChiThuongTruTN0_edt" name="DiaChiThuongTruTN[]" value="${value.DiaChiThuongTruTN}">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Số CMND/CCCD</label>
                                            <input type="text" class="form-control" placeholder="nhập số CMND/CCCD" id="SoCCCDTN0_edt" name="SoCCCDTN[]" value="${value.SoCCCDTN}">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Ngày Cấp</label>
                                            <input type="date" class="form-control" id="NgayCapTN0_edt" name="NgayCapTN[]" value="${parts2[2]}-${parts2[1]}-${parts2[0]}">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nơi Cấp</label>
                                            <input type="text" class="form-control" placeholder="nơi cấp" id="NoiCapTN0_edt" name="NoiCapTN[]" value="${value.NoiCapTN}">
                                        </div>
                                    </div>
                                    <div class="col-3" hidden>
                                        <div class="form-group">
                                            <label>Thân nhân là</label>
                                            <select class="form-control" name="VaiTroThanNhan[]" id="VaiTroThanNhan0_edt" style="width: 100%; height: 100%" >
                                                <option value="Vợ Hoặc Chồng">Vợ Hoặc Chồng</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>`
                $("#collapseThanNhan").append(rowVoChong)
                flagVoChong = true
            }
            else {

                var parts1 = value.DoBTN.split("/");
                var parts2 = value.NgayCapTN.split("/");
                demcon++;

                var rowCon = `<div class="row" style="padding-top: 1%; padding-bottom: 1%;  position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                    <div class="clear" onclick="clearform(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Họ Và Tên</label>
                                            <input type="text" class="form-control" placeholder="nhập họ và tên" name="HoTenThanNhan[]"  value="${value.HoTenThanNhan}">
                                           
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Ngày sinh</label>
                                            <input type="date" class="form-control" id="DoB" name="DoBTN[]" value="${parts1[2]}-${parts1[1]}-${parts1[0]}">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Nghề Nghiệp</label>
                                            <input type="text" class="form-control" placeholder="nhập nghề nghiệp" id="NgheNghiep" name="NgheNghiep[]">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Nơi Làm Việc</label>
                                            <input type="text" class="form-control" placeholder="nhập nơi làm việc" id="NoiLamViec" name="NoiLamViec[]">
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Nơi thường trú</label>
                                            <input type="text" class="form-control" placeholder="nhập Nơi thường trú" id="DiaChiThuongTru" name="DiaChiThuongTruTN[]" value="${value.DiaChiThuongTruTN}">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Số CMND/CCCD</label>
                                            <input type="text" class="form-control" placeholder="nhập số CMND/CCCD" id="SoCCCD" name="SoCCCDTN[]" value="${value.SoCCCDTN}">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Ngày Cấp</label>
                                            <input type="date" class="form-control" id="NgayCap" name="NgayCapTN[]" value="${parts2[2]}-${parts2[1]}-${parts2[0]}">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Nơi Cấp</label>
                                            <input type="text" class="form-control" placeholder="nơi cấp" id="NoiCap" name="NoiCapTN[]" value="${value.NoiCapTN}">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Thân nhân là</label>
                                            <select class="form-control" name="VaiTroThanNhan[]" id="VaiTroThanNhan" style="width: 100%; height: 100%">
                                                <option value="Con">Con</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>`
                $("#collapseThanNhanCon").append(rowCon)
                flagCon = true
            }

        

           

        })

        if (!flagVoChong) {

            var rowVoChong = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                    <div class="clear" onclick="clearform1(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Họ Và Tên</label>
                                            <input type="text" class="form-control" placeholder="nhập họ và tên" id="HoTenThanNhan0_edt" name="HoTenThanNhan[]">
                                            <input type="text" class="form-control" id="Ma_CanBo_ThanNhan0_edt" name="Ma_CanBo_ThanNhan" hidden>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Ngày sinh</label>
                                            <input type="date" class="form-control" id="DoBTN0_edt" name="DoBTN[]">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nghề Nghiệp</label>
                                            <input type="text" class="form-control" placeholder="nhập nghề nghiệp" id="NgheNghiep0_edt" name="NgheNghiep[]">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nơi Làm Việc</label>
                                            <input type="text" class="form-control" placeholder="nhập nơi làm việc" id="NoiLamViec0_edt" name="NoiLamViec[]">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nơi thường trú</label>
                                            <input type="text" class="form-control" placeholder="nhập Nơi thường trú" id="DiaChiThuongTruTN0_edt" name="DiaChiThuongTruTN[]">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Số CMND/CCCD</label>
                                            <input type="text" class="form-control" placeholder="nhập số CMND/CCCD" id="SoCCCDTN0_edt" name="SoCCCDTN[]">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Ngày Cấp</label>
                                            <input type="date" class="form-control" id="NgayCapTN0_edt" name="NgayCapTN[]">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nơi Cấp</label>
                                            <input type="text" class="form-control" placeholder="nơi cấp" id="NoiCapTN0_edt" name="NoiCapTN[]">
                                        </div>
                                    </div>
                                    <div class="col-3" hidden>
                                        <div class="form-group">
                                            <label>Thân nhân là</label>
                                            <select class="form-control" name="VaiTroThanNhan[]" id="VaiTroThanNhan0_edt" style="width: 100%; height: 100%">
                                                <option value="Vợ Hoặc Chồng">Vợ Hoặc Chồng</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>`
            $("#collapseThanNhan").append(rowVoChong)

        }

        if (!flagCon) {
            console.log("Aaa")
            demcon++;
            var rowCon = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                    <div class="clear" onclick="clearform(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Họ Và Tên</label>
                                            <input type="text" class="form-control" placeholder="nhập họ và tên" name="HoTenThanNhan[]">
                                            <input type="text" class="form-control" name="Ma_CanBo_ThanNhan" hidden>
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Ngày sinh</label>
                                            <input type="date" class="form-control" id="DoB" name="DoBTN[]">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Nghề Nghiệp</label>
                                            <input type="text" class="form-control" placeholder="nhập nghề nghiệp" id="NgheNghiep" name="NgheNghiep[]">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Nơi Làm Việc</label>
                                            <input type="text" class="form-control" placeholder="nhập nơi làm việc" id="NoiLamViec" name="NoiLamViec[]">
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Nơi thường trú</label>
                                            <input type="text" class="form-control" placeholder="nhập Nơi thường trú" id="DiaChiThuongTru" name="DiaChiThuongTruTN[]">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Số CMND/CCCD</label>
                                            <input type="text" class="form-control" placeholder="nhập số CMND/CCCD" id="SoCCCD" name="SoCCCDTN[]">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Ngày Cấp</label>
                                            <input type="date" class="form-control" id="NgayCap" name="NgayCapTN[]">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Nơi Cấp</label>
                                            <input type="text" class="form-control" placeholder="nơi cấp" id="NoiCap" name="NoiCapTN[]">
                                        </div>
                                    </div>
                                    <div class="col-4" hidden>
                                        <div class="form-group">
                                            <label>Thân nhân là</label>
                                            <select class="form-control" name="VaiTroThanNhan[]" id="VaiTroThanNhan" style="width: 100%; height: 100%">
                                                <option value="Con">Con</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>`
            $("#collapseThanNhanCon").append(rowCon)
        }
    })

})

