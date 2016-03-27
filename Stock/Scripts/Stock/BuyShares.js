$.connection.hub.start().done(function () {
    $(document).ready(function () {
        $('.btn').click(function () {
            console.log('clicked');
        });
    });
});

