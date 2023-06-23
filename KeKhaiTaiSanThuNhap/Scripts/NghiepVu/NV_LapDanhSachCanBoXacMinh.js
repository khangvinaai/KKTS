var url = window.location.href.split('/')
var MaDanhSachCanBoXacMinh = url[url.length - 1]
var DanhSachCoQuan = []
var dataCanBo = ``
var dataCoQuan = ``
console.log("madanhsachxacminh", MaDanhSachCanBoXacMinh)


function FnSuccess(data) {
    $('.loading').addClass('d-none');
    Swal.fire({
        icon: 'success',
        title: 'Thành Công',
        text: `Lập Danh Sách Cán Bộ Xác Minh Thành Công`,
        timer: 2000,
        showConfirmButton: false,
    }).then(() => {
        window.location.href = "/danh-sach-can-bo-xac-minh";
    })
}

function FnBegin() {
    $('.loading').removeClass('d-none');
    $('#luulai').prop('disabled', true)
}

function Failure(data) {
    $('.loading').addClass('d-none');
    Swal.fire({
        icon: 'error',
        title: 'Tên tài khoản đã tồn tại',
        text: data,
        timer: 2000,
        showConfirmButton: false,

    })
}

var canbolist;

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
    var tid2 = setInterval(ProcessingCanBo, 100);

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
        $.get(`/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/LayDanhSachCanBo?id=${MaDanhSachCanBoXacMinh}&DSNgauNhienCoQuan=${DSNgauNhienCoQuan}`, (data) => {
            DanhSachCanBo = data
            $('#danhsachcanboduocxacminh').empty();
        
            $.each(data, (index, value) => {
                $('#DanhSachCanBoDuocXacMinh').prepend(`<tr>
                                                        <td></td>
                                                        <td>${value.HoTen}</td>
                                                        <td>${value.NgaySinh}</td>
                                                        <td>${(value.CCCD === null) ? "" : value.CCCD}</td>
                                                        <td>${value.TenCoQuan}</td>
                                                        <td>${value.ChucVu}</td>
                                                        <td>${value.TrangThaiKK == true ? "<span class='badge badge-success'>Đã kê khai</span>" : "<span class='badge badge-danger'>Chưa kê khai</span>"}</td>
                                                    </tr>`)
            })
            Swal.fire({
                icon: 'success',
                title: 'Thành Công',
                text: `Lấy danh sách cơ quan - đơn vị thành công`,
                timer: 2000,
                showConfirmButton: false,
            })
            $('#LayNgauNhienDanhSachCanBonDuocXacMinh').removeClass("d-none")
        })
        $("#next-page1").prop("disabled", false)
    }
    $('#clsAddRowDanhSachCanBoDuocXacMinh').remove();
    $("#tfDanhSachCanBoDuocXacMinh").empty()
})

var DSNgauNhienCoQuan

//Loading show danh sách Cơ Quan
$('#clsAddRowDanhSachCoQuanDuocXacMinh').click((e) => {
   
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
    async function abortTimer() {
        clearInterval(tid);
        canbolist = await $.get(`/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/LayDanhSachCoQuan/${MaDanhSachCanBoXacMinh}`, (data) => {
           
            DanhSachCoQuan=data.data
            $('#DanhSachCoQuanDuocXacMinh').empty();
            var checkTrangThai = 0
            $.each(data.data, (index, value) => {
                if (value.TrangThai == false) {
                    checkTrangThai++;
                }
                $('#DanhSachCoQuanDuocXacMinh').prepend(`<tr>
                                                        <td></td>
                                                        <td><input type="checkbox" class="Ma_CoQuan" style="font-size:150%" id="vehicle1" name="ID_CoQuan" value="${value.Ma_CoQuan_DonVi}"></td>
                                                        <td>${value.Ten_Loai_CQDV}</td>
                                                        <td>${value.Ten}</td>
                                                        <td>${value.TongCanBo}</td>
                                                        <td>${value.TongLanhDao}</td>
                                                        <td>${value.TongChuyenVien}</td>

                                                        <td>${(parseInt)((value.TongCanBo) / 100 * 10) == 0 ? 1 : (parseInt)((value.TongCanBo) / 100 * 10) }</td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>${value.TrangThai == true ? "<span class='badge badge-success'>Đã lập kế hoạch kê khai</span>" : "<span class='badge badge-danger'>Chưa lập kế hoạch kê khai</span>"}</td>
                                                    </tr>`)
            })
            Swal.fire({
                icon: 'success',
                title: 'Thành Công',
                text: `Lấy danh sách cơ quan - đơn vị thành công`,
                timer: 2000,
                showConfirmButton: false,
            })
            if (checkTrangThai == 0) {
                //$('#LayNgauNhienDanhSachCoQuanDuocXacMinh').removeClass("d-none")
                $('#LayDanhSachCoQuanDuocXacMinhDaChon').removeClass("d-none")
            }
        })
    }

    $('#clsAddRowDanhSachCoQuanDuocXacMinh').remove();
    $("#tfDanhSachCoQuanDuocXacMinh").empty()
})

