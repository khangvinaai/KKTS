const params = new URLSearchParams(window.location.search)
var makehoachkekhai = Object.fromEntries(params.entries()).id;

function checkThongTinCanBo() {

    $.post("/NV_KeKhai_TSTN/GetThongTinNguoiKeKhai/", (data) => {
        if (data.status == false) {
            window.location = `/DM_CanBo/Edit/${data.MaCanBo}`
        }
    })
    
}

    function LoadData() {
        //Get Data Loại Kê Khai
        $.get("/DM_Loai_KeKhai/GetLoaiKeKhai/", { makehoachkekhai: makehoachkekhai}, (data) => {
            $("#Ma_Loai_KeKhai").append(`<option value ="${data.Ma_Loai_KeKhai}" selected>${data.Ten_KeKhai}</option>`)
           
        })
        $("#Ma_Loai_KeKhai").select2();

        //Get Data Lĩnh Vực Kê Khai
        $.get("/DM_LinhVuc_KeKhai/GetLinhVucKeKhai/", (data) => {
        $("#Ma_LinhVuc_KeKhai").append(`<option value = "">Chọn</option>`)

            $.each(data, (index, value) => {
        $("#Ma_LinhVuc_KeKhai").append(`<option value ="${value.MaLinhVuc}">${value.TenLinhVuc}</option>`)
    })
        })
        $("#Ma_LinhVuc_KeKhai").select2();

        $.get("/NV_KeKhai_TSTN/GetThongTinNguoiKeKhai/", (data) => {
        $("#HoTen").val(data.nguoikekhai.HoTen)
            $("#DiaChiThuongTru").val(data.nguoikekhai.DiaChiThuongTru)
            $("#DoB").val(data.nguoikekhai.DoB)
            $("#Ma_ChucVu_ChucDanh").val(data.nguoikekhai.Ma_ChucVu_ChucDanh)
            $("#Ma_CoQuan_DonVi").val(data.nguoikekhai.Ma_CoQuan_DonVi)
            $("#Ma_PhuongXa").val(data.nguoikekhai.Ma_PhuongXa)
            $("#Ma_QuanHuyen").val(data.nguoikekhai.Ma_QuanHuyen)
            $("#Ma_TinhThanh").val(data.nguoikekhai.Ma_TinhThanh)
            $("#NgayCap").val(data.nguoikekhai.NgayCap)
            $("#NoiCap").val(data.nguoikekhai.NoiCap)
            $("#SoCCCD").val(data.nguoikekhai.SoCCCD)
            $("#Ma_CanBo").val(data.nguoikekhai.Ma_CanBo)

            $.each(data.thannhan, (index, value) => {
                var row = ` <div class="row tn1" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                                <div class="col-5">
                                    <div class="form-group">
                                        <label>Họ Và Tên</label>
                                        <input type="text" class="form-control" id="HoTenThanNhan" readonly value="${value.HoTenThanNhan}">
                                    </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Ngày sinh</label>
                                            <input type="text" class="form-control" id="DoBTN" readonly value="${value.DoBTN}">
                                        </div>
                                    </div>
                                    <div class="col-5">
                                        <div class="form-group">
                                            <label>Địa chỉ</label>
                                            <input type="text" class="form-control" id="DiaChiThuongTruTN" readonly value="${value.DiaChiThuongTruTN}">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Tỉnh thành</label>
                                            <input type="text" class="form-control" id="Ma_TinhThanhTN" readonly value="${value.Ma_TinhThanhTN}">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Quận huyện</label>
                                            <input type="text" class="form-control" id="Ma_QuanHuyenTN" readonly value="${value.Ma_QuanHuyenTN}">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Phường Xã</label>
                                            <input type="text" class="form-control" id="Ma_PhuongXa_TTTN" readonly value="${value.Ma_PhuongXa_TTTN}">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Số CMND/CCCD</label>
                                            <input type="text" class="form-control" id="SoCCCDTN" readonly value="${value.SoCCCDTN}">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Ngày Cấp</label>
                                            <input type="text" class="form-control" id="NgayCapTN" readonly value="${value.NgayCapTN}">
                                        </div>
                                    </div>

                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Nơi Cấp</label>
                                            <input type="text" class="form-control" id="NoiCapTN" readonly value="${value.NoiCapTN}">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Thân nhân là</label>
                                            <input type="text" class="form-control" id="VaiTroThanNhan" readonly value="${value.VaiTroThanNhan}">

                                        </div>
                                    </div>
                                </div>`
                $("#collapseThanNhan").append(row);
            })
        })


        $("#clsAddRow_collapseDat").click(() => {
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ</label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiDat" name="DiaChiDat[]">
                                    <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Ở">
                                    <input type="text" class="form-control" hidden name="TenLoaiDat[]" value="">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích</label>
                                    <input type="text" class="form-control" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giấy chứng nhận quyền sử dụng</label>
                                    <textarea class="form-control" placeholder="Ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); Nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi ''Chưa được cấp giấy chứng nhận quyền sử dụng đất''. " id="GiayChungNhanQuyenSoHuuDatO" name="GiayChungNhanQuyenSoHuuDat[]"></textarea>
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Thông tin khác (nếu có)</label>
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
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Loại đất</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenLoaiDat" name="TenLoaiDat[]">
                                    <input type="text" class="form-control" hidden name="LoaiDat[]" value="Đất Khác">
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
                                    <input type="text" class="form-control" placeholder="Nhập diện tích" id="DienTichDat" name="DienTichDat[]">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị của đất" id="GiaTriDat" name="GiaTriDat[]">
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
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">

                            <div class="col-4">
                                <div class="form-group">
                                    <label>Địa chỉ</label>
                                    <input type="text" class="form-control" placeholder="Nhập địa chỉ" id="DiaChiNhaO" name="DiaChiNhaO[]">
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
                                    <input type="text" class="form-control" placeholder="Nhập diện tích" id="DienTichNhaO" name="DienTichNhaO[]">
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị của nhà ở" id="GiaTriNhaO" name="GiaTriNhaO[]">
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
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Tên công trình</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="TenCongTrinh" name="TenCongTrinh[]">
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
                                    <input type="text" class="form-control" placeholder="Nhập diện tích" id="DienTichCongTrinh" name="DienTichCongTrinh[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị của công trình" id="GiaTriCongTrinh" name="GiaTriCongTrinh[]">
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
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
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
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại đất" id="GiaTriCay" name="GiaTriTS[]">
                                </div>
                            </div>
                        </div>`

            $('#clsAddRow_collapseCayLauNam').before(row)
        })

        $("#clsAddRow_collapseRungSanXuat").click(() => {
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Loại rừng</label>
                                    <input type="text" class="form-control" placeholder="Nhập loại rừng" id="LoaiRung" name="TenTaiSan[]">
                                    <input type="text" class="form-control" hidden id="LoaiTaiSan" name="LoaiTaiSan[]" value='rsx'>  
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Diện tích</label>
                                    <input type="text" class="form-control" placeholder="Nhập diện tích rừng" id="DienTichRung" name="SoLuong_DienTich[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị rừng" id="GiaTriRung" name="GiaTriTS[]">
                                </div>
                            </div>
                        </div>`

            $('#clsAddRow_collapseRungSanXuat').before(row)
        })

        $("#clsAddRow_collapseKienTruc").click(() => {
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
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
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị vật kiến trúc" id="GiaTriVatKienTruc" name="GiaTriTS[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseKienTruc').before(row)
        })
        $("#clsAddRow_collapseKimLoaiDaQuy").click(() => {
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenTrangSuc" name="TenTrangSuc[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriTrangSuc" name="GiaTriTrangSuc[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseKimLoaiDaQuy').before(row)
        })
        $("#clsAddRow_collapseTien").click(() => {
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Tên gọi</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên" id="TenLoaiTien" name="TenLoaiTien[]">
                                </div>
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriLoaiTien" name="GiaTriLoaiTien[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseTien').before(row)
        })
        $("#clsAddRow_collapseCoPhieu").click(() => {
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
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
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseCoPhieu').before(row)
        })
        $("#clsAddRow_collapseTraiPhieu").click(() => {
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
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
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseTraiPhieu').before(row)
        })
        $("#clsAddRow_collapseGiayToKhac").click(() => {
            var row = ` <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
                            <div class="col-8">
                                <div class="form-group">
                                    <label>Tên giấy tờ có giá</label>
                                    <input type="text" class="form-control" placeholder="Nhập tên giấy tờ" id="TenPhieu" name="TenPhieu[]">
                                    <input hidden value="GiayTo" id="LoaiPhieu" name="LoaiPhieu">
                                    <input hidden value="0" id="SoLuongPhieu" name="SoLuongPhieu[]">
                                </div>
                            </div>
                            <div class="col-4">
                                <div class="form-group">
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseGiayToKhac').before(row)
        })
        $("#clsAddRow_collapseGopVon").click(() => {
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
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
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriPhieu" name="GiaTriPhieu[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseGopVon').before(row)
        })
        $("#clsAddRow_collapseGiayDangKy").click(() => {
            var row = `  <div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
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
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
                                </div>
                            </div>
                        </div> `

            $('#clsAddRow_collapseGiayDangKy').before(row)
        })
        $("#clsAddRow_collapseTaiSanKhac").click(() => {
            var row = `<div class="row" style="padding-top: 1%; padding-bottom: 1%; padding-left: 2%; padding-right: 2%; border-radius: 10px; box-shadow: 0 5px 20px 0 rgb(0 0 0 / 15%); margin-top: 10px;">
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
                                <label>Giá trị</label>
                                <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriTaiSanKhac" name="GiaTriTaiSanKhac[]">
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
                                    <label>Giá trị</label>
                                    <input type="text" class="form-control" placeholder="Nhập giá trị" id="GiaTriTaiSanNuocNgoai" name="GiaTriTaiSanNuocNgoai[]">
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