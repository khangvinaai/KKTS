var dt;
var XEM = false;
var XUAT = false;
var XEMCHITIET = false;

function loadDataTable() {
    dt = $("#dataTable").DataTable({
        "lengthChange": false,
        "info": false,
        "searching": true,
        "language": {
            "search": "",
            "info": "Tổng số _TOTAL_ hàng",
            "infoEmpty": "",
            "infoFiltered": "",
            "paginate": {
                "next": "»",
                "previous": "«"
            },
            "processing": `Đang tải dữ liệu`,
            searchPlaceholder: "Tìm...",
            zeroRecords: "Không tìm thấy kết quả",

        },
        dom: 'Bfrtip',
        buttons: [
            {
                text: '<i class="fa fa-file-excel"></i>',
                extend: 'excel',
                className: 'btn btn-outline-primary btn-sm mt-2 ml-3'
            },
            {
                text: '<i class="fa fa-file-pdf"></i>',
                extend: 'pdf',
                className: 'btn btn-outline-primary btn-sm mt-2'
            },
            {
                text: '<i class="fa fa-print"></i>',
                extend: 'print',
                className: 'btn btn-outline-primary btn-sm mt-2'
            }
        ],

        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/NV_BaoCaoTienDo/LoadDataKHKK",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [

            { className: "cn", "targets": [6] }
        ],
        "columns": [
            { "data": "MaKeHoachKeKhai" },
            { "data": "TenKeHoachKeKhai" },

            { "data": "KeHoachNam" },
            {
                "data": "ThoiGianBatDau", "render": function (data, type, row, meta) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            {
                "data": "ThoiGianKetThuc", "render": function (data, type, row, meta) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            {
                "data": { "DaKeKhai": "DaKeKhai", "TrangThaiTonTaiDS":"TrangThaiTonTaiDS"}, "render": function (data, type, row, meta) {
                    return `<div class="progress" style="height:28px; border-radius: 3px;">
                                <div class="progress-bar"
                                    style="width:${Math.round((100 * data.DaKeKhai) / data.TrangThaiTonTaiDS)}%;">
                                    ${Math.round((100 * data.DaKeKhai) / data.TrangThaiTonTaiDS)}%
                                </div>
                            </div>`
                }
            },
            
            {
                "data": "MaKeHoachKeKhai", "render": function (data, type, row) {
                    var row_xemchitiet = ``
                    if (XEMCHITIET) {
                        row_xemchitiet = `<a class="dropdown-item" href="/ket-qua-ke-khai/xem-chi-tiet/${data}" >
                                                Xem Chi Tiết
                                            </a> `
                    }
                    if (row_xemchitiet != ``) {
                        return `<div class="dropleft">
                                          <button class="btn btn-outline-info btn-sm" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                            <i class="fa-solid fa-ellipsis-vertical"></i>
                                          </button>
                                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                              ${row_xemchitiet}
                                           </div>
                                        </div>`
                    }
                    else {
                        return `<div class="dropleft">
                                          <button class="btn btn-outline-info btn-sm disabled" disabled type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding: 4px 13px;">
                                            <i class="fa-solid fa-ellipsis-vertical"></i>
                                          </button>
                                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                              ${row_xemchitiet}
                                           </div>
                                        </div>`
                    }
                   
                }
            },
        ]
    });
    dt.on('draw.dt', function () {
        var info = dt.page.info();
        dt.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });
    if (!XUAT) {
        dt.buttons().disable();
    }
}

async function CheckQuyen() {
    await $.get("/Home/GetQuyen", { MenuCode: "NV_KetQuaKeKhai" }, (data) => {
        if (data.includes("XEM")) {
            XEM = true;
        }
        if (data.includes("XUAT")) {
            XUAT = true;
        }
        if (data.includes("XEMCHITIET")) {
            XEMCHITIET = true;
        }
    })
}
$(document).ready(async function () {
    await CheckQuyen();
    loadDataTable();
})

