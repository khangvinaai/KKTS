(function ($) {
    "use strict";

    //* Form js
    function verificationForm() {
        //jQuery time
        var current_fs, next_fs, previous_fs; //fieldsets
        var left, opacity, scale; //fieldset properties which we will animate
        var animating; //flag to prevent quick multi-click glitches

        $(".next").click(function () {
            if (animating) return false;
            animating = true;

            current_fs = $(this).parent();
            next_fs = $(this).parent().next();

            //activate next step on progressbar using the index of next_fs
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

            //show the next fieldset
            next_fs.show();
            //hide the current fieldset with style
            current_fs.animate({
                opacity: 0
            }, {
                step: function (now, mx) {
                    //as the opacity of current_fs reduces to 0 - stored in "now"
                    //1. scale current_fs down to 80%
                    scale = 1 - (1 - now) * 0.2;
                    //2. bring next_fs from the right(50%)
                    left = (now * 50) + "%";
                    //3. increase opacity of next_fs to 1 as it moves in
                    opacity = 1 - now;
                    current_fs.css({
                        'transform': 'scale(' + scale + ')',
                        'position': 'absolute'
                    });
                    next_fs.css({
                        'left': left,
                        'opacity': opacity
                    });
                },
                duration: 800,
                complete: function () {
                    current_fs.hide();
                    animating = false;
                },
                //this comes from the custom easing plugin
                easing: 'easeInOutBack'
            });
        });

        $(".previous").click(function () {
            if (animating) return false;
            animating = true;

            current_fs = $(this).parent();
            previous_fs = $(this).parent().prev();

            //de-activate current step on progressbar
            $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

            //show the previous fieldset
            previous_fs.show();
            //hide the current fieldset with style
            current_fs.animate({
                opacity: 0
            }, {
                step: function (now, mx) {
                    //as the opacity of current_fs reduces to 0 - stored in "now"
                    //1. scale previous_fs from 80% to 100%
                    scale = 0.8 + (1 - now) * 0.2;
                    //2. take current_fs to the right(50%) - from 0%
                    left = ((1 - now) * 50) + "%";
                    //3. increase opacity of previous_fs to 1 as it moves in
                    opacity = 1 - now;
                    current_fs.css({
                        'left': left
                    });
                    previous_fs.css({
                        'transform': 'scale(' + scale + ')',
                        'opacity': opacity
                    });
                },
                duration: 800,
                complete: function () {
                    current_fs.hide();
                    animating = false;
                },
                //this comes from the custom easing plugin
                easing: 'easeInOutBack'
            });
        });

        $(".submit").click(function () {
            return false;
        })
    };

    //* Add Phone no select
    function phoneNoselect() {
        if ($('#msform').length) {
            $("#phone").intlTelInput();
            $("#phone").intlTelInput("setNumber", "+880");
        };
    };
    //* Select js
    function nice_Select() {
        if ($('.product_select').length) {
            $('select').niceSelect();
        };
    };
    /*Function Calls*/
    verificationForm();
    phoneNoselect();
    nice_Select();
})(jQuery);

