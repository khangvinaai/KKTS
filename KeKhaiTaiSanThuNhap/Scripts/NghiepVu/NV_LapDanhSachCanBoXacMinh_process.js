(function ($) {
    "use strict";
    console.log(MaDanhSachCanBoXacMinh)
    //* Form js
    function verificationForm() {
        //jQuery time
        var current_fs, next_fs, previous_fs; //fieldsets
        var left, opacity, scale; //fieldset properties which we will animate
        var animating; //flag to prevent quick multi-click glitches

        $(".next").click( function () {

            var arraysCoQuan = []
            var arraysSoLanhDao = []
            var arraysSoNhanVien = []
            $("input[name='DanhSachCoQuan[]']").each(function () {
                arraysCoQuan.push($(this).val());
            })
            $("input[name='LayLanhDao[]']").each(function () {
                arraysSoLanhDao.push($(this).val());
            })
            $("input[name='LayNhanVien[]']").each(function () {
                arraysSoNhanVien.push($(this).val());
            })
            var t = arraysCoQuan.join().toString()
            var SoLanhDao = arraysSoLanhDao.join().toString()
            var SoNhanVien = arraysSoNhanVien.join().toString()
            DSNgauNhienCoQuan = t;
            console.log(t)
             $.ajax({
                url: `/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/LayDanhSachCanBoNgauNhien?MaDanhSachCanBoXacMinh=${MaDanhSachCanBoXacMinh}&DanhSachCoQuan=${t}&SoLanhDao=${SoLanhDao}&SoNhanVien=${SoNhanVien}`,
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                type: "POST",
                beforeSend: function (response) {
                    //console.log("nextpage success", response)
                    var data = response
                    $(".next").prop('disabled', true)
                },
                success: function (response) {
                    /* console.log("nextpage success", response)*/
                    $(".next").prop('disabled', false)
                },
                error: function (response) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Thất Bại',
                        text: 'Lỗi hệ thống',
                        timer: 2000,
                        showConfirmButton: false,
                    }).then(() => {

                    })
                }
             }).then((response) => {
                 console.log(response)
                 if (response.TrangThai == "true") {
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
                     danhsachCanBoRandom =  response.listcanbo;
                 } else if (response.TrangThai == "SoLanhDaoLonHonChoPhep") {
                     Swal.fire({
                         icon: 'error',
                         title: 'Thất Bại',
                         text: 'Số Lượng Lãnh Đạo Vượt Qúa Số Lượng Cho Phép ',
                         timer: 2000,
                         showConfirmButton: false,
                     }).then(() => {

                     })
                 } else if (response.TrangThai == "fasle") {
                     Swal.fire({
                         icon: 'error',
                         title: 'Thất Bại',
                         text: 'Vui lòng nhập số lượng Lãnh Đạo',
                         timer: 2000,
                         showConfirmButton: false,
                     }).then(() => {

                     })
                 } else if (response.TrangThai == "quasoluong10phantram") {
                     Swal.fire({
                         icon: 'error',
                         title: 'Thất Bại',
                         text: 'Số Lượng Người Cần Xác Minh Ngẫu Nhiên Đảm Bảo Tối Thiểu 10%',
                         timer: 2000,
                         showConfirmButton: false,
                     }).then(() => {

                     })
                 }
                 else if (response.TrangThai == "LanhDaochuadu10phantram") {
                     Swal.fire({
                         icon: 'error',
                         title: 'Thất Bại',
                         text: 'Số lượng danh sách Lãnh Đạo xác minh chưa đủ 10%',
                         timer: 2000,
                         showConfirmButton: false,
                     }).then(() => {

                     })
                 } else if (response.TrangThai == "canbochuadu10phantram") {
                     Swal.fire({
                         icon: 'error',
                         title: 'Thất Bại',
                         text: 'Số lượng danh sách cán bộ xác minh chưa đủ 10% ',
                         timer: 2000,
                         showConfirmButton: false,
                     }).then(() => {

                     })
                 } 
                 
             }).then(() => {
                 $.ajax({
                     url: `/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/InDanhSachThongKe?MaDanhSachCanBoXacMinh=${MaDanhSachCanBoXacMinh}&DanhSachCoQuan=${t}&SoLanhDao=${SoLanhDao}&SoNhanVien=${SoNhanVien}`,
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     type: "POST",
                     beforeSend: function (response) {

                     },
                     success: function (response) {
                         window.open("/danh-sach-can-bo-xac-minh/ban-in/" + response)
                     },
                     error: function (response) {
                         Swal.fire({
                             icon: 'error',
                             title: 'Thất Bại',
                             text: 'Lỗi hệ thống',
                             timer: 2000,
                             showConfirmButton: false,
                         }).then(() => {

                         })
                     }
                 })
             }) ;
            //---------------------------
            //if (animating) return false;
            //animating = true;

            //current_fs = $(this).parent();
            //next_fs = $(this).parent().next();

            ////activate next step on progressbar using the index of next_fs
            //$("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

            ////show the next fieldset
            //next_fs.show();
            ////hide the current fieldset with style
            //current_fs.animate({
            //    opacity: 0
            //}, {
            //    step: function (now, mx) {
            //        //as the opacity of current_fs reduces to 0 - stored in "now"
            //        //1. scale current_fs down to 80%
            //        scale = 1 - (1 - now) * 0.2;
            //        //2. bring next_fs from the right(50%)
            //        left = (now * 50) + "%";
            //        //3. increase opacity of next_fs to 1 as it moves in
            //        opacity = 1 - now;
            //        current_fs.css({
            //            'transform': 'scale(' + scale + ')',
            //            'position': 'absolute'
            //        });
            //        next_fs.css({
            //            'left': left,
            //            'opacity': opacity
            //        });
            //    },
            //    duration: 800,
            //    complete: function () {
            //        current_fs.hide();
            //        animating = false;
            //    },
            //    //this comes from the custom easing plugin
            //    easing: 'easeInOutBack'
            //});
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
