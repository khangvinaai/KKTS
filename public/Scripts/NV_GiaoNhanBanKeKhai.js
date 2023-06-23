const params = window.location.pathname.split('/');
const MaKeHoachKeKhai = params[params.length - 1];

var MaCoQuan = 0;
var MaCanBoBenGiao = 0;
var MaCanBoBenNhan = 0;




$.ajax({
    type: 'POST',
    dataType: "json",
    url: "/NV_GiaoNhanBanKeKhai/loadEditGiaoNhanBanKeKhai",
    data: { MaKeHoachKeKhai: MaKeHoachKeKhai }
}).done(function (data) {
    
    var dateCVN = new Date(Number(data.NgayCongVan.match(/\d+/)[0]));
    var dateCongVanNgay = moment(dateCVN).format("YYYY-MM-DD")

    var dateNGN = new Date(Number(data.NgayGiaoNhan.match(/\d+/)[0]));
    var dateNgayGiaoNhan = moment(dateNGN).format("YYYY-MM-DD") + "T" + moment(dateNGN).format("hh:mm:ss")
    console.log(dateNgayGiaoNhan)
    if (data != null) {
        $("#CongVanSo").val(`${data.CongVanSo}`);
        $("#spanCongVanSo").text(`${data.CongVanSo}`);
        $("#NamKeKhai").val(`${data.NamKeKhai}`);
        $("#CongVanNgay").val(dateCongVanNgay);

        $("#spanNgayCongVan").text(moment(dateCVN).format("DD"));
        $("#spanThangCongVan").text(moment(dateCVN).format("MM"));
        $("#spanNamCongVan").text(moment(dateCVN).format("YYYY"));

        $("#NgayGiaoNhan").val(dateNgayGiaoNhan);
        $("#spanGioGiaoNhan").text(moment(dateNGN).format("hh") + " giờ " + moment(dateNGN).format("mm") + " phút, ");
  
        
        $("#spanNamGiaoNhan").text(moment(dateNGN).format("YYYY"));
        $("#spanThangGiaoNhan").text(moment(dateNGN).format("MM"));
        $("#spanNgayGiaoNhan").text(moment(dateNGN).format("DD"));
        

        $("#DiaDiemGiaoNhan").val(`${data.DiaDiemGiaoNhan}`);
        $("#spanDiaDiemGiaoNhan").text(`${data.DiaDiemGiaoNhan}`);
        
        $("#SoLuongBanKeKhaiHangNam").val(`${data.SoLuongKeKhaiHangNam}`);
        $("#LyDoKeKhaiHangNam").val(`${data.LyDoKeKhaiHangNam}`);
        $("#TinhTrangTaiLieuHangNam").val(`${data.TinhTrangTaiLieuHangNam}`);
        $("#SoLuongBanKeKhaiBoSung").val(`${data.SoLuongKeKhaiBoSung}`);
        $("#LyDoKeKhaiBoSung").val(`${data.LyDoKeKhaiBoSung}`);
        $("#TinhTrangTaiLieuBoSung").val(`${data.TinhTrangTaiLieuBoSung}`);

        $.get("/DM_CanBo/GetCanBoByID/", { MaCanBo: data.Ma_CanBo_BenGiao }, (data) => {
            $("#spanCanBoGiao").text(data.TenCanBo);
        })



        $.get("/DM_CanBo/GetCanBoByID/", { MaCanBo: data.Ma_CanBo_BenNhan }, (data) => {
            $("#spanCanBoNhan").text(data.TenCanBo);
        })

        MaCoQuan = data.Ma_CoQuan_DonVi_BenGiao;
        MaCanBoBenGiao = data.Ma_CanBo_BenGiao;
        MaCanBoBenNhan = data.Ma_CanBo_BenNhan;



    } else {

    }

}).fail(function () {
    alert("Giao nhận bản kê khai chưa có bản ghi nào, hãy điền thông tin và lưu để tạo mới bản ghi");
})



