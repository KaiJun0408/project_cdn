$(document).ready(function () {

    loadList = function () {
        var username = ($('#username').val() ?? null);
        var email = ($('#email').val() ?? null);
        var phone = ($('#phone').val() ?? null);
        var skillsets = ($('#skillsets').val() ?? null);
        var hobby = ($('#hobby').val() ?? null);

        var filter = [username, email, phone, skillsets, hobby];

        $('#user_tbl').DataTable({
            destroy: true,
            "ajax": {
                "url": "/User/Listing",
                "traditional": true,
                "data": { 'filter': filter },
                "type": "GET",
                "datatype": "json",
                "dataSrc": "",
            },
            "columns": [
                { "data": "id" },
                { "data": "username" },
                { "data": "email" },
                { "data": "phone" },
                { "data": "skillsets" },
                { "data": "hobby" },
                {
                    data: function (row, type, set) {
                        var r = `<a href="Edit" class="btn btn-sm btn-clean btn-icon" title="Edit details">
                                            Edit
                                        </a>`;

                        r += `<a onclick="confirmationDeleteUser(${row.id})" class="btn btn-sm btn-clean btn-icon" title="Delete">
                                            Delete
                                        </a>`;


                        return r;
                    }
                }
            ]
        });
    }

    loadList();

    $("#searchBtn").click(function () {
        loadList();
    });
});

function confirmationDeleteUser(id) {
    Swal.fire({
        title: 'Are you sure want to delete this user?',
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#47BE7D',
        confirmButtonText: 'Delete'
    }).then((result) => {
        if (result.isConfirmed) {
            deleteUser(id);
        }
    })
}

function deleteUser(id) {
    $.ajax({
        type: 'POST',
        url: 'User/DeleteUser',
        data: { id: id },
        datatype: 'json',
        success: function (response) {
            Swal.fire({
                title: response.msg,
                icon: 'success',
                showCancelButton: false,
                confirmButtonColor: '#47BE7D',
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.isConfirmed) {
                    location.reload();
                }
            })
        },
        error: function (msg) {
            alert(msg.responseText);
        }
    });
}