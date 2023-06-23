$.post("/NV_CapTaiKhoan/getDemYeuCauCapTaiKhoan/", (data) => {
    if (data > 0) {
        $("#MN0104").append(`<span class="badge badge-danger ml-1 mb-1" > ${data} </span>`);
    }
})