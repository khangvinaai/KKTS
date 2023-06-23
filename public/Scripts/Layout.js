function LoadMenuList() {
    var ele = $(obj);
    var Ma_CoQuan_DonVi = ele.data("model-id");
    var url = `/DM_CoQuanDonVi/GetSuaCoQuanDonVi/`
    $.get(url, { id: Ma_CoQuan_DonVi })
        .done(function (data) {

            $("#sua").empty()
            var row = ``
            var row2 = ``

            var row1 = `  <div class="row">
                                        <input type="hidden" disable="true" class="form-control" id="Ma_CoQuan_DonVi" name="Ma_CoQuan_DonVi" value="${data.Ma_CoQuan_DonVi}">
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Tên Cơ Quan - Đơn Vị</label>

                                            <input type="text" class="form-control" placeholder="nhập tên cơ quan" id="Ten" name="Ten" value="${data.Ten}">
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Địa chỉ</label>
                                            <input type="text" class="form-control" placeholder="nhập địa chỉ" id="DiaChi" name="DiaChi" value="${data.DiaChi}">
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Mã Số Thuế</label>
                                            <input type="text" class="form-control" placeholder="nhập mã số thuế" id="MST" name="MST" value="${data.MST}">
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Số Điện Thoại</label>
                                            <input type="text" class="form-control" placeholder="nhập sdt" id="SoDienThoai" name="SoDienThoai" value="${data.SoDienThoai}">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Email</label>
                                            <input type="text" class="form-control" placeholder="nhập email" id="Email" name="Email" value="${data.Email}">
                                        </div>
                                    </div>
                           
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Website</label>
                                            <input type="text" class="form-control" placeholder="nhập website" id="Website" name="Website" value="${data.Website}">
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <div class="form-group">
                                            <label>Phường Xã</label>
                                            <select class="form-control" name="Ma_PhuongXa" id="Ma_PhuongXa1" style="width: 100%; height: 100%">
                                                <option value="">Chọn</option>
                                            </select>
                                        </div>
                                    </div>



                                </div>

                                `
            $('#sua').prepend(row1);

            $.get("/DM_PhuongXa/GetPhuongXa/", (data2) => {
                $("#Ma_PhuongXa1").empty();
                $.each(data2, (index, value) => {

                    if (value.Ma_PhuongXa == data.Ma_PhuongXa) {

                        $("#Ma_PhuongXa1").append(`<option selected value = "${value.Ma_PhuongXa}">${value.Ten_PhuongXa}</option>`)
                    }
                    else {
                        $("#Ma_PhuongXa1").append(`<option value = "${value.Ma_PhuongXa}">${value.Ten_PhuongXa}</option>`)
                    }
                })
            });
        })
};