$("#CongVanSo").keyup(function () {
    $("#spanCongVanSo").text($("#CongVanSo").val());
});

$("#CongVanNgay").change(function () {
    var dateInput = new Date($("#CongVanNgay").val());
    $("#spanNgayCongVan").text(dateInput.getDate());
    $("#spanThangCongVan").text(dateInput.getMonth() + 1);
    $("#spanNamCongVan").text(dateInput.getFullYear());
});

$("#DiaDiemGiaoNhan").keyup(function () {
    $("#spanDiaDiemGiaoNhan").text($("#DiaDiemGiaoNhan").val());
});

$("#NgayGiaoNhan").change(function () {
    var dateInput = new Date($("#NgayGiaoNhan").val());
    $("#spanNgayGiaoNhan").text(dateInput.getDate());
    $("#spanThangGiaoNhan").text(dateInput.getMonth() + 1);
    $("#spanNamGiaoNhan").text(dateInput.getFullYear());
    $("#spanGioGiaoNhan").text(dateInput.getHours() + ' giờ ' + dateInput.getMinutes() + ' phút,');
});
$("#spanCoQuanGiao").text($("#CoQuanBenGiao").val());


$("#spanCoQuanBenNhan").text($("#CoQuanBenNhan").val());

$("#Ma_CanBo_BenNhan").change(function () {
    var MaCanBo = $(this).val()
    $.get("/DM_CanBo/GetCanBoByID/", { MaCanBo: MaCanBo }, (data) => {
        $("#Ma_CoQuan_DonVi_BenNhan").val(data.MaCoQuanDonVi)
        $("#spanCanBoNhan").text(` ${data.TenCanBo}`);
        $("#spanChucVuBenNhan").text(` ${data.TenChucVu} Tại cơ quan ${data.tenCoQuanDonVi}`);
        $("#ChucVuBenNhan").text(` ${data.TenChucVu} `);
        console.log($("#Ma_CoQuan_DonVi_BenNhan").val())
    })
});

$("#SoLuongBanKeKhaiHangNam").keyup(function () {
    $("#spanSoLuongKeKhaiHangNam").text($("#SoLuongBanKeKhaiHangNam").val() + ' bản');
});
$("#LyDoKeKhaiHangNam").keyup(function () {
    $("#LyDoKhongBanGiaoKKDuHangNam").text($("#LyDoKeKhaiHangNam").val());
});
$("#TinhTrangTaiLieuHangNam").keyup(function () {
    $("#TinhTrangGiaoBanKKHangNam").text($("#TinhTrangTaiLieuHangNam").val());
});
// 
$("#SoLuongBanKeKhaiBoSung").keyup(function () {
    $("#spanSoLuongKeKhaiBoSung").text($("#SoLuongBanKeKhaiBoSung").val() + ' bản');
});
$("#LyDoKeKhaiBoSung").keyup(function () {
    $("#LyDoKhongBanGiaoKKDuBoSung").text($("#LyDoKeKhaiBoSung").val());
});
$("#TinhTrangTaiLieuBoSung").keyup(function () {
    $("#TinhTrangGiaoBanKKBoSung").text($("#TinhTrangTaiLieuBoSung").val());
});

$("#NamKeKhai").keyup(function () {
    $("#spanNamKeKhai1").text($("#NamKeKhai").val());
    $("#spanNamKeKhai2").text($("#NamKeKhai").val());
    $("#spanNamKeKhai3").text($("#NamKeKhai").val());
    $("#spanNamKeKhai4").text($("#NamKeKhai").val());
    $("#spanNamKeKhai5").text($("#NamKeKhai").val());
    $("#spanNamKeKhai6").text($("#NamKeKhai").val());
    $("#spanNamKeKhai7").text($("#NamKeKhai").val());
});