$("#LayNgauNhienDanhSachCanBonDuocXacMinh").click(async (e) => {
    $("#LayNgauNhienDanhSachCanBonDuocXacMinh").remove()
    $('#DanhSachCanBoDuocXacMinh').empty();

    await $.each(danhsachCanBoRandom, async (index, value) => {
        await setTimeout(() => {
            $(`#${danhsachCanBoRandom.MaCanBo}`).remove();
            $('#DanhSachCanBoDuocXacMinh').append(`<tr>
                                                        <td></td>
                                                        <td>${value.HoTen}<input hidden value="${value.MaCanBo}" name="Ma_CanBo_XacMinh[]"/></td>
                                                        <td>${value.NgaySinh}</td>
                                                        <td>${(value.CCCD === null) ? "" : value.CCCD }</td>
                                                        <td>${value.TenCoQuan}</td>
                                                        <td>${value.ChucVu}</td>
                                                        <td>${value.TrangThaiKK == true ? "<span class='badge badge-success'>Đã kê khai</span>" : "<span class='badge badge-danger'>Chưa kê khai</span>"}</td>
                                                    </tr>
                                                    <tr id="${danhsachCanBoRandom.MaCanBo}">
                                                        <td style="position: relative;"></td>
                                                        <td style="position: relative;"><svg id="loading1_${danhsachCanBoRandom.Ma_CoQuan_DonVi}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: #fff0; display: block; width: 48px; position: absolute; left: 47%;;top: -6px;" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                                                            <circle cx="75" cy="50" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.9090909090909091s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.9090909090909091s"></animate>
                                                            </circle>
                                                            <circle cx="71.03133832077953" cy="63.51602043638994" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.8181818181818182s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.8181818181818182s"></animate>
                                                            </circle>
                                                            <circle cx="60.385375325047164" cy="72.74079988386296" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.7272727272727273s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.7272727272727273s"></animate>
                                                            </circle>
                                                            <circle cx="46.442129043167874" cy="74.74553604702332" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.6363636363636364s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.6363636363636364s"></animate>
                                                            </circle>
                                                            <circle cx="33.62848165136788" cy="68.89373935885646" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.5454545454545454s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.5454545454545454s"></animate>
                                                            </circle>
                                                            <circle cx="26.012675659637566" cy="57.04331392103574" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.45454545454545453s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.45454545454545453s"></animate>
                                                            </circle>
                                                            <circle cx="26.012675659637562" cy="42.95668607896427" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.36363636363636365s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.36363636363636365s"></animate>
                                                            </circle>
                                                            <circle cx="33.62848165136787" cy="31.106260641143546" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.2727272727272727s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.2727272727272727s"></animate>
                                                            </circle>
                                                            <circle cx="46.44212904316787" cy="25.254463952976682" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.18181818181818182s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.18181818181818182s"></animate>
                                                            </circle>
                                                            <circle cx="60.38537532504715" cy="27.259200116137038" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.09090909090909091s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.09090909090909091s"></animate>
                                                            </circle>
                                                            <circle cx="71.03133832077953" cy="36.48397956361006" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="0s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="0s"></animate>
                                                            </circle>
                                                        </svg></td>
                                                        <td style="position: relative;"><svg  id="loading1_${danhsachCanBoRandom.Ma_CoQuan_DonVi}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: #fff0; display: block; width: 48px; position: absolute; left: 47%;;top: -6px;" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                                                            <circle cx="75" cy="50" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.9090909090909091s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.9090909090909091s"></animate>
                                                            </circle>
                                                            <circle cx="71.03133832077953" cy="63.51602043638994" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.8181818181818182s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.8181818181818182s"></animate>
                                                            </circle>
                                                            <circle cx="60.385375325047164" cy="72.74079988386296" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.7272727272727273s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.7272727272727273s"></animate>
                                                            </circle>
                                                            <circle cx="46.442129043167874" cy="74.74553604702332" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.6363636363636364s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.6363636363636364s"></animate>
                                                            </circle>
                                                            <circle cx="33.62848165136788" cy="68.89373935885646" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.5454545454545454s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.5454545454545454s"></animate>
                                                            </circle>
                                                            <circle cx="26.012675659637566" cy="57.04331392103574" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.45454545454545453s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.45454545454545453s"></animate>
                                                            </circle>
                                                            <circle cx="26.012675659637562" cy="42.95668607896427" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.36363636363636365s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.36363636363636365s"></animate>
                                                            </circle>
                                                            <circle cx="33.62848165136787" cy="31.106260641143546" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.2727272727272727s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.2727272727272727s"></animate>
                                                            </circle>
                                                            <circle cx="46.44212904316787" cy="25.254463952976682" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.18181818181818182s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.18181818181818182s"></animate>
                                                            </circle>
                                                            <circle cx="60.38537532504715" cy="27.259200116137038" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.09090909090909091s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.09090909090909091s"></animate>
                                                            </circle>
                                                            <circle cx="71.03133832077953" cy="36.48397956361006" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="0s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="0s"></animate>
                                                            </circle>
                                                        </svg></td>
                                                        <td style="position: relative;"><svg  id="loading1_${danhsachCanBoRandom.Ma_CoQuan_DonVi}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: #fff0; display: block; width: 48px; position: absolute; left: 47%;;top: -6px;" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                                                            <circle cx="75" cy="50" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.9090909090909091s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.9090909090909091s"></animate>
                                                            </circle>
                                                            <circle cx="71.03133832077953" cy="63.51602043638994" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.8181818181818182s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.8181818181818182s"></animate>
                                                            </circle>
                                                            <circle cx="60.385375325047164" cy="72.74079988386296" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.7272727272727273s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.7272727272727273s"></animate>
                                                            </circle>
                                                            <circle cx="46.442129043167874" cy="74.74553604702332" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.6363636363636364s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.6363636363636364s"></animate>
                                                            </circle>
                                                            <circle cx="33.62848165136788" cy="68.89373935885646" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.5454545454545454s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.5454545454545454s"></animate>
                                                            </circle>
                                                            <circle cx="26.012675659637566" cy="57.04331392103574" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.45454545454545453s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.45454545454545453s"></animate>
                                                            </circle>
                                                            <circle cx="26.012675659637562" cy="42.95668607896427" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.36363636363636365s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.36363636363636365s"></animate>
                                                            </circle>
                                                            <circle cx="33.62848165136787" cy="31.106260641143546" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.2727272727272727s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.2727272727272727s"></animate>
                                                            </circle>
                                                            <circle cx="46.44212904316787" cy="25.254463952976682" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.18181818181818182s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.18181818181818182s"></animate>
                                                            </circle>
                                                            <circle cx="60.38537532504715" cy="27.259200116137038" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.09090909090909091s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.09090909090909091s"></animate>
                                                            </circle>
                                                            <circle cx="71.03133832077953" cy="36.48397956361006" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="0s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="0s"></animate>
                                                            </circle>
                                                        </svg></td>
                                                        <td style="position: relative;"><svg  id="loading1_${danhsachCanBoRandom.Ma_CoQuan_DonVi}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: #fff0; display: block; width: 48px; position: absolute; left: 47%;;top: -6px;" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                                                            <circle cx="75" cy="50" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.9090909090909091s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.9090909090909091s"></animate>
                                                            </circle>
                                                            <circle cx="71.03133832077953" cy="63.51602043638994" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.8181818181818182s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.8181818181818182s"></animate>
                                                            </circle>
                                                            <circle cx="60.385375325047164" cy="72.74079988386296" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.7272727272727273s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.7272727272727273s"></animate>
                                                            </circle>
                                                            <circle cx="46.442129043167874" cy="74.74553604702332" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.6363636363636364s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.6363636363636364s"></animate>
                                                            </circle>
                                                            <circle cx="33.62848165136788" cy="68.89373935885646" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.5454545454545454s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.5454545454545454s"></animate>
                                                            </circle>
                                                            <circle cx="26.012675659637566" cy="57.04331392103574" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.45454545454545453s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.45454545454545453s"></animate>
                                                            </circle>
                                                            <circle cx="26.012675659637562" cy="42.95668607896427" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.36363636363636365s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.36363636363636365s"></animate>
                                                            </circle>
                                                            <circle cx="33.62848165136787" cy="31.106260641143546" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.2727272727272727s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.2727272727272727s"></animate>
                                                            </circle>
                                                            <circle cx="46.44212904316787" cy="25.254463952976682" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.18181818181818182s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.18181818181818182s"></animate>
                                                            </circle>
                                                            <circle cx="60.38537532504715" cy="27.259200116137038" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.09090909090909091s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.09090909090909091s"></animate>
                                                            </circle>
                                                            <circle cx="71.03133832077953" cy="36.48397956361006" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="0s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="0s"></animate>
                                                            </circle>
                                                        </svg></td>
                                                        <td style="position: relative;"><svg  id="loading1_${danhsachCanBoRandom.Ma_CoQuan_DonVi}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: #fff0; display: block; width: 48px; position: absolute; left: 47%;;top: -6px;" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                                                            <circle cx="75" cy="50" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.9090909090909091s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.9090909090909091s"></animate>
                                                            </circle>
                                                            <circle cx="71.03133832077953" cy="63.51602043638994" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.8181818181818182s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.8181818181818182s"></animate>
                                                            </circle>
                                                            <circle cx="60.385375325047164" cy="72.74079988386296" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.7272727272727273s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.7272727272727273s"></animate>
                                                            </circle>
                                                            <circle cx="46.442129043167874" cy="74.74553604702332" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.6363636363636364s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.6363636363636364s"></animate>
                                                            </circle>
                                                            <circle cx="33.62848165136788" cy="68.89373935885646" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.5454545454545454s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.5454545454545454s"></animate>
                                                            </circle>
                                                            <circle cx="26.012675659637566" cy="57.04331392103574" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.45454545454545453s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.45454545454545453s"></animate>
                                                            </circle>
                                                            <circle cx="26.012675659637562" cy="42.95668607896427" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.36363636363636365s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.36363636363636365s"></animate>
                                                            </circle>
                                                            <circle cx="33.62848165136787" cy="31.106260641143546" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.2727272727272727s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.2727272727272727s"></animate>
                                                            </circle>
                                                            <circle cx="46.44212904316787" cy="25.254463952976682" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.18181818181818182s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.18181818181818182s"></animate>
                                                            </circle>
                                                            <circle cx="60.38537532504715" cy="27.259200116137038" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.09090909090909091s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.09090909090909091s"></animate>
                                                            </circle>
                                                            <circle cx="71.03133832077953" cy="36.48397956361006" fill="#93e6d6" r="5">
                                                                <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="0s"></animate>
                                                                <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="0s"></animate>
                                                            </circle>
                                                        </svg></td>
                                                    </tr>  `)

        }, 100 * index);
    })

    await setTimeout(() => {
        $(`#${danhsachCanBoRandom.Ma_CoQuan_DonVi}`).remove();
        $(`#next-page1`).prop('disabled', false);
    }, 100 * danhsachCanBoRandom.length)


    $('#luulai').removeClass("d-none")
})

