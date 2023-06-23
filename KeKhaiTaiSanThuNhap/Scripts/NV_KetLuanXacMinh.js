$('input[name="XuLy"]').change(function () {
    if (this.checked) {
        $('#DonViXuLyViPham').prop('disabled', false);
    } else {
        $('#DonViXuLyViPham').prop('disabled', 'disabled');
    }
});

$('input[name="XuLy1"]').change(function () {
    if (this.checked) {
        $('#DonViXuLyViPham1').prop('disabled', false);
    } else {
        $('#DonViXuLyViPham1').prop('disabled', 'disabled');
    }
});



