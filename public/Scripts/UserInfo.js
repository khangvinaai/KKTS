

    var loadFile1 = function (event) {
        var image = document.getElementById('user-image1');
        image.src = URL.createObjectURL(event.target.files[0])
    }

    function FnSuccess_Password(data) {
        $('.closeform').click()
        Swal.fire({
        icon: 'success',
            title: 'Thành Công',
            text: `Đổi mật khẩu thành công`,
            timer: 2000,
            showConfirmButton: false,
        })
        location.reload();
    }

function FnSuccess_DoiAnh(data) {
    if (data == true) {
        $('.closeform').click()
        Swal.fire({
            icon: 'success',
            title: 'Thành Công',
            text: `Đổi ảnh đại diện thành công`,
            timer: 2000,
            showConfirmButton: false,
        })
        location.reload();
    } else {
        $('.closeform').click()
        Swal.fire({
            icon: 'error',
            title: 'Lỗi',
            text: `Bạn đã nhập sai mật khẩu hoặc mật khẩu không trùng khớp`,
            timer: 2000,
            showConfirmButton: false,
        })
    }
        
    }

function FnSuccess_DoiEmail(data) {
    console.log(data)
    if (data == true) {
        $('.closeform').click()
        Swal.fire({
            icon: 'success',
            title: 'Thành Công',
            text: `Đổi email thành công`,
            timer: 2000,
            showConfirmButton: false,
        })
        location.reload()
    } else if (data == false) {
        $('.closeform').click()
        Swal.fire({
            icon: 'error',
            title: 'Lỗi',
            text: `Bạn đã nhập sai mật khẩu`,
            timer: 2000,
            showConfirmButton: false,
        })
    }
}
function FnBegin() {
    $('.loading_newtaikhoan').removeClass('d-none');
}