$("#btnprintBaoCao").click((e) => {
    //var yourUrl = "/NV_GiaoNhanBanKeKhai/inGiaoNhanBanKeKhai/35";
    //var opts = 'width=700,height=500,toolbar=0,menubar=0,location=1,status=1,scrollbars=1,resizable=1,left=0,top=0';
    //var newWindow = window.open(yourUrl, 'GiaoNhanBanKeKhai', opts);
    //setTimeout(function () { newWindow.print(); }, 3000)


    console.log("print")
    var divContents = $('#MaBienBan').html();
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


function FnSuccess(data) {
    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Đã lưu biên bản giao nhận`,
        timer: 2000,
        showConfirmButton: false,
    }).then(() => {
        location.reload()
    })
}

function Failure(data) {
    Swal.fire({
        icon: 'error',
        title: 'Có lỗi xảy ra',
        text: 'Vui lòng kiểm tra lại',
        timer: 2000,
        showConfirmButton: false,

    })
}


$.ajax({
    type: 'POST',
    dataType: "json",
    url: "/NV_GiaoNhanBanKeKhai/loadDanhSachBanKeKhai",
    data: { MaKeHoachKeKhai: MaKeHoachKeKhai}

}).done(function (data) {
    var indexi = 1
    var indexj = 1;

    $.each(data, (index, item) => {
        
        if (item.Ma_Loai_KeKhai == 4) {
          
            $("#dsKeKhaiHN").append(`<tr>
                                <td style="width:33.3pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${indexi}</p>
                                </td>
                                <td style="width:120.95pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.HoTen}</p>
                                </td>
                                <td style="width:85.15pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.Ten_ChucVu_ChucDanh}</p>
                                </td>
                                <td style="width:163.8pt; border-top-style:solid; border-top-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.Ten}</p>
                                </td>
                            </tr>`)
            ++indexi;
        } else if (item.Ma_Loai_KeKhai == 5) {
            $("#dsKeKhaiBS").append(`<tr>
                                <td style="width:33.3pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${indexj}</p>
                                </td>
                                <td style="width:120.95pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.HoTen}</p>
                                </td>
                                <td style="width:85.15pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.Ten_ChucVu_ChucDanh}</p>
                                </td>
                                <td style="width:163.8pt; border-top-style:solid; border-top-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.Ten}</p>
                                </td>
                            </tr>`)
            ++indexj;
        }
        
    })
    
}).fail(function () {
    alert("error");
})



$.ajax({
    type: 'POST',
    dataType: "json",
    url: "/NV_GiaoNhanBanKeKhai/LoadDataEdit",
    data: { MaKeHoachKeKhai: MaKeHoachKeKhai }

}).done(function (data) {
    var indexi = 1
    var indexj = 1;

    $.each(data, (index, item) => {

        if (item.Ma_Loai_KeKhai == 4) {

            $("#dsKeKhaiHN").append(`<tr>
                                <td style="width:33.3pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${indexi}</p>
                                </td>
                                <td style="width:120.95pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.HoTen}</p>
                                </td>
                                <td style="width:85.15pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.Ten_ChucVu_ChucDanh}</p>
                                </td>
                                <td style="width:163.8pt; border-top-style:solid; border-top-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.Ten}</p>
                                </td>
                            </tr>`)
            ++indexi;
        } else if (item.Ma_Loai_KeKhai == 5) {
            $("#dsKeKhaiBS").append(`<tr>
                                <td style="width:33.3pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${indexj}</p>
                                </td>
                                <td style="width:120.95pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.HoTen}</p>
                                </td>
                                <td style="width:85.15pt; border-top-style:solid; border-top-width:0.75pt; border-right-style:solid; border-right-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-right:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.Ten_ChucVu_ChucDanh}</p>
                                </td>
                                <td style="width:163.8pt; border-top-style:solid; border-top-width:0.75pt; border-left-style:solid; border-left-width:0.75pt; padding-right:5.03pt; padding-left:5.03pt; vertical-align:top; -aw-border-left:0.5pt single; -aw-border-top:0.5pt single">
                                    <p style="margin-top:6pt; margin-bottom:6pt; text-align:justify; font-size:13pt">${item.Ten}</p>
                                </td>
                            </tr>`)
            ++indexj;
        }
    })

}).fail(function () {
    alert("error");
})



$.get("/DM_CoQuanDonVi/GetCoQuan/", (data) => {
    $("#Ma_CoQuan_DonVi_BenGiao").append(`<option value = "">Chọn</option>`)
   
    if (MaCoQuan == 0) {
        $.each(data, (index, value) => {
            $("#Ma_CoQuan_DonVi_BenGiao").append(`<option value ="${value.MaCoQuan}">${value.TenCoQuan}</option>`)
        })
    } else if (MaCoQuan > 0) {
        console.log(MaCoQuan)
        $.each(data, (index, value) => {
            if (value.MaCoQuan == MaCoQuan) {
                $("#Ma_CoQuan_DonVi_BenGiao").append(`<option value ="${value.MaCoQuan}" selected>${value.TenCoQuan}</option>`)
            } else {
                $("#Ma_CoQuan_DonVi_BenGiao").append(`<option value ="${value.MaCoQuan}">${value.TenCoQuan}</option>`)
            }
        })


        if (MaCanBoBenGiao > 0)
            $.get("/DM_CanBo/GetCanBoByCoQuan/", { MaCoQuan: $("#Ma_CoQuan_DonVi_BenGiao").val() }, (data) => {
                $("#Ma_CanBo_BenGiao").append(`<option value = "">Chọn</option>`)
                $.each(data, (index, value) => {
                    if (value.Ma_CanBo == MaCanBoBenGiao) {
                        $("#Ma_CanBo_BenGiao").append(`<option value ="${value.Ma_CanBo}" selected> ${value.Ten_ChucVu_ChucDanh} || ${value.HoTen}</option>`)
                    } else {
                        $("#Ma_CanBo_BenGiao").append(`<option value ="${value.Ma_CanBo}"> ${value.Ten_ChucVu_ChucDanh} || ${value.HoTen}</option>`)
                    }
                })
            })
               
    }
    
})


$(document).ready(function () {
    $('#ID').val(MaKeHoachKeKhai);
    $("#Ma_CoQuan_DonVi_BenGiao").select2();

    $("#Ma_CoQuan_DonVi_BenGiao").select2().on("change", function () {
        $("#Ma_CanBo_BenGiao").empty()
        IDCoQuan = $(this).val();
        
    })

    



    $("#Ma_CanBo_BenGiao").select2();

    $("#Ma_CanBo_BenGiao").select2().on("change", function () {
        var MaCanBo = $(this).val()
        $.get("/DM_CanBo/GetCanBoByID/", { MaCanBo: MaCanBo }, (data) => {
            console.log(data)
            $("#spanCanBoGiao").empty()
            $("#spanCanBoGiao").append(` ${data.TenCanBo}`)
            $("#spanChucVuBenGiao").empty();
            $("#spanChucVuBenGiao").append(` ${data.TenChucVu} tại cơ quan ${data.tenCoQuanDonVi}`)
        })
    })

    $.get("/DM_CanBo/GetCanBoThanhTra/", (data) => {
        $("#Ma_CanBo_BenNhan").empty()
        $("#Ma_CanBo_BenNhan").append(`<option value = "">Chọn</option>`)
        if (MaCanBoBenNhan == 0) {
            $.each(data, (index, value) => {
                $("#Ma_CanBo_BenNhan").append(`<option value ="${value.Ma_CanBo}">${value.HoTen}</option>`)
            })
        } else if (MaCanBoBenNhan > 0) {
            $.each(data, (index, value) => {
                if (value.Ma_CanBo == MaCanBoBenNhan) {
                    $("#Ma_CanBo_BenNhan").append(`<option value ="${value.Ma_CanBo}" selected>${value.HoTen}</option>`)
                } else {
                    $("#Ma_CanBo_BenNhan").append(`<option value ="${value.Ma_CanBo}">${value.HoTen}</option>`)
                }
               
            })
        }
       
    })
    

})


