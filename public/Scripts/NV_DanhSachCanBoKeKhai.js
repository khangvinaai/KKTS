

// kết thúc chỉnh sửa
var dt;


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

        //Hoàng chỉnh sửa option tỉnh, huyện 23-2-2022
        "serverSide": true,
        "processing": true,
        "ajax": {
            "url": "/NV_LapKeHoachKeKhai/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "MaKeHoachKeKhai" },
            {
                "data": { "TenKeHoachKeKhai": "TenKeHoachKeKhai", "MaKeHoachKeKhai": "MaKeHoachKeKhai" }, "render": function (data, type, row, meta) {
                    return `<a  href="/NV_DanhSachCanBoKeKhai/ThemChiTiet_CanBoKeKhai?id=${data.MaKeHoachKeKhai}">
                                ${data.TenKeHoachKeKhai}
                            </a>`
                }
            },

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
                "data": { "NghiDinh": "NghiDinh", "MaKeHoachKeKhai": "MaKeHoachKeKhai" }, "render": function (data, type, row, meta) {
                    return `<a class="btn btn-outline-info btn-sm" href="/NV_LapKeHoachKeKhai/Download?id=${data.MaKeHoachKeKhai}">
                                <i class="fa fa-download" aria-hidden="true"></i> ${data.NghiDinh}
                            </a>`
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
    //Kết thúc chỉnh sửa  23-2-2022
}




$(document).ready(function () {
    loadDataTable();
    
})