$("#LayNgauNhienDanhSachCoQuanDuocXacMinh").click((e) => {
    $('#DanhSachCoQuanDuocXacMinh').empty();
    $.get(`/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/LayDanhSachCoQuanNgauNhien/${MaDanhSachCanBoXacMinh}`, async (data) => {
        $("#LayNgauNhienDanhSachCoQuanDuocXacMinh").remove()
        await $.each(data.data1, async (index, value) => {
            await setTimeout(() => {
                $(`#${data.data1.Ma_CoQuan_DonVi}`).remove();
               $('#DanhSachCoQuanDuocXacMinh').append(`<tr>
                                                            <td></td>
                                                            <td>${value.Ten_Loai_CQDV}<input hidden value="${value.Ma_CoQuan_DonVi}" name="DanhSachCoQuan[]"/></td>
                                                            <td>${value.Ten}</td>
                                                            <td>${value.TongCanBo}</td>
                                                            <td>${value.TongLanhDao}</td>
                                                            <td>${value.TongChuyenVien}</td>
                                                            <td>${(parseInt)((value.TongCanBo) / 100 * 10) == 0 ? 1 : (parseInt)((value.TongCanBo) / 100 * 10) }</td>
                                                            <td><input class="form-control" name="LayLanhDao[]" id="LanhDaoInput_${index}" type="number" step="1" max="${value.TongLanhDao}" min="${(parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100) == 0 ? 1 : (parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100)}" value="${(parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100) == 0 ? 1 : (parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100) }" /></td>
                                                            <td><input class="form-control" name="LayNhanVien[]"  id="NhanVienInput_${index}" type="number" step="1" max="${value.TongCanBo - value.TongLanhDao}" min="0" /></td>
                                                            <td>${value.TrangThai == true ? "<span class='badge badge-success'>Đã lập kế hoạch kê khai</span>" : "<span class='badge badge-danger'>Chưa lập kế hoạch kê khai</span>"}</td>
                                                        </tr>
                                                        <tr id="${data.data1.Ma_CoQuan_DonVi}">
                                                            <td style="position: relative;"></td>
                                                            <td style="position: relative;"><svg id="loading1_${data.data1.Ma_CoQuan_DonVi}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: #fff0; display: block; width: 48px; position: absolute; left: 47%;;top: -6px;" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                                                                <circle cx="75" cy="50" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.9090909090909091s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.9090909090909091s"></animate>
                                                                </circle>
                                                                <circle cx="71.03133832077953" cy="63.51602043638994" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.8181818181818182s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.8181818181818182s"></animate>
                                                                </circle>
                                                                <circle cx="60.385375325047164" cy="72.74079988386296" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.7272727272727273s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.7272727272727273s"></animate>
                                                                </circle>
                                                                <circle cx="46.442129043167874" cy="74.74553604702332" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.6363636363636364s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.6363636363636364s"></animate>
                                                                </circle>
                                                                <circle cx="33.62848165136788" cy="68.89373935885646" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.5454545454545454s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.5454545454545454s"></animate>
                                                                </circle>
                                                                <circle cx="26.012675659637566" cy="57.04331392103574" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.45454545454545453s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.45454545454545453s"></animate>
                                                                </circle>
                                                                <circle cx="26.012675659637562" cy="42.95668607896427" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.36363636363636365s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.36363636363636365s"></animate>
                                                                </circle>
                                                                <circle cx="33.62848165136787" cy="31.106260641143546" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.2727272727272727s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.2727272727272727s"></animate>
                                                                </circle>
                                                                <circle cx="46.44212904316787" cy="25.254463952976682" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.18181818181818182s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.18181818181818182s"></animate>
                                                                </circle>
                                                                <circle cx="60.38537532504715" cy="27.259200116137038" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.09090909090909091s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.09090909090909091s"></animate>
                                                                </circle>
                                                                <circle cx="71.03133832077953" cy="36.48397956361006" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="0s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="0s"></animate>
                                                                </circle>
                                                            </svg></td>
                                                            <td style="position: relative;"><svg  id="loading1_${data.data1.Ma_CoQuan_DonVi}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: #fff0; display: block; width: 48px; position: absolute; left: 47%;;top: -6px;" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                                                                <circle cx="75" cy="50" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.9090909090909091s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.9090909090909091s"></animate>
                                                                </circle>
                                                                <circle cx="71.03133832077953" cy="63.51602043638994" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.8181818181818182s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.8181818181818182s"></animate>
                                                                </circle>
                                                                <circle cx="60.385375325047164" cy="72.74079988386296" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.7272727272727273s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.7272727272727273s"></animate>
                                                                </circle>
                                                                <circle cx="46.442129043167874" cy="74.74553604702332" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.6363636363636364s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.6363636363636364s"></animate>
                                                                </circle>
                                                                <circle cx="33.62848165136788" cy="68.89373935885646" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.5454545454545454s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.5454545454545454s"></animate>
                                                                </circle>
                                                                <circle cx="26.012675659637566" cy="57.04331392103574" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.45454545454545453s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.45454545454545453s"></animate>
                                                                </circle>
                                                                <circle cx="26.012675659637562" cy="42.95668607896427" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.36363636363636365s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.36363636363636365s"></animate>
                                                                </circle>
                                                                <circle cx="33.62848165136787" cy="31.106260641143546" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.2727272727272727s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.2727272727272727s"></animate>
                                                                </circle>
                                                                <circle cx="46.44212904316787" cy="25.254463952976682" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.18181818181818182s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.18181818181818182s"></animate>
                                                                </circle>
                                                                <circle cx="60.38537532504715" cy="27.259200116137038" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.09090909090909091s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.09090909090909091s"></animate>
                                                                </circle>
                                                                <circle cx="71.03133832077953" cy="36.48397956361006" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="0s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="0s"></animate>
                                                                </circle>
                                                            </svg></td>
                                                            
                                                        </tr>
                                                        
                                                        `)
                if ((parseInt)((parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100) - $(`#LanhDaoInput_${index}`).val()) < 0) {
                    $(`#NhanVienInput_${index}`).val(0)
                    console.log("nho hon 0")
                } else {
                    $(`#NhanVienInput_${index}`).val((parseInt)((parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100) - $(`#LanhDaoInput_${index}`).val()))
                 
                }
                
           }, 100 * index);
            
        })

        setTimeout(() => {
            $(`#${data.data1.Ma_CoQuan_DonVi}`).remove();
            $(`#next-page2`).prop('disabled', false);
        }, 100 * data.data1.length)
    })
    
})


