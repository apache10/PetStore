var dataTable; 

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    console.log("test", dataTable)
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/pet/getall' },
        "columns": [
            { data: 'name', "width": "30%" },
            { data: 'type', "width": "10%" },
            { data: 'dob', render: DataTable.render.date() ,"width": "10%" },
            { data: 'weight', render: $.fn.dataTable.render.number(',', '.', 2, '',' kg'), "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/pet/edit/${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                     <a onClick=Delete('/pet/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: 'Are you sure you want to delete this pet?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}