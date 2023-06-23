const params = new URLSearchParams(window.location.search)
var url = window.location.href.split('/')
var MaKeHoachKeKhai = url[url.length - 1]

var demdat = 1;
var demdatkhac = 1;
var demnhao = 1;
var demnxaydungkhac = 1;
var demncaylaunam = 1;
var demrungsanxuat = 1;
var demvktglvd = 1;
var demvangkimcuong = 1;
var demtien = 1;
var demcophieu = 1;
var demtraiphieu = 1;
var demvongop = 1;
var demgiaytokhac = 1;
var demtaisantheoquydinh = 1;
var demtaisankhac = 1;

//tài sản nước ngoài
var tsnndemdat = 1;
var tsnndemdatkhac = 1;
var tsnndemnhao = 1;
var tsnndemnxaydungkhac = 1;
var tsnndemncaylaunam = 1;
var tsnndemrungsanxuat = 1;
var tsnndemvktglvd = 1;
var tsnndemvangkimcuong = 1;
var tsnndemtien = 1;
var tsnndemcophieu = 1;
var tsnndemtraiphieu = 1;
var tsnndemvongop = 1;
var tsnndemgiaytokhac = 1;
var tsnndemtaisantheoquydinh = 1;
var tsnndemtaisankhac = 1;

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
function clearformdat(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demdat--;
    if (demdat == 0) {
        demdat = 1;
        var rowDat = `
                        <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformdat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ
                                     <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup>
                                    </label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]">
                                    <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Ở">
                                    <input type="text" class="form-control" hidden name="TenLoaiDat[]" value="">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); Nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi ''Chưa được cấp giấy chứng nhận quyền sử dụng đất''. " id="GiayChungNhanQuyenSoHuuDatO" name="GiayChungNhanQuyenSoHuuDat[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Ví dụ: Người kê khai đứng tên đăng kí quyền sử dụng, quyền sở hưu nhưng thực tế là của người khác" id="ThongTinDatKhacNeuCo" name="ThongTinDatKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div>
                        `
        $('#clsAddRow_collapseDat').before(rowDat)
    } else {
        
    }
}
//Xóa form đất khác
function clearformdatkhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demdatkhac--;
    if (demdatkhac == 0) {
        demdatkhac = 1;
        var rowdatkhac = `
                            <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                <div class="clear" onclick="clearformdatkhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Loại đất</label>
                                        <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenLoaiDat" name="TenLoaiDat[]">
                                        <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Khác">
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Địa chỉ <sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (8)
                                                <div class="tooltip-right-contain" style="text-align: justify;">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]">
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                        <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]">
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]">
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                        <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanQuyenSoHuuDat" name="GiayChungNhanQuyenSoHuuDat[]"></textarea>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                        <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinDatKhacNeuCo" name="ThongTinDatKhacNeuCo[]"></textarea>
                                    </div>
                                </div>

                            </div>`
        $('#clsAddRow_collapseCacLoaiDatKhac').before(rowdatkhac)
    } else {

    }
}
//Xóa form nhà ở
function clearformnhao(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demnhao--;
    if (demnhao == 0) {
        demnhao = 1;
        var rownhao = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformnhao(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiNhaO" name="DiaChiNhaO[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Loại nhà<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (14)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi “căn hộ” nếu là căn hộ trong nhà tập thể, chung cư; ghi “nhà ở riêng lẻ” nếu là nhà được xây dựng trên thửa đất riêng biệt.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="LoaiNhaO" name="LoaiNhaO[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Diện tích sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (15)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi tổng diện tích (m2) sàn xây dựng của tất cả các tầng của nhà ở riêng lẻ, biệt thự bao gồm cả các tầng hầm, tầng nửa hầm, tầng kỹ thuật, tầng áp mái và tầng mái tum. Nếu là căn hộ thì diện tích được ghi theo giấy chứng nhận quyền sở hữu hoặc hợp đồng mua, hợp đồng thuê của nhà nước.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichNhaO" name="DienTichNhaO[]">
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của nhà ở" id="GiaTriNhaO" name="GiaTriNhaO[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanNhaO" name="GiayChungNhanNhaO[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinNhaOKhacNeuCo" name="ThongTinNhaOKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapseNhaO').before(rownhao)
    } else {

    }
}
//Xóa form xây dựng khác
function clearformxaydungkhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demnxaydungkhac--;
    if (demnxaydungkhac == 0) {
        demnxaydungkhac = 1;
        var rowxaydungkhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformxaydungkhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên công trình</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenCongTrinh" name="TenCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
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
                                    <label>Diện tích<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (9)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichCongTrinh" name="DienTichCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của công trình" id="GiaTriCongTrinh" name="GiaTriCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanCongTrinh" name="GiayChungNhanCongTrinh[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinCongTrinhKhacNeuCo" name="ThongTinCongTrinhKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapseCongTrinh').before(rowxaydungkhac)
    } else {

    }
}
//Xóa form caylaunam
function clearformcaylaunam(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demncaylaunam--;
    if (demncaylaunam == 0) {
        demncaylaunam = 1;
        var rowcaylaunam = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformcaylaunam(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại cây</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại cây" id="LoaiCay" name="TenTaiSan[]">
                                        <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='cln'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng cây" id="SoLuongCay" name="SoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập loại đất" id="GiaTriCay" name="GiaTriTS[]">
                                </div>
                            </div>
                        </div>`
        $('#clsAddRow_collapseCayLauNam').before(rowcaylaunam)
    } else {

    }
}
//Xóa form rungsanxuat
function clearformrungsanxuat(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demrungsanxuat--;
    if (demrungsanxuat == 0) {
        demrungsanxuat = 1;
        var rowrungsanxuat = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformrungsanxuat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại rừng</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại rừng" id="LoaiRung" name="TenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='rsx'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (9)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích rừng" id="DienTichRung" name="SoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị rừng" id="GiaTriRung" name="GiaTriTS[]">
                                </div>
                            </div>
                        </div>`
        $('#clsAddRow_collapseRungSanXuat').before(rowrungsanxuat)
    } else {

    }
}
//Xóa form Vật kiến trúc khác gắn liền với đất
function clearformvktglvd(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demvktglvd--;
    if (demvktglvd == 0) {
        demvktglvd = 1;
        var rowvktglvd = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                             <div class="clear" onclick="clearformvktglvd(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenKienTrucGanDat" name="TenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='vkt'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng vật kiến trúc" id="SoLuongKienTruc" name="SoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị vật kiến trúc" id="GiaTriVatKienTruc" name="GiaTriTS[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapseKienTruc').before(rowvktglvd)
    } else {

    }
}
//Xóa form Vật kiến trúc khác gắn liền với đất
function clearformvangkimcuong(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demvangkimcuong--;
    if (demvangkimcuong == 0) {
        demvangkimcuong = 1;
        var rowvangkimcuong = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformvangkimcuong(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenTrangSuc" name="TenTrangSuc[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTrangSuc" name="GiaTriTrangSuc[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapseKimLoaiDaQuy').before(rowvangkimcuong)
    } else {

    }
}
//Xóa form tiền
function clearformtien(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demtien--;
    if (demtien == 0) {
        demtien = 1;
        var rowtien = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtien(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenLoaiTien" name="TenLoaiTien[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriLoaiTien" name="GiaTriLoaiTien[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapseTien').before(rowtien)
    } else {

    }
}
//Xóa form cổ phiếu
function clearformcophieu(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demcophieu--;
    if (demcophieu == 0) {
        demcophieu = 1;
        var rowcophieu = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformcophieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên cố phiếu</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="TenPhieu" name="TenPhieu[]">
                                    <input hidden value="CoPhieu" id="LoaiPhieu" name="LoaiPhieu">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapseCoPhieu').before(rowcophieu)
    } else {

    }
}
//Xóa form trái phiếu
function clearformtraiphieu(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demtraiphieu--;
    if (demtraiphieu == 0) {
        demtraiphieu = 1;
        var rowtraiphieu = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtraiphieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên trái phiếu</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên trái phiếu" id="TenPhieu" name="TenPhieu[]">
                                     <input hidden value="TraiPhieu" id="LoaiPhieu" name="LoaiPhieu">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapseTraiPhieu').before(rowtraiphieu)
    } else {

    }
}
//Xóa form vốn gốp
function clearformvongop(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demvongop--;
    if (demvongop == 0) {
        demvongop = 1;
        var rowvongop = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformvongop(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-8">
                                <div class="form-group">
                                    <label>Hình thức góp vốn</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên hình thức góp vốn" id="TenPhieu" name="TenPhieu[]">
                                    <input hidden value="VonGop" id="LoaiPhieu" name="LoaiPhieu">
                                    <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div>`
        $('#clsAddRow_collapseGopVon').before(rowvongop)
    } else {

    }
}
//Xóa form giấy tờ có giá trị
function clearformgiaytokhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demgiaytokhac--;
    if (demgiaytokhac == 0) {
        demgiaytokhac = 1;
        var rowgiaytokhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformgiaytokhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-8">
                                <div class="form-group">
                                    <label>Tên giấy tờ có giá<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (23)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view"    >
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Các loại giấy tờ có giá khác như chứng chỉ quỹ, kỳ phiếu, séc,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="TenPhieu" name="TenPhieu[]">
                                    <input hidden value="GiayTo" id="LoaiPhieu" name="LoaiPhieu">
                                    <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div>  `
        $('#clsAddRow_collapseGiayToKhac').before(rowgiaytokhac)
    } else {

    }
}
//Xóa form tài sản theo quy định
function clearformtaisantheoquydinh(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demtaisantheoquydinh--;
    if (demtaisantheoquydinh == 0) {
        demtaisantheoquydinh = 1;
        var rowtaisantheoquydinh = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtaisantheoquydinh(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên tài sản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanKhac" name="TenTaiSanKhac[]">
                                    <input hidden value="GiayDangKy" id="LoaiTaiSanKhac" name="LoaiTaiSanKhac">
                                    <input hidden value="" id="NamBatDauSoHuuTaiSanKhac" name="NamBatDauSoHuuTaiSanKhac[]">
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
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapseGiayDangKy').before(rowtaisantheoquydinh)
    } else {

    }
}
//Xóa form tài sản khác
function clearformtaisankhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    demtaisankhac--;
    if (demtaisankhac == 0) {
        demtaisankhac = 1;
        var rowtaisankhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                        <div class="clear" onclick="clearformtaisankhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanKhac" name="TenTaiSanKhac[]">
                                <input hidden value="TaiSanKhac" id="LoaiTaiSanKhac" name="LoaiTaiSanKhac">
                                <input hidden value="" id="SoDangKyTaiSanKhac" name="SoDangKyTaiSanKhac[]">
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
                                <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
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
function clearformtsnndat(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemdat--;
    if (tsnndemdat == 0) {
        tsnndemdat = 1;
        var rowtsnnDat = `
                            <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                <div class="clear" onclick="clearformtsnndat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Địa chỉ <sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (8)
                                                <div class="tooltip-right-contain" style="text-align: justify;">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiDat" name="tsnnDiaChiDat[]">
                                        <input type="text" class="form-control" hidden name="tsnnLoaiDat[]" value="Đất Ở">
                                        <input type="text" class="form-control" hidden name="tsnnTenLoaiDat[]" value="">
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                        <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichDat" name="tsnnDienTichDat[]">
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="tsnnGiaTriDat" name="tsnnGiaTriDat[]">
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                        <textarea class="form-control" placeholder="Ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); Nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi ''Chưa được cấp giấy chứng nhận quyền sử dụng đất''. " id="tsnnGiayChungNhanQuyenSoHuuDatO" name="tsnnGiayChungNhanQuyenSoHuuDat[]"></textarea>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                        <textarea class="form-control" placeholder="Ví dụ: Người kê khai đứng tên đăng kí quyền sử dụng, quyền sở hưu nhưng thực tế là của người khác" id="tsnnThongTinDatKhacNeuCo" name="tsnnThongTinDatKhacNeuCo[]"></textarea>
                                    </div>
                                </div>
                            </div>
                        `
        $('#clsAddRow_collapsetsnnDat').before(rowtsnnDat)
    } else {

    }
}
//Xóa form đất khác tài sản nước ngoài
function clearformtsnndatkhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    console.log("SSS")
    tsnndemdatkhac--;
    if (tsnndemdatkhac == 0) {
        tsnndemdatkhac = 1;
        var rowtsnndatkhac = `
                            <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                <div class="clear" onclick="clearformtsnndatkhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Loại đất</label>
                                        <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnTenLoaiDat" name="tsnnTenLoaiDat[]">
                                        <input type="text" class="form-control" hidden name="tsnnLoaiDat[]" value="Đất Khác">
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Địa chỉ <sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (8)
                                                <div class="tooltip-right-contain" style="text-align: justify;">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                        <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiDat" name="tsnnDiaChiDat[]">
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                        <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichDat" name="tsnnDienTichDat[]">
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                        <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="tsnnGiaTriDat" name="tsnnGiaTriDat[]">
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                        <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanQuyenSoHuuDat" name="tsnnGiayChungNhanQuyenSoHuuDat[]"></textarea>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="form-group">
                                        <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                        <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinDatKhacNeuCo" name="tsnnThongTinDatKhacNeuCo[]"></textarea>
                                    </div>
                                </div>

                            </div>`
        $('#clsAddRow_collapsetsnnCacLoaiDatKhac').before(rowtsnndatkhac)
    } else {

    }
}
//Xóa form nhà ở tài sản nước ngoài
function clearformtsnnnhao(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemnhao--;
    if (tsnndemnhao == 0) {
        tsnndemnhao = 1;
        var rowtsnnnhao = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnnhao(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiNhaO" name="tsnnDiaChiNhaO[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Loại nhà<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (14)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi “căn hộ” nếu là căn hộ trong nhà tập thể, chung cư; ghi “nhà ở riêng lẻ” nếu là nhà được xây dựng trên thửa đất riêng biệt.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnLoaiNhaO" name="tsnnLoaiNhaO[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Diện tích sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (15)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi tổng diện tích (m2) sàn xây dựng của tất cả các tầng của nhà ở riêng lẻ, biệt thự bao gồm cả các tầng hầm, tầng nửa hầm, tầng kỹ thuật, tầng áp mái và tầng mái tum. Nếu là căn hộ thì diện tích được ghi theo giấy chứng nhận quyền sở hữu hoặc hợp đồng mua, hợp đồng thuê của nhà nước.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichNhaO" name="tsnnDienTichNhaO[]">
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của nhà ở" id="tsnnGiaTriNhaO" name="tsnnGiaTriNhaO[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanNhaO" name="tsnnGiayChungNhanNhaO[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinNhaOKhacNeuCo" name="tsnnThongTinNhaOKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapsetsnnNhaO').before(rowtsnnnhao)
    } else {

    }
}
//Xóa form xây dựng khác tài sản nước ngoài
function clearformtsnnxaydungkhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemnxaydungkhac--;
    if (tsnndemnxaydungkhac == 0) {
        tsnndemnxaydungkhac = 1;
        var rowtsnnxaydungkhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnxaydungkhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên công trình</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnTenCongTrinh" name="tsnnTenCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
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
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichCongTrinh" name="tsnnDienTichCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của công trình" id="tsnnGiaTriCongTrinh" name="tsnnGiaTriCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanCongTrinh" name="tsnnGiayChungNhanCongTrinh[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinCongTrinhKhacNeuCo" name="tsnnThongTinCongTrinhKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapsetsnnCongTrinh').before(rowtsnnxaydungkhac)
    } else {

    }
}
//Xóa form caylaunam tài sản nước ngoài
function clearformtsnncaylaunam(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemncaylaunam--;
    if (tsnndemncaylaunam == 0) {
        tsnndemncaylaunam = 1;
        var rowtsnncaylaunam = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnncaylaunam(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại cây</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại cây" id="tsnnLoaiCay" name="tsnnTenTaiSan[]">
                                        <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='cln'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng cây" id="tsnnSoLuongCay" name="tsnnSoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập loại đất" id="tsnnGiaTriCay" name="tsnnGiaTriTS[]">
                                </div>
                            </div>
                        </div>`
        $('#clsAddRow_collapsetsnnCayLauNam').before(rowtsnncaylaunam)
    } else {

    }
}
//Xóa form rungsanxuat tài sản nước ngoài
function clearformtsnnrungsanxuat(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemrungsanxuat--;
    if (tsnndemrungsanxuat == 0) {
        tsnndemrungsanxuat = 1;
        var rowtsnnrungsanxuat = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformrungsanxuat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại rừng</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại rừng" id="tsnnLoaiRung" name="tsnnTenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='rsx'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích rừng" id="tsnnDienTichRung" name="tsnnSoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị rừng" id="tsnnGiaTriRung" name="tsnnGiaTriTS[]">
                                </div>
                            </div>
                        </div>`
        $('#clsAddRow_collapsetsnnRungSanXuat').before(rowtsnnrungsanxuat)
    } else {

    }
}
//Xóa form Vật kiến trúc khác gắn liền với đất tài sản nước ngoài
function clearformtsnnvktglvd(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemvktglvd--;
    if (tsnndemvktglvd == 0) {
        tsnndemvktglvd = 1;
        var rowtsnnvktglvd = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                             <div class="clear" onclick="clearformtsnnvktglvd(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenKienTrucGanDat" name="tsnnTenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='vkt'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng vật kiến trúc" id="tsnnSoLuongKienTruc" name="tsnnSoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị vật kiến trúc" id="tsnnGiaTriVatKienTruc" name="tsnnGiaTriTS[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapsetsnnKienTruc').before(rowtsnnvktglvd)
    } else {

    }
}
//Xóa form vàng kim cương tài sản nước ngoài
function clearformtsnnvangkimcuong(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemvangkimcuong--;
    if (tsnndemvangkimcuong == 0) {
        tsnndemvangkimcuong = 1;
        var rowtsnnvangkimcuong = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnvangkimcuong(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenTrangSuc" name="tsnnTenTrangSuc[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTrangSuc" name="tsnnGiaTriTrangSuc[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapsetsnnKimLoaiDaQuy').before(rowtsnnvangkimcuong)
    } else {

    }
}
//Xóa form tiền tài sản nước ngoài
function clearformtsnntien(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemtien--;
    if (tsnndemtien == 0) {
        tsnndemtien = 1;
        var rowtsnntien = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnntien(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenLoaiTien" name="tsnnTenLoaiTien[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriLoaiTien" name="tsnnGiaTriLoaiTien[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapsetsnnTien').before(rowtsnntien)
    } else {

    }
}
//Xóa form cổ phiếu tài sản nước ngoài
function clearformtsnncophieu(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemcophieu--;
    if (tsnndemcophieu == 0) {
        tsnndemcophieu = 1;
        var rowtsnncophieu = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnncophieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên cố phiếu</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                    <input hidden value="CoPhieu" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapsetsnnCoPhieu').before(rowtsnncophieu)
    } else {

    }
}
//Xóa form trái phiếu tài sản nước ngoài
function clearformtsnntraiphieu(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemtraiphieu--;
    if (tsnndemtraiphieu == 0) {
        tsnndemtraiphieu = 1;
        var rowtsnntraiphieu = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnntraiphieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên trái phiếu</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên trái phiếu" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                     <input hidden value="TraiPhieu" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapsetsnnTraiPhieu').before(rowtsnntraiphieu)
    } else {

    }
}
//Xóa form vốn gốp tài sản nước ngoài
function clearformtsnnvongop(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemvongop--;
    if (tsnndemvongop == 0) {
        tsnndemvongop = 1;
        var rowtsnnvongop = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnvongop(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-8">
                                <div class="form-group">
                                    <label>Hình thức góp vốn</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên hình thức góp vốn" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                    <input hidden value="VonGop" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                    <input hidden value="0" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                                </div>
                            </div>
                        </div>`
        $('#clsAddRow_collapsetsnnGopVon').before(rowtsnnvongop)
    } else {

    }
}
//Xóa form giấy tờ có giá trị tài sản nước ngoài
function clearformtsnngiaytokhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemgiaytokhac--;
    if (tsnndemgiaytokhac == 0) {
        tsnndemgiaytokhac = 1;
        var rowtsnngiaytokhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnngiaytokhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-8">
                                <div class="form-group">
                                    <label>Tên giấy tờ có giá<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (23)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view"    >
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Các loại giấy tờ có giá khác như chứng chỉ quỹ, kỳ phiếu, séc,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="TenPhieu" name="tsnnTenPhieu[]">
                                    <input hidden value="GiayTo" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                    <input hidden value="0" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                                </div>
                            </div>
                        </div>  `
        $('#clsAddRow_collapsetsnnGiayToKhac').before(rowtsnngiaytokhac)
    } else {

    }
}
//Xóa form tài sản theo quy định tài sản nước ngoài
function clearformtsnntaisantheoquydinh(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemtaisantheoquydinh--;
    if (tsnndemtaisantheoquydinh == 0) {
        tsnndemtaisantheoquydinh = 1;
        var rowtsnntaisantheoquydinh = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnntaisantheoquydinh(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên tài sản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="tsnnTenTaiSanKhac" name="tsnnTenTaiSanKhac[]">
                                    <input hidden value="GiayDangKy" id="tsnnLoaiTaiSanKhac" name="tsnnLoaiTaiSanKhac">
                                    <input hidden value="" id="tsnnNamBatDauSoHuuTaiSanKhac" name="tsnnNamBatDauSoHuuTaiSanKhac[]">
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
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTaiSanKhac" name="tsnnGiaTriTaiSanKhac[]">
                                </div>
                            </div>
                        </div> `
        $('#clsAddRow_collapsetsnnGiayDangKy').before(rowtsnntaisantheoquydinh)
    } else {

    }
}
//Xóa form tài sản khác tài sản nước ngoài
function clearformtsnntaisankhac(obj) {
    var ele = $(obj);
    ele.parent().remove()
    tsnndemtaisankhac--;
    if (tsnndemtaisankhac == 0) {
        tsnndemtaisankhac = 1;
        var rowtsnntaisankhac = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                        <div class="clear" onclick="clearformtsnntaisankhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="tsnnTenTaiSanKhac" name="tsnnTenTaiSanKhac[]">
                                <input hidden value="TaiSanKhac" id="tsnnLoaiTaiSanKhac" name="tsnnLoaiTaiSanKhac">
                                <input hidden value="" id="tsnnSoDangKyTaiSanKhac" name="tsnnSoDangKyTaiSanKhac[]">
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
                                <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTaiSanKhac" name="tsnnGiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div> `
        $('#clsAddRow_collapsetsnnTaiSanKhac').before(rowtsnntaisankhac)
    } else {

    }
}




function checkThongTinCanBo() {

    $.post("/NV_KeKhai_TSTN/GetThongTinNguoiKeKhai/", (data) => {
        if (data.status == false) {
            window.location = `/DM_CanBo/Edit/${data.MaCanBo}`
        }
    })
}

    function LoadData() {

        $.get("/NV_KeKhai_TSTN/GetLoaiKeKhai/", { MaKeHoachKeKhai: MaKeHoachKeKhai }, (data) => {
            if (data.Ma_Loai_KeKhai == 3) {
                $('#TenBanKeKhai').text("BẢN KÊ KHAI TÀI SẢN, THU NHẬP LẦN ĐẦU")
                $("#BienDongTaiSan").remove()
            }
            else if (data.Ma_Loai_KeKhai == 4) {
                $('#TenBanKeKhai').text("BẢN KÊ KHAI TÀI SẢN, THU NHẬP HẰNG NĂM")
            }
            else if (data.Ma_Loai_KeKhai == 5) {
                $('#TenBanKeKhai').text("BẢN KÊ KHAI TÀI SẢN, THU NHẬP BỔ SUNG")
                $("#BienDongTaiSan").remove()
            }
            else if (data.Ma_Loai_KeKhai == 12) {
                $('#TenBanKeKhai').text("BẢN KÊ KHAI TÀI SẢN, THU NHẬP PHỤC VỤ CÔNG TÁC CÁN BỘ")
            }
           
            $("#Ma_Loai_KeKhai").val(data.Ma_Loai_KeKhai)
        })
       


        $.get("/NV_KeKhai_TSTN/GetThongTinNguoiKeKhai/", (data) => {
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

            var parts1
            var parts2
            if (data.nguoikekhai.DoB != null) {
                parts1 = data.nguoikekhai.DoB.split("/");
            } else {
                window.location.replace(`/can-bo/cap-nhat/${data.nguoikekhai.Ma_CanBo}`);
            }

            if (data.nguoikekhai.NgayCap != null) {
                parts2 = data.nguoikekhai.NgayCap.split("/");
            } else {
                window.location.replace(`/can-bo/cap-nhat/${data.nguoikekhai.Ma_CanBo}`);
            }
            



            $('#DoB').val(`${parts1[2]}-${parts1[1]}-${parts1[0]}`)
            $('#NgayCap').val(`${parts2[2]}-${parts2[1]}-${parts2[0]}`)

     
            var flagVoChong = false
            var flagCon = false
            $.each(data.thannhan, (index, value) => {
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

        //sự kiện click đất 
        $("#clsAddRow_collapseDat").click(() => {
            demdat++;
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformdat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]">
                                    <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Ở">
                                    <input type="text" class="form-control" hidden name="TenLoaiDat[]" value="">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); Nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi ''Chưa được cấp giấy chứng nhận quyền sử dụng đất''. " id="GiayChungNhanQuyenSoHuuDatO" name="GiayChungNhanQuyenSoHuuDat[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Ví dụ: Người kê khai đứng tên đăng kí quyền sử dụng, quyền sở hưu nhưng thực tế là của người khác" id="ThongTinDatKhacNeuCo" name="ThongTinDatKhacNeuCo[]"></textarea>
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
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformdatkhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Loại đất</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenLoaiDat" name="TenLoaiDat[]">
                                    <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Khác">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanQuyenSoHuuDat" name="GiayChungNhanQuyenSoHuuDat[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinDatKhacNeuCo" name="ThongTinDatKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div>`

            $('#clsAddRow_collapseCacLoaiDatKhac').before(row)
        })
        $("#clsAddRow_collapseNhaO").click(() => {
            demnhao++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformnhao(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiNhaO" name="DiaChiNhaO[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Loại nhà<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (14)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi “căn hộ” nếu là căn hộ trong nhà tập thể, chung cư; ghi “nhà ở riêng lẻ” nếu là nhà được xây dựng trên thửa đất riêng biệt.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="LoaiNhaO" name="LoaiNhaO[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Diện tích sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (15)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi tổng diện tích (m2) sàn xây dựng của tất cả các tầng của nhà ở riêng lẻ, biệt thự bao gồm cả các tầng hầm, tầng nửa hầm, tầng kỹ thuật, tầng áp mái và tầng mái tum. Nếu là căn hộ thì diện tích được ghi theo giấy chứng nhận quyền sở hữu hoặc hợp đồng mua, hợp đồng thuê của nhà nước.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichNhaO" name="DienTichNhaO[]">
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của nhà ở" id="GiaTriNhaO" name="GiaTriNhaO[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanNhaO" name="GiayChungNhanNhaO[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinNhaOKhacNeuCo" name="ThongTinNhaOKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div> `
            $('#clsAddRow_collapseNhaO').before(row)
        })
        $("#clsAddRow_collapseCongTrinh").click(() => {
            demnxaydungkhac++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformxaydungkhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên công trình</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenCongTrinh" name="TenCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
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
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="DienTichCongTrinh" name="DienTichCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của công trình" id="GiaTriCongTrinh" name="GiaTriCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="GiayChungNhanCongTrinh" name="GiayChungNhanCongTrinh[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="ThongTinCongTrinhKhacNeuCo" name="ThongTinCongTrinhKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseCongTrinh').before(row)
        })
        $("#clsAddRow_collapseCayLauNam").click(() => {
            demncaylaunam++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformcaylaunam(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại cây</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại cây" id="LoaiCay" name="TenTaiSan[]">
                                        <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='cln'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng cây" id="SoLuongCay" name="SoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập loại đất" id="GiaTriCay" name="GiaTriTS[]">
                                </div>
                            </div>
                        </div>`

            $('#clsAddRow_collapseCayLauNam').before(row)
        })
        $("#clsAddRow_collapseRungSanXuat").click(() => {
            demrungsanxuat++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformrungsanxuat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại rừng</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại rừng" id="LoaiRung" name="TenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='rsx'>  
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích rừng" id="DienTichRung" name="SoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị rừng" id="GiaTriRung" name="GiaTriTS[]">
                                </div>
                            </div>
                        </div>`

            $('#clsAddRow_collapseRungSanXuat').before(row)
        })
        $("#clsAddRow_collapseKienTruc").click(() => {
            demvktglvd++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                             <div class="clear" onclick="clearformvktglvd(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenKienTrucGanDat" name="TenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='vkt'> 
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng vật kiến trúc" id="SoLuongKienTruc" name="SoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị vật kiến trúc" id="GiaTriVatKienTruc" name="GiaTriTS[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseKienTruc').before(row)
        })
        $("#clsAddRow_collapseKimLoaiDaQuy").click(() => {
            demvangkimcuong++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformvangkimcuong(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenTrangSuc" name="TenTrangSuc[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTrangSuc" name="GiaTriTrangSuc[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseKimLoaiDaQuy').before(row)
        })
        $("#clsAddRow_collapseTien").click(() => {
            demtien++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtien(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenLoaiTien" name="TenLoaiTien[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriLoaiTien" name="GiaTriLoaiTien[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseTien').before(row)
        })
        $("#clsAddRow_collapseCoPhieu").click(() => {
            demcophieu++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformcophieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên cố phiếu</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="TenPhieu" name="TenPhieu[]">
                                    <input hidden value="CoPhieu" id="LoaiPhieu" name="LoaiPhieu">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseCoPhieu').before(row)
        })
        $("#clsAddRow_collapseTraiPhieu").click(() => {
            demtraiphieu++;
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtraiphieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên trái phiếu</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên trái phiếu" id="TenPhieu" name="TenPhieu[]">
                                     <input hidden value="TraiPhieu" id="LoaiPhieu" name="LoaiPhieu">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseTraiPhieu').before(row)
        })
        $("#clsAddRow_collapseGiayToKhac").click(() => {
            demgiaytokhac++;
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformgiaytokhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-8">
                                <div class="form-group">
                                    <label>Tên giấy tờ có giá<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (23)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view"    >
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Các loại giấy tờ có giá khác như chứng chỉ quỹ, kỳ phiếu, séc,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="TenPhieu" name="TenPhieu[]">
                                    <input hidden value="GiayTo" id="LoaiPhieu" name="LoaiPhieu">
                                    <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `
            $('#clsAddRow_collapseGiayToKhac').before(row)
        })
        $("#clsAddRow_collapseGopVon").click(() => {
            demvongop++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformvongop(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-8">
                                <div class="form-group">
                                    <label>Hình thức góp vốn</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên hình thức góp vốn" id="TenPhieu" name="TenPhieu[]">
                                    <input hidden value="VonGop" id="LoaiPhieu" name="LoaiPhieu">
                                    <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseGopVon').before(row)
        })
        $("#clsAddRow_collapseGiayDangKy").click(() => {
            demtaisantheoquydinh++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtaisantheoquydinh(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên tài sản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanKhac" name="TenTaiSanKhac[]">
                                    <input hidden value="GiayDangKy" id="LoaiTaiSanKhac" name="LoaiTaiSanKhac">
                                    <input hidden value="" id="NamBatDauSoHuuTaiSanKhac" name="NamBatDauSoHuuTaiSanKhac[]">
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
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseGiayDangKy').before(row)
        })
        $("#clsAddRow_collapseTaiSanKhac").click(() => {
            demtaisankhac++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                        <div class="clear" onclick="clearformtaisankhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanKhac" name="TenTaiSanKhac[]">
                                <input hidden value="TaiSanKhac" id="LoaiTaiSanKhac" name="LoaiTaiSanKhac">
                                <input hidden value="" id="SoDangKyTaiSanKhac" name="SoDangKyTaiSanKhac[]">                    
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
                                <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div> `

            $('#clsAddRow_collapseTaiSanKhac').before(row)
        })
        $("#clsAddRow_collapseTaiSanNuocNgoai").click(() => {
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên tài sản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="TenTaiSanNuocNgoai" name="TenTaiSanNuocNgoai[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="GiaTriTaiSanNuocNgoai" name="GiaTriTaiSanNuocNgoai[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseTaiSanNuocNgoai').before(row)
        })
        $("#clsAddRow_collapseTaiKhoanNuocNgoai").click(() => {
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên chủ tài khoản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên chủ tài khoản" id="TenTaiKhoanNguocNgoai" name="TenTaiKhoanNuocNgoai[]">
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

        //sự kiện click tài sản ở nước ngoài
        $("#clsAddRow_collapsetsnnDat").click(() => {
            tsnndemdat++;
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnndat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiDat" name="tsnnDiaChiDat[]">
                                    <input type="text" class="form-control" hidden name="tsnnLoaiDat[]" value="Đất Ở">
                                    <input type="text" class="form-control" hidden name="tsnnTenLoaiDat[]" value="">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichDat" name="tsnnDienTichDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="tsnnGiaTriDat" name="tsnnGiaTriDat[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); Nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi ''Chưa được cấp giấy chứng nhận quyền sử dụng đất''. " id="tsnnGiayChungNhanQuyenSoHuuDatO" name="tsnnGiayChungNhanQuyenSoHuuDat[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Ví dụ: Người kê khai đứng tên đăng kí quyền sử dụng, quyền sở hưu nhưng thực tế là của người khác" id="tsnnThongTinDatKhacNeuCo" name="tsnnThongTinDatKhacNeuCo[]"></textarea>
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
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnndatkhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Loại đất</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnTenLoaiDat" name="tsnnTenLoaiDat[]">
                                    <input type="text" class="form-control" hidden name="tsnnLoaiDat[]" value="Đất Khác">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiDat" name="tsnnDiaChiDat[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichDat" name="tsnnDienTichDat[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của đất" id="tsnnGiaTriDat" name="tsnnGiaTriDat[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanQuyenSoHuuDat" name="tsnnGiayChungNhanQuyenSoHuuDat[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinDatKhacNeuCo" name="tsnnThongTinDatKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div>`

            $('#clsAddRow_collapsetsnnCacLoaiDatKhac').before(row)
        })
        $("#clsAddRow_collapsetsnnNhaO").click(() => {
            tsnndemnhao++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnnhao(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnDiaChiNhaO" name="tsnnDiaChiNhaO[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Loại nhà<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (14)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi “căn hộ” nếu là căn hộ trong nhà tập thể, chung cư; ghi “nhà ở riêng lẻ” nếu là nhà được xây dựng trên thửa đất riêng biệt.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="tsnnLoaiNhaO" name="tsnnLoaiNhaO[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Diện tích sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (15)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi tổng diện tích (m2) sàn xây dựng của tất cả các tầng của nhà ở riêng lẻ, biệt thự bao gồm cả các tầng hầm, tầng nửa hầm, tầng kỹ thuật, tầng áp mái và tầng mái tum. Nếu là căn hộ thì diện tích được ghi theo giấy chứng nhận quyền sở hữu hoặc hợp đồng mua, hợp đồng thuê của nhà nước.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichNhaO" name="tsnnDienTichNhaO[]">
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của nhà ở" id="tsnnGiaTriNhaO" name="tsnnGiaTriNhaO[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanNhaO" name="tsnnGiayChungNhanNhaO[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinNhaOKhacNeuCo" name="tsnnThongTinNhaOKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div> `
            $('#clsAddRow_collapsetsnnNhaO').before(row)
        })
        $("#clsAddRow_collapsetsnnCongTrinh").click(() => {
            tsnndemnxaydungkhac++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnxaydungkhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên công trình</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="tsnnTenCongTrinh" name="tsnnTenCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ <sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (8)
                                            <div class="tooltip-right-contain" style="text-align: justify;">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
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
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích" id="tsnnDienTichCongTrinh" name="tsnnDienTichCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị của công trình" id="tsnnGiaTriCongTrinh" name="tsnnGiaTriCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (11)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập số giấy chứng nhận và tên người đại diện" id="tsnnGiayChungNhanCongTrinh" name="tsnnGiayChungNhanCongTrinh[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (12)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <textarea class="form-control" placeholder="Nhập cụ thể về tình trạng thực tế quản lý và sử dụng" id="tsnnThongTinCongTrinhKhacNeuCo" name="tsnnThongTinCongTrinhKhacNeuCo[]"></textarea>
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnCongTrinh').before(row)
        })
        $("#clsAddRow_collapsetsnnCayLauNam").click(() => {
            tsnndemncaylaunam++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnncaylaunam(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại cây</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại cây" id="tsnnLoaiCay" name="tsnnTenTaiSan[]">
                                        <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='cln'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng cây" id="tsnnSoLuongCay" name="tsnnSoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập loại đất" id="tsnnGiaTriCay" name="tsnnGiaTriTS[]">
                                </div>
                            </div>
                        </div>`

            $('#clsAddRow_collapsetsnnCayLauNam').before(row)
        })
        $("#clsAddRow_collapsetsnnRungSanXuat").click(() => {
            tsnndemrungsanxuat++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformrungsanxuat(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại rừng</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại rừng" id="tsnnLoaiRung" name="tsnnTenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='rsx'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích<sup>
                                            <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                                (9)
                                                <div class="tooltip-right-contain">
                                                    <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                        <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                            <h6>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất).</h6>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>
                                        </sup></label>
                                    <input type="text" class="form-control area" placeholder="Nhập diện tích rừng" id="tsnnDienTichRung" name="tsnnSoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị rừng" id="tsnnGiaTriRung" name="tsnnGiaTriTS[]">
                                </div>
                            </div>
                        </div>`

            $('#clsAddRow_collapsetsnnRungSanXuat').before(row)
        })
        $("#clsAddRow_collapsetsnnKienTruc").click(() => {
            tsnndemvktglvd++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                             <div class="clear" onclick="clearformtsnnvktglvd(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenKienTrucGanDat" name="tsnnTenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="tsnnLoaiTaiSan" name="tsnnLoaiTaiSan[]" value='vkt'>
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng vật kiến trúc" id="tsnnSoLuongKienTruc" name="tsnnSoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị vật kiến trúc" id="tsnnGiaTriVatKienTruc" name="tsnnGiaTriTS[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnKienTruc').before(row)
        })
        $("#clsAddRow_collapsetsnnKimLoaiDaQuy").click(() => {
            tsnndemvangkimcuong++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnvangkimcuong(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenTrangSuc" name="tsnnTenTrangSuc[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTrangSuc" name="tsnnGiaTriTrangSuc[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnKimLoaiDaQuy').before(row)
        })
        $("#clsAddRow_collapsetsnnTien").click(() => {
            tsnndemtien++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnntien(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="tsnnTenLoaiTien" name="tsnnTenLoaiTien[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriLoaiTien" name="tsnnGiaTriLoaiTien[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnTien').before(row)
        })
        $("#clsAddRow_collapsetsnnCoPhieu").click(() => {
            tsnndemcophieu++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnncophieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên cố phiếu</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên cổ phiếu" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                    <input hidden value="CoPhieu" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnCoPhieu').before(row)
        })
        $("#clsAddRow_collapsetsnnTraiPhieu").click(() => {
            tsnndemtraiphieu++;
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnntraiphieu(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên trái phiếu</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên trái phiếu" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                     <input hidden value="TraiPhieu" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Số lượng</label>
                                    <input type="text" class="form-control" placeholder="Nhập số lượng" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnTraiPhieu').before(row)
        })
        $("#clsAddRow_collapsetsnnGiayToKhac").click(() => {
            tsnndemgiaytokhac++;
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnngiaytokhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-8">
                                <div class="form-group">
                                    <label>Tên giấy tờ có giá<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (23)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view"    >
                                                    <div class="don-vi-thanh-tra" style="text-align: justify;">
                                                        <h6>Các loại giấy tờ có giá khác như chứng chỉ quỹ, kỳ phiếu, séc,...</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                    <input hidden value="GiayTo" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                    <input hidden value="0" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `
            $('#clsAddRow_collapsetsnnGiayToKhac').before(row)
        })
        $("#clsAddRow_collapsetsnnGopVon").click(() => {
            tsnndemvongop++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnnvongop(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-8">
                                <div class="form-group">
                                    <label>Hình thức góp vốn</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên hình thức góp vốn" id="tsnnTenPhieu" name="tsnnTenPhieu[]">
                                    <input hidden value="VonGop" id="tsnnLoaiPhieu" name="tsnnLoaiPhieu">
                                    <input hidden value="0" id="tsnnSoLuongPhieu" name="tsnnSoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriPhieu" name="tsnnGiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnGopVon').before(row)
        })
        $("#clsAddRow_collapsetsnnGiayDangKy").click(() => {
            tsnndemtaisantheoquydinh++;
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="clear" onclick="clearformtsnntaisantheoquydinh(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên tài sản</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="tsnnTenTaiSanKhac" name="tsnnTenTaiSanKhac[]">
                                    <input hidden value="GiayDangKy" id="tsnnLoaiTaiSanKhac" name="tsnnLoaiTaiSanKhac">
                                    <input hidden value="" id="tsnnNamBatDauSoHuuTaiSanKhac" name="tsnnNamBatDauSoHuuTaiSanKhac[]">
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
                                    <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup></label>
                                    <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTaiSanKhac" name="tsnnGiaTriTaiSanKhac[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapsetsnnGiayDangKy').before(row)
        })
        $("#clsAddRow_collapsetsnnTaiSanKhac").click(() => {
            tsnndemtaisankhac++;
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; position: relative; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                        <div class="clear" onclick="clearformtsnntaisankhac(this)" style="position: absolute; right: 10px; top: 5px; color: red; font-size: 18px; cursor: pointer"><i class="fa-solid fa-circle-xmark"></i></div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Tên tài sản</label>
                                <input type="text" class="form-control" placeholder="Nhập tên tài sản" id="tsnnTenTaiSanKhac" name="tsnnTenTaiSanKhac[]">
                                <input hidden value="TaiSanKhac" id="tsnnLoaiTaiSanKhac" name="tsnnLoaiTaiSanKhac">
                                <input hidden value="" id="tsnnSoDangKyTaiSanKhac" name="tsnnSoDangKyTaiSanKhac[]">
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
                                <label>Giá trị<sup>
                                        <a style="cursor:pointer;" class="tooltip-history tooltip-right">
                                            (10)
                                            <div class="tooltip-right-contain">
                                                <div class="group-item history-view" data-nam="hide" data-tongtt="0" data-solanvipham="0" data-dauhieuvp="" data-kehoachnam="2023" data-donvitt="Sở Y tế - Thanh Tra Sở Y tế" data-donviph="">
                                                    <div class="don-vi-thanh-tra" style="text-align:justify;">
                                                        <h6>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do.</h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </sup>label>
                                <input type="text" class="form-control currency" placeholder="Nhập giá trị" id="tsnnGiaTriTaiSanKhac" name="tsnnGiaTriTaiSanKhac[]">
                            </div>
                        </div>
                    </div> `

            $('#clsAddRow_collapsetsnnTaiSanKhac').before(row)
        })

    }

    function FnSuccess(data) {
        Swal.fire({
            icon: 'success',
            title: 'Thành Công',
            text: `Bạn đã kê khai tài sản thành công`,
            timer: 2000,
            showConfirmButton: false,
        }).then(() => { window.location.replace(data);})
    }





$(document).ready(async function  () {
    checkThongTinCanBo();
    
    LoadData();

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

    //CheckValidate form sửa Cơ quan cán bộ

    $(".form_KeKhai").validate({
        errorElement: 'div',
        rules: {
            "DienTichDat[]": {
                number: true,
            },
            "GiaTriDat[]": {
                number: true,
            },
            "DienTichNhaO[]": {
                number: true,
            },
            "GiaTriNhaO[]": {
                number: true,
            },
            "DienTichCongTrinh[]": {
                number: true,
            },
            "GiaTriCongTrinh[]": {
                number: true,
            },
            "SoLuong_DienTich[]": {
                required: true,
            },
            "GiaTriTS[]": {
                number: true,
            },
            "SoLuong_DienTich[]": {
                number: true,
            },
            "GiaTriTrangSuc[]": {
                number: true,
            },
            "GiaTriLoaiTien[]": {
                number: true,
            },
            "SoLuongPhieu[]": {
                number: true,
            },
            "GiaTriPhieu[]": {
                number: true,
            },  
            "GiaTriTrangSuc[]": {
                number: true,
            },
            "GiaTriTaiSanKhac[]": {
                number: true,
            },
            "GiaTriTaiSanNuocNgoai[]": {
                number: true,
            },
            "ThuNhapNguoiKeKhai[]": {
                number: true,
            },
            "ThuNhapVoHoacChong[]": {
                required: true,
            },
            "ThuNhapCon[]": {
                number: true,
            },
            "ThuNhapChung[]": {
                number: true,
            },
        },
        messages: {
            "DienTichDat[]": {
                number: "Vui lòng nhập số", 
            },
            "GiaTriDat[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "DienTichNhaO[]": {
                number: "Vui lòng nhập số",
            },
            "GiaTriNhaO[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "DienTichCongTrinh[]": {
                number: "Vui lòng nhập số",
            },
            "GiaTriCongTrinh[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "SoLuong_DienTich[]": {
                number: "Vui lòng nhập số",
            },
            "GiaTriTS[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "SoLuong_DienTich[]": {
                number: "Vui lòng nhập số",
            },
            "GiaTriTrangSuc[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "GiaTriLoaiTien[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "SoLuongPhieu[]": {
                number: "Vui lòng nhập số",
            },
            "GiaTriPhieu[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "GiaTriTrangSuc[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "GiaTriTaiSanKhac[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "GiaTriTaiSanNuocNgoai[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "ThuNhapNguoiKeKhai[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "ThuNhapVoHoacChong[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "ThuNhapCon[]": {
                number: "Vui lòng nhập giá tiền",
            },
            "ThuNhapChung[]": {
                number: "Vui lòng nhập giá tiền",
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
})