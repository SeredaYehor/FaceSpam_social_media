$(document).ready(function () {

    var formData = new FormData();

    $(".HeartImage").click(function ChangeColor() {
        if ($(this).attr("src") == "/images/heartRed.svg") {
            $(this).attr("src", "/images/heart.svg");
        }
        else {
            $(this).attr("src", "/images/heartRed.svg");
        }
        var id = $(this).attr("id");
        var likes = UpdateLikes(id);
        $(this).parent().children(".LikeLabel").html(likes);
    })

    $("#Browse").click(function () {
        document.getElementById('myFile').click();
    })

    $("#Write").click(function () {
        var text = $(".Post").val();
        formData.set('text', text);
        $.ajax({
            type: "POST",
            url: '/Home/AddPost',
            data: formData,
            contentType: false,
            processData: false,
            success: function () {
                alert("Ok");
            }
        });
    })

    $("#myFile").change(function () {
        var path = document.getElementById("myFile").files[0];
        var tb_text = $(".Post").val();
        formData.set('file', path);
    })

    function UpdateLikes(id) {
        var result = 0;
        $.ajax({
            type: "GET",
            url: '/Home/ChangeLike',
            data: { postId: id, },
            async: false,
            success: function (likes) {
                result = likes;
            }
        });
        return result;
    }
});