function FnFailure_Password(data) {
    $('.closeform').click()
    Swal.fire({
    icon: 'error',
        title: 'Lỗi',
        text: `Đổi mật khẩu không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}

function FnFailure_DoiAnh(data) {
    $('.closeform').click()
    Swal.fire({
    icon: 'error',
        title: 'Lỗi',
        text: `Đổi ảnh đại diện không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}

function FnFailure_DoiEmail(data) {
    $('.closeform').click()
    Swal.fire({
    icon: 'error',
        title: 'Lỗi',
        text: `Đổi email không thành công`,
        timer: 2000,
        showConfirmButton: false,
    })
}

    $(document).ready(function () {

        jQuery.validator.addMethod("NotAllowNumber", function (value, element) {
            return this.optional(element) || /^([^0-9]*)$/.test(value);
        }, "Không được phép có chữ số.");

        jQuery.validator.addMethod("NotAllowFirstSpace", function (value, element) {
            return this.optional(element) || /^\S{1}/.test(value);
        }, "Kí tự đầu tiên không được có khoảng trắng.");

        jQuery.validator.addMethod("NotAllowSpecial", function (value, element) {
            return this.optional(element) || /^([^*\u0040.!/'#$%^&(){ }[:;<>,.?/~_+-=|]*)$/.test(value);
    }, "Không được phép có kí tự đặc biệt.");

        var form = $(".formDoiMatKhau").validate({
            onfocusout: function (element) {
            $(element).valid();
            },
            invalidHandler: function (form, validator) {
            validator.focusInvalid();
                Swal.fire({
            icon: 'error',
                    title: 'Xuất hiện lỗi',
                    text: `Đổi mật khẩu không thành công`,
                    timer: 2000,
                    showConfirmButton: false
                })

            },
            errorClass: "is-invalid",
            validClass: "is-valid",
            rules: {
            MatKhau1: {
                required: true,
                remote: {
                    url: "/HT_TaiKhoan/CheckMatKhau/",
                    type: "post",
                    dataType: "json",
                }
            },
            MatKhauMoi: {
                required: true,
                 minlength: 8
            },
            ReMatKhauMoi: {
                equalTo: '[name="MatKhauMoi"]'

                },
            },
            messages: {
            MatKhau1: {
            required: "Mật khẩu hiện tại không được bỏ trống",
                    remote: "Mật khẩu hiện tại không chính xác"
                },
                MatKhauMoi: {
            required: "Mật khẩu mới không được bỏ trống",
                    minlength: "Mật khẩu ít nhất 8 kí tự"
                },
                ReMatKhauMoi: {
            equalTo: "Mật khẩu mới không trùng khớp"
                },

            }
        });

        var form = $(".formDoiEmail").validate({
            onfocusout: function (element) {
            $(element).valid();
            },
            invalidHandler: function (form, validator) {
            validator.focusInvalid();
                Swal.fire({
            icon: 'error',
                    title: 'Xuất hiện lỗi',
                    text: `Đổi email không thành công`,
                    timer: 2000,
                    showConfirmButton: false
                })

            },
            errorClass: "is-invalid",
            validClass: "is-valid",
            rules: {
            email1: {
            required: true,

                },
                email2: {
            required: true,
            remote: {
                url: "/DM_CanBo/EmailTonTai_DoiEmail/",
                type: "post",
                dataType: 'json',
                
             },
                    email: true
                },
            MatKhau1: {
                required: true,
               //remote: {
               //   url: "/HT_TaiKhoan/CheckMatKhau/",
               //   type: "post",
               //   }
                },
            },
            messages: {
            email1: {
            required: "Email hiện tại không được bỏ trống",
                },
            email2: {
                required: "Email mới không được bỏ trống",
                remote: "Email đã được sử dụng",
                email: "Email không hợp lệ"
            },
            MatKhau1: {
                required: "Mật khẩu hiện tại không được bỏ trống",
                //remote: "Mật khẩu hiện tại không đúng"
                },
            }
        });

        $(".closeform").click(function () {

            $(':input', '.formDoiMatKhau')
                .not(':button, :submit, :reset, :hidden')
                .val('')
                .prop('checked', false)
                .prop('selected', false)
                .removeClass('is-invalid')
                .removeClass('is-valid')
            form.resetForm()
        })
    })




        function loadthongbao() {
            $.get("/HT_ThongBao/GetThongBao/", (data) => {

                $("#thongbao").empty()

                var row1 = `   <div class="dropdown-divider"></div>
                    <span style="padding: 14px;">
                        Thông báo mới
                    </span>
                    `

                var row2 = `<div class="dropdown-divider"></div>
                    <span style="padding: 14px;">
                        Thông báo đã xem
                    </span>
                   `


                $.each(data.chuaxem, (index, value) => {
                    var date = new Date(Number(value.tb.ThoiGian.match(/\d+/)[0]));
                    row1 = row1 + `<div class="dropdown-divider"></div>
                    <a href="#" style="padding: 10px 15px; display: block; color: black;">
                        <!-- Message Start -->
                        <div class="media">
                            <div class="media-body">
                                <h3 class="dropdown-item-title" style="font-weight: 600; margin-bottom: 5px; color: #007bff">
                                    ${value.tennguoigui}
                                    <span class="float-right text-sm" style="color: #ffc107"><i class="fas fa-star"></i></span>
                                </h3>
                                <p  style="overflow: hidden; text-overflow: ellipsis; max-width: 200px; font-weight: 600;">${value.tb.TieuDe}</p>
                                <p  style="overflow: hidden; text-overflow: ellipsis; max-width: 200px; font-size: 13px;">${value.tb.NoiDung}</p>
                                <p class="text-sm text-muted"><i class="far fa-clock mr-1"></i>${moment(date).fromNow()}</p>
                            </div>
                        </div>
                        <!-- Message End -->
                    </a>`
                })


                $.each(data.daxem, (index, value) => {
                    var date = new Date(Number(value.tb.ThoiGian.match(/\d+/)[0]));
                    row2 = row2 + `<div class="dropdown-divider"></div>
                    <a href="#" style="padding: 10px 15px; display: block; color: black;">
                        <!-- Message Start -->
                        <div class="media">
                            <div class="media-body">
                                <h3 class="dropdown-item-title" style="font-weight: 600; margin-bottom: 5px; color: #007bff">
                                    ${value.tennguoigui}
                                   <span class="float-right text-sm text-primary">Đã xem</span>
                                </h3>
                                <p  style="overflow: hidden; text-overflow: ellipsis; max-width: 200px; font-weight: 600;">${value.tb.TieuDe}</p>
                                <p  style="overflow: hidden; text-overflow: ellipsis; max-width: 200px; font-size: 13px;">${value.tb.NoiDung}</p>
                                <p class="text-sm text-muted"><i class="far fa-clock mr-1"></i>${moment(date).fromNow()}</p>
                            </div>
                        </div>
                        <!-- Message End -->
                    </a>`
                })


                if (data.chuaxem.length == 0) {
                    row1 = row1 + `<div class="dropdown-divider"></div>
                    <span style="padding: 10px 15px; display: block; color: black;">
                      Chưa có thông báo để hiển thị
                    </span>`
                }
                if (data.daxem.length == 0) {
                    row2 = row2 + `<div class="dropdown-divider"></div>
                    <span style="padding: 10px 15px; display: block; color: black;">
                      Chưa có thông báo để hiển thị
                    </span>`
                }
                if (data.chuaxem.length == 0 && data.daxem.length == 0) {
                    $("#thongbao").append(`<div class="dropdown-divider"></div>
                    <span style="padding: 10px 15px; display: block; color: black;">
                      Chưa có thông báo để hiển thị
                    </span>`)
                }
                else {
                    $("#thongbao").append(row1)
                    $("#thongbao").append(row2)
                }

            })
        }

    loadthongbao()

    $("#checkallseen").click(() => {
            $("#numberthongbao").remove();
        $.post("/HT_ThongBao/AllSeen/", (data) => {
            loadthongbao()
        })
    })

$.post("/DM_CanBo/GetCanBoByIdCookei", (data) => {
    console.log(data)
    $("#emailCu").val(data[0].Email)
    $("#idCanBo").val(data[0].Ma_CanBo)
})