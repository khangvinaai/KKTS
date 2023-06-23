const params = new URLSearchParams(window.location.search)
var id = Object.fromEntries(params.entries());



    $("#them").click(() => {
        $.get("/DM_CanBo/GetCanBo/", (data) => {
            $("#Ma_CanBo").empty();
            $.each(data, (index, value) => {
                if (value.Ten == "vinaai") {
                    $("#Ma_CanBo").append(`<option selected value = "${value.Ma_CanBo}">${value.HoTen}</option>`)
                }
                else {
                    $("#Ma_CanBo").append(`<option value = "${value.Ma_CanBo}">${value.HoTen}</option>`)
                }
            });
        });

        $.get("/HT_NhomTaiKhoan/GetNhomTaiKhoan/", (data1) => {
        $("#MaNhomTaiKhoan").empty();
            $.each(data1, (index, value) => {
                if (value.Ten_ChucVu_ChucDanh == "vinaai") {
        $("#MaNhomTaiKhoan").append(`<option selected value = "${value.MaNhomTaiKhoan}">${value.TenNhomTaiKhoan}</option>`)
    }
                else {
        $("#MaNhomTaiKhoan").append(`<option value = "${value.MaNhomTaiKhoan}">${value.TenNhomTaiKhoan}</option>`)
    }
            })
        })
    })
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
                "url": `/NV_KeKhai_TSTN/LoadData?id=${id.id}`,
                "type": "POST",
                "datatype": "json"
            },
            "columns": [
                { "data": "Ten_KeKhai" },
                { "data": "Ten_KeKhai" },
                { "data": "HoTen" },
                { "data": "Nam_KeKhai" },
                {
                    "data": "NgayThangNam", "render": function (data, type, row) {
                        Number.prototype.padLeft = function (base, chr) {
                            var len = (String(base || 10).length - String(this).length) + 1;
                            return len > 0 ? new Array(len).join(chr || '0') + this : this;
                        }

                        var datemili = data.toString();
                        var mili = datemili.substring(6, 19);
                        var d = new Date(parseInt(mili));
                        var dformat = [d.getDate().padLeft(), (d.getMonth() + 1).padLeft(),

                        d.getFullYear()].join('/') +
                            ' ' +
                            [d.getHours().padLeft(),
                            d.getMinutes().padLeft(),
                            d.getSeconds().padLeft()].join(':');
                        return dformat;
                    }
                },
                { "data": "Ten_LinhVuc_KeKhai" },
                //{
                //    "data": { "Ma_KeKhai_TSTN": "Ma_KeKhai_TSTN", "Ma_CanBo": "Ma_CanBo" }, "render": function (data, type, row) {
                //        if (data.Ma_CanBo == UserID) {

                //            return ` <a class="btn btn-outline-info btn-sm btn-edit" href="/NV_KeKhai_TSTN/Edit/${data.Ma_KeKhai_TSTN}">
                //                        <i class="fas fa-pencil-alt"></i>
                //                    </a>`
                //        } else {
                //            return ``
                //        }
                //    }
                //},
                {
                    "data": { "Ma_KeKhai_TSTN": "Ma_KeKhai_TSTN", "Ma_CanBo": "Ma_CanBo" }, "render": function (data, type, row) {

                        if (data.Ma_CanBo == UserID) {

                            return `
                                    <a class="btn btn-outline-info btn-sm btn-edit" href="/NV_KeKhai_TSTN/Edit/${data.Ma_KeKhai_TSTN}">
                                        <i class="fas fa-pencil-alt"></i>
                                        </a>
                                    <button class="btn btn-outline-info btn-sm btn-edit" data-model-id="${data.Ma_KeKhai_TSTN}" data-toggle="modal" data-target="#Print_KKCB" onclick="printDSKK(this)" >
                                        In bản kê khai
                                    </button>
                                    `
                        } else {
                            return `
                                    
                                    `
                        }

                    }
                },
            ]
        });
        t.on('draw.dt', function () {
            var info = t.page.info();
            t.column(0, {search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
        cell.innerHTML = i + 1 + info.start;
            });
        });
    
    }
