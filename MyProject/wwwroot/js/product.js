var product = {
    init: function () {
        product.load();
        product.create();
    },
    load: function () {
        $(document).ready(function () {

            $.ajax({
                url: '/brand/getbrands',
                dataType: 'json',
                type: 'GET',
                success: function (response) {
                    var data = response;
                    var html = '';
                    $.each(data, function (i, item) {
                        html += "<option value=" + item.id + ">" + item.name + "</option>";
                    });
                    $('#brand-id').html(html);
                }
            });

        });
    },
    create: function () {
        $(document).ready(function () {
            $('#create-product').on('click', function () {
                $('#modal-create').modal("show");
                
            });

        });
    },
    update: function () {
        $(document).ready(function () {
            $('#create-product').on('click', function () {
                $('#modal-create').modal("show");
                $.ajax({
                    url: '/product/',
                    dataType: 'json',
                    type: 'GET',
                    success: function (response) {
                        var data = response;
                        var html = '';
                        $.each(data, function (i, item) {
                            html += "<option value=" + item.id + ">" + item.name + "</option>";
                        });
                        $('#brand-id').html(html);
                    }
                });
            });

        });
    }
}
product.init();