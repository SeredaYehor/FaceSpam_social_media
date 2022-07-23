$(document).ready(function () {
    var formData = new FormData();

    formData.set('executorId', executorId);

    $(".Upload").click(function () {
        document.getElementById('myFile').click();
    })


    $("#myFile").change(function () {
        const path = document.getElementById("myFile").files[0];
        $(".ChangePhotoImage").empty();
        if (path) {
            formData.set('file', path);
            const fileReader = new FileReader();
            fileReader.readAsDataURL(path);
            fileReader.addEventListener("load", function () {
                var image = '<img src="' + fileReader.result + '" />';
                $(".ChangePhotoImage").append(image);
            });
        }
    })

    $(".ConfirmButton").click(function () {
        $.ajax({
            type: "POST",
            url: '/Home/GetPhotoUrl',
            async: false,
            data: formData,
            contentType: false,
            processData: false
        });
    });
})
