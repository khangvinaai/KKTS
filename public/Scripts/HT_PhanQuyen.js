$("#capnhat").click(() => {
    var list_check = []
    var list_nonecheck = []
    var list_tc = ["QLCT_TATCA", "QLNV_TATCA", "QLPB_TATCA", "TCKH_TATCA", "QLKH_TATCA", "QLKHCT_TATCA", "DSTK_TATCA", "DSNQ_TATCA", "DSQ_TATCA", "BACKUP_TATCA", "IP_TATCA"]
    $('input[type="checkbox"]:checked').each(function () {
        var sThisVal = $(this).attr("name")
        list_check.push(sThisVal)
    });
    $(":checkbox:not(:checked)").each(function () {
        var sThisVal = $(this).attr("name")
        list_nonecheck.push(sThisVal)
    });


    var MaNhomQuyen = $("#MaNhomTaiKhoan").val()

    list_check = list_check.filter(x => !list_tc.includes(x));
    list_nonecheck = list_nonecheck.filter(x => !list_tc.includes(x));

    $.ajax({
        type: 'POST',
        url: "/HT_PhanQuyen/UpdateQuyen",
        data: { Check: list_check, NoCheck: list_nonecheck, MaNhomQuyen: MaNhomQuyen },
        beforeSend: function () {
            $(".loading").removeClass("d-none")
        },
        success: function (data) {
            $(".loading").addClass("d-none")
            Swal.fire({
                icon: 'success',
                title: 'Thành Công',
                text: `Cập nhật nhóm quyền thành công`,
                timer: 2000,
                showConfirmButton: false,
            })
            location.reload();
        },
        error: function () {
            $(".loading").addClass("d-none")
            Swal.fire({
                icon: 'error',
                title: 'Có Lỗi',
                text: `Cập nhật nhóm quyền không thành công`,
                timer: 2000,
                showConfirmButton: false,
            })
        }
    });


})