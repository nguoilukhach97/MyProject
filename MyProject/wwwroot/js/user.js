var user = {
    init: function () {
        user.delete();
        user.load();
        user.update();
    },
    load: function () {
        $(document).ready(function () {

        });
    },
    delete: function () {
        $(document).ready(function () {
            $('.delete-user').off('click').on('click', function () {
                var id = $(this).data("id");
                $('#id-delete').val(id);
                $('#modal-delete').modal('show');
            });
        });
    },
    update: function () {
        $(document).ready(function () {
            $('.update-user').off('click').on('click', function () {
                var id = $(this).data("id");
                $.ajax({
                    url: "/user/GetUserById",
                    data: { id:id },
                    dataType: "json",
                    type: "GET",
                    success: function (response) {
                        var data = response;
                        $('#id').val(id);
                        $('#first-name').val(data.firstName);
                        $('#last-name').val(data.lastName);
                        var date = new Date(data.dob).toISOString().split('T')[0];
                        
                        $('#dob').val(date);
                        $('#email').val(data.email);
                        $('#phone-number').val(data.phoneNumber);
                    }
                });
                $('#modal-update').modal('show');
            });
        });
    }
}
user.init();