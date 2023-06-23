
var t;

function loadDataTable() {

    t = $("#dataTable").DataTable({
        "lengthChange": false,
        "info": false,

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

        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/NV_GiaoNhanBanKeKhai/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "TenKeHoachKeKhai" },
            { "data": "TenKeHoachKeKhai" },
            { "data": "KeHoachNam" },

            {
                "data": { "NghiDinh": "NghiDinh", "MaKeHoachKeKhai": "MaKeHoachKeKhai" }, "render": function (data, type, row, meta) {
                    return `<a class="btn btn-outline-info btn-sm" href="/NV_LapKeHoachKeKhai/Download?id=${data.MaKeHoachKeKhai}">
                                <i class="fa fa-download" aria-hidden="true"></i> ${data.NghiDinh}
                            </a>`
                }
            },
            {
                "data": "MaKeHoachKeKhai", "render": function (data, type, row) {
                    return ` <a style="cursor:pointer" class="btn-edit" pattern="edit" href="/NV_GiaoNhanBanKeKhai/GiaoNhanBanKeKhai/${data}"><span class="fa fa-pager"></span> Xem chi tiết</a>`
                }
            },
        ]

    });
    t.on('draw.dt', function () {
        var info = t.page.info();
        t.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });

}




$(document).ready(function () {
    loadDataTable();

})
