
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
            "url": "/NV_TienHanhXacMinh/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "TenKeHoachXacMinh" },
            { "data": "TenKeHoachXacMinh" },
            { "data": "KeHoachNam" },

            { "data": "CanCuQuyetDinh" },

            { "data": "NoiDungXacMinh" },
            { "data": "TenTienDo" },


            {
                "data": "ID", "render": function (data, type, row) {
                    return ` <a style="cursor:pointer" class="btn-edit" pattern="edit" href = "/NV_TienHanhXacMinh/TienHanhXacMinh/${data}"><span class="fa fa-pager"></span> Xem chi tiết</a>
                             
                                    `

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
