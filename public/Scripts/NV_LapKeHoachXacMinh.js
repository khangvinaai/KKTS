
    const idRadioAddNew = document.getElementById("idRadioAddNew"),
    idRadioEdit = document.getElementById("idRadioEdit")
    
    idRadioEdit.addEventListener("change", function () {
        $("#TenToXacMinh").prop("disabled", true);
        $("#ToTruong").prop("disabled", true);
        $("#DanhSachToXacMinh").prop("disabled", false);
    });

    idRadioAddNew.addEventListener("change", function () {
        $("#TenToXacMinh").prop("disabled", false);
        $("#ToTruong").prop("disabled", false);
        $("#DanhSachToXacMinh").prop("disabled", true);
    });

    var dataCanBo = ``

    var dataCoQuan = ``

    function FnSuccess(data) {
        Swal.fire({
            icon: 'success',
            title: 'Thành Công',
            text: `Lập kế hoạch xác minh thành công`,
            timer: 2000,
            showConfirmButton: false,
        }).then(() => {window.location.replace(data)})
        
}

var canbolist;
//Loading show danh sách cán bộ
$('#clsAddRowDanhSachCanBoDuocXacMinh').click((e) => {
    $('#clsAddRowDanhSachCanBoDuocXacMinh').prop('disabled', true);

    var processCanBo = 0;
    $('#textProcessCanBo').show()
    $('#process-loading-canbo').remove();
    $('#processCanBo').empty();
    $('#lableTyLe').remove();
    $('#processCoQuanDonVi').append(`<div class="progress" style=" border-radius: 10px;">
                                    <div class="progress-bar progress-bar-success progress-bar-striped" id="process-loading-coquan" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 0%; border-radius: 10px;">
                                        <span class="sr-only">40% Complete (success)</span>
                                    </div>
                                </div>`);

    // set interval
    var tid2 = setInterval(ProcessingCanBo, 50);

    // set interval
    function ProcessingCanBo() {
        $('#ModalCanBo').modal('show');
        processCanBo += 1;
        $("#loadingCanBo").width(processCanBo + "%");
        $("#loadingCanBo-b").html(processCanBo + "%");
        if (processCanBo >= 100) {
            abortTimerCanBo()
            $('#ModalCanBo').modal('hide');
        }
    }
    async function abortTimerCanBo() { // to be called when you want to stop the timer
        clearInterval(tid2);
        await $.each(await canbolist.data1, (index, value) => {
            console.log(value)
            $('#DanhSachCanBoDuocXacMinh').prepend(`<tr>
                                                    <td></td>
                                                    <td>
                                                        ${ value.HoTenCanBo}
                                                        <input type="text" class="form-group" name="Ma_CanBo_DuocXacMinh[]" hidden value="${ value.MaCanBo}"/>
                                                    </td>
                                                    <td>${ value.NgaySinh}</td>
                                                    <td>${( value.CCCD) ? value.CCCD : "" }</td>
                                                    <td>${  value.TenCoQuan }</td>
                                                    <td>${  value.ChucVu}</td>
                                                    <td><span class="btnDelete btn btn-outline-danger btn-sm"><i class="fas fa-trash"></i></span></td>
                                                </tr>`)
        })

        $("#next-page2").prop("disabled", false)
    }
})