$(document).ready(() => {
    var url = window.location.href.split('/')
    var id = url[url.length - 1]
    $.ajax({
        url: `/lap-ke-hoach-xac-minh/LoadDataXemChiTiet/${id}`,
        method: "GET",
    }).done(function (data) {
        if (data != null) {
            $("#TenKeHoachXacMinh").val(data.data.lapkehoachxacminh.TenKeHoachXacMinh)
            $("#CanCuQuyetDinh").val(data.data.lapkehoachxacminh.CanCuQuyetDinh)
            $("#NamKeHoach").val(data.data.lapkehoachxacminh.KeHoachNam)
            $("#ThoiHanXacMinh").val(data.data.lapkehoachxacminh.ThoiHanXacMinh)
            $("#ThoiGianBatDau").val(data.data.lapkehoachxacminh.ThoiGianBatDau)
            $("#ThoiGianKetThuc").val(data.data.lapkehoachxacminh.ThoiGianKetThuc)
            $("#NoiDungXacMinh").val(data.data.lapkehoachxacminh.NoiDungXacMinh)

            if (data.data.lapkehoachxacminh.FileQuyetDinh == null) {
                $("#FileQuyetDinh").append(" Không có file đính kèm")
            } else {
                $("#FileQuyetDinh").append(" " + data.data.lapkehoachxacminh.FileQuyetDinh)
                $("#FileQuyetDinh").attr("href", function (i, href) {
                    return `/Content/uploads/${data.data.lapkehoachxacminh.FileQuyetDinh}`;
                });
            }

            if (data.data.lapkehoachxacminh.FileKeHoachChiTiet == null) {
                $("#FileKeHoachChiTiet").append(" Không có file đính kèm")
            } else {
                $("#FileKeHoachChiTiet").append(" " + data.data.lapkehoachxacminh.FileKeHoachChiTiet)
                $("#FileKeHoachChiTiet").attr("href", function (i, href) {
                    return `/Content/uploads/${data.data.lapkehoachxacminh.FileKeHoachChiTiet}`;
                });
            }
            
            $("#DanhSachCoQuanPhoiHop").append(" " + data.data.lapkehoachxacminh.DanhSachCoQuanPhoiHop)
            $("#DanhSachCoQuanPhoiHop").attr("href", function (i, href) {
                return `/Content/uploads/${data.data.lapkehoachxacminh.DanhSachCoQuanPhoiHop}`;
            });
            $("#DanhSachToXacMinh").append(" " + data.data.lapkehoachxacminh.DanhSachToXacMinh)
            $("#DanhSachToXacMinh").attr("href", function (i, href) {
                return `/Content/uploads/${data.data.lapkehoachxacminh.DanhSachToXacMinh}`;
            });
            $("#next-page1").click(() => {
                $("#DanhSachCanBoDuocXacMinh").html("")
                $.each(data.data.lapkehoachxacminh_chitiet, (index, item) => {
                    console.log()
                   
                    $("#DanhSachCanBoDuocXacMinh").append(`
                       <tr>
                            <td style="width: 2%;"></td>
                            <td style="width: 10%;">${item.cb.HoTen}</td>
                            <td style="width: 8%;">${item.cb.DoB}</td>
                            <td style="width: 7%;">${item.cb.SoCCCD ? item.cb.SoCCCD : ""}</td>
                            <td style="width: 11%;">${item.cq.Ten}</td>
                            <td style="width: 18%;">${item.cv.Ten_ChucVu_ChucDanh}</td>
                        </tr>
                `
                    )
                })
            })
            $("tfoot").remove();
        }
    });
})



function print_bankkCBkk(obj) {
    var url = window.location.href.split('/')
    var id = url[url.length - 1]
    $.ajax({
        url: `/lap-ke-hoach-xac-minh/indanhsachcanbo?id=${id}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        beforeSend: function () {
            $(`#btn_download_bankekhai_${id}`).addClass("d-none")
            $(`#loading_btnPrint_bankkCBkk_${id}`).removeClass("d-none")
        },
        success: function (response) {
            $(`#btn_download_bankekhai_${id}`).removeClass("d-none")
            $(`#loading_btnPrint_bankkCBkk_${id}`).addClass("d-none")
            window.open("/lap-ke-hoach-xac-minh/ban-in/" + response)
        },
        error: function (response) {
            Swal.fire({
                icon: 'error',
                title: 'Thất Bại',
                text: 'Lỗi hệ thống',
                timer: 2000,
                showConfirmButton: false,
            }).then(() => {
                $(`#btn_download_bankekhai_${id}`).removeClass("d-none")
                $(`#loading_btnPrint_bankkCBkk_${id}`).addClass("d-none")
            })
        }
    });

}