function printDSKK(obj) {
    var ele = $(obj);
    var MaKeKhai = ele.data("model-id");
    var url = `/NV_KeKhai_TSTN/InBanKeKhai/${MaKeKhai}`

    function currency(nStr) {
        nStr += '';
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + '.' + '$2');
        }
        return x1 + x2;
    }

    $.post(url, { MaKeKhai: MaKeKhai })
        .done(function (data) {
            if (data != false) {
                //Thông tin chung

                var ngay = data.bankekhai.Ngay_KeKhai, thang = data.bankekhai.Thang_KeKhai, nam = data.bankekhai.Nam_KeKhai;

                var thongtinchung = `<table style="width: 100%;">
                                            <tr >
                                            <td style="width: 50%;"><p>- Họ và tên: ${data.nguoikekhai.HoTen}</p></td>
                                            <td style="width: 50%;"><p>Ngày tháng năm sinh: ${data.nguoikekhai.DoB}.</p></td></tr></table>
                                        <p>- Chức vụ/chức danh công tác: 
                                            ${data.nguoikekhai.Ma_ChucVu_ChucDanh}.</p>
                                        <p>- Cơ quan/đơn vị công tác:
                                            ${data.nguoikekhai.Ma_CoQuan_DonVi}.</p>
                                        <p>- Nơi thường trú:
                                            ${data.nguoikekhai.DiaChiThuongTru} -${data.nguoikekhai.Ma_PhuongXa} - ${data.nguoikekhai.Ma_QuanHuyen} -${data.nguoikekhai.Ma_TinhThanh}.
                                        </p>
                                        <p>-Số căn cước công dân hoặc giấy chứng minh nhân dân<sup>(3)</sup>:${data.nguoikekhai.SoCCCD}</p>
                                            <table style="width: 100%;">
                                            <tr >
                                            <td style="width: 50%;"><p>ng&agrave;y cấp: ${data.nguoikekhai.SoCCCD}</p></td> <td><p>nơi cấp:
                                            ${data.nguoikekhai.NoiCap}.</p></td></tr></table>`;

                //Thông tin Thân nhân
                var thongtinVoChong = '';
                var thongtinCon = '';
                $.each(data.thannhan, (index, value) => {
                    if (value.VaiTroThanNhan == "Con") {
                        thongtinCon += `<p><strong>3.${index + 1}. Con thứ ${index+1}:</strong></p>
                                        <table style="width: 100%;">
                                            <tr >
                                            <td style="width: 50%;"><p>- Họ và tên: ${value.HoTenThanNhan}</p></td> 
                                            <td style="width: 50%;"><p> Ngày tháng năm sinh:  ${value.DoBTN}</p></td></tr></table>
                                        <p>- Nơi thường tr&uacute;:
                                            ${value.DiaChiThuongTruTN} - ${value.Ma_PhuongXa_TTTN} - ${value.Ma_QuanHuyenTN} - ${value.Ma_TinhThanhTN} 
                                        </p>
                                        <p>- Số căn cước c&ocirc;ng d&acirc;n hoặc giấy chứng minh nh&acirc;n d&acirc;n:
                                              ${value.SoCCCDTN}</p>
                                        <table style="width: 100%;">
                                            <tr >
                                            <td style="width: 50%;"><p>- ng&agrave;y cấp:   ${value.NgayCapTN}</p></td>
                                            <td><p>nơi cấp:   ${value.NoiCapTN}.</p></td>
                                            </tr>
                                        </table>
                                        `
                    } else {
                        thongtinVoChong += `<table style="width: 100%;">
                                                <tr >
                                                    <td style="width: 50%;"><p>- Họ và tên: ${value.HoTenThanNhan}</p></td> 
                                                    <td style="width: 50%;"><p> Ngày tháng năm sinh:  ${value.DoBTN}</p></td>
                                                </tr>
                                            </table>
                                            <p>- Nghề nghiệp:
                                                ......................................................................................................................
                                            </p>
                                            <p>- Nơi l&agrave;m việc<sup>(4)</sup>:
                                                ....................................................................................................................
                                            </p>
                                            <p>- Nơi thường tr&uacute;:
                                                ${value.DiaChiThuongTruTN} - ${value.Ma_PhuongXa_TTTN} - ${value.Ma_QuanHuyenTN} - ${value.Ma_TinhThanhTN} ..............................
                                            </p>
                                            <p>- Số căn cước c&ocirc;ng d&acirc;n hoặc giấy chứng minh nh&acirc;n d&acirc;n:
                                              ${value.SoCCCDTN}</p>
                                            <table style="width: 100%;">
                                                <tr >
                                                    <td style="width: 50%;"><p>Ngày cấp:   ${value.NgayCapTN}</p></td>
                                                    <td><p> nơi cấp:   ${value.NoiCapTN}.</p></td>
                                                </tr>
                                            </table>
                                            `
                    }
                })


                var thuadat = ``;
                $.each(data.dato, (index, value) => {
                    if (value == null) {
                        thuadat = `<p><strong>1.1.1. Thửa thứ nhất:</strong></p>
                                    <p>- Địa chỉ<sup>(8)</sup> <sup>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương</sup>:
                                        ..........................................................................................................................................................
                                    </p>
                                    <p>- Diện tích<sup>(9)</sup> <sup>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất)</sup>:
                                        .................................................................................................................................
                                    </p>
                                    <p>- Giá trị<sup>(10)</sup> <sup>) Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do</sup>:
                                        ..............................................................................................................................
                                    </p>
                                    <p>- Giấy chứng nhận quyền sử dụng<sup>(11)</sup> <sup>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”</sup>
                                        ...................................................................................</p>
                                    <p>- Thông tin khác (nếu có)<sup>(12)</sup> <sup>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</sup>:
                                        ..................................................................................................</p>`;
                    } else {
                        thuadat = `<p><strong>1.1.${index + 1}. Thửa thứ ${index + 1}:</strong></p>
                                    <p>- Địa chỉ<sup>(8)</sup> <sup>Ghi cụ thể số nhà (nếu có), ngõ, ngách, khu phố, thôn, xóm, bản; xã, phường, thị trấn; quận, huyện, thị xã, thành phố thuộc tỉnh; tỉnh, thành phố trực thuộc trung ương</sup>:
                                        ${value.DiaChi}.
                                    </p>
                                    <p>- Diện tích<sup>(9)</sup> <sup>Ghi diện tích đất (m2) theo giấy chứng nhận quyền sử dụng đất hoặc diện tích đo thực tế (nếu chưa có giấy chứng nhận quyền sử dụng đất)</sup>:
                                        ${currency(value.DienTich)}m<sup>2</sup>.
                                    </p>
                                    <p>- Giá trị<sup>(10)</sup> <sup>) Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do</sup>:
                                        ${currency(value.GiaTri)}VNĐ.
                                    </p>
                                    <p>- Giấy chứng nhận quyền sử dụng<sup>(11)</sup> <sup>Nếu thửa đất đã được cấp giấy chứng nhận quyền sử dụng đất thì ghi số giấy chứng nhận quyền sử dụng đất và tên người được cấp hoặc tên người đại diện (nếu là giấy chứng nhận quyền sử dụng đất chung của nhiều người); nếu thửa đất chưa được cấp giấy chứng nhận quyền sử dụng đất thì ghi “chưa được cấp giấy chứng nhận quyền sử dụng đất”</sup>:
                                        ${value.GiayChungNhan}.</p>
                                    <p>- Thông tin khác (nếu có)<sup>(12)</sup> <sup>Ghi cụ thể về tình trạng thực tế quản lý, sử dụng (ví dụ như người kê khai đứng tên đăng ký quyền sử dụng, quyền sở hữu nhưng thực tế là của người khác); tình trạng chuyển nhượng, sang tên và hiện trạng sử dụng như cho thuê, cho mượn,...</sup>:
                                        ${value.ThongTinKhac}.</p>`;
                    }
                })

                var datkhac = ``;
                $.each(data.datkhac, (index, value) => {
                    if (value == null) {
                        datkhac = `<p><strong>1.2.1. Thửa thứ nhất:</strong></p>
                                    <p>- Loại đất:................................ Địa chỉ:
                                        ..................................................................................</p>
                                    <p>- Diện tích:
                                        ..............................................................................................................................
                                    </p>
                                    <p>- Giá trị<sup>(10) Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do</sup>:
                                        .............................................................................................................................
                                    </p>
                                    <p>- Giấy chứng nhận quyền sử dụng:
                                        ......................................................................................</p>
                                    <p>- Th&ocirc;ng tin kh&aacute;c (nếu c&oacute;):
                                        .....................................................................................................</p>`;
                    } else {
                        datkhac = `<p><strong>1.2.${index + 1}. Thửa thứ ${index + 1}:</strong></p>
                                      <table style="width: 100%;">
                                            <tr >
                                                <td style="width: 50%;"><p>- Loại đất: ${value.TenLoaiDat}.</p></td>
                                                <td style="width: 50%;"><p>  Địa chỉ: ${value.DiaChi}.</p></td>
                                            </tr>
                                        </table>
                                    <p>- Diện tích:
                                        ${currency(value.DienTich)}m<sup>2</sup>.
                                    </p>
                                    <p>- Giá trị<sup>(10) Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do</sup>:
                                        ${currency(value.GiaTri)}VNĐ.
                                    </p>
                                    <p>- Giấy chứng nhận quyền sử dụng:
                                        ${value.GiayChungNhan}.</p>
                                    <p>- Th&ocirc;ng tin kh&aacute;c (nếu c&oacute;):
                                        ${value.ThongTinKhac}.</p>`;
                    }
                })
                var nhao = ``;
                $.each(data.nhao, (index, value) => {
                    if (value == null) {
                        nhao = ` <p><strong>2.1.1. Nh&agrave; thứ nhất</strong>:
                                    ...............................................................................................................</p>
                                <p>- Địa chỉ:
                                    .................................................................................................................................
                                </p>
                                <p>- Loại nh&agrave;<sup>(14) Ghi “căn hộ” nếu là căn hộ trong nhà tập thể, chung cư; ghi “nhà ở riêng lẻ” nếu là nhà được xây dựng trên thửa đất riêng biệt</sup>:
                                    ..........................................................................................................................
                                </p>
                                <p>- Diện tích sử dụng <sup>(15)</sup> <sup>Ghi tổng diện tích (m2) sàn xây dựng của tất cả các tầng của nhà ở riêng lẻ, biệt thự bao gồm cả các tầng hầm, tầng nửa hầm, tầng kỹ thuật, tầng áp mái và tầng mái tum. Nếu là căn hộ thì diện tích được ghi theo giấy chứng nhận quyền sở hữu hoặc hợp đồng mua, hợp đồng thuê của nhà nước.:</sup>
                                    ..........................................................................................................</p>
                                <p>- Giá trị<sup>(10)</sup> <sup>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do</sup>:
                                    ..............................................................................................................................
                                </p>
                                <p>- Giấy chứng nhận quyền sở hữu:
                                    .........................................................................................</p>
                                <p>- Thông tin khác (nếu c&oacute;):
                                    ......................................................................................................</p>`;
                    } else {
                        nhao = `<p><strong>2.1.${index + 1}. Nhà thứ ${index + 1}</strong>:
                                </p>
                                <p>- Địa chỉ:
                                    ${value.DiaChi}.
                                </p>
                                <p>- Loại nh&agrave;<sup>(14) Ghi “căn hộ” nếu là căn hộ trong nhà tập thể, chung cư; ghi “nhà ở riêng lẻ” nếu là nhà được xây dựng trên thửa đất riêng biệt</sup>:
                                    ${value.LoaiNha}.
                                </p>
                                <p>- Diện tích sử dụng <sup>(15)</sup> <sup>Ghi tổng diện tích (m2) sàn xây dựng của tất cả các tầng của nhà ở riêng lẻ, biệt thự bao gồm cả các tầng hầm, tầng nửa hầm, tầng kỹ thuật, tầng áp mái và tầng mái tum. Nếu là căn hộ thì diện tích được ghi theo giấy chứng nhận quyền sở hữu hoặc hợp đồng mua, hợp đồng thuê của nhà nước.:</sup>
                                    ${currency(value.DienTichSuDung)}m<sup>2</sup>.</p>
                                <p>- Giá trị<sup>(10)</sup> <sup>Giá trị là giá gốc tính bằng tiền Việt Nam, cụ thể: Trường hợp tài sản có được do mua, chuyển nhượng thì ghi số tiền thực tế phải trả khi mua hoặc nhận chuyển nhượng cộng với các khoản thuế, phí khác (nếu có); trường hợp tài sản có được do tự xây dựng, chế tạo, tôn tạo thì ghi tổng chi phí đã chi trả để hoàn thành việc xây dựng, chế tạo, tôn tạo cộng với phí, lệ phí (nếu có) tại thời điểm hình thành tài sản; trường hợp tài sản được cho, tặng, thừa kế thì ghi theo giá thị trường tại thời điểm được cho, tặng, thừa kế cộng với các khoản thuế, phí khác (nếu có) và ghi “giá trị ước tính”; trường hợp không thể ước tính giá trị tài sản vì các lý do như tài sản sử dụng đã quá lâu hoặc không có giao dịch đối với tài sản tương tự thì ghi “không xác định được giá trị” và ghi rõ lý do</sup>:
                                    ${currency(value.GiaTri)}VNĐ.
                                </p>
                                <p>- Giấy chứng nhận quyền sở hữu:
                                    ${value.GiayChungNhan}.</p>
                                <p>- Thông tin khác (nếu c&oacute;):
                                    ${value.ThongTinKhac}.</p>`;
                    }
                })
                var congtrinhxaydung = ``;
                $.each(data.congtrinhxaydung, (index, value) => {
                    if (value == null) {
                        congtrinhxaydung = `<p><strong>2.2.1. Công trình thứ nhất</strong>:</p>
                                            <p>- Tên công trình:........................................ Địa chỉ:
                                                ...............................................................</p>
                                            <p>- Loại công trình:............................................ Cấp công trình:
                                                ..............................................</p>
                                            <p>- Diện tích:
                                                .............................................................................................................................
                                            </p>
                                            <p>- Giá trị <sup>(10)</sup>:
                                                .............................................................................................................................
                                            </p>
                                            <p>- Giấy chứng nhận quyền sở hữu:
                                                .........................................................................................</p>
                                            <p>- Thông tin khác (nếu có):
                                                ......................................................................................................</p>`;
                    } else {
                        congtrinhxaydung = `<p> <strong>2.2.${index + 1}. Công trình thứ ${index + 1}</strong>:</p >
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 50%;"><p>- Tên công trình: ${value.TenCongTrinh}.</p></td>
                                                    <td style="width: 50%;"><p> Địa chỉ: ${value.DiaChi}.</p></p></td>
                                                </tr>
                                            </table>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 50%;"><p>- Loại công trình: ${value.LoaiCongTrinh}.</p></td>
                                                    <td style="width: 50%;"><p>Cấp công trình:  ${value.CapCongTrinh}.</p></td>
                                                </tr>
                                            </table>
                                            <p>- Diện tích:
                                                ${currency(value.DienTich)}m<sup>2</sup>.
                                            </p>
                                            <p>- Giá trị <sup>(10)</sup>:
                                                ${currency(value.GiaTri)}VNĐ.
                                            </p>
                                            <p>- Giấy chứng nhận quyền sở hữu:
                                                ${value.GiayChungNhan}.</p>
                                            <p>- Thông tin khác (nếu có):
                                                ${value.ThongTinKhac}.</p>`;
                    }
                })
                var caylaunam = ``;
                $.each(data.caylaunam, (index, value) => {
                    if (value == null) {
                        caylaunam = `
                                        <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 40%;"><p>- Loại cây:</p></td>
                                                    <td style="width: 30%;"><p> Số lượng:</p></td>
                                                    <td style="width: 30%;"><p> Giá trị<sup>(10)</sup>: </p></td>
                                                </tr>
                                            </table>`;
                    } else {
                        caylaunam = `<table style="width: 100%;">
                                        <tr>
                                            <td style="width: 40%;"><p>- Loại cây: ${value.TenTaiSan}</p></td>
                                            <td style="width: 30%;"><p>Số lượng: ${currency(value.SoLuong_DienTich)}</p></td>
                                            <td style="width: 30%;"><p> Giá trị<sup>(10)</sup>: ${currency(value.GiaTri)}VNĐ.</p></td>
                                        </tr>
                                    </table>`;
                    }
                })
                var rungsanxuat = ``;
                $.each(data.rungsanxuat, (index, value) => {
                    if (value == null) {
                        rungsanxuat = `
                                    <p>- Loại rừng:.......................................... Diện t&iacute;ch:................................. Gi&aacute;
                                        trị<sup>(10)</sup>: ...................</p>`;
                    } else {
                        rungsanxuat = `<table style="width: 100%;">
                                            <tr>
                                                <td style="width: 40%;"><p>- Loại rừng: ${value.TenTaiSan}</p></td>
                                                <td style="width: 30%;"><p>Diện t&iacute;ch: ${currency(value.SoLuong_DienTich)}m<sup>2</sup></p></td>
                                                <td style="width: 30%;"><p>  Giá trị<sup>(10)</sup>: ${currency(value.GiaTri)}VNĐ.</p></td>
                                            </tr>
                                        </table>`;
                    }
                })
                var vatkientruc = ``;
                $.each(data.vatkientruc, (index, value) => {
                    if (value == null) {
                        vatkientruc = `<p>- T&ecirc;n gọi:.................................... Số lượng:............................. Gi&aacute;
                                            trị<sup>(10)</sup>: ................................</p>
                                        <p>- T&ecirc;n gọi:.................................... Số lượng:............................. Gi&aacute;
                                            trị<sup>(10)</sup>: ................................</p>`;
                    } else {
                        vatkientruc = `<table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 50%;"><p>- T&ecirc;n gọi: ${value.TenTaiSan}</p></td>
                                                <td style="width: 30%;"><p> Số lượng: ${currency(value.SoLuong_DienTich)}</p></td>
                                                <td style="width: 30%;"><p>  Gi&aacute; trị<sup>(10)</sup>: ${currency(value.GiaTri)}VNĐ.</p></td>
                                            </tr>
                                        </table>
                                       `;
                    }
                })

                var kimloaidaquy = ``;
                $.each(data.kimloaidaquy, (index, value) => {
                    if (value == null) {
                        kimloaidaquy = ` <p>- T&ecirc;n gọi:....................................  Gi&aacute;
                                            trị<sup>(10)</sup>: ................................</p>
                                        <p>- T&ecirc;n gọi:.................................... Số lượng:............................. Gi&aacute;
                                            trị<sup>(10)</sup>: ................................</p>`;
                    } else {
                        kimloaidaquy = `<table style="width: 100%;">
                                            <tr>
                                                <td style="width: 50%;"><p>- T&ecirc;n gọi: ${value.TenTrangSuc}</p></td>
                                                <td style="width: 50%;"><p>Gi&aacute; trị<sup>(10)</sup>: ${currency(value.GiaTri)}VNĐ.</p></td>
                                            </tr>
                                        </table>
                                       `;
                    }
                })

                var tien = ``;
                $.each(data.tien, (index, value) => {
                    if (value == null) {
                        tien = ` <p>- T&ecirc;n gọi:....................................  Gi&aacute;
                                            trị<sup>(10)</sup>: ................................</p>
                                        <p>- T&ecirc;n gọi:.................................... Số lượng:............................. Gi&aacute;
                                            trị<sup>(10)</sup>: ................................</p>`;
                    } else {
                        tien = `<table style="width: 100%;">
                                    <tr>
                                        <td style="width: 50%;"><p>- T&ecirc;n gọi: ${value.TenLoaiTien}</p></td>
                                        <td style="width: 50%;"><p> Giá trị<sup>(10)</sup>: ${currency(value.GiaTri)}VNĐ.</p></td>
                                    </tr>
                                </table>
                                       `;
                    }
                })

                var cophieu = ``;
                $.each(data.cophieu, (index, value) => {
                    if (value == null) {
                        cophieu = `<p>- T&ecirc;n cổ phiếu:............................................ Số lượng:.......................... Gi&aacute; trị:
                                        .......................</p>
                                    <p>- T&ecirc;n cổ phiếu:............................................ Số lượng:.......................... Gi&aacute; trị:
                                        .......................</p>`;
                    } else {
                        cophieu = ` <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 40%;"><p>- Tên cổ phiếu: ${value.TenPhieu}</p></td>
                                            <td style="width: 30%;"><p>Số lượng: ${currency(value.SoLuong)}</p></td>
                                            <td style="width: 30%;"><p>Giá trị: ${currency(value.GiaTri)}VNĐ.</p></td>
                                        </tr>
                                    </table>
                                    `
                    }
                })
                var traiphieu = ``;
                $.each(data.traiphieu, (index, value) => {
                    if (value == null) {
                        traiphieu = `<p>- T&ecirc;n tr&aacute;i phiếu:............................... Số lượng:...........................
                                        Gi&aacute; trị: ......................</p>
                                    <p>- T&ecirc;n tr&aacute;i phiếu:........................................... Số lượng:...........................
                                        Gi&aacute; trị: ......................</p>`;
                    } else {
                        traiphieu = `<table style="width: 100%;">
                                        <tr>
                                            <td style="width: 40%;"> <p>- T&ecirc;n tr&aacute;i phiếu: ${value.TenPhieu}</p></td>
                                            <td style="width: 30%;"><p> Số lượng: ${currency(value.SoLuong)}</p></td>
                                            <td style="width: 30%;"><p>Gi&aacute; trị: ${currency(value.GiaTri)}VNĐ.</p></td>
                                        </tr>
                                    </table>
                                    `
                    }
                })

                var vongop = ``;
                $.each(data.vongop, (index, value) => {
                    if (value == null) {
                        cophieu = ` <p>- H&igrave;nh thức g&oacute;p vốn:......................................................... Gi&aacute;
                                    trị:.............................................</p>
                                    <p>- H&igrave;nh thức g&oacute;p vốn:......................................................... Gi&aacute;
                                    trị:.............................................</p>`;
                    } else {
                        vongop = `<table style="width: 100%;">
                                        <tr>
                                            <td style="width: 50%;"><p>- H&igrave;nh thức g&oacute;p vốn: ${value.TenPhieu}</p></td>
                                            <td style="width: 50%;"><p> Giá trị: ${currency(value.GiaTri)}VNĐ.</p></td>
                                        </tr>
                                    </table>
                                  `
                    }
                })

                var giayto = ``;
                $.each(data.giayto, (index, value) => {
                    if (value == null) {
                        giayto = ` <p>- H&igrave;nh thức g&oacute;p vốn:......................................................... Gi&aacute;
                                    trị:.............................................</p>
                                    <p>- H&igrave;nh thức g&oacute;p vốn:......................................................... Gi&aacute;
                                    trị:.............................................</p>`;
                    } else {
                        giayto = `<table style="width: 100%;">
                                        <tr>
                                            <td style="width: 50%;"> <p>- H&igrave;nh thức g&oacute;p vốn: ${value.TenPhieu}</p></td>
                                            <td style="width: 50%;"><p>Giá trị: ${currency(value.GiaTri)}VNĐ.</p></td>
                                        </tr>
                                    </table>
                                  `
                    }
                })

                var giaydangky = ``;
                $.each(data.giaydangky, (index, value) => {
                    if (value == null) {
                        giaydangky = `  <p>- T&ecirc;n t&agrave;i sản:................................... Số đăng k&yacute;:................................
                                        Gi&aacute; trị: ..........................</p>
                                    <p>- T&ecirc;n t&agrave;i sản:................................... Số đăng k&yacute;:................................
                                        Gi&aacute; trị: ..........................</p>`;
                    } else {
                        giaydangky = ` <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 40%;"> <p>- T&ecirc;n t&agrave;i sản: ${value.TenTaiSan}</p></td>
                                                <td style="width: 30%;"><p> Số đăng ký: ${value.SoDangKy_NamBatDauSuDung}</p></td>
                                                <td style="width: 30%;"><p>Gi&aacute; trị: ${currency(value.GiaTri)}VNĐ.</p></td>
                                            </tr>
                                        </table>
                                        `
                    }
                })
                
                var taisankhac = ``;
                $.each(data.taisankhac, (index, value) => {
                    if (value == null) {
                        taisankhac = `  <p>- T&ecirc;n t&agrave;i sản:................................... Số đăng k&yacute;:................................
                                        Gi&aacute; trị: ..........................</p>
                                    <p>- T&ecirc;n t&agrave;i sản:................................... Số đăng k&yacute;:................................
                                        Gi&aacute; trị: ..........................</p>`;
                    } else {
                        taisankhac = `<table style="width: 100%;">
                                        <tr>
                                            <td style="width: 40%;"> <p>- T&ecirc;n t&agrave;i sản: ${value.TenTaiSan}</td>
                                            <td style="width: 30%;">Số đăng k&yacute;: ${value.SoDangKy_NamBatDauSuDung}</td>
                                            <td style="width: 30%;">Gi&aacute; trị: ${currency(value.GiaTri)}VNĐ</p></td>
                                        </tr>
                                    </table>
                                        `
                    }
                })
                var taisannuocngoai = ``;
                $.each(data.taisannuocngoai, (index, value) => {
                    if (value == null) {
                        taisannuocngoai = ` <p>- T&ecirc;n t&agrave;i sản:................................... Số đăng k&yacute;:................................
                                                Gi&aacute; trị: ..........................</p>
                                            <p>- T&ecirc;n t&agrave;i sản:................................... Số đăng k&yacute;:................................
                                                Gi&aacute; trị: ..........................</p>`;
                    } else {
                        taisannuocngoai = `<table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 50%;"> <p>- T&ecirc;n t&agrave;i sản: ${value.TenTaiSanNuocNgoai}</p></td>
                                                    <td style="width: 50%;"><p> Giá trị: ${currency(value.GiaTri)}VNĐ</p></td>
                                                </tr>
                                            </table>
                                            `
                    }
                })

                var taikhoannuocngoai = ``;
                $.each(data.taikhoannuocngoai, (index, value) => {
                    if (value == null) {
                        taikhoannuocngoai = ` <p>- Tên chủ tài khoản:................................... Số tài khoản:................................
                                                Tên ngân hàng: ..........................</p>
                                            <p>- T&ecirc;n t&agrave;i sản:................................... Số đăng k&yacute;:................................
                                                Gi&aacute; trị: ..........................</p>`;
                    } else {
                        taikhoannuocngoai = `<table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 30%;"><p>- Tên chủ tài khoản: ${value.TenChuTaiKhoan}</td>
                                                    <td style="width: 30%;">Số tài khoản: ${value.SoTaiKhoan}</p></td>
                                                    <td style="width: 50%;"><p>Tên ngân hàng, chi nhánh ngân hàng, tổ chức nơi mở tài khoản: ${value.TenNganHang}</p></td>
                                                </tr>
                                            </table>`
                    }
                })

                var TongThuNhap_ConChuaThanhNien, TongThuNhap_NguoiKeKhai, TongThuNhap_VoHoacChong, TongThuNhap_CacKhoanChung = ``
                var taikhoannuocngoai = ``;
                $.each(data.tongthunhap, (index, value) => {
                    if (value == null) {
                       
                    } else {
                        TongThuNhap_CacKhoanChung = `${currency(value.TongThuNhap_CacKhoanChung)}VNĐ`
                        TongThuNhap_ConChuaThanhNien = `${currency(value.TongThuNhap_ConChuaThanhNien)}VNĐ`
                        TongThuNhap_NguoiKeKhai = `${currency(value.TongThuNhap_NguoiKeKhai)}VNĐ`
                        TongThuNhap_VoHoacChong = `${currency(value.TongThuNhap_VoHoacChong)}VNĐ`
                    }
                })
              

                var biendongtaisan_dato = " "
                var biendongtaisan_datkhac = " "
                var biendongtaisan_nhaocongtrinh = " "
                var biendongtaisan_congtrinhkhac = " "
                var biendongtaisan_caylaunam = " "
                var biendongtaisan_vatkientruc = " "
                var biendongtaisan_vangbacdaquy = " "
                var biendongtaisan_tien = " "
                var biendongtaisan_cophieu = " "
                var biendongtaisan_traiphieu = " "
                var biendongtaisan_vongop = " "
                var biendongtaisan_giaytokhac = " "
                var biendongtaisan_taisanqdpl = " "
                var biendongtaisan_taisankhac = " "
                var biendongtaisan_taisannuocngoai = " "
                var biendongtaisan_taikhoannuocngoai = " "
                var biendongtaisan_thunhap = " "

                biendongtaisan_dato = data.biendongtaisan.dato[0];
                biendongtaisan_datkhac = data.biendongtaisan.datkhac[0];
                biendongtaisan_nhaocongtrinh = data.biendongtaisan.nhaocongtrinh[0];
                biendongtaisan_congtrinhkhac = data.biendongtaisan.congtrinhkhac[0];
                biendongtaisan_caylaunam = data.biendongtaisan.caylaunam[0];
                biendongtaisan_vatkientruc = data.biendongtaisan.vatkientruc[0];
                biendongtaisan_vangbacdaquy = data.biendongtaisan.vangbacdaquy[0];
                biendongtaisan_tien = data.biendongtaisan.tien[0];
                biendongtaisan_cophieu = data.biendongtaisan.cophieu[0];
                biendongtaisan_traiphieu = data.biendongtaisan.traiphieu[0];
                biendongtaisan_vongop = data.biendongtaisan.vongop[0];
                biendongtaisan_giaytokhac = data.biendongtaisan.giaytokhac[0];
                biendongtaisan_taisanqdpl = data.biendongtaisan.taisanqdpl[0];
                biendongtaisan_taisankhac = data.biendongtaisan.taisankhac[0];
                biendongtaisan_taisannuocngoai = data.biendongtaisan.taisannuocngoai[0];
                biendongtaisan_taikhoannuocngoai = data.biendongtaisan.taikhoannuocngoai[0];
                biendongtaisan_thunhap = data.biendongtaisan.thunhap[0];

                $("#print_CBKK_body").empty();
                if (biendongtaisan_dato == undefined && biendongtaisan_datkhac == undefined &&
                    biendongtaisan_nhaocongtrinh == undefined && biendongtaisan_congtrinhkhac == undefined &&
                    biendongtaisan_caylaunam == undefined && biendongtaisan_vatkientruc == undefined &&
                    biendongtaisan_vangbacdaquy == undefined && biendongtaisan_tien == undefined &&
                    biendongtaisan_cophieu == undefined && biendongtaisan_traiphieu == undefined &&
                    biendongtaisan_vongop == undefined && biendongtaisan_giaytokhac == undefined &&
                    biendongtaisan_taisanqdpl == undefined && biendongtaisan_taisankhac == undefined &&
                    biendongtaisan_taisannuocngoai == undefined && biendongtaisan_taikhoannuocngoai == undefined &&
                    biendongtaisan_thunhap == undefined ) {
                    $("#print_CBKK_body").append(
                        `
                            <center>
                          
                            <table width="669">
                                <tbody>
                                    <tr>
                                        <td width="223">
                                            <center>
                                                <p><strong>${data.nguoikekhai.TenDonVi}<br />-------</strong></p>
                                            </center>
                                        </td>
                                        <td width="446">
                                            <center>
                                            <p><strong>CỘNG H&Ograve;A X&Atilde; HỘI CHỦ NGHĨA VIỆT NAM<br />Độc lập - Tự do - Hạnh ph&uacute;c
                                                    <br />---------------</strong></p>
                                            </center>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            
                            <p>&nbsp;</p>
                            <p><strong>BẢN KÊ; KHAI TÀI SẢN, THU NHẬP ${data.bankekhai.Ten_KeKhai.toUpperCase()}<sup>(1)
                                    </sup><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    (Ng&agrave;y ${ngay} th&aacute;ng ${thang} năm ${nam} )<sup>(2) </sup></strong><em><sup>Ng&agrave;y ho&agrave;n
                                        th&agrave;nh việc k&ecirc; khai</sup></em></p>
                            </center>
                            <p><sup>(1) Người c&oacute; nghĩa vụ k&ecirc; khai t&agrave;i sản, thu nhập ghi r&otilde; phương thức k&ecirc; khai theo
                                    quy định tại Điều 36 của Luật Ph&ograve;ng, chống tham nhũng (k&ecirc; khai lần đầu hay k&ecirc; khai hằng năm,
                                    k&ecirc; khai phục vụ c&ocirc;ng t&aacute;c c&aacute;n bộ). K&ecirc; khai lần đầu th&igrave; kh&ocirc;ng phải
                                    k&ecirc; khai Mục III &ldquo;Biến động t&agrave;i sản, thu nhập; giải tr&igrave;nh nguồn gốc của t&agrave;i sản,
                                    thu nhập tăng th&ecirc;m&rdquo;, kh&ocirc;ng tự &yacute; thay đổi t&ecirc;n gọi, thứ tự c&aacute;c nội dung quy
                                    định tại mẫu n&agrave;y. Người k&ecirc; khai phải k&yacute; ở từng trang v&agrave; k&yacute;, ghi r&otilde; họ
                                    t&ecirc;n ở trang cuối c&ugrave;ng của bản k&ecirc; khai. <u>Người k&ecirc; khai phải lập 02 bản k&ecirc; khai
                                        để b&agrave;n giao cho cơ quan, tổ chức, đơn vị quản l&yacute; m&igrave;nh</u> (01 bản b&agrave;n giao cho
                                    Cơ quan kiểm so&aacute;t t&agrave;i sản, thu nhập, 01 bản để phục vụ c&ocirc;ng t&aacute;c quản l&yacute; của cơ
                                    quan, tổ chức, đơn vị v&agrave; hoạt động c&ocirc;ng khai bản k&ecirc; khai). Người của cơ quan, tổ chức, đơn vị
                                    quản l&yacute; người c&oacute; nghĩa vụ k&ecirc; khai khi tiếp nhận bản k&ecirc; khai phải kiểm tra t&iacute;nh
                                    đầy đủ của c&aacute;c nội dung phải k&ecirc; khai. Sau đ&oacute; k&yacute; v&agrave; ghi r&otilde; họ t&ecirc;n,
                                    ng&agrave;y th&aacute;ng năm nhận bản k&ecirc; khai.</sup></p>      
                            <strong>I. THÔNG TIN CHUNG</strong><br/>
                            
                         
                           <strong>1. Người kê khai tài sản, thu nhập</strong>
                            
                           ${thongtinchung}
                           <br/>
                            <strong>2. Vợ hoặc chồng của người k&ecirc; khai t&agrave;i sản, thu nhập</strong>
                            <br/>
                            ${thongtinVoChong}
                         
                            <strong>3. Con chưa thành niên (con đẻ, con nuôi theo quy định của pháp luật)</strong>
                             
                           
                            ${thongtinCon}
                           <strong>II: THÔNG TIN MÔ TẢ VỀ TÀI SẢN<sup>(5) </sup></strong><sup> Tài sản phải kê khai là tài sản hiện có thuộc quyền sở hữu, quyền sử dụng của người kê khai, của vợ hoặc chồng và con đẻ, con nuôi (nếu có) chưa thành niên theo quy định của pháp luật.</sup>
                            <br/>
                           <strong>1. Quyền sử dụng thực tế đối với đất</strong><sup>(6) Quyền sử dụng thực tế đối với đất l&agrave;
                                        tr&ecirc;n thực tế người k&ecirc; khai c&oacute; quyền sử dụng đối với thửa đất bao gồm đất đ&atilde; được
                                        cấp hoặc chưa được cấp giấy chứng nhận quyền sử dụng đất.</sup>
                           
                            <p><strong>1.1. Đất ở</strong> <sup>(</sup><sup>7)</sup> <sup>Đất ở l&agrave; đất được sử dụng v&agrave;o mục
                                    đ&iacute;ch để ở theo quy định của ph&aacute;p luật về đất đai. Trường hợp thửa đất được sử dụng cho nhiều mục
                                    đ&iacute;ch kh&aacute;c nhau m&agrave; trong đ&oacute; c&oacute; đất ở th&igrave; k&ecirc; khai v&agrave;o mục
                                    đất ở</sup>:</p>
                            ${thuadat}
                           
                            <p><strong>1.2. Các loại đất khác </strong><sup>(13)</sup> <sup>K&ecirc; khai c&aacute;c loại đất
                                    c&oacute; mục đ&iacute;ch sử dụng kh&ocirc;ng phải l&agrave; đất ở theo quy định của Luật Đất đai:</sup></p>
                            ${datkhac}
                            
                           
                            <strong>2. Nhà ở, công trình xây dựng:</strong>
                            
                            <p><strong>2.1. Nh&agrave; ở:</strong></p>
                            ${nhao}
                            
                            <p><strong>2.2. Công trình xây dựng khác</strong><sup>(16)</sup> <sup>Công trình xây dựng khác là công trình xây dựng không phải nhà ở</sup>:
                            </p>
                            ${congtrinhxaydung}
                           
                            
                            
                            <strong> T&agrave;i sản kh&aacute;c gắn liền với đất<sup>(</sup><sup>17)</sup></strong> <sup>K&ecirc; khai những
                                        t&agrave;i sản gắn liền với đất m&agrave; c&oacute; tổng gi&aacute; trị mỗi loại ước t&iacute;nh từ 50 triệu
                                        trở l&ecirc;n</sup><strong>:</strong>

                           
                            <p><strong>3.1. C&acirc;y l&acirc;u năm</strong><sup>(18)</sup> <sup>C&acirc;y l&acirc;u năm l&agrave; c&acirc;y trồng
                                    một lần, sinh trưởng v&agrave; cho thu hoạch trong nhiều năm gồm: c&acirc;y c&ocirc;ng nghiệp l&acirc;u năm,
                                    c&acirc;y ăn quả l&acirc;u năm, c&acirc;y l&acirc;u năm lấy gỗ, c&acirc;y tạo cảnh, b&oacute;ng m&aacute;t.
                                    C&acirc;y m&agrave; thuộc rừng sản xuất th&igrave; kh&ocirc;ng ghi v&agrave;o mục n&agrave;y:</sup></p>
                           ${caylaunam}
                            <p><strong>3.2. Rừng sản xuất<sup>(</sup></strong><sup>19)</sup> <sup>Rừng sản xuất l&agrave; rừng trồng</sup>:</p>
                           ${rungsanxuat}
                            <p><strong>3.3. Vật kiến tr&uacute;c kh&aacute;c gắn liền với đất</strong>:</p>
                            ${vatkientruc}
                          
                        
                            <strong> V&agrave;ng, kim cương, bạch kim v&agrave; c&aacute;c kim loại qu&yacute;, đ&aacute; qu&yacute;
                                        kh&aacute;c c&oacute; tổng gi&aacute; trị từ 50 triệu đồng trở l&ecirc;n</strong><sup>(20)</sup> <sup>Ghi
                                        c&aacute;c loại v&agrave;ng, kim cương, bạch kim v&agrave; c&aacute;c kim loại qu&yacute;, đ&aacute;
                                        qu&yacute; kh&aacute;c c&oacute; tổng gi&aacute; trị từ 50 triệu đồng trở l&ecirc;n</sup>
                            ${kimloaidaquy}
                             <strong> Tiền (tiền Việt Nam, ngoại tệ) gồm tiền mặt, tiền cho vay, tiền trả trước, tiền gửi c&aacute;
                                        nh&acirc;n, tổ chức trong nước, tổ chức nước ngo&agrave;i tại Việt Nam m&agrave; tổng gi&aacute; trị quy đổi
                                        từ 50 triệu đồng trở l&ecirc;n </strong><sup>(21)</sup> <sup>Tiền (tiền Việt Nam, ngoại tệ) gồm tiền mặt,
                                        tiền cho vay, tiền trả trước, tiền gửi c&aacute; nh&acirc;n, tổ chức trong nước, tổ chức nước ngo&agrave;i
                                        tại Việt Nam m&agrave; tổng gi&aacute; trị quy đổi từ 50 triệu đồng trở l&ecirc;n. Nếu ngoại tệ th&igrave;
                                        ghi số lượng v&agrave; số tiền quy đổi ra tiền Việt Nam</sup>
                            ${tien}
                             <strong> Cổ phiếu, tr&aacute;i phiếu, vốn g&oacute;p, c&aacute;c loại giấy tờ c&oacute; gi&aacute; kh&aacute;c
                                        m&agrave; tổng gi&aacute; trị từ 50 triệu đồng trở l&ecirc;n (khai theo từng loại):</strong>
                            </ol>
                            <p>6.1. Cổ phiếu:</p>
                            ${cophieu}
                           
                            <p>6.2. Tr&aacute;i phiếu:</p>
                            ${traiphieu}
                            
                            <p>6.3. Vốn g&oacute;p<sup>(22)</sup> <sup>Ghi từng h&igrave;nh thức g&oacute;p vốn đầu tư kinh doanh, cả trực tiếp
                                    v&agrave; gi&aacute;n tiếp</sup>:</p>
                            ${vongop}
                           
                            <p>6.4. C&aacute;c loại giấy tờ c&oacute; gi&aacute; kh&aacute;c<sup>(23)</sup> <sup>C&aacute;c loại giấy tờ c&oacute;
                                    gi&aacute; kh&aacute;c như chứng chỉ quỹ, kỳ phiếu, s&eacute;c,...</sup>:</p>
                            ${giayto}
                           
                            <strong>7. T&agrave;i sản kh&aacute;c m&agrave; mỗi t&agrave;i sản c&oacute; gi&aacute; trị từ 50 triệu đồng trở
                                    l&ecirc;n, bao gồm:</strong>

                            <p>7.1. Tài sản theo quy định của pháp luật phải đăng ký sử dụng và được cấp giấy đăng ký (tầu bay, tầu thủy, thuyền, máy ủi, máy xúc, ô tô, mô tô, xe gắn máy...)<sup>(24)</sup> <sup>Ô tô, mô tô, xe gắn máy, xe máy (máy ủi, máy xúc, các loại xe máy khác), tầu thủy, tàu bay, thuyền và những động sản khác mà theo quy định phải đăng ký sử dụng và được cấp giấy đăng ký có giá trị mỗi loại từ 50 triệu đồng trở lên.</sup>:</p>
                               ${giaydangky}
                          
                            <p>7.2. T&agrave;i sản kh&aacute;c (đồ mỹ nghệ, đồ thờ c&uacute;ng, b&agrave;n ghế, c&acirc;y cảnh, tranh, ảnh,
                                c&aacute;c loại t&agrave;i sản kh&aacute;c) <sup>(25)</sup> <sup>C&aacute;c loại t&agrave;i sản kh&aacute;c như
                                    c&acirc;y cảnh, b&agrave;n ghế, tranh ảnh v&agrave; c&aacute;c loại t&agrave;i sản kh&aacute;c m&agrave;
                                    gi&aacute; trị quy đổi mỗi loại từ 50 triệu đồng trở l&ecirc;n</sup>:</p>
                            ${taisankhac}
                            <strong>8. T&agrave;i sản ở nước ngo&agrave;i<sup>(26)</sup></strong><sup> K&ecirc; khai t&agrave;i sản ở nước
                                        ngo&agrave;i phải k&ecirc; khai tất cả loại t&agrave;i sản nằm ngo&agrave;i l&atilde;nh thổ Việt Nam, tương
                                        tự mục 1 đến mục 7 của Phần II v&agrave; n&ecirc;u r&otilde; t&agrave;i sản đang ở nước
                                        n&agrave;o</sup>:
                            ${taisannuocngoai ? taisannuocngoai : `<br/>`}
                            
                            <strong>9. T&agrave;i khoản ở nước ngo&agrave;i<sup>(27)</sup></strong> <sup>K&ecirc; khai c&aacute;c t&agrave;i
                                        khoản mở tại ng&acirc;n h&agrave;ng ở nước ngo&agrave;i; c&aacute;c t&agrave;i khoản kh&aacute;c mở ở nước
                                        ngo&agrave;i kh&ocirc;ng phải l&agrave; t&agrave;i khoản ng&acirc;n h&agrave;ng nhưng c&oacute; thể thực
                                        hiện c&aacute;c giao dịch bằng tiền, t&agrave;i sản (như t&agrave;i khoản mở ở c&aacute;c c&ocirc;ng ty
                                        chứng kho&aacute;n nước ngo&agrave;i, s&agrave;n giao dịch v&agrave;ng nước ngo&agrave;i, v&iacute; điện tử
                                        ở nước ngo&agrave;i...).</sup>:
                            ${taikhoannuocngoai ? taikhoannuocngoai : `<br/>`}
                         
                            <strong>10. Tổng thu nhập giữa hai lần kê khai  <sup>(28)</sup></strong> <sup>Kê khai riêng tổng thu nhập của người kê khai, vợ hoặc chồng, con chưa thành niên. Trong trường hợp có những khoản thu nhập chung mà không thể tách riêng thì ghi tổng thu nhập chung vào phần các khoản thu nhập chung; nếu có thu nhập bằng ngoại tệ, tài sản khác thì quy đổi thành tiền Việt Nam (gồm các khoản lương, phụ cấp, trợ cấp, thưởng, thù lao, cho, tặng, biếu, thừa kế, tiền thu do bán tài sản, thu nhập hưởng lợi từ các khoản đầu tư, phát minh, sáng chế, các khoản thu nhập khác). Đối với kê khai lần đầu thì không phải kê khai tổng thu nhập giữa 02 lần kê khai. Đối với lần kê khai thứ hai trở đi được xác định từ ngày kê khai liền kề trước đó đến ngày trước ngày kê khai</sup>:
                            <p>- Tổng thu nhập của người kê khai:
                                ${TongThuNhap_NguoiKeKhai}.</p>
                            <p>- Tổng thu nhập của vợ (hoặc chồng):
                                 ${TongThuNhap_VoHoacChong}.</p>
                            <p>- Tổng thu nhập của con chưa thành niên:
                                ${TongThuNhap_ConChuaThanhNien}.</p>
                            <p>- Tổng các khoản thu nhập chung:
                                ${TongThuNhap_CacKhoanChung}.</p>
                           
                            `
                    );
                } else {
                    


                    console.log(biendongtaisan_dato)
                    //biendongtaisan_dato.TenLoaiTSTNJson
                    //biendongtaisan_datkhac.TenLoaiTSTNJson
                    //biendongtaisan_dato.SoLuongTaiSanJson
                    //biendongtaisan_datkhac.SoLuongTaiSanJson
                    //biendongtaisan_dato.GiaTriTSTNJson
                    //biendongtaisan_datkhac.NoiDungGiaiTrinhJson
                    //biendongtaisan_nhaocongtrinh.TenLoaiTSTNJson
                    //biendongtaisan_congtrinhkhac.TenLoaiTSTNJson
                    //biendongtaisan_nhaocongtrinh.SoLuongTaiSanJson
                    //biendongtaisan_nhaocongtrinh.NoiDungGiaiTrinhJson
                    //biendongtaisan_congtrinhkhac.NoiDungGiaiTrinhJson
                    //biendongtaisan_caylaunam.TenLoaiTSTNJson
                    //biendongtaisan_vatkientruc.TenLoaiTSTNJson
                    //biendongtaisan_caylaunam.SoLuongTaiSanJson
                    //biendongtaisan_vatkientruc.SoLuongTaiSanJson
                    //biendongtaisan_caylaunam.GiaTriTSTNJson
                    //biendongtaisan_vatkientruc.GiaTriTSTNJson
                    //biendongtaisan_caylaunam.NoiDungGiaiTrinhJson
                    //biendongtaisan_vatkientruc.NoiDungGiaiTrinhJson
                    //biendongtaisan_vangbacdaquy.TenLoaiTSTNJson
                    //biendongtaisan_vangbacdaquy.SoLuongTaiSanJson
                    //biendongtaisan_vangbacdaquy.GiaTriTSTNJson
                    //biendongtaisan_vangbacdaquy.NoiDungGiaiTrinhJson
                    //biendongtaisan_tien.TenLoaiTSTNJson
                    //biendongtaisan_tien.SoLuongTaiSanJson
                    //biendongtaisan_tien.GiaTriTSTNJson
                    //biendongtaisan_tien.NoiDungGiaiTrinhJson
                    //biendongtaisan_cophieu.TenLoaiTSTNJson
                    //biendongtaisan_traiphieu.TenLoaiTSTNJson
                    //biendongtaisan_vongop.TenLoaiTSTNJson
                    //biendongtaisan_giaytokhac.TenLoaiTSTNJson
                    //biendongtaisan_cophieu.SoLuongTaiSanJson
                    //biendongtaisan_traiphieu.SoLuongTaiSanJson
                    //biendongtaisan_vongop.SoLuongTaiSanJson
                    //biendongtaisan_giaytokhac.SoLuongTaiSanJson
                    //biendongtaisan_cophieu.GiaTriTSTNJson
                    //biendongtaisan_traiphieu.GiaTriTSTNJson
                    //biendongtaisan_vongop.GiaTriTSTNJson
                    //biendongtaisan_giaytokhac.GiaTriTSTNJson
                    //biendongtaisan_cophieu.NoiDungGiaiTrinhJson
                    //biendongtaisan_traiphieu.NoiDungGiaiTrinhJson
                    //biendongtaisan_vongop.NoiDungGiaiTrinhJson
                    //biendongtaisan_giaytokhac.NoiDungGiaiTrinhJson
                    //biendongtaisan_taisanqdpl.TenLoaiTSTNJson
                    //biendongtaisan_taisanqdpl.SoLuongTaiSanJson
                    //biendongtaisan_taisanqdpl.GiaTriTSTNJson
                    //biendongtaisan_taisanqdpl.NoiDungGiaiTrinhJson
                    //biendongtaisan_taisankhac.TenLoaiTSTNJson
                    //biendongtaisan_taisankhac.SoLuongTaiSanJson
                    //biendongtaisan_taisankhac.GiaTriTSTNJson
                    //biendongtaisan_taisankhac.NoiDungGiaiTrinhJson
                    //biendongtaisan_taisannuocngoai.TenLoaiTSTNJson
                    //biendongtaisan_taisannuocngoai.SoLuongTaiSanJson
                    //biendongtaisan_taisannuocngoai.GiaTriTSTNJson
                    //biendongtaisan_taisannuocngoai.NoiDungGiaiTrinhJson
                    //biendongtaisan_taikhoannuocngoai.TenLoaiTSTNJson
                    //biendongtaisan_taikhoannuocngoai.SoLuongTaiSanJson
                    //biendongtaisan_taikhoannuocngoai.GiaTriTSTNJson
                    //biendongtaisan_taikhoannuocngoai.NoiDungGiaiTrinhJson
                    //biendongtaisan_thunhap.TenLoaiTSTNJson
                    //biendongtaisan_thunhap.SoLuongTaiSanJson
                    //biendongtaisan_thunhap.GiaTriTSTNJson
                    //biendongtaisan_thunhap.NoiDungGiaiTrinhJson

                                        
                    $("#print_CBKK_body").append(
                        `
                            <center>
                          
                            <table width="669">
                                <tbody>
                                    <tr>
                                        <td width="223">
                                            <center>
                                                <p><strong>${data.nguoikekhai.TenDonVi}<br />-------</strong></p>
                                            </center>
                                        </td>
                                        <td width="446">
                                            <center>
                                            <p><strong>CỘNG H&Ograve;A X&Atilde; HỘI CHỦ NGHĨA VIỆT NAM<br />Độc lập - Tự do - Hạnh ph&uacute;c
                                                    <br />---------------</strong></p>
                                            </center>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            
                            <p>&nbsp;</p>
                            <p><strong>BẢN KÊ; KHAI TÀI SẢN, THU NHẬP ${data.bankekhai.Ten_KeKhai.toUpperCase()}<sup>(1)
                                    </sup><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    (Ng&agrave;y ${ngay} th&aacute;ng ${thang} năm ${nam} )<sup>(2) </sup></strong><em><sup>Ng&agrave;y ho&agrave;n
                                        th&agrave;nh việc k&ecirc; khai</sup></em></p>
                            </center>
                            <p><sup>(1) Người c&oacute; nghĩa vụ k&ecirc; khai t&agrave;i sản, thu nhập ghi r&otilde; phương thức k&ecirc; khai theo
                                    quy định tại Điều 36 của Luật Ph&ograve;ng, chống tham nhũng (k&ecirc; khai lần đầu hay k&ecirc; khai hằng năm,
                                    k&ecirc; khai phục vụ c&ocirc;ng t&aacute;c c&aacute;n bộ). K&ecirc; khai lần đầu th&igrave; kh&ocirc;ng phải
                                    k&ecirc; khai Mục III &ldquo;Biến động t&agrave;i sản, thu nhập; giải tr&igrave;nh nguồn gốc của t&agrave;i sản,
                                    thu nhập tăng th&ecirc;m&rdquo;, kh&ocirc;ng tự &yacute; thay đổi t&ecirc;n gọi, thứ tự c&aacute;c nội dung quy
                                    định tại mẫu n&agrave;y. Người k&ecirc; khai phải k&yacute; ở từng trang v&agrave; k&yacute;, ghi r&otilde; họ
                                    t&ecirc;n ở trang cuối c&ugrave;ng của bản k&ecirc; khai. <u>Người k&ecirc; khai phải lập 02 bản k&ecirc; khai
                                        để b&agrave;n giao cho cơ quan, tổ chức, đơn vị quản l&yacute; m&igrave;nh</u> (01 bản b&agrave;n giao cho
                                    Cơ quan kiểm so&aacute;t t&agrave;i sản, thu nhập, 01 bản để phục vụ c&ocirc;ng t&aacute;c quản l&yacute; của cơ
                                    quan, tổ chức, đơn vị v&agrave; hoạt động c&ocirc;ng khai bản k&ecirc; khai). Người của cơ quan, tổ chức, đơn vị
                                    quản l&yacute; người c&oacute; nghĩa vụ k&ecirc; khai khi tiếp nhận bản k&ecirc; khai phải kiểm tra t&iacute;nh
                                    đầy đủ của c&aacute;c nội dung phải k&ecirc; khai. Sau đ&oacute; k&yacute; v&agrave; ghi r&otilde; họ t&ecirc;n,
                                    ng&agrave;y th&aacute;ng năm nhận bản k&ecirc; khai.</sup></p>      
                            <strong>I. THÔNG TIN CHUNG</strong><br/>
                            
                         
                           <strong>1. Người kê khai tài sản, thu nhập</strong>
                            
                           ${thongtinchung}
                           <br/>
                            <strong>2. Vợ hoặc chồng của người k&ecirc; khai t&agrave;i sản, thu nhập</strong>
                            <br/>
                            ${thongtinVoChong}
                         
                            <strong>3. Con chưa thành niên (con đẻ, con nuôi theo quy định của pháp luật)</strong>
                             
                           
                            ${thongtinCon}
                           <strong>II: THÔNG TIN MÔ TẢ VỀ TÀI SẢN<sup>(5) </sup></strong><sup> Tài sản phải kê khai là tài sản hiện có thuộc quyền sở hữu, quyền sử dụng của người kê khai, của vợ hoặc chồng và con đẻ, con nuôi (nếu có) chưa thành niên theo quy định của pháp luật.</sup>
                            <br/>
                           <strong>1. Quyền sử dụng thực tế đối với đất</strong><sup>(6) Quyền sử dụng thực tế đối với đất l&agrave;
                                        tr&ecirc;n thực tế người k&ecirc; khai c&oacute; quyền sử dụng đối với thửa đất bao gồm đất đ&atilde; được
                                        cấp hoặc chưa được cấp giấy chứng nhận quyền sử dụng đất.</sup>
                           
                            <p><strong>1.1. Đất ở</strong> <sup>(</sup><sup>7)</sup> <sup>Đất ở l&agrave; đất được sử dụng v&agrave;o mục
                                    đ&iacute;ch để ở theo quy định của ph&aacute;p luật về đất đai. Trường hợp thửa đất được sử dụng cho nhiều mục
                                    đ&iacute;ch kh&aacute;c nhau m&agrave; trong đ&oacute; c&oacute; đất ở th&igrave; k&ecirc; khai v&agrave;o mục
                                    đất ở</sup>:</p>
                            ${thuadat}
                           
                            <p><strong>1.2. Các loại đất khác </strong><sup>(13)</sup> <sup>K&ecirc; khai c&aacute;c loại đất
                                    c&oacute; mục đ&iacute;ch sử dụng kh&ocirc;ng phải l&agrave; đất ở theo quy định của Luật Đất đai:</sup></p>
                            ${datkhac}
                            
                           
                            <strong>2. Nhà ở, công trình xây dựng:</strong>
                            
                            <p><strong>2.1. Nh&agrave; ở:</strong></p>
                            ${nhao}
                            
                            <p><strong>2.2. Công trình xây dựng khác</strong><sup>(16)</sup> <sup>Công trình xây dựng khác là công trình xây dựng không phải nhà ở</sup>:
                            </p>
                            ${congtrinhxaydung}
                           
                            
                            
                            <strong> T&agrave;i sản kh&aacute;c gắn liền với đất<sup>(</sup><sup>17)</sup></strong> <sup>K&ecirc; khai những
                                        t&agrave;i sản gắn liền với đất m&agrave; c&oacute; tổng gi&aacute; trị mỗi loại ước t&iacute;nh từ 50 triệu
                                        trở l&ecirc;n</sup><strong>:</strong>

                           
                            <p><strong>3.1. C&acirc;y l&acirc;u năm</strong><sup>(18)</sup> <sup>C&acirc;y l&acirc;u năm l&agrave; c&acirc;y trồng
                                    một lần, sinh trưởng v&agrave; cho thu hoạch trong nhiều năm gồm: c&acirc;y c&ocirc;ng nghiệp l&acirc;u năm,
                                    c&acirc;y ăn quả l&acirc;u năm, c&acirc;y l&acirc;u năm lấy gỗ, c&acirc;y tạo cảnh, b&oacute;ng m&aacute;t.
                                    C&acirc;y m&agrave; thuộc rừng sản xuất th&igrave; kh&ocirc;ng ghi v&agrave;o mục n&agrave;y:</sup></p>
                           ${caylaunam}
                            <p><strong>3.2. Rừng sản xuất<sup>(</sup></strong><sup>19)</sup> <sup>Rừng sản xuất l&agrave; rừng trồng</sup>:</p>
                           ${rungsanxuat}
                            <p><strong>3.3. Vật kiến tr&uacute;c kh&aacute;c gắn liền với đất</strong>:</p>
                            ${vatkientruc}
                          
                        
                            <strong> V&agrave;ng, kim cương, bạch kim v&agrave; c&aacute;c kim loại qu&yacute;, đ&aacute; qu&yacute;
                                        kh&aacute;c c&oacute; tổng gi&aacute; trị từ 50 triệu đồng trở l&ecirc;n</strong><sup>(20)</sup> <sup>Ghi
                                        c&aacute;c loại v&agrave;ng, kim cương, bạch kim v&agrave; c&aacute;c kim loại qu&yacute;, đ&aacute;
                                        qu&yacute; kh&aacute;c c&oacute; tổng gi&aacute; trị từ 50 triệu đồng trở l&ecirc;n</sup>
                            ${kimloaidaquy}
                             <strong> Tiền (tiền Việt Nam, ngoại tệ) gồm tiền mặt, tiền cho vay, tiền trả trước, tiền gửi c&aacute;
                                        nh&acirc;n, tổ chức trong nước, tổ chức nước ngo&agrave;i tại Việt Nam m&agrave; tổng gi&aacute; trị quy đổi
                                        từ 50 triệu đồng trở l&ecirc;n </strong><sup>(21)</sup> <sup>Tiền (tiền Việt Nam, ngoại tệ) gồm tiền mặt,
                                        tiền cho vay, tiền trả trước, tiền gửi c&aacute; nh&acirc;n, tổ chức trong nước, tổ chức nước ngo&agrave;i
                                        tại Việt Nam m&agrave; tổng gi&aacute; trị quy đổi từ 50 triệu đồng trở l&ecirc;n. Nếu ngoại tệ th&igrave;
                                        ghi số lượng v&agrave; số tiền quy đổi ra tiền Việt Nam</sup>
                            ${tien}
                             <strong> Cổ phiếu, tr&aacute;i phiếu, vốn g&oacute;p, c&aacute;c loại giấy tờ c&oacute; gi&aacute; kh&aacute;c
                                        m&agrave; tổng gi&aacute; trị từ 50 triệu đồng trở l&ecirc;n (khai theo từng loại):</strong>
                            </ol>
                            <p>6.1. Cổ phiếu:</p>
                            ${cophieu}
                           
                            <p>6.2. Tr&aacute;i phiếu:</p>
                            ${traiphieu}
                            
                            <p>6.3. Vốn g&oacute;p<sup>(22)</sup> <sup>Ghi từng h&igrave;nh thức g&oacute;p vốn đầu tư kinh doanh, cả trực tiếp
                                    v&agrave; gi&aacute;n tiếp</sup>:</p>
                            ${vongop}
                           
                            <p>6.4. C&aacute;c loại giấy tờ c&oacute; gi&aacute; kh&aacute;c<sup>(23)</sup> <sup>C&aacute;c loại giấy tờ c&oacute;
                                    gi&aacute; kh&aacute;c như chứng chỉ quỹ, kỳ phiếu, s&eacute;c,...</sup>:</p>
                            ${giayto}
                           
                            <strong>7. T&agrave;i sản kh&aacute;c m&agrave; mỗi t&agrave;i sản c&oacute; gi&aacute; trị từ 50 triệu đồng trở
                                    l&ecirc;n, bao gồm:</strong>

                            <p>7.1. Tài sản theo quy định của pháp luật phải đăng ký sử dụng và được cấp giấy đăng ký (tầu bay, tầu thủy, thuyền, máy ủi, máy xúc, ô tô, mô tô, xe gắn máy...)<sup>(24)</sup> <sup>Ô tô, mô tô, xe gắn máy, xe máy (máy ủi, máy xúc, các loại xe máy khác), tầu thủy, tàu bay, thuyền và những động sản khác mà theo quy định phải đăng ký sử dụng và được cấp giấy đăng ký có giá trị mỗi loại từ 50 triệu đồng trở lên.</sup>:</p>
                               ${giaydangky}
                          
                            <p>7.2. T&agrave;i sản kh&aacute;c (đồ mỹ nghệ, đồ thờ c&uacute;ng, b&agrave;n ghế, c&acirc;y cảnh, tranh, ảnh,
                                c&aacute;c loại t&agrave;i sản kh&aacute;c) <sup>(25)</sup> <sup>C&aacute;c loại t&agrave;i sản kh&aacute;c như
                                    c&acirc;y cảnh, b&agrave;n ghế, tranh ảnh v&agrave; c&aacute;c loại t&agrave;i sản kh&aacute;c m&agrave;
                                    gi&aacute; trị quy đổi mỗi loại từ 50 triệu đồng trở l&ecirc;n</sup>:</p>
                            ${taisankhac}
                            <strong>8. T&agrave;i sản ở nước ngo&agrave;i<sup>(26)</sup></strong><sup> K&ecirc; khai t&agrave;i sản ở nước
                                        ngo&agrave;i phải k&ecirc; khai tất cả loại t&agrave;i sản nằm ngo&agrave;i l&atilde;nh thổ Việt Nam, tương
                                        tự mục 1 đến mục 7 của Phần II v&agrave; n&ecirc;u r&otilde; t&agrave;i sản đang ở nước
                                        n&agrave;o</sup>:
                            ${taisannuocngoai ? taisannuocngoai : `<br/>`}
                            
                            <strong>9. T&agrave;i khoản ở nước ngo&agrave;i<sup>(27)</sup></strong> <sup>K&ecirc; khai c&aacute;c t&agrave;i
                                        khoản mở tại ng&acirc;n h&agrave;ng ở nước ngo&agrave;i; c&aacute;c t&agrave;i khoản kh&aacute;c mở ở nước
                                        ngo&agrave;i kh&ocirc;ng phải l&agrave; t&agrave;i khoản ng&acirc;n h&agrave;ng nhưng c&oacute; thể thực
                                        hiện c&aacute;c giao dịch bằng tiền, t&agrave;i sản (như t&agrave;i khoản mở ở c&aacute;c c&ocirc;ng ty
                                        chứng kho&aacute;n nước ngo&agrave;i, s&agrave;n giao dịch v&agrave;ng nước ngo&agrave;i, v&iacute; điện tử
                                        ở nước ngo&agrave;i...).</sup>:
                            ${taikhoannuocngoai ? taikhoannuocngoai : `<br/>`}
                         
                            <strong>10. Tổng thu nhập giữa hai lần kê khai  <sup>(28)</sup></strong> <sup>Kê khai riêng tổng thu nhập của người kê khai, vợ hoặc chồng, con chưa thành niên. Trong trường hợp có những khoản thu nhập chung mà không thể tách riêng thì ghi tổng thu nhập chung vào phần các khoản thu nhập chung; nếu có thu nhập bằng ngoại tệ, tài sản khác thì quy đổi thành tiền Việt Nam (gồm các khoản lương, phụ cấp, trợ cấp, thưởng, thù lao, cho, tặng, biếu, thừa kế, tiền thu do bán tài sản, thu nhập hưởng lợi từ các khoản đầu tư, phát minh, sáng chế, các khoản thu nhập khác). Đối với kê khai lần đầu thì không phải kê khai tổng thu nhập giữa 02 lần kê khai. Đối với lần kê khai thứ hai trở đi được xác định từ ngày kê khai liền kề trước đó đến ngày trước ngày kê khai</sup>:
                            <p>- Tổng thu nhập của người kê khai:
                                ${TongThuNhap_NguoiKeKhai}.</p>
                            <p>- Tổng thu nhập của vợ (hoặc chồng):
                                 ${TongThuNhap_VoHoacChong}.</p>
                            <p>- Tổng thu nhập của con chưa thành niên:
                                ${TongThuNhap_ConChuaThanhNien}.</p>
                            <p>- Tổng các khoản thu nhập chung:
                                ${TongThuNhap_CacKhoanChung}.</p>
                            <p><strong>III. BIẾN ĐỘNG TÀI SẢN, THU NHẬP; GIẢI TRÌNH NGUỒN GỐC CỦA TÀI SẢN, THU NHẬP TĂNG THÊM  <sup>(29)</sup></strong> <sup>Kê khai tài sản tăng hoặc giảm tại thời điểm kê khai so với tài sản đã kê khai trước đó và giải trình nguồn gốc của tài sản tăng thêm, nguồn hình thành thu nhập trong kỳ áp dụng với lần kê khai thứ hai trở đi. Nếu không có tăng, giảm tài sản thì ghi rõ là “Không có biến động” ngay sau tên của Mục III
                                </sup><strong><em>&nbsp;</em></strong><strong><em>(nếu là kê khai tài sản, thu nhập lần đầu thì không phải kê khai Mục này): </em></strong></p>
                            <table width="100%" style="border-collapse: collapse;">
                                <thead>
                                    <tr>
                                        <td rowspan="2" width="33%" style="border: 1px solid black">
                                            <p><strong>Loại t&agrave;i sản, thu nhập</strong></p>
                                        </td>
                                        <td colspan="2" width="34%" style="border: 1px solid black">
                                            <p><strong>Tăng <sup>(30)</sup></strong> <sup>Nếu t&agrave;i sản tăng th&igrave; ghi dấu cộng (+)
                                                    v&agrave; số lượng t&agrave;i sản tăng v&agrave;o cột &ldquo;số lượng t&agrave;i sản&rdquo;, ghi
                                                    gi&aacute; trị t&agrave;i sản tăng v&agrave;o cột &ldquo;gi&aacute; trị t&agrave;i sản, thu
                                                    nhập&rdquo; v&agrave; giải th&iacute;ch nguy&ecirc;n nh&acirc;n tăng v&agrave;o cột &ldquo;nội
                                                    dung giải tr&igrave;nh nguồn gốc của t&agrave;i sản tăng th&ecirc;m v&agrave; tổng thu
                                                    nhập&rdquo;</sup><strong>/ giảm <sup>(31)</sup></strong> <sup>Nếu t&agrave;i sản giảm th&igrave;
                                                    ghi dấu trừ (-) v&agrave;o cột &ldquo;số lượng t&agrave;i sản&rdquo;, ghi gi&aacute; trị
                                                    t&agrave;i sản giảm v&agrave;o cột &ldquo;gi&aacute; trị t&agrave;i sản, thu nhập&rdquo;
                                                    v&agrave; giải th&iacute;ch nguy&ecirc;n nh&acirc;n giảm t&agrave;i sản v&agrave;o cột
                                                    &ldquo;Nội dung giải tr&igrave;nh nguồn gốc của t&agrave;i sản tăng th&ecirc;m v&agrave; tổng
                                                    thu nhập&rdquo;</sup></p>
                                        </td>
                                        <td rowspan="2" width="33%" style="border: 1px solid black">
                                            <p><strong>Nội dung giải tr&igrave;nh nguồn gốc của t&agrave;i sản tăng th&ecirc;m v&agrave; tổng thu
                                                    nhập</strong></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" style="border: 1px solid black">
                                            <p><strong>Số lượng t&agrave;i sản</strong></p>
                                        </td>
                                        <td width="20%" style="border: 1px solid black">
                                            <p><strong>Gi&aacute; trị t&agrave;i sản, thu nhập</strong></p>
                                        </td>
                                        
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td width="43%" style="border: 1px solid black">
                                            <p>1. Quyền sử dụng thực tế đối với đất</p>
                                            1.1. Đất ở
                                            <p>- ${ biendongtaisan_dato === undefined ? "" : biendongtaisan_dato.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_dato.TenLoaiTSTNJson  }</p>
                                           1.2. C&aacute;c loại đất kh&aacute;c
                                             <p>- ${biendongtaisan_datkhac === undefined ? "" : biendongtaisan_datkhac.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_datkhac.TenLoaiTSTNJson }</p>
                                            
                                        </td>
                                        <td width="18%" style="border: 1px solid black">
                                            <p>${biendongtaisan_dato === undefined ? "" : biendongtaisan_dato.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_dato.SoLuongTaiSanJson }</p>
                                            <p>${biendongtaisan_datkhac === undefined ? "" : biendongtaisan_datkhac.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_datkhac.SoLuongTaiSanJson }</p>
                                        </td>
                                        <td width="22%" style="border: 1px solid black">
                                            <p>${biendongtaisan_dato === undefined ? "" : biendongtaisan_dato.GiaTriTSTNJson === undefined ? "" : biendongtaisan_dato.GiaTriTSTNJson }</p>
                                            <p>${biendongtaisan_datkhac === undefined ? "" : biendongtaisan_datkhac.GiaTriTSTNJson === undefined ? "" : biendongtaisan_datkhac.GiaTriTSTNJson }</p>
                                        </td>
                                        <td width="15%" style="border: 1px solid black">
                                            <p>${biendongtaisan_dato === undefined ? "" : biendongtaisan_dato.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_dato.NoiDungGiaiTrinhJson  }</p>
                                            <p>${biendongtaisan_datkhac === undefined ? "" : biendongtaisan_datkhac.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_datkhac.NoiDungGiaiTrinhJson }</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="43%" style="border: 1px solid black">
                                            <p>2. Nh&agrave; ở, c&ocirc;ng tr&igrave;nh x&acirc;y dựng</p>
                                            2.1. Nh&agrave; ở
                                            <p>- ${biendongtaisan_nhaocongtrinh === undefined ? "" : biendongtaisan_nhaocongtrinh.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_nhaocongtrinh.TenLoaiTSTNJson }</p>
                                           2.2. C&ocirc;ng tr&igrave;nh x&acirc;y dựng kh&aacute;c
                                            <p>- ${biendongtaisan_congtrinhkhac === undefined ? "" : biendongtaisan_congtrinhkhac.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_congtrinhkhac.TenLoaiTSTNJson }</p>
                                        </td>
                                        <td width="18%" style="border: 1px solid black">
                                            <p>${biendongtaisan_nhaocongtrinh === undefined ? "" : biendongtaisan_nhaocongtrinh.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_nhaocongtrinh.SoLuongTaiSanJson }</p>
                                            <p>${biendongtaisan_congtrinhkhac === undefined ? "" : biendongtaisan_congtrinhkhac.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_congtrinhkhac.SoLuongTaiSanJson  }</p>
                                        </td>
                                        <td width="22%" style="border: 1px solid black">
                                            <p>${biendongtaisan_nhaocongtrinh === undefined ? "" : biendongtaisan_nhaocongtrinh.GiaTriTSTNJson === undefined ? "" : biendongtaisan_nhaocongtrinh.GiaTriTSTNJson}</p>
                                            <p>${biendongtaisan_congtrinhkhac === undefined ? "" : biendongtaisan_congtrinhkhac.GiaTriTSTNJson === undefined ? "" : biendongtaisan_congtrinhkhac.GiaTriTSTNJson}</p>
                                        </td>
                                        <td width="15%" style="border: 1px solid black">
                                            <p>${biendongtaisan_nhaocongtrinh === undefined ? "" : biendongtaisan_nhaocongtrinh.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_nhaocongtrinh.NoiDungGiaiTrinhJson }</p>
                                            <p>${biendongtaisan_congtrinhkhac === undefined ? "" : biendongtaisan_congtrinhkhac.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_congtrinhkhac.NoiDungGiaiTrinhJson }</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="43%" style="border: 1px solid black">
                                            <p>3. T&agrave;i sản kh&aacute;c gắn liền với đất</p>
                                            3.1. C&acirc;y l&acirc;u năm, rừng sản xuất
                                            <p>- ${biendongtaisan_caylaunam === undefined ? "" : biendongtaisan_caylaunam.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_caylaunam.TenLoaiTSTNJson }</p>
                                            3.2. Vật kiến tr&uacute;c gắn liền với đất
                                            <p>- ${biendongtaisan_vatkientruc === undefined ? "" : biendongtaisan_vatkientruc.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_vatkientruc.TenLoaiTSTNJson }</p>
                                        </td>
                                        <td width="18%" style="border: 1px solid black">
                                            <p>${biendongtaisan_caylaunam === undefined ? "" : biendongtaisan_caylaunam.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_caylaunam.SoLuongTaiSanJson }</p>
                                            <p>${biendongtaisan_vatkientruc === undefined ? "" : biendongtaisan_vatkientruc.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_vatkientruc.SoLuongTaiSanJson}</p>
                                        </td>
                                        <td width="22%" style="border: 1px solid black">
                                            <p>${biendongtaisan_caylaunam === undefined ? "" : biendongtaisan_caylaunam.GiaTriTSTNJson === undefined ? "" : biendongtaisan_caylaunam.GiaTriTSTNJson }</p>
                                            <p>${biendongtaisan_vatkientruc === undefined ? "" : biendongtaisan_vatkientruc.GiaTriTSTNJson === undefined ? "" : biendongtaisan_vatkientruc.GiaTriTSTNJson }</p>
                                        </td>
                                        <td width="15%" style="border: 1px solid black">
                                            <p>${biendongtaisan_caylaunam === undefined ? "" : biendongtaisan_caylaunam.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_caylaunam.NoiDungGiaiTrinhJson }</p>
                                            <p>${biendongtaisan_vatkientruc === undefined ? "" : biendongtaisan_vatkientruc.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_vatkientruc.NoiDungGiaiTrinhJson }</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="43%" style="border: 1px solid black">
                                            4. V&agrave;ng, kim cương, bạch kim v&agrave; c&aacute;c kim loại qu&yacute;, đ&aacute; qu&yacute;
                                                kh&aacute;c c&oacute; tổng gi&aacute; trị từ 50 triệu đồng trở l&ecirc;n
                                            <p>- ${biendongtaisan_vangbacdaquy === undefined ? "" : biendongtaisan_vangbacdaquy.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_vangbacdaquy.TenLoaiTSTNJson }</p>
                                        </td>
                                        <td width="18%" style="border: 1px solid black">
                                            <p>${biendongtaisan_vangbacdaquy === undefined ? "" : biendongtaisan_vangbacdaquy.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_vangbacdaquy.SoLuongTaiSanJson }</p>
                                        </td>
                                        <td width="22%" style="border: 1px solid black">
                                            <p>${biendongtaisan_vangbacdaquy === undefined ? "" : biendongtaisan_vangbacdaquy.GiaTriTSTNJson === undefined ? "" : biendongtaisan_vangbacdaquy.GiaTriTSTNJson }</p>
                                        </td>
                                        <td width="15%" style="border: 1px solid black">
                                            <p>${biendongtaisan_vangbacdaquy === undefined ? "" : biendongtaisan_vangbacdaquy.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_vangbacdaquy.NoiDungGiaiTrinhJson }</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="43%" style="border: 1px solid black">
                                           5. Tiền (tiền Việt Nam, ngoại tệ) gồm tiền mặt, tiền cho vay, tiền trả trước, tiền gửi c&aacute;
                                                nh&acirc;n, tổ chức trong nước, tổ chức nước ngo&agrave;i tại Việt Nam m&agrave; tổng gi&aacute; trị
                                                quy đổi từ 50 triệu đồng trở l&ecirc;n.
                                            <p>- ${biendongtaisan_tien === undefined ? "" : biendongtaisan_tien.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_tien.TenLoaiTSTNJson }</p>
                                        </td>
                                        <td width="18%" style="border: 1px solid black">
                                            <p>${biendongtaisan_tien === undefined ? "" : biendongtaisan_tien.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_tien.SoLuongTaiSanJson }</p>
                                        </td>
                                        <td width="22%" style="border: 1px solid black">
                                            <p>${biendongtaisan_tien === undefined ? "" : biendongtaisan_tien.GiaTriTSTNJson === undefined ? "" : biendongtaisan_tien.GiaTriTSTNJson }</p>
                                        </td>
                                        <td width="15%" style="border: 1px solid black">
                                            <p>${biendongtaisan_tien === undefined ? "" : biendongtaisan_tien.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_tien.NoiDungGiaiTrinhJson }</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="43%" style="border: 1px solid black">
                                            <p>6. Cổ phiếu, tr&aacute;i phiếu, vốn g&oacute;p, c&aacute;c loại giấy tờ c&oacute; gi&aacute;
                                                kh&aacute;c m&agrave; tổng gi&aacute; trị từ 50 triệu đồng trở l&ecirc;n (khai theo từng loại):</p>
                                            6.1. Cổ phiếu
                                            <p>${biendongtaisan_cophieu === undefined ? "" : biendongtaisan_cophieu.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_cophieu.TenLoaiTSTNJson }</p>
                                            6.2. Tr&aacute;i phiếu
                                            <p>${biendongtaisan_traiphieu === undefined ? "" : biendongtaisan_traiphieu.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_traiphieu.TenLoaiTSTNJson }</p>
                                            6.3. Vốn g&oacute;p
                                            <p>${biendongtaisan_vongop === undefined ? "" : biendongtaisan_vongop.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_vongop.TenLoaiTSTNJson }</p>
                                            6.4. C&aacute;c loại giấy tờ c&oacute; gi&aacute; kh&aacute;c
                                            <p>${biendongtaisan_giaytokhac === undefined ? "" : biendongtaisan_giaytokhac.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_giaytokhac.TenLoaiTSTNJson}</p>
                                        </td>
                                        <td width="18%" style="border: 1px solid black">
                                            <p>${biendongtaisan_cophieu === undefined ? "" : biendongtaisan_cophieu.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_cophieu.SoLuongTaiSanJson }</p>
                                            <p>${biendongtaisan_traiphieu === undefined ? "" : biendongtaisan_traiphieu.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_traiphieu.SoLuongTaiSanJson }</p>
                                            <p>${biendongtaisan_vongop === undefined ? "" : biendongtaisan_vongop.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_vongop.SoLuongTaiSanJson}</p>
                                            <p>${biendongtaisan_giaytokhac === undefined ? "" : biendongtaisan_giaytokhac.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_giaytokhac.SoLuongTaiSanJson }</p>
                                        </td>
                                        <td width="22%" style="border: 1px solid black">
                                            <p>${biendongtaisan_cophieu === undefined ? "" : biendongtaisan_cophieu.GiaTriTSTNJson === undefined ? "" : biendongtaisan_cophieu.GiaTriTSTNJson }</p>
                                            <p>${biendongtaisan_traiphieu === undefined ? "" : biendongtaisan_traiphieu.GiaTriTSTNJson === undefined ? "" : biendongtaisan_traiphieu.GiaTriTSTNJson }</p>
                                            <p>${biendongtaisan_vongop === undefined ? "" : biendongtaisan_vongop.GiaTriTSTNJson === undefined ? "" : biendongtaisan_vongop.GiaTriTSTNJson }</p>
                                            <p>${biendongtaisan_giaytokhac === undefined ? "" : biendongtaisan_giaytokhac.GiaTriTSTNJson === undefined ? "" : biendongtaisan_giaytokhac.GiaTriTSTNJson}</p>
                                        </td>
                                        <td width="15%" style="border: 1px solid black">
                                            <p>${biendongtaisan_cophieu === undefined ? "" : biendongtaisan_cophieu.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_cophieu.NoiDungGiaiTrinhJson }</p>
                                            <p>${biendongtaisan_traiphieu === undefined ? "" : biendongtaisan_traiphieu.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_traiphieu.NoiDungGiaiTrinhJson}</p>
                                            <p>${biendongtaisan_vongop === undefined ? "" : biendongtaisan_vongop.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_vongop.NoiDungGiaiTrinhJson }</p>
                                            <p>${biendongtaisan_giaytokhac === undefined ? "" : biendongtaisan_giaytokhac.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_giaytokhac.NoiDungGiaiTrinhJson }</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="43%" style="border: 1px solid black">
                                            <p>7. T&agrave;i sản kh&aacute;c c&oacute; gi&aacute; trị từ 50 triệu đồng trở l&ecirc;n:</p>
                                            7.1. T&agrave;i sản theo quy định của ph&aacute;p luật phải đăng k&yacute; sử dụng v&agrave; được cấp
                                                giấy đăng k&yacute; (tầu bay, t&agrave;u thủy, thuyền, m&aacute;y ủi, m&aacute;y x&uacute;c, &ocirc;
                                                t&ocirc;, m&ocirc; t&ocirc;, xe gắn m&aacute;y...).
                                            <p>${biendongtaisan_taisanqdpl === undefined ? "" : biendongtaisan_taisanqdpl.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_taisanqdpl.TenLoaiTSTNJson }</p>
                                            7.2. T&agrave;i sản kh&aacute;c (đồ mỹ nghệ, đồ thờ c&uacute;ng, b&agrave;n ghế, c&acirc;y cảnh,
                                                tranh ảnh, c&aacute;c loại t&agrave;i sản kh&aacute;c).
                                            <p>${biendongtaisan_taisankhac === undefined ? "" : biendongtaisan_taisankhac.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_taisankhac.TenLoaiTSTNJson }</p>
                                        </td>
                                        <td width="18%" style="border: 1px solid black">
                                            <p>${biendongtaisan_taisanqdpl === undefined ? "" : biendongtaisan_taisanqdpl.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_taisanqdpl.SoLuongTaiSanJson }</p>
                                            <p>${biendongtaisan_taisankhac === undefined ? "" : biendongtaisan_taisankhac.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_taisankhac.SoLuongTaiSanJson }</p>
                                        </td>
                                        <td width="22%" style="border: 1px solid black">
                                            <p>${biendongtaisan_taisanqdpl === undefined ? "" : biendongtaisan_taisanqdpl.GiaTriTSTNJson === undefined ? "" : biendongtaisan_taisanqdpl.GiaTriTSTNJson }</p>
                                            <p>${biendongtaisan_taisankhac === undefined ? "" : biendongtaisan_taisankhac.GiaTriTSTNJson === undefined ? "" : biendongtaisan_taisankhac.GiaTriTSTNJson }</p>
                                        </td>
                                        <td width="15%" style="border: 1px solid black">
                                            <p>${biendongtaisan_taisankhac === undefined ? "" : biendongtaisan_taisankhac.GiaTriTSTNJson === undefined ? "" : biendongtaisan_taisankhac.GiaTriTSTNJson }</p>
                                            <p>${biendongtaisan_taisankhac === undefined ? "" : biendongtaisan_taisankhac.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_taisankhac.NoiDungGiaiTrinhJson }</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="43%" style="border: 1px solid black">
                                            8. T&agrave;i sản ở nước ngo&agrave;i.
                                            <p>${biendongtaisan_taisannuocngoai === undefined ? "" : biendongtaisan_taisannuocngoai.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_taisannuocngoai.TenLoaiTSTNJson}</p>
                                        </td>
                                        <td width="18%" style="border: 1px solid black">
                                            <p>${biendongtaisan_taisannuocngoai === undefined ? "" : biendongtaisan_taisannuocngoai.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_taisannuocngoai.SoLuongTaiSanJson }</p>
                                        </td>
                                        <td width="22%" style="border: 1px solid black">
                                            <p>${biendongtaisan_taisannuocngoai === undefined ? "" : biendongtaisan_taisannuocngoai.GiaTriTSTNJson === undefined ? "" : biendongtaisan_taisannuocngoai.GiaTriTSTNJson}</p>
                                        </td>
                                        <td width="15%" style="border: 1px solid black">
                                            <p>${biendongtaisan_taisannuocngoai === undefined ? "" : biendongtaisan_taisannuocngoai.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_taisannuocngoai.NoiDungGiaiTrinhJson }</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="43%" style="border: 1px solid black">
                                            9. T&agrave;i khoản ở nước ngo&agrave;i
                                            <p>${biendongtaisan_taikhoannuocngoai === undefined ? "" : biendongtaisan_taikhoannuocngoai.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_taikhoannuocngoai.TenLoaiTSTNJson}</p>
                                        </td>
                                        <td width="18%" style="border: 1px solid black">
                                            <p>${biendongtaisan_taikhoannuocngoai === undefined ? "" : biendongtaisan_taikhoannuocngoai.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_taikhoannuocngoai.SoLuongTaiSanJson  }</p>
                                        </td>
                                        <td width="22%" style="border: 1px solid black">
                                            <p>${biendongtaisan_taikhoannuocngoai === undefined ? "" : biendongtaisan_taikhoannuocngoai.GiaTriTSTNJson === undefined ? "" : biendongtaisan_taikhoannuocngoai.GiaTriTSTNJson }</p>
                                        </td>
                                        <td width="15%" style="border: 1px solid black">
                                            <p>${biendongtaisan_taikhoannuocngoai === undefined ? "" : biendongtaisan_taikhoannuocngoai.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_taikhoannuocngoai.NoiDungGiaiTrinhJson }</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="43%" style="border: 1px solid black">
                                            10. Tổng thu nhập giữa hai lần k&ecirc; khai<sup>(32)</sup> <sup>Ghi tổng thu nhập giữa 02 lần
                                                    k&ecirc; khai v&agrave;o cột &ldquo;gi&aacute; trị t&agrave;i sản, thu nhập&rdquo; v&agrave; ghi
                                                    r&otilde; từng khoản thu nhập c&oacute; được trong kỳ k&ecirc; khai</sup>.
                                            <p>${biendongtaisan_thunhap === undefined ? "" : biendongtaisan_thunhap.TenLoaiTSTNJson === undefined ? "" : biendongtaisan_thunhap.TenLoaiTSTNJson  }</p>
                                        </td>
                                        <td width="18%" style="border: 1px solid black">
                                            <p>${biendongtaisan_thunhap === undefined ? "" : biendongtaisan_thunhap.SoLuongTaiSanJson === undefined ? "" : biendongtaisan_thunhap.SoLuongTaiSanJson  }</p>
                                        </td>
                                        <td width="22%" style="border: 1px solid black">
                                            <p>${biendongtaisan_thunhap === undefined ? "" : biendongtaisan_thunhap.GiaTriTSTNJson === undefined ? "" : biendongtaisan_thunhap.GiaTriTSTNJson }</p>
                                        </td>
                                        <td width="15%" style="border: 1px solid black">
                                            <p>${biendongtaisan_thunhap === undefined ? "" : biendongtaisan_thunhap.NoiDungGiaiTrinhJson === undefined ? "" : biendongtaisan_thunhap.NoiDungGiaiTrinhJson  }</p>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                           
                            `
                    );
                }
                

            }
            else {
                $("#print_kkCB_body").empty();
                $("#print_kkCB_body").append("<p>lỗi</p>")
            }

        })
        .fail(function (data) {
            $("#print_kkCB_body").empty();
            $("#print_kkCB_body").append("<p>lỗi</p>")
        });

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

$("#printdsCanBoBS").click(() => {
    console.log("print")
    var divContents = $('#print_CBKK_body').html();
    if (typeof divContents === "undefined") return;
    var printWindow = window.open('', '', 'height=auto,width=auto');
    printWindow.document.write('<html>');
    printWindow.document.write('<head>');
    printWindow.document.write('<link rel="stylesheet" href="/Content/BieuMaus.css">');
    printWindow.document.write('<\/head>');
    printWindow.document.write('<body style="background:#fff; width:595px; margin: 0 auto;">');
    printWindow.document.write(divContents);
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.print();

})

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

