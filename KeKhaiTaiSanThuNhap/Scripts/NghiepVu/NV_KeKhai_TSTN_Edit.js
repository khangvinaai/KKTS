const params = window.location.pathname.split('/');

var demdat = 0;
var demdatkhac = 0;
var demnhao = 0;
var demxaydungkhac = 0;
var demcaylaunam = 0;
var demrungsanxuat = 0;
var demvktglvd = 0;
var demvangkimcuong = 0;
var demtien = 0;
var demcophieu = 0;
var demtraiphieu = 0;
var demvongop = 0;
var demgiaytokhac = 0;
var demtaisantheoquydinh = 0;
var demtaisankhac = 0;

//tài sản nước ngoài
var tsnndemdat = 0;
var tsnndemdatkhac = 0;
var tsnndemnhao = 0;
var tsnndemxaydungkhac = 0;
var tsnndemcaylaunam = 0;
var tsnndemrungsanxuat = 0;
var tsnndemvktglvd = 0;
var tsnndemvangkimcuong = 0;
var tsnndemtien = 0;
var tsnndemcophieu = 0;
var tsnndemtraiphieu = 0;
var tsnndemvongop = 0;
var tsnndemgiaytokhac = 0;
var tsnndemtaisantheoquydinh = 0;
var tsnndemtaisankhac = 0;

// xóa form con thân nhân

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



//Xóa form đất
function clearformDat(obj) {
    var ele = $(obj);
    ele.parent().remove()
    --demdat;
    
    if (demdat == 0) {
        demdat = 1;
        var rowDat = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformDat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ</label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]">
                                    <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Ở">
                                    <input type="text" class="form-control" hidden name="TenLoaiDat[]" value="">
                                    <input type="text" class="form-control" hidden name="Ma_QuyenSuDungDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích</label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng</label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanQuyenSoHuuDatO" name="GiayChungNhanQuyenSoHuuDat[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)</label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinDatKhacNeuCo" name="ThongTinDatKhacNeuCo[]"></textarea>
                                </div>
                            </div>

                        </div>`
        $('#clsAddRow_collapseDat').before(rowDat)
    } else {
    }
}
//Xóa form đất khác
function clearformCacLoaiDatKhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    console.log(" remove:", demdatkhac)
    demdatkhac--;
    if (demdatkhac == 0) {
        demdatkhac = 1;
        var rowdatkhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCacLoaiDatKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                    <div class="col-3">
                        <div class="form-group">
                            <label>Loại đất</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenLoaiDat" name="TenLoaiDat[]">
                                <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Khác">
                                    <input type="text" class="form-control" hidden name="Ma_QuyenSuDungDat[]">
                                </div>
                            </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Diện tích</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]">
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giấy chứng nhận quyền sử dụng</label>
                                <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanQuyenSoHuuDat" name="GiayChungNhanQuyenSoHuuDat[]"></textarea>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Thông tin khác (nếu có)</label>
                                <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinDatKhacNeuCo" name="ThongTinDatKhacNeuCo[]"></textarea>
                            </div>
                        </div>

                    </div>`
        $('#clsAddRow_collapseCacLoaiDatKhac').before(rowdatkhac)
    } else {

    }
}
//Xóa form nhà ở
function clearformNhaO(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demnhao--;
    if (demnhao == 0) {
        demnhao = 1;
        var rownhao = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformNhaO(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiNhaO" name="DiaChiNhaO[]">
                                    <input type="text" class="form-control" name="Ma_NhaO[]" hidden>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Loại nhà</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="LoaiNhaO" name="LoaiNhaO[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Diện tích sử dụng</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichNhaO" name="DienTichNhaO[]">
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị của nhà ở" id="GiaTriNhaO" name="GiaTriNhaO[]">
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giấy chứng nhận quyền sử dụng</label>
                                <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanNhaO" name="GiayChungNhanNhaO[]"></textarea>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Thông tin khác (nếu có)</label>
                                <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinNhaOKhacNeuCo" name="ThongTinNhaOKhacNeuCo[]"></textarea>
                            </div>
                        </div>

                    </div> `
        $('#clsAddRow_collapseNhaO').before(rownhao)
    } else {

    }
}
//Xóa form xây dựng khác
function clearformCongTrinh(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demxaydungkhac--;
    if (demxaydungkhac == 0) {
        demxaydungkhac = 1;
        var rowxaydungkhac = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCongTrinh(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên công trình</label>
                                <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenCongTrinh" name="TenCongTrinh[]">
                                    <input type="text" class="form-control" hidden name="Ma_CongTrinh[]">
                            </div>
                        </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Địa chỉ</label>
                            <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiCongTrinh" name="DiaChiCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Loại công trình</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="LoaiCongTrinh" name="LoaiCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Cấp công trình</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="CapCongTrinh" name="CapCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Diện tích</label>
                            <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichCongTrinh" name="DienTichCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị của công trình" id="GiaTriCongTrinh" name="GiaTriCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Giấy chứng nhận quyền sử dụng</label>
                            <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanCongTrinh" name="GiayChungNhanCongTrinh[]"></textarea>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Thông tin khác (nếu có)</label>
                            <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinCongTrinhKhacNeuCo" name="ThongTinCongTrinhKhacNeuCo[]"></textarea>
                        </div>
                    </div>

                </div> `
        $('#clsAddRow_collapseCongTrinh').before(rowxaydungkhac)
    } else {

    }
}
//Xóa form caylaunam
function clearformCayLauNam(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demcaylaunam--;
    if (demcaylaunam == 0) {
        demcaylaunam = 1;
        var rowcaylaunam = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCayLauNam(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Loại cây</label>
                                <input type="text" class="form-control" placeholder="Nhập loại cây" id="LoaiCay" name="TenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='cln'>
                                        <input type="text" class="form-control" hidden name="Ma_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng cây" id="SoLuongCay" name="SoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập loại đất" id="GiaTriCay" name="GiaTriTS[]">
                            </div>
                        </div>
                    </div>`
        $('#clsAddRow_collapseCayLauNam').before(rowcaylaunam)
    } else {

    }
}
//Xóa form rungsanxuat
function clearformRungSanXuat(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demrungsanxuat--;
    if (demrungsanxuat == 0) {
        demrungsanxuat = 1;
        var rowrungsanxuat = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformRungSanXuat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Loại rừng</label>
                                <input type="text" class="form-control" placeholder="Nhập loại rừng" id="LoaiRung" name="TenTaiSan[]">
                                <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='rsx'>
                                <input type="text" class="form-control" hidden name="Ma_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Diện tích</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích rừng" id="DienTichRung" name="SoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị rừng" id="GiaTriRung" name="GiaTriTS[]">
                            </div>
                        </div>
                    </div>`
        $('#clsAddRow_collapseRungSanXuat').before(rowrungsanxuat)
    } else {

    }
}
//Xóa form Vật kiến trúc khác gắn liền với đất
function clearformKienTruc(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demvktglvd--;
    if (demvktglvd == 0) {
        demvktglvd = 1;
        var rowvktglvd = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformKienTruc(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên gọi</label>
                                <input type="text" class="form-control" placeholder="Nhập tên" id="TenKienTrucGanDat" name="TenTaiSan[]">
                                <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='vkt'>
                                <input type="text" class="form-control" hidden name="Ma_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng vật kiến trúc" id="SoLuongKienTruc" name="SoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị vật kiến trúc" id="GiaTriVatKienTruc" name="GiaTriTS[]">
                            </div>
                        </div>
                    </div> `
        $('#clsAddRow_collapseKienTruc').before(rowvktglvd)
    } else {

    }
}
//Xóa form Vật kiến trúc khác gắn liền với đất
function clearformKimLoaiDaQuy(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demvangkimcuong--;
    if (demvangkimcuong == 0) {
        demvangkimcuong = 1;
        var rowvangkimcuong = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformKimLoaiDaQuy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Tên gọi</label>
                                <input type="text" class="form-control" placeholder="Nhập tên" id="TenTrangSuc" name="TenTrangSuc[]">
                                <input type="text" class="form-control" hidden name="Ma_TrangSuc[]" value=''>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTrangSuc" name="GiaTriTrangSuc[]">
                            </div>
                        </div>
                    </div> `
        $('#clsAddRow_collapseKimLoaiDaQuy').before(rowvangkimcuong)
    } else {

    }
}
//Xóa form tiền
function clearformTien(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demtien--;
    if (demtien == 0) {
        demtien = 1;
        var rowtien = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                       <div class="clear" onclick="clearformTien(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Tên gọi</label>
                            <input type="text" class="form-control" placeholder="Nhập tên" id="TenLoaiTien" name="TenLoaiTien[]">
                                <input type="text" class="form-control" hidden name="Ma_Tien[]" value=''>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriLoaiTien" name="GiaTriLoaiTien[]">
                        </div>
                    </div>
                </div>`
        $('#clsAddRow_collapseTien').before(rowtien)
    } else {

    }
}
//Xóa form cổ phiếu
function clearformCoPhieu(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demcophieu--;
    if (demcophieu == 0) {
        demcophieu = 1;
        var rowcophieu = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCoPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên cố phiếu</label>
                                <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="TenPhieu" name="TenPhieu[]">
                                    <input hidden value="CoPhieu" id="LoaiPhieu" name="LoaiPhieu">
                                        <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="SoLuongPhieu" name="SoLuongPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                            </div>
                        </div>
                     </div>`
        $('#clsAddRow_collapseCoPhieu').before(rowcophieu)
    } else {

    }
}
//Xóa form trái phiếu
function clearformTraiPhieu(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demtraiphieu--;
    if (demtraiphieu == 0) {
        demtraiphieu = 1;
        var rowtraiphieu = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformTraiPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên trái phiếu</label>
                                <input type="text" class="form-control" placeholder="Nhập tên trái phiếu" id="TenPhieu" name="TenPhieu[]">
                                <input hidden value="TraiPhieu" id="LoaiPhieu" name="LoaiPhieu">
                                <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]">
                            </div>
                        </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Số lượng</label>
                            <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="SoLuongPhieu" name="SoLuongPhieu[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                        </div>
                    </div>
                </div> `
        $('#clsAddRow_collapseTraiPhieu').before(rowtraiphieu)
    } else {

    }
}
//Xóa form vốn gốp
function clearformGopVon(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demvongop--;
    if (demvongop == 0) {
        demvongop = 1;
        var rowvongop = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformGopVon(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-8">
                            <div class="form-group">
                                <label>Hình thức góp vốn</label>
                                <input type="text" class="form-control" placeholder="Nhập tên hình thức góp vốn" id="TenPhieu" name="TenPhieu[]">
                                <input hidden value="VonGop" id="LoaiPhieu" name="LoaiPhieu">
                                <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                            </div>
                        </div>
                    </div> `
        $('#clsAddRow_collapseGopVon').before(rowvongop)
    } else {

    }
}
//Xóa form giấy tờ có giá trị
function clearformGiayToKhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demgiaytokhac--;
    if (demgiaytokhac == 0) {
        demgiaytokhac = 1;
        var rowgiaytokhac = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformGiayToKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-8">
                            <div class="form-group">
                                <label>Tên giấy tờ có giá</label>
                                <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="TenPhieu" name="TenPhieu[]">
                                <input hidden value="GiayTo" id="LoaiPhieu" name="LoaiPhieu">
                                <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                            </div>
                        </div>
                    </div> `
        $('#clsAddRow_collapseGiayToKhac').before(rowgiaytokhac)
    } else {

    }
}
//Xóa form tài sản theo quy định
function clearformGiayDangKy(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demtaisantheoquydinh--;
    if (demtaisantheoquydinh == 0) {
        demtaisantheoquydinh = 1;
        var rowtaisantheoquydinh = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformGiayDangKy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanKhac" name="TenTaiSanKhac[]">
                                <input hidden value="GiayDangKy" id="LoaiTaiSanKhac" name="LoaiTaiSanKhac">
                                <input hidden value="" id="NamBatDauSoHuuTaiSanKhac" name="NamBatDauSoHuuTaiSanKhac[]">
                                <input type="text" class="form-control" hidden name="Ma_TaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số đăng ký</label>
                                <input type="text" class="form-control" placeholder="Nhập số đăng ký" id="SoDangKyTaiSanKhac" name="SoDangKyTaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div> `
        $('#clsAddRow_collapseGiayDangKy').before(rowtaisantheoquydinh)
    } else {

    }
}
//Xóa form tài sản khác
function clearformTaiSanKhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demtaisankhac--;
    if (demtaisankhac == 0) {
        demtaisankhac = 1;
        var rowtaisankhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformTaiSanKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanKhac" name="TenTaiSanKhac[]">
                                <input hidden value="TaiSanKhac" id="LoaiTaiSanKhac" name="LoaiTaiSanKhac">
                                <input hidden value="" id="SoDangKyTaiSanKhac" name="SoDangKyTaiSanKhac[]">
                                <input type="text" class="form-control" hidden name="Ma_TaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Năm bắt đầu sở hữu</label>
                                <input type="text" class="form-control" placeholder="Nhập năm bắt đầu sỡ hữu" id="NamBatDauSoHuuTaiSanKhac" name="NamBatDauSoHuuTaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div> `
        $('#clsAddRow_collapseTaiSanKhac').before(rowtaisankhac)
    } else {

    }
}