// Lấy danh sách cơ quan đã chọn
$("#LayDanhSachCoQuanDuocXacMinhDaChon").click((e) => {
    var listCoQuanDaChon = [];
    $.each($("input[name='ID_CoQuan']:checked"), function () {
        console.log(this.value)
        listCoQuanDaChon.push(this.value);
    });
    
    
    console.log(listCoQuanDaChon)
    $.post(`/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/LayDanhSachCoQuanDaChon`, { listCoQuanDaChon: listCoQuanDaChon, MaDanhSachCanBoXacMinh: MaDanhSachCanBoXacMinh }, async (data) => {
        if (data.TrangThai == "true") {
            $('#DanhSachCoQuanDuocXacMinh').empty();
            $("#LayDanhSachCoQuanDuocXacMinhDaChon").remove()
            await $.each(data.data1, async (index, value) => {
                await setTimeout(() => {
                    $(`#${data.data1.Ma_CoQuan_DonVi}`).remove();
                    $('#DanhSachCoQuanDuocXacMinh').append(`<tr>
                                                            <td></td>
                                                            <td></td>
                                                            <td>${value.Ten_Loai_CQDV}<input hidden value="${value.Ma_CoQuan_DonVi}" name="DanhSachCoQuan[]"/></td>
                                                            <td>${value.Ten}</td>
                                                            <td>${value.TongCanBo}</td>
                                                            <td>${value.TongLanhDao}</td>
                                                            <td>${value.TongChuyenVien}</td>
                                                            <td>${(parseInt)((value.TongCanBo) / 100 * 10) == 0 ? 1 : (parseInt)((value.TongCanBo) / 100 * 10)}</td>
                                                            <td><input class="form-control" name="LayLanhDao[]" id="LanhDaoInput_${index}" type="number" step="1" max="${value.TongLanhDao}" min="${(parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100) == 0 ? 1 : (parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100)}" value="${(parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100) == 0 ? 1 : (parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100)}" /></td>
                                                            <td><input class="form-control" name="LayNhanVien[]"  id="NhanVienInput_${index}" type="number" step="1" max="${value.TongCanBo - value.TongLanhDao}" min="0" /></td>
                                                            <td>${value.TrangThai == true ? "<span class='badge badge-success'>Đã lập kế hoạch kê khai</span>" : "<span class='badge badge-danger'>Chưa lập kế hoạch kê khai</span>"}</td>
                                                        </tr>
                                                        <tr id="${data.data1.Ma_CoQuan_DonVi}">
                                                            <td style="position: relative;"></td>
                                                            <td style="position: relative;"><svg id="loading1_${data.data1.Ma_CoQuan_DonVi}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: #fff0; display: block; width: 48px; position: absolute; left: 47%;;top: -6px;" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                                                                <circle cx="75" cy="50" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.9090909090909091s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.9090909090909091s"></animate>
                                                                </circle>
                                                                <circle cx="71.03133832077953" cy="63.51602043638994" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.8181818181818182s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.8181818181818182s"></animate>
                                                                </circle>
                                                                <circle cx="60.385375325047164" cy="72.74079988386296" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.7272727272727273s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.7272727272727273s"></animate>
                                                                </circle>
                                                                <circle cx="46.442129043167874" cy="74.74553604702332" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.6363636363636364s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.6363636363636364s"></animate>
                                                                </circle>
                                                                <circle cx="33.62848165136788" cy="68.89373935885646" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.5454545454545454s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.5454545454545454s"></animate>
                                                                </circle>
                                                                <circle cx="26.012675659637566" cy="57.04331392103574" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.45454545454545453s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.45454545454545453s"></animate>
                                                                </circle>
                                                                <circle cx="26.012675659637562" cy="42.95668607896427" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.36363636363636365s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.36363636363636365s"></animate>
                                                                </circle>
                                                                <circle cx="33.62848165136787" cy="31.106260641143546" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.2727272727272727s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.2727272727272727s"></animate>
                                                                </circle>
                                                                <circle cx="46.44212904316787" cy="25.254463952976682" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.18181818181818182s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.18181818181818182s"></animate>
                                                                </circle>
                                                                <circle cx="60.38537532504715" cy="27.259200116137038" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.09090909090909091s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.09090909090909091s"></animate>
                                                                </circle>
                                                                <circle cx="71.03133832077953" cy="36.48397956361006" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="0s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="0s"></animate>
                                                                </circle>
                                                            </svg></td>
                                                            <td style="position: relative;"><svg  id="loading1_${data.data1.Ma_CoQuan_DonVi}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" style="margin: auto; background: #fff0; display: block; width: 48px; position: absolute; left: 47%;;top: -6px;" viewBox="0 0 100 100" preserveAspectRatio="xMidYMid">
                                                                <circle cx="75" cy="50" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.9090909090909091s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.9090909090909091s"></animate>
                                                                </circle>
                                                                <circle cx="71.03133832077953" cy="63.51602043638994" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.8181818181818182s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.8181818181818182s"></animate>
                                                                </circle>
                                                                <circle cx="60.385375325047164" cy="72.74079988386296" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.7272727272727273s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.7272727272727273s"></animate>
                                                                </circle>
                                                                <circle cx="46.442129043167874" cy="74.74553604702332" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.6363636363636364s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.6363636363636364s"></animate>
                                                                </circle>
                                                                <circle cx="33.62848165136788" cy="68.89373935885646" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.5454545454545454s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.5454545454545454s"></animate>
                                                                </circle>
                                                                <circle cx="26.012675659637566" cy="57.04331392103574" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.45454545454545453s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.45454545454545453s"></animate>
                                                                </circle>
                                                                <circle cx="26.012675659637562" cy="42.95668607896427" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.36363636363636365s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.36363636363636365s"></animate>
                                                                </circle>
                                                                <circle cx="33.62848165136787" cy="31.106260641143546" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.2727272727272727s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.2727272727272727s"></animate>
                                                                </circle>
                                                                <circle cx="46.44212904316787" cy="25.254463952976682" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.18181818181818182s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.18181818181818182s"></animate>
                                                                </circle>
                                                                <circle cx="60.38537532504715" cy="27.259200116137038" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="-0.09090909090909091s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="-0.09090909090909091s"></animate>
                                                                </circle>
                                                                <circle cx="71.03133832077953" cy="36.48397956361006" fill="#93e6d6" r="5">
                                                                    <animate attributeName="r" values="3;3;5;3;3" times="0;0.1;0.2;0.3;1" dur="1s" repeatCount="indefinite" begin="0s"></animate>
                                                                    <animate attributeName="fill" values="#93e6d6;#93e6d6;#17a2b8;#93e6d6;#93e6d6" repeatCount="indefinite" times="0;0.1;0.2;0.3;1" dur="1s" begin="0s"></animate>
                                                                </circle>
                                                            </svg></td>
                                                            
                                                        </tr>
                                                        
                                                        `)
                    if ((parseInt)((parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100) - $(`#LanhDaoInput_${index}`).val()) < 0) {
                        $(`#NhanVienInput_${index}`).val(0)
                        console.log("nho hon 0")
                    } else {
                        $(`#NhanVienInput_${index}`).val((parseInt)((parseInt)((parseInt)(value.TongCanBo * 10 / 100) * 10 / 100) - $(`#LanhDaoInput_${index}`).val()))

                    }

                }, 100 * index);

            })

            setTimeout(() => {
                $(`#${data.data1.Ma_CoQuan_DonVi}`).remove();
                $(`#next-page2`).prop('disabled', false);
            }, 100 * data.data1.length)
        } else if (data.TrangThai == "false") {
            Swal.fire({
                icon: 'error',
                title: 'Thất bại',
                text: `Bạn Chưa Chọn Cơ Quan - Đơn Vị nào`,
                timer: 2000,
                showConfirmButton: false,
            })
        } else if (data.TrangThai == "cao") {
            Swal.fire({
                icon: 'error',
                title: 'Thất bại',
                text: `Đã vượt quá 20% Cơ Quan - Đơn Vị, vui lòng kiểm tra lại`,
                timer: 2000,
                showConfirmButton: false,
            })
        } else if (data.TrangThai == "thap") {
            Swal.fire({
                icon: 'error',
                title: 'Thất bại',
                text: `Chưa đủ 20% Cơ Quan - Đơn Vị, vui lòng kiểm tra lại`,
                timer: 2000,
                showConfirmButton: false,
            })
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Thất bại',
                text: `Lỗi Hệ thống`,
                timer: 2000,
                showConfirmButton: false,
            })
        }
        
    })


})


