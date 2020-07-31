var user = {
    init: function () {
        user.delete();
        user.update();
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
                
                $('#modal-update').modal('show');
            });
        });
    }
}
user.init();