//tài sản nước ngoài
//Xóa form đất tài sản nước ngoài
function clearformtsnnDat(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemdat--;
    console.log("remove: ", tsnndemdat)
    if (tsnndemdat == 0) {
        tsnndemdat = 1;
        var rowtsnnDat = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnDat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ</label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiDat" name="tsnnDiaChiDat[]">
                                    <input type="text" class="form-control" hidden name="tsnnLoaiDat[]" value="Đất Ở">
                                    <input type="text" class="form-control" hidden name="tsnnTenLoaiDat[]" value="">
                                    <input type="text" class="form-control" hidden name="tsnnMa_QuyenSuDungDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích</label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichDat" name="tsnnDienTichDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="tsnnGiaTriDat" name="tsnnGiaTriDat[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng</label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanQuyenSoHuuDat" name="tsnnGiayChungNhanQuyenSoHuuDat[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)</label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinDatKhacNeuCo" name="tsnnThongTinDatKhacNeuCo[]"></textarea>
                                </div>
                            </div>

                        </div>`
        $('#clsAddRow_collapsetsnnDat').before(rowtsnnDat)
    } else {

    }
}
//Xóa form đất khác tài sản nước ngoài
function clearformtsnnCacLoaiDatKhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemdatkhac--;
    if (tsnndemdatkhac == 0) {
        tsnndemdatkhac = 1;
        var rowtsnndatkhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCacLoaiDatKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                    <div class="col-3">
                        <div class="form-group">
                            <label>Loại đất</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnTenLoaiDat" name="tsnnTenLoaiDat[]">
                                <input type="text" class="form-control" hidden name="tsnnLoaiDat[]" value="Đất Khác">
                                    <input type="text" class="form-control" hidden name="tsnnMa_QuyenSuDungDat[]">
                                </div>
                            </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiDat" name="tsnnDiaChiDat[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Diện tích</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichDat" name="tsnnDienTichDat[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="tsnnGiaTriDat" name="tsnnGiaTriDat[]">
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giấy chứng nhận quyền sử dụng</label>
                                <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanQuyenSoHuuDat" name="tsnnGiayChungNhanQuyenSoHuuDat[]"></textarea>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Thông tin khác (nếu có)</label>
                                <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinDatKhacNeuCo" name="tsnnThongTinDatKhacNeuCo[]"></textarea>
                            </div>
                        </div>

                    </div>`
        $('#clsAddRow_collapsetsnnCacLoaiDatKhac').before(rowtsnndatkhac)
    } else {

    }
}
//Xóa form nhà ở tài sản nước ngoài
function clearformtsnnNhaO(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemnhao--;
    if (tsnndemnhao == 0) {
        tsnndemnhao = 1;
        var rowtsnnnhao = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnNhaO(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiNhaO" name="tsnnDiaChiNhaO[]">
                                    <input type="text" class="form-control" name="tsnnMa_NhaO[]" hidden>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Loại nhà</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnLoaiNhaO" name="tsnnLoaiNhaO[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Diện tích sử dụng</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichNhaO" name="tsnnDienTichNhaO[]">
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị của nhà ở" id="tsnnGiaTriNhaO" name="tsnnGiaTriNhaO[]">
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giấy chứng nhận quyền sử dụng</label>
                                <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanNhaO" name="tsnnGiayChungNhanNhaO[]"></textarea>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Thông tin khác (nếu có)</label>
                                <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinNhaOKhacNeuCo" name="tsnnThongTinNhaOKhacNeuCo[]"></textarea>
                            </div>
                        </div>

                    </div> `
        $('#clsAddRow_collapsetsnnNhaO').before(rowtsnnnhao)
    } else {

    }
}
//Xóa form xây dựng khác tài sản nước ngoài
function clearformtsnnCongTrinh(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemxaydungkhac--;
    if (tsnndemxaydungkhac == 0) {
        tsnndemxaydungkhac = 1;
        var rowtsnnxaydungkhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCongTrinh(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên công trình</label>
                                <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnTenCongTrinh" name="tsnnTenCongTrinh[]">
                                    <input type="text" class="form-control" hidden name="tsnnMa_CongTrinh[]">
                            </div>
                        </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Địa chỉ</label>
                            <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiCongTrinh" name="tsnnDiaChiCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Loại công trình</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnLoaiCongTrinh" name="tsnnLoaiCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Cấp công trình</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnCapCongTrinh" name="tsnnCapCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Diện tích</label>
                            <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichCongTrinh" name="tsnnDienTichCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị của công trình" id="tsnnGiaTriCongTrinh" name="tsnnGiaTriCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Giấy chứng nhận quyền sử dụng</label>
                            <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanCongTrinh" name="tsnnGiayChungNhanCongTrinh[]"></textarea>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Thông tin khác (nếu có)</label>
                            <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinCongTrinhKhacNeuCo" name="tsnnThongTinCongTrinhKhacNeuCo[]"></textarea>
                        </div>
                    </div>

                </div> `
        $('#clsAddRow_collapsetsnnCongTrinh').before(rowtsnnxaydungkhac)
    } else {

    }
}
//Xóa form caylaunam tài sản nước ngoài
function clearformtsnnCayLauNam(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemcaylaunam--;
    if (tsnndemcaylaunam == 0) {
        tsnndemcaylaunam = 1;
        var rowtsnncaylaunam = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCayLauNam(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Loại cây</label>
                                <input type="text" class="form-control" placeholder="Nhập loại cây" id="tsnnLoaiCay" name="tsnnTenTaiSan[]">
                                <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='cln'>
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng cây" id="tsnnSoLuongCay" name="tsnnSoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập loại đất" id="tsnnGiaTriCay" name="tsnnGiaTriTS[]">
                            </div>
                        </div>
                    </div>`
        $('#clsAddRow_collapsetsnnCayLauNam').before(rowtsnncaylaunam)
    } else {

    }
}
//Xóa form rungsanxuat tài sản nước ngoài
function clearformtsnnRungSanXuat(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemrungsanxuat--;
    if (tsnndemrungsanxuat == 0) {
        tsnndemrungsanxuat = 1;
        var rowtsnnrungsanxuat = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnRungSanXuat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Loại rừng</label>
                                <input type="text" class="form-control" placeholder="Nhập loại rừng" id="tsnnLoaiRung" name="tsnnTenTaiSan[]">
                                <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='rsx'>
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Diện tích</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích rừng" id="tsnnDienTichRung" name="tsnnSoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị rừng" id="tsnnGiaTriRung" name="tsnnGiaTriTS[]">
                            </div>
                        </div>
                    </div>`
        $('#clsAddRow_collapsetsnnRungSanXuat').before(rowtsnnrungsanxuat)
    } else {

    }
}
//Xóa form Vật kiến trúc khác gắn liền với đất tài sản nước ngoài
function clearformtsnnKienTruc(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemvktglvd--;
    if (tsnndemvktglvd == 0) {
        tsnndemvktglvd = 1;
        var rowtsnnvktglvd = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnKienTruc(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên gọi</label>
                                <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenKienTrucGanDat" name="tsnnTenTaiSan[]">
                                <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='vkt'>
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng vật kiến trúc" id="tsnnSoLuongKienTruc" name="tsnnSoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị vật kiến trúc" id="tsnnGiaTriVatKienTruc" name="tsnnGiaTriTS[]">
                            </div>
                        </div>
                    </div>`
        $('#clsAddRow_collapsetsnnKienTruc').before(rowtsnnvktglvd)
    } else {

    }
}
//Xóa form vàng kim cương tài sản nước ngoài
function clearformtsnnKimLoaiDaQuy(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemvangkimcuong--;
    if (tsnndemvangkimcuong == 0) {
        tsnndemvangkimcuong = 1;
        var rowtsnnvangkimcuong = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnKimLoaiDaQuy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Tên gọi</label>
                                <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenTrangSuc" name="tsnnTenTrangSuc[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_TrangSuc[]" value=''>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTrangSuc" name="tsnnGiaTriTrangSuc[]">
                            </div>
                        </div>
                    </div>`
        $('#clsAddRow_collapsetsnnKimLoaiDaQuy').before(rowtsnnvangkimcuong)
    } else {

    }
}
//Xóa form tiền tài sản nước ngoài
function clearformtsnnTien(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemtien--;
    if (tsnndemtien == 0) {
        tsnndemtien = 1;
        var rowtsnntien = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                       <div class="clear" onclick="clearformtsnnTien(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Tên gọi</label>
                            <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenLoaiTien" name="tsnnTenLoaiTien[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_Tien[]" value=''>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriLoaiTien" name="tsnnGiaTriLoaiTien[]">
                        </div>
                    </div>
                </div> `
        $('#clsAddRow_collapsetsnnTien').before(rowtsnntien)
    } else {

    }
}
//Xóa form cổ phiếu tài sản nước ngoài
function clearformtsnnCoPhieu(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemcophieu--;
    if (tsnndemcophieu == 0) {
        tsnndemcophieu = 1;
        var rowtsnncophieu = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCoPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên cố phiếu</label>
                                <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                    <input hidden value="CoPhieu" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                        <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                            </div>
                        </div>
                     </div> `
        $('#clsAddRow_collapsetsnnCoPhieu').before(rowtsnncophieu)
    } else {

    }
}
//Xóa form trái phiếu tài sản nước ngoài
function clearformtsnnTraiPhieu(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemtraiphieu--;
    if (tsnndemtraiphieu == 0) {
        tsnndemtraiphieu = 1;
        var rowtsnntraiphieu = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnTraiPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên trái phiếu</label>
                                <input type="text" class="form-control" placeholder="Nhập tên trái phiếu" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                <input hidden value="TraiPhieu" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]">
                            </div>
                        </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Số lượng</label>
                            <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                        </div>
                    </div>
                </div> `
        $('#clsAddRow_collapsetsnnTraiPhieu').before(rowtsnntraiphieu)
    } else {
     
    }
}
//Xóa form vốn gốp tài sản nước ngoài
function clearformtsnnGopVon(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemvongop--;
    if (tsnndemvongop == 0) {
        tsnndemvongop = 1;
        var rowtsnnvongop = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnGopVon(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-8">
                            <div class="form-group">
                                <label>Hình thức góp vốn</label>
                                <input type="text" class="form-control" placeholder="Nhập tên hình thức góp vốn" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                <input hidden value="VonGop" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                <input hidden value="0" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                            </div>
                        </div>
                    </div> `
        $('#clsAddRow_collapsetsnnGopVon').before(rowtsnnvongop)
    } else {

    }
}
//Xóa form giấy tờ có giá trị tài sản nước ngoài
function clearformtsnnGiayToKhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemgiaytokhac--;
    if (tsnndemgiaytokhac == 0) {
        tsnndemgiaytokhac = 1;
        var rowtsnngiaytokhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnGiayToKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-8">
                            <div class="form-group">
                                <label>Tên giấy tờ có giá</label>
                                <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                <input hidden value="GiayTo" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                <input hidden value="0" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                            </div>
                        </div>
                    </div>`
        $('#clsAddRow_collapsetsnnGiayToKhac').before(rowtsnngiaytokhac)
    } else {

    }
}
//Xóa form tài sản theo quy định tài sản nước ngoài
function clearformtsnnGiayDangKy(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemtaisantheoquydinh--;
    if (tsnndemtaisantheoquydinh == 0) {
        tsnndemtaisantheoquydinh = 1;
        var rowtsnntaisantheoquydinh = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnGiayDangKy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="tsnnTenTaiSanKhac" name="tsnnTenTaiSanKhac[]">
                                <input hidden value="GiayDangKy" id="tsnnLoaiTaiSanKhac" name="tsnnLoaiTaiSanKhac">
                                <input hidden value="" id="tsnnNamBatDauSoHuuTaiSanKhac" name="tsnnNamBatDauSoHuuTaiSanKhac[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số đăng ký</label>
                                <input type="text" class="form-control" placeholder="Nhập số đăng ký" id="tsnnSoDangKyTaiSanKhac" name="tsnnSoDangKyTaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTaiSanKhac" name="tsnnGiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div> `
        $('#clsAddRow_collapsetsnnGiayDangKy').before(rowtsnntaisantheoquydinh)
    } else {

    }
}
//Xóa form tài sản khác tài sản nước ngoài
function clearformtsnnTaiSanKhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemtaisankhac--;
    if (tsnndemtaisankhac == 0) {
        tsnndemtaisankhac = 1;
        var rowtsnntaisankhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnTaiSanKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="tsnnTenTaiSanKhac" name="tsnnTenTaiSanKhac[]">
                                <input hidden value="TaiSanKhac" id="tsnnLoaiTaiSanKhac" name="tsnnLoaiTaiSanKhac">
                                <input hidden value="" id="tsnnSoDangKyTaiSanKhac" name="tsnnSoDangKyTaiSanKhac[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Năm bắt đầu sở hữu</label>
                                <input type="text" class="form-control" placeholder="Nhập năm bắt đầu sỡ hữu" id="tsnnNamBatDauSoHuuTaiSanKhac" name="tsnnNamBatDauSoHuuTaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTaiSanKhac" name="tsnnGiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div>`
        $('#clsAddRow_collapsetsnnTaiSanKhac').before(rowtsnntaisankhac)
    } else {

    }
}
// kết thúc xóa form

function LoadData() {
    var s1 = window.location.href.split("/");
    var idd = s1[s1.length - 1];

 
    $.get("/NV_KeKhai_TSTN/GetLoaiKeKhai/", { MaKeHoachKeKhai: $('#MaKeHoachKeKhai').val() }, (data) => {
        if (data.Ma_Loai_KeKhai == 3) {
            $('#TenBanKeKhai').text("BẢN KÊ KHAI TÀI SẢN, THU NHẬP LẦN ĐẦU")
        }
        else if (data.Ma_Loai_KeKhai == 4) {
            $('#TenBanKeKhai').text("BẢN KÊ KHAI TÀI SẢN, THU NHẬP HẰNG NĂM")
        }
        else if (data.Ma_Loai_KeKhai == 5) {
            $('#TenBanKeKhai').text("BẢN KÊ KHAI TÀI SẢN, THU NHẬP BỔ SUNG")
        }
        else {
            $('#TenBanKeKhai').text("BẢN KÊ KHAI TÀI SẢN, THU NHẬP PHỤC VỤ CÔNG TÁC CÁN BỘ")
        }

        $("#Ma_Loai_KeKhai").val(data.Ma_Loai_KeKhai)
    })


    $.get("/NV_KeKhai_TSTN/GetSuaThongTinNguoiKeKhai/", { id: idd }, (data) => {
        $("#HoTen").val(data.nguoikekhai.HoTen)
        $("#DiaChiThuongTru").val(data.nguoikekhai.DiaChiThuongTru)

        $("#Ma_ChucVu_ChucDanh").val(data.nguoikekhai.Ma_ChucVu_ChucDanh)
        $("#Ma_CoQuan_DonVi").val(data.nguoikekhai.Ma_CoQuan_DonVi)
        $("#Ma_PhuongXa").val(data.nguoikekhai.Ma_PhuongXa)
        $("#Ma_QuanHuyen").val(data.nguoikekhai.Ma_QuanHuyen)
        $("#Ma_TinhThanh").val(data.nguoikekhai.Ma_TinhThanh)

        $("#NoiCap").val(data.nguoikekhai.NoiCap)
        $("#SoCCCD").val(data.nguoikekhai.SoCCCD)
        $("#Ma_CanBo").val(data.nguoikekhai.Ma_CanBo)
        $("#BienDongTaiSan").val(data.bankekhai.BienDongTaiSan)

        var parts1 
        var parts2
        if (data.nguoikekhai.DoB != null) {
            parts1 = data.nguoikekhai.DoB.split("/");
        }

        if (data.nguoikekhai.NgayCap != null) {
            parts2 = data.nguoikekhai.NgayCap.split("/");
        }

        console.log(parts1)
        console.log(parts2)

        

        $('#DoB').val(`${parts1[2]}-${parts1[1]}-${parts1[0]}`)
        $('#NgayCap').val(`${parts2[2]}-${parts2[1]}-${parts2[0]}`)


        $("#Ma_CoQuan_DonVi_edt").val(data.nguoikekhai.Ma_CoQuan_DonVi)
        $('#Ma_CoQuan_DonVi_edt').change()
        console.log(data.nguoikekhai.Ma_ChucVu_ChucDanh)
        $("#Ma_ChucVu_ChucDanh_edt").val(data.nguoikekhai.Ma_ChucVu_ChucDanh)
        $("#Ma_ChucVu_ChucDanh_edt").change()

        $("#Ma_KeKhai_TSTN").val(data.bankekhai.Ma_KeKhai_TSTN)
        $("#MaKeHoachKeKhai").val(data.bankekhai.MaKeHoachKeKhai)
        var flagVoChong = false
        var flagCon = false
        $.each(data.thannhan, (index, value) => {
        //    var row = ` <div class="row tn1" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
        //<div class="col-5">
        //    <div class="form-group">
        //        <label>Họ Và Tên</label>
        //        <input type="text" class="form-control" id="HoTenThanNhan" name="HoTenThanNhan[]" value="${value.HoTenThanNhan}">
        //                            </div>
        //    </div>
        //    <div class="col-2">
        //        <div class="form-group">
        //            <label>Ngày sinh</label>
        //            <input type="text" class="form-control" id="DoBTN"  name="DoBTN[]"  value="${value.DoBTN}">
        //                            </div>
        //        </div>
        //        <div class="col-5">
        //            <div class="form-group">
        //                <label>Địa chỉ</label>
        //                <input type="text" class="form-control" id="DiaChiThuongTruTN" name="DiaChiThuongTruTN[]" value="${value.DiaChiThuongTruTN}">
        //                            </div>
        //            </div>

        //                        <div class="col-3">
        //                            <div class="form-group">
        //                                <label>Số CMND/CCCD</label>
        //                                <input type="text" class="form-control" id="SoCCCDTN" name="SoCCCDTN[]" value="${value.SoCCCDTN}">
        //                            </div>
        //                            </div>

        //                            <div class="col-3">
        //                                <div class="form-group">
        //                                    <label>Ngày Cấp</label>
        //                                    <input type="text" class="form-control" id="NgayCapTN" name="NgayCapTN[]" value="${value.NgayCapTN}">
        //                            </div>
        //                                </div>

        //                                <div class="col-3">
        //                                    <div class="form-group">
        //                                        <label>Nơi Cấp</label>
        //                                        <input type="text" class="form-control" id="NoiCapTN"  name="NoiCapTN[]" value="${value.NoiCapTN}">
        //                            </div>
        //                                    </div>
        //                                    <div class="col-3">
        //                                        <div class="form-group">
        //                                            <label>Thân nhân là</label>
        //                                            <input type="text" class="form-control" id="VaiTroThanNhan" name="VaiTroThanNhan[]" value="${value.VaiTroThanNhan}">

        //                            </div>
        //                                        </div>
        //                                    </div>`
        //    $("#collapseThanNhan").append(row);
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
        //Đất ở
        if (data.dato.length == 0) {
            $("#clsAddRow_collapseDat").click();
        } else {
            $.each(data.dato, (index, value) => {
                demdat++;
                var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformDat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ</label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]" value="${value.DiaChi}">
                                    <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Ở">
                                    <input type="text" class="form-control" hidden name="TenLoaiDat[]" value="">
                                    <input type="text" class="form-control" hidden name="Ma_QuyenSuDungDat[]" value="${value.Ma_QuyenSuDungDat}">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích</label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]" value="${value.DienTich}">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]" value="${value.GiaTri}">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng</label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanQuyenSoHuuDatO" name="GiayChungNhanQuyenSoHuuDat[]">${value.GiayChungNhan}</textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)</label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinDatKhacNeuCo" name="ThongTinDatKhacNeuCo[]">${value.ThongTinKhac}</textarea>
                                </div>
                            </div>

                        </div>`
                $('#clsAddRow_collapseDat').before(row)
            })
        }
        

        //Đất khác
        if (data.datkhac.length == 0) {
            console.log("if:", demdatkhac)
            $("#clsAddRow_collapseCacLoaiDatKhac").click();
        } else {
            $.each(data.datkhac, (index, value) => {
                demdatkhac++;
                console.log( "else:", demdatkhac)
                var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCacLoaiDatKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Loại đất</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenLoaiDat" name="TenLoaiDat[]" value="${value.TenLoaiDat}">
                                    <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Khác">
                                    <input type="text" class="form-control" hidden name="Ma_QuyenSuDungDat[]" value="${value.Ma_QuyenSuDungDat}">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Địa chỉ</label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]" value="${value.DiaChi}">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Diện tích</label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]" value="${value.DienTich}">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]" value="${value.GiaTri}">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng</label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanQuyenSoHuuDat" name="GiayChungNhanQuyenSoHuuDat[]">${value.GiayChungNhan}</textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)</label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinDatKhacNeuCo" name="ThongTinDatKhacNeuCo[]">${value.ThongTinKhac}</textarea>
                                </div>
                            </div>

                        </div>`

                $('#clsAddRow_collapseCacLoaiDatKhac').before(row)
            })
        }
        

        //Nhà ở
        if (data.nhao.length == 0) {
            $("#clsAddRow_collapseNhaO").click();
        } else {
            $.each(data.nhao, (index, value) => {
                demnhao++;
                var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformNhaO(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Địa chỉ</label>
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiNhaO" name="DiaChiNhaO[]" value="${value.DiaChi}">
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="Ma_NhaO" name="Ma_NhaO[]" hidden value="${value.Ma_NhaO}">
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Loại nhà</label>
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="LoaiNhaO" name="LoaiNhaO[]" value="${value.LoaiNha}">
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Diện tích sử dụng</label>
                                        <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichNhaO" name="DienTichNhaO[]" value="${value.DienTichSuDung}">
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Giá trị</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị của nhà ở" id="GiaTriNhaO" name="GiaTriNhaO[]" value="${value.GiaTri}">
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Giấy chứng nhận quyền sử dụng</label>
                                        <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanNhaO" name="GiayChungNhanNhaO[]">${value.GiayChungNhan}</textarea>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Thông tin khác (nếu có)</label>
                                        <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinNhaOKhacNeuCo" name="ThongTinNhaOKhacNeuCo[]">${value.ThongTinKhac}</textarea>
                                    </div>
                                </div>

                            </div> `

                $('#clsAddRow_collapseNhaO').before(row)
            })
        }
        

        //công trình xây dựng
        if (data.congtrinhxaydung.length == 0) {
            $("#clsAddRow_collapseCongTrinh").click();
        }
        $.each(data.congtrinhxaydung, (index, value) => {
            demxaydungkhac++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCongTrinh(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Tên công trình</label>
                                        <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenCongTrinh" name="TenCongTrinh[]" value='${value.TenCongTrinh}'>
                                        <input type="text" class="form-control" hidden name="Ma_CongTrinh[]" value='${value.Ma_CongTrinh}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Địa chỉ</label>
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiCongTrinh" name="DiaChiCongTrinh[]" value='${value.DiaChi}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Loại công trình</label>
                                        <input type="text" class="form-control" placeholder="Nhập loại đất" id="LoaiCongTrinh" name="LoaiCongTrinh[]" value='${value.LoaiCongTrinh}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Cấp công trình</label>
                                        <input type="text" class="form-control" placeholder="Nhập loại đất" id="CapCongTrinh" name="CapCongTrinh[]" value='${value.CapCongTrinh}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Diện tích</label>
                                        <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichCongTrinh" name="DienTichCongTrinh[]" value='${value.DienTich}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Giá trị</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị của công trình" id="GiaTriCongTrinh" name="GiaTriCongTrinh[]" value='${value.GiaTri}'>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Giấy chứng nhận quyền sử dụng</label>
                                        <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanCongTrinh" name="GiayChungNhanCongTrinh[]">${value.GiayChungNhan}</textarea>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Thông tin khác (nếu có)</label>
                                        <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinCongTrinhKhacNeuCo" name="ThongTinCongTrinhKhacNeuCo[]">${value.ThongTinKhac}</textarea>
                                    </div>
                                </div>

                            </div> `

            $('#clsAddRow_collapseCongTrinh').before(row)
        })

        //cây lâu năm
        if (data.caylaunam.length == 0) {
            $("#clsAddRow_collapseCayLauNam").click();
        }
        $.each(data.caylaunam, (index, value) => {
            demcaylaunam++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCayLauNam(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại cây</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại cây" id="LoaiCay" name="TenTaiSan[]" value='${value.TenTaiSan}'>
                                    <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='cln'>
                                    <input type="text" class="form-control" hidden id="Ma_TaiSanGanVoiDat" name="Ma_TaiSanGanVoiDat[]" value='${value.Ma_TaiSanGanVoiDat}'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập số lượng cây" id="SoLuongCay" name="SoLuong_DienTich[]" value='${value.SoLuong_DienTich}'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập loại đất" id="GiaTriCay" name="GiaTriTS[]" value='${value.GiaTri}'>
                                </div>
                            </div>
                        </div>`

            $('#clsAddRow_collapseCayLauNam').before(row)
        })

        //rừng sản xuất
        if (data.rungsanxuat.length == 0) {
            $("#clsAddRow_collapseRungSanXuat").click();
        }
        $.each(data.rungsanxuat, (index, value) => {
            demrungsanxuat++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformRungSanXuat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Loại rừng</label>
                                        <input type="text" class="form-control" placeholder="Nhập loại rừng" id="LoaiRung" name="TenTaiSan[]" value='${value.TenTaiSan}'>
                                            <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='rsx'>
                                                <input type="text" class="form-control" hidden name="Ma_TaiSanGanVoiDat[]" value='${value.Ma_TaiSanGanVoiDat}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Diện tích</label>
                                        <input type="text" class="form-control area" placeholder="Nhập diện tích rừng" id="DienTichRung" name="SoLuong_DienTich[]" value='${value.SoLuong_DienTich}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Giá trị</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị rừng" id="GiaTriRung" name="GiaTriTS[]" value='${value.GiaTri}'>
                                    </div>
                                </div>
                            </div>`

            $('#clsAddRow_collapseRungSanXuat').before(row)
        })

        //vật kiến trúc
        if (data.vatkientruc.length == 0) {
            $("#clsAddRow_collapseKienTruc").click();
        }
        $.each(data.vatkientruc, (index, value) => {
            demvktglvd++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformKienTruc(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenKienTrucGanDat" name="TenTaiSan[]" value='${value.TenTaiSan}'>
                                    <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='vkt'>
                                    <input type="text" class="form-control" hidden name="Ma_TaiSanGanVoiDat[]" value='${value.Ma_TaiSanGanVoiDat}'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập số lượng vật kiến trúc" id="SoLuongKienTruc" name="SoLuong_DienTich[]" value='${value.SoLuong_DienTich}'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị vật kiến trúc" id="GiaTriVatKienTruc" name="GiaTriTS[]" value='${value.GiaTri}'>
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseKienTruc').before(row)
        })

        //kim loại đá quý
        if (data.kimloaidaquy.length == 0) {
            $("#clsAddRow_collapseKimLoaiDaQuy").click();
        }
        $.each(data.kimloaidaquy, (index, value) => {
            demvangkimcuong++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformKimLoaiDaQuy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenTrangSuc" name="TenTrangSuc[]" value='${value.TenTrangSuc}'>
                                        <input type="text" class="form-control" hidden name="Ma_TrangSuc[]" value='${value.Ma_TrangSuc}'>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTrangSuc" name="GiaTriTrangSuc[]" value='${value.GiaTri}'>
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseKimLoaiDaQuy').before(row)
        })

        //tiền
        if (data.tien.length == 0) {
            $("#clsAddRow_collapseTien").click();
        }
        $.each(data.tien, (index, value) => {
            demtien++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformTien(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenLoaiTien" name="TenLoaiTien[]" value='${value.TenLoaiTien}'>
                                        <input type="text" class="form-control" hidden name="Ma_Tien[]" value='${value.Ma_Tien}'>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriLoaiTien" name="GiaTriLoaiTien[]" value='${value.GiaTri}'>
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseTien').before(row)
        })

        //cổ phiếu
        if (data.cophieu.length == 0) {
            $("#clsAddRow_collapseCoPhieu").click();
        }
        $.each(data.cophieu, (index, value) => {
            demcophieu++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCoPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Tên cố phiếu</label>
                                        <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="TenPhieu" value="${value.TenPhieu}" name="TenPhieu[]">
                                            <input hidden value="CoPhieu" id="LoaiPhieu" name="LoaiPhieu">
                                                <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]" value="${value.Ma_TaiSanPhieu}">
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Số lượng</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="SoLuongPhieu" value="${value.SoLuong}" name="SoLuongPhieu[]">
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Giá trị</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" value="${value.GiaTri}" name="GiaTriPhieu[]">
                                    </div>
                                </div>
                            </div> `

            $('#collapseCoPhieu').before(row)
        })

        //trái phiếu
        if (data.traiphieu.length == 0) {
            $("#clsAddRow_collapseTraiPhieu").click();
        }
        $.each(data.traiphieu, (index, value) => {
            demtraiphieu++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformTraiPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên cố phiếu</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="TenPhieu" value="${value.TenPhieu}" name="TenPhieu[]">
                                    <input hidden value="TraiPhieu" id="LoaiPhieu" name="LoaiPhieu">
                                    <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]" value="${value.Ma_TaiSanPhieu}">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="SoLuongPhieu" value="${value.SoLuong}" name="SoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" value="${value.GiaTri}" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#collapseTraiPhieu').before(row)
        })

        //góp vốn
        if (data.vongop.length == 0) {
            $("#clsAddRow_collapseGopVon").click();
        }
        $.each(data.vongop, (index, value) => {
            demvongop++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; position: relative; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformGopVon(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-8">
                                <div class="form-group">
                                    <label>Hình thức góp vốn</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên hình thức góp vốn" id="TenPhieu" value="${value.TenPhieu}" name="TenPhieu[]">
                                    <input hidden value="VonGop" id="LoaiPhieu" name="LoaiPhieu">
                                    <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                    <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]" value="${value.Ma_TaiSanPhieu}">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" value="${value.GiaTri}" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div>`

            $('#collapseGopVon').before(row)
        })

        //giấy tờ khác
        if (data.giayto.length == 0) {
            $("#clsAddRow_collapseGiayToKhac").click();
        }
        $.each(data.giayto, (index, value) => {
            demgiaytokhac++;
            var row = `    <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformGiayToKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                                <div class="col-8">
                                    <div class="form-group">
                                        <label>Tên giấy tờ có giá</label>
                                        <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="TenPhieu" value="${value.TenPhieu}" name="TenPhieu[]">
                                        <input hidden value="GiayTo" id="LoaiPhieu" name="LoaiPhieu">
                                        <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                        <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]" value="${value.Ma_TaiSanPhieu}">
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Giá trị</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" value="${value.GiaTri}" name="GiaTriPhieu[]">
                                    </div>
                                </div>
                            </div>`

            $('#collapseGiayToKhac').before(row)
        })

        //tài sản khác
        if (data.taisankhac.length == 0) {
            $("#clsAddRow_collapseTaiSanKhac").click();
        }
        $.each(data.taisankhac, (index, value) => {
            demtaisankhac++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformTaiSanKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên tài sản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanKhac" value="${value.TenTaiSan}" name="TenTaiSanKhac[]">
                                    <input hidden value="TaiSanKhac" id="LoaiTaiSanKhac" name="LoaiTaiSanKhac">
                                    <input hidden value="" id="SoDangKyTaiSanKhac" name="SoDangKyTaiSanKhac[]">
                                    <input type="text" class="form-control" hidden name="Ma_TaiSanKhac[]" value="${value.Ma_TaiSanKhac}">

                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Năm bắt đầu sở hữu</label>
                                    <input type="text" class="form-control" placeholder="Nhập năm bắt đầu sỡ hữu" value="${value.SoDangKy_NamBatDauSuDung}" id="NamBatDauSoHuuTaiSanKhac" name="NamBatDauSoHuuTaiSanKhac[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" value="${value.GiaTri}" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
                                </div>
                            </div>
                        </div> `

            $('#collapseTaiSanKhac').before(row)
        })

        //giấy đăng kí
        if (data.giaydangky.length == 0) {
            $("#clsAddRow_collapseGiayDangKy").click();
        }
        $.each(data.giaydangky, (index, value) => {
            demtaisantheoquydinh++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformGiayDangKy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên tài sản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanKhac" value="${value.TenTaiSan}" name="TenTaiSanKhac[]">
                                    <input hidden value="GiayDangKy" id="LoaiTaiSanKhac" name="LoaiTaiSanKhac">
                                    <input hidden value="" id="NamBatDauSoHuuTaiSanKhac" name="NamBatDauSoHuuTaiSanKhac[]">
                                    <input type="text" class="form-control" hidden name="Ma_TaiSanKhac[]" value="${value.Ma_TaiSanKhac}">

                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số đăng ký</label>
                                    <input type="text" class="form-control" placeholder="Nhập số đăng ký" value="${value.SoDangKy_NamBatDauSuDung}" id="SoDangKyTaiSanKhac" name="SoDangKyTaiSanKhac[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" value="${value.GiaTri}" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
                                </div>
                            </div>
                        </div> `

            $('#collapseGiayDangKy').before(row)
        })

        //tài sản nước ngoài
        if (data.taisannuocngoai.length == 0) {
            $("#clsAddRow_collapseTaiSanNuocNgoai").click();
        }
  //      $.each(data.taisannuocngoai, (index, value) => {

  //          var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
  //                          <div class="clear" onclick="clearformTaiSanNuocNgoai(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

  //                          <div class="col-6">
  //                              <div class="form-group">
  //                                  <label>Tên tài sản</label>
  //                                  <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanNuocNgoai" value="${value.TenTaiSanNuocNgoai}" name="TenTaiSanNuocNgoai[]">
  //                                      <input type="text" class="form-control" hidden name="Ma_TaiSanNuocNgoai[]" value="${value.Ma_TaiSanNuocNgoai}" >
  //                              </div>
  //                          </div>
  //                          <div class="col-6">
  //                              <div class="form-group">
  //                                  <label>Giá trị</label>
  //                                  <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriTaiSanNuocNgoai" value="${value.GiaTri}" name="GiaTriTaiSanNuocNgoai[]">
  //                              </div>
  //                          </div>
  //                      </div>
  //`

  //          $('#collapseTaiSanNuocNgoai').before(row)
  //      })


        //Tài sản nước ngoài edit
        //Đất ở
        if (data.tsnndato.length == 0) {
            $("#clsAddRow_collapsetsnnDat").click();
            console.log("begin: ", tsnndemdat)
        }
        $.each(data.tsnndato, (index, value) => {
            tsnndemdat++;
            console.log("each: ", tsnndemdat)
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnDat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ</label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiDat" name="tsnnDiaChiDat[]" value="${value.DiaChi_TSNN}">
                                    <input type="text" class="form-control" hidden name="tsnnLoaiDat[]" value="Đất Ở">
                                    <input type="text" class="form-control" hidden name="tsnnTenLoaiDat[]" value="">
                                    <input type="text" class="form-control" hidden name="tsnnMa_QuyenSuDungDat[]" value="${value.Ma_QuyenSuDungDat_TSNN}">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích</label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="tsnnDienTichDat[]" value="${value.DienTich_TSNN}">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="tsnnGiaTriDat" name="tsnnGiaTriDat[]" value="${value.GiaTri_TSNN}">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng</label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanQuyenSoHuuDat" name="tsnnGiayChungNhanQuyenSoHuuDat[]">${value.GiayChungNhan_TSNN}</textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)</label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinDatKhacNeuCo" name="tsnnThongTinDatKhacNeuCo[]">${value.ThongTinKhac_TSNN}</textarea>
                                </div>
                            </div>

                        </div>`
            $('#clsAddRow_collapsetsnnDat').before(row)
        })

        //Đất khác
        if (data.tsnndatkhac.length == 0) {
            $("#clsAddRow_collapsetsnnCacLoaiDatKhac").click();
        }
        $.each(data.tsnndatkhac, (index, value) => {
            tsnndemdatkhac++;
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCacLoaiDatKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Loại đất</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnTenLoaiDat" name="tsnnTenLoaiDat[]" value="${value.TenLoaiDat_TSNN}">
                                    <input type="text" class="form-control" hidden name="tsnnLoaiDat[]" value="Đất Khác">
                                    <input type="text" class="form-control" hidden name="tsnnMa_QuyenSuDungDat[]" value="${value.Ma_QuyenSuDungDat_TSNN}">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Địa chỉ</label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiDat" name="tsnnDiaChiDat[]" value="${value.DiaChi_TSNN}">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Diện tích</label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichDat" name="tsnnDienTichDat[]" value="${value.DienTich_TSNN}">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="tsnnGiaTriDat" name="tsnnGiaTriDat[]" value="${value.GiaTri_TSNN}">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng</label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanQuyenSoHuuDat" name="tsnnGiayChungNhanQuyenSoHuuDat[]">${value.GiayChungNhan_TSNN}</textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)</label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinDatKhacNeuCo" name="tsnnThongTinDatKhacNeuCo[]">${value.ThongTinKhac_TSNN}</textarea>
                                </div>
                            </div>

                        </div>`

            $('#clsAddRow_collapsetsnnCacLoaiDatKhac').before(row)
        })

        //Nhà ở
        if (data.tsnnnhao.length == 0) {
            $("#clsAddRow_collapsetsnnNhaO").click();
        }
        $.each(data.tsnnnhao, (index, value) => {
            tsnndemnhao++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnNhaO(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Địa chỉ</label>
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiNhaO" name="tsnnDiaChiNhaO[]" value="${value.DiaChi_TSNN}">
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="Ma_NhaO" name="tsnnMa_NhaO[]" hidden value="${value.Ma_NhaO_TSNN}">
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Loại nhà</label>
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnLoaiNhaO" name="tsnnLoaiNhaO[]" value="${value.LoaiNha_TSNN}">
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Diện tích sử dụng</label>
                                        <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichNhaO" name="tsnnDienTichNhaO[]" value="${value.DienTichSuDung_TSNN}">
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label>Giá trị</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị của nhà ở" id="tsnnGiaTriNhaO" name="tsnnGiaTriNhaO[]" value="${value.GiaTri_TSNN}">
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Giấy chứng nhận quyền sử dụng</label>
                                        <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanNhaO" name="tsnnGiayChungNhanNhaO[]">${value.GiayChungNhan_TSNN}</textarea>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Thông tin khác (nếu có)</label>
                                        <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinNhaOKhacNeuCo" name="tsnnThongTinNhaOKhacNeuCo[]">${value.ThongTinKhac_TSNN}</textarea>
                                    </div>
                                </div>

                            </div> `

            $('#clsAddRow_collapsetsnnNhaO').before(row)
        })

        //công trình xây dựng
        if (data.tsnncongtrinhxaydung.length == 0) {
            $("#clsAddRow_collapsetsnnCongTrinh").click();
        }
        $.each(data.tsnncongtrinhxaydung, (index, value) => {
            tsnndemxaydungkhac++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCongTrinh(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Tên công trình</label>
                                        <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnTenCongTrinh" name="tsnnTenCongTrinh[]" value='${value.TenCongTrinh_TSNN}'>
                                        <input type="text" class="form-control" hidden name="tsnnMa_CongTrinh[]" value='${value.Ma_CongTrinh_TSNN}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Địa chỉ</label>
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiCongTrinh" name="tsnnDiaChiCongTrinh[]" value='${value.DiaChi_TSNN}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Loại công trình</label>
                                        <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnLoaiCongTrinh" name="tsnnLoaiCongTrinh[]" value='${value.LoaiCongTrinh_TSNN}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Cấp công trình</label>
                                        <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnCapCongTrinh" name="tsnnCapCongTrinh[]" value='${value.CapCongTrinh_TSNN}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Diện tích</label>
                                        <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichCongTrinh" name="tsnnDienTichCongTrinh[]" value='${value.DienTich_TSNN}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Giá trị</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị của công trình" id="tsnnGiaTriCongTrinh" name="tsnnGiaTriCongTrinh[]" value='${value.GiaTri_TSNN}'>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Giấy chứng nhận quyền sử dụng</label>
                                        <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanCongTrinh" name="tsnnGiayChungNhanCongTrinh[]">${value.GiayChungNhan_TSNN}</textarea>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Thông tin khác (nếu có)</label>
                                        <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinCongTrinhKhacNeuCo" name="tsnnThongTinCongTrinhKhacNeuCo[]">${value.ThongTinKhac_TSNN}</textarea>
                                    </div>
                                </div>

                            </div> `

            $('#clsAddRow_collapsetsnnCongTrinh').before(row)
        })

        //cây lâu năm
        if (data.tsnncaylaunam.length == 0) {
            $("#clsAddRow_collapsetsnnCayLauNam").click();
        }
        $.each(data.tsnncaylaunam, (index, value) => {
            tsnndemcaylaunam++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCayLauNam(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại cây</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại cây" id="tsnnLoaiCay" name="tsnnTenTaiSan[]" value='${value.TenTaiSan_TSNN}'>
                                    <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='cln'>
                                    <input type="text" class="form-control" hidden id="tsnnMa_TaiSanGanVoiDat" name="tsnnMa_TaiSanGanVoiDat[]" value='${value.Ma_TaiSanGanVoiDat_TSNN}'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập số lượng cây" id="tsnnSoLuongCay" name="tsnnSoLuong_DienTich[]" value='${value.SoLuong_DienTich_TSNN}'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập loại đất" id="tsnnGiaTriCay" name="tsnnGiaTriTS[]" value='${value.GiaTri_TSNN}'>
                                </div>
                            </div>
                        </div>`

            $('#clsAddRow_collapsetsnnCayLauNam').before(row)
        })

        //rừng sản xuất
        if (data.tsnnrungsanxuat.length == 0) {
            $("#clsAddRow_collapsetsnnRungSanXuat").click();
        }
        $.each(data.tsnnrungsanxuat, (index, value) => {
            tsnndemrungsanxuat++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnRungSanXuat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Loại rừng</label>
                                        <input type="text" class="form-control" placeholder="Nhập loại rừng" id="tsnnLoaiRung" name="tsnnTenTaiSan[]" value='${value.TenTaiSan_TSNN}'>
                                            <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='rsx'>
                                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanGanVoiDat[]" value='${value.Ma_TaiSanGanVoiDat_TSNN}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Diện tích</label>
                                        <input type="text" class="form-control area" placeholder="Nhập diện tích rừng" id="tsnnDienTichRung" name="tsnnSoLuong_DienTich[]" value='${value.SoLuong_DienTich_TSNN}'>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Giá trị</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị rừng" id="tsnnGiaTriRung" name="tsnnGiaTriTS[]" value='${value.GiaTri_TSNN}'>
                                    </div>
                                </div>
                            </div>`

            $('#clsAddRow_collapsetsnnRungSanXuat').before(row)
        })

        //vật kiến trúc
        if (data.tsnnvatkientruc.length == 0) {
            $("#clsAddRow_collapsetsnnKienTruc").click();
        }
        $.each(data.tsnnvatkientruc, (index, value) => {
            tsnndemvktglvd++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnKienTruc(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenKienTrucGanDat" name="tsnnTenTaiSan[]" value='${value.TenTaiSan_TSNN}'>
                                    <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='vkt'>
                                    <input type="text" class="form-control" hidden name="tsnnMa_TaiSanGanVoiDat[]" value='${value.Ma_TaiSanGanVoiDat_TSNN}'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập số lượng vật kiến trúc" id="tsnnSoLuongKienTruc" name="tsnnSoLuong_DienTich[]" value='${value.SoLuong_DienTich_TSNN}'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị vật kiến trúc" id="tsnnGiaTriVatKienTruc" name="tsnnGiaTriTS[]" value='${value.GiaTri_TSNN}'>
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnKienTruc').before(row)
        })

        //kim loại đá quý
        if (data.tsnnkimloaidaquy.length == 0) {
            $("#clsAddRow_collapsetsnnKimLoaiDaQuy").click();
        }
        $.each(data.tsnnkimloaidaquy, (index, value) => {
            tsnndemvangkimcuong++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnKimLoaiDaQuy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenTrangSuc" name="tsnnTenTrangSuc[]" value='${value.TenTrangSuc_TSNN}'>
                                        <input type="text" class="form-control" hidden name="tsnnMa_TrangSuc[]" value='${value.Ma_TrangSuc_TSNN}'>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTrangSuc" name="tsnnGiaTriTrangSuc[]" value='${value.GiaTri_TSNN}'>
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnKimLoaiDaQuy').before(row)
        })

        //tiền
        if (data.tsnntien.length == 0) {
            $("#clsAddRow_collapsetsnnTien").click();
        }
        $.each(data.tsnntien, (index, value) => {
            tsnndemtien++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnTien(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenLoaiTien" name="tsnnTenLoaiTien[]" value='${value.TenLoaiTien_TSNN}'>
                                        <input type="text" class="form-control" hidden name="tsnnMa_Tien[]" value='${value.Ma_Tien_TSNN}'>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriLoaiTien" name="tsnnGiaTriLoaiTien[]" value='${value.GiaTri_TSNN}'>
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnTien').before(row)
        })

        //cổ phiếu
        if (data.tsnncophieu.length == 0) {
            $("#clsAddRow_collapsetsnnCoPhieu").click();
        }
        $.each(data.tsnncophieu, (index, value) => {
            tsnndemcophieu++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCoPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Tên cố phiếu</label>
                                        <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="tsnnTenPhieu" value="${value.TenPhieu_TSNN}" name="tsnnTenPhieu[]">
                                            <input hidden value="CoPhieu" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]" value="${value.Ma_TaiSanPhieu_TSNN}">
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Số lượng</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="tsnnSoLuongPhieu" value="${value.SoLuong_TSNN}" name="tsnnSoLuongPhieu[]">
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Giá trị</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" value="${value.GiaTri_TSNN}" name="tsnnGiaTriPhieu[]">
                                    </div>
                                </div>
                            </div> `

            $('#collapsetsnnCoPhieu').before(row)
        })

        //trái phiếu
        if (data.tsnntraiphieu.length == 0) {
            $("#clsAddRow_collapsetsnnTraiPhieu").click();
        }
        $.each(data.tsnntraiphieu, (index, value) => {
            tsnndemtraiphieu++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnTraiPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên cố phiếu</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="tsnnTenPhieu" value="${value.TenPhieu_TSNN}" name="tsnnTenPhieu[]">
                                    <input hidden value="TraiPhieu" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                    <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]" value="${value.Ma_TaiSanPhieu_TSNN}">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="tsnnSoLuongPhieu" value="${value.SoLuong_TSNN}" name="tsnnSoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" value="${value.GiaTri_TSNN}" name="tsnnGiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#collapsetsnnTraiPhieu').before(row)
        })

        //góp vốn
        if (data.tsnnvongop.length == 0) {
            $("#clsAddRow_collapsetsnnGopVon").click();
        }
        $.each(data.tsnnvongop, (index, value) => {
            tsnndemvongop++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; position: relative; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnGopVon(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-8">
                                <div class="form-group">
                                    <label>Hình thức góp vốn</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên hình thức góp vốn" id="tsnnTenPhieu" value="${value.TenPhieu_TSNN}" name="tsnnTenPhieu[]">
                                    <input hidden value="VonGop" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                    <input hidden value="0" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                    <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]" value="${value.Ma_TaiSanPhieu_TSNN}">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" value="${value.GiaTri_TSNN}" name="tsnnGiaTriPhieu[]">
                                </div>
                            </div>
                        </div>`

            $('#collapsetsnnGopVon').before(row)
        })

        //giấy tờ khác
        if (data.tsnngiayto.length == 0) {
            $("#clsAddRow_collapsetsnnGiayToKhac").click();
        }
        $.each(data.tsnngiayto, (index, value) => {
            tsnndemgiaytokhac++;
            var row = `    <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnGiayToKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                                <div class="col-8">
                                    <div class="form-group">
                                        <label>Tên giấy tờ có giá</label>
                                        <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="tsnnTenPhieu" value="${value.TenPhieu_TSNN}" name="tsnnTenPhieu[]">
                                        <input hidden value="GiayTo" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                        <input hidden value="0" id="SoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                        <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]" value="${value.Ma_TaiSanPhieu_TSNN}">
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Giá trị</label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" value="${value.GiaTri_TSNN}" name="tsnnGiaTriPhieu[]">
                                    </div>
                                </div>
                            </div>`

            $('#collapsetsnnGiayToKhac').before(row)
        })

        //tài sản khác
        if (data.tsnntaisankhac.length == 0) {
            $("#clsAddRow_collapsetsnnTaiSanKhac").click();
        }
        $.each(data.tsnntaisankhac, (index, value) => {
            tsnndemtaisankhac++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnTaiSanKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên tài sản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="tsnnTenTaiSanKhac" value="${value.TenTaiSan_TSNN}" name="tsnnTenTaiSanKhac[]">
                                    <input hidden value="TaiSanKhac" id="tsnnLoaiTaiSanKhac" name="tsnnLoaiTaiSanKhac">
                                    <input hidden value="" id="tsnnSoDangKyTaiSanKhac" name="tsnnSoDangKyTaiSanKhac[]">
                                    <input type="text" class="form-control" hidden name="tsnnMa_TaiSanKhac[]" value="${value.Ma_TaiSanKhac_TSNN}">

                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Năm bắt đầu sở hữu</label>
                                    <input type="text" class="form-control" placeholder="Nhập năm bắt đầu sỡ hữu" value="${value.SoDangKy_NamBatDauSuDung_TSNN}" id="tsnnNamBatDauSoHuuTaiSanKhac" name="tsnnNamBatDauSoHuuTaiSanKhac[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" value="${value.GiaTri_TSNN}" id="tsnnGiaTriTaiSanKhac" name="tsnnGiaTriTaiSanKhac[]">
                                </div>
                            </div>
                        </div> `

            $('#collapsetsnnTaiSanKhac').before(row)
        })

        //giấy đăng kí
        if (data.tsnngiaydangky.length == 0) {
            $("#clsAddRow_collapsetsnnGiayDangKy").click();
        }
        $.each(data.tsnngiaydangky, (index, value) => {
            tsnndemtaisantheoquydinh++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformGiayDangKy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên tài sản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="tsnnTenTaiSanKhac" value="${value.TenTaiSan_TSNN}" name="tsnnTenTaiSanKhac[]">
                                    <input hidden value="GiayDangKy" id="tsnnLoaiTaiSanKhac" name="tsnnLoaiTaiSanKhac">
                                    <input hidden value="" id="tsnnNamBatDauSoHuuTaiSanKhac" name="tsnnNamBatDauSoHuuTaiSanKhac[]">
                                    <input type="text" class="form-control" hidden name="tsnnMa_TaiSanKhac[]" value="${value.Ma_TaiSanKhac_TSNN}">

                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số đăng ký</label>
                                    <input type="text" class="form-control" placeholder="Nhập số đăng ký" value="${value.SoDangKy_NamBatDauSuDung_TSNN}" id="tsnnSoDangKyTaiSanKhac" name="tsnnSoDangKyTaiSanKhac[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" value="${value.GiaTri_TSNN}" id="tsnnGiaTriTaiSanKhac" name="tsnnGiaTriTaiSanKhac[]">
                                </div>
                            </div>
                        </div> `

            $('#collapsetsnnGiayDangKy').before(row)
        })

        //End Tài sản nước ngoài edit

        //tài khoản nước ngoài
        if (data.taikhoannuocngoai.length == 0) {
            $("#clsAddRow_collapseTaiKhoanNuocNgoai").click();
        }
        $.each(data.taikhoannuocngoai, (index, value) => {

            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformTaiKhoanNuocNgoai(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên chủ tài khoản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên chủ tài khoản" value="${value.TenChuTaiKhoan}" id="TenTaiKhoanNguocNgoai" name="TenTaiKhoanNuocNgoai[]">
                                        <input type="text" class="form-control" hidden name="Ma_TaiKhoanNuocNgoai[]" value="${value.Ma_TaiKhoanNuocNgoai}" >
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số tài khoản</label>
                                    <input type="text" class="form-control" placeholder="Nhập số tài khoản" id="SoTaiKhoanNuocNgoai" value="${value.SoTaiKhoan}" name="SoTaiKhoanNuocNgoai[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên ngân hàng, chi nhánh ngân hàng, nơi mở tài khoản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenNganHangNuocNgoai" value="${value.TenNganHang}" name="TenNganHangNguocNgoai[]">
                                </div>
                            </div>
                        </div>
  `

            $('#collapseTaiKhoanNuocNgoai').before(row)
        })

        $.each(data.tongthunhap, (index, value) => {

            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                        <div class="col-6">
                            <div class="form-group">
                                <label>Tổng thu nhập người kê khai</label>
                                <input type="text" class="form-control currency" placeholder="Nhập tên chủ tài khoản" value="${value.TongThuNhap_NguoiKeKhai}" id="ThuNhapNguoiKeKhai" name="ThuNhapNguoiKeKhai[]">
                                <input type="text" class="form-control " hidden name="Ma_TongThuNhap[]" value="${value.Ma_TongThuNhap}" >
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Tổng thu nhập của vợ (hoặc chồng)</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số tài khoản" value="${value.TongThuNhap_VoHoacChong}" id="ThuNhapVoHoacChong" name="ThuNhapVoHoacChong[]">
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Tổng thu nhập của con chưa thành niên</label>
                                <input type="text" class="form-control currency" placeholder="Nhập tên" value="${value.TongThuNhap_ConChuaThanhNien}" id="ThuNhapCon" name="ThuNhapCon[]">
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Tổng các khoản thu nhập chung</label>
                                <input type="text" class="form-control" placeholder="Nhập tên" value="${value.TongThuNhap_CacKhoanChung}" id="ThuNhapChung" name="ThuNhapChung[]">
                            </div>
                        </div>
                    </div>

  `

            $('#collapseTongThuNhap').before(row)
        })

        if (data.tongthunhap.length == 0) {

            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
        <div class="col-6">
            <div class="form-group">
                <label>Tổng thu nhập người kê khai</label>
                <input type="text" class="form-control currency" placeholder="Nhập tên chủ tài khoản" id="ThuNhapNguoiKeKhai" name="ThuNhapNguoiKeKhai[]">
                    <input type="text" class="form-control" hidden name="Ma_TongThuNhap[]"  >
            </div>
        </div>
        <div class="col-6">
            <div class="form-group">
                <label>Tổng thu nhập của vợ (hoặc chồng)</label>
                <input type="text" class="form-control currency" placeholder="Nhập số tài khoản" id="ThuNhapVoHoacChong" name="ThuNhapVoHoacChong[]">
                </div>
            </div>
            <div class="col-6">
                <div class="form-group">
                    <label>Tổng thu nhập của con chưa thành niên</label>
                    <input type="text" class="form-control currency" placeholder="Nhập tên" id="ThuNhapCon" name="ThuNhapCon[]">
                </div>
            </div>
            <div class="col-6">
                <div class="form-group">
                    <label>Tổng các khoản thu nhập chung</label>
                    <input type="text" class="form-control currency" placeholder="Nhập tên" id="ThuNhapChung" name="ThuNhapChung[]">
                </div>
            </div>
        </div>

  `
            $('#collapseTongThuNhap').before(row)
        }

        if (data.biendongtaisan == undefined) {
            var row = `<tbody>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>1. Quyền sử dụng đất</label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">1.1. Đất ở</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">1.2. Các loại đất khác</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-md-12" style="height:21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td hidden>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">DatO</textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">DatKhac</textarea>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>2. Nhà ở, công trình xây dựng</label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">2.1. Nhà ở</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">2.2. Công trình xây dụng khác</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-md-12" style="height:21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td hidden>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">NhaOCongTrinh</textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">CongTrinhKhac</textarea>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>3. Tài sản khác gắn liền với đất</label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">3.1. Cây lâu năm</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">3.2. Rừng sản xuất</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">3.3. Vật kiến trúc khác gắn liền với đất</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-md-12" style="height:21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td hidden>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">CayLauNam</textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">Rung</textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">VatKienTruc</textarea>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>4. Vàng, kim cương, bạch kim và kim loại quý, đá quý khác có tổng giá trị từ 50 triệu đồng trở lên</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td hidden>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">VangBacDaQuy</textarea>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>5. Tiền (tiền Việt Nam, ngoại tệ) gồm tiền mặt, tiền cho vay, tiền trả trước, tiền gửi cá nhân, tổ chức trong nước, tổ chức nước ngoài tại Việt Nam mà tổng giá trị quy đổi từ 50 triệu đồng trở lên</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td hidden>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">Tien</textarea>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>6. Cổ phiếu, trái phiếu, vốn góp, các loại giấy tờ có giá khác mà tổng giá trị từ 50 triệu đồng trở lên</label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">6.1. Cổ phiếu</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">6.2. Trái phiếu</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">6.3. Vốn góp</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">6.4. Các loại giấy tờ có giá khác</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align:bottom;">
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align:bottom;">
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align:bottom;">
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td hidden>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">CoPhieu</textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">TraiPhieu</textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">VonGop</textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">GiayToKhac</textarea>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align:bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>7. Tài sản khác có giá trị từ 50 triệu đồng trở lên, bao gồm: </label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">7.1. Tài sản theo quy định của pháp luật phải đăng ký sử dụng và được cấp giấy đăng ký</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info">7.2. Tài sản khác</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align:bottom;">
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align:bottom;">
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align:bottom;">
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td hidden>
                                <div class="row">
                                    <div class="col-md-12" style="height: 21px;">
                                        <label></label>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 21px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">TaiSanQDPL</textarea>
                                    </div>
                                    <div class="col-md-12">
                                        <label class="label-info" style="height: 15px;"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">TaiSanKhac</textarea>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>8. Tài sản nước ngoài</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td hidden>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">TaiSanNuocNgoai</textarea>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>9. Tổng thu nhập giữa 02 lần kê khai</label>
                                        <textarea class="input-group" name="TenBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="SoLuongBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="GiaTriBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td style="vertical-align: bottom;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="NoiDungBienDongTS[]"></textarea>
                                    </div>
                                </div>
                            </td>
                            <td hidden>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="label-info"></label>
                                        <textarea class="input-group" name="LoaiBienDongTS[]">ThuNhap</textarea>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>`
            $('#BienDongTSTN_tb').append(row);
        }
        else {
            var row = `<tbody>
                            <tr>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>1. Quyền sử dụng đất</label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">1.1. Đất ở</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.dato[0] != undefined) ? data.biendongtaisan.dato[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.dato[0] != undefined) ? data.biendongtaisan.dato[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">1.2. Các loại đất khác</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.datkhac[0] != undefined) ? data.biendongtaisan.datkhac[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.datkhac[0] != undefined) ? data.biendongtaisan.datkhac[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.dato[0] != undefined) ? data.biendongtaisan.dato[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.datkhac[0] != undefined) ? data.biendongtaisan.datkhac[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12" style="height:21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.dato[0] != undefined) ? data.biendongtaisan.dato[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.datkhac[0] != undefined) ? data.biendongtaisan.datkhac[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.dato[0] != undefined) ? data.biendongtaisan.dato[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.datkhac[0] != undefined) ? data.biendongtaisan.datkhac[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td hidden>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">DatO</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">DatKhac</textarea>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>2. Nhà ở, công trình xây dựng</label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">2.1. Nhà ở</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.nhaocongtrinh[0] != undefined) ? data.biendongtaisan.nhaocongtrinh[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.nhaocongtrinh[0] != undefined) ? data.biendongtaisan.nhaocongtrinh[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">2.2. Công trình xây dựng khác</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.congtrinhkhac[0] != undefined) ? data.biendongtaisan.congtrinhkhac[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.congtrinhkhac[0] != undefined) ? data.biendongtaisan.congtrinhkhac[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.nhaocongtrinh[0] != undefined) ? data.biendongtaisan.nhaocongtrinh[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.congtrinhkhac[0] != undefined) ? data.biendongtaisan.congtrinhkhac[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12" style="height:21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.nhaocongtrinh[0] != undefined) ? data.biendongtaisan.nhaocongtrinh[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.congtrinhkhac[0] != undefined) ? data.biendongtaisan.congtrinhkhac[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.nhaocongtrinh[0] != undefined) ? data.biendongtaisan.nhaocongtrinh[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.congtrinhkhac[0] != undefined) ? data.biendongtaisan.congtrinhkhac[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td hidden>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">NhaOCongTrinh</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">CongTrinhKhac</textarea>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>3. Tài sản khác gắn liền với đất</label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">3.1. Cây lâu năm</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.caylaunam[0] != undefined) ? data.biendongtaisan.caylaunam[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.caylaunam[0] != undefined) ? data.biendongtaisan.caylaunam[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">3.2. Rừng sản xuất</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.rung[0] != undefined) ? data.biendongtaisan.rung[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.rung[0] != undefined) ? data.biendongtaisan.rung[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">3.3. Vật kiến trúc khác gắn liền với đất</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.vatkientruc[0] != undefined) ? data.biendongtaisan.vatkientruc[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.vatkientruc[0] != undefined) ? data.biendongtaisan.vatkientruc[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.caylaunam[0] != undefined) ? data.biendongtaisan.caylaunam[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.rung[0] != undefined) ? data.biendongtaisan.rung[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.vatkientruc[0] != undefined) ? data.biendongtaisan.vatkientruc[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12" style="height:21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.caylaunam[0] != undefined) ? data.biendongtaisan.caylaunam[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.rung[0] != undefined) ? data.biendongtaisan.rung[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.vatkientruc[0] != undefined) ? data.biendongtaisan.vatkientruc[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.caylaunam[0] != undefined) ? data.biendongtaisan.caylaunam[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.rung[0] != undefined) ? data.biendongtaisan.rung[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.vatkientruc[0] != undefined) ? data.biendongtaisan.vatkientruc[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td hidden>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">CayLauNam</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">Rung</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">VatKienTruc</textarea>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>4. Vàng, kim cương, bạch kim và kim loại quý, đá quý khác có tổng giá trị từ 50 triệu đồng trở lên</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.vangbacdaquy[0] != undefined) ? data.biendongtaisan.vangbacdaquy[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.vangbacdaquy[0] != undefined) ? data.biendongtaisan.vangbacdaquy[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.vangbacdaquy[0] != undefined) ? data.biendongtaisan.vangbacdaquy[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.vangbacdaquy[0] != undefined) ? data.biendongtaisan.vangbacdaquy[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.vangbacdaquy[0] != undefined) ? data.biendongtaisan.vangbacdaquy[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td hidden>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">VangBacDaQuy</textarea>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>5. Tiền (tiền Việt Nam, ngoại tệ) gồm tiền mặt, tiền cho vay, tiền trả trước, tiền gửi cá nhân, tổ chức trong nước, tổ chức nước ngoài tại Việt Nam mà tổng giá trị quy đổi từ 50 triệu đồng trở lên</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.tien[0] != undefined) ? data.biendongtaisan.tien[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.tien[0] != undefined) ? data.biendongtaisan.tien[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.tien[0] != undefined) ? data.biendongtaisan.tien[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.tien[0] != undefined) ? data.biendongtaisan.tien[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.tien[0] != undefined) ? data.biendongtaisan.tien[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td hidden>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">Tien</textarea>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>6. Cổ phiếu, trái phiếu, vốn góp, các loại giấy tờ có giá khác mà tổng giá trị từ 50 triệu đồng trở lên</label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">6.1. Cổ phiếu</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.cophieu[0] != undefined) ? data.biendongtaisan.cophieu[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.cophieu[0] != undefined) ? data.biendongtaisan.cophieu[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">6.2. Trái phiếu</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.traiphieu[0] != undefined) ? data.biendongtaisan.traiphieu[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.traiphieu[0] != undefined) ? data.biendongtaisan.traiphieu[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">6.3. Vốn góp</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.vongop[0] != undefined) ? data.biendongtaisan.vongop[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.vongop[0] != undefined) ? data.biendongtaisan.vongop[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">6.4. Các loại giấy tờ có giá khác</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.giaytokhac[0] != undefined) ? data.biendongtaisan.giaytokhac[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.giaytokhac[0] != undefined) ? data.biendongtaisan.giaytokhac[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align:bottom;">
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.cophieu[0] != undefined) ? data.biendongtaisan.cophieu[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.traiphieu[0] != undefined) ? data.biendongtaisan.traiphieu[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.vongop[0] != undefined) ? data.biendongtaisan.vongop[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.giaytokhac[0] != undefined) ? data.biendongtaisan.giaytokhac[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align:bottom;">
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.cophieu[0] != undefined) ? data.biendongtaisan.cophieu[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.traiphieu[0] != undefined) ? data.biendongtaisan.traiphieu[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.vongop[0] != undefined) ? data.biendongtaisan.vongop[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.giaytokhac[0] != undefined) ? data.biendongtaisan.giaytokhac[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align:bottom;">
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.cophieu[0] != undefined) ? data.biendongtaisan.cophieu[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.traiphieu[0] != undefined) ? data.biendongtaisan.traiphieu[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.vongop[0] != undefined) ? data.biendongtaisan.vongop[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.giaytokhac[0] != undefined) ? data.biendongtaisan.giaytokhac[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td hidden>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">CoPhieu</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">TraiPhieu</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">VonGop</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">GiayToKhac</textarea>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align:bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>7. Tài sản khác có giá trị từ 50 triệu đồng trở lên, bao gồm: </label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">7.1. Tài sản theo quy định của pháp luật phải đăng ký sử dụng và được cấp giấy đăng ký</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.taisanqdpl[0] != undefined) ? data.biendongtaisan.taisanqdpl[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.taisanqdpl[0] != undefined) ? data.biendongtaisan.taisanqdpl[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info">7.2. Tài sản khác</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.taisankhac[0] != undefined) ? data.biendongtaisan.taisankhac[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.taisankhac[0] != undefined) ? data.biendongtaisan.taisankhac[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align:bottom;">
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.taisanqdpl[0] != undefined) ? data.biendongtaisan.taisanqdpl[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.taisankhac[0] != undefined) ? data.biendongtaisan.taisankhac[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align:bottom;">
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.taisanqdpl[0] != undefined) ? data.biendongtaisan.taisanqdpl[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.taisankhac[0] != undefined) ? data.biendongtaisan.taisankhac[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align:bottom;">
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.taisanqdpl[0] != undefined) ? data.biendongtaisan.taisanqdpl[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.taisankhac[0] != undefined) ? data.biendongtaisan.taisankhac[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td hidden>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 21px;">
                                            <label></label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 21px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">TaiSanQDPL</textarea>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="label-info" style="height: 15px;"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">TaiSanKhac</textarea>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>8. Tài sản nước ngoài</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.taisannuocngoai[0] != undefined) ? data.biendongtaisan.taisannuocngoai[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.taisannuocngoai[0] != undefined) ? data.biendongtaisan.taisannuocngoai[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.taisannuocngoai[0] != undefined) ? data.biendongtaisan.taisannuocngoai[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.taisannuocngoai[0] != undefined) ? data.biendongtaisan.taisannuocngoai[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.taisannuocngoai[0] != undefined) ? data.biendongtaisan.taisannuocngoai[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td hidden>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">TaiSanNuocNgoai</textarea>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                                                                                                                                                                                                                                                                                                                                                                                                                                  
                            <tr>
                                <td>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>9. Tổng thu nhập giữa 02 lần kê khai</label>
                                            <textarea class="input-group" name="TenBienDongTS[]">${(data.biendongtaisan.thunhap[0] != undefined) ? data.biendongtaisan.thunhap[0].TenLoaiTSTNJson : ''}</textarea>
                                            <input type='text' value="${(data.biendongtaisan.thunhap[0] != undefined) ? data.biendongtaisan.thunhap[0].Ma_BienDongTaiSan : ''}" name='Ma_BienDongTaiSan[]' hidden />
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="SoLuongBienDongTS[]">${(data.biendongtaisan.thunhap[0] != undefined) ? data.biendongtaisan.thunhap[0].SoLuongTaiSanJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="GiaTriBienDongTS[]">${(data.biendongtaisan.thunhap[0] != undefined) ? data.biendongtaisan.thunhap[0].GiaTriTSTNJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td style="vertical-align: bottom;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="NoiDungBienDongTS[]">${(data.biendongtaisan.thunhap[0] != undefined) ? data.biendongtaisan.thunhap[0].NoiDungGiaiTrinhJson : ''}</textarea>
                                        </div>
                                    </div>
                                </td>
                                <td hidden>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="label-info"></label>
                                            <textarea class="input-group" name="LoaiBienDongTS[]">ThuNhap</textarea>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>`
            $('#BienDongTSTN_tb').append(row);
        }

    })


    $("#clsAddRow_collapseDat").click(() => {
        demdat++;
        var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformDat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ</label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]">
                                    <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Ở">
                                    <input type="text" class="form-control" hidden name="TenLoaiDat[]" value="">
                                    <input type="text" class="form-control" hidden name="Ma_QuyenSuDungDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích</label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng</label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanQuyenSoHuuDatO" name="GiayChungNhanQuyenSoHuuDat[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)</label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinDatKhacNeuCo" name="ThongTinDatKhacNeuCo[]"></textarea>
                                </div>
                            </div>

                        </div>`

        $('#clsAddRow_collapseDat').before(row)
    })

    $("#clsAddRow_collapseXXX").click(() => {
        var row = ` `

        $('#clsAddRow_collapseXXX').before(row)
    })

    $("#clsAddRow_collapseCacLoaiDatKhac").click(() => {
        demdatkhac++;
        console.log( "click: ", demdatkhac)
        var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCacLoaiDatKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                    <div class="col-3">
                        <div class="form-group">
                            <label>Loại đất</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenLoaiDat" name="TenLoaiDat[]">
                                <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Khác">
                                    <input type="text" class="form-control" hidden name="Ma_QuyenSuDungDat[]">
                                </div>
                            </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Diện tích</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]">
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giấy chứng nhận quyền sử dụng</label>
                                <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanQuyenSoHuuDat" name="GiayChungNhanQuyenSoHuuDat[]"></textarea>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Thông tin khác (nếu có)</label>
                                <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinDatKhacNeuCo" name="ThongTinDatKhacNeuCo[]"></textarea>
                            </div>
                        </div>

                    </div>`

        $('#clsAddRow_collapseCacLoaiDatKhac').before(row)
    })
    $("#clsAddRow_collapseNhaO").click(() => {
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformNhaO(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiNhaO" name="DiaChiNhaO[]">
                                    <input type="text" class="form-control" name="Ma_NhaO[]" hidden>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Loại nhà</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="LoaiNhaO" name="LoaiNhaO[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Diện tích sử dụng</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichNhaO" name="DienTichNhaO[]">
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị của nhà ở" id="GiaTriNhaO" name="GiaTriNhaO[]">
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giấy chứng nhận quyền sử dụng</label>
                                <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanNhaO" name="GiayChungNhanNhaO[]"></textarea>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Thông tin khác (nếu có)</label>
                                <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinNhaOKhacNeuCo" name="ThongTinNhaOKhacNeuCo[]"></textarea>
                            </div>
                        </div>

                    </div> `

        $('#clsAddRow_collapseNhaO').before(row)
    })
    $("#clsAddRow_collapseCongTrinh").click(() => {
        demxaydungkhac++;
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCongTrinh(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên công trình</label>
                                <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenCongTrinh" name="TenCongTrinh[]">
                                    <input type="text" class="form-control" hidden name="Ma_CongTrinh[]">
                            </div>
                        </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Địa chỉ</label>
                            <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiCongTrinh" name="DiaChiCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Loại công trình</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="LoaiCongTrinh" name="LoaiCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Cấp công trình</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="CapCongTrinh" name="CapCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Diện tích</label>
                            <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichCongTrinh" name="DienTichCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị của công trình" id="GiaTriCongTrinh" name="GiaTriCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Giấy chứng nhận quyền sử dụng</label>
                            <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanCongTrinh" name="GiayChungNhanCongTrinh[]"></textarea>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Thông tin khác (nếu có)</label>
                            <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinCongTrinhKhacNeuCo" name="ThongTinCongTrinhKhacNeuCo[]"></textarea>
                        </div>
                    </div>

                </div> `

        $('#clsAddRow_collapseCongTrinh').before(row)
    })

    $("#clsAddRow_collapseCayLauNam").click(() => {
        demcaylaunam++;
        var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCayLauNam(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Loại cây</label>
                                <input type="text" class="form-control" placeholder="Nhập loại cây" id="LoaiCay" name="TenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='cln'>
                                        <input type="text" class="form-control" hidden name="Ma_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng cây" id="SoLuongCay" name="SoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập loại đất" id="GiaTriCay" name="GiaTriTS[]">
                            </div>
                        </div>
                    </div>`

        $('#clsAddRow_collapseCayLauNam').before(row)
    })

    $("#clsAddRow_collapseRungSanXuat").click(() => {
        demrungsanxuat++
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformRungSanXuat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Loại rừng</label>
                                <input type="text" class="form-control" placeholder="Nhập loại rừng" id="LoaiRung" name="TenTaiSan[]">
                                <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='rsx'>
                                <input type="text" class="form-control" hidden name="Ma_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Diện tích</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích rừng" id="DienTichRung" name="SoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị rừng" id="GiaTriRung" name="GiaTriTS[]">
                            </div>
                        </div>
                    </div>`

        $('#clsAddRow_collapseRungSanXuat').before(row)
    })

    $("#clsAddRow_collapseKienTruc").click(() => {
        demvktglvd++;
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformKienTruc(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên gọi</label>
                                <input type="text" class="form-control" placeholder="Nhập tên" id="TenKienTrucGanDat" name="TenTaiSan[]">
                                <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='vkt'>
                                <input type="text" class="form-control" hidden name="Ma_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng vật kiến trúc" id="SoLuongKienTruc" name="SoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị vật kiến trúc" id="GiaTriVatKienTruc" name="GiaTriTS[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapseKienTruc').before(row)
    })

    $("#clsAddRow_collapseKimLoaiDaQuy").click(() => {
        demvangkimcuong++;
        var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformKimLoaiDaQuy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Tên gọi</label>
                                <input type="text" class="form-control" placeholder="Nhập tên" id="TenTrangSuc" name="TenTrangSuc[]">
                                <input type="text" class="form-control" hidden name="Ma_TrangSuc[]" value=''>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTrangSuc" name="GiaTriTrangSuc[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapseKimLoaiDaQuy').before(row)
    })


    $("#clsAddRow_collapseTien").click(() => {
        demtien++;
        var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                       <div class="clear" onclick="clearformTien(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Tên gọi</label>
                            <input type="text" class="form-control" placeholder="Nhập tên" id="TenLoaiTien" name="TenLoaiTien[]">
                                <input type="text" class="form-control" hidden name="Ma_Tien[]" value=''>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriLoaiTien" name="GiaTriLoaiTien[]">
                        </div>
                    </div>
                </div> `

        $('#clsAddRow_collapseTien').before(row)
    })

    //Phần Ngôi
    $("#clsAddRow_collapseCoPhieu").click(() => {
        demcophieu++;
        var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformCoPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên cố phiếu</label>
                                <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="TenPhieu" name="TenPhieu[]">
                                    <input hidden value="CoPhieu" id="LoaiPhieu" name="LoaiPhieu">
                                        <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="SoLuongPhieu" name="SoLuongPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                            </div>
                        </div>
                     </div> `

        $('#clsAddRow_collapseCoPhieu').before(row)
    })
    $("#clsAddRow_collapseTraiPhieu").click(() => {
        demtraiphieu++;
        var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformTraiPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên trái phiếu</label>
                                <input type="text" class="form-control" placeholder="Nhập tên trái phiếu" id="TenPhieu" name="TenPhieu[]">
                                <input hidden value="TraiPhieu" id="LoaiPhieu" name="LoaiPhieu">
                                <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]">
                            </div>
                        </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Số lượng</label>
                            <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="SoLuongPhieu" name="SoLuongPhieu[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                        </div>
                    </div>
                </div> `

        $('#clsAddRow_collapseTraiPhieu').before(row)
    })
    $("#clsAddRow_collapseGiayToKhac").click(() => {
        demgiaytokhac++;
        var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformGiayToKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-8">
                            <div class="form-group">
                                <label>Tên giấy tờ có giá</label>
                                <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="TenPhieu" name="TenPhieu[]">
                                <input hidden value="GiayTo" id="LoaiPhieu" name="LoaiPhieu">
                                <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapseGiayToKhac').before(row)
    })

    $("#clsAddRow_collapseGopVon").click(() => {
        demvongop++;
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformGopVon(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-8">
                            <div class="form-group">
                                <label>Hình thức góp vốn</label>
                                <input type="text" class="form-control" placeholder="Nhập tên hình thức góp vốn" id="TenPhieu" name="TenPhieu[]">
                                <input hidden value="VonGop" id="LoaiPhieu" name="LoaiPhieu">
                                <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                <input type="text" class="form-control" hidden name="Ma_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapseGopVon').before(row)
    })
    $("#clsAddRow_collapseGiayDangKy").click(() => {
        demtaisantheoquydinh++;
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformGiayDangKy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanKhac" name="TenTaiSanKhac[]">
                                <input hidden value="GiayDangKy" id="LoaiTaiSanKhac" name="LoaiTaiSanKhac">
                                <input hidden value="" id="NamBatDauSoHuuTaiSanKhac" name="NamBatDauSoHuuTaiSanKhac[]">
                                <input type="text" class="form-control" hidden name="Ma_TaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số đăng ký</label>
                                <input type="text" class="form-control" placeholder="Nhập số đăng ký" id="SoDangKyTaiSanKhac" name="SoDangKyTaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapseGiayDangKy').before(row)
    })
    $("#clsAddRow_collapseTaiSanKhac").click(() => {
        demtaisankhac++;
        var row = `   <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformTaiSanKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanKhac" name="TenTaiSanKhac[]">
                                <input hidden value="TaiSanKhac" id="LoaiTaiSanKhac" name="LoaiTaiSanKhac">
                                <input hidden value="" id="SoDangKyTaiSanKhac" name="SoDangKyTaiSanKhac[]">
                                <input type="text" class="form-control" hidden name="Ma_TaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Năm bắt đầu sở hữu</label>
                                <input type="text" class="form-control" placeholder="Nhập năm bắt đầu sỡ hữu" id="NamBatDauSoHuuTaiSanKhac" name="NamBatDauSoHuuTaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapseTaiSanKhac').before(row)
    })
    //$("#clsAddRow_collapseTaiSanNuocNgoai").click(() => {
       
    //    var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
    //                        <div class="clear" onclick="clearformTaiSanNuocNgoai(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

    //                    <div class="col-6">
    //                        <div class="form-group">
    //                            <label>Tên tài sản</label>
    //                            <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanNuocNgoai" name="TenTaiSanNuocNgoai[]">
    //                                <input type="text" class="form-control" hidden name="Ma_TaiSanNuocNgoai[]" >
    //                            </div>
    //                        </div>
    //                        <div class="col-6">
    //                            <div class="form-group">
    //                                <label>Giá trị</label>
    //                                <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriTaiSanNuocNgoai" name="GiaTriTaiSanNuocNgoai[]">
    //                            </div>
    //                        </div>
    //                    </div> `

    //    $('#clsAddRow_collapseTaiSanNuocNgoai').before(row)
    //})

    //Tài sản nước ngoài edit
    $("#clsAddRow_collapsetsnnDat").click(() => {
        tsnndemdat++;
        console.log("click: ", tsnndemdat)
        var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnDat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ</label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiDat" name="tsnnDiaChiDat[]">
                                    <input type="text" class="form-control" hidden name="tsnnLoaiDat[]" value="Đất Ở">
                                    <input type="text" class="form-control" hidden name="tsnnTenLoaiDat[]" value="">
                                    <input type="text" class="form-control" hidden name="tsnnMa_QuyenSuDungDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích</label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichDat" name="tsnnDienTichDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="tsnnGiaTriDat" name="tsnnGiaTriDat[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng</label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanQuyenSoHuuDat" name="tsnnGiayChungNhanQuyenSoHuuDat[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)</label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinDatKhacNeuCo" name="tsnnThongTinDatKhacNeuCo[]"></textarea>
                                </div>
                            </div>

                        </div>`

        $('#clsAddRow_collapsetsnnDat').before(row)
    })

    $("#clsAddRow_collapseXXX").click(() => {
        var row = ` `

        $('#clsAddRow_collapseXXX').before(row)
    })

    $("#clsAddRow_collapsetsnnCacLoaiDatKhac").click(() => {
        tsnndemdatkhac++;
        var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCacLoaiDatKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                    <div class="col-3">
                        <div class="form-group">
                            <label>Loại đất</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnTenLoaiDat" name="tsnnTenLoaiDat[]">
                                <input type="text" class="form-control" hidden name="tsnnLoaiDat[]" value="Đất Khác">
                                    <input type="text" class="form-control" hidden name="tsnnMa_QuyenSuDungDat[]">
                                </div>
                            </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiDat" name="tsnnDiaChiDat[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Diện tích</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichDat" name="tsnnDienTichDat[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="tsnnGiaTriDat" name="tsnnGiaTriDat[]">
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giấy chứng nhận quyền sử dụng</label>
                                <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanQuyenSoHuuDat" name="tsnnGiayChungNhanQuyenSoHuuDat[]"></textarea>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Thông tin khác (nếu có)</label>
                                <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinDatKhacNeuCo" name="tsnnThongTinDatKhacNeuCo[]"></textarea>
                            </div>
                        </div>

                    </div>`

        $('#clsAddRow_collapsetsnnCacLoaiDatKhac').before(row)
    })
    $("#clsAddRow_collapsetsnnNhaO").click(() => {
        tsnndemnhao++;
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnNhaO(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Địa chỉ</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiNhaO" name="tsnnDiaChiNhaO[]">
                                    <input type="text" class="form-control" name="tsnnMa_NhaO[]" hidden>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Loại nhà</label>
                                <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnLoaiNhaO" name="tsnnLoaiNhaO[]">
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Diện tích sử dụng</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichNhaO" name="tsnnDienTichNhaO[]">
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị của nhà ở" id="tsnnGiaTriNhaO" name="tsnnGiaTriNhaO[]">
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giấy chứng nhận quyền sử dụng</label>
                                <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanNhaO" name="tsnnGiayChungNhanNhaO[]"></textarea>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Thông tin khác (nếu có)</label>
                                <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinNhaOKhacNeuCo" name="tsnnThongTinNhaOKhacNeuCo[]"></textarea>
                            </div>
                        </div>

                    </div> `

        $('#clsAddRow_collapsetsnnNhaO').before(row)
    })
    $("#clsAddRow_collapsetsnnCongTrinh").click(() => {
        tsnndemxaydungkhac++;
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCongTrinh(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên công trình</label>
                                <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnTenCongTrinh" name="tsnnTenCongTrinh[]">
                                    <input type="text" class="form-control" hidden name="tsnnMa_CongTrinh[]">
                            </div>
                        </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Địa chỉ</label>
                            <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiCongTrinh" name="tsnnDiaChiCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Loại công trình</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnLoaiCongTrinh" name="tsnnLoaiCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Cấp công trình</label>
                            <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnCapCongTrinh" name="tsnnCapCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Diện tích</label>
                            <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichCongTrinh" name="tsnnDienTichCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị của công trình" id="tsnnGiaTriCongTrinh" name="tsnnGiaTriCongTrinh[]">
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Giấy chứng nhận quyền sử dụng</label>
                            <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanCongTrinh" name="tsnnGiayChungNhanCongTrinh[]"></textarea>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Thông tin khác (nếu có)</label>
                            <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinCongTrinhKhacNeuCo" name="tsnnThongTinCongTrinhKhacNeuCo[]"></textarea>
                        </div>
                    </div>

                </div> `

        $('#clsAddRow_collapsetsnnCongTrinh').before(row)
    })

    $("#clsAddRow_collapsetsnnCayLauNam").click(() => {
        tsnndemcaylaunam++;
        var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCayLauNam(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Loại cây</label>
                                <input type="text" class="form-control" placeholder="Nhập loại cây" id="tsnnLoaiCay" name="tsnnTenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='cln'>
                                        <input type="text" class="form-control" hidden name="tsnnMa_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng cây" id="tsnnSoLuongCay" name="tsnnSoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập loại đất" id="tsnnGiaTriCay" name="tsnnGiaTriTS[]">
                            </div>
                        </div>
                    </div>`

        $('#clsAddRow_collapsetsnnCayLauNam').before(row)
    })

    $("#clsAddRow_collapsetsnnRungSanXuat").click(() => {
        tsnndemrungsanxuat++
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnRungSanXuat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Loại rừng</label>
                                <input type="text" class="form-control" placeholder="Nhập loại rừng" id="tsnnLoaiRung" name="tsnnTenTaiSan[]">
                                <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='rsx'>
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Diện tích</label>
                                <input type="text" class="form-control area" placeholder="Nhập diện tích rừng" id="tsnnDienTichRung" name="tsnnSoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị rừng" id="tsnnGiaTriRung" name="tsnnGiaTriTS[]">
                            </div>
                        </div>
                    </div>`

        $('#clsAddRow_collapsetsnnRungSanXuat').before(row)
    })

    $("#clsAddRow_collapsetsnnKienTruc").click(() => {
        tsnndemvktglvd++;
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnKienTruc(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên gọi</label>
                                <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenKienTrucGanDat" name="tsnnTenTaiSan[]">
                                <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='vkt'>
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanGanVoiDat[]" value=''>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng vật kiến trúc" id="tsnnSoLuongKienTruc" name="tsnnSoLuong_DienTich[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị vật kiến trúc" id="tsnnGiaTriVatKienTruc" name="tsnnGiaTriTS[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapsetsnnKienTruc').before(row)
    })

    $("#clsAddRow_collapsetsnnKimLoaiDaQuy").click(() => {
        tsnndemvangkimcuong++;
        var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnKimLoaiDaQuy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Tên gọi</label>
                                <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenTrangSuc" name="tsnnTenTrangSuc[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_TrangSuc[]" value=''>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTrangSuc" name="tsnnGiaTriTrangSuc[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapsetsnnKimLoaiDaQuy').before(row)
    })


    $("#clsAddRow_collapsetsnnTien").click(() => {
        tsnndemtien++;
        var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                       <div class="clear" onclick="clearformtsnnTien(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Tên gọi</label>
                            <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenLoaiTien" name="tsnnTenLoaiTien[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_Tien[]" value=''>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriLoaiTien" name="tsnnGiaTriLoaiTien[]">
                        </div>
                    </div>
                </div> `

        $('#clsAddRow_collapsetsnnTien').before(row)
    })

    //Phần Ngôi
    $("#clsAddRow_collapsetsnnCoPhieu").click(() => {
        tsnndemcophieu++;
        var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnCoPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên cố phiếu</label>
                                <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                    <input hidden value="CoPhieu" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                        <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số lượng</label>
                                <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                            </div>
                        </div>
                     </div> `

        $('#clsAddRow_collapsetsnnCoPhieu').before(row)
    })
    $("#clsAddRow_collapsetsnnTraiPhieu").click(() => {
        tsnndemtraiphieu++;
        var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnTraiPhieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên trái phiếu</label>
                                <input type="text" class="form-control" placeholder="Nhập tên trái phiếu" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                <input hidden value="TraiPhieu" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]">
                            </div>
                        </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Số lượng</label>
                            <input type="text" class="form-control currency" placeholder="Nhập số lượng" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Giá trị</label>
                            <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                        </div>
                    </div>
                </div> `

        $('#clsAddRow_collapsetsnnTraiPhieu').before(row)
    })
    $("#clsAddRow_collapsetsnnGiayToKhac").click(() => {
        tsnndemgiaytokhac++;
        var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnGiayToKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-8">
                            <div class="form-group">
                                <label>Tên giấy tờ có giá</label>
                                <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                <input hidden value="GiayTo" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                <input hidden value="0" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapsetsnnGiayToKhac').before(row)
    })

    $("#clsAddRow_collapsetsnnGopVon").click(() => {
        tsnndemvongop++;
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnGopVon(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-8">
                            <div class="form-group">
                                <label>Hình thức góp vốn</label>
                                <input type="text" class="form-control" placeholder="Nhập tên hình thức góp vốn" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                <input hidden value="VonGop" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                <input hidden value="0" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanPhieu[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapsetsnnGopVon').before(row)
    })
    $("#clsAddRow_collapsetsnnGiayDangKy").click(() => {
        tsnndemtaisantheoquydinh++;
        var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnGiayDangKy(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="tsnnTenTaiSanKhac" name="tsnnTenTaiSanKhac[]">
                                <input hidden value="GiayDangKy" id="tsnnLoaiTaiSanKhac" name="tsnnLoaiTaiSanKhac">
                                <input hidden value="" id="tsnnNamBatDauSoHuuTaiSanKhac" name="tsnnNamBatDauSoHuuTaiSanKhac[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số đăng ký</label>
                                <input type="text" class="form-control" placeholder="Nhập số đăng ký" id="tsnnSoDangKyTaiSanKhac" name="tsnnSoDangKyTaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTaiSanKhac" name="tsnnGiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapsetsnnGiayDangKy').before(row)
    })
    $("#clsAddRow_collapsetsnnTaiSanKhac").click(() => {
        tsnndemtaisankhac++;
        var row = `   <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnTaiSanKhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="tsnnTenTaiSanKhac" name="tsnnTenTaiSanKhac[]">
                                <input hidden value="TaiSanKhac" id="tsnnLoaiTaiSanKhac" name="tsnnLoaiTaiSanKhac">
                                <input hidden value="" id="tsnnSoDangKyTaiSanKhac" name="tsnnSoDangKyTaiSanKhac[]">
                                <input type="text" class="form-control" hidden name="tsnnMa_TaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Năm bắt đầu sở hữu</label>
                                <input type="text" class="form-control" placeholder="Nhập năm bắt đầu sỡ hữu" id="tsnnNamBatDauSoHuuTaiSanKhac" name="tsnnNamBatDauSoHuuTaiSanKhac[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Giá trị</label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTaiSanKhac" name="tsnnGiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div> `

        $('#clsAddRow_collapsetsnnTaiSanKhac').before(row)
    })
    //End tài sản nước ngoài
    $("#clsAddRow_collapseTaiKhoanNuocNgoai").click(() => {
        
        var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative;  padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformTaiKhoanNuocNgoai(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>

                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên chủ tài khoản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên chủ tài khoản" id="TenTaiKhoanNguocNgoai" name="TenTaiKhoanNuocNgoai[]">
                                    <input type="text" class="form-control" hidden name="Ma_TaiKhoanNuocNgoai[]" >
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Số tài khoản</label>
                                <input type="text" class="form-control" placeholder="Nhập số tài khoản" id="SoTaiKhoanNuocNgoai" name="SoTaiKhoanNuocNgoai[]">
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên ngân hàng, chi nhánh ngân hàng, nơi mở tài khoản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên" id="TenNganHangNuocNgoai" name="TenNganHangNguocNgoai[]">
                            </div>
                        </div>
                    </div>`

        $('#clsAddRow_collapseTaiKhoanNuocNgoai').before(row)
    })
  
}



function FnSuccess() {
    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Bản kê khai đã được cập nhật thành công`,
        timer: 2000,
        showConfirmButton: false,
    }).then(() => { location.reload() })
}

function FnBegin() {
    $('.btnCreateKeKhaiTaiSan').attr("disabled", true);
}

function Failure(data) {
    $('.loading_newcanbo').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Sửa cán bộ bị lỗi',
        timer: 2000,
        showConfirmButton: false,
    })
    $('.btnCreateKeKhaiTaiSan').attr("disabled", false);
}

$(document).ready(async function () {
    LoadData()
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

   
})