//Loading show danh sách Cơ Quan
$('#clsAddRowDanhSachCoQuanDuocXacMinh').click((e) => {
    $('#clsAddRowDanhSachCoQuanDuocXacMinh').prop('disabled', true);
    var process = 0;
    $('#textProcessCoQuan').show()
    $('#processCoQuanDonVi').empty();
    $('#lableTyLe').remove();
    $('#processCoQuanDonVi').append(`<div class="progress" style=" border-radius: 10px;">
                                        <div class="progress-bar progress-bar-success progress-bar-striped" id="process-loading-coquan" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 0%; border-radius: 10px;">
                                            <span class="sr-only">40% Complete (success)</span>
                                        </div>
                                    </div>`);

    // set interval

    var tid = setInterval(ProcessingCoQuan, 50);
    function ProcessingCoQuan() {
        $('#ModalCoQuan').modal('show');
        process += 1;
        $("#loadingCoQuan").width(process+"%");
        $("#loadingCoQuan-b").html(process + "%");
        if (process >= 100) {
            
            abortTimer()
            $('#ModalCoQuan').modal('hide');
        }
    }
    async function abortTimer() { // to be called when you want to stop the timer
        clearInterval(tid);
        canbolist = await $.get("/DM_CanBo/GetDanhSachCanBoDuocXacMinh/", (data) => {

            $('#danhsachcanboduocxacminh').empty();
            $.each(data.data2, (index, value) => {
                $('#DanhSachCoQuanDuocXacMinh').prepend(`<tr>
                                                        <td></td>
                                                        <td>
                                                            ${value.Ten}
                                                            
                                                        </td>
                                                        <td>${value.SoDienThoai}</td>
                                                        <td>${(value.DiaChi) ? value.DiaChi + ", " : "" + value.Ten_PhuongXa + ", " + value.Ten_QuanHuyen + ", " + value.Ten_TinhThanh}</td>
                                                        <td>${value.MST}</td>
                                                        <td><span class="btnDelete btn btn-outline-danger btn-sm"><i class="fas fa-trash"></i></span></td>
                                                    </tr>`)
            })
        })
        $('#next-page1').prop("disabled", false);
    }
})

    
    $(document).ready(function () {
        
        $("#datatable-ToXacMinh").on('click', '.btnDelete', function () {
            $(this).closest('tr').remove();
        });

        $("#datatable-DanhSachCanBoDuocXacMinh").on('click', '.btnDelete', function () {
        $(this).closest('tr').remove();
        });

        $("#datatable-DanhSachCoQuanPhoiHop").on('click', '.btnDelete', function () {
        $(this).closest('tr').remove();
        });

        $("#datatable-ToXacMinh").on('change', '.Ma_CanBo_XacMinh', function () {
        $.get("/DM_CanBo/GetThongTinCanBoKeHoach", { id: $(this).val() }, (data) => {

            $(this).closest('tr').find('td').each(function (column, td) {

                if (column == 2) {
                    $(td).html(data[0].NgaySinh)
                }
                if (column == 3) {
                    $(td).html(data[0].DiaChi)
                }
                if (column == 4) {
                    $(td).html(data[0].CCCD)
                }
                if (column == 5) {
                    $(td).html(data[0].TenCoQuan)
                }
                if (column == 6) {
                    $(td).html(data[0].DiaChiCoQuan)
                }
            });
        })
    });

        $("#datatable-DanhSachCoQuanPhoiHop").on('change', '.MaCoQuan', function () {
        $.get("/DM_CoQuanDonVi/GetThongTinCoQuanKeHoach/", { id: $(this).val() }, (data) => {

            $(this).closest('tr').find('td').each(function (column, td) {

                if (column == 2) {
                    $(td).html(data[0].DiaChi)
                }
                if (column == 3) {
                    $(td).html(data[0].MaSoThue)
                }
                if (column == 4) {
                    $(td).html(data[0].SoDienThoai)
                }
                if (column == 5) {
                    $(td).html(data[0].Email)
                }
                if (column == 6) {
                    $(td).html(data[0].Website)
                }
            });
        })
    });

        $.get("/NV_LapKeHoachXacMinh/GetDanhSachToXacMinh/", (data) => {
        $('#DanhSachToXacMinh').append(`<option value="">Chọn tổ xác minh</option>`)

            $.each(data, (index, value) => {
        $('#DanhSachToXacMinh').append(`<option value="${value.MaToXacMinh}">${value.TenToXacMinh}`)
    })
        })



        $.get("/DM_CanBo/GetCanBoKeHoach/", (data) => {
        $('#ToTruong').append(`<option value="">Chọn tổ trưởng</option>`)
            $('#Ma_CanBo_XacMinh').append(`<option value="">Chọn cán bộ</option>`)
            dataCanBo = dataCanBo + `<option value="">Chọn cán bộ</option>`
            $.each(data, (index, value) => {
        $('#ToTruong').append(`<option value="${value.MaCanBo}">${value.TenCoQuan} | ${value.TenCanBo}</option>`)
                $('#Ma_CanBo_XacMinh').append(`<option value="${value.MaCanBo}">${value.TenCoQuan} | ${value.TenCanBo}</option>`)
                dataCanBo = dataCanBo + `<option value="${value.MaCanBo}">${value.TenCoQuan} | ${value.TenCanBo}</option>`
            })
        })

        $.get("/DM_CoQuanDonVi/GetCoQuanKeHoach/", (data) => {
        $('#MaCoQuan').append(`<option value="">Chọn cơ quan</option>`)
            dataCoQuan = dataCoQuan + `<option value="">Chọn cơ quan</option>`
            $.each(data, (index, value) => {
        $('#MaCoQuan').append(`<option value="${value.MaCoQuan}">${value.TenCoQuan}</option>`)
                dataCoQuan = dataCoQuan + `<option value="${value.MaCoQuan}">${value.TenCoQuan}</option>`
            })
        })

        $('.Ma_CanBo_XacMinh').select2()

        $('.MaCoQuan').select2()

        $('#DanhSachToXacMinh').select2().on("change", () => {
        $.get("/NV_LapKeHoachXacMinh/GetThongTinToXacMinh/", { id: $('#DanhSachToXacMinh').val() }, (data) => {
            $("#danhsachtoxacminh").empty()
            $.each(data, (index, value) => {
                if (value.ChucVu == "Tổ Trưởng") {
                    $("#danhsachtoxacminh").append(` <tr class="remove">
                                                <td></td>
                                                <td>
                                                     ${value.HoTenCanBo}
                                                    <input type="text" name="Ma_CanBo_XacMinh[]" hidden value="${value.MaCanBo}"/>
                                                    <input type="text" name="ChucVu_CanBo_XacMinh[]" hidden value="${value.ChucVu}"/>
                                                </td>
                                                <td>${value.NgaySinh}</td>
                                                <td>${value.DiaChi}</td>
                                                <td>${value.CCCD}</td>
                                                <td>${value.TenCoQuan}</td>
                                                <td>${value.DiaChiCoQuan}</td>
                                                <td>${value.ChucVu}</td>
                                                <td><span class="btnDelete btn btn-outline-danger btn-sm"><i class="fas fa-trash"></i></span></td>
                                            </tr>`)
                }
                else {
                    $("#danhsachtoxacminh").append(` <tr>
                                                <td></td>
                                                <td>
                                                     ${value.HoTenCanBo}
                                                    <input type="text" name="Ma_CanBo_XacMinh[]" hidden value="${value.MaCanBo}"/>
                                                    <input type="text" name="ChucVu_CanBo_XacMinh[]" hidden value="${value.ChucVu}"/>
                                                </td>
                                                <td>${value.NgaySinh}</td>
                                                <td>${value.DiaChi}</td>
                                                <td>${value.CCCD}</td>
                                                <td>${value.TenCoQuan}</td>
                                                <td>${value.DiaChiCoQuan}</td>
                                                <td>${value.ChucVu}</td>
                                                <td><span class="btnDelete btn btn-outline-danger btn-sm"><i class="fas fa-trash"></i></span></td>
                                            </tr>`)
                }



            })
        })
    })

        $('#ToTruong').select2().on("change", function () {
        $('.remove').remove()

            $.get("/DM_CanBo/GetThongTinCanBoKeHoach", {id: $('#ToTruong').val() }, (data) => {
        $('#danhsachtoxacminh').prepend(`<tr class='remove'>
                                            <td></td>
                                            <td>
                                                ${data[0].HoTenCanBo}
                                                <input type="text" name="Ma_CanBo_XacMinh[]" hidden value="${data[0].MaCanBo}"/>
                                                <input type="text" name="ChucVu_CanBo_XacMinh[]" hidden value="Tổ Trưởng"/>
                                            </td>
                                            <td>${data[0].NgaySinh}</td>
                                            <td>${data[0].DiaChi}</td>
                                            <td>${data[0].CCCD}</td>
                                            <td>${data[0].TenCoQuan}</td>
                                            <td>${data[0].DiaChiCoQuan}</td>
                                            <td>Tổ Trưởng</td>
                                            <td><span class="btnDelete btn btn-outline-danger btn-sm"><i class="fas fa-trash"></i></span></td>
                                        </tr>`)
    })
        })

        $('#clsAddRowThanhVienToXacMinh').click(() => {

        $("#danhsachtoxacminh").append(` <tr>
                                            <td></td>
                                            <td>
                                                <select class="form-control Ma_CanBo_XacMinh" name="Ma_CanBo_XacMinh[]" id="Ma_CanBo_XacMinh" style="width: 100%; height: 100%">
                                                    ${dataCanBo}
                                                </select>
                                                <input type="text" name="ChucVu_CanBo_XacMinh[]" hidden value="Thành Viên"/>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>Thành viên</td>
                                            <td><span class="btnDelete btn btn-outline-danger btn-sm"><i class="fas fa-trash"></i></span></td>
                                        </tr>`)
            $(".Ma_CanBo_XacMinh").select2();
        })

    //    $('#clsAddRowDanhSachCanBoDuocXacMinh').click(() => {
    //    $.get("/DM_CanBo/GetDanhSachCanBoDuocXacMinh/", (data) => {
    //        $('#danhsachcanboduocxacminh').empty();
    //        $.each(data, (index, value) => {
    //            $('#danhsachcanboduocxacminh').prepend(`<tr>
    //                                                        <td></td>
    //                                                        <td>
    //                                                            ${value.HoTenCanBo}
    //                                                            <input type="text" name="Ma_CanBo_DuocXacMinh[]" hidden value="${value.MaCanBo}"/>
    //                                                        </td>
    //                                                        <td>${value.NgaySinh}</td>
    //                                                        <td>${value.DiaChi}</td>
    //                                                        <td>${value.CCCD}</td>
    //                                                        <td>${value.TenCoQuan}</td>
    //                                                        <td>${value.DiaChiCoQuan}</td>
    //                                                        <td>${value.ChucVu}</td>
    //                                                        <td><span class="btnDelete btn btn-outline-danger btn-sm"><i class="fas fa-trash"></i></span></td>
    //                                                    </tr>`)
    //        })
    //    })
    //})

        $('#clsAddRowDanhSachCoQuanPhoiHop').click(() => {
        $("#danhsachcoquanphoihop").append(` <tr>
                                                <td></td>
                                                <td>
                                                    <select class="form-control MaCoQuan" name="MaCoQuan[]" id="MaCoQuan" style="width: 100%; height: 100%">
                                                        ${dataCoQuan}
                                                    </select>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td><span class="btnDelete btn-outline-danger btn-sm delete" data-model-id="16" onclick=""><i class="fas fa-trash"></i></span></td>
                                            </tr>`)
            $('.MaCoQuan').select2()
        })


    })
