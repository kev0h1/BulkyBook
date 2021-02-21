var dataTable;

$(document).ready(function () {
    loadDataTable();
})
//ajax defines the way the data will be retrieved - in this case we specify a url

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },
        //for datatables variables are in camel case to that is why the name variable is in lowercase
        "columns": [
            { "data": "name", "width": "60%" },
            //rows are added with curly braces
            {
                "data": "id", 
                "render": function (data) {
                    //to type in html we need ` quote
                    //${} needed to refer to variable
                    return `
                        <div class="text-center">
                            <a href="/Admin/Category/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i> &nbsp;
                            </a>
                            <a onclick=Delete("/Admin/Category/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash-alt"></i> &nbsp;
                            </a>
                        </div>
                    `;
                }, "width":"40%"
            }
        ]
    });

}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}