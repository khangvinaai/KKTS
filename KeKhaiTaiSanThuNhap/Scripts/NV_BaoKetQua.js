$("#CongVanSo").keyup(function () {
    $("#spanCongVanSo").text($("#CongVanSo").val());
});
$("#BaoCaoSo").keyup(function () {
    $("#spanBaoCaoSo").text($("#BaoCaoSo").val());
});
$("#DanhGiaKienNghi").keyup(function () {
    $("#spanDanhGiaKienNghi").text($("#DanhGiaKienNghi").val());
});

$("#CongVanNgay").change(function () {
    var dateInput = new Date($("#CongVanNgay").val());
    $("#spanNgayCongVan").text(dateInput.getDate());
    $("#spanThangCongVan").text(dateInput.getMonth() + 1);
    $("#spanNamCongVan").text(dateInput.getFullYear());
});

$("#BaoCaoNgay").change(function () {
    var dateInput = new Date($("#BaoCaoNgay").val());
    $("#spanThuBaoCao").text(dateInput.getDay() + 1);
    $("#spanNgayBaoCao").text(dateInput.getDate());
    $("#spanThangBaoCao").text(dateInput.getMonth() + 1);
    $("#spanNamBaoCao").text(dateInput.getFullYear());
});

$("#NoiDungChiDao").keyup(function () {
    $("#spanNoiDungChiDao").html($("#NoiDungChiDao").val());
});
function defaultnoidung() {
    $("#spanNoiDungChiDao").html('- Phạm vi, đặc điểm tổ chức, hoạt động của cơ quan, tổ chức, đơn vị.</br>' +
        ' - Các văn bản pháp luật áp dụng.</br>' +
        ' - Các văn bản chỉ đạo, đôn đốc của cấp trên.</br>' +
        ' - Các văn bản cơ quan, đơn vị đã triển khai như: Kế hoạch, phương án, tổ chức tuyên truyền…</br>');
}
defaultnoidung();