var danhsachCanBoRandom
  //jQuery time
var current_fs, next_fs, previous_fs; //fieldsets
var left, opacity, scale; //fieldset properties which we will animate
var animating; //flag to prevent quick multi-click glitches

 $(".next").click(async () => {
    //var arraysCoQuan = []
    //var arraysSoLanhDao = []
    //var arraysSoNhanVien = []
    //$("input[name='DanhSachCoQuan[]']").each(function () {
    //    arraysCoQuan.push($(this).val());
    //})
    //$("input[name='LayLanhDao[]']").each(function () {
    //    arraysSoLanhDao.push($(this).val());
    //})
    //$("input[name='LayNhanVien[]']").each(function () {
    //    arraysSoNhanVien.push($(this).val());
    //})
    //var t = arraysCoQuan.join().toString()
    //var SoLanhDao = arraysSoLanhDao.join().toString()
    //var SoNhanVien = arraysSoNhanVien.join().toString()
    //DSNgauNhienCoQuan = t;
    //console.log(t)
    //$.ajax({
    //    url: `/NV_LapKeHoachXacMinh_DanhSachCanBoXacMinh/LayDanhSachCanBoNgauNhien?MaDanhSachCanBoXacMinh=${MaDanhSachCanBoXacMinh}&DanhSachCoQuan=${t}&SoLanhDao=${SoLanhDao}&SoNhanVien=${SoNhanVien}`,
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    type: "POST",
    //    beforeSend: function () {
           
    //    },
    //    success: async function (response) {
    //        console.log("nextpage success")
    //        if (animating) return false;
    //        animating = true;

    //        current_fs = $(this).parent();
    //        next_fs = $(this).parent().next();

    //        //activate next step on progressbar using the index of next_fs
    //        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

    //        //show the next fieldset
    //        next_fs.show();
    //        //hide the current fieldset with style
    //        current_fs.animate({
    //            opacity: 0
    //        }, {
    //            step: function (now, mx) {
    //                //as the opacity of current_fs reduces to 0 - stored in "now"
    //                //1. scale current_fs down to 80%
    //                scale = 1 - (1 - now) * 0.2;
    //                //2. bring next_fs from the right(50%)
    //                left = (now * 50) + "%";
    //                //3. increase opacity of next_fs to 1 as it moves in
    //                opacity = 1 - now;
    //                current_fs.css({
    //                    'transform': 'scale(' + scale + ')',
    //                    'position': 'absolute'
    //                });
    //                next_fs.css({
    //                    'left': left,
    //                    'opacity': opacity
    //                });
    //            },
    //            duration: 800,
    //            complete: function () {
    //                current_fs.hide();
    //                animating = false;
    //            },
    //            //this comes from the custom easing plugin
    //            easing: 'easeInOutBack'
    //        });
    //        if (response.TrangThai == "true") {

    //            danhsachCanBoRandom = await response.listcanbo;
    //        } else if (response.TrangThai = "fasle") {
    //            Swal.fire({
    //                icon: 'error',
    //                title: 'Thất Bại',
    //                text: 'Vui lòng nhập số lượng Lãnh Đạo',
    //                timer: 2000,
    //                showConfirmButton: false,
    //            }).then(() => {

    //            })
    //        } else if (response.TrangThai == "LanhDaochuadu10phantram") {
    //            Swal.fire({
    //                icon: 'error',
    //                title: 'Thất Bại',
    //                text: 'Số lượng danh sách Lãnh Đạo xác minh chưa đủ 10%',
    //                timer: 2000,
    //                showConfirmButton: false,
    //            }).then(() => {

    //            })
    //        } else if (response.TrangThai == "canbochuadu10phantram"){
    //            Swal.fire({
    //                icon: 'error',
    //                title: 'Thất Bại',
    //                text: 'Số lượng danh sách cán bộ xác minh chưa đủ 10% ',
    //                timer: 2000,
    //                showConfirmButton: false,
    //            }).then(() => {

    //            })
    //        }
           
    //    },
    //    error: function (response) {
    //        Swal.fire({
    //            icon: 'error',
    //            title: 'Thất Bại',
    //            text: 'Lỗi hệ thống',
    //            timer: 2000,
    //            showConfirmButton: false,
    //        }).then(() => {
              
    //        })
    //    }
    //});
})


$(document).ready( function () {
